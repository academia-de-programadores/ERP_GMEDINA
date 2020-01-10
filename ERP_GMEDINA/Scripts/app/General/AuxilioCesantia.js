//FUNCION GENERICA PARA REUTILIZAR AJAX
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        type: type,
        data: { params },
        success: function (data)
        {
            callback(data);
        }
    });
}

// REGION DE VARIABLES
var EliminarID = 0;

//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });

//Funcion para refrescar la tabala (Index)
function cargarGridAuxilioCesantia() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AuxilioDeCesantias/GetData',
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
            var ListaAuxCes = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAuxCes.length; i++)
            {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaAuxCes[i].aces_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = ListaAuxCes[i].aces_Activo == true ? '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-primary btn-xs"  id="btnModalDetalles">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAuxCes[i].aces_Activo == true ? '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-default btn-xs"  id="btnModalEdit">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAuxCes[i].aces_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-primary btn-xs"  id="btnModalActivarAuxCes">Activar</button>' : '' : '';


                console.log(estadoRegistro);

                template += '<tr data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '">' +
                    '<td>' + ListaAuxCes[i].aces_IdAuxilioCesantia + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_RangoInicioMeses + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_RangoFinMeses + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_DiasAuxilioCesantia + '</td>' +
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
            $('#tbodyAuxCes').html(template);
        });
    FullBody();

}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnModalCrear", function ()
{
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #aces_RangoInicioMeses").val('');
    $("#Crear #aces_RangoFinMeses").val('');
    $("#Crear #aces_DiasAuxilioCesantia").val('');
    $("#frmCrearAuxCes").modal();
});

$("#frmCrearAuxCes").submit(function (e)
{
    return false;
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCrearAuxCes').click(function ()
{
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCrearAuxCess").serializeArray();

    var rangoInicio = $("#Crear #aces_RangoInicioMeses").val();
    var rangoFin = $("#Crear #aces_RangoFinMeses").val();
    var diasAuxCes = $("#Crear #aces_DiasAuxilioCesantia").val();

   // console.log(rangoInicio + ' ' + rangoFin +' '+ diasAuxCes);

    //VALIDAMOS LOS CAMPOS
    if (rangoInicio >= 0 && rangoFin > 0 && diasAuxCes > 0)
    {
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/AuxilioDeCesantias/Create",
            method: "POST",
            data: data
        }).done(function (data)
        {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR

            if (data == "error")
            {
                $("#frmCrearAuxCes").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
            else {
                $("#frmCrearAuxCes").modal('hide');
                cargarGridAuxilioCesantia();
                $("#Crear #aces_RangoInicioMeses").val('');
                $("#Crear #aces_RangoFinMeses").val('');
                $("#Crear #aces_DiasAuxilioCesantia").val('');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro se agregó de forma exitosa!',
                });
            }

        });
    }
    else
    {
        $("#rangoinicio").focus();
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
});


// OCULTAR MODAL DE REGISTRO NUEVO
$("#btnCerrarCrearAuxCes").click(function () {
    $("#frmCrearAuxCes").modal('hide');
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrearAuxCes").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
});

// DETALLES Auxilio Cesantias
$(document).on("click", "#tblAuxCesantia tbody tr td #btnModalDetalles", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/AuxilioDeCesantias/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data)
        {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data)
            {
                console.log('la data es:');
                console.log(data);
                console.log(data);
                var FechaCrea = FechaFormato(data[0].aces_FechaCrea);
                var FechaModifica = FechaFormato(data[0].aces_FechaModifica);
                $("#aces_IdAuxilioCesantia").html(data[0].aces_IdAuxilioCesantia);
                $("#frmDetallesAuxCess #aces_RangoInicioMeses").html(data[0].aces_RangoInicioMeses);
                //$("frmDetallesAuxCess #aces_RangoInicioMeses2").html(data[0].aces_RangoInicioMeses);
                $("#frmDetallesAuxCess #aces_RangoFinMeses").html(data[0].aces_RangoFinMeses);
                $("#frmDetallesAuxCess #aces_DiasAuxilioCesantia").html(data[0].aces_DiasAuxilioCesantia);
                $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#aces_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#aces_UsuarioModifica").val(data[0].aces_UsuarioModifica);
                $("#aces_FechaModifica").html(FechaModifica);
                $("#frmDetailAuxCes").modal();

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
$(document).on("click", "#tblAuxCesantia tbody tr td #btnModalEdit", function () {
    var ID = $(this).data('id');
    EliminarID = ID;
    $.ajax({
        url: "/AuxilioDeCesantias/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data)
        {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data)
            {
                var FechaModifica = FechaFormato(data.aces_FechaModifica);
                $("#frmEditarAuxCes #aces_IdAuxilioCesantia").val(data.aces_IdAuxilioCesantia);
                $("#frmEditarAuxCes #aces_RangoInicioMeses").val(data.aces_RangoInicioMeses);
                $("#frmEditarAuxCes #aces_RangoFinMeses").val(data.aces_RangoFinMeses);
                $("#frmEditarAuxCes #aces_DiasAuxilioCesantia").val(data.aces_DiasAuxilioCesantia);
                $("#aces_UsuarioModifica").val(data.aces_UsuarioModifica);
                $("#aces_FechaModifica").val(FechaModifica);
                $("#frmEditarAuxCes").modal();
            }
            else
            {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateAuxCes").click(function () {

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEditarAuxCesan").serializeArray();
   // var descripcionEditar = $("#Editar #cin_DescripcionIngreso").val();
    var rangoInicio = $("#Editar #aces_RangoInicioMeses").val();
    var rangoFin = $("#Editar #aces_RangoFinMeses").val();
    var diasAuxCes = $("#Editar #aces_DiasAuxilioCesantia").val();
    console.log(data);
    debugger;

    //VALIDAMOS LOS CAMPOS
    if (rangoInicio >= 0 && rangoInicio < rangoFin && rangoFin > 0 && diasAuxCes > 0)
    {
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/AuxilioDeCesantias/Edit",
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
                $("#frmEditarAuxCes").modal('hide');
                cargarGridAuxilioCesantia();

                iziToast.success({
                    title: 'Exito',
                    message: 'El registro se editó de forma exitosa!',
                });
            }
        });
    }
    else {
        $("#Editar #aces_RangoInicioMeses").focus();
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
});

// INACTIVAR 
$("#btnModalEliminar").click(function () {
    $("#frmEditarAuxCes").modal('hide');
    $("#frmEliminarAuxCes").modal();
});

$("#btnEliminarAuxCes").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEliminarAuxCes").serializeArray();
    var ID = EliminarID;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AuxilioDeCesantias/Inactivar/" + ID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            $("#frmEliminarAuxCes").modal('hide');
            $("#frmEditarAuxCes").modal('hide');
            cargarGridAuxilioCesantia();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro se inactivó de forma exitosa!',
            });
        }
    });
});


// activar
var activarID = 0;
$(document).on("click", "#btnModalActivarAuxCes", function () {
    activarID = $(this).data('id');
    $("#frmActivarAuxCes").modal();
});

//activar ejecutar
$("#btnActivarAuxCes").click(function () {

    $.ajax({
        url: "/AuxilioDeCesantias/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridAuxilioCesantia();
            $("#frmActivarAuxCes").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});


