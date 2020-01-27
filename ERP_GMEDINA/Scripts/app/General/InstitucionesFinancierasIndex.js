//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        //console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
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

// REGION DE VARIABLES
//var registroID = 0;
var esAdministrador = $("#rol_Usuario").val();

//Funcion para refrescar la tabla (Index)
function cargarGridINFS() {
    //    var esAdministrador = $("#rol_Usuario").val();
    //    cons.log("Hola: " +esAdministrador);
    _ajax(null,
        '/InstitucionesFinancieras/GetData',
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
            var ListaINFS = data;
            //LIMPIAR LA DATA DEL DATATABLE
            $('#IndexTabla').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaINFS.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaINFS[i].insf_Activo == false ? 'Inactivo' : 'Activo';
                //variable boton detalles
                var botonDetalles = ListaINFS[i].insf_Activo == true ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnModalDetallesINFS">Detalles</button>' : '';
                //variable boton editar
                var botonEditar = ListaINFS[i].insf_Activo == true ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" class="btn btn-default btn-xs" id="btnModalEditarINFS">Editar</button>' : '';
                //variable donde está el boton activar
                var botonActivar = ListaINFS[i].insf_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" class="btn btn-primary btn-xs"  id="btnModalActivarINFS">Activar</button>' : '' : '';

                $('#IndexTabla').dataTable().fnAddData([
                    ListaINFS[i].insf_IdInstitucionFinanciera,
                    ListaINFS[i].insf_DescInstitucionFinanc,
                    ListaINFS[i].insf_Contacto,
                    ListaINFS[i].insf_Telefono,
                    ListaINFS[i].insf_Correo,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
}




//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarInstitucion", function () {
    //OCULTAR VALIDACIONES
    Vaciar_ModalCrear();
    //DESBLOQUEAR EL BOTON DE CREACION
    $("#btnCrearInstitucion").attr("disabled", false);
    //VACIAR LOS CAMPOS DEL MODAL
    Vaciar_ModalCrear();
    //MOSTRAR EL MODAL DE AGREGAR
    $("#CrearInstitucion").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearInstitucion').click(function () {

    //CAPTURA DE LOS VALORES DE LOS CAMPOS
    var insf_DescInstitucionFinanc = $("#Crear #insf_DescInstitucionFinanc").val();
    var insf_Contacto = $("#Crear #insf_Contacto").val();
    var insf_Telefono = $("#Crear #insf_Telefono").val();
    var insf_Correo = $("#Crear #insf_Correo").val();

    //VALIDAR QUE EL CAMPO NO ESTE VACIO
    if (DataAnnotationsCrear(insf_DescInstitucionFinanc, insf_Contacto, insf_Telefono, insf_Correo)) {

        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmCreateInstitucionFinanciera").serializeArray();
        //BLOQUEAR EL BOTON
        $("#btnCrearInstitucion").attr("disabled", true);
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/InstitucionesFinancieras/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
                //DESBLOQUEAR EL BOTON
                $("#btnCrearInstitucion").attr("disabled", false);
            }
            else {
                //OCULTAR EL MODAL DE CREACION
                $("#CrearInstitucion").modal('hide');
                //REFRESCAR LA TABLA 
                cargarGridINFS();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
});

//OCULTAR MODAL DE CREACION CON EL BOTON DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearInstitucion").modal("hide");
});

//DESHABILITAR EL POSTBACK DEL CREATE
$("#frmCreateInstitucionFinanciera").submit(function (e) {
    e.preventDefault();
});




//VARIABLE DE INACTIVACION
var IDInactivar = 0;
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#IndexTabla tbody tr td #btnModalEditarINFS", function () {
    //DESBLOQUEAR EL BOTON
    $("#btnConfirmarEditar2").attr("disabled", false);
    //CAPTURAR EL ID DEL REGISTRO SELECCIONADO
    var ID = $(this).data('id');
    //SETEAR LA VARIABLE GLOBAL DE INACTIVACION
    IDInactivar = ID;
    //OCULTAR EL DATAANNOTATIONS
    Vaciar_ModalEditar();
    //EJECUTAR LA PETICION AL SERVIDOR
    $.ajax({
        url: "/InstitucionesFinancieras/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
        if (data != "error") {
            $("#Editar #insf_IdInstitucionFinanciera").val(data[0].insf_IdInstitucionFinanciera);
            $("#Editar #insf_DescInstitucionFinanc").val(data[0].insf_Descripcion);
            $("#Editar #insf_Contacto").val(data[0].insf_Contacto);
            $("#Editar #insf_Telefono").val(data[0].insf_Telefono);
            $("#Editar #insf_Correo").val(data[0].insf_Correo);
            //DESPLEGAR EL MODAL DE EDICION
            $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
        }
        else {
            //Mensaje de error si no hay data
            iziToast.error({
                title: 'Error',
                message: '¡No se cargó la información, contacte al administrador!',
            });
        }
    }).fail(function (jqxhr, settings, exception) {
        console.log("No se pudo realizar la petición");
    });
});

$("#btnCerrarEditar").click(function () {
    //OCULTAR EL MODAL DE EDICION
    $("#EditarInstitucion").modal('hide');
});

//DESPLEGAR MODAL DE CONFIRMACION
$("#btnModalActualizarINFS").click(function () {
    //DESBLOQUEAR BOTON DE EDICION
    $("#btnConfirmarEditar2").attr("disabled", false);

    //CAPTURA DE LOS VALORES DE LOS CAMPOS
    var insf_DescInstitucionFinanc = $("#Editar #insf_DescInstitucionFinanc").val();
    var insf_Contacto = $("#Editar #insf_Contacto").val();
    var insf_Telefono = $("#Editar #insf_Telefono").val();
    var insf_Correo = $("#Editar #insf_Correo").val();

    //VALIDAR QUE EL CAMPO NO ESTE VACIO
    if (DataAnnotationsEditar(insf_DescInstitucionFinanc, insf_Contacto, insf_Telefono, insf_Correo)) {
        //OCULTAR MODAL DE EDICION
        $("#EditarInstitucion").modal('hide');
        //MOSTRAR MODAL DE CONFIRMACION
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
    }
});

//GUARDAR LA EDICION DEL REGISTRO
$("#btnConfirmarEditar2").click(function () {

    //BLOQUEAR EL BOTON
    $("#btnConfirmarEditar2").attr("disabled", true);
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    //var data = $("#frmEditInstitucionFinanciera").serializeArray();

    var data = {
        insf_IdInstitucionFinanciera: $("#Editar #insf_IdInstitucionFinanciera").val(),
        insf_DescInstitucionFinanc: $("#Editar #insf_DescInstitucionFinanc").val(),
        insf_Contacto: $("#Editar #insf_Contacto").val(),
        insf_Telefono: $("#Editar #insf_Telefono").val(),
        insf_Correo: $("#Editar #insf_Correo").val()
    };

    $.ajax({
        url: "/InstitucionesFinancieras/Edit",
        method: "POST",
        data: data
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != 'error') {
                //REFRESCAR LA TABLA 
                cargarGridINFS();
                //OCULTAR MODAL DE CONFIRMACION
                $("#ConfirmarEdicion").modal('hide');
                //MOSTRAR MENSAJE DE EXITO
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
            else {
                //DESBLOQUEAR EL BOTON
                $("#btnConfirmarEditar2").attr("disabled", false);
                //HACER EL CAMBIO DE MODALES
                $("#ConfirmarEdicion").modal('hide');
                $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
                //MOSTRAR MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
});

//CERRAR MODAL DE CONFIRMACIÓN DE EDICION
$(document).on("click", "#btnCerrarConfirmarEditar", function () {
    //DESBLOQUEAR EL BOTON
    $("#btnConfirmarEditar2").attr("disabled", false);
    //OCULTAR MODAL DE CONFIRMACIÓN DE EDICION
    $("#ConfirmarEdicion").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
});

//DESHABILITAR EL POSTBACK DEL EDITAR
$("#frmEditInstitucionFinanciera").submit(function (e) {
    e.preventDefault();
});


//DETALLES
$(document).on("click", "#IndexTabla tbody tr td #btnModalDetallesINFS", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/InstitucionesFinancieras/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                var FechaCrea = FechaFormato(data[0].insf_FechaCrea);
                var FechaModifica = (FechaFormato(data[0].insf_FechaModifica));

                $("#frmDetallesInstitucionFinanciera #insf_IdInstitucionFinanciera").html(data[0].insf_IdInstitucionFinanciera);
                $("#frmDetallesInstitucionFinanciera #insf_DescInstitucionFinanc").html(data[0].insf_Descripcion);
                $("#frmDetallesInstitucionFinanciera #insf_Contacto").html(data[0].insf_Contacto);
                $("#frmDetallesInstitucionFinanciera #insf_Telefono").html(data[0].insf_Telefono);
                $("#frmDetallesInstitucionFinanciera #insf_Correo").html(data[0].insf_Correo);


                /* AUDITORIA */
                $("#frmDetallesInstitucionFinanciera #tbUsuario_usu_NombreUsuario").html(data[0].insf_UsuarioCrea_Nombres);
                $("#frmDetallesInstitucionFinanciera #fpa_FechaCrea").html(FechaCrea);

                $("#frmDetallesInstitucionFinanciera #tbUsuario1_usu_NombreUsuario").html((data[0].insf_UsuarioModifica_Nombres == null) ? "Sin modificaciones" : data[0].insf_UsuarioModifica_Nombres);
                $("#frmDetallesInstitucionFinanciera #insf_FechaModifica").html(FechaModifica);


                //DESPLEGAR MODAL 
                $("#DetailsInstitucion").modal({ backdrop: 'static', keyboard: false });
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


$(document).on("click", "#btnCerrarDetailsInstitucion", function () {
    //OCULTAR MODAL DE EDICION
    $("#DetailsInstitucion").modal('hide');
});

// INACTIVAR 
$(document).on("click", "#btnModalInactivarINFS", function () {
    //DESBLOQUEAR EL BOTON
    $("#btnInactivarINFS").attr("disabled", false);
    //OCULTAR MODAL DE EDICION
    $("#EditarInstitucion").modal('hide');
    //DESPLEGAR EL MODAL DE CONFIRMACION DE INACTIVACION
    $("#frmInactivarINFS").modal({ backdrop: 'static', keyboard: false });
});

//CONFIRMAR INACTIVAR
$("#btnInactivarINFS").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnInactivarINFS").attr("disabled", true);
    //EJECUTAR LA PETICION AL SERVIDOR
    $.ajax({
        url: "/InstitucionesFinancieras/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            //DESBLOQUEAR EL BOTON
            $("#btnInactivarINFS").attr("disabled", false);
            //OCULTAR EL MODAL DE CONFIRMACION DE INACTIVACION
            $("#frmInactivarINFS").modal("hide");
            cargarGridINFS();
            //MOSTRAR EL MENSAJE DE EXITO
            iziToast.success({
                title: 'Exito',
                message: 'El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//CERRAR MODAL DE INACTIVAR CON EL BOTON NO
$("#InactivarInstitucionCerrar").click(function () {
    //OCULTAR MODAL
    $("#frmInactivarINFS").modal("hide");
    //MOSTRAR MODAL
    $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
});




// ACTIVAR
var activarID = 0;
$(document).on("click", "#btnModalActivarINFS", function () {
    //DESBLOQUEAR EL BOTON
    $("#btnActivarINFS").attr("disabled", false);
    //SETEAR LA VARIABLE GLOBAL DE ACTIVAR
    activarID = $(this).data('id');
    //DESPLEGAR EL MODAL DE ACTIVAR
    $("#frmActivarINFS").modal({ backdrop: 'static', keyboard: false });
});

//CONFIRMAR ACTIVAR
$("#btnActivarINFS").click(function () {

    //BLOQUEAR EL BOTON
    $("#btnActivarINFS").attr("disabled", true);
    //EJECUTAR LA PETICION AL SERVIDOR
    $.ajax({
        url: "/InstitucionesFinancieras/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON
            $("#btnActivarINFS").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se logró activar el registro, contacte al administrador',
            });
        }
        else {
            //REFRESCAR LA DATA DEL DATATABLE
            cargarGridINFS();
            //OCULTAR EL MODAL
            $("#frmActivarINFS").modal('hide');
            //MOSTRAR MENSAJE DE EXITO
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se Activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});

//CERRAR MODAL DE EDICION CON EL BOTON CERRAR
$("#ActivarInstitucionCerrar").click(function () {
    //OCULTAR MODAL
    $("#frmActivarINFS").modal("hide");
});




//FUNCION: OCULTAR VALIDACIONES DE CREACION
function Vaciar_ModalCrear() {
    //VACIADO DE INPUTS
    $("#Crear #insf_DescInstitucionFinanc").val("");
    $("#Crear #insf_Contacto").val("");
    $("#Crear #insf_Telefono").val("");
    $("#Crear #insf_Correo").val("");

    //
    //OCULTAR DATAANNOTATIONS 
    $("#Crear #Span_insf_DescInstitucionFinanc").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Contacto").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Contacto").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Telefono").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Telefono").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Correo").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Correo").removeClass("text-danger");

}

//FUNCION: OCULTAR VALIDACIONES DE EDICION
function Vaciar_ModalEditar() {
    //VACIADO DE INPUTS
    $("#Editar #insf_DescInstitucionFinanc").val("");
    $("#Editar #insf_Contacto").val("");
    $("#Editar #insf_Telefono").val("");
    $("#Editar #insf_Correo").val("");

    //
    //OCULTAR DATAANNOTATIONS 
    $("#Editar #Span_insf_DescInstitucionFinanc").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Contacto").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Contacto").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Telefono").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Telefono").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Correo").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Correo").removeClass("text-danger");

}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsCrear(insf_DescInstitucionFinanc, insf_Contacto, insf_Telefono, insf_Correo) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (insf_DescInstitucionFinanc != "-1") {
        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_DescInstitucionFinanc.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_DescInstitucionFinanc.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #insf_DescInstitucionFinanc").val(insf_DescInstitucionFinanc.substring(0, FirstChar + 1));
        }
        if (insf_DescInstitucionFinanc == "" || insf_DescInstitucionFinanc == " " || insf_DescInstitucionFinanc == "  " || insf_DescInstitucionFinanc == null) {
            //ELIMINAR EL ESPACIO EN BLANCO INICIAL
            if (insf_DescInstitucionFinanc == ' ')
                $("#Crear #insf_DescInstitucionFinanc").val("");
            //MOSTRAR DATAANNOTATIONS
            $("#Crear #Span_insf_DescInstitucionFinanc").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #Asterisco_insf_DescInstitucionFinanc").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #Span_insf_DescInstitucionFinanc").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");
        }
    }


    if (insf_Contacto != "-1") {
        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Contacto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Contacto.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #insf_Contacto").val(insf_Contacto.substring(0, FirstChar + 1));
        }
        //CONTACTO
        if (insf_Contacto == "" || insf_Contacto == " " || insf_Contacto == "  " || insf_Contacto == null) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Contacto == ' ')
                $("#Crear #insf_Contacto").val("");

            //MOSTRAR DATAANNOTATIONS
            $("#Crear #Span_insf_Contacto").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #Asterisco_insf_Contacto").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #Span_insf_Contacto").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #Asterisco_insf_Contacto").removeClass("text-danger");
        }
    }


    if (insf_Telefono != "-1") {

        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Telefono.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Telefono.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #insf_DescInstitucionFinanc").val(insf_Telefono.substring(0, FirstChar + 1));
        }

        //Telefono
        if (insf_Telefono == "" || insf_Telefono == " " || insf_Telefono == "  " || isNaN(insf_Telefono)) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Telefono == ' ')
                $("#Crear #insf_Telefono").val("");

            //MOSTRAR DATAANNOTATIONS
            $("#Crear #Span_insf_Telefono").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #Asterisco_insf_Telefono").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #Span_insf_Telefono").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #Asterisco_insf_Telefono").removeClass("text-danger");
        }
    }


    if (insf_Correo != "-1") {

        //FORMATO DE LA MASCARA
        emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;

        //VALIDAR
        if (!emailRegex.test(insf_Correo)) {
            //MOSTRAR LA VALIDACIÓN DE CORREO
            $("#Crear #Span_insf_Correo_Validar").show();
        } else {
            //MOSTRAR LA VALIDACIÓN DE CORREO
            $("#Crear #Span_insf_Correo_Validar").hide();
        }

        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Correo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Correo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #insf_Correo").val(insf_Correo.substring(0, FirstChar + 1));
        }

        //CORREO
        if (insf_Correo == "" || insf_Correo == " " || insf_Correo == "  " || insf_Correo == null) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Telefono == ' ')
                $("#Crear #insf_Correo").val("");

            //MOSTRAR DATAANNOTATIONS
            $("#Crear #Span_insf_Correo").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #Asterisco_insf_Correo").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #Span_insf_Correo").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #Asterisco_insf_Correo").removeClass("text-danger");
        }
    }

    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsEditar(insf_DescInstitucionFinanc, insf_Contacto, insf_Telefono, insf_Correo) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (insf_DescInstitucionFinanc != "-1") {
        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_DescInstitucionFinanc.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_DescInstitucionFinanc.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #insf_DescInstitucionFinanc").val(insf_DescInstitucionFinanc.substring(0, FirstChar + 1));
        }
        if (insf_DescInstitucionFinanc == "" || insf_DescInstitucionFinanc == " " || insf_DescInstitucionFinanc == "  " || insf_DescInstitucionFinanc == null) {
            //ELIMINAR EL ESPACIO EN BLANCO INICIAL
            if (insf_DescInstitucionFinanc == ' ')
                $("#Editar #insf_DescInstitucionFinanc").val("");

            //MOSTRAR DATAANNOTATIONS
            $("#Editar #Span_insf_DescInstitucionFinanc").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #Asterisco_insf_DescInstitucionFinanc").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #Span_insf_DescInstitucionFinanc").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");
        }
    }


    if (insf_Contacto != "-1") {
        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Contacto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Contacto.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #insf_Contacto").val(insf_Contacto.substring(0, FirstChar + 1));
        }
        //CONTACTO
        if (insf_Contacto == "" || insf_Contacto == " " || insf_Contacto == "  " || insf_Contacto == null) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Contacto == ' ')
                $("#Editar #insf_Contacto").val("");

            //MOSTRAR DATAANNOTATIONS
            $("#Editar #Span_insf_Contacto").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #Asterisco_insf_Contacto").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #Span_insf_Contacto").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #Asterisco_insf_Contacto").removeClass("text-danger");
        }
    }


    if (insf_Telefono != "-1") {
        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Telefono.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Telefono.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #insf_DescInstitucionFinanc").val(insf_Telefono.substring(0, FirstChar + 1));
        }

        //Telefono
        if (insf_Telefono == "" || insf_Telefono == " " || insf_Telefono == "  " || isNaN(insf_Telefono)) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Telefono == ' ')
                $("#Editar #insf_Telefono").val("");
            //MOSTRAR DATAANNOTATIONS
            $("#Editar #Span_insf_Telefono").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #Asterisco_insf_Telefono").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #Span_insf_Telefono").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #Asterisco_insf_Telefono").removeClass("text-danger");
        }
    }


    if (insf_Correo != "-1") {

        //FORMATO DE LA MASCARA
        emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;

        //VALIDAR
        if (!emailRegex.test(insf_Correo)) {
            //MOSTRAR LA VALIDACIÓN DE CORREO
            $("#Editar #Span_insf_Correo_Validar").show();
        } else {
            //MOSTRAR LA VALIDACIÓN DE CORREO
            $("#Editar #Span_insf_Correo_Validar").hide();
        }

        //VALIDAR ESPACIOS EN BLANCO
        var LengthString = insf_Correo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = insf_Correo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #insf_Correo").val(insf_Correo.substring(0, FirstChar + 1));
        }

        //CORREO
        if (insf_Correo == "" || insf_Correo == " " || insf_Correo == "  " || insf_Correo == null) {
            //VACIAR EL ESPACIO EN BLANCO INICIAL
            if (insf_Telefono == ' ')
                $("#Editar #insf_Correo").val("");
            //MOSTRAR DATAANNOTATIONS
            $("#Editar #Span_insf_Correo").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #Asterisco_insf_Correo").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #Span_insf_Correo").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #Asterisco_insf_Correo").removeClass("text-danger");
        }
    }

    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}



