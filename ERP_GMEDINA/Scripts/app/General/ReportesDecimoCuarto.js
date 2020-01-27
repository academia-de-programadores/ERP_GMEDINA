// Reportes varios 

// validar planilla seleccionada
$('#cpla_IdPlanilla').on('change', function () {

    if (this.value != 0) {
        $("#validation_PlanillaRequerida").css('display', 'none');
        $("#AsteriscoPlanilla").removeClass('text-danger');

    }
    else {
        $("#validation_PlanillaRequerida").css('display', '');
        $("#AsteriscoPlanilla").addClass("text-danger");
    }
});


// validar fecha pago
$('#dcm_FechaPago').on("keyup change", function () {

    var date = $(this).val();

    // validar que sea mayor que 1920
    if (date < '1920-01-01') {
        $("#validation_FechaInicioMenor1920").css('display', '');
        $("#AsteriscoFechaPago").addClass("text-danger");
    }
    else {
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#AsteriscoFechaPago").removeClass('text-danger');
    }    

    // validar que no esté vacío
    if (date == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#validation_FechaInicioRequerida").css('display', '');
        $("#AsteriscoFechaPago").addClass("text-danger");
    }
    else {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        if (date >= '1920-01-01') {
            $("#AsteriscoFechaPago").removeClass('text-danger');
        }
    }

});

$("#frmReportDeduccionesPreview").bind('submit', function (e) {

    modelState = true;

    // validar reporte seleccionado    
    if ($('#cin_IdIngreso').val() != 0) {
        $("#validation_ReporteRequerida").css('display', 'none');
        $("#AsteriscoReporte").removeClass('text-danger');

    }
    else {
        $("#validation_ReporteRequerida").css('display', '');
        $("#AsteriscoReporte").addClass("text-danger");
        modelState = false;
    }

    // validar planilla seleccionada    
    if ($('#cpla_IdPlanilla').val() != 0) {
        $("#validation_PlanillaRequerida").css('display', 'none');
        $("#AsteriscoPlanilla").removeClass('text-danger');

    }
    else {
        $("#validation_PlanillaRequerida").css('display', '');
        $("#AsteriscoPlanilla").addClass("text-danger");
        modelState = false;
    }

    // validar fecha pago    
    var date = $('#dcm_FechaPago').val();

    // validar que sea mayor que 1920
    if (date < '1920-01-01') {
        $("#validation_FechaInicioMenor1920").css('display', '');
        $("#AsteriscoFechaPago").addClass("text-danger");
        modelState = false;
    }
    else {
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#AsteriscoFechaPago").removeClass('text-danger');
    }

    // validar que no esté vacío
    if (date == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#validation_FechaInicioRequerida").css('display', '');
        $("#AsteriscoFechaPago").addClass("text-danger");
        modelState = false;
    }
    else {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        if (date >= '1920-01-01') {
            $("#AsteriscoFechaPago").removeClass('text-danger');
        }
    }


    if (modelState == true) {
        // serializar formulario
        var data = $("#frmReportDeduccionesPreview").serializeArray();

        $.ajax({
            url: "/ReportesPlanilla/DecimoCuartoParametros",
            method: "POST",
            data: data
        })
    }
    else {
        return false;
    }
});

