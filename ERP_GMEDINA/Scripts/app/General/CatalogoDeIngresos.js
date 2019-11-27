//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });
// REGION DE VARIABLES
var InactivarID = 0;


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

// REFRESCAR INFORMACIÓN DE LA TABLA
function cargarGridIngresos() {
    _ajax(null,
        '/CatalogoDeIngresos/GetData',
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
            var ListaIngresos = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaIngresos.length; i++) {
                template += '<tr data-id = "' + ListaIngresos[i].cin_IdIngresos + '">' +
                    '<td>' + ListaIngresos[i].cin_IdIngresos + '</td>' +
                    '<td>' + ListaIngresos[i].cin_DescripcionIngreso + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaIngresos[i].cin_IdIngresos + '" type="button" class="btn btn-primary btn-xs" id="btnEditarIngreso">Editar</button>' +
                    '<button data-id = "' + ListaIngresos[i].cin_IdIngresos + '" type="button" class="btn btn-default btn-xs" id="btnDetalle">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyIngresos').html(template);
        });
    FullBody();
}


// DETALLES
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnDetalle", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/CatalogoDeIngresos/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log(data);
                var FechaCrea = FechaFormato(data[0].cin_FechaCrea);
                var FechaModifica = FechaFormato(data[0].cin_FechaModifica);
                $("#Detallar #cin_IdIngreso").val(data[0].cin_IdIngreso);
                $("#Detallar #cin_DescripcionIngreso").val(data[0].cin_DescripcionIngreso);
                $("#Detallar #cin_UsuarioCrea").val(data[0].cin_UsuarioCrea);
                $("#Detallar #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detallar #cin_FechaCrea").val(FechaCrea);
                data[0].UsuModifica == null ? $("#Detallar #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detallar #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detallar #cin_UsuarioModifica").val(data[0].cin_UsuarioModifica);
                $("#Detallar #cin_FechaModifica").val(FechaModifica);
                $("#DetailCatalogoIngresos").modal();

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



//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnEditarIngreso", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
    $.ajax({
        url: "/CatalogoDeIngresos/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #cin_IdIngreso").val(data.cin_IdIngreso);
                $("#Editar #cin_DescripcionIngreso").val(data.cin_DescripcionIngreso);
                //$(".field-validation-error").css('display', 'none');
                $("#EditarCatalogoIngresos").modal();
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
$("#btnUpdateIngresos").click(function () {

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmCatalogoIngresos").serializeArray();
    var descripcionEditar = $("#Editar #cin_DescripcionIngreso").val();

    //VALIDAMOS LOS CAMPOS
    if (descripcionEditar != '' && descripcionEditar != null && descripcionEditar != undefined && isNaN(descripcionEditar) == true) {

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Edit",
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
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarCatalogoIngresos").modal('hide');
                cargarGridIngresos();

                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue editado de forma exitosa!',
                });
            }
        });
    }
    else {
        $("#Editar #cin_DescripcionIngreso").focus();
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
});


// INACTIVAR 
$("#btnModalInactivar").click(function () {
    $("#EditarCatalogoIngresos").modal('hide');
    $("#InactivarCatalogoIngresos").modal();
});

$("#btnInactivarIngresos").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmInactivarCatalogoIngresos").serializeArray();
    var ID = InactivarID;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Inactivar/" + ID,
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
            $("#InactivarCatalogoIngresos").modal('hide');
            $("#EditarCatalogoIngresos").modal('hide');
            cargarGridIngresos();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue inactivado de forma exitosa!',
            });
        }
    });
});




//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarCatalogoIngresos", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarCatalogoIngresos").modal();
});

$("#frmCatalogoIngresosCreate").submit(function (e) {
    return false;
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroIngresos').click(function () {

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCatalogoIngresosCreate").serializeArray();

    var descripcion = $("#Crear #cin_DescripcionIngreso").val();

    //VALIDAMOS LOS CAMPOS
    if (descripcion != '' && descripcion != null && descripcion != undefined && isNaN(descripcion) == true) {

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR

            if (data == "error") {
                $("#AgregarCatalogoIngresos").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else {
                $("#AgregarCatalogoIngresos").modal('hide');
                cargarGridIngresos();
                $("#Crear #cin_DescripcionIngreso").val('');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue registrado de forma exitosa!',
                });
            }

        });
    }
    else {
        $("#Crear #cin_DescripcionIngreso").focus();
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarCatalogoIngresos").modal('hide');
    $("#frmCatalogoIngresosCreate").modal('hide');
});

