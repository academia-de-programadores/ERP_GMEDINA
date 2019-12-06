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


// MOSTRAR MODAL DE FECHAS
$(document).on("click", "#ParametrosISRRPT", function () {
    $("#ISRModalRPT").modal();
});

// OCULTAR MODAL DE FECHAS
$("#btnCerrarModalRPT").click(function () {
    $("#ISRModalRPT").modal('hide');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModalRPT").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");

    $("#hipa_FechaPago").val('');
    $("#cpla_DescripcionPlanilla").val('');

});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");

    $("#hipa_FechaPago").val('');
    $("#cpla_DescripcionPlanilla").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnEnviarParametros").click(function () {
    var Parametro1 = $("#hipa_FechaPago").val();
    var Parametro2 = $("#cpla_DescripcionPlanilla").val();

    if (Parametro1 == "" || Parametro2 == "") {
        $("#Validation_descipcion").css("display", "");
        $("#Validation_descipcion2").css("display", "");
    }

});