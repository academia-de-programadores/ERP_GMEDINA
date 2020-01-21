var IDInactivar = 0;
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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridFormaPago() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/FormaPago/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaFormaPago = data;
            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblFormaPago').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaFormaPago.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaFormaPago[i].fpa_Activo == false ? 'Inactivo' : 'Activo';
                //variable boton detalles
                var botonDetalles = ListaFormaPago[i].fpa_Activo == true ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetallesFormaPago">Detalles</button>' : '';
                //variable boton editar
                var botonEditar = ListaFormaPago[i].fpa_Activo == true ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-default btn-xs" id="btnEditarFormaPago">Editar</button>' : '';
                //variable donde está el boton activar
                var botonActivar = ListaFormaPago[i].fpa_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarFormaPago">Activar</button>' : '' : '';
                //AGREGAR FILA AL DATATABLE POR ITERACIÓN DEL CICLO
                $('#tblFormaPago').dataTable().fnAddData([
                     ListaFormaPago[i].fpa_IdFormaPago,
                     ListaFormaPago[i].fpa_Descripcion,
                     estadoRegistro,
                     botonDetalles + botonEditar + botonActivar]
                 );
            }
            //APLICAR EL MAX-WIDTH DEL BODY
            FullBody();
        });
}



function DataAnnotations(ToF) {
    if (ToF) {
        //TRUE PARA OCULTAR DATAANNOTATIONS
        $("#Editar #Edit_Validation_descripcion").css("display", "none");
        $("#Crear #Validation_descripcion").css("display", "none");
    }
    else {
        //FALSE PARA MOSTRAR DATAANNOTATIONS
        $("#Editar #Edit_Validation_descripcion").css("display", "block");
        $("#Crear #Validation_descripcion").css("display", "block");
    }
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarFormaPago", function () {
    document.getElementById("btnCrearFormaPago").disabled = false;
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #fpa_Descripcion").val('');
    $("#CrearFormaPago").modal({ backdrop: 'static', keyboard: false });
    //CAMBIAR EL ASTERISCO A COLOR NEGRO
    $("#AsteriscoFormaPago").removeClass("text-danger");
    //OCULTAR DATAANNOTATIONS
    DataAnnotations(true);
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearFormaPago').click(function () {
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateFormaPago").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#CrearFormaPago #Crear #fpa_Descripcion").val() != "") {
        //BLOQUEAR EL BOTON
        $("#btnCrearFormaPago").attr("disabled", true);
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/FormaPago/Create",
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
                $("#btnCrearFormaPago").attr("disabled", false);
            }
            else {
                $("#Crear #fpa_Descripcion").val();
                $("#CrearFormaPago").modal('hide');
                cargarGridFormaPago();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
        $("#AsteriscoFormaPago").removeClass("text-danger");
    }
    else {
        //SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        DataAnnotations(false);
        //CAMBIAR A COLOR ROJO EL ASTERISCO
        $("#AsteriscoFormaPago").addClass("text-danger");
    }
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblFormaPago tbody tr td #btnEditarFormaPago", function () {
    document.getElementById("btnConfirmarEditar2").disabled = false;
    //CAPTURAR EL ID DEL REGISTRO SELECCIONADO
    var ID = $(this).data('id');
    //SETEAR LA VARIABLE GLOBAL DE INACTIVACION
    IDInactivar = ID;
    //OCULTAR EL DATAANNOTATIONS
    DataAnnotations(true);
    //PONER EL ASTERISCO DE VALIDACION EN NEGRO
    $("#AsteriscoFormaPagoEditar").removeClass("text-danger");
    $.ajax({
        url: "/FormaPago/Edit/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                //debugger;
                $.each(data, function (i, iter) {
                    $("#Editar #fpa_IdFormaPago").val(iter.fpa_IdFormaPago);
                    $("#Editar #fpa_Descripcion").val(iter.fpa_Descripcion);
                });
                $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
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





$("#btnUpdateFormaPago").click(function () {
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

//MOSTRAR EL MODAL DE EDICION AL MOMENTO DE CERRAR EL MODAL CON EL BOTON CERRAR
$("#InactivarFormaPago #IconCerrar").click(function () {
    document.getElementById("btnInactivarFormaPagoConfirm").disabled = false;
    $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//GUARADR LA EDICION DEL REGISTRO
$("#btnConfirmarEditar2").click(function () {
    document.getElementById("btnConfirmarEditar2").disabled = true;
    //VALIDAR QUE EL CAMPO NO ESTE VACIO
    DataAnnotations(false);
    if ($("#Editar #fpa_Descripcion").val() != "") {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditFormaPago").serializeArray();
        $.ajax({
            url: "/FormaPago/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != 'error') {
                cargarGridFormaPago();
                $("#ConfirmarEdicion").modal('hide');
                //$("#EditarFormaPago").modal('hide');
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
            else {
                $("#ConfirmarEdicion").modal('hide');
                $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
    }
    else {
        //MOSTRAR DATAANNOTATION
        DataAnnotations(false);
        //OCULTAR EL SCROLLVIEW
    }
});

$(document).on("click", "#tblFormaPago tbody tr td #btnDetallesFormaPago", function () {
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


//CERRAR MODAL DE CONFIRMACIÓN DE EDICION
$(document).on("click", "#btnCerrarConfirmarEditar", function () {
    document.getElementById("btnConfirmarEditar2").disabled = false;
    //OCULTAR MODAL DE CONFIRMACIÓN DE EDICION
    $("#ConfirmarEdicion").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//
//INACTIVAR

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarFormaPago", function () {
    document.getElementById("btnInactivarFormaPagoConfirm").disabled = false;
    //OCULTAR MODAL DE EDICION
    $("#EditarFormaPago").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarFormaPago").modal({ backdrop: 'static', keyboard: false });
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
    document.getElementById("btnActivarFormaPagoConfirm").disabled = false;
    ActivarID = $(this).data('id');
    $("#ActivarFormaPago").modal({ backdrop: 'static', keyboard: false });
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


//FUNCIONES PARA EL ASTERISCO DE VALIDACION

$("#AsteriscoFormaPago").addClass("text-danger");
$("#AsteriscoFormaPago").removeClass("text-danger");