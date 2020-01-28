// Reportes varios 

// validar reporte seleccionado
$('#cde_IdDeducciones').on('change', function () {

	if (this.value != 0) {
		$("#validation_ReporteRequerida").css('display', 'none');
		$("#AsteriscoReporte").removeClass('text-danger');

	}
	else {
		$("#validation_ReporteRequerida").css('display', '');
		$("#AsteriscoReporte").addClass("text-danger");
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


// validar fecha inicio
$('#hipa_FechaInicio').on("keyup change", function () {

	var date = $(this).val();
	var dateFin = $('#hipa_FechaFin').val();

	// validar que sea mayor que 1920
	if (date < '1920-01-01') {
		$("#validation_FechaInicioMenor1920").css('display', '');
		$("#AsteriscoFechaInicio").addClass("text-danger");
	}
	else {
		$("#validation_FechaInicioMenor1920").css('display', 'none');
		$("#AsteriscoFechaInicio").removeClass('text-danger');
	}

	// validar que no sea mayor que el rango fin
	if (date >= dateFin) {

		$("#validation_FechaInicioRequerida").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');

		$("#validation_FechaFinMenorFechaInicio").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
	}
	else {
		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');

		if (date >= '1920-01-01') {
			$("#AsteriscoFechaInicio").removeClass('text-danger');
		}

		$("#AsteriscoFechaFin").removeClass("text-danger");
	}


	// validar que no esté vacío
	if (date == '') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaInicioMenor1920").css('display', 'none');
		$("#validation_FechaInicioRequerida").css('display', '');
		$("#AsteriscoFechaInicio").addClass("text-danger");
	}
	else {

		$("#validation_FechaInicioRequerida").css('display', 'none');
		if (date >= '1920-01-01') {
			$("#AsteriscoFechaInicio").removeClass('text-danger');
		}
	}

});

// validar fecha inicio
$('#hipa_FechaFin').on("keyup change", function () {

	var date = $('#hipa_FechaInicio').val();
	var dateFin = $(this).val();

	// validar que sea mayor que 1920
	if (dateFin < '1920-01-01') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');
		$("#validation_FechaFinMenor192").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
	}
	else {
		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#AsteriscoFechaFin").removeClass('text-danger');
	}

	// validar que no sea mayor que el rango fin
	if (date >= dateFin) {

		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');
		$("#validation_FechaFinMenorFechaInicio").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
	}
	else {
		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');

		if (dateFin >= '1920-01-01') {
			$("#AsteriscoFechaFin").removeClass('text-danger');
		}
	}


	// validar que no esté vacío
	if (dateFin == '') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
	}
	else {

		$("#validation_FechaFinRequerida").css('display', 'none');
		if (dateFin >= '1920-01-01') {
			$("#AsteriscoFechaFin").removeClass('text-danger');
		}
	}

});

$("#frmReportDeduccionesPreview").bind('submit', function (e) {

	modelState = true;

	// validar reporte seleccionado    
	if ($('#cde_IdDeducciones').val() != 0) {
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

	// validar fecha inicio    
	var date = $('#hipa_FechaInicio').val();
	var dateFin = $('#hipa_FechaFin').val();

	// validar que sea mayor que 1920
	if (date < '1920-01-01') {
		$("#validation_FechaInicioMenor1920").css('display', '');
		$("#AsteriscoFechaInicio").addClass("text-danger");
		modelState = false;
	}
	else {
		$("#validation_FechaInicioMenor1920").css('display', 'none');
		$("#AsteriscoFechaInicio").removeClass('text-danger');
	}

	// validar que no sea mayor que el rango fin
	if (date >= dateFin) {

		$("#validation_FechaInicioRequerida").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');

		$("#validation_FechaFinMenorFechaInicio").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
		modelState = false;
	}
	else {
		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');

		if (date >= '1920-01-01') {
			$("#AsteriscoFechaInicio").removeClass('text-danger');
		}

		$("#AsteriscoFechaFin").removeClass("text-danger");
	}


	// validar que no esté vacío
	if (date == '') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaInicioMenor1920").css('display', 'none');
		$("#validation_FechaInicioRequerida").css('display', '');
		$("#AsteriscoFechaInicio").addClass("text-danger");
		modelState = false;
	}
	else {

		$("#validation_FechaInicioRequerida").css('display', 'none');
		if (date >= '1920-01-01') {
			$("#AsteriscoFechaInicio").removeClass('text-danger');
		}
	}

	// validar fecha fin 

	// validar que sea mayor que 1920
	if (dateFin < '1920-01-01') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');
		$("#validation_FechaFinMenor192").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
		modelState = false;
	}
	else {
		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#AsteriscoFechaFin").removeClass('text-danger');
	}

	// validar que no sea mayor que el rango fin
	if (date >= dateFin) {

		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', 'none');
		$("#validation_FechaFinMenorFechaInicio").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
		modelState = false;
	}
	else {
		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');

		if (dateFin >= '1920-01-01') {
			$("#AsteriscoFechaFin").removeClass('text-danger');
		}
	}


	// validar que no esté vacío
	if (dateFin == '') {

		$("#validation_FechaFinMenorFechaInicio").css('display', 'none');
		$("#validation_FechaFinMenor192").css('display', 'none');
		$("#validation_FechaFinRequerida").css('display', '');
		$("#AsteriscoFechaFin").addClass("text-danger");
		modelState = false;
	}
	else {

		$("#validation_FechaFinRequerida").css('display', 'none');
		if (dateFin >= '1920-01-01') {
			$("#AsteriscoFechaFin").removeClass('text-danger');
		}
	}

	if (modelState == true) {
		// serializar formulario
		var data = $("#frmReportDeduccionesPreview").serializeArray();

		$.ajax({
			url: "/ReportesPlanilla/DeduccionesParametros",
			method: "POST",
			data: data
		})
	}
	else {
		return false;
	}
});

