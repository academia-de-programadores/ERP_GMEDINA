//VARIABLES PARA INACTIVACION Y ACTIVACION DE REGISTROS
var IDInactivar = 0, IDActivar = 0;
//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })

//FUNCION GENERICA PARA REUTILIZAR AJAX
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        type: type,
        data: { params },
        success: function (data) {
            callback(data);
        }
    });
}

//EVITAR POSTBACK DE FORMULARIOS
$('#frmCrearTechoComisiones').submit(function (e) {
    return false;
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridTechoComisiones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/TechosComisiones/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaTeC = data;
            $('#tblTechoCom').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTeC.length; i++) {
                var Estado = ListaTeC[i].tc_Estado == true ? 'Activo' : 'Inactivo';

                var botonDetalles = '<button data-id = "' + ListaTeC[i].tc_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetallesTechosComisiones">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaTeC[i].tc_Estado == true ? '<button data-id = "' + ListaTeC[i].tc_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarTechosComisiones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaTeC[i].tc_Estado == false ? esAdministrador == "1" ? '<button data-id = "' + ListaTeC[i].tc_Id + '" type="button" class="btn btn-default btn-xs"  id="btnActivarTechosComisiones">Activar</button>' : '' : '';

                $('#tblTechoCom').dataTable().fnAddData([
                    ListaTeC[i].tc_Id,
                     ListaTeC[i].cin_DescripcionIngreso,
                    (ListaTeC[i].tc_RangoInicio % 1 == 0) ? ListaTeC[i].tc_RangoInicio + ".00" : ListaTeC[i].tc_RangoInicio,
                    (ListaTeC[i].tc_RangoFin % 1 == 0) ? ListaTeC[i].tc_RangoFin + ".00" : ListaTeC[i].tc_RangoFin,
                    (ListaTeC[i].tc_PorcentajeComision % 1 == 0) ? ListaTeC[i].tc_PorcentajeComision + ".00" + '%' : ListaTeC[i].tc_PorcentajeComision + '%',
                   Estado,
                   botonDetalles + botonEditar + botonActivar]
                   );
            }
        });
    (FullBody)
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnCreateTechoComision", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosComisiones/Create");

    if (validacionPermiso.status == true) {
        //LLAMAR LA FUNCION PARA OCULTAR LAS VALIDACIONES
        OcultarValidacionesCrear();
        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCrearTechoComis").attr("disabled", false);
        //FUNCION PARA CARGAR EL INGRESO
        $.ajax({
            url: "/TechosComisiones/EditGetDDLIngreso",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
            .done(function (data) {
                $("#Crear #cin_IdIngreso").empty();
                $("#Crear #cin_IdIngreso").append("<option value='0'>Seleccione una opción...</option>");
                $.each(data, function (i, iter) {
                    $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });
        //MOSTRAR EL MODAL DE AGREGAR
        $("#CrearTechoComision").modal({ backdrop: 'static', keyboard: false });
    }
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCrearTechoComis').click(function () {
    //CAPTURAR LOS VALORES DEL FORMULARIO
    var IdIngreso = $("#Crear #cin_IdIngreso").val();
    var Inicio = $("#Crear #tc_RangoInicio").val();
    var Fin = $("#Crear #tc_RangoFin").val();
    var Porcentaje = $("#Crear #tc_PorcentajeComision").val();
    //FUNCION PARA FORMATEAR LOS VALORES NUMERICOS
    var response = FormatearNumericos(Inicio, Fin, Porcentaje);
    InicioFormateado = parseFloat(response[0]);
    FinFormateado = parseFloat(response[1]);
    // VALIDAR EL MODEL STATE DEL FORMULARIO
    if (ValidarCamposCrear(IdIngreso, Inicio, Fin, Porcentaje)) {
        if (InicioFormateado > FinFormateado) {
                iziToast.error({
                    title: 'Error',
                    message: 'El Rango Fin debe ser mayor que el Rango Inicio',
                });
            } else {
                //BLOQUEAR EL BOTON DE CREAR
                $("#btnCrearTechoComis").attr("disabled", true);
                var data = {
                    cin_IdIngreso: IdIngreso,
                    tc_RangoInicio: response[0],
                    tc_RangoFin: response[1],
                    tc_PorcentajeComision: response[2]
                };
                //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
                $.ajax({
                    url: "/TechosComisiones/Create",
                    method: "POST",
                    data: data
                }).done(function (data) {
                    //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
                    $("#CrearTechoComision").modal('hide');
                    OcultarValidacionesCrear();
                    $("#btnCrearTechoComis").attr("disabled", false);

                    if (data == "error") {
                        iziToast.error({
                            title: 'Error',
                            message: 'No se guardó el registro, contacte al administrador',
                        });
                    }
                    else {
                        //CERRAR EL MODAL DE AGREGAR
                        $("#CrearTechoComision").modal('hide');
                        OcultarValidacionesCrear();
                        $("#btnCrearTechoComis").attr("disabled", false);
                        cargarGridTechoComisiones();
                        // Mensaje de exito cuando un registro se ha guardado bien
                        iziToast.success({
                            title: 'Éxito',
                            message: '¡El registro se agregó de forma exitosa!',
                        });
                    }
                });
            }
    }
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(IdIngreso, Inicio, Fin, Porcentaje) {
    var pasoValidacion = true;
    //var ValidarInicio = false;
    //var ValidarFin = false;
    var response = FormatearNumericos(Inicio, Fin, Porcentaje);
    if (IdIngreso != "-1") {

        if (IdIngreso <= 0 || isNaN(IdIngreso)) {
            
            pasoValidacion = false;
            $('#Crear #Validation_IdIngreso').show();
            $("#Crear #ComisionAsterisco").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_IdIngreso').hide();
            $("#Crear #ComisionAsterisco").removeClass("text-danger");
        }
    }

    //VALIDACION DEL RANGO INICIO
    if (Inicio != "-1") {
        InicioFormateado = parseFloat(response[0]);
        

        //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (InicioFormateado == null || InicioFormateado == '' || InicioFormateado == undefined || isNaN(InicioFormateado)) {
            pasoValidacion = false;
            $('#Crear #RangoInicio_Validation2').hide();
            $('#Crear #RangoInicio_Validation').show();
            $("#Crear #InicioAsterisco").addClass("text-danger");
        } else {
            $('#Crear #RangoInicio_Validation').hide();
            $("#Crear #InicioAsterisco").removeClass("text-danger");
            if (InicioFormateado <= 0) {
                pasoValidacion = false;
                $('#Crear #RangoInicio_Validation').hide();
                $("#Crear #InicioAsterisco").addClass("text-danger");
                $('#Crear #RangoInicio_Validation2').show();
            } else {
                ValidarInicio = true;
                $("#Crear #InicioAsterisco").removeClass("text-danger");
                $('#Crear #RangoInicio_Validation').hide();
                $('#Crear #RangoInicio_Validation2').hide();
            }
        }
    }

    //VALIDACION DEL RANFO FIN
    if (Fin != "-1") {
        FinFormateado = parseFloat(response[1]);

        //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (FinFormateado == null || FinFormateado == '' || FinFormateado == undefined || isNaN(FinFormateado)) {
            pasoValidacion = false;
            $('#Crear #RangoFin_Validation2').hide();
            $('#Crear #RangoFin_Validation').show();
            $("#Crear #FinAsterisco").addClass("text-danger");
        } else {
            $('#Crear #RangoFin_Validation').hide();
            $("#Crear #FinAsterisco").removeClass("text-danger");
            if (FinFormateado <= 0) {
                pasoValidacion = false;
                //$('#Editar #RangoFin_Validation').hide();
                $("#Crear #FinAsterisco").addClass("text-danger");
                $('#Crear #RangoFin_Validation2').show();
            } else {
                ValidarFin = true;
                $("#Crear #FinAsterisco").removeClass("text-danger");
                $('#Crear #RangoFin_Validation').hide();
                $('#Crear #RangoFin_Validation2').hide();
            }
        }
    }

    //VALIDACION DEL PORCENTAJE
    if (Porcentaje != "-1") {
        PorcentajeFormateado = parseFloat(response[2]);

        //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (PorcentajeFormateado == null || PorcentajeFormateado == '' || PorcentajeFormateado == undefined || isNaN (PorcentajeFormateado)) {
            pasoValidacion = false;
            $('#Crear #Porcentaje_Validation2').hide();
            $('#Crear #Porcentaje_Validation').show();
            $("#Crear #PorcentajeAsterisco").addClass("text-danger");
        } else {
            $('#Crear #Porcentaje_Validation').hide();
            $("#Crear #PorcentajeAsterisco").removeClass("text-danger");
            if (PorcentajeFormateado <= 0) {
                pasoValidacion = false;
                //$('#Editar #Porcentaje_Validation').hide();
                $("#Crear #PorcentajeAsterisco").addClass("text-danger");
                $('#Crear #Porcentaje_Validation2').show();
            } else {
                $("#Crear #PorcentajeAsterisco").removeClass("text-danger");
                $('#Crear #Porcentaje_Validation').hide();
                $('#Crear #Porcentaje_Validation2').hide();
            }
        }
    }
    return pasoValidacion;
}

//CERRAR EL MODAL DE CREAR
$("#btnCerrarCrear").click(function () {
    //OCULTAR EL MODAL DE CREACION
    $("#CrearTechoComision").modal("hide");
    //CULTAR LAS VALIDACIONES DE CREAR
    OcultarValidacionesCrear();
    $("#btnCrearTechoComis").attr("disabled", false);
});

//FUNCION: OCULTAR LAS VALIDACIONES AL CREAR
function OcultarValidacionesCrear() {
    //ASTERISCOS
    $("#Crear #ComisionAsterisco").removeClass("text-danger");
    $("#Crear #PorcentajeAsterisco").removeClass("text-danger");
    $("#Crear #InicioAsterisco").removeClass("text-danger");
    $("#Crear #FinAsterisco").removeClass("text-danger");

    $('#Crear #Validation_IdIngreso').hide();
    $('#Crear #Porcentaje_Validation').hide();
    $('#Crear #Porcentaje_Validation2').hide();
    $('#Crear #RangoInicio_Validation').hide();
    $('#Crear #RangoInicio_Validation2').hide();
    $('#Crear #RangoFin_Validation').hide();
    $('#Crear #RangoFin_Validation2').hide();

    $("#Crear #cin_IdIngreso").children("option:selected").val(0);
    //$("#Crear #cin_IdIngreso").val() = 0;
    $("#Crear #tc_RangoInicio").val('');
    $("#Crear #tc_RangoFin").val('');
    $("#Crear #tc_PorcentajeComision").val('');
}

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblTechoCom tbody tr td #btnEditarTechosComisiones", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosComisiones/Edit");

    if (validacionPermiso.status == true) {
        //OCULTAR VALIDACIONES
        OcultarValidacionesEditar();

        $("#Editar #cin_IdIngreso").empty();
        var ID = $(this).data('id');
        IDInactivar = ID;

        var idIngresoSelected = "", DescripcionIngresoSelected = "";

        $.ajax({
            url: "/TechosComisiones/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        }).done(function (data) {

            var SelectedIdIng = data.cin_IdIngreso;
            DescripcionIngresoSelected = data.cin_DescripcionIngreso;
            //LLENAR EL DROPDOWNLIST
            $("#Editar #cin_IdIngreso").append("<option value='" + idIngresoSelected + "' selected>" + DescripcionIngresoSelected + "</option>");

            $.ajax({
                url: "/TechosComisiones/Edit/" + ID,
                method: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: ID })
            }).done(function (data) {
                if (data) {
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL

                    $.ajax({
                        url: "/EmpleadoComisiones/EditGetDDLIngreso",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
              .done(function (data) {
                  //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                  $("#Editar #cin_IdIngreso").empty();
                  //LLENAR EL DROPDOWNLIST
                  $.each(data, function (i, iter) {
                      $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdIng ? " selected" : "") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                  });

              });
                    $("#Editar #tc_Id").val(data.tc_Id);
                    $("#Editar #tc_RangoInicio").val(data.tc_RangoInicio);
                    $("#Editar #tc_RangoFin").val(data.tc_RangoFin);
                    $("#Editar #tc_PorcentajeComision").val(data.tc_PorcentajeComision);
                    //MOSTRAR EL MODAL Y BLOQUEAR EL FONDO
                    $("#EditarTechoComision").modal({ backdrop: 'static', keyboard: false });
                }
            })

        }).fail(function (jqXHR, textStatus, error) {
            iziToast.error({
                title: 'Error',
                message: 'No se cargó la información de la comisión, contacte al administrador',
            });
        });
    }
});

//FUNCION: OCULTAR EL MODAL DE EDITAR Y MOSTRAR EL MODAL DE CONFIRMACION
$("#btnUpdateTechosComisiones").click(function () {

    var IdIngreso = $("#Editar #cin_IdIngreso").children("option:selected").val();
    var Inicio = $("#Editar #tc_RangoInicio").val();
    var Fin = $("#Editar #tc_RangoFin").val();
    var Porcentaje = $("#Editar #tc_PorcentajeComision").val();

    var response = FormatearNumericos(Inicio, Fin, Porcentaje);
    InicioFormateado = parseFloat(response[0]);
    FinFormateado = parseFloat(response[1]);

    //DESBLOQUEAR EL BOTON DE EDICION
    $("#btnConfirmarEditar").attr("disabled", false);
    //VALIDAR EL FORMULARIO
    if (ValidarCamposEditar(IdIngreso, Inicio, Fin, Porcentaje)) {
        if (InicioFormateado > FinFormateado) {
            iziToast.error({
                title: 'Error',
                message: 'El Rango Fin debe ser mayor que el Rango Inicio',
            });
        } else {
            //OCULTAR EL MODAL DE EDICION
            $("#EditarTechoComision").modal('hide');
            //DESPLEGAR EL MODAL DE CONFIRMACION
            document.getElementById("btnConfirmarEditar").disabled = false;
            $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
        }
    }
});

//FUNCION: FORMATEAR LOS CAMPOS NUMERICOS
function FormatearNumericos(Inicio, Fin, Porcentaje) {
    var indicesInicio = Inicio.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var InicioFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indicesInicio.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        InicioFormateado += indicesInicio[i];
    }
    //FORMATEAR A DECIMAL
    InicioFormateado = parseFloat(InicioFormateado);

    var indicesFin = Fin.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var FinFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indicesFin.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        FinFormateado += indicesFin[i];
    }
    //FORMATEAR A DECIMAL
    FinFormateado = parseFloat(FinFormateado);

    var indicesPorcentaje = Porcentaje.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var PorcentajeFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indicesPorcentaje.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        PorcentajeFormateado += indicesPorcentaje[i];
    }
    //FORMATEAR A DECIMAL
    PorcentajeFormateado = parseFloat(PorcentajeFormateado);
    
    var response = [InicioFormateado, FinFormateado, PorcentajeFormateado];
    return response;
}

//FUNCION: EJECUTAR EDICION DE REGISTROS
$("#btnConfirmarEditar").click(function () {
    document.getElementById("btnConfirmarEditar").disabled = true;

    var IdIngreso = $("#Editar #cin_IdIngreso").children("option:selected").val();
    var Inicio = $("#Editar #tc_RangoInicio").val();
    var Fin = $("#Editar #tc_RangoFin").val();
    var Porcentaje = $("#Editar #tc_PorcentajeComision").val();

    var response = FormatearNumericos(Inicio, Fin, Porcentaje);

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data_valida = {
        tc_Id: IDInactivar,
        cin_IdIngreso: IdIngreso,
        tc_RangoInicio: response[0],
        tc_RangoFin: response[1],
        tc_PorcentajeComision: response[2]
    };

    $.ajax({
        url: "/TechosComisiones/Edit",
        method: "POST",
        data:data_valida
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            $("#ConfirmarEdicion").modal('hide');
            document.getElementById("btnConfirmarEditar").disabled = false;
            iziToast.error({
                title: 'Error',
                message: 'No se editó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridTechoComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ConfirmarEdicion").modal('hide');
            document.getElementById("btnConfirmarEditar").disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
    });                
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(IdIngreso, Inicio, Fin, Porcentaje) {
    var pasoValidacion = true;
    var response = FormatearNumericos(Inicio, Fin, Porcentaje);
    if (IdIngreso != "-1") {

        if (IdIngreso <= 0 || isNaN(IdIngreso)) {
            pasoValidacion = false;
            $('#Editar #Validation_IdIngreso').show();
            $("#Editar #ComisionAsterisco").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_IdIngreso').hide();
            $("#Editar #ComisionAsterisco").removeClass("text-danger");
        }
    }

    //VALIDACION DEL RANGO INICIO
    if (Inicio != "-1") {
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        //var indicesInicio = Inicio.split(",");
        ////VARIABLE CONTENEDORA DEL MONTO
        //var InicioFormateado = "";
        ////ITERAR LOS INDICES DEL ARRAY MONTO
        //for (var i = 0; i < indicesInicio.length; i++) {
        //    //SETEAR LA VARIABLE DE MONTO
        //    InicioFormateado += indicesInicio[i];
        //}
        //FORMATEAR A DECIMAL
        InicioFormateado = parseFloat(response[0]);
        
            //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (InicioFormateado == null || InicioFormateado == '' || InicioFormateado == undefined) {
                pasoValidacion = false;
                $('#Editar #RangoInicio_Validation2').hide();
                $('#Editar #RangoInicio_Validation').show();
                $("#Editar #InicioAsterisco").addClass("text-danger");
            } else {
            $('#Editar #RangoInicio_Validation').hide();
            $("#Editar #InicioAsterisco").removeClass("text-danger");
                if (InicioFormateado <= 0) {
                    pasoValidacion = false;
                    $('#Editar #RangoInicio_Validation').hide();
                    $("#Editar #InicioAsterisco").addClass("text-danger");
                    $('#Editar #RangoInicio_Validation2').show();
                } else {
                    $("#Editar #InicioAsterisco").removeClass("text-danger");
                    $('#Editar #RangoInicio_Validation').hide();
                    $('#Editar #RangoInicio_Validation2').hide();
                }
            }
    }

    //VALIDACION DEL RANFO FIN
    if (Fin != "-1") {
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        //var indicesFin = Fin.split(",");
        ////VARIABLE CONTENEDORA DEL MONTO
        //var FinFormateado = "";
        ////ITERAR LOS INDICES DEL ARRAY MONTO
        //for (var i = 0; i < indicesFin.length; i++) {
        //    //SETEAR LA VARIABLE DE MONTO
        //    FinFormateado += indicesFin[i];
        //}
        //FORMATEAR A DECIMAL
        FinFormateado = parseFloat(response[1]);
        
        //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (FinFormateado == null || FinFormateado == '' || FinFormateado == undefined) {
            pasoValidacion = false;
            $('#Editar #RangoFin_Validation2').hide();
            $('#Editar #RangoFin_Validation').show();
            $("#Editar #FinAsterisco").addClass("text-danger");
        } else {
            $('#Editar #RangoFin_Validation').hide();
            $("#Editar #FinAsterisco").removeClass("text-danger");
            if (FinFormateado <= 0) {
                pasoValidacion = false;
                //$('#Editar #RangoFin_Validation').hide();
                $("#Editar #FinAsterisco").addClass("text-danger");
                $('#Editar #RangoFin_Validation2').show();
            } else {
                $("#Editar #FinAsterisco").removeClass("text-danger");
                $('#Editar #RangoFin_Validation').hide();
                $('#Editar #RangoFin_Validation2').hide();
            }
        }
    }

    //VALIDACION DEL PORCENTAJE
    if (Porcentaje != "-1") {
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        //var indicesPorcentaje = Porcentaje.split(",");
        ////VARIABLE CONTENEDORA DEL MONTO
        //var PorcentajeFormateado = "";
        ////ITERAR LOS INDICES DEL ARRAY MONTO
        //for (var i = 0; i < indicesPorcentaje.length; i++) {
        //    //SETEAR LA VARIABLE DE MONTO
        //    PorcentajeFormateado += indicesPorcentaje[i];
        //}
        //FORMATEAR A DECIMAL
        PorcentajeFormateado = parseFloat(response[2]);

        //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
        if (PorcentajeFormateado == null || PorcentajeFormateado == '' || PorcentajeFormateado == undefined) {
            pasoValidacion = false;
            $('#Editar #Porcentaje_Validation2').hide();
            $('#Editar #Porcentaje_Validation').show();
            $("#Editar #PorcentajeAsterisco").addClass("text-danger");
        } else {
            $('#Editar #Porcentaje_Validation').hide();
            $("#Editar #PorcentajeAsterisco").removeClass("text-danger");
            if (PorcentajeFormateado <= 0) {
                pasoValidacion = false;
                //$('#Editar #Porcentaje_Validation').hide();
                $("#Editar #PorcentajeAsterisco").addClass("text-danger");
                $('#Editar #Porcentaje_Validation2').show();
            } else {
                $("#Editar #PorcentajeAsterisco").removeClass("text-danger");
                $('#Editar #Porcentaje_Validation').hide();
                $('#Editar #Porcentaje_Validation2').hide();
            }
        }
    }
    return pasoValidacion;
}

//FUNCION: OCULTAR LOS MENSAJES DE ERROR DE VALIDACIONES
function OcultarValidacionesEditar() {
    //VACIAR LOS CAMPOS
    $("#Editar #cin_IdIngreso").val();
    $("#Editar #tc_RangoInicio").val();
    $("#Editar #tc_RangoFin").val();
    $("#Editar #tc_PorcentajeComision").val();

    //OCULTAR ASTERISCOS
    $("#Editar #ComisionAsterisco").removeClass("text-danger");
    $("#Editar #InicioAsterisco").removeClass("text-danger");
    $("#Editar #FinAsterisco").removeClass("text-danger");
    $("#Editar #PorcentajeAsterisco").removeClass("text-danger");

    //OCULTAR VALIDACIONES
    $('#Editar #Validation_IdIngreso').hide();
    $('#Editar #RangoInicio_Validation').hide();
    $('#Editar #RangoInicio_Validation2').hide();
    $('#Editar #RangoFin_Validation').hide();
    $('#Editar #RangoFin_Validation2').hide();
    $('#Editar #Porcentaje_Validation').hide();
    $('#Editar #Porcentaje_Validation2').hide();
}

//FUNCION: CERRAR EL MODAL DE CONFIRMACION AL EDITAR, (CON EL BOTON DE CERRAR)
$("#btnCerrarConfirmarEditar").click(function () {
    $("#ConfirmarEdicion").modal('hide');
    $("#EditarTechoComision").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: CERRAR MODAL DE EDICION CON EL BOTON CERRAR DEL MODAL DE EDITAR
$("#btnCerrarEditar").click(function () {
    //OCULTAR MODAL DE EDITAR
    $("#EditarTechoComision").modal('hide');
    OcultarValidacionesEditar();
});


//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblTechoCom tbody tr td #btnDetallesTechosComisiones", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosComisiones/Details");

    if (validacionPermiso.status == true) {
        ActivarID = $(this).data('id');
        var ID = $(this).data('id');
        
        $.ajax({
            url: "/TechosComisiones/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        })
            .done(function (data) {
                
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {

                    var FechaCrea = FechaFormato(data.tc_FechaCrea);
                    var FechaModifica = FechaFormato(data.tc_FechaModifica);

                    $("#Detalles #tc_Id").html(data.tc_Id);
                    $("#Detalles #cin_DescripcionIngreso").html(data.cin_DescripcionIngreso);
                    $("#Detalles #tc_RangoInicio").html((data.tc_RangoInicio % 1 == 0) ? data.tc_RangoInicio + ".00" : data.tc_RangoInicio);
                    $("#Detalles #tc_RangoFin").html((data.tc_RangoFin % 1 == 0) ? data.tc_RangoFin + ".00" : data.tc_RangoFin);
                    $("#Detalles #tc_PorcentajeComision").html((data.tc_PorcentajeComision % 1 == 0) ? data.tc_PorcentajeComision + ".00" + '%' : data.tc_PorcentajeComision + '%');

                    $("#Detalles #NombreUsuarioCrea").html(data.NombreUsuarioCrea);
                    $("#Detalles #tc_FechaCrea").html(FechaCrea);
                    $("#Detalles #NombreUsuarioModifica").html((data.NombreUsuarioModifica != null ? data.NombreUsuarioModifica : 'Sin Modificaciones'));
                    $("#Detalles #tc_FechaModifica").html(FechaModifica);
                    
                    $("#DetallesTechoComision").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se cargó la información, contacte al administrador',
                    });
                }
            });
    }
});


//INACTIVAR
$(document).on("click", "#btnmodalInactivarTechosComisiones", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosComisiones/Inactivar");

    if (validacionPermiso.status == true) {
        //MOSTRAR EL MODAL DE INACTIVAR
        $("#EditarTechoComision").modal('hide');
        $("#InactivarTechoComision").modal({ backdrop: 'static', keyboard: false });
    }
});

//FUNCION: PRIMERA FASE DE INACTIVACION DE REGISTROS, MOSTRAR MODAL CON MENSAJE DE CONFIRMACION
$("#btnInactivarTechosComisiones").click(function () {
    $("#EditarTechosComisiones").modal('hide');
    $("#btnInactivarTechoComision").attr("disabled", false);
    $("#InactivarTechosComisiones").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarTechoComision").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnInactivarTechoComision").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TechosComisiones/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        //DESPLEGAR MODAL DE EDITAR OTRA VEZ
        //$("#InactivarTechoComision").modal('hide');
        //$("#EditarTechosComisiones").modal({ backdrop: 'static', keyboard: false });
        //BLOQUEAR EL BOTON
        $("#btnInactivarTechoComision").attr("disabled", false);
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
            $("#InactivarTechoComision").modal('hide');
            $("#btnInactivarTechoComision").attr("disabled", false);
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridTechoComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarTechoComision").modal('hide');
            $("#btnInactivarTechoComision").attr("disabled", false);
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//FUNCION: OCULTAR MODAL DE INACTIVACION
$("#btnCerrarInactivar").click(function () {
    $("#InactivarTechoComision").modal('hide');
    $("#EditarTechoComision").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#tblTechoCom tbody tr td #btnActivarTechosComisiones", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosComisiones/Activar");

    if (validacionPermiso.status == true) {
        IDActivar = $(this).data('id');
        $("#btnActivarTechoComision").attr("disabled", false);
        $("#ActivarTechoComision").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR ACTIVACION DEL REGISTRO EN EL MODAL
$("#btnActivarTechoComision").click(function () {
    document.getElementById("btnActivarTechoComision").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TechosComisiones/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
            $("#ActivarTechoComision").modal('hide');
            document.getElementById("btnActivarTechoComision").disabled = false;
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridTechoComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarTechoComision").modal('hide');
            document.getElementById("btnActivarTechoComision").disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});

//FUNCION: OCULTAR MODAL DE ACTIVACION
$("#btnCerrarActivar").click(function () {
    $("#ActivarTechoComision").modal('hide');
});