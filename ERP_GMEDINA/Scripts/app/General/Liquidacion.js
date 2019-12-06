//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback, enviar) {
	$.ajax({
		url: uri,
		type: type,
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		data: JSON.stringify(params),
		beforeSend: function() {
			enviar();
		},
		success: function(data) {
			callback(data);
		}
	});
}

//#region Variables Globales
var fecha = new Date();
// Inputs divs y ddl
const inputFechaFin = $('#fechaFin'),
	ddlEmpleados = $('#cmbxEmpleados'),
	validacionSelectFechaFin = $('#validacionSelectFechaFin'),
	validacionSelectEmpleado = $('#validacionSelectEmpleado'),
	cargarSpinnerDatosColaborador = $('#cargarSpinnerDatosColaborador'),
	cargarSpinnerSalarios = $('#cargarSpinnerSalarios'),
	divSalarios = $('#Salarios'),
	divDatosColaborador = $('#datosColaborador'),
	// Datos del empleado
	spanDiasLaborados = $('#spanDiasLaborados'),
	spanMesesLaborados = $('#spanMesesLaborados'),
	spanAniosLaborados = $('#spanAniosLaborados'),
	spanNombreEmpleado = $('#spanNombreEmpleado'),
	spanApellidoEmpleado = $('#spanApellidoEmpleado'),
	spanEdadEmpleado = $('#spanEdadEmpleado'),
	spanSexoEmpleado = $('#spanSexoEmpleado'),
	spanDepartamentoEmpleado = $('#spanDepartamentoEmpleado'),
	spanIdentidadEmpleado = $('#spanIdentidadEmpleado'),
	spanSueldoEmpleado = $('#spanSueldoEmpleado'),
	spanCargoEmpleado = $('#spanCargoEmpleado'),
	spanFechaIngresoEmpleado = $('#spanFechaIngresoEmpleado'),
	// Salarios
	spanSalario = $('#spanSalario'),
	spanSalarioOrdinarioDiario = $('#spanSalarioOrdinarioDiario'),
	spanSalarioOrdinarioPromedioDiario = $('#spanSalarioOrdinarioPromedioDiario'),
	spanSalarioPromedioDiario = $('#spanSalarioPromedioDiario');
//const  = $('#');
//#endregion

//Mostrar el spinner
function spinner() {
	return `<div class="sk-spinner sk-spinner-wave">
                <div class="sk-rect1"></div>
                <div class="sk-rect2"></div>
                <div class="sk-rect3"></div>
                <div class="sk-rect4"></div>
                <div class="sk-rect5"></div>
             </div>`;
}

$(ddlEmpleados).change(() => {
	const ddlEmpleadosLleno = ddlEmpleados.val() != '';
	if (ddlEmpleadosLleno) validacionSelectEmpleado.hide();

	const fechaFinVal = inputFechaFin.val();
	const ddlEmpleadosVal = ddlEmpleados.val();
	if (ddlEmpleadosLleno && inputFechaFinLlena())
		if (validarCampos()) {
			obtenerDatosEmpleados(ddlEmpleadosVal, fechaFinVal);
		}
});

$(inputFechaFin).change(() => {
	if (inputFechaFinLlena()) validacionSelectFechaFin.hide();
});

$(document).ready(function() {
	validacionSelectEmpleado.val('');
	$('#datepicker .input-group.date')
		.datepicker({
			todayBtn: 'linked',
			keyboardNavigation: false,
			forceParse: false,
			calendarWeeks: true,
			autoclose: true,
			format: 'yyyy/mm/dd'
		})
		.on('changeDate', function(e) {
			if (validarCampos()) {
				const fechaFinVal = inputFechaFin.val();
				const ddlEmpleadosVal = ddlEmpleados.val();
				obtenerDatosEmpleados(ddlEmpleadosVal, fechaFinVal);
			}
		});

	//Llengar DDL Areas con Empleados
	_ajax(
		null,
		'Liquidacion/GetEmpleadosAreas',
		'GET',
		(data) => {
			$('#cmbxEmpleados').select2({
				placeholder: 'Seleccione un empleado',
				allowClear: true,
				language: {
					noResults: function() {
						return 'Resultados no encontrados.';
					},
					searching: function() {
						return 'Buscando...';
					}
				},
				data: data.results
			});
		},
		() => {}
	);
});
function obtenerDatosEmpleados(idEmpleado, fechaFin) {
	_ajax(
		{
			idEmpleado: idEmpleado,
			fechaFin: fechaFin
		},
		'/Liquidacion/GetInfoEmpleado',
		'POST',
		(data) => {
			console.log(data);
			mostrarDatosColaborador(
				data.consulta[0].nombreEmpleado,
				data.consulta[0].apellidoEmpleado,
				data.consulta[0].numeroIdentidad,
				data.consulta[0].sexoEmpleado,
				data.consulta[0].edadEmpleado,
				data.consulta[0].cantidadSueldo,
				data.consulta[0].descripcionCargo,
				data.consulta[0].descripcionDepartamento,
				data.consulta[0].descripcionMoneda,
				data.consulta[0].fechaIngreso,
				data.anios,
				data.meses,
				data.dias,
				data.salarios.salario,
				data.salarios.salarioOrdinarioDiario,
				data.salarios.salarioOrdinarioPromedioDiario,
				data.salarios.salarioPromedioDiario
			);

			cargarSpinnerDatosColaborador.html('');
			cargarSpinnerDatosColaborador.hide();
			divDatosColaborador.show();
			cargarSpinnerSalarios.html('');
			cargarSpinnerSalarios.hide();
			divSalarios.show();
		},
		() => {
			divDatosColaborador.hide();
			cargarSpinnerDatosColaborador.html(spinner());
			cargarSpinnerDatosColaborador.show();
			divSalarios.hide();
			cargarSpinnerSalarios.html(spinner());
			cargarSpinnerSalarios.show();
		}
	);
}

function inputFechaFinLlena() {
	return inputFechaFin.val() != '';
}

function validarCampos() {
	var todoBien = true;

	//Validar que el drop down list tenga seleccionado un empleado.
	if (ddlEmpleados.val() == '') {
		todoBien = false;
		validacionSelectEmpleado.show();
	}

	//Validar que no este vacio el campo de fecha de despido.
	if (inputFechaFin.val() == '') {
		todoBien = false;
		validacionSelectFechaFin.show();
	}

	return todoBien;
}

function mostrarDatosColaborador(
	nombreEmpleado = '',
	apellidoEmpleado = '',
	numeroIdentidad = '',
	sexoEmpleado = '',
	edadEmpleado = '',
	cantidadSueldo = '',
	descripcionCargo = '',
	descripcionDepartamento = '',
	descripcionMoneda = '',
	fechaIngreso = '',
	anios = '',
	meses = '',
	dias = '',
	salario = '',
	salarioOrdinarioDiario = '',
	salarioOrdinarioPromedioDiario = '',
	salarioPromedioDiario = ''
) {
	spanDiasLaborados.html(dias);
	spanMesesLaborados.html(meses);
	spanAniosLaborados.html(anios);
	spanNombreEmpleado.html(nombreEmpleado);
	spanApellidoEmpleado.html(apellidoEmpleado);
	spanEdadEmpleado.html(edadEmpleado);
	spanSexoEmpleado.html(sexoEmpleado);
	spanDepartamentoEmpleado.html(descripcionDepartamento);
	spanIdentidadEmpleado.html(numeroIdentidad);
	spanSueldoEmpleado.html(cantidadSueldo + ' ' + descripcionMoneda);
	spanCargoEmpleado.html(descripcionCargo);
	spanFechaIngresoEmpleado.html(fechaIngreso);
	spanSalario.html(salario);
	spanSalarioOrdinarioDiario.html(salarioOrdinarioDiario);
	spanSalarioOrdinarioPromedioDiario.html(salarioOrdinarioPromedioDiario);
	spanSalarioPromedioDiario.html(salarioPromedioDiario);
}
