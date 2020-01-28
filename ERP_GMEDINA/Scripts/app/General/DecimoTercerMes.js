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


$("body").on("click", "#btnProcesar", function () {
    //Recorra las filas de la tabla y cree una matriz JSON.
    var DecimoTercer = new Array();
    $("#tblDecimoTercerMes TBODY TR").each(function () {
        var row = $(this);
        var DC = {};
        DC.emp_Id = row.find("TD").eq(0).html();
        DC.dtm_Monto = row.find("TD").eq(5).html();
        DecimoTercer.push(DC);
    });

    //Envíe la matriz JSON al controlador con AJAX.
    $.ajax({
        type: "POST",
        url: "/DecimoTercerMes/InsertDecimoTercerMes",
        data: JSON.stringify(DecimoTercer),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            if (data != "-1")
                iziToast.success({
                    title: 'Decimotercer Mes',
                    message: "¡Decimotercer mes procesado de forma exitosa!"
                });
            else
                iziToast.error({
                    title: 'Decimotercer Mes',
                    message: "No puede procesar dos veces un pago."
                });
        },
        error: function (e) {
            iziToast.error({
                title: 'Decimotercer Mes',
                message: "No puede procesar dos veces un pago."
            });
        }
    });
});



// MOSTRAR MODAL DE FECHAS
$(document).on("click", "#btnFechaEspecifica", function () {
    $("#frmFechaDecimoTercer").modal();
});

// OCULTAR MODAL DE FECHAS
$("#btnCerrarFecha").click(function () {
    $("#frmFechaDecimoTercer").modal('hide');
    $("#hipa_FechaInicio").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarFecha").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#hipa_FechaInicio").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#hipa_FechaInicio").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnEnviarFecha").click(function () {
    var FechaInicial = $("#hipa_FechaInicio").val();
    var FechaFinal = $("#hipa_FechaFin").val();

    if (FechaInicial == "" || FechaFinal == "") {
        $("#Validation_descipcion").css("display", "");
        $("#Validation_descipcion2").css("display", "");
    }

});