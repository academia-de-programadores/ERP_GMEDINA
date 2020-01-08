var IDInactivar = 0;
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
function cargarGridPreaviso() {
    _ajax(null,
        '/Preaviso/GetData',
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
            var ListaPreaviso = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaPreaviso.length; i++) {
                var FechaCrea = FechaFormato(ListaPreaviso[i].prea_FechaCrea);

                var FechaModifica = FechaFormato(ListaPreaviso[i].prea_FechaModifica);

                UsuarioModifica = ListaPreaviso[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListaPreaviso[i].NombreUsuarioModifica;

                template += '<tr data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '">' +
                    '<td>' + ListaPreaviso[i].prea_RangoInicioMeses + '</td>' +
                    '<td>' + ListaPreaviso[i].prea_RangoFinMeses + '</td>' +
                    '<td>' + ListaPreaviso[i].prea_DiasPreaviso + '</td>' +
                    '<td>' + FechaCrea + '</td>' +
                    '<td>' + ListaPreaviso[i].NombreUsuarioCrea + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-primary btn-xs" id="btnDetallePreaviso">Detalle</button>' +
                    '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-default btn-xs" id="btnEditarPreaviso">Editar</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyPreaviso').html(template);
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarPreaviso", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#CrearPreaviso #prea_RangoInicioMeses").val('');
    $("#CrearPreaviso #prea_RangoFinMeses").val('');
    $("#CrearPreaviso #prea_DiasPreaviso").val('');
    $("#CrearPreaviso").modal();
    $("#CrearPreaviso #Validation_descripcion").css("display", "none");
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearPreavisoConfirmar').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    $("#CrearPreaviso #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreatePreaviso").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#CrearPreaviso #Crear #prea_RangoInicioMeses").val() != "") {
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/Preaviso/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                $("#CrearPreaviso").modal('hide');
                cargarGridPreaviso();
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
$(document).on("click", "#tblPreaviso tbody tr td #btnEditarPreaviso", function () {
	var ID = $(this).data('id');
	IDInactivar = ID;
	$.ajax({
		url: "/Preaviso/Edit/" + ID,
		method: "POST",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify({ ID: ID })
	})
        .done(function (data) {
        	//SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
        	if (data) {
        		debugger;
        		$.each(data, function (i, iter) {
        		    $("#Editar #prea_IdPreaviso").val(iter.prea_IdPreaviso);
        		    $("#Editar #prea_RangoInicioMeses").val(iter.prea_RangoInicioMeses);
        		    $("#Editar #prea_RangoFinMeses").val(iter.prea_RangoFinMeses);
        		    $("#Editar #prea_DiasPreaviso").val(iter.prea_DiasPreaviso);
        		});
        		$("#EditarPreaviso").modal();
        	}
        });
});

//GUARADAR LA EDICION DEL REGISTRO
$(document).on("click", "#btnUpdatePreaviso", function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON

    $("#CrearPreaviso #Validation_descripcion").css("display", "block");

    var data = $("#frmEditPreaviso").serializeArray();
    console.log(data);
    
    if ($("#EditarPreaviso #Editar #prea_RangoInicioMeses").val() != "" || $("#EditarPreaviso #Editar #prea_RangoFinMeses").val() != "" || $("#EditarPreaviso #Editar #prea_DiasPreaviso").val() != "") {
        $.ajax({
            url: "/Preaviso/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != "error") {
                cargarGridPreaviso();
                $("#EditarPreaviso").modal('hide');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡Se editó de forma exitosa!',
                });
            }

        });
    }
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarPreaviso", function () {
    $("#InactivarPreaviso").modal();
});

//CONFORMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPreavisoConfirmar").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Preaviso/Inactivar/" + IDInactivar,
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
            cargarGridPreaviso();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarPreaviso").modal('hide');
            $("#EditarPreaviso").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPreaviso tbody tr td #btnDetallePreaviso", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/Preaviso/Details/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {

                    var FechaCrea = FechaFormato(data[0].prea_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].prea_FechaModifica);
                    $("#Detalles #prea_IdPreaviso").val(iter.prea_IdPreaviso);
                    $("#Detalles #prea_RangoInicioMeses").val(iter.prea_RangoInicioMeses);
                    $("#Detalles #prea_RangoFinMeses").val(iter.prea_RangoFinMeses);
                    $("#Detalles #prea_DiasPreaviso").val(iter.prea_DiasPreaviso);
                    data[0].prea_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                    $("#Detalles #prea_UsuarioCrea").val(iter.prea_UsuarioCrea);
                    $("#Detalles #prea_FechaCrea").val(FechaCrea);
                    data[0].prea_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                    $("#Detalles #prea_UsuarioModifica").val(data[0].prea_UsuarioModifica);
                    $("#Detalles #prea_FechaModifica").val(FechaModifica);
                });
                $("#DetallarPreaviso").modal();
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

//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#CrearPreaviso").modal("hide");
});

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearPreaviso #Validation_descripcion").css("display", "none");
    $("#CrearPreaviso").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreatePreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditPreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarPreaviso", function () {
    $("#DetallarPreaviso").modal('hide');
    $("#InactivarPreaviso").modal();
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarPreaviso").modal('hide');
});
