// Reportes varios 

// validar institucion financiera
$('#insf_IdInstitucionFinanciera').on('change', function () {

    if (this.value != 0) {
        $("#validation_InstitucionRequerida").css('display', 'none');
        $("#AsteriscoInstitucion").removeClass('text-danger');

    }
    else {
        $("#validation_InstitucionRequerida").css('display', '');
        $("#AsteriscoInstitucion").addClass("text-danger");
    }
});

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
$('#deif_FechaCrea').on("keyup change", function () {

    var date = $(this).val();

    // validar que sea mayor que 1920
    if (date < '1920-01-01') {
        $("#validation_FechaInicioMenor1920").css('display', '');
        $("#AsteriscoFechaRegistro").addClass("text-danger");
    }
    else {
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#AsteriscoFechaRegistro").removeClass('text-danger');
    }

    // validar que no esté vacío
    if (date == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#validation_FechaInicioRequerida").css('display', '');
        $("#AsteriscoFechaRegistro").addClass("text-danger");
    }
    else {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        if (date >= '1920-01-01') {
            $("#AsteriscoFechaRegistro").removeClass('text-danger');
        }
    }

});

$("#frmReportIngresosPreview").bind('submit', function (e) {

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

    // validar institucion
    
    if ($('#insf_IdInstitucionFinanciera').val() != 0) {
        $("#validation_InstitucionRequerida").css('display', 'none');
        $("#AsteriscoInstitucion").removeClass('text-danger');

    }
    else {
        $("#validation_InstitucionRequerida").css('display', '');
        $("#AsteriscoInstitucion").addClass("text-danger");
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
    var date = $('#deif_FechaCrea').val();

    // validar que sea mayor que 1920
    if (date < '1920-01-01') {
        $("#validation_FechaInicioMenor1920").css('display', '');
        $("#AsteriscoFechaRegistro").addClass("text-danger");
        modelState = false;
    }
    else {
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#AsteriscoFechaRegistro").removeClass('text-danger');
    }

    // validar que no esté vacío
    if (date == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#validation_FechaInicioRequerida").css('display', '');
        $("#AsteriscoFechaRegistro").addClass("text-danger");
        modelState = false;
    }
    else {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        if (date >= '1920-01-01') {
            $("#AsteriscoFechaRegistro").removeClass('text-danger');
        }
    }


    if (modelState == true) {
        // serializar formulario
        var data = $("#frmReportIngresosPreview").serializeArray();

        $.ajax({
            url: "/ReportesPlanilla/InstitucionesFinancierasParametros",
            method: "POST",
            data: data
        })
    }
    else {
        return false;
    }
});

