var IDInactivar = 0;

// script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
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
function cargarGridPeriodo() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/Periodos/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {

                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }

            var ListPeriodo = data;
            // limpiar datatable
            $('#tblPeriodo').DataTable().clear();

            for (var i = 0; i < ListPeriodo.length; i++) {
                var FechaCrea = FechaFormato(ListPeriodo[i].peri_FechaCrea);
                var FechaModifica = FechaFormato(ListPeriodo[i].peri_FechaModifica);
                UsuarioModifica = ListPeriodo[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListPeriodo[i].NombreUsuarioModifica;

                // variable para verificar el estado del registro
                var estadoRegistro = ListPeriodo[i].peri_Activo == false ? 'Inactivo' : 'Activo'

                // variable boton detalles
                var botonDetalles = ListPeriodo[i].peri_Activo == true ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetallePeriodo">Detalles</button>' : '';

                // variable boton editar
                var botonEditar = ListPeriodo[i].peri_Activo == true ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-default btn-xs"  id="btnEditarPeriodo">Editar</button>' : '';

                // variable donde está el boton activar
                var botonActivar = ListPeriodo[i].peri_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarPeriodos">Activar</button>' : '' : '';

                // agregar row
                $('#tblPeriodo').dataTable().fnAddData([
                    ListPeriodo[i].peri_IdPeriodo,
                    ListPeriodo[i].peri_DescripPeriodo,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

// create 1
$(document).on("click", "#btnAgregarPeriodo", function () {

    // habilitar boton
    $("#btnCrearPeriodoConfirmar").attr("disabled", false);

    // vaciar cajas de texto
    $('#Crear input[type=text], input[type=number]').val('');

    // * descripcion 
    $('#AsteriscoDescripcion').removeClass('text-danger');

    // mesanje descripcion requerida
    $("#Crear #validation_DescripcionRequerida").css('display', 'none');

    // mesanje descripcion requerida
    $("#Crear #validation_DescripcionNumerico").css('display', 'none');

    // modal crear
    $("#CrearPeriodo").modal();
});

// create validaciones keyup
$('#Crear #peri_DescripPeriodo').keyup(function () {

    var descripcion = $("#Crear #peri_DescripPeriodo").val();

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
$('#btnCrearPeriodoConfirmar').click(function () {

    // deshabilitar boton
    $("#btnCrearPeriodoConfirmar").attr("disabled", true);

    var modelState = true;

    var descripcion = $("#Crear #peri_DescripPeriodo").val();

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

        var data = $("#frmCreatePeriodo").serializeArray();

        
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/Periodos/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data == "error") {

                $("#btnCrearPeriodoConfirmar").attr("disabled", false);

                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
            else {
                $("#CrearPeriodo").modal('hide');
                cargarGridPeriodo();
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
    else {

        // habilitar boton
        $("#btnCrearPeriodoConfirmar").attr("disabled", false);
    }
});




// editar 1
$(document).on("click", "#tblPeriodo tbody tr td #btnEditarPeriodo", function () {
   
    var ID = $(this).data('id');
    IDInactivar = ID;

    // habilitar boton
    $("#btnUpdatePeriodo").attr("disabled", false);
    
    // * descripcion 
    $('#EditAsteriscoDescripcion').removeClass('text-danger');

    // mesanje descripcion requerida
    $("#Editar #validation_EditDescripcionRequerida").css('display', 'none');

    // mesanje descripcion requerida
    $("#Editar #validation_EditDescripcionNumerico").css('display', 'none');

    $.ajax({
        url: "/Periodos/Edit/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {            
            if (data) {
                $.each(data, function (i, iter) {
                    $("#Editar #peri_IdPeriodo").val(iter.peri_IdPeriodo);
                    $("#Editar #peri_DescripPeriodo").val(iter.peri_DescripPeriodo);
                });
                $("#EditarPeriodo").modal();
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});

// update descripcion validaciones keyup
$('#Editar #peri_DescripPeriodo').keyup(function () {

    var descripcion = $("#Editar #peri_DescripPeriodo").val();

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

$("#btnUpdatePeriodo").click(function () {
    
    // deshabilitar boton
    $("#btnUpdatePeriodo").attr('disabled', true);

    var modelState = true;
    var descripcion = $("#Editar #peri_DescripPeriodo").val();

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
        $("#EditarPeriodo").modal('hide');
        $("#ConfirmarEdicion").modal();
        //DESBLOQUEAR EL BOTON DE CONFIRMAR EDICION
        $("#btnConfirmarEditar").attr("disabled", false);
    }
    else {
        // habilitar boton
        $("#btnUpdatePeriodo").attr('disabled', false);
    }

});

$("#btnCerrarConfirmarEditar").click(function () {   

    //ocultar modal de confirmación
    $("#ConfirmarEdicion").modal('hide');

    // habilitar boton
    $("#btnUpdatePeriodo").attr('disabled', false);

    //modal de edicion
    $("#EditarPeriodo").modal();
});

//editar 3 ejecutar 
$(document).on("click", "#btnConfirmarEditar", function () {   
    
    
        var data = $("#frmEditPeriodo").serializeArray();
        $.ajax({
            url: "/Periodos/Editar",
            method: "POST",
            data: data
        })
            .done(function (data) {
                
                if (data != 'error') {
                    
                    // habilitar boton
                    $("#btnConfirmarEditar").attr("disabled", false);
                    
                    // actualizar datatable
                    cargarGridPeriodo();
                    $("#ConfirmarEdicion").modal('hide');

                    iziToast.success({
                        title: 'Exito',
                        message: '¡El registro se editó de forma exitosa!',
                    });
                } 
                else {
                    $("#ConfirmarEdicion").modal('hide');

                    // habilitar boton
                    $("#btnUpdatePeriodo").attr('disabled', false);

                    $("#EditarPeriodo").modal();
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se editó el registro, contacte al administrador!',
                    });
                }
            });
    $("#btnConfirmarEditar").attr("disabled", false);
});




//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPeriodo tbody tr td #btnDetallePeriodo", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/Periodos/Details/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {

                    var FechaCrea = FechaFormato(data[0].peri_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].peri_FechaModifica);
                    $("#Detalles #peri_IdPeriodo").html(iter.peri_IdPeriodo);
                    $("#Detalles #peri_DescripPeriodo").html(iter.peri_DescripPeriodo);
                    data[0].peri_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #peri_UsuarioCrea").html(iter.peri_UsuarioCrea);
                    $("#Detalles #peri_FechaCrea").html(FechaCrea);
                    data[0].peri_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #peri_UsuarioModifica").html(data[0].peri_UsuarioModifica);
                    $("#Detalles #peri_FechaModifica").html(FechaModifica);
                });
                $("#DetallarPeriodo").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarPeriodo", function () {
    //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnInactivarPeriodoConfirmar").attr("disabled", false);
    //OCULTAR MODAL DE EDICION
    $("#EditarPeriodo").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarPeriodo").modal();
});

//CERRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarPeriodo").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarPeriodo").modal();
});

//CONFIRMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPeriodoConfirmar").click(function () {
    //BLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnInactivarPeriodoConfirmar").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Periodos/Inactivar/" + IDInactivar,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
            $("#btnInactivarPeriodoConfirmar").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridPeriodo();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarPeriodo").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});


//
//ACTIVAR
var ActivarID = 0;
$(document).on("click", "#btnActivarPeriodos", function () {
    //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnActivarPeriodoConfirm").attr("disabled", false);
    ActivarID = $(this).data('id');
    //DESPLEGAR EL MODAL DE ACTIVAR
    $("#ActivarPeriodo").modal();
});

//CONFIRMAR ACTIVACION DEL REGISTRO
$("#btnActivarPeriodoConfirm").click(function () {
    //BLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnActivarPeriodoConfirm").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Periodos/Activar/" + ActivarID,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
            $("#btnActivarPeriodoConfirm").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridPeriodo();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarPeriodo").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    ActivarID = 0
});

//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPeriodo tbody tr td #btnDetallePeriodo", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/Periodos/Details/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {

                    var FechaCrea = FechaFormato(data[0].peri_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].peri_FechaModifica);
                    $("#Detalles #peri_IdPeriodo").html(iter.peri_IdPeriodo);
                    $("#Detalles #peri_DescripPeriodo").html(iter.peri_DescripPeriodo);
                    data[0].peri_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #peri_UsuarioCrea").html(iter.peri_UsuarioCrea);
                    $("#Detalles #peri_FechaCrea").html(FechaCrea);
                    data[0].peri_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #peri_UsuarioModifica").html(data[0].peri_UsuarioModifica);
                    $("#Detalles #peri_FechaModifica").html(FechaModifica);
                });
                $("#DetallarPeriodo").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});

//*****************CREAR******************//

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearPeriodo #Validation_descripcion").css("display", "none");
    $("#CrearPeriodo").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreatePeriodo").submit(function (event) {
    event.preventDefault();
});



function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
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


//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#EditarPeriodo #Validation_descripcion").css("display", "none");
    $("#EditarPeriodo").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#EditarPeriodo #Validation_descripcion").css("display", "none");
    $("#EditarPeriodo").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditPeriodo").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarPeriodo", function () {
    $("#DetallesFormaPago").modal('hide');
    $("#InactivarFormaPago").modal();
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarPeriodo").modal('hide');
});


//*****************DETALLES******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarDetalles").click(function () {
    $("#DetallarPeriodo #Validation_descripcion").css("display", "none");
    $("#DetallarPeriodo").modal('hide');
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarDetails").click(function () {
    $("#DetallarPeriodo").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE DETALLE
$("#frmDetailsPeriodo").submit(function (event) {
    event.preventDefault();
});

function mostrarError(Mensaje) {
    iziToast.error({
        title: 'Error',
        message: Mensaje,
    });
}