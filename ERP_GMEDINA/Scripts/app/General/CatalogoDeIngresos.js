//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {

    });
// REGION DE VARIABLES
var InactivarID = 0;


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

function getTipoIngreso(tipoIngreso) {
    let descripcionTipoIngreso = '';
    switch (tipoIngreso) {
        case 1:
            descripcionTipoIngreso = 'Bonos';
            break;
        case 2:
            descripcionTipoIngreso = 'Comisiones';
            break;
        case 3:
            descripcionTipoIngreso = 'Extra';
            break;
        default:
            descripcionTipoIngreso = 'Otros';
            break;
    }
    return descripcionTipoIngreso;
}

// REFRESCAR INFORMACIÓN DE LA TABLA
function cargarGridIngresos() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/CatalogoDeIngresos/GetData',
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
            var ListaIngresos = data;
            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblCatalogoIngresos').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaIngresos.length; i++) {
                var estadoRegistro = ListaIngresos[i].cin_Activo == false ? "Inactivo" : "Activo";

                var botonDetalles = '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalle" data-id="' + ListaIngresos[i].cin_IdIngreso + '">Detalles</button>';

                var botonEditar = ListaIngresos[i].cin_Activo == true ?
                    '<button type="button" class="btn btn-default btn-xs" id="btnEditarIngreso" data-id="'
                    + ListaIngresos[i].cin_IdIngreso + '">Editar</button>' : '';

                var botonActivar = ListaIngresos[i].cin_Activo == false ? esAdministrador == "1" ?
                    '<button type="button" class="btn btn-default btn-xs" id="btnActivar" data-id="'
                    + ListaIngresos[i].cin_IdIngreso + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblCatalogoIngresos').dataTable().fnAddData([
                    ListaIngresos[i].cin_IdIngreso,
                    ListaIngresos[i].cin_DescripcionIngreso,
                    getTipoIngreso(ListaIngresos[i].cin_TipoIngreso),
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarCatalogoIngresos", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("CatalogoDeIngresos/Create");

    if (validacionPermiso.status == true) {
        //OCULTAR VALIDACIONES
        OcultarValidacionesCrear();
        //DESLOQUEAR EL BOTON
        $("#btnCreateRegistroIngresos").attr("disabled", false);
        //MOSTRAR EL MODAL DE CREACION
        $("#AgregarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    }
});

//EVITAR EL POSTBACK DEL FORMULARIO
$("#frmCatalogoIngresosCreate").submit(function (e) {
    return false;
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroIngresos').click(function () {
    //CAPTURAR EL VALOR DEL CAMPO DESCRIPCION
    var descripcion = $("#Crear #cin_DescripcionIngreso").val();
    let validacionTipoIngreso = ValidarTipoIngreso('Crear');
    //VALIDAMOS LOS CAMPOS
    if (ValidarCamposCrear(descripcion) == true && validacionTipoIngreso == true) {
        //SERIALIZAR EL FORMULARIO DEL MODAL 
        var data = $("#frmCatalogoIngresosCreate").serializeArray();
        //BLOQUEAMOS EL BOTON
        $("#btnCreateRegistroIngresos").attr("disabled", true);
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                //REFRESCAR LA DATA DEL DATATBLE
                cargarGridIngresos();
                //OCULTAR EL MODAL DE CREACION
                $("#AgregarCatalogoIngresos").modal('hide');

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });

                $("#Crear #cin_DescripcionIngreso").val('');
            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
        });
    }
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrear").click(function () {
    //OCULTAR EL MODAL DE CREACION
    $("#AgregarCatalogoIngresos").modal("hide");
});

// DETALLES
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnDetalle", function () {
    let validacionPermiso = userModelState("CatalogoDeIngresos/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        $.ajax({
            url: "/CatalogoDeIngresos/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    let tipoIngreso = getTipoIngreso(data[0].cin_TipoIngreso);
                    console.table(tipoIngreso)
                    var FechaCrea = FechaFormato(data[0].cin_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].cin_FechaModifica);
                    $("#Detallar #cin_IdIngreso").html(data[0].cin_IdIngreso);
                    $("#Detallar #cin_DescripcionIngreso").html(data[0].cin_DescripcionIngreso);
                    $("#Detallar #cin_UsuarioCrea").val(data[0].cin_UsuarioCrea);
                    $("#Detallar #cin_FechaCrea").val(FechaCrea);
                    $("#Detallar #tipoDeIngresoDetalle").html(tipoIngreso);
                    data[0].UsuModifica == null ? $("#Detallar #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detallar #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                    $("#Detallar #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                    $("#Detallar #cin_UsuarioModifica").val(data[0].cin_UsuarioModifica);
                    $("#Detallar #cin_FechaModifica").val(FechaModifica);
                    $("#DetailCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });

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

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnEditarIngreso", function () {
    let validacionPermiso = userModelState("CatalogoDeIngresos/Edit");

    if (validacionPermiso.status == true) {
        //OCULTAR VALIDACIONES
        OcultarValidacionesEditar();
        //CAPTURAR EL ID DEL REGISTRO
        var ID = $(this).data('id');
        //SETEAR LA VARIABLE GLOBAL DE INACTIVACION
        InactivarID = ID;
        //REALIZAR LA PETICION AL SERVIDOR
        $.ajax({
            url: "/CatalogoDeIngresos/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    //SETEAR LOS CAMPOS
                    $("#Editar #cin_IdIngreso").val(data.cin_IdIngreso);
                    $("#Editar #cin_DescripcionIngreso").val(data.cin_DescripcionIngreso);
                    $('#idTipoIngreso').val((data.cin_TipoIngreso == null) ? 4 : data.cin_TipoIngreso).trigger('change');
                    //MOSTRAR MODAL DE EDICION
                    $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
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

//DESPLEGAR MODAL DE CONFIRMA EDICION
$("#btnUpdateIngresos").click(function () {
    //DESBLOQUEAR EL BOTON DE EDICION
    $("#btnEditarIngresos").attr("disabled", false);
    //CAPTURAR EL VALOR DE EL CAMPO DESCRIPCION
    var descedit = $("#Editar #cin_DescripcionIngreso").val();
    let validacionTipoIngreso = ValidarTipoIngreso('Editar');
    //VALIDAR MODELSTATE
    if (ValidarCamposEditar(descedit) && validacionTipoIngreso == true) {
        //OCULTAR EL MODAL DE EDICION
        $("#EditarCatalogoIngresos").modal('hide');
        //MOSTRAR EL MODAL DE CONFIRMACION
        $("#EditarCatalogoIngresosConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarIngresos").click(function () {

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $('#Editar input[name$="cin_DescripcionIngreso"').val();
    var id = $('#Editar input[name$="cin_IdIngreso"').val();
    let TipoIngreso = $('#Editar select[name$="cin_TipoIngreso"').val();

    var descedit = $("#Editar #cin_DescripcionIngreso").val();
    let validacionTipoIngreso = ValidarTipoIngreso('Editar');
    //VALIDAR MODELSTATE
    if (ValidarCamposEditar(descedit) && validacionTipoIngreso == true) {
        //BLOQUEAR EL BOTON DE EDICION
        $("#btnEditarIngresos").attr("disabled", true);
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Edit",
            method: "POST",
            data: { cin_DescripcionIngreso: data, id: id, cin_TipoIngreso: TipoIngreso },
            beforeSend: function () {
                resetTimeOut();
                clearInterval(timer);
                timer = setInterval(() => {
                    console.log(++timeOut);
                    if (timeOut == 10) {
                        cerrarSesion();
                    }
                }, 5000);
            },
            data: { cin_DescripcionIngreso: data, id: id, cin_TipoIngreso: TipoIngreso }
        }).done(function (data) {

            if (data != "error") {
                //REFRESCAR LA DATA DEL DATATBLE
                cargarGridIngresos();
                $("#EditarCatalogoIngresosConfirmacion").modal('hide');

                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });

            } else {
                //DESBLOQUEAR EL BOTON DE EDICION
                $("#btnEditarIngresos").attr("disabled", false);
                //MOSTRAR MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }

        });


    }
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    //OCULTAR MODAL DE EDICION
    $("#EditarCatalogoIngresos").modal('hide');
});

//CERRAR EL MODAL DE CONFIRMACION DE LA EDICION
$("#btnEditarNo").click(function () {
    //OCULTAR EL MODAL DE CONFIRMACION DE EDITAR
    $("#EditarCatalogoIngresosConfirmacion").modal('hide');
    //MOSTRAR EL MODAL DE EDITAR
    $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
});

// INACTIVAR 
$("#btnModalInactivar").click(function () {
    // validar informacion del usuario
    let validacionPermiso = userModelState("CatalogoDeIngresos/Inactivar");

    if (validacionPermiso.status == true) {
        //DESBLOQUEAR EL BOTON
        $("#btnInactivarIngresos").attr("disabled", false);
        //OCULTAR EL MODAL DE EDICION
        $("#EditarCatalogoIngresos").modal('hide');
        //MOSTRAR EL MODAL DE INACTIVACION
        $("#InactivarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    }
});

//CERRAR EL MODAL DE CONFIRMACION DE INACTIVAR
$("#btnNoInactivar").click(function () {
    //OCULTAR EL MODAL DE INACTIVACION
    $("#InactivarCatalogoIngresos").modal('hide');
    //MOSTRAR EL MODAL DE EDICION
    $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
});

//CONFIRMAR LA INACTIVACION
$("#btnInactivarIngresos").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnInactivarIngresos").attr("disabled", true);
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmInactivarCatalogoIngresos").serializeArray();
    var ID = InactivarID;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Inactivar/" + ID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON
            $("#btnInactivarIngresos").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            //OCULTAR EL MODAL
            $("#InactivarCatalogoIngresos").modal('hide');
            //REFRESCAR LA DATA DEL DATATBLE
            cargarGridIngresos();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    $("#frmCatalogoIngresos").submit(function (e) {
        return false;
    });
});

//FUNCION: PRIMERA FASE DE ACTIVAR
var IDActivar = 0;
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnActivar", function () {
    let validacionPermiso = userModelState("CatalogoDeIngresos/Activar");

    if (validacionPermiso.status == true) {
        //DESBLOQUEAR EL BOTON DE ACTIVAR
        $("#btnActivarIngreso").attr("disabled", false);
        //SETEAR LA VARIABLE GLOBAL DE ACTIVACION
        IDActivar = $(this).data('id');
        //FUNCION: MOSTRAR EL MODAL DE ACTIVAR
        $("#ActivarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR LA ACTIVACION DEL REGISTRO
$("#btnActivarIngreso").click(function () {
    //BLOQUEAR EL BOTON DE ACTIVAR
    $("#btnActivarIngreso").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE ACTIVAR
            $("#btnActivarIngreso").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarCatalogoIngresos").modal('hide');
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridIngresos();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(Descripcion) {
    var Local_modelState = true;
    //VALIDACIONES DEL CAMPO DESCRIPCION
    if (Descripcion != "-1") {
        var LengthString = Descripcion.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Descripcion.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #cin_DescripcionIngreso").val(Descripcion.substring(0, FirstChar + 1));
        }
        if (Descripcion == "" || Descripcion == " " || Descripcion == "  " || Descripcion == null || Descripcion == undefined) {
            if (Descripcion == ' ')
                $("#Crear #cin_DescripcionIngreso").val("");
            Local_modelState = false;
            $("#Crear #asteriscoCreate").addClass("text-danger");
            $("#Crear #DescripcionCrear").show();

        } else {
            $("#Crear #asteriscoCreate").removeClass("text-danger");
            $("#Crear #DescripcionCrear").hide();
        }
    }
    return Local_modelState;
}
function ValidarTipoIngreso(modal) {
    let todoBien = true;
    let tipoIngreso = $('#' + modal + ' #idTipoIngreso').val();

    if (tipoIngreso != null && tipoIngreso != 0) {
        $('#' + modal + ' #valTipoIngreso').hide();
        $('#' + modal + ' #asteriscoTipoIngreso').removeClass('text-danger')

    } else {
        $('#' + modal + ' #valTipoIngreso').show();
        $('#' + modal + ' #asteriscoTipoIngreso').addClass('text-danger')
        todoBien = false;
    }
    return todoBien;
}

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposEditar(Descripcion) {
    var Local_modelState = true;
    //VALIDACIONES DEL CAMPO DESCRIPCION
    if (Descripcion != "-1") {
        var LengthString = Descripcion.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Descripcion.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #cin_DescripcionIngreso").val(Descripcion.substring(0, FirstChar + 1));
        }
        if (Descripcion == "" || Descripcion == " " || Descripcion == "  " || Descripcion == null || Descripcion == undefined) {
            if (Descripcion == ' ')
                $("#Editar #cin_DescripcionIngreso").val("");
            Local_modelState = false;
            $("#Editar #asteriscoEdit").addClass("text-danger");
            $("#Editar #DescripcionEditar").show();

        } else {
            $("#Editar #asteriscoEdit").removeClass("text-danger");
            $("#Editar #DescripcionEditar").hide();
        }
    }
    return Local_modelState;
}

//OCULTAR LAS VALIDACIONES DE CREAR
function OcultarValidacionesCrear() {
    //OCULTAR EL SPAN
    $("#Crear #DescripcionCrear").hide();
    $("#Crear #valTipoIngreso").hide();
    //VACIAR EL INPUT
    $("#Crear #cin_DescripcionIngreso").val('');
    $('#Crear #asteriscoTipoIngreso').removeClass('text-danger');
    //REMOVER EL TEXT DANGER DEL ASTERISCO
    $('#asteriscoCreate').removeClass('text-danger');
}

//OCULTAR LAS VALIDACIONES DE EDITAR
function OcultarValidacionesEditar() {
    //OCULTAR EL SPAN
    $("#Editar #DescripcionEditar").hide();
    $("#Editar #valTipoIngreso").hide();
    //VACIAR EL INPUT
    $("#Editar #cin_DescripcionIngreso").val('');
    //REMOVER EL TEXT DANGER DEL ASTERISCO
    $('#Editar #asteriscoEdit').removeClass('text-danger');
    $('#Editar #asteriscoTipoIngreso').removeClass('text-danger');
}