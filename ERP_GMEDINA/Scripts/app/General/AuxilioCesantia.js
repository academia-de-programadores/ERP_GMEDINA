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

function cargarGridAuxilioCesantia() {
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
                template += '<tr data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '">' +
                    '<td>' + ListaAuxCes[i].aces_IdAuxilioCesantia + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_RangoInicioMeses + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_RangoFinMeses + '</td>' +
                    '<td>' + ListaAuxCes[i].aces_DiasAuxilioCesantia + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-primary btn-xs" id="btnModalCrear">Editar</button>' +
                    '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-default btn-xs" id="btnModalCrear">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyAuxCes').html(template);
        });

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

    console.log(rangoInicio + ' ' + rangoFin +' '+ diasAuxCes);

    //VALIDAMOS LOS CAMPOS
    //if (rangoInicio >= 0 && rangoFin > 0 && diasAuxCes > 0)
    //{
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
                    message: 'El registro fue registrado de forma exitosa!',
                });
            }

        });
    //}
    //else
    //{
    //    $("#rangoinicio").focus();
    //    iziToast.error({
    //        title: 'Error',
    //        message: 'Ingrese datos válidos.',
    //    });
    //}
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


// DETALLES
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
                console.log(data);
                //var FechaCrea = FechaFormato(data[0].aces_FechaCrea);
                //var FechaModifica = FechaFormato(data[0].aces_FechaModifica);
                $("#aces_IdAuxilioCesantia").val(data[0].aces_IdAuxilioCesantia);
                $("#aces_RangoInicioMeses").val(data[0].aces_RangoInicioMeses);
                $("#aces_RangoFinMeses").val(data[0].aces_RangoFinMeses);
                $("#aces_DiasAuxilioCesantia").val(data[0].aces_DiasAuxilioCesantia);

                $("#tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#aces_FechaCrea").val(data[0].aces_FechaCrea);
                data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#aces_UsuarioModifica").val(data[0].aces_UsuarioModifica);
                $("#aces_FechaModifica").val(data[0].aces_FechaModifica);
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
