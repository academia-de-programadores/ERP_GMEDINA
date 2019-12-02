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
$(document).on("click", "#ParametrosDecimoTercerMesRPT", function () {
	$("#DecimoTercerMesModalRPT").modal();
});

// OCULTAR MODAL DE FECHAS
$("#btnCerrarModalRPT").click(function () {
	$("#DecimoTercerMesModalRPT").modal('hide');
});








////FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
//$("#btnCerrarFecha").click(function () {
//    $("#Validation_descipcion").css("display", "none");
//    $("#Validation_descipcion2").css("display", "none");
//});


////FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
//$("#IconCerrar").click(function () {
//    $("#Validation_descipcion").css("display", "none");
//    $("#Validation_descipcion2").css("display", "none");
//});


////FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
//$("#btnEnviarFecha").click(function () {
//    var FechaInicial = $("#hipa_FechaInicio").val();
//    var FechaFinal = $("#hipa_FechaFin").val();

//    if (FechaInicial == "" || FechaFinal == "") {
//        $("#Validation_descipcion").css("display", "");
//        $("#Validation_descipcion2").css("display", "");
//    }

//});