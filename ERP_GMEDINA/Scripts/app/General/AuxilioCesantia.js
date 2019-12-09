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

$(document).ready(function () {
    console.clear();
});


//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnModalCrear", function ()
{
    //MOSTRAR EL MODAL DE AGREGAR
    $("#rangoinicio").val('');
    $("#rangofin").val('');
    $("#diasauxces").val('');
    $("#frmCrearAuxCes").modal();
});

//$("#frmCrearAuxCes").submit(function (e)
//{
//    return false;
//});


//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCrearAuxCes').click(function ()
{
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCrearAuxCes").serializeArray();

    var rangoInicio = $("#rangoinicio").val();
    var rangoFin = $("#rangofin").val();
    var diasAuxCes = $("#diasauxces").val();

    //VALIDAMOS LOS CAMPOS
    if (rangoInicio != '' && rangoInicio != null && rangoInicio != undefined && isNaN(rangoInicio) == true && rangoFin != '' && rangoFin != null && rangoFin != undefined && isNaN(rangoFin) == true
        && diasAuxCes != '' && diasAuxCes != null && diasAuxCes != undefined && isNaN(diasAuxCes) == true)
    {

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/AuxilioDeCesantias/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR

            if (data == "error")
            {
                $("#frmCrearAuxCes").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else
            {
                $("#frmCrearAuxCes").modal('hide');
                cargarGridIngresos();
                $("#rangoinicio").val('');
                $("#rangofin").val('');
                $("#diasauxces").val('');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue registrado de forma exitosa!',
                });
            }

        });
    }
    else {
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


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
//$("#btnCrearAuxCes").click(function () {
//    var FechaInicial = $("#hipa_FechaInicio").val();
//    var FechaFinal = $("#hipa_FechaFin").val();

//    if (FechaInicial == "" || FechaFinal == "") {
//        $("#Validation_descipcion").css("display", "");
//        $("#Validation_descipcion2").css("display", "");
//    }

//});