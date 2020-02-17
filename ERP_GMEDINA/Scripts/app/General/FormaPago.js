// actualizar datatable
function cargarGridFormaPago() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/FormaPago/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }

            var ListaFormaPago = data;
            //limpiar datatable
            $('#tblFormaPago').DataTable().clear();

            for (var i = 0; i < ListaFormaPago.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaFormaPago[i].fpa_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetallesFormaPago">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaFormaPago[i].fpa_Activo == true ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-default btn-xs" id="btnEditarFormaPago">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaFormaPago[i].fpa_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-default btn-xs"  id="btnActivarFormaPago">Activar</button>' : '' : '';

                //agregar fila
                $('#tblFormaPago').dataTable().fnAddData([
                     ListaFormaPago[i].fpa_IdFormaPago,
                     ListaFormaPago[i].fpa_Descripcion,
                     estadoRegistro,
                     botonDetalles + botonEditar + botonActivar]
                 );
            }
            FullBody();
        });
}


// create 1
$(document).on("click", "#btnAgregarFormaPago", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("FormaPago/Create");

    if (validacionPermiso.status == true) {

        // habilitar boton
        $('#btnCrearFormaPago').attr('disabled', false);

        // vaciar cajas de texto
        $('#Crear input[type=text], input[type=number]').val('');

        // * descripcion 
        $('#AsteriscoDescripcion').removeClass('text-danger');

        // mesanje descripcion requerida
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');

        // mesanje descripcion requerida
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');

        // modal
        $("#CrearFormaPago").modal({ backdrop: 'static', keyboard: false });
    }
});

// validaciones key up create 

// validar descripcion  create
$('#Crear #fpa_Descripcion').keyup(function () {

    var descripcion = $("#Crear #fpa_Descripcion").val();

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

// create 2 ejecurar
$('#btnCrearFormaPago').click(function () {

    // deshabilitar el boton
    $("#btnCrearFormaPago").attr("disabled", true);

    var modalState = true;

    // validaciones 
    var descripcion = $("#Crear #fpa_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
        modalState = false;
        $("#Crear #fpa_Descripcion").focus();
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
        modalState = false;
        $("#Crear #fpa_Descripcion").focus();
    }
        // si es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }


    if (modalState == true) {

        var data = $("#frmCreateFormaPago").serializeArray();

        $.ajax({
            url: "/FormaPago/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });

                $("#btnCrearFormaPago").attr("disabled", false);
            }
            else {

                $("#CrearFormaPago").modal('hide');
                cargarGridFormaPago();

                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
        $("#AsteriscoFormaPago").removeClass("text-danger");
    }
    else {
        $("#btnCrearFormaPago").attr("disabled", false);
    }
});

// editar 1
$(document).on("click", "#tblFormaPago tbody tr td #btnEditarFormaPago", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("FormaPago/Edit");

    if (validacionPermiso.status == true) {

        $("#btnConfirmarEditar2").attr('disabled', false);
        $("#btnUpdateFormaPago").attr('disabled', false);

        var ID = $(this).data('id');
        IDInactivar = ID;

        // * descripcion 
        $('#EditAsteriscoDescripcion').removeClass('text-danger');

        // mesanje descripcion requerida
        $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');

        // mesanje descripcion requerida
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');

        $.ajax({
            url: "/FormaPago/Edit/" + ID,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {

                if (data) {
                    $.each(data, function (i, iter) {
                        $("#Editar #fpa_IdFormaPago").val(iter.fpa_IdFormaPago);
                        $("#Editar #fpa_Descripcion").val(iter.fpa_Descripcion);
                    });
                    $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se cargó la información, contacte al administrador!',
                    });
                }
            });
    }
});


// validaciones key up udapte 

// validar descripcion  update
$('#Editar #fpa_Descripcion').keyup(function () {

    var descripcion = $("#Editar #fpa_Descripcion").val();

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
        $("#Editar #validation_EditDescripcionNumerico").css('display', '');
    }
        // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
    }

});

// editar 2 validar modal
$("#btnUpdateFormaPago").click(function () {
    $("#btnUpdateFormaPago").attr('disabled', true);

    var modelState = true;
    var descripcion = $("#Editar #fpa_Descripcion").val();

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
        $("#Editar #validation_EditDescripcionNumerico").css('display', '');
        modelState = false;
    }
        // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditAsteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');
    }

    if (modelState == true) {
        $("#EditarFormaPago").modal('hide');
        $("#btnConfirmarEditar2").attr('disabled', false);
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
    }
    else {
        $("#btnUpdateFormaPago").attr('disabled', false);
    }
});

//editar 3 ejecutar
$("#btnConfirmarEditar2").click(function () {
    $("#btnConfirmarEditar2").attr('disabled', true);

    var data = $("#frmEditFormaPago").serializeArray();

    $.ajax({
        url: "/FormaPago/Editar",
        method: "POST",
        data: data
    })
    .done(function (data) {

        if (data != 'error') {

            cargarGridFormaPago();
            $("#ConfirmarEdicion").modal('hide');

            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
        else {
            $("#ConfirmarEdicion").modal('hide');
            $("#btnUpdateFormaPago").attr('disabled', false);
            $("#btnConfirmarEditar2").attr('disabled', false);
            $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });

            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
        }
    });
});

$(document).on("click", "#tblFormaPago tbody tr td #btnDetallesFormaPago", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("FormaPago/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        $.ajax({
            url: "/FormaPago/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    var FechaCrea = FechaFormato(data[0].fpa_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].fpa_FechaModifica);
                    $(".field-validation-error").css('display', 'none');
                    $("#frmDetailFormaPago #fpa_Descripcion").html(data[0].fpa_Descripcion);
                    $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#frmDetailFormaPago #fpa_FechaCrea").html(FechaCrea);
                    data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#fpa_UsuarioModifica").val(data[0].fpa_UsuarioModifica);
                    $("#frmDetailFormaPago #fpa_FechaModifica").html(FechaModifica);
                    $("#frmDetailFormaPago").modal({ backdrop: 'static', keyboard: false });
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


//CERRAR MODAL DE CONFIRMACIÓN DE EDICION
$(document).on("click", "#btnCerrarConfirmarEditar", function () {
    $("#btnUpdateFormaPago").attr('disabled', false);
    $("#btnConfirmarEditar2").attr('disabled', false);

    $("#ConfirmarEdicion").modal('hide');
    $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//
//INACTIVAR

var IDInactivar = 0;

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarFormaPago", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("FormaPago/Inactivar");

    if (validacionPermiso.status == true) {
        document.getElementById("btnInactivarFormaPagoConfirm").disabled = false;
        //OCULTAR MODAL DE EDICION
        $("#EditarFormaPago").modal('hide');
        //MOSTRAR MODAL DE INACTIVACION
        $("#InactivarFormaPago").modal({ backdrop: 'static', keyboard: false });
    }
});

//OCULTAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    document.getElementById("btnInactivarFormaPagoConfirm").disabled = false;
    //OCULTAR DATAANOTATIONS
    //DataAnnotations(true);
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarFormaPago").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//CONFORMAR INACTIVACION DEL REGISTRO
$("#btnInactivarFormaPagoConfirm").click(function () {
    document.getElementById("btnInactivarFormaPagoConfirm").disabled = true;
    //SE OCULTA EL MODAL DE EDICION
    $("#InactivarFormaPago").modal('hide');
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/FormaPago/Inactivar/" + IDInactivar,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridFormaPago();
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//
//ACTIVAR

var ActivarID = 0;
//DESPLEGAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnActivarFormaPago", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Planilla/Index");

    if (validacionPermiso.status == true) {
        document.getElementById("btnActivarFormaPagoConfirm").disabled = false;
        ActivarID = $(this).data('id');
        $("#ActivarFormaPago").modal({ backdrop: 'static', keyboard: false });
    }
});

//CONFORMAR ACTIVACION DEL REGISTRO
$("#btnActivarFormaPagoConfirm").click(function () {
    document.getElementById("btnActivarFormaPagoConfirm").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/FormaPago/Activar/" + ActivarID,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
            $("#ActivarFormaPago").modal('hide');
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridFormaPago();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarFormaPago").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    ActivarID = 0
});

//BOTONES


//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
    $("#Crear #AsteriscoFormaPago").css("display", "none");
    $("#EditarFormaPago #Validation_descripcion").css("display", "none");
    $("#CrearFormaPago").modal("hide");
});

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearFormaPago #Validation_descripcion").css("display", "none");
    $("#CrearFormaPago").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreateFormaPago").submit(function (event) {
    event.preventDefault();
});

//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#Crear #AsteriscoFormaPagoEditar").css("display", "none");
    $("#EditarFormaPago #Validation_descripcion").css("display", "none");
    $("#EditarFormaPago").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#EditarFormaPago #Validation_descripcion").css("display", "none");
    $("#EditarFormaPago").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditFormaPago").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarFormaPago", function () {
    $("#InactivarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//******************ACTIVAR*******************//

//MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnmodalActivarFormaPago", function () {
    $("#InactivarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//BOTON PARA CERRAR EL MODAL DE ACTIVAR
$("#btnCerrarActivar").click(function () {
    $("#ActivarFormaPago").modal('hide');
});


// script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      //
  })
  .fail(function (jqxhr, settings, exception) {

  });

//funcion generica ajax
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