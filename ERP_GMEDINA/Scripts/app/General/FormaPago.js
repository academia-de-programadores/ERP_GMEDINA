var IDInactivar = 0;

const btnGuardar = $('#btnCrearFormaPago'),
cargandoCrearcargandoCrear = $('#cargandoCrear'),
cargandoCrear = $('#cargandoCrear') //Div que aparecera cuando se le de click en crear
//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
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
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaFormaPago = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaFormaPago.length; i++) {

                var FechaCrea = FechaFormato(ListaFormaPago[i].fpa_FechaCrea);

                var FechaModifica = FechaFormato(ListaFormaPago[i].fpa_FechaModifica);

                UsuarioModifica = ListaFormaPago[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListaFormaPago[i].NombreUsuarioModifica;



                //variable para verificar el estado del registro
                var estadoRegistro = ListaFormaPago[i].fpa_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaFormaPago[i].fpa_Activo == true ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleFormaPago">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaFormaPago[i].fpa_Activo == true ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-default btn-xs"  id="btnEditarFormaPago">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaFormaPago[i].fpa_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarFormaPago">Activar</button>' : '' : '';



                template += '<tr data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '">' +
                    '<td>' + ListaFormaPago[i].fpa_IdFormaPago + '</td>' +
                    '<td>' + ListaFormaPago[i].fpa_Descripcion + '</td>' +
                    //variable del estado del registro creada en el operador ternario de arriba
                    '<td>' + estadoRegistro + '</td>' +

                    //variable donde está el boton de detalles
                    '<td>' + botonDetalles +

                    //variable donde está el boton de detalles
                     botonEditar +

                    //boton activar 
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
    $("#CrearFormaPago #fpa_Descripcion").val('');
    $("#CrearFormaPago").modal();
    $("#CrearFormaPago #Validation_descripcion").css("display", "none");
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearFormaPago').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    $("#CrearFormaPago #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateFormaPago").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#CrearFormaPago #Crear #fpa_Descripcion").val() != "") {
        mostrarCargandoCrear();
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
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                $("#CrearFormaPago").modal('hide');
                cargarGridFormaPago();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡Se registró de forma exitosa!',
                });
                ocultarCargandoCrear();
            }
        });
    }
    else {
        ocultarCargandoCrear();
    }
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblFormaPago tbody tr td #btnEditarFormaPago", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
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

//GUARADR LA EDICION DEL REGISTRO
$(document).on("click", "#btnUpdateFormaPago", function () {
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
	            message: '¡Se editó de forma exitosa!',
	        });
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

//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblFormaPago tbody tr td #btnDetalleFormaPago", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/FormaPago/Details/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {
                    debugger;
                    var FechaCrea = FechaFormato(data[0].fpa_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].fpa_FechaModifica);
                    $("#Detalles #peri_IdPeriodo").html(iter.fpa_IdFormaPago);
                    $("#Detalles #peri_DescripPeriodo").html(iter.fpa_Descripcion);
                    data[0].fpa_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #fpa_UsuarioCrea").html(iter.fpa_UsuarioCrea);
                    $("#Detalles #fpa_FechaCrea").html(FechaCrea);
                    data[0].fpa_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #fpa_UsuarioModifica").html(data[0].fpa_UsuarioModifica);
                    $("#Detalles #fpa_FechaModifica").html(FechaModifica);
                });
                $("#DetallarFormaPago").modal();
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarFormaPago", function () {
    $("#EditarFormaPago").modal('hide');
    $("#InactivarFormaPago").modal();
});

//CONFORMAR INACTIVACION DEL REGISTRO
$("#btnInactivarFormaPagoConfirm").click(function () {
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
                message: 'No se pudo Inhabilitar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridFormaPago();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarFormaPago").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inhabiltado de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});


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
    $("#DetallesFormaPago").modal('hide');
    $("#InactivarFormaPago").modal();
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarFormaPago").modal('hide');
});



//FUNCION: PRIMERA FASE DE ACTIVAR

$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnActivar", function () {
    //FUNCION: MOSTRAR EL MODAL DE ACTIVAR
    IDActivar = $(this).data('id');
    $("#ActivarCatalogoIngresos").modal();
});


//EJECUTAR LA ACTIVACION DEL REGISTRO
$("#btnActivarIngreso").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo Activar el registro, contacte al administrador',
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
                message: '¡El registro fue Activado de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
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


// Activar
var activarID = 0;
$(document).on("click", "#btnActivarFormaPago", function () {
    activarID = $(this).data('id');
    $("#frmActivarForP").modal();
});

//activar ejecutar
$("#btnActivarForP").click(function () {

    $.ajax({
        url: "/FormaPago/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se logró Activar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridFormaPago();
            $("#frmActivarForP").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se Activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});