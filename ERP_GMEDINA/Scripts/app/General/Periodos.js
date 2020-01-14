var IDInactivar = 0;

const btnGuardar = $('#btnCrearPreavisoConfirmar'),
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
function cargarGridPeriodo() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/Periodos/GetData',
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
            var ListPeriodo = data, template = '';
            //RECORRER DATA OBTENINDA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListPeriodo.length; i++) {

                var FechaCrea = FechaFormato(ListPeriodo[i].peri_FechaCrea);

                var FechaModifica = FechaFormato(ListPeriodo[i].peri_FechaModifica);

                UsuarioModifica = ListPeriodo[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListPeriodo[i].NombreUsuarioModifica;


                //variable para verificar el estado del registro
                var estadoRegistro = ListPeriodo[i].peri_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListPeriodo[i].peri_Activo == true ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetallePeriodo">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListPeriodo[i].peri_Activo == true ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-default btn-xs"  id="btnEditarPeriodo">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListPeriodo[i].peri_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarPeriodos">Activar</button>' : '' : '';



                template += '<tr data-id = "' + ListPeriodo[i].peri_IdPeriodo + '">' +
                    '<td>' + ListPeriodo[i].peri_IdPeriodo + '</td>' +
                    '<td>' + ListPeriodo[i].peri_DescripPeriodo + '</td>' +
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

            $('#tbodyPeriodo').html(template);
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarPeriodo", function () {
    //DESPLEGAR EL MODAL DE PERIODO
    $("#CrearPeriodo").modal();
    //OCULTAR EL DATAANNOTATION
    $("#CrearPeriodo #Crear_Validation_descripcion").css("display", "none");
    //OCULTAR EL SCROLLVIEW
    $('body').css("overflow", "hidden");
});

//FUNCION: CREAR UN NUEVO REGISTRO

$('#btnCrearPeriodoConfirmar').click(function () {
    var DescripPerio = $("#Crear #peri_DescripPeriodo").val();
    console.log(DescripPerio);
    if (!DescripPerio == ''){
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
         var data = $("#frmCreatePeriodo").serializeArray();
         console.log(data);
         $.ajax({
             url: "/Periodos/Create",
             method: "POST",
             data: data
          }).done(function (data) {
             $("#CrearPeriodo").modal('hide');
             //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
             if (data == "error") {
                 iziToast.error({
                     title: 'Error',
                     message: '¡No se guardó el registro, contacte al administrador!',
                 });
             }
             else if (data == "bien") {
                    cargarGridPeriodo();
                    console.log(data);
                    // Mensaje de exito cuando un registro se ha guardado bien
                  iziToast.success({
                        title: 'Exito',
                        message: '¡El registro se agregó de forma exitosa!',
                    });
              }
            });
    } else {
        console.log("validar")
        //MOSTRAR EL DATAANNOTATION
        $("#CrearPeriodo #Crear_Validation_descripcion").css("display", "block");
        //OCULTAR EL SCROLLVIEW
        $('body').css("overflow", "hidden");
    }
});




//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblPeriodo tbody tr td #btnEditarPeriodo", function () {
    //OCULTAR EL DATAANNOTATIONS
    $("#frmEditPeriodo #Edit_Validation_descripcion").css("display", "none");
    //CAPTURAR EL ID DEL REGISTRO
    var ID = $(this).data('id');
    //SETEAR LA VARIABLE INACTIVAR CON EL ID DEL REGISTRO
    IDInactivar = ID;


        //EJECUCION DE LA PETICION AL SERVIDOR
	    $.ajax({
	        url: "/Periodos/Edit/" + ID,
	        method: "POST",
	        dataType: "json",
	        contentType: "application/json; charset=utf-8",
	        data: JSON.stringify({ ID: ID })
	    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {
                    $("#Editar #peri_IdPeriodo").val(iter.peri_IdPeriodo);
                    $("#Editar #peri_DescripPeriodo").val(iter.peri_DescripPeriodo);
                });
                $("#EditarPeriodo").modal();
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



$("#btnUpdatePeriodo").click(function () {
    if ($("#Crear #peri_DescripPeriodo").val() != '') {
        $("#ConfirmarEdicion").modal();
    }
});

$("#btnCerrarConfirmarEditar").click(function () {
    $("#ConfirmarEdicion").modal('hide');
});

//GUARADAR LA EDICION DEL REGISTRO
$(document).on("click", "#btnConfirmarEditar", function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON

 //   $("#EditarPeriodo #Validation_descripcion").css("display", "block");

    var data = $("#frmEditPeriodo").serializeArray();
    var descedit = $("#Editar #peri_DescripPeriodo").val();

    if ($("#Editar #peri_DescripPeriodo").val() != "") {

        $.ajax({
            url: "/Periodos/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != "error") {
                cargarGridPeriodo();
                $("#EditarPeriodo").modal('hide');
                $("#ConfirmarEdicion").modal('hide');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            } else {
                $("#ConfirmarEdicion").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
    }
    else {
        //MOSTRAR DATAANNOTATION
        $("#frmEditPeriodo #Edit_Validation_descripcion").css("display", "block");
        //OCULTAR EL SCROLLVIEW
        $('body').css("overflow", "hidden");
    }
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
    //OCULTAR MODAL DE EDICION
    $("#EditarPeriodo").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarPeriodo").modal();
});

//OCULTAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR MODAL DE EDICION
    $("#EditarPeriodo").modal();
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarPeriodo").modal('hide');
});

//CONFIRMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPeriodoConfirmar").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $("#EditarPeriodo").modal('hide');
    $.ajax({
        url: "/Periodos/Inactivar/" + IDInactivar,
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
//DESPLEGAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnActivarPeriodos", function () {
    ActivarID = $(this).data('id');
    $("#ActivarPeriodo").modal();
});

//CONFORMAR ACTIVACION DEL REGISTRO
$("#btnActivarPeriodoConfirm").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Periodos/Activar/" + ActivarID,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
            $("#ActivarPeriodo").modal('hide');
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


//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
    $("#EditarPeriodo #Validation_descripcion").css("display", "none");
    $("#CrearPeriodo").modal("hide");
});

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