//VARIABLE GLOBAL PARA INACTIVAR
var inactivar = 0;

// script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });

// funcion generica ajax
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

// actualizar datatable
function cargarGridTipoDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/TipoDeducciones/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se pudo cargar la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaTipoDeducciones = data;
            $('#tblTipoDeducciones').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTipoDeducciones.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaTipoDeducciones[i].tde_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-primary btn-xs" style="margin-right:3px;" id="btnDetalleTipoDeducciones">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaTipoDeducciones[i].tde_Activo == true ? '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-default btn-xs"  id="btnEditarTipoDeducciones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaTipoDeducciones[i].tde_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-default btn-xs"  id="btnActivarTipoDeducciones">Activar</button>' : '' : '';


                var FechaCrea = FechaFormato(ListaTipoDeducciones[i].tde_FechaCrea);
                var FechaModifica = FechaFormato(ListaTipoDeducciones[i].tde_FechaModifica);

                UsuarioModifica = ListaTipoDeducciones[i].tde_UsuarioModifica == null ? 'Sin modificaciones' : ListaTipoDeducciones[i].NombreUsuarioModifica;

                $('#tblTipoDeducciones').dataTable().fnAddData([
                    ListaTipoDeducciones[i].tde_IdTipoDedu,
                    ListaTipoDeducciones[i].tde_Descripcion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

// create 1 
$(document).on("click", "#btnAgregarTipoDeducciones", function () {
    var validacionPermiso = userModelState("TipoDeducciones/Create");

if (validacionPermiso.status == true) {
    // habilitar boton
    $("#btnCreateRegistroTipoDeducciones").attr("disabled", false);

    // vaciar cajas de texto
    $('#Crear input[type=text], input[type=number]').val('');

    // * descripcion 
    $('#AsteriscoDescripcion').removeClass('text-danger');

    // mesanje descripcion requerida
    $("#Crear #validation_DescripcionRequerida").css('display', 'none');

    // mesanje descripcion requerida
    $("#Crear #validation_DescripcionNumerico").css('display', 'none');

    // mostrar modal
    $("#AgregarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
}
});
// create validaciones keyup
$('#Crear #tde_Descripcion').keyup(function () {

    var descripcion = $("#Crear #tde_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

});

// create 2 ejecutar
$('#btnCreateRegistroTipoDeducciones').click(function () {

    $('#btnCreateRegistroTipoDeducciones').attr('disabled', true);

    var modelState = true;
    var descripcion = $("#Crear #tde_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
        modelState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
        modelState = false;
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }


    if (modelState == true) {
        var data = $("#frmTipoDeduccionCreate").serializeArray();

        if ($("#Crear #tde_Descripcion").val()) {

            $.ajax({
                url: "/TipoDeducciones/Create",
                method: "POST",
                data: data
            }).done(function (data) {

                if (data == "error") {

                    $('#btnCreateRegistroTipoDeducciones').attr('disabled', false);
                }
                else {
                    $("#AgregarTipoDeducciones").modal('hide');
                    cargarGridTipoDeducciones();
                    iziToast.success({
                        title: 'Éxito',
                        message: '¡El registro se agregó de forma exitosa!',
                    });
                }
            });
        }
    }
    else {

        $('#btnCreateRegistroTipoDeducciones').attr('disabled', false);
    }
});
// editar 1 
$(document).on("click", "#tblTipoDeducciones tbody tr td #btnEditarTipoDeducciones", function () {
    var validacionPermiso = userModelState("TipoDeducciones/Edit");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        inactivar = ID;

        // habilitar boton
        $("#btnUpdateTipoDeducciones").attr("disabled", false);

        // vaciar cajas de texto
        $('#Editar input[type=text], input[type=number]').val('');

        // * descripcion 
        $('#EditAsteriscoDescripcion').removeClass('text-danger');

        // mesanje descripcion requerida
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');

        // mesanje descripcion requerida
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');

        $.ajax({
            url: "/TipoDeducciones/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {

                if (data) {

                    $.each(data, function (i, iter) {
                        $("#Editar #tde_IdTipoDedu").val(iter.tde_IdTipoDedu);
                        $("#Editar #tde_Descripcion").val(iter.tde_Descripcion);
                    });

                    $("#EditarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    iziToast.error({
                        title: 'Error',
                        message: 'No se cargó la información, contacte al administrador',
                    });
                }
            });
    }
});

// editar validaciones key up
$('#Editar #tde_Descripcion').keyup(function () {

    var descripcion = $("#Editar #tde_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#EditAsteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#EditAsteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');
        $("#Editar #validation_EditDescripcionNumerico").css('display', '');
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
    }
});

// editar 2
$("#btnUpdateTipoDeducciones").click(function () {

    $("#btnUpdateTipoDeducciones").attr('disabled', true);

    var modelState = true;
    var descripcion = $("#Editar #tde_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#EditAsteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
        modelState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#EditAsteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');
        $("#Editar #validation_EditDescripcionNumerico").css('display', '');
        modelState = false;
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
    }


    if (modelState == true) {

        $("#EditarTipoDeducciones").modal('hide');
        $("#EditarTipoDeduccionConfirmacion").modal({ backdrop: 'static', keyboard: false });

    }
    else {
        $("#btnUpdateTipoDeducciones").attr('disabled', false);
    }

});

$("#denegarEdicion").click(function () {
    $("#btnUpdateTipoDeducciones").attr('disabled', false);
    $("#EditarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
});

// editar 3 ejecutar
$("#btnEditarTipoDedu").click(function () {

    var data = $("#frmTipoDeduccionEdit").serializeArray();

    $.ajax({
        url: "/TipoDeducciones/Edit",
        method: "POST",
        data: data
    }).done(function (data) {

        if (data != "error") {

            $("#EditarTipoDeduccionConfirmacion").modal('hide');
            cargarGridTipoDeducciones();
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
        else {

            $("#EditarTipoDeduccionConfirmacion").modal('hide');
            $("#EditarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
            $("#btnUpdateTipoDeducciones").attr('disabled', false);
            iziToast.error({
                title: 'Error',
                message: 'El registro no se editó, contacte al administrador',
            });
        }
    });

});

const btneditar = $('#btnEditarTipoDedu'),

    cargandoEditar = $('#cargandoEditar')//Div que aparecera cuando se le de click en crear

function mostrarcargandoEditar() {
    btneditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarcargandoEditar() {
    btneditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}


//Mostrar el spinner
function spinner() {
    return `<div class="sk-spinner sk-spinner-wave">
 <div class="sk-rect1"></div>
 <div class="sk-rect2"></div>
 <div class="sk-rect3"></div>
 <div class="sk-rect4"></div>
 <div class="sk-rect5"></div>
 </div>`;
}


$(document).on("click", "#tblTipoDeducciones tbody tr td #btnDetalleTipoDeducciones", function () {    
    var validacionPermiso = userModelState("TipoDeducciones/Details");
if (validacionPermiso.status == true) {
    var ID = $(this).data('id');
    //
    $.ajax({
        url: "/TipoDeducciones/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                var FechaCrea = FechaFormato(data[0].tde_FechaCrea);
                var FechaModifica = FechaFormato(data[0].tde_FechaModifica);
                $("#Detalles #tde_UsuarioCrea").val(data[0].tde_UsuarioCrea);
                $("#Detalles #tde_IdTipoDedu").val(data[0].tde_IdTipoDedu);
                $("#Detalles #tde_Descripcion").html(data[0].tde_Descripcion);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #tde_FechaCrea").html(FechaCrea);
                $("#Detalles #tde_UsuarioModifica").html(data.tde_UsuarioModifica);
                $("#Detalles #tde_FechaModifica").html(FechaModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION

                //$("#DetailsTipoDeducciones").modal();
                $("#DetailsTipoDeducciones").modal({ backdrop: 'static', keyboard: false });


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

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA MENSAJE DE CONFIRMACION
$("#btnInactivarTipoDeducciones").click(function () {
    var validacionPermiso = userModelState("TipoDeducciones/Inactivar");

    if (validacionPermiso.status == true) {
        $("#EditarTipoDeducciones").modal('hide');
        //$("#InactivarTipoDeducciones").modal();
        $("#InactivarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
    }
});

// inactivar ejecutar
$("#btnInactivarRegistroTipoDeducciones").click(function () {

    $.ajax({
        url: "/TipoDeducciones/Inactivar/" + inactivar,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: inactivar })
    })
        .done(function (data) {

            if (data != "Error") {

                $("#InactivarTipoDeducciones").modal('hide');
                cargarGridTipoDeducciones();
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se inactivó de forma exitosa!',
                });
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se inactivó el registro, contacte al administrador',
                });
            }
        });
});

//FUNCION: OCULTAR MODAL DE CREACION
$("#btnCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE EDICION
$("#btnCerrarEditar").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: HABILITAR EL DATAANNOTATION AL DESPLEGAR EL MODAL
$("#btnCerrar").click(function () {
    $("#EditarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });

});



$("#frmTipoDeduccionCreate").submit(function (event) {
    event.preventDefault();
});

$("#frmTipoDeduccionEdit").submit(function (event) {
    event.preventDefault();
});

// activar
$(document).on("click", "#tblTipoDeducciones tbody tr td #btnActivarTipoDeducciones", function () {
    activarID = $(this).data('id');
    var validacionPermiso = userModelState("TipoDeducciones/Activar");

    if (validacionPermiso.status == true) {
        //$("#ActivarTipoDeducciones").modal();
        $("#ActivarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
    }
});

//FUNCION: SEGUNDA FASE DE EDICION DE REGISTROS, REALIZAR LA EJECUCION PARA INACTIVAR EL REGISTRO
$("#btnActivarRegistroTipoDeducciones").click(function () {
    $.ajax({
        url: "/TipoDeducciones/Activar/" + activarID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: { id: activarID }
    }).done(function (data) {
        $("#ActivarTipoDeducciones").modal('hide');
        //Refrescar la tabla de TipoDeducciones
        cargarGridTipoDeducciones();

        //Mensaje de error si no hay data
        iziToast.success({
            title: 'Exito',
            message: '¡El registro se activó de forma exitosa!',
        });
    });
});

$("#frmTipoDeduccionEdit").submit(function (e) {
    return false;
});
