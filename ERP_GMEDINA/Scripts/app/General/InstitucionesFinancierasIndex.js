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
function cargarGridINFS()
{
//    var esAdministrador = $("#rol_Usuario").val();
//    cons.log("Hola: " +esAdministrador);
    _ajax(null,
        '/InstitucionesFinancieras/GetData',
        'GET',
        (data) => {
            if (data.length == 0)
            {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaINFS = data;            
            //LIMPIAR LA DATA DEL DATATABLE
            $('#IndexTable').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaINFS.length; i++)
            {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaINFS[i].insf_Activo == false ? 'Inactivo' : 'Activo';
                //variable boton detalles
                var botonDetalles = ListaINFS[i].insf_Activo == true ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnModalDetallesINFS">Detalles</button>' : '';
                //variable boton editar
                var botonEditar = ListaINFS[i].insf_Activo == true ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" class="btn btn-primary btn-xs" id="btnModalEditarINFS">Detalles</button>' : '';
                //variable donde está el boton activar
                var botonActivar = ListaINFS[i].insf_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaINFS[i].insf_IdInstitucionFinanciera + '" type="button" class="btn btn-primary btn-xs"  id="btnModalActivarINFS">Activar</button>' : '' : '';

                $('#IndexTable').dataTable().fnAddData([
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

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function Vaciar_ModalCrear() {
    //VACIADO DE INPUTS
    $("#Crear #insf_DescInstitucionFinanc").val("");
    $("#Crear #insf_Contacto").val("");
    $("#Crear #insf_Telefono").val("");

    //
    //OCULTAR DATAANNOTATIONS 
    $("#Crear #Span_insf_DescInstitucionFinanc").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Contacto").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Contacto").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Telefono").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Telefono").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #Span_insf_Correo").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #Asterisco_insf_Correo").removeClass("text-danger");

    console.log("Vaciado");
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function Vaciar_ModalEditar() {
    //VACIADO DE INPUTS
    $("#Editar #insf_DescInstitucionFinanc").val("");
    $("#Editar #insf_Contacto").val("");
    $("#Editar #insf_Telefono").val("");
    $("#Editar #insf_Correo").val("");

    //
    //OCULTAR DATAANNOTATIONS 
    $("#Editar #Span_insf_DescInstitucionFinanc").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Contacto").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Contacto").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Telefono").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Telefono").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Span_insf_Correo").css("display", "none");
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #Asterisco_insf_Correo").removeClass("text-danger");

    console.log("Vaciado");
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsCrear() {
    //CAPTURA DE INPUTS
    var insf_DescInstitucionFinanc = $("#Crear #insf_DescInstitucionFinanc").val();
    var insf_Contacto = $("#Crear #insf_Contacto").val();
    var insf_Telefono = $("#Crear #insf_Telefono").val();
    var insf_Correo = $("#Crear #insf_Correo").val();
    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    //DESCRIPCION
    if (insf_DescInstitucionFinanc == "" || insf_DescInstitucionFinanc == null) {
        //MOSTRAR DATAANNOTATIONS
        $("#Crear #Span_insf_DescInstitucionFinanc").css("display", "block");
        //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
        $("#Crear #Asterisco_insf_DescInstitucionFinanc").addClass("text-danger");
        ModelState = false;
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #Span_insf_DescInstitucionFinanc").css("display", "none");
        //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
        $("#Crear #Asterisco_insf_DescInstitucionFinanc").removeClass("text-danger");
    }


    //CONTACTO
    if (insf_Contacto == "" || insf_Contacto == null) {
        //MOSTRAR DATAANNOTATIONS
        $("#Crear #Span_insf_Contacto").css("display", "block");
        //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
        $("#Crear #Asterisco_insf_Contacto").addClass("text-danger");
        ModelState = false;
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #Span_insf_Contacto").css("display", "none");
        //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
        $("#Crear #Asterisco_insf_Contacto").removeClass("text-danger");
    }


    //Telefono
    if (insf_Telefono == "" || isNaN(insf_Telefono)) {
        //MOSTRAR DATAANNOTATIONS
        $("#Crear #Span_insf_Telefono").css("display", "block");
        //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
        $("#Crear #Asterisco_insf_Telefono").addClass("text-danger");
        ModelState = false;
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #Span_insf_Telefono").css("display", "none");
        //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
        $("#Crear #Asterisco_insf_Telefono").removeClass("text-danger");
    }

    //CORREO
    if (insf_Correo == "" || insf_Correo == null) {
        //MOSTRAR DATAANNOTATIONS
        $("#Crear #Span_insf_Correo").css("display", "block");
        //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
        $("#Crear #Asterisco_insf_Correo").addClass("text-danger");
        ModelState = false;
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #Span_insf_Correo").css("display", "none");
        //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
        $("#Crear #Asterisco_insf_Correo").removeClass("text-danger");
    }


    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}


//FUNCION KEYUP
$('#Crear #aisr_Descripcion').keyup(function () {
    //Vaciar_ModalCrear
    //Vaciar_ModalCrear();
});


//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarInstitucion", function () {
    //DESBLOQUEAR EL BOTON DE CREACION
    $("#btnCrearInstitucion").attr("disabled", false);
    //VACIAR LOS CAMPOS DEL MODAL
    Vaciar_ModalCrear();
    //MOSTRAR EL MODAL DE AGREGAR
    $("#CrearInstitucion").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearInstitucion').click(function () {
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateInstitucionFinanciera").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if (DataAnnotationsCrear()) {
        console.log("ENTRA");
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


//VARIABLE DE INACTIVACION
var IDInactivar = 0;
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#IndexTable tbody tr td #btnModalEditarINFS", function () {
    //DESBLOQUEAR EL BOTON
    $("#btnConfirmarEditar2").attr("disabled", false);
    //CAPTURAR EL ID DEL REGISTRO SELECCIONADO
    var ID = $(this).data('id');
    console.log(ID);
    //SETEAR LA VARIABLE GLOBAL DE INACTIVACION
    IDInactivar = ID;
    //OCULTAR EL DATAANNOTATIONS
    Vaciar_ModalEditar();
    $.ajax({
        url: "/InstitucionesFinancieras/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log(data);
                $.each(data, function (i, iter) {
                    $("#Editar #insf_DescInstitucionFinanc").val(data.insf_DescInstitucionFinanc);
                    $("#Editar #insf_Contacto").val(data.insf_Contacto);
                    $("#Editar #insf_Telefono").val(data.insf_Telefono);
                    $("#Editar #insf_Correo").val(data.insf_Correo);
                });
                //DESPLEGAR EL MODAL DE EDICION
                $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
                console.log("ENTRA");
            }
            else {
                console.log("NO ENTRA");
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
    document.getElementById("btnConfirmarEditar2").disabled = false;
    var Descripcion = $("#Editar #fpa_Descripcion").val();
    if (Descripcion != '' && Descripcion != null && Descripcion != undefined && isNaN(Descripcion) == true) {
        $("#EditarFormaPago").modal('hide');
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
    }
    else {
        //MOSTRAR EL DATAANNOTATIONS
        DataAnnotations(false);
        //CAMBIAR EL COLOR DEL ASTERISCO DE VALIDACION
        $("#AsteriscoFormaPagoEditar").addClass("text-danger");
    }

});


//GUARDAR LA EDICION DEL REGISTRO
$("#btnConfirmarEditar2").click(function () {
    //VALIDAR QUE EL CAMPO NO ESTE VACIO
    DataAnnotations(false);
    if ($("#Editar #fpa_Descripcion").val() != "") {
        //BLOQUEAR EL BOTON
        $("#btnConfirmarEditar2").attr("disabled", true);
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmCreateInstitucionFinanciera").serializeArray();
        $.ajax({
            url: "/InstitucionesFinancierasController/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != 'error') {
                //REFRESCAR LA TABLA 
                cargarGridINFS();
                $("#ConfirmarEdicion").modal('hide');
                //$("#EditarFormaPago").modal('hide');
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
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
    }
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


//CERRAR MODAL DE EDICION CON EL BOTON CERRAR
$("#InactivarFormaPagoCerrar").click(function () {
    //OCULTAR MODAL
    $("#frmInactivarINFS").modal("hide");
    //MOSTRAR MODAL
    $("#EditarInstitucion").modal({ backdrop: 'static', keyboard: false });
});


//DETALLES
$(document).on("click", "#IndexTable tbody tr td #btnModalDetallesINFS", function () {
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
                $("#frmDetailFormaPago #fpa_Descripcion").html(data[0].fpa_Descripcion);
                $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#fpa_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#fpa_UsuarioModifica").val(data[0].fpa_UsuarioModifica);
                $("#fpa_FechaModifica").html(FechaModifica);
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
});



// INACTIVAR 
$(document).on("click", "#btnModalInactivarINFS", function ()
{
    //DESPLEGAR EL MODAL DE CONFIRMACION DE INACTIVACION
    $("#frmInactivarINFS").modal();
});

$("#btnInactivarINFS").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA INACTIVACIóN
    $.ajax({
        url: "/InstitucionesFinancieras/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            $("#frmInactivarINFS").modal('hide');
            cargarGridINFS();
            //Mensaje de exito de la inactivación
            iziToast.success({
                title: 'Exito',
                message: 'El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});


// Activar
var activarID = 0;
$(document).on("click", "#btnModalActivarINFS", function () {
    activarID = $(this).data('id');
    $("#frmActivarINFS").modal();
});

//activar ejecutar
$("#btnActivarINFS").click(function ()
{
    $.ajax({
        url: "/InstitucionesFinancieras/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data)
    {
        if (data == "error")
        {
            iziToast.error({
                title: 'Error',
                message: 'No se logró activar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridINFS();
            $("#frmActivarINFS").modal('hide');
            //Mensaje de exito de la activación
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se Activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});


//DESHABILITAR EL POSTBACK DEL CREATE
$("#frmCreateInstitucionFinanciera").submit(function (e) {
    e.preventDefault();
});
