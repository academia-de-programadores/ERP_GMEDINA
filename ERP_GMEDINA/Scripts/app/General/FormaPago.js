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

                template += '<tr data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '">' +
                    '<td>' + ListaFormaPago[i].fpa_Descripcion + '</td>' +
                    '<td>' + ListaFormaPago[i].NombreUsuarioCrea + '</td>' +
                    '<td>' + FechaCrea + '</td>' +
                    '<td>' + UsuarioModifica + '</td>' +
                    '<td>' + FechaModifica + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-primary btn-xs" id="btnEditarFormaPago">Editar</button>' +
                    //'<button data-id = "' + ListaFormaPago[i].fpa_IdFormaPago + '" type="button" class="btn btn-default btn-xs" id="btnDetalleFormaPago">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyFormaPago').html(template);
        });
    FullBody();
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
            else {
                $("#CrearFormaPago").modal('hide');
                cargarGridFormaPago();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡Se registró de forma exitosa!',
                });
            }
        });
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
                message: 'No se pudo inactivar el registro, contacte al administrador',
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
                message: 'El registro fue Inactivado de forma exitosa!',
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