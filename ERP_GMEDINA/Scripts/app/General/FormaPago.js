var IDInactivar = 0;
//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
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
            var ListaFormaPago = data, template = '';
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


                console.log(estadoRegistro);

                template += '<tr data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '">' +
                    '<td>' + ListaFormaPago[i].fpa_IdFormaPago + '</td>' +
                    '<td>' + ListaFormaPago[i].fpa_Descripcion + '</td>' +
                    '<td>' + estadoRegistro + '</td>' +
                    '<td>' +
                    botonDetalles +
                    botonEditar +
                    botonActivar
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            FullBody();
            $('#tbodyFormaPago').html(template);
        });
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarFormaPago", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #fpa_Descripcion").val('');
    $("#CrearFormaPago").modal();
    $("#CrearFormaPago #Validation_descripcion").css("display", "none");
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearFormaPago').click(function () {
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateFormaPago").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#CrearFormaPago #Crear #fpa_Descripcion").val() != "") {
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
            }
            else {
                $("#Crear #fpa_Descripcion").val();
                $("#CrearFormaPago").modal('hide');
                cargarGridFormaPago();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
    else {
        // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        $("#CrearFormaPago #Validation_descripcion").css("display", "block");
    }
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblFormaPago tbody tr td #btnEditarFormaPago", function () {
    //CAPTURAR EL ID DEL REGISTRO SELECCIONADO
    var ID = $(this).data('id');
    //SETEAR LA VARIABLE GLOBAL DE INACTIVACION
    IDInactivar = ID;
    //OCULTAR EL DATAANNOTATIONS
    $("#frmEditFormaPago #Edit_Validation_descripcion").css("display", "none");
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
                $("#EditarFormaPago").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});

$("#btnUpdateFormaPago").click(function () {
    var Descripcion = $("#Editar #fpa_Descripcion").val();
    if (Descripcion != '' && Descripcion != null && Descripcion != undefined && isNaN(Descripcion) == true) {
        $("#ConfirmarEdicion").modal();
    }
    else {
       
        $("#Editar #Edit_Validation_descripcion").css("display", "");
        $("#ConfirmarEdicion").modal('hide');
    }
   
});

//GUARADR LA EDICION DEL REGISTRO
$("#btnConfirmarEditar2").click(function () {
    //VALIDAR QUE EL CAMPO NO ESTE VACIO
    if ($("#Editar #fpa_Descripcion").val() != "") {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditFormaPago").serializeArray();
        console.log(data);
        $.ajax({
            url: "/FormaPago/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                cargarGridFormaPago();
                $("#EditarFormaPago").modal('hide');
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
            else {
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
        $("#frmEditFormaPago #Edit_Validation_descripcion").css("display", "block");
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
                console.log(data);
                console.log(data[0].fpa_Descripcion);
                var FechaCrea = FechaFormato(data[0].fpa_FechaCrea);
                var FechaModifica = FechaFormato(data[0].fpa_FechaModifica);
                $("#frmDetailFormaPago #fpa_Descripcion").html(data[0].fpa_Descripcion);
                $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#fpa_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#fpa_UsuarioModifica").val(data[0].fpa_UsuarioModifica);
                $("#fpa_FechaModifica").html(FechaModifica);
                $("#frmDetailFormaPago").modal();

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

//
//INACTIVAR

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarFormaPago", function () {
    //OCULTAR MODAL DE EDICION
    $("#EditarFormaPago").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarFormaPago").modal();
});

//OCULTAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarFormaPago").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarFormaPago").modal();
});

//CONFORMAR INACTIVACION DEL REGISTRO
$("#btnInactivarFormaPagoConfirm").click(function () {
    //SE OCULTA EL MODAL DE EDICION
    $("#EditarFormaPago").modal('hide');
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
//DESPLEGAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnActivarFormaPago", function () {
    ActivarID = $(this).data('id');
    $("#ActivarFormaPago").modal();
});

//CONFORMAR ACTIVACION DEL REGISTRO
$("#btnActivarFormaPagoConfirm").click(function () {
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
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    ActivarID = 0
});

//BOTONES


//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
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
    $("#InactivarFormaPago").modal();
});

//BOTON PARA CERRAR EL MODAL DE INACTIVAR
$("#btnCerrarInactivar").click(function () {
    $("#InactivarFormaPago").modal('hide');
});



//******************ACTIVAR*******************//

//MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnmodalActivarFormaPago", function () {
    $("#InactivarFormaPago").modal();
});

//BOTON PARA CERRAR EL MODAL DE ACTIVAR
$("#btnCerrarActivar").click(function () {
    $("#ActivarFormaPago").modal('hide');
});
