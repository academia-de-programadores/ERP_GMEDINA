const btnGuardar = $('#btnCreateRegistroDeduccion')
const btnGuardarEditar = $('#btnUpdateDeduccion2')
const btnGuardarActivar = $('#btnActivarRegistroDeduccion')
cargandoCrearcargandoCrear = $('#cargandoCrear')
cargandoCrear = $('#cargandoCrear')
cargandoActivar = $('#cargandoCrear')//Div que aparecera cuando se le de click en crear

//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });

//VARIABLE PARA INACTIVAR
var InactivarID = 0;
var ActivarID = 0;

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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/CatalogoDeDeducciones/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaDeducciones = data, template = '';

            $('#tblCatalogoDeducciones').DataTable().clear();

            //Recorrer la data y crear el template que se pondrá en el tbody
            for (var i = 0; i < ListaDeducciones.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeducciones[i].cde_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = ListaDeducciones[i].cde_Activo == true ? '<button data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" style= "margin-right:3px;" type="button" class="btn btn-primary btn-xs"  id="btnDetalleCatalogoDeducciones">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaDeducciones[i].cde_Activo == true ? '<button data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" type="button" class="btn btn-default btn-xs"  id="btnEditarCatalogoDeducciones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeducciones[i].cde_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarCatalogoDeducciones">Activar</button>' : '' : '';

                $('#tblCatalogoDeducciones').dataTable().fnAddData([
					ListaDeducciones[i].cde_IdDeducciones,
					ListaDeducciones[i].cde_DescripcionDeduccion,
					ListaDeducciones[i].cde_PorcentajeColaborador,
					ListaDeducciones[i].cde_PorcentajeEmpresa,
					ListaDeducciones[i].tde_Descripcion,
                    estadoRegistro,
					botonDetalles + botonEditar + botonActivar
                ]);
            }            
            });
      FullBody();
 }        

//VALIDAR CREATE//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrear").click(function () {
    document.getElementById('Validation_PorcentajeEmpresa').innerHTML = '';
    document.getElementById('Validation_PorcentajeColaborador').innerHTML = '';
    $("#Crear #Validation_descipcionA").css("display", "none");
    $("#Crear #Validation_descipcion2A").css("display", "none");
    $("#Crear #Validation_descipcion3A").css("display", "none");
    $("#Crear #Validation_descipcion4A").css("display", "none");
    $("#Crear #cde_DescripcionDeduccionA").val("");
    $("#Crear #cde_PorcentajeColaboradorA").val("");
    $("#Crear #cde_PorcentajeEmpresaA").val("");
    $("#Crear #tde_IdTipoDedu").val("0");
    $("#Crear #AsteriscoDescripcionDedu").removeClass("text-danger");
    $("#Crear #AsteriscoPorcentajeColaborador").removeClass("text-danger");
    $("#Crear #AsteriscoPorcentajeEmpresa").removeClass("text-danger");
    $("#Crear #AsteriscoTipoDedu").removeClass("text-danger");
    //ocultarCargandoCrear(); 
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarCatalogoDeducciones", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/CatalogoDeDeducciones/EditGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #tde_IdTipoDedu").empty();
            $("#Crear #tde_IdTipoDedu").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #tde_IdTipoDedu").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#Crear #tde_IdTipoDedu").val("0");
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccion').click(function () {
    
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var cde_DescripcionDeduccionA = $("#Crear #cde_DescripcionDeduccionA").val();
    var cde_PorcentajeColaboradorA = $("#Crear #cde_PorcentajeColaboradorA").val();
    var cde_PorcentajeEmpresaA = $("#Crear #cde_PorcentajeEmpresaA").val();
    var tde_IdTipoDeduc = $("#Crear #tde_IdTipoDedu").val();
    var Correcto = true;
    var Correcto2 = true;
    var Correcto3 = true;
    var Correcto4 = true;
    var colaborador = cde_PorcentajeColaboradorA.split(".");
    var empresa = cde_PorcentajeEmpresaA.split(".");

    if (cde_DescripcionDeduccionA == "" || cde_DescripcionDeduccionA == null || cde_DescripcionDeduccionA == undefined) {
        $("#Crear #cde_DescripcionDeduccionA").val("");
        $("#Crear #AsteriscoDescripcionDedu").addClass("text-danger");
        $("#Crear #Validation_descipcionA").css("display", "");
        Correcto = false;
    }
    else {
        $("#Crear #Validation_descipcionA").css("display", "none");
        Correcto = true;
    }
    if (tde_IdTipoDeduc == "0" || tde_IdTipoDeduc == null) {
        $("#Crear #Validation_descipcion2A").css("display", "");
        $("#Crear #tde_IdTipoDedu").val("0");
        $("#Crear #AsteriscoTipoDedu").addClass("text-danger");
        Correcto2 = false;
    }
    else {
        $("#Crear #Validation_descipcion2A").css("display", "none");
        $("#Crear #AsteriscoTipoDedu").removeClass("text-danger");
        Correcto2 = true;
    }

    //if (cde_PorcentajeColaboradorA == "" || cde_PorcentajeColaboradorA == 0 || cde_PorcentajeColaboradorA == null || cde_PorcentajeColaboradorA == undefined || cde_PorcentajeColaboradorA < 0) {
    //    $("#Crear #Validation_descipcion3A").css("display", "");
    //    $("#Crear #c_Validation_decimal1").css("display", "none");
    //    $("#Crear #AsteriscoPorcentajeColaborador").addClass("text-danger");
    //    Correcto3 = false;
    //}
    //else {
    //    if (colaborador[1] == null && cde_PorcentajeColaboradorA != 0
    //        || colaborador[1] == undefined && cde_PorcentajeColaboradorA != 0) {
    //        $("#Crear #c_Validation_decimal1").css("display", "");
    //        $("#Crear #Validation_descipcion3A").css("display", "none");
    //        $("#Crear #AsteriscoPorcentajeColaborador").addClass("text-danger");
    //        Correcto3 = false;
    //    }else{
    //        $("#Crear #c_Validation_decimal1").css("display", "none");
    //        $("#Crear #Validation_descipcion3A").css("display", "none");
    //        $("#Crear #AsteriscoPorcentajeColaborador").removeClass("text-danger");
    //        Correcto3 = true;
    //    }
    //}

    //.innerHTML= 'Nuevo valor';
    if (cde_PorcentajeColaboradorA == "" || cde_PorcentajeColaboradorA == null || cde_PorcentajeColaboradorA == undefined) {
        //$("#Validation_PorcentajeColaborador").innerHTML = 'Campo Porcentaje Colaborador requerido';
        document.getElementById('Validation_PorcentajeColaborador').innerHTML = 'Campo Porcentaje Colaborador requerido';
        $("#Crear #AsteriscoPorcentajeColaborador").addClass("text-danger");
        Correcto3 = false;
    } else {
        if (cde_PorcentajeColaboradorA <= 0) {
            document.getElementById('Validation_PorcentajeColaborador').innerHTML = 'El campo Porcentaje Colaborador debe ser mayor que cero';
            $("#Crear #AsteriscoPorcentajeColaborador").addClass("text-danger");
            Correcto3 = false;
        } else {
            if (colaborador[1] == null || colaborador[1] == undefined) {
                document.getElementById('Validation_PorcentajeColaborador').innerHTML = 'Número decimal válido con un máximo de 2 decimales';
                $("#Crear #AsteriscoPorcentajeColaborador").addClass("text-danger");
                Correcto3 = false;
            } else {
                document.getElementById('Validation_PorcentajeColaborador').innerHTML = '';
                Correcto3 = true;
            }
        }
    }

    if (cde_PorcentajeEmpresaA == "" || cde_PorcentajeEmpresaA == null || cde_PorcentajeEmpresaA == undefined) {
        //$("#Validation_PorcentajeColaborador").innerHTML = 'Campo Porcentaje Colaborador requerido';
        document.getElementById('Validation_PorcentajeEmpresa').innerHTML = 'Campo Porcentaje Colaborador requerido';
        $("#Crear #AsteriscoPorcentajeEmpresa").addClass("text-danger");
        Correcto3 = false;
    } else {
        if (cde_PorcentajeEmpresaA <= 0) {
            document.getElementById('Validation_PorcentajeEmpresa').innerHTML = 'El campo Porcentaje Colaborador debe ser mayor que cero';
            $("#Crear #AsteriscoPorcentajeEmpresa").addClass("text-danger");
            Correcto3 = false;
        } else {
            if (empresa[1] == null || empresa[1] == undefined) {
                document.getElementById('Validation_PorcentajeEmpresa').innerHTML = 'Número decimal válido con un máximo de 2 decimales';
                $("#Crear #AsteriscoPorcentajeEmpresa").addClass("text-danger");
                Correcto3 = false;
            } else {
                document.getElementById('Validation_PorcentajeEmpresa').innerHTML = '';
                Correcto3 = true;
            }
        }
    }


    //if (cde_PorcentajeEmpresaA == "" || cde_PorcentajeEmpresaA == "0" || cde_PorcentajeEmpresaA == null || cde_PorcentajeEmpresaA == undefined || cde_PorcentajeEmpresaA < 0) {
    //    $("#Crear #Validation_descipcion4A").css("display", "");
    //    $("#Crear #c_Validation_decimal2").css("display", "none");
    //    $("#Crear #AsteriscoPorcentajeEmpresa").addClass("text-danger");
    //    Correcto4 = false;
    //}
    //else {
    //    if(empresa[1] == null || empresa[1] == undefined){
    //        $("#Crear #c_Validation_decimal2").css("display", "");
    //        $("#Crear #Validation_descipcion4A").css("display", "none");
    //        $("#Crear #AsteriscoPorcentajeEmpresa").addClass("text-danger");
    //        Correcto4 = false;
    //    } else {
    //        $("#Crear #c_Validation_decimal2").css("display", "none");
    //        $("#Crear #Validation_descipcion4A").css("display", "none");
    //        $("#Crear #AsteriscoPorcentajeEmpresa").removeClass("text-danger");
    //        Correcto4 = true;
    //        }
    //}

    if (Correcto == false || Correcto2 == false || Correcto3 == false || Correcto4 == false) {

    }
    else {
        document.getElementById('Validation_PorcentajeEmpresa').innerHTML = '';
        document.getElementById('Validation_PorcentajeColaborador').innerHTML = '';
        $("#Crear #Validation_descipcionA").css("display", "none");
        $("#Crear #Validation_descipcion2A").css("display", "none");
        $("#Crear #Validation_descipcion3A").css("display", "none");
        $("#Crear #Validation_descipcion4A").css("display", "none");
        $("#Crear #AsteriscoDescripcionDedu").removeClass("text-danger");
        $("#Crear #AsteriscoPorcentajeColaborador").removeClass("text-danger");
        $("#Crear #AsteriscoPorcentajeEmpresa").removeClass("text-danger");
        $("#Crear #AsteriscoTipoDedu").removeClass("text-danger");
        //mostrarCargandoCrear();
        document.getElementById('btnCreateRegistroDeduccion').disabled = true;
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmCatalogoDeduccionesCreate").serializeArray();

        $.ajax({
            url: "/CatalogoDeDeducciones/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data != "error") {
                cargarGridDeducciones();

                $("#Crear #cde_DescripcionDeduccionA").val("");
                $("#Crear #cde_PorcentajeColaboradorA").val("");
                $("#Crear #cde_PorcentajeEmpresaA").val("");
                $("#Crear #tde_IdTipoDedu").val("0");

                //ocultar el modal
                $("#AgregarCatalogoDeducciones").modal('hide');
                document.getElementById('btnCreateRegistroDeduccion').disabled = false;

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else
            {
                $("#AgregarCatalogoDeducciones").modal('hide');
                document.getElementById('btnCreateRegistroDeduccion').disabled = false;
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
            //ocultarCargandoCrear();
        });       
    }
});

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeduccionesCreate").submit(function (e) {
    return false;
});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarEditar").click(function () {
    OcultarValidacionesEditar();
});

//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE EDITAR
function OcultarValidacionesEditar() {
    $("#Validation_decimal").css("display", "none");
    $("#Validation_decimal2").css("display", "none");
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    $("#Editar #AsteriscoDescripcionDeduEdit").removeClass("text-danger");
    $("#Editar #AsteriscoPorcentajeColaboradorEdit").removeClass("text-danger");
    $("#Editar #AsteriscoPorcentajeEmpresaEdit").removeClass("text-danger");
    $("#Editar #AsteriscoTipoDeduEdit").removeClass("text-danger")
}

//FUNCION: CERRAR EL MODAL DE CONFIRMACION Y VOLVER AL MODAL DE EDITAR
$("#btnEditarConfirmacion").click(function () {
    $("#EditarCatalogoDeduccionesConfirmacion").modal('hide');
    $("#EditarCatalogoDeducciones").modal();
})


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnEditarCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
    $.ajax({
        url: "/CatalogoDeDeducciones/Edit/" + ID,      
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    }).done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #cde_IdDeducciones").val(data.cde_IdDeducciones);
                $("#Editar #cde_DescripcionDeduccion").val(data.cde_DescripcionDeduccion);
                $("#Editar #cde_PorcentajeColaborador").val(data.cde_PorcentajeColaborador);
                $("#Editar #cde_PorcentajeEmpresa").val(data.cde_PorcentajeEmpresa);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/CatalogoDeDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST                    
                        $.each(data, function (i, iter) {
                            $("#Editar #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
});

$('#btnUpdateDeduccion').click(function () {
    var cde_DescripcionDeduccionE2 = $("#Editar #cde_DescripcionDeduccion").val();
    var cde_PorcentajeColaboradorE2 = $("#Editar #cde_PorcentajeColaborador").val();
    var cde_PorcentajeEmpresaE2 = $("#Editar #cde_PorcentajeEmpresa").val();
    var CorrectoEdit = true;
    var CorrectoEdit2 = true;
    var CorrectoEdit3 = true;
    var colaborador = cde_PorcentajeColaboradorE2.split(".");
    var empresa = cde_PorcentajeEmpresaE2.split(".");

    if (cde_DescripcionDeduccionE2 == "") {
        $("#Editar #Validation_descipcion").css("display", "");
        $("#Editar #AsteriscoDescripcionDeduEdit").addClass("text-danger");
        CorrectoEdit = false;
    }
    else{
        $("#Editar #Validation_descipcion").css("display", "none");
        $("#Editar #AsteriscoDescripcionDeduEdit").removeClass("text-danger");
        var CorrectoEdit = true;
    }

    if (cde_PorcentajeColaboradorE2 == "" || cde_PorcentajeColaboradorE2 == "0" || cde_PorcentajeColaboradorE2 < 0) {
        $("#Editar #Validation_descipcion2").css("display", "");
        $("#Editar #Validation_decimal").css("display", "none")
        $("#Editar #AsteriscoPorcentajeColaboradorEdit").addClass("text-danger");
        CorrectoEdit2 = false;
    }
    else {
        if (colaborador[1] == null || colaborador[1] == undefined) {
            $("#Editar #Validation_decimal").css("display", "");
            $("#Editar #Validation_descipcion2").css("display", "none");
            $("#Editar #AsteriscoPorcentajeColaboradorEdit").addClass("text-danger");
            CorrectoEdit2 = false;
        } else {
            $("#Editar #Validation_decimal").css("display", "none");
            $("#Editar #Validation_descipcion2").css("display", "none");
            $("#Editar #AsteriscoPorcentajeColaboradorEdit").removeClass("text-danger");
            CorrectoEdit2 = true;
        }
    }


    if ( cde_PorcentajeEmpresaE2 == "" || cde_PorcentajeEmpresaE2 == "0" || cde_PorcentajeEmpresaE2 < 0){
        $("#Editar #Validation_descipcion3").css("display", "");
        $("#Editar #Validation_decimal2").css("display", "none");
        $("#Editar #AsteriscoPorcentajeEmpresaEdit").addClass("text-danger");
        CorrectoEdit3 = false;
    }
    else {
        if (empresa[1] == null || empresa[1] == undefined) {
            CorrectoEdit3 = false;
            $("#Editar #Validation_descipcion3").css("display", "none");
            $("#Editar #Validation_decimal2").css("display", "");
            $("#Editar #AsteriscoPorcentajeEmpresaEdit").addClass("text-danger");
        } else {
            $("#Editar #Validation_decimal2").css("display", "none");
            $("#Editar #Validation_descipcion3").css("display", "none");
            $("#Editar #AsteriscoPorcentajeEmpresaEdit").removeClass("text-danger");
            CorrectoEdit3 = true;
            } 
    }

    if(CorrectoEdit == false || CorrectoEdit2 == false || CorrectoEdit3 == false){
        $("#EditarCatalogoDeduccionesConfirmacion").modal('hide');
    }
    else {
        $("#EditarCatalogoDeducciones").modal('hide');
        $("#EditarCatalogoDeduccionesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("html, body").css("overflow", "hidden");
        $("html, body").css("overflow", "scroll");
    } 
});



//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateDeduccion2").click(function () {
    document.getElementById('btnUpdateDeduccion2').disabled = true;
    
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmCatalogoDeducciones").serializeArray();

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/CatalogoDeDeducciones/Edit",
            method: "POST",
            data: data
        })
        .done(function (data) {
            if (data != "error") {
                // REFRESCAR UNICAMENTE LA TABLA
                cargarGridDeducciones();

                //Ocultar el modal 
                $("#EditarCatalogoDeducciones").modal('hide');
                $("#EditarCatalogoDeduccionesConfirmacion").modal('hide');
                document.getElementById('btnUpdateDeduccion2').disabled = false;
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
            else
            {
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
                $("#EditarCatalogoDeduccionesConfirmacion").modal('hide');
                document.getElementById('btnUpdateDeduccion2').disabled = false;
            }
        });
});  

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeducciones").submit(function (e) {
    return false;
});


//FUNCTION: MOSTRAR DETALLE
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnDetalleCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    console.log(ID);
    $.ajax({
        url: "/CatalogoDeDeducciones/Details/"+ ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].cde_FechaCrea);
                var FechaModifica = FechaFormato(data[0].cde_FechaModifica);
                $("#Detalles #cde_IdDeducciones").html(data[0].cde_IdDeducciones);
                $("#Detalles #cde_DescripcionDeduccion").html(data[0].cde_DescripcionDeduccion);
                $("#Detalles #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
                $("#Detalles #cde_PorcentajeColaborador").html(data[0].cde_PorcentajeColaborador);
                $("#Detalles #cde_PorcentajeEmpresa").html(data[0].cde_PorcentajeEmpresa);
                $("#Detalles #cde_UsuarioCrea").html(data[0].cde_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #cde_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #cde_UsuarioModifica").val(data[0].cde_UsuarioModifica);
                $("#Detalles #cde_FechaModifica").html(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/CatalogoDeDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                .done(function (data) {

                    //LLENAR EL DROPDOWNLIST
                    $.each(data, function (i, iter) {
                        if (iter.Id == SelectedId) {
                            $("#Detalles #tde_IdTipoDedu").html(iter.Descripcion);
                        }
                    });
                });

                $("#DetallesCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
});



//MOSTRAR MODAL INACTIVAR
$(document).on("click", "#btnmodalInactivarCatalogoDeducciones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

    //Ocultar el modal editar
    $("#EditarCatalogoDeducciones").modal('hide');
});


$("#btnCerrarInhabilitar").click(function () {
    $("#InactivarCatalogoDeducciones").modal('hide');
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccion").click(function () {
    document.getElementById('btnInactivarRegistroDeduccion').disabled = true;
    var data = $("#frmCatalogoDeduccionesInactivar").serializeArray();
  //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeDeducciones/Inactivar/" + InactivarID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            $("#InactivarCatalogoDeducciones").modal('hide');
            $("#EditarCatalogoDeducciones").modal('hide');
            OcultarValidacionesEditar();
            document.getElementById('btnInactivarRegistroDeduccion').disabled = false;

            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarCatalogoDeducciones").modal('hide');
            $("#EditarCatalogoDeducciones").modal('hide');
            OcultarValidacionesEditar();
            document.getElementById('btnInactivarRegistroDeduccion').disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });

        }
    });
});


//MOSTRAR MODAL ACTIVAR
$(document).on("click", "#btnActivarCatalogoDeducciones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#ActivarCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});


//EJECUTAR ACTIVACION DEL REGISTRO EN EL MODAL
$("#btnCerrarInhabilitar").click(function () {
    //Mostrar modal editar nuevamente
    $("#EditarCatalogoDeducciones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#InactivarCatalogoDeducciones").modal('hide');
});

$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnActivarCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    ActivarID = ID;
    
});


//EJECUTAR ACTIVACION DEL REGISTRO EN EL MODAL
$("#btnActivarRegistroDeduccion").click(function () {
    //mostrarCargandoActivar();
    document.getElementById('btnActivarRegistroDeduccion').disabled = true;
    var data = $("#frmCatalogoDeduccionesActivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeDeducciones/Activar/" + ActivarID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            $("#ActivarCatalogoDeducciones").modal('hide');
            document.getElementById('btnActivarRegistroDeduccion').disabled = false;
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarCatalogoDeducciones").modal('hide');
            document.getElementById('btnActivarRegistroDeduccion').disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
            //ocultarCargandoActivar();
        }
    });
});
 

//function mostrarCargandoCrear() {
//    btnGuardar.hide();
//    cargandoCrear.html(spinner());
//    cargandoCrear.show();
//}

//function ocultarCargandoCrear() {
//    btnGuardar.show();
//    cargandoCrear.html('');
//    cargandoCrear.hide();
//}

//function mostrarCargandoEditar() {
//    btnGuardarEditar.hide();
//    cargandoCrear.html(spinner());
//    cargandoCrear.show();
//}

//function ocultarCargandoEditar() {
//    btnGuardarEditar.show();
//    cargandoCrear.html('');
//    cargandoCrear.hide();
//}

//function mostrarCargandoActivar() {
//    btnGuardarActivar.hide();
//    cargandoCrear.html(spinner());
//    cargandoCrear.show();
//}

//function ocultarCargandoActivar() {
//    btnGuardarActivar.show();
//    cargandoCrear.html('');
//    cargandoCrear.hide();
//}

////Mostrar el spinner
//function spinner() {
//    return `<div class="sk-spinner sk-spinner-wave">
// <div class="sk-rect1"></div>
// <div class="sk-rect2"></div>
// <div class="sk-rect3"></div>
// <div class="sk-rect4"></div>
// <div class="sk-rect5"></div>
// </div>`;
//}