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
function cargarGrid() {
    _ajax(null,
        '/TipoAmonestaciones/GetData',
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
            var template;
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < data.length; i++) {

                template += '<tr data-id = "' + data[i].tamo_Id + '">' +
                    '<td>' + data[i].tamo_Descripcionn + '</td>' +
                    '<td>' +
                    '<button type="button" data-id = "' + data[i].tamo_Id + '" class="btn btn-primary btn-xs" id="btnEditar">Editar</button>' +
                    '<button type="button" data-id = "' + data[i].tamo_Id + '" class="btn btn-default btn-xs" id="btnDetalle">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbody').html(template);
        });
}

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#table tbody tr td #btnEditar", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/TipoAmonestaciones/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #tamo_Id").val(data.tamo_Id);
                $("#Editar #tamo_Descripcion").val(data.tamo_Descripcion);
                $("#ModalEditar").modal();
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

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdate").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEdit").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TipoAmonestaciones/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGrid();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ModalEditar").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });
        }
    });
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregar", function () {

    //MOSTRAR EL MODAL DE AGREGAR
    $("#ModalAgregar").modal();
});

///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#table tbody tr td #btnDetalle", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/TipoAmonestaciones/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data.tamo_FechaCrea);
                var FechaModifica = FechaFormato(data.tamo_FechaModifica);
                $("#Detalles #tamo_Id").val(data.tamo_Id);
                $("#Detalles #tamo_Descripcion").val(data.tamo_Descripcion);
                $("#Detalles #tamo_Estado").val(data.tamo_Estado);
                $("#Detalles #tamo_RazonInactivo").val(data.tamo_RazonInactivo);
                $("#Detalles #tamo_UsuarioCrea").val(data.tamo_UsuarioCrea);
                $("#Detalles #tamo_FechaCrea").val(FechaCrea);
                $("#Detalles #tamo_UsuarioModifica").val(data.tamo_UsuarioModifica);
                $("#Detalles #tamo_FechaModifica").val(FechaModifica);


                $("#ModalDetalles").modal();
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
///////////////////////////////////////////////////////////////////////////////////////////////////

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistro').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreate").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/TipoAmonestaciones/Create",
        method: "POST",
        data: data
    }).done(function (data) {
        //CERRAR EL MODAL DE AGREGAR
        $("#ModalAgregar").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo guardar el registro, contacte al administrador',
            });
        }
        else {
            cargarGrid();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#ModalEditar").modal('hide');
});

$(document).on("click", "#btnmodalInactivar", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#ModalInactivar").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistro").click(function () {

    var data = $("#frmInactivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TipoAmonestaciones/Inactivar",
        method: "POST",
        data: data
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
            cargarGrid();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ModalInactivar").modal('hide');
            $("#ModalEditar").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
});