//#region Declaracion de variables
//Validaciones de Botones de las Pantallas
const btnAgregar = $('#btnAgregar'),
	//Div que aparecera cuando se le de click en crear
	cargandoCrear = $('#cargandoCrear'),
	equipoEmpId = $('#eqem_Id'),
	equipoEmpIdEdit = $('#eqem_Id'),
	montoInicial = $('#dex_MontoInicial'),
	montoRestante = $('#dex_MontoRestante'),
	observaciones = $('#dex_ObservacionesComentarios'),
	idDeduccion = $('#cde_Id'),
	cuota = $('#dex_Cuota'),
	asteriscoEquipoEmpleado = $('#asteriscoEquipoEmpleado'),
	asteriscoMontoInicial = $('#asteriscoMontoInicial'),
	asteriscoMontoRestante = $('#asteriscoMontoRestante'),
	asteriscoObservaciones = $('#asteriscoObservaciones'),
	asteriscoIdDeducciones = $('#asteriscoIdDeducciones'),
	asteriscoCuota = $('#asteriscoCuota'),
	validacionEquipoEmpleado = $('#validacionEquipoEmpleado'),
	validacionMontoInicial = $('#validacionMontoInicial'),
	validacionMontoRestante = $('#validacionMontoRestante'),
	validacionObservaciones = $('#validacionObservaciones'),
	validacionIdDeducciones = $('#validacionIdDeducciones'),
	validacionCuota = $('#validacionCuota');

const btnEditar = $("#btnEditar"),
	MontoInicial = $('#dex_MontoInicial'),
	MontoRestante = $('#dex_MontoRestante'),
	Observaciones = $('#dex_ObservacionesComentarios'),
	Cuota = $('#dex_Cuota'),
	asteriscMontoInicial = $('#asteriscMontoInicial'),
	asteriscMontoRestante = $('#asteriscMontoRestante'),
	asteriscObservaciones = $('#asteriscObservaciones'),
	asteriscCuota = $('#asteriscCuota'),
	validnEquipoEmpleado = $('#validEquipoEmpleado'),
	validMontoInicial = $('#validMontoInicial'),
	validMontoRestante = $('#validMontoRestante'),
	validObservaciones = $('#validObservaciones'),
	validCuota = $('#validCuota'),
	MontoRestanteEditar = $('#MontoRestanteEditar');



//#endregion

//#region Obtención de Script para Formateo de Fechas
//#endregion

//#region Blur

$('#eqemp_Id').blur(function () {
	let equipoEmpleadoId = $(this).val();
	if (equipoEmpleadoId == null || equipoEmpleadoId == "" || equipoEmpleadoId == 0 || equipoEmpleadoId == "0") {
		$("#validacionEquipoEmpleado").html('Campo Equipo Empleado Requerido');
		$("#validacionEquipoEmpleado").show();
		$("#asteriscoEquipoEmpleado").addClass('text-danger');

	} else {
		$("#validacionEquipoEmpleado").html('');
		$("#validacionEquipoEmpleado").hide();
		$("#asteriscoEquipoEmpleado").removeClass('text-danger');
	}
});

$('#dex_MontoInicial').blur(function () {
	let montoInicial = $(this).val();
	let hayAlgo = false;

	if (montoInicial == "" || montoInicial == null || montoInicial == undefined) {
		$("#valMontoInicial").html('Campo Monto Inicial Requerido');
		$("#valMontoInicial").show();
		$("#asteriscoMontoInicial").addClass('text-danger');
	} else {
		hayAlgo = true;
		$("#valMontoInicial").html('');
		$("#valMontoInicial").hide();
		$("#asteriscoMontoInicial").removeClass('text-danger');
	}

	if (hayAlgo)
		if (montoInicial == 0.00 || montoInicial < 0) {
			$("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
			$("#valMontoInicial").show();
			$("#asteriscoMontoInicial").addClass('text-danger');
		}
		else {
			$("#valMontoInicial").html('');
			$("#valMontoInicial").hide();
			$("#asteriscoMontoInicial").removeClass('text-danger');
		}
});

$('#dex_MontoRestante').blur(function () {
	let montoRestante = $(this).val().replace(/,/g, '');
	let hayAlgo = false;
	let montoInicial = $('#dex_MontoInicial').val().replace(/,/g, '');

	if (montoRestante == "" || montoRestante == null || montoRestante == undefined) {
		$("#valMontoRestante").html('Campo Monto Restante Requerido');
		$("#valMontoRestante").show();
		$("#asteriscoMontoRestante").addClass('text-danger');
	} else {
		hayAlgo = true
		$("#valMontoRestante").html('');
		$("#valMontoRestante").hide();
		$("#asteriscoMontoRestante").removeClass('text-danger');
	}

	if (hayAlgo) {
		let esMayorCero = false;
		if (montoRestante == 0.00 || montoRestante < "0") {
			$("#valMontoRestante").html('El campo Monto Restante no puede ser menor o igual que cero.');
			$("#valMontoRestante").show();
			$("#asteriscoMontoRestante").addClass('text-danger');
		}
		else {
			esMayorCero = true;
			$("#valMontoRestante").html('');
			$("#valMontoRestante").hide()
			$("#asteriscoMontoRestante").removeClass('text-danger');
		}

		let floatMontoRestante = parseFloat(montoRestante);
		let floatMontoInicial = parseFloat(montoInicial)

		if (esMayorCero)
			if (floatMontoRestante > floatMontoInicial) {
				$("#valMontoRestante").html('El campo Monto Restante no puede ser mayor que Monto Inicial.');
				$("#valMontoRestante").show();
				$("#asteriscoMontoRestante").addClass('text-danger');
				hayAlgo = false;
			}
			else {
				hayAlgo = false;
				$("#valMontoRestante").html('');
				$("#valMontoRestante").hide()
				$("#asteriscoMontoRestante").removeClass('text-danger');
			}
	}
});

$('#dex_ObservacionesComentarios').blur(function () {
	let observaciones = $(this)
		.val()
		.trim();
	if (
		observaciones == ""
	) {
		$('#validacionObservaciones').html('Campo Observaciones Requerido');
		$('#validacionObservaciones').show();
		$('#asteriscoObservaciones').addClass('text-danger');
	} else {
		$('#validacionObservaciones').hide();
		$('#asteriscoObservaciones').removeClass('text-danger');
	}
});

$('#cde_Id').blur(function () {
	let deduccion = $(this).val();
	if (deduccion == null || deduccion == "" || deduccion == 0 || deduccion == "0") {
		$("#validacionIdDeducciones").html('Campo Deducción Requerido');
		$("#validacionIdDeducciones").show();
		$("#asteriscoIdDeducciones").addClass('text-danger');

	} else {
		$("#validacionIdDeducciones").html('');
		$("#validacionIdDeducciones").hide();
		$("#asteriscoIdDeducciones").removeClass('text-danger');
	}
});

$('#dex_Cuota').blur(function () {
	let cuota = $(this).val().replace(/,/g, '');
	let hayAlgo = false;
	let montoInicial = $('#dex_MontoInicial').val().replace(/,/g, '');

	if (cuota == null || cuota == "") {
		$("#valCuota").html('Campo Cuota Requerido');
		$("#valCuota").show();
		$("#asteriscoCuota").addClass('text-danger');

	} else {
		hayAlgo = true;
		$("#valCuota").html('');
		$("#valCuota").hide();
		$("#asteriscoCuota").removeClass('text-danger');
	}

	if (hayAlgo) {
		let esMayorCero = false;
		if (cuota == 0.00 || cuota < 0) {
			$("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
			$("#valCuota").show();
			$("#asteriscoCuota").addClass('text-danger');
		}
		else {
			esMayorCero = true;
			$("#valCuota").html('');
			$("#valCuota").hide()
			$("#asteriscoCuota").removeClass('text-danger');
		}

		let cuotaFloat = parseFloat(cuota);
		let floatMontoInicial = parseFloat(montoInicial)

		if (esMayorCero)
			if (cuotaFloat > floatMontoInicial) {
				$("#valCuota").html('El campo Cuota no puede ser mayor que Monto Inicial.');
				$("#valCuota").show();
				$("#asteriscoCuota").addClass('text-danger');
				hayAlgo = false;
			}
			else {
				hayAlgo = false;
				$("#valCuota").html('');
				$("#valCuota").hide()
				$("#asteriscoCuota").removeClass('text-danger');
			}

	}
});

//#endregion

//#region Funciones
//Funció Genérica para utilizar Ajax
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

//Función: Cargar y Actualizar la Data del Index
function cargarGridDeducciones() {
	var esAdministrador = $("#rol_Usuario").val();
	_ajax(null,
		'/DeduccionesExtraordinarias/GetData',
		'GET',
		(data) => {
			if (data.length == 0) {

				//Validar si se genera un error al cargar de nuevo el Index
				iziToast.error({
					title: 'Error',
					message: '¡No se cargó la información, contacte al administrador!',
				});
			}

			//Variable para guardar la data obtenida
			var ListaDeduccionesExtraordinarias = data;

			//LIMPIAR LA DATA DEL DATATABLE
			$('#tblDeduccionesExtraordinarias').DataTable().clear();

			//Recorrer la data obtenida a traves de la función anterior y se crea un Template de la Tabla para Actualizarse
			for (var i = 0; i < ListaDeduccionesExtraordinarias.length; i++) {
				//variable para verificar el estado del registro
				var estadoRegistro = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? 'Inactivo' : 'Activo'

				//variable boton detalles
				var botonDetalles = '<a type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Detalles</a>';

				//variable boton editar
				var botonEditar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? '<a type="button" style="margin-right:3px;" class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Edit?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Editar</a>' : '';

				//variable donde está el boton activar
				var botonActivar = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnActivarDeduccionesExtraordinarias" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Activar</button>' : '' : '';

				//variable boton inactivar
				var botonInactivar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? esAdministrador == "1" ? '<button type="button" name="iddeduccionesextraordinarias" class="btn btn-danger btn-xs" id="btnInactivarDeduccionesExtraordinarias" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Inactivar</button>' : '' : '';

				//AGREGAR EL ROW AL DATATABLE
				$('#tblDeduccionesExtraordinarias').dataTable().fnAddData([
					ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra,
					ListaDeduccionesExtraordinarias[i].per_Nombres + ' ' + ListaDeduccionesExtraordinarias[i].per_Apellidos,
					ListaDeduccionesExtraordinarias[i].dex_MontoInicial,
					ListaDeduccionesExtraordinarias[i].dex_MontoRestante,
					ListaDeduccionesExtraordinarias[i].dex_ObservacionesComentarios,
					ListaDeduccionesExtraordinarias[i].dex_Cuota,
					ListaDeduccionesExtraordinarias[i].cde_DescripcionDeduccion,
					estadoRegistro,
					botonDetalles + botonEditar + botonInactivar + botonActivar
				]);
			}
		});
}

function validaciones(equipoEmpId,
	montoInicial,
	montoRestante,
	observaciones,
	idDeduccion,
	cuota) {
	var todoBien = true;
	let equipoEmpleadoId = equipoEmpId.val();
	//Equipo Empleado
	if (equipoEmpleadoId == null || equipoEmpleadoId == "") {
		$("#validacionEquipoEmpleado").html('Campo Equipo Empleado Requerido');
		$("#validacionEquipoEmpleado").show();
		$("#asteriscoEquipoEmpleado").addClass('text-danger');
		todoBien = false;
	} else {
		$("#validacionEquipoEmpleado").html('');
		$("#validacionEquipoEmpleado").hide();
		$("#asteriscoEquipoEmpleado").removeClass('text-danger');
	}

	montoInicial = montoInicial.val();
	let hayAlgoMontoInicial = false;
	// Monto inicial
	if (montoInicial == "" || montoInicial == null || montoInicial == undefined) {
		$("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
		$("#valMontoInicial").show();
		$("#asteriscoMontoInicial").addClass('text-danger');
		todoBien = false;
	} else {
		$("#valMontoInicial").html('');
		$("#valMontoInicial").hide();
		$("#asteriscoMontoInicial").removeClass('text-danger');
		hayAlgoMontoInicial = true;
	}

	if (hayAlgoMontoInicial) {
		if (montoInicial == 0.00 || montoInicial < 0) {
			$("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
			$("#valMontoInicial").show();
			$("#asteriscoMontoInicial").addClass('text-danger');
			todoBien = false;
		}
		else {
			$("#valMontoInicial").html('');
			$("#valMontoInicial").hide();
			$("#asteriscoMontoInicial").removeClass('text-danger');
		}
	}
	// Monto Restante
	let hayAlgo = false;
	montoRestante = montoRestante.val();
	if (montoRestante == null || montoRestante == "") {
		todoBien = false;
		$("#valMontoRestante").html('Campo Monto Restante Requerido');
		$("#valMontoRestante").show();
		$("#asteriscoMontoRestante").addClass('text-danger');
	} else {
		hayAlgo = true
		$("#valMontoRestante").html('');
		$("#valMontoRestante").hide();
		$("#asteriscoMontoRestante").removeClass('text-danger');
	}

	if (hayAlgo) {
		let esMayorCero = false;
		if (montoRestante == 0.00 || montoRestante < "0") {
			todoBien = false;
			$("#valMontoRestante").html('El campo Monto Restante no puede ser menor o igual que cero.');
			$("#valMontoRestante").show();
			$("#asteriscoMontoRestante").addClass('text-danger');
		}
		else {
			esMayorCero = true;
			$("#valMontoRestante").html('');
			$("#valMontoRestante").hide()
			$("#asteriscoMontoRestante").removeClass('text-danger');
		}

		let floatMontoRestante = parseFloat(montoRestante);
		let floatMontoInicial = parseFloat(montoInicial)

		if (esMayorCero)
			if (floatMontoRestante > floatMontoInicial) {
				todoBien = false;
				$("#valMontoRestante").html('El campo Monto Restante no puede ser mayor que Monto Inicial.');
				$("#valMontoRestante").show();
				$("#asteriscoMontoRestante").addClass('text-danger');
				hayAlgo = false;
			}
			else {
				hayAlgo = false;
				$("#valMontoRestante").html('');
				$("#valMontoRestante").hide()
				$("#asteriscoMontoRestante").removeClass('text-danger');
			}
	}

	// Observaciones
	if (observaciones.val().trim() != '') {
		$('#validacionObservaciones').hide();
		$('#asteriscoObservaciones').removeClass('text-danger');
	} else {
		$('#validacionObservaciones').html('Campo Observaciones Requerido');
		$('#validacionObservaciones').show();
		$('#asteriscoObservaciones').addClass('text-danger');
		todoBien = false;
	}

	// Id deduccion
	if (idDeduccion.val() != '') {
		$("#validacionIdDeducciones").html('');
		$("#validacionIdDeducciones").hide();
		$("#asteriscoIdDeducciones").removeClass('text-danger');
	} else {
		$("#validacionIdDeducciones").html('Campo Deducción Requerido');
		$("#validacionIdDeducciones").show();
		$("#asteriscoIdDeducciones").addClass('text-danger');
		todoBien = false;
	}

	let hayAlgoCuota = false;
	// Cuota
	if (cuota.val() != '') {
		hayAlgoCuota = true;
		$("#valCuota").html('');
		$("#valCuota").hide()
		$("#asteriscoCuota").removeClass('text-danger');
	} else {
		$("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
		$("#valCuota").show();
		$("#asteriscoCuota").addClass('text-danger');
		todoBien = false;
	}

	if (hayAlgoCuota) {
		let esMayorCero = false;
		if (cuota.val() == 0.00 || cuota.val() < 0) {
			$("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
			$("#valCuota").show();
			$("#asteriscoCuota").addClass('text-danger');
			todoBien = false;
		}
		else {
			esMayorCero = true;
			$("#valCuota").html('');
			$("#valCuota").hide()
			$("#asteriscoCuota").removeClass('text-danger');
		}
		cuota = cuota.val();
		let cuotaFloat = parseFloat(cuota.replace(/,/g, ""));
		let floatMontoInicial = parseFloat(montoInicial.replace(/,/g, ""));
		if (esMayorCero)
			if (cuotaFloat > floatMontoInicial) {
				$("#valCuota").html('El campo Cuota no puede ser mayor que Monto Inicial.');
				$("#valCuota").show();
				$("#asteriscoCuota").addClass('text-danger');
				todoBien = false;
				hayAlgo = false;
			}
			else {
				hayAlgo = false;
				$("#valCuota").html('');
				$("#valCuota").hide()
				$("#asteriscoCuota").removeClass('text-danger');
			}
	}
	return todoBien;
}
//#endregion

//#region Activar

//VARIABLE GLOBAL DE INACTIVAR
var GB_Activar = 0;

//Activar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnActivarDeduccionesExtraordinarias", function () {
	//DESBLOQUEAR EL BOTON
	$("#btnActivarRegistroDeduccionesExtraordinarias").attr("disabled", false);
	//OBTENER EL ID
	var ID = $(this).data('id');
	GB_Activar = ID;

	//var ID = $(this).attr('iddeduccionesextra');
	//localStorage.setItem('id', ID);

	//Mostrar el Modal
	$("#ActivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });
});

//Activar
$("#btnActivarRegistroDeduccionesExtraordinarias").click(function () {
	//BLOQUEAR EL BOTON
	$("#btnActivarRegistroDeduccionesExtraordinarias").attr("disabled", true);

	//let ID = localStorage.getItem('id')
	$.ajax({
		url: "/DeduccionesExtraordinarias/Activar/" + GB_Activar,
		method: "GET"
	}).done(function (data) {
		$("#ActivarDeduccionesExtraordinarias").modal('hide');
		//VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
		if (data == "error") {
			//DESBLOQUEAR EL BOTON
			$("#btnActivarRegistroDeduccionesExtraordinarias").attr("disabled", false);
			//MOSTRAR MENSAJE DE ERROR
			iziToast.error({
				title: 'Error',
				message: '¡No se activó el registro, contacte al administrador!',
			});
		}
		else {
			cargarGridDeducciones();
			// Mensaje de exito cuando un registro se ha guardado bien
			iziToast.success({
				title: 'Exito',
				message: '¡El registro se activó de forma exitosa!',
			});
		}
	});
	GB_Activar = 0;
});

// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#ActivarDeduccionesExtraordinarias").submit(function (e) {
	return false;
});

//#endregion

//#region Create

//Ocultar Modal de Create
$("#btnCerrarCreate").click(function () {
	$("#AgregarDeduccionesExtraordinarias").modal('hide');
});



$(btnAgregar).click(function () {
	var data2 = $("#frmCreate").serializeArray();
	if (validaciones(equipoEmpId,
		montoInicial,
		montoRestante,
		observaciones,
		idDeduccion,
		cuota

	)) {

		if ($('#dex_DeducirISR').is(':checked')) {
			dex_DeducirISR = true;
		}
		else {
			dex_DeducirISR = false;
		}

		var data = $("#frmCreate").serializeArray();
		data[2].value = data[2].value.replace(/,/g, '');
		data[6].value = data[6].value.replace(/,/g, '');
		data[3].value = data[3].value.replace(/,/g, '');
		if ($('#dex_DeducirISR').is(':checked')) {
			dex_DeducirISR = true;
			data[7].value = dex_DeducirISR;
		}
		else {
			dex_DeducirISR = false;
		}

		//ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
		$.ajax({
			url: "/DeduccionesExtraordinarias/Create",
			method: "POST",
			data: data
		}).done(function (data) {

			//VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
			if (data != "error") {
				$("#btnAgregar").attr('disabled', true);
				window.location.href = '/DeduccionesExtraordinarias/Index';
				// Mensaje de exito cuando un registro se ha guardado bien
				iziToast.success({
					title: 'Éxito',
					message: '¡El registro se agregó de forma exitosa!',
				});
			}
			else {
				iziToast.error({
					title: 'Error',
					message: '¡No se guardó el registro, contacte al administrador!',
				});
			}
		});

	}
	// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
	$("#frmCreate").submit(function (e) {
		return false;
	});
	$("#btnAgregar").attr('disabled', false);
});
//#endregion

//#region Editar
//Editar
$("#btnEditar").click(function () {
	$("#btnEditar").attr('disabled', true);
	if (validaciones(equipoEmpIdEdit,
		montoInicial,
		montoRestante,
		observaciones,
		idDeduccion,
		cuota
	) === true) {
		if ($('#dex_DeducirISR').is(':checked')) {
			dex_DeducirISR = true;
		}
		else {
			dex_DeducirISR = false;
		}
		var data = $("#frmEditar").serializeArray();
		data[3].value = data[3].value.replace(/,/g, '');
		data[4].value = data[4].value.replace(/,/g, '');
		data[7].value = data[7].value.replace(/,/g, '');
		if ($('#dex_DeducirISR').is(':checked')) {
			dex_DeducirISR = true;
			data[8].value = dex_DeducirISR;
		}
		else {
			dex_DeducirISR = false;
		}

		//ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
		$.ajax({
			url: "/DeduccionesExtraordinarias/Edit",
			method: "POST",
			data: data
		}).done(function (data) {

			//VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
			if (data == "Exito") {
				$("#btnEditar").attr('disabled', true);
				window.location.href = '/DeduccionesExtraordinarias/Index';
				// Mensaje de exito cuando un registro se ha guardado bien
				iziToast.success({
					title: 'Exito',
					message: '¡El registro se editó de forma exitosa!',
				});
			}
			else {
				iziToast.error({
					title: 'Error',
					message: '¡No se editó el registro, contacte al administrador!',
				});
			}

		});
		// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
	}
	$("#frmEditar").submit(function (e) {
		return false;
	});
	$('#btnEditar').prop('disabled', false);
});
//#endregion

//#region Inactivar

//VARIABLE GLOBAL DE INACTIVAR
var GB_Inactivar = 0;
//Modal de Inactivar
$(document).on("click", "#btnInactivarDeduccionesExtraordinarias", function () {
	//DESBLOQUEAR EL BOTON
	$("#btnInactivar").attr("disabled", false);
	var ID = $(this).data('id');
	GB_Inactivar = ID;
	//Mostrar el Modal
	$("#InactivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });

});


//Funcionamiento del Modal Inactivar
$("#btnInactivar").click(function () {

	//BLOQUEAR EL BOTON
	$("#btnInactivar").attr("disabled", true);

	//Se envia el Formato Json al Controlador para realizar la Inactivación
	$.ajax({
		url: "/DeduccionesExtraordinarias/Inactivar/" + GB_Inactivar,
		method: "GET"
	}).done(function (data) {
		if (data == "Error") {
			//DESBLOQUEAR EL BOTON
			$("#btnInactivar").attr("disabled", false);
			//Cuando trae un error en el BackEnd al realizar la Inactivación
			iziToast.error({
				title: 'Error',
				message: '¡No se inactivó el registro, contacte al administrador!',
			});
		}
		else {
			// Actualizar el Index para ver los cambios
			$("#InactivarDeduccionesExtraordinarias").modal('hide');
			//REFRESCAR LA DATA DEL DATATABLE
			cargarGridDeducciones();
			//Mensaje de Éxito de la Inactivación
			iziToast.success({
				title: 'Éxito',
				message: '¡El registro se inactivó de forma exitosa!',
			});
		}
	});
	GB_Inactivar = 0;
});

// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#InactivarDeduccionesExtraordinarias").submit(function (e) {
	return false;
});

//#endregion

//#region Otras
//Ocultar Modal de Details
$("#btnCerrarDetails").click(function () {
	$("#DetailsDeduccionesExtraordinarias").modal('hide');
});

//Ocultar Modal de Edit
$("#btnCerrarEdit").click(function () {
	$("#EditarDeduccionesExtraordinarias").modal('hide');
});

//Ocultar Modal de Inactivar
$("#btnCerrarInactivar").click(function () {
	$("#InactivarDeduccionesExtraordinarias").modal('hide');
});
//#endregion

$(document).ready(function () {
	if (location.href.indexOf('Create') || location.href.indexOf('Edit'))
		$.ajax({
			url: "/DeduccionesExtraordinarias/EquipoEmpleadoGetDDL",
			method: "GET",
			dataType: "json",
			contentType: "application/json; charset=utf-8"
		}).done(function (data) {
			$('#eqem_Id').select2({
				placeholder: 'Seleccione un empleado',
				allowClear: true,
				language: {
					noResults: function () {
						return 'Resultados no encontrados.';
					},
					searching: function () {
						return 'Buscando...';
					}
				},
				data: data.results
			});
			let idEmpleado = localStorage.getItem('idEmpleado');
			if (idEmpleado == null || idEmpleado == "") {
				$("#eqem_Id").val('').trigger('change.select2');
			} else {
				$("#eqem_Id option[value='" + idEmpleado + "']").remove().trigger('change.select2');
				localStorage.removeItem('idEmpleado');
			}
		});
});