//VARIABLES PARA INACTIVACION Y ACTIVACION DE REGISTROS
var IDInactivar = 0, IDActivar = 0;
//VARIABLE PARA VALIDAR SI ESTA PAGADO
var varPagado;
var dataSelect;

//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });

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
$('#frmEmpleadoBonosCreate').submit(function (e) {
    return false;
});
$('#frmEmpleadoBonos').submit(function (e) {
    return false;
});



//FUNCION: MOSTRAR ERRORES IZITOAST
function mostrarError(Mensaje) {
    iziToast.error({
        title: 'Error',
        message: Mensaje,
    });
}

//FUNCION: ESCUCHA EL CAMBIO EN EL CHECKBOX Y CAMBIA SU VALOR
$('#Editar #cb_Pagado').click(function () {
    if ($('#Editar #cb_Pagado').is(':checked')) {
        $('#Editar #cb_Pagado').val(true);
    }
    else {
        $('#Editar #cb_Pagado').val(false);
    }
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridBonos() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/EmpleadoBonos/GetData',
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
            var ListaBonos = data;
            $('#tblEmpleadoBonos').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaBonos.length; i++) {
                var FechaRegistro = FechaFormato(ListaBonos[i].cb_FechaRegistro);
                var Estado = ListaBonos[i].cb_Activo == true ? 'Activo' : 'Inactivo';

                var botonDetalles = '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleEmpleadoBonos">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaBonos[i].cb_Activo == true ? '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarEmpleadoBonos">Editar</button>' : '';


                //variable donde está el boton activar
                var botonActivar = ListaBonos[i].cb_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" style="margin right:3px;" class="btn btn-default btn-xs"  id="btnActivarEmpleadoBonos">Activar</button>' : '' : '';
       
                //VALIDACION PARA RECARGAR LA TABLA SIN AFECTAR LOS CHECKBOX
                var Check = "";
                //ESTA VARIABLE GUARDA CODIGO HTML DE UN CHECKBOX, PARA ENVIARLO A LA TABLA
                if (ListaBonos[i].cb_Pagado == true) {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" checked disabled>'; //SE LLENA LA VARIABLE CON UN INPUT CHEQUEADO
                } else {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" disabled>'; //SE LLENA LA VARIABLE CON UN INPUT QUE NO ESTA CHEQUEADO
                }

                $('#tblEmpleadoBonos').dataTable().fnAddData([
                   ListaBonos[i].cb_Id,
                   ListaBonos[i].per_Nombres + ' ' + ListaBonos[i].per_Apellidos,
                   ListaBonos[i].cin_DescripcionIngreso,
                   (ListaBonos[i].cb_Monto % 1 == 0) ? ListaBonos[i].cb_Monto + ".00" : ListaBonos[i].cb_Monto,
                   FechaRegistro,
                   Check,
                   Estado,
                   botonDetalles + botonEditar + botonActivar]
                   );
            }
        });
    (FullBody);
}
$(document).ready(function () {
    $.ajax({
        url: "/EmpleadoBonos/EditGetDDLEmpleado",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
           //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
           .done(function (data) {

               $('#Crear #emp_IdEmpleadoCrear').select2({
                   dropdownParent: $('#Crear'),
                   placeholder: 'Seleccione un empleado...',
                   allowClear: true,
                   language: {
                       noResults: function () {
                           return 'Resultados no encontrados.';
                       },
                       searching: function () {
                           return 'Buscando...';
                       }
                   },
                   data: data.results
               });

               var idEmpSelect = "";
               var NombreSelect = "";

               $('#Editar #emp_IdEmpleado').select2({
                   dropdownParent: $('#Editar'),
                   placeholder: 'Seleccione un empleado...',
                   allowClear: true,
                   language: {
                       noResults: function () {
                           return 'Resultados no encontrados.';
                       },
                       searching: function () {
                           return 'Buscando...';
                       }
                   },
                   data: data.results
               });
           });
});
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoBonos", function () {

    var validacionPermiso = userModelState("EmpleadoBonos/Create");

    if (validacionPermiso.status == true) {

        // crear
        let valCreate = $("#Crear #emp_IdEmpleadoCrear").val();
        if (valCreate != null && valCreate != "")
            $("#Crear #emp_IdEmpleadoCrear").val('').trigger('change');
        //LLAMAR LA FUNCION PARA OCULTAR LAS VALIDACIONES
        OcultarValidacionesCrear();
        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCreateRegistroBonos").attr("disabled", false);
        //FUNCION PARA CARGAR EL EMPLEADO SELECCIONADO

        //MOSTRAR EL MODAL DE AGREGAR
        $("#Crear #cb_Monto").val("");
        $("#AgregarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });

        //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DE INGRESO DEL MODAL
        $.ajax({
            url: "/EmpleadoBonos/EditGetDDLIngreso",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
            .done(function (data) {
                $("#Crear #cin_IdIngreso").empty();
                $("#Crear #cin_IdIngreso").append("<option value='0'>Selecione un bono...</option>");
                $.each(data, function (i, iter) {
                    $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });
    }

});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroBonos').click(function () {
    //CAPTURAR LOS VALORES DEL FORMULARIO
    var fnc_Colaborador = $("#Crear #emp_IdEmpleadoCrear").val();
    var fnc_Ingreso = $("#Crear #cin_IdIngreso").val();
    var fnc_Monto = $("#Crear #cb_Monto").val();

    // VALIDAR EL MODEL STATE DEL FORMULARIO
    if (ValidarCamposCrear(fnc_Colaborador, fnc_Ingreso, fnc_Monto)) {

        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCreateRegistroBonos").attr("disabled", true);
        debugger;
        //SEGMENTAR LA CADENA DE MONTO
        var MontoFormateado = $("#Crear #cb_Monto").val().replace(/,/g, '');
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado).toFixed(2);
        var data = {
            emp_Id: fnc_Colaborador,
            cin_IdIngreso: fnc_Ingreso,
            cb_Monto: MontoFormateado
        };
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/EmpleadoBonos/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarEmpleadoBonos").modal('hide');
            $("#Crear #cb_Monto").val("");
            //$("#Validation_descripcion1").css("display", "none");
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
            else {
                cargarGridBonos();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });

    }

});


//CERRAR EL MODAL DE CREAR
$("#btnCerrarCrearBono").click(function () {
    //OCULTAR EL MODAL DE CREACION
    $("#AgregarEmpleadoBonos").modal("hide");
    //CULTAR LAS VALIDACIONES DE CREAR
    OcultarValidacionesCrear();
});


//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposCrear(colaborador, Ingreso, monto) {
    var pasoValidacion = true;

    if (colaborador != "-1") {

        if (colaborador <= 0 || isNaN(colaborador) || colaborador == "0") {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Crear #Validation_IdEmpleado').css("display", "block");
            $("#Crear #AsteriscoEmpleado").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_IdEmpleado').css("display", "none");
            $("#Crear #AsteriscoEmpleado").removeClass("text-danger");
        }
    }

    if (Ingreso != "-1") {

        if (Ingreso <= 0 || isNaN(Ingreso) || Ingreso == "0") {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Crear #Validation_IdIngreso').css("display", "block");
            $("#Crear #AsteriscoBono").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_IdIngreso').css("display", "none");
            $("#Crear #AsteriscoBono").removeClass("text-danger");
        }
    }

    if (monto != "-1") {
        var LengthString = monto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = monto.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #cb_Monto").val(monto.substring(0, FirstChar + 1));
        }

        if (monto == null || monto == '' || monto == ' ' || monto == '  ' || parseFloat(monto) == 0.00 || monto == "0.00") {
            pasoValidacion = false;
            if (monto == ' ')
                $("#Crear #cb_Monto").val("");

            $('#Crear #Validation_Monto').show();
            $("#Crear #AsteriscoMonto").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_Monto').hide();
            $("#Crear #AsteriscoMonto").removeClass("text-danger");
        }
    }

    return pasoValidacion;
}

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(colaborador, Ingreso, monto) {
    var pasoValidacion = true;

    if (colaborador != "-1") {

        if (colaborador <= 0 || isNaN(colaborador) || colaborador == "0") {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Editar #Validation_IdEmpleado').css("display", "block");
            $("#Editar #AsteriscoEmpleado").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_IdEmpleado').css("display", "none");
            $("#Editar #AsteriscoEmpleado").removeClass("text-danger");
        }
    }

    if (Ingreso != "-1") {

        if (Ingreso <= 0 || isNaN(Ingreso) || Ingreso == "0") {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Editar #Validation_IdIngreso').css("display", "block");
            $("#Editar #AsteriscoBono").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_IdIngreso').css("display", "none");
            $("#Editar #AsteriscoBono").removeClass("text-danger");
        }
    }

    if (monto != "-1") {
        var LengthString = monto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = monto.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #cb_Monto").val(monto.substring(0, FirstChar + 1));
        }

        if (parseFloat(FormatearMonto(monto)) < 0) {
            $('#Editar #Validation_Monto').empty();
            $('#Editar #Validation_Monto').html("El campo monto no puede ser menor a cero.");
            $('#Editar #Validation_Monto').show();
            $("#Editar #AsteriscoMonto").addClass("text-danger");
        }
        else if (monto == null || monto == '' || monto == ' ' || monto == '  ' || parseFloat(FormatearMonto(monto)) == 0.00 || monto == "0.00") {
            pasoValidacion = false;
            if (monto == ' ')
                $("#Editar #cb_Monto").val("");

            $('#Editar #Validation_Monto').empty();
            $('#Editar #Validation_Monto').html("El campo monto es requerido.");
            $('#Editar #Validation_Monto').show();
            $("#Editar #AsteriscoMonto").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_Monto').hide();
            $("#Editar #AsteriscoMonto").removeClass("text-danger");
        }
    }

    return pasoValidacion;
}

//FUNCION: OCULTAR LAS VALIDACIONES AL CREAR
function OcultarValidacionesCrear() {
    //VALIDACIONES DE EMPLEADOS
    $('#Crear #Validation_IdEmpleado').css("display", "none");
    $("#Crear #AsteriscoEmpleado").removeClass("text-danger");
    //VALIDACIONES DE INGRESO
    $('#Crear #Validation_IdIngreso').css("display", "none");
    $("#Crear #AsteriscoBono").removeClass("text-danger");
    //VALIDACIONES DE MONTO
    $('#Crear #Validation_Monto').hide();
    $("#Crear #AsteriscoMonto").removeClass("text-danger");
}

//FUNCION: OCULTAR LAS VALIDACIONES AL EDITAR
function OcultarValidacionesEditar() {
    //VALIDACIONES DE EMPLEADOS
    $('#Editar #Validation_IdEmpleado').css("display", "none");
    $("#Editar #AsteriscoEmpleado").removeClass("text-danger");
    //VALIDACIONES DE INGRESO
    $('#Editar #Validation_IdIngreso').css("display", "none");
    $("#Editar #AsteriscoBono").removeClass("text-danger");
    //VALIDACIONES DE MONTO
    $('#Editar #Validation_Monto').hide();
    $("#Editar #AsteriscoMonto").removeClass("text-danger");
}

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnEditarEmpleadoBonos", function () {

    var validacionPermiso = userModelState("EmpleadoBonos/Edit");
    if (validacionPermiso.status == true) {
        //OBTENER TODA LA DATA DEL ROW SELECCIONADO
        let dataEmp = table.row($(this).parents('tr')).data();
        //INICIALIZAR UNA VARIABLE CON EL VALOR DEL ALMACENAMIENTO LOCAL
        let itemEmpleado = localStorage.getItem('idEmpleado');

        if (itemEmpleado != null) {
            $("#Editar #emp_IdEmpleado option[value='" + itemEmpleado + "']").remove().trigger('change');
            $("#Editar #emp_IdEmpleado #opt-gr-emp-info-incompleta").remove().trigger('change');
            localStorage.removeItem('idEmpleado');
        }
        //OCULTAR VALIDACIONES DE EDITAR
        OcultarValidacionesEditar();
        //CAPTURA DEL ID DEL REGISTRO A EDITAR
        var ID = $(this).data('id');
        //SETEAR LA VARIABLE DE INACTIVACION
        IDInactivar = ID;
        var idEmpSelect = "";
        var NombreSelect = "";

        $.ajax({
            url: "/EmpleadoBonos/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    if (data.cb_Pagado) {
                        varPagado = 1;
                        $("#btnUpdateBonos").attr('disabled', true);
                    } else {
                        varPagado = 0;
                        $("#btnUpdateBonos").attr('disabled', false);
                    }
                    var FechaRegistro = FechaFormato(data.cb_FechaRegistro);

                    //AQUI VALIDA EL CHECKBOX PARA PODER CARGARLO EN EL MODAL
                    if (data.cb_Pagado) {
                        $('#Editar #cb_Pagado').prop('checked', true);
                    } else {
                        $('#Editar #cb_Pagado').prop('checked', false);
                    }
                    idEmpSelect = data.emp_Id;
                    NombreSelect = dataEmp[1];

                    $("#Editar #emp_IdEmpleado").select2("val", "");
                    $('#Editar #emp_IdEmpleado').val(idEmpSelect).trigger('change');

                    let valor = $('#Editar #emp_IdEmpleado').val();

                    if (valor == null) {
                        $("#Editar #emp_IdEmpleado").prepend('<optgroup id="opt-gr-emp-info-incompleta" label="Empleado con información incompleta"></optgroup>').trigger('change');
                        $("#opt-gr-emp-info-incompleta").prepend(`<option value='` + idEmpSelect + `' selected>` + NombreSelect + `</option>`).trigger('change');
                        localStorage.setItem('idEmpleado', idEmpSelect);
                    }

                    $("#Editar #cb_Id").val(data.cb_Id);
                    $("#Editar #cb_Monto").val(data.cb_Monto);
                    $("#Editar #cb_FechaRegistro").val(FechaRegistro);
                    $("#Editar #cb_Pagado").val(data.cb_Pagado);
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedIdEmp = data.emp_Id;
                    var SelectedIdCatIngreso = data.cin_IdIngreso;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL

                    $.ajax({
                        url: "/EmpleadoBonos/EditGetDDLIngreso",
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
                                $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });
                        });
                    $("#EditarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se pudo cargar la información, contacte al administrador',
                    });
                }
                Check = "";
            });
    }
});

//VALIDAR LOS CAMPOS DE EDITAR Y MOSTRAR EL MODAL DE CONFIRMACION
$("#btnUpdateBonos").click(function () {

    var fnc_Colaborador = $("#Editar #emp_IdEmpleado").val();
    var fnc_Ingreso = $("#Editar #cin_IdIngreso").val();
    var fnc_Monto = $("#Editar #cb_Monto").val();

    // VALIDAR EL MODEL STATE DEL FORMULARIO
    if (ValidarCamposEditar(fnc_Colaborador, fnc_Ingreso, fnc_Monto)) {
        $("#btnUpdateBonos2").attr('disabled', false);
        $("#EditarEmpleadoBonos").modal('hide');
        $("#EditarEmpleadoBonosConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }

});
//FUNCION: EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL DE CONFIRMACION
$("#btnUpdateBonos2").click(function () {

    //CAPTURAR LOS VALORES DEL FORMULARIO
    var fnc_cb_Id = $("#Editar #cb_Id").val();
    var fnc_Colaborador = $("#Editar #emp_IdEmpleado").val();
    var fnc_Ingreso = $("#Editar #cin_IdIngreso").val();
    var cb_FechaRegistro = $("#Editar #cb_FechaRegistro").val();
    var cb_Pagado = ($('#Editar #cb_Pagado').is('checked', true)) ? true : false;
    //VALIDAR QUE EL MONTO NO ESTE PAGADO
    if (varPagado == 0) {
        $("#btnUpdateBonos2").attr("disabled", true);
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var indices = $("#Editar #cb_Monto").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i <= indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);
        var data = {
            cb_Id: fnc_cb_Id,
            emp_Id: fnc_Colaborador,
            cin_IdIngreso: fnc_Ingreso,
            cb_Monto: MontoFormateado,
            cb_FechaRegistro: cb_FechaRegistro,
            cb_Pagado: cb_Pagado
        };

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/EmpleadoBonos/edit",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data == "error") {
                //Cuando traiga un error del backend al guardar la edicion
                iziToast.error({

                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
                $("#btnUpdateBonos2").attr('disabled', false);
                $("#EditarEmpleadoBonosConfirmacion").modal('hide');
            }
            else {
                // REFRESCAR UNICAMENTE LA TABLA
                cargarGridBonos();
                FullBody();
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarEmpleadoBonos").modal('hide');
                $("#EditarEmpleadoBonosConfirmacion").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
        });
    } else {
        $("#btnUpdateBonos2").attr('disabled', false);
        $("#EditarEmpleadoBonosConfirmacion").modal('hide');
        iziToast.error({
            title: 'Error',
            message: '¡No puede editar un registro pagado!',
        });
    }
});

//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnDetalleEmpleadoBonos", function () {
    var validacionPermiso = userModelState("EmpleadoBonos/Details");
    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        $.ajax({
            url: "/EmpleadoBonos/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    var FechaRegistro = FechaFormato(data.cb_FechaRegistro);
                    var FechaCrea = FechaFormato(data.cb_FechaCrea);
                    var FechaModifica = FechaFormato(data.cb_FechaModifica);
                    var usuarioModifica = data.usuarioModifica == null ? 'Sin modificaciones' : data.usuarioModifica;
                    var usuarioCrea = data.NombreUsarioCrea == null ? 'N/A' : data.NombreUsarioCrea;

                    if (data.cb_Pagado) {
                        $("#Detalles #cb_Pagado").html("Si");
                    } else {
                        $("#Detalles #cb_Pagado").html("No");
                    }
                    $("#Detalles #emp_Id").html(data.per_Nombres + ' ' + data.per_Apellidos);
                    $("#Detalles #cb_Id").val(data.cb_Id);
                    $("#Detalles #cb_Monto").html(data.cb_Monto);
                    $("#Detalles #cb_FechaRegistro").html(FechaRegistro);
                    //$("#Detalles #cb_Pagado").val(data.cb_Pagado);
                    $("#Detalles #cb_UsuarioCrea").html(data.cb_UsuarioCrea);
                    $("#Detalles #tbUsuario_usu_NombreUsuario").html(usuarioCrea);
                    $("#Detalles #cb_FechaCrea").html(FechaCrea);
                    $("#Detalles #cb_UsuarioModifica").html(data.cb_UsuarioModifica);
                    $("#Detalles #tbUsuario1_usu_NombreUsuario").html(usuarioModifica);
                    $("#Detalles #cb_FechaModifica").html(FechaModifica);

                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedIdEmp = data.emp_Id;
                    var SelectedIdCatIngreso = data.cin_IdIngreso;

                    $.ajax({
                        url: "/EmpleadoBonos/EditGetDDLIngreso",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                        }).done(function (data) {
                            //-----------------------------------------NO ENTRA EN ESTE each
                            $.each(data, function (i, iter) {
                                if (iter.Id == SelectedIdCatIngreso) {
                                    $("#Detalles #cin_IdIngreso").html(iter.Descripcion);
                                }
                            });
                        });
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                    $.ajax({
                        url: "/EmpleadoBonos/EditGetDDLEmpleado",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            //$("#Detalles #emp_IdEmpleado").empty();
                            //LLENAR EL DROPDOWNLIST
                            //$("#Detalles #emp_IdEmpleado").append("<option value=0>Selecione una opción...</option>");
                            $.each(data, function (i, iter) {
                                //$("#Detalles #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                                if (iter.Id == SelectedIdEmp) {
                                    $("#Detalles #emp_Id").html(iter.Descripcion);
                                }

                            });
                        });

                    $("#DetallesEmpleadoBonos").modal();
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se cargó la información, contacte al administrador!',
                    });
                }
            });
    }
});


//FUNCION: MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarEmpleadoBonos", function () {
    var validacionPermiso = userModelState("EmpleadoBonos/Inactivar");
    if (validacionPermiso.status == true) {
        //INHABILITAR EL BOTON DE INACTIVACION
        $("#btnInactivarRegistroBono").attr("disabled", false);
        //OCULTAR EL MODAL DE EDICION
        $("#EditarEmpleadoBonos").modal('hide');
        //MOSTRAR EL MODAL DE INACTIVAR
        $("#InactivarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroBono").click(function () {
    //INHABILITAR EL BOTON DE INACTIVACION
    $("#btnInactivarRegistroBono").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //HABILITAR EL BOTON DE INACTIVACION
            $("#btnInactivarRegistroBono").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!!',
            });
        }
    });
    IDInactivar = 0;
});

//VOLVER AL MODAL DE EDITAR CERRANDO EL MODAL DE INACTIVAR CON EL BOTON 'CERRAR'
$("#btCerrarNo").click(function () {
    //OCULTAR EL MODAL DE CONFIRMAR INACTIVACION
    $("#InactivarEmpleadoBonos").modal('hide');
    //MOSTRAR EL MODAL DE ACTIVACION
    $("#EditarEmpleadoBonos").modal();
});


//OCULTAR EL MODAL DE CONFIRMAR EDICION
$("#btCerrarEditar").click(function () {
    //OCULTAR EL MODAL DE CONFIRMAR EDICION
    $("#EditarEmpleadoBonosConfirmacion").modal('hide');
    //MOSTRAR EL MODAL DE EDICIONs
    $("#EditarEmpleadoBonos").modal();
});


//FUNCION: MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnActivarEmpleadoBonos", function () {
    var validacionPermiso = userModelState("EmpleadoBonos/Activar");
    if (validacionPermiso.status == true) {
        IDActivar = $(this).data('id');
        //HABILITAR EL BOTON DE ACTIVACION
        $("#btnActivarRegistroBono").attr("disabled", false);
        //MOSTRAR EL MODAL DE ACTIVACION DE REGISTROS
        $("#ActivarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR LA ACTIVACION DEL REGISTRO
$("#btnActivarRegistroBono").click(function () {
    //INHABILITAR EL BOTON DE INACTIVACION
    $("#btnActivarRegistroBono").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //HABILITAR EL BOTON DE INACTIVACION
            $("#btnActivarRegistroBono").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});

//btactivarNO
$("#btactivarNO").click(function () {
    //OCULTAR MODAL DE ACTIVACION
    $("#ActivarEmpleadoBonos").modal('hide');
});



//FUNCION: FORMATEAR MONTOS A DECIMAL 
function FormatearMonto(StringValue) {
    //SEGMENTAR LA CADENA DE MONTO
    var indices = StringValue.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i <= indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);
    //RETORNAR MONTO FORMATEADO
    return MontoFormateado;
}