//#region Variables
//Obtener las URL
var pathname = window.location.pathname + '/',
	URLactual = window.location.toString(),
	getUrl = window.location,
	baseUrl =
		getUrl.protocol + '//' + getUrl.host + '/' + getUrl.pathname.split('/')[1],
	urlProtocoloDominio =
		location.protocol +
		'//' +
		location.hostname +
		(location.port ? ':' + location.port : ''),
	table,
	table2,
	checkSeleccionarTodasLasDeducciones = $('#checkSeleccionarTodasDeducciones'); //Almacenar la tabla

//Constantes
const btnGuardar = $('#btnGuardarCatalogoDePlanillasIngresosDeducciones'), //Boton para guardar el catalogo de planilla con sus detalles
	btnEditar = $('#btnEditarCatalogoDePlanillasIngresosDeducciones'), //Boton para editar editar el catalogo de planilla con sus detalles
	validacionDescripcionPlanilla = $('#validacionDescripcionPlanilla'), //Mensaje de validacion para la descripcion de la planilla
	asteriscoDescripcionPlanilla = $('#asteriscoDescripcion'),
	asteriscoFrecuenciaPago = $('#asteriscoFrecuenciaPago'),
	validacionFrecuenciaDias = $('#validacionFrecuenciaDias'), //Mensaje de validación para la frecuencia en días
	validacionCatalogoIngresos = $(
		'#catalogoDeIngresos #validacionCatalogoIngresos'
	), //Mensaje de validación de los ingresos
	htmlBody = $('html, body'), //Seleccionar el HTML y el body
	validacionCatalogoDeducciones = $(
		'#catalogoDeDeducciones #validacionCatalogoDeducciones'
	), //Mensaje de validación de las deducciones
	inputDescripcionPlanilla = $('#cpla_DescripcionPlanilla'), //Seleccionar la descripción de la planilla
	inputFrecuenciaEnDias = $('#cpla_FrecuenciaEnDias'), //Seleccionar el campo de frecuencia en días
	inputIdPlanilla = $('form #cpla_IdPlanilla'), //Seleccionar el id de la planilla (esta oculto)
	cargandoCrear = $('#cargandoCrear'), //Div que aparecera cuando se le de click en crear
	cargandoEditar = $('#cargandoEditar'), //Div que aparecera cuando se de click en editar
	cargandoEliminar = $('#cargandoEliminar'), //Div que aparecera cuando se de click en eliminar
	elementsSwitch = Array.prototype.slice.call(
		document.querySelectorAll('.js-switch')
	),
	checkRecibeComision = $('#check-recibe-comision')[0];
//#endregion

//#region Funciones
//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback, enviar) {
	$.ajax({
		url: uri,
		type: type,
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		data: JSON.stringify(params),
		beforeSend: function () {
			enviar();
		},
		success: function (data) {
			callback(data);
		}
	});
}

// Funcion para crear y editar
var crearEditar = function (edit) {
	//Array de Ingresos y deducciones
	var arrayIngresos = [];
	var arrayDeducciones = [];
	//Obtener los valores del catalogo de planillas
	var descripcionPlanilla = inputDescripcionPlanilla.val();
	var frecuenciaDias = inputFrecuenciaEnDias.val();

	//Obtener las lista del catalogo de ingresos
	listaCatalogoIngresos(arrayIngresos);

	//Obtener las lista del catalogo de deducciones
	listaCatalogoDeducciones(arrayDeducciones);


	if (!edit) {
		mostrarSpinner($('#btnGuardarCatalogoDePlanillasIngresosDeducciones'), cargandoCrear);

		_ajax(
			{
				catalogoDePlanillas: [descripcionPlanilla, frecuenciaDias],
				catalogoIngresos: arrayIngresos,
				catalogoDeducciones: arrayDeducciones,
				checkRecibeComision: checkRecibeComision.checked
			},
			'/CatalogoDePlanillas/Create',
			'POST',
			(data) => {
				if (data == 'bien') {
					iziToast.success({
						title: 'Exito',
						message: '¡El registro se agregó de forma exitosa!'
					});
					location.href = baseUrl;
				} else {
					iziToast.error({
						title: 'Error',
						message: 'No se guardó el registro, contacte al administrador'
					});

					ocultarSpinner($('#btnGuardarCatalogoDePlanillasIngresosDeducciones'), cargandoCrear);
				}
			},
			(enviar) => { }
		);
	} else {
		let idPlanilla = inputIdPlanilla.val();
		mostrarSpinner($('#btnConfirmacionEdit'), cargandoEditar);
		_ajax(
			{
				id: idPlanilla,
				catalogoDePlanillas: [descripcionPlanilla, frecuenciaDias],
				catalogoIngresos: arrayIngresos,
				catalogoDeducciones: arrayDeducciones,
				checkRecibeComision: checkRecibeComision.checked
			},
			'/CatalogoDePlanillas/Edit',
			'POST',
			(data) => {
				if (data == 'bien') {
					iziToast.success({
						title: 'Exito',
						message: '¡El registro se editó de forma exitosa!'
					});
					location.href = baseUrl;
				} else {
					iziToast.error({
						title: 'Error',
						message: 'No se editó el registro, contacte al administrador'
					});
					ocultarSpinnerSpinner($('#btnConfirmacionEdit'), cargandoEditar);
				}
			},
			(enviar) => { }
		);
	}
};

function listaCatalogoDeducciones(arrayDeducciones) {
	$('#catalogoDeDeducciones #tblCatalogoDeducciones tbody tr td input[type="checkbox"].i-checks').each(function (
		index,
		val
	) {
		//Obtener el atributo id del ckeckbox
		let checkIdDedu = $(val).attr('id');
		//Separar el id por el caracter "-"
		let arrDedu = checkIdDedu.split('-');
		//Obtener el id del checkbox para identificar el id de los ingresos a guardar
		let currentCheckboxIdDedu = arrDedu[1];
		//Ver si esta chequeado o no para guardar solo los que esten chequeados
		let isCheckedDedu = $(
			'#catalogoDeDeducciones #tblCatalogoDeducciones tbody tr td #check-' + currentCheckboxIdDedu
		).is(':checked', true);
		//Agregar a la lista de
		if (isCheckedDedu === true) {
			arrayDeducciones.push(currentCheckboxIdDedu);
		}
	});
}

function listaCatalogoIngresos(arrayIngresos) {
	$('#catalogoDeIngresos input[type="checkbox"].i-checks').each(function (
		index,
		val
	) {
		//Obtener el atributo id del ckeckbox
		let checkId = $(val).attr('id');
		//Separar el id por el caracter "-"
		let arr = checkId.split('-');
		//Obtener el id del checkbox para identificar el id de los ingresos a guardar
		let currentCheckboxIdIngreso = arr[1];
		//Ver si esta chequeado o no para guardar solo los que esten chequeados
		let isChecked = $(
			'#catalogoDeIngresos #check-' + currentCheckboxIdIngreso
		).is(':checked', true);

		//Agregar a la lista de ingresos
		if (isChecked) {
			arrayIngresos.push(currentCheckboxIdIngreso);
		}
	});
}

function listaCatalogoDeduccionesFalse() {
	var hayUnoFalso = true;
	$('#catalogoDeDeducciones table tbody tr td input[type="checkbox"].i-checks').each(function (
		index,
		val
	) {
		//Obtener el atributo id del ckeckbox
		let checkIdDedu = $(val).attr('id');
		//Separar el id por el caracter "-"
		let arrDedu = checkIdDedu.split('-');
		//Obtener el id del checkbox para identificar el id de los ingresos a guardar
		let currentCheckboxIdDedu = arrDedu[1];
		//Ver si esta chequeado o no para guardar solo los que esten chequeados
		let isCheckedDedu = $(
			'#catalogoDeDeducciones tbody tr td input#check-' + currentCheckboxIdDedu
		).prop('checked');
		//Agregar a la lista de
		if (!isCheckedDedu) {
			hayUnoFalso = false;
			return;
		}
	});
	return hayUnoFalso;
}

function listaCatalogoIngresosFalse() {
	var hayUnoFalso = true;
	$('#catalogoDeIngresos table tbody tr td input[type="checkbox"].i-checks').each(function (
		index,
		val
	) {
		//Obtener el atributo id del ckeckbox
		let checkId = $(val).attr('id');
		//Separar el id por el caracter "-"
		let arr = checkId.split('-');
		//Obtener el id del checkbox para identificar el id de los ingresos a guardar
		let currentCheckboxIdIngreso = arr[1];
		//Ver si esta chequeado o no para guardar solo los que esten chequeados
		let isChecked = $(
			'#catalogoDeIngresos tbody tr td input#check-' + currentCheckboxIdIngreso
		).prop('checked');

		//Agregar a la lista de ingresos
		if (!isChecked) {
			hayUnoFalso = false;
			return;
		}
	});

	return hayUnoFalso;
}

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

function estaEnCrear() {
	return getUrl.toString().indexOf('Create');
}

function estaEnEditar() {
	return getUrl.toString().indexOf('Edit');
}

function estaEnDetalles() {
	return getUrl.toString().indexOf('Details');
}


function estaEnIndex() {
	return getUrl.toString().indexOf('Index');
}

//Posicionaarse en la parte superior de la pagina cuando falle una validación
function scrollArriba() {
	htmlBody.animate({ scrollTop: 60 }, 300);
}

function mostrarSpinner(btn, div) {
	btn.hide();
	div.html(spinner());
	div.show();
}

function ocultarSpinner(btn, div) {
	btn.show();
	div.html('');
	div.hide();
}

//Para editar o insertar utilizare esta función, para validar los campos
function verificarCampos(
	descripcionPlanilla,
	frecuenciaDias,
	catalogoIngresos,
	catalogoDeducciones
) {
	var todoBien = true;
	//Validar que la descripción este bien
	if (descripcionPlanilla.trim() == '') {
		scrollArriba();
		validacionDescripcionPlanilla.show();
		inputDescripcionPlanilla.focus();
		asteriscoDescripcionPlanilla.addClass('text-danger');
		todoBien = false;
	} else {
		asteriscoDescripcionPlanilla.removeClass('text-danger');
		validacionDescripcionPlanilla.hide();
	}
	//Validar que la frecuencia en días esté bien
	if (frecuenciaDias == null || frecuenciaDias.trim() == '' || parseInt(frecuenciaDias) <= 0) {
		scrollArriba();
		validacionFrecuenciaDias.show();
		if (todoBien) inputFrecuenciaEnDias.focus();
		asteriscoFrecuenciaPago.addClass('text-danger');
		todoBien = false;
	} else {
		asteriscoFrecuenciaPago.removeClass('text-danger');
		validacionFrecuenciaDias.hide();
	}

	//Validar que se haya seleccionado por lo menos un ingreso
	if (catalogoIngresos.length == 0) {
		scrollArriba();
		validacionCatalogoIngresos.show();
		validacionCatalogoIngresos.parent().show();
		todoBien = false;
	} else {
		validacionCatalogoIngresos.parent().hide();
	}

	//Validar qeu por lo meno se halla seleccionado una deducción
	if (catalogoDeducciones.length == 0 && !$('#noAplica').is(':checked', true)) {
		scrollArriba();
		validacionCatalogoDeducciones.parent().show();
		todoBien = false;
	} else validacionCatalogoDeducciones.parent().hide();

	return todoBien;
}

const Activar = `
							<button type="button" class="btn btn-primary btn-xs" id="btnActivar">Activar</button>
							`;
const DetallesEditar = `
							<button type="button" class="btn btn-primary btn-xs" id="btnDetalleCatalogoDeducciones">Detalles</button>
							<button type="button" class="btn btn-default btn-xs" id="btnEditarCatalogoDeducciones">Editar</button>
							`;
//Datatables

//Index
function listar() {
	//Almacenar la tabla creada
	table = $('.dataTables-example').DataTable({
		//Con este metodo se le dan los estilos y funcionalidades de datatable a la tabla
		destroy: true, //Es para que pueda volver a inicializar el datatable, aunque ya este creado
		ajax: {
			//Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
			method: 'GET',
			url: 'CatalogoDePlanillas/getPlanilla',
			contentType: 'application/json; charset=utf-8',
			dataType: 'json'
		},
		// stateSave: true,
		// "scrollY": "400px",
		// "scrollCollapse": true,
		//"pagingType": "full_numbers",
		columns: [
			{
				//Columna 1: el boton de desplegar
				orderable: false,
				bSort: false,
				className: 'details-control', //Estos estilos estan en: Content/app/General
				data: null,
				defaultContent: ''
			},
			{
				data: 'idPlanilla'
			},
			{ data: 'descripcionPlanilla' }, //Columna 2: descripción de la planilla, esto viene de la petición que se hizo al servidor
			{ data: 'frecuenciaDias' }, //Columna 3: frecuencia en días de la planilla, esto viene de la petición que se hizo al servidor
			{
				data: 'recibeComision'
			},
			{
				data: 'activoAdmin',
				render: function (data) {
					return (data.activo) ? "Activo" : "Inactivo";
				}
			},
			{
				//Columna 4: los botones que tendrá cada fila, editar y detalles de la planilla
				orderable: false,
				data: 'activoAdmin',
				render: function (data) {
					if (!data.activo && data.esAdmin) {
						return Activar;
					}
					else if (data.activo && data.esAdmin) {
						return DetallesEditar;
					}
					else if (!data.activo && !data.esAdmin) {
						return '';
					}
					else {
						return DetallesEditar;
					}
				}
			}
		],
		language: {
			sProcessing: spinner(),
			sLengthMenu: 'Mostrar _MENU_ registros',
			sZeroRecords: 'No se encontraron resultados',
			sEmptyTable: 'No se cargó la información, contacte al administrador',
			sInfo:
				'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
			sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
			sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
			sInfoPostFix: '',
			sSearch: 'Buscar:',
			sUrl: '',
			sInfoThousands: ',',
			sLoadingRecords: spinner(),
			oPaginate: {
				sFirst: 'Primero',
				sLast: 'Último',
				sNext: 'Siguiente',
				sPrevious: 'Anterior'
			},
			oAria: {
				sSortAscending:
					': Activar para ordenar la columna de manera ascendente',
				sSortDescending:
					': Activar para ordenar la columna de manera descendente'
			}
		}, //Con esto se hace la traducción al español del datatables
		responsive: false,
		pageLength: 10,
		dom: '<"html5buttons"B>lTfgtpi',
		buttons: [
			{
				extend: 'copy',
				text: '<i class="fa fa-copy btn-xs"></i>',
				titleAttr: 'Copiar',
				exportOptions: {
					columns: [1, 2, 3, 4],
				},
				className: 'btn btn-primary'
			}
		]
	});
	//Cuando le de click en detalles, o editar, le pasare el id
	obtenerIdDetallesEditar('#tblCatalogoPlanillas tbody', table);
}

const idiomaEspaniolCatalogos = {
	"paging": false,
	"sProcessing": spinner(),
	"sLengthMenu": "_MENU_",
	"sZeroRecords": "No se encontraron resultados",
	"sEmptyTable": "Ningún dato disponible en esta tabla",
	"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
	"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
	"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
	"sInfoPostFix": "",
	"sSearch": "Buscar:",
	"sUrl": "",
	"sInfoThousands": ",",
	"sLoadingRecords": spinner(),
	"oPaginate": {
		"sFirst": "Primero",
		"sLast": "Último",
		"sNext": "Siguiente",
		"sPrevious": "Anterior"
	},
	"oAria": {
		"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
		"sSortDescending": ": Activar para ordenar la columna de manera descendente"
	}
};

//Create o Edit
function listarCatalogos() {
	$('.i-checks, .i-checks-no-aplica').iCheck({
		checkboxClass: 'icheckbox_square-green',
		radioClass: 'iradio_square-green'
	});

	elementsSwitch.forEach(function (html) {
		var switchery = new Switchery(html, {
			color: '#18a689',
			jackColor: '#fff',
			size: 'small',
			disabled: true
		});
	});

	var urlFetchData = baseUrl + '/GetIngresosDeducciones?esCrear=';
	if (estaEnEditar() > 1) {
		urlFetchData += 'false&id=' + URLactual.substr((URLactual.search(/Edit/i) + 'Edit'.length + 1))

		//Ingresos
		table = $('.tbl-catalogos').DataTable({
			"language": {
				"paging": false,
				"sProcessing": spinner(),
				"sLengthMenu": "_MENU_",
				"sZeroRecords": "No se encontraron resultados",
				"sEmptyTable": "Ningún dato disponible en esta tabla",
				"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
				"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
				"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
				"sInfoPostFix": "",
				"sSearch": "Buscar:",
				"sUrl": "",
				"sInfoThousands": ",",
				"sLoadingRecords": spinner(),
				"oPaginate": {
					"sFirst": "Primero",
					"sLast": "Último",
					"sNext": "Siguiente",
					"sPrevious": "Anterior"
				},
				"oAria": {
					"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
					"sSortDescending": ": Activar para ordenar la columna de manera descendente"
				}
			},
			"paging": false,
			ajax: {
				//Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
				method: 'GET',
				url: urlFetchData,
				contentType: 'application/json; charset=utf-8',
				dataType: 'json'
			},
			responsive: false,
			dom: 'lft',
			"columns": [
				{
					"searchable": false,
					"orderable": true,
					data: 'checkId',
					render: function (data) {
						let check = (data.check) ? 'checked' : '';
						return `<input type="checkbox" class="i-checks" id="check-` + data.id + `" ` + check + ` />`;
					},
					className: '',
					defaultContent: ''
				},
				{
					data: 'descripcion'
				}
			],
			"order": [[1, "asc"]],
			initComplete: function (settings, json) {
				$('#tblCatalogoIngresos tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				$('#tblCatalogoIngresos tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				var catalogoIngresosChangeCheckbox = document.querySelector(
					'#catalogoDeIngresos .js-check-change'
				);
				const catalogoIngresosInputs = $('#tblCatalogoIngresos tbody tr td input.i-checks');

				//Seleccionar o deseleccionar los ingresos
				catalogoIngresosChangeCheckbox.onchange = function () {
					const seleccionarTodosLosIngresos = $('#seleccionarTodosLosIngresos');
					let seleccionarDeseleccionar = seleccionarTodosLosIngresos.html();
					if (catalogoIngresosChangeCheckbox.checked) {
						catalogoIngresosInputs.iCheck('check');
						seleccionarTodosLosIngresos.html(
							seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar')
						);
					} else {
						catalogoIngresosInputs.iCheck('uncheck');
						seleccionarTodosLosIngresos.html(
							seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar')
						);
					}
				};

				//Si estan todos los checkboxs seleccionados en catalogo de ingresos 
				//Se activara el switch, si esta desactivado
				$(catalogoIngresosInputs).on('ifChecked', () => {
					validacionCatalogoIngresos.hide();
					if (listaCatalogoIngresosFalse()) {
						if (!$('#checkSeleccionarTodosIngresos').is(':checked')) {
							$('#checkSeleccionarTodosIngresos').click();
						}
					}
				});

				//Activar switch seleccionar todos en ingresos
				if (listaCatalogoIngresosFalse()) {
					$('#checkSeleccionarTodosIngresos').click();
				}

			}
		});

		//Deducciones
		table2 = $('.tbl-catalogos-d').DataTable({
			"language": idiomaEspaniolCatalogos,
			"paging": false,
			ajax: {
				//Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
				method: 'GET',
				url: urlFetchData += '&esIngreso=false',
				contentType: 'application/json; charset=utf-8',
				dataType: 'json'
			},
			responsive: false,
			dom: 'lft',
			"columns": [
				{
					"searchable": false,
					"orderable": true,
					data: 'checkId',
					render: function (data) {
						let check = (data.check) ? 'checked' : '';
						return `<input type="checkbox" class="i-checks" id="check-` + data.id + `" ` + check + ` />`;
					},
					className: '',
					defaultContent: ''
				},
				{
					data: 'descripcion'
				}
			],
			"order": [[1, "asc"]],
			initComplete: function (settings, json) {

				$('#tblCatalogoDeducciones tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				const catalogoDeduccionesInputs = $(
					'#catalogoDeDeducciones #tblCatalogoDeducciones tbody tr td input.i-checks'
				);

				var catalogoDeduccionesChangeCheckbox = document.querySelector(
					'#catalogoDeDeducciones .js-check-change'
				);

				$('#noAplica').on('ifChecked', () => {
					catalogoDeduccionesInputs.iCheck('uncheck');
					const seleccionarTodasLasDeducciones = checkSeleccionarTodasLasDeducciones;
					if (seleccionarTodasLasDeducciones.is(':checked'))
						seleccionarTodasLasDeducciones.click();
				});

				$(catalogoDeduccionesInputs).on('ifChecked', () => {
					$('#noAplica').iCheck('uncheck');
					if (listaCatalogoDeduccionesFalse()) {
						if (!checkSeleccionarTodasLasDeducciones.is(':checked'))
							checkSeleccionarTodasLasDeducciones.click();
					}
				});

				catalogoDeduccionesChangeCheckbox.onchange = function () {
					const seleccionarTodasLasDeducciones = $('#seleccionarTodasLasDeducciones');
					let seleccionarDeseleccionar = seleccionarTodasLasDeducciones.html();
					if (catalogoDeduccionesChangeCheckbox.checked) {
						catalogoDeduccionesInputs.iCheck('check');
						seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar'));
					}
					else {
						seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar'));
						catalogoDeduccionesInputs.iCheck('uncheck');
					}
				};

				//Activar switch seleccionar todos en deducciones
				if (listaCatalogoDeduccionesFalse()) {
					checkSeleccionarTodasLasDeducciones.click();
				}
			}
		});
	} //crear
	else {
		//Ingresos
		table = $('.tbl-catalogos').DataTable({
			"language": idiomaEspaniolCatalogos,
			"paging": false,
			ajax: {
				//Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
				method: 'GET',
				url: urlFetchData,
				contentType: 'application/json; charset=utf-8',
				dataType: 'json'
			},
			responsive: false,
			dom: 'lft',
			"columns": [
				{
					"searchable": false,
					"orderable": true,
					data: 'id',
					render: function (data) {
						return `<input type="checkbox" class="i-checks" id="check-` + data + `" />`;
					},
					className: '',
					defaultContent: ''
				},
				{
					data: 'descripcion'
				}
			],
			"order": [[1, "asc"]],
			initComplete: function (settings, json) {
			
				$('#tblCatalogoIngresos tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				$('#tblCatalogoIngresos tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				var catalogoIngresosChangeCheckbox = document.querySelector(
					'#catalogoDeIngresos .js-check-change'
				);
				const catalogoIngresosInputs = $('#tblCatalogoIngresos tbody tr td input.i-checks');

				//Seleccionar o deseleccionar los ingresos
				catalogoIngresosChangeCheckbox.onchange = function () {
					const seleccionarTodosLosIngresos = $('#seleccionarTodosLosIngresos');
					let seleccionarDeseleccionar = seleccionarTodosLosIngresos.html();
					if (catalogoIngresosChangeCheckbox.checked) {
						catalogoIngresosInputs.iCheck('check');
						seleccionarTodosLosIngresos.html(
							seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar')
						);
					} else {
						catalogoIngresosInputs.iCheck('uncheck');
						seleccionarTodosLosIngresos.html(
							seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar')
						);
					}
				};

				//Si estan todos los checkboxs seleccionados en catalogo de ingresos 
				//Se activara el switch, si esta desactivado
				$(catalogoIngresosInputs).on('ifChecked', () => {
					validacionCatalogoIngresos.hide();
					if (listaCatalogoIngresosFalse()) {
						if (!$('#checkSeleccionarTodosIngresos').is(':checked')) {
							$('#checkSeleccionarTodosIngresos').click();
						}
					}
				});

				//Activar switch seleccionar todos en ingresos
				if (listaCatalogoIngresosFalse()) {
					$('#checkSeleccionarTodosIngresos').click();
				}

			}
		});

		//Deducciones
		table2 = $('.tbl-catalogos-d').DataTable({
			"language": idiomaEspaniolCatalogos,
			"paging": false,
			ajax: {
				//Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
				method: 'GET',
				url: urlFetchData += 'true&esIngreso=false',
				contentType: 'application/json; charset=utf-8',
				dataType: 'json'
			},
			responsive: false,
			dom: 'lft',
			"columns": [
				{
					"searchable": false,
					"orderable": true,
					data: 'id',
					render: function (data) {
						return `<input type="checkbox" class="i-checks" id="check-` + data + `" />`;
					},
					className: '',
					defaultContent: ''
				},
				{
					data: 'descripcion'
				}
			],
			"order": [[1, "asc"]],
			initComplete: function (settings, json) {

				$('#tblCatalogoDeducciones tbody tr td .i-checks').iCheck({
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green'
				});

				const catalogoDeduccionesInputs = $(
					'#catalogoDeDeducciones #tblCatalogoDeducciones tbody tr td input.i-checks'
				);

				var catalogoDeduccionesChangeCheckbox = document.querySelector(
					'#catalogoDeDeducciones .js-check-change'
				);

				$('#noAplica').on('ifChecked', () => {
					catalogoDeduccionesInputs.iCheck('uncheck');
					const seleccionarTodasLasDeducciones = checkSeleccionarTodasLasDeducciones;
					if (seleccionarTodasLasDeducciones.is(':checked'))
						seleccionarTodasLasDeducciones.click();
				});

				$(catalogoDeduccionesInputs).on('ifChecked', () => {
					validacionCatalogoDeducciones.hide();
					$('#noAplica').iCheck('uncheck');
					if (listaCatalogoDeduccionesFalse()) {
						if (!checkSeleccionarTodasLasDeducciones.is(':checked'))
							checkSeleccionarTodasLasDeducciones.click();
					}
				});

				catalogoDeduccionesChangeCheckbox.onchange = function () {
					const seleccionarTodasLasDeducciones = $('#seleccionarTodasLasDeducciones');
					let seleccionarDeseleccionar = seleccionarTodasLasDeducciones.html();
					if (catalogoDeduccionesChangeCheckbox.checked) {
						catalogoDeduccionesInputs.iCheck('check');
						seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar'));
					}
					else {
						seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar'));
						catalogoDeduccionesInputs.iCheck('uncheck');
					}
				};

				//Activar switch seleccionar todos en deducciones
				if (listaCatalogoDeduccionesFalse()) {
					checkSeleccionarTodasLasDeducciones.click();
				}
			}
		});
	}

	function seleccionarCheckbox_CatalogoIngresos() {

		var catalogoIngresosChangeCheckbox = document.querySelector(
			'#catalogoDeIngresos .js-check-change'
		);
		const catalogoIngresosInputs = $('#tblCatalogoIngresos tbody tr td input.i-checks');

		//Seleccionar o deseleccionar los ingresos
		catalogoIngresosChangeCheckbox.onchange = function () {
			const seleccionarTodosLosIngresos = $('#seleccionarTodosLosIngresos');
			let seleccionarDeseleccionar = seleccionarTodosLosIngresos.html();
			if (catalogoIngresosChangeCheckbox.checked) {
				catalogoIngresosInputs.iCheck('check');
				seleccionarTodosLosIngresos.html(
					seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar')
				);
			} else {
				catalogoIngresosInputs.iCheck('uncheck');
				seleccionarTodosLosIngresos.html(
					seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar')
				);
			}
		};

		//Si estan todos los checkboxs seleccionados en catalogo de ingresos 
		//Se activara el switch, si esta desactivado
		$(catalogoIngresosInputs).on('ifChecked', () => {
			if (listaCatalogoIngresosFalse()) {
				if (!$('#checkSeleccionarTodosIngresos').is(':checked'))
					$('#checkSeleccionarTodosIngresos').prop("checked", true);
			}
		});

		//Activar switch seleccionar todos en ingresos
		if (listaCatalogoIngresosFalse()) {
			$('#checkSeleccionarTodosIngresos').click();
		}
	}
}

//Redireccionar a Edit o Details
function obtenerIdDetallesEditar(tbody, table) {
	//Validar bien que la URL esté bien
	if (pathname == '//') pathname = baseUrl;

	if (pathname.toString().indexOf('CatalogoDePlanillas') < 1)
		pathname += 'CatalogoDePlanillas/';

	//Cuando de click en editar, que obtenga el id del tr, y que redireccione a la pantalla de Edit
	$(document).on('click', 'button#btnEditarCatalogoDeducciones', function () {
		var data = table.row($(this).parents('tr')).data();
		location.href = pathname + 'Edit/' + data.idPlanilla;
	});

	//Cuando de click en detalles, que obtenga el id del tr, y que redireccione a la pantalla de Details
	$(tbody).on('click', 'button#btnDetalleCatalogoDeducciones', function () {
		var data = table.row($(this).parents('tr')).data();
		location.href = pathname + 'Details/' + data.idPlanilla;
	});

	$(tbody).on('click', 'button#btnActivar', function () {
		localStorage.setItem('id', table.row($(this).parents('tr')).data().idPlanilla);
		localStorage.setItem('element', JSON.stringify($(this).parents('tr')));
		$('#frmActivarCatalogoPlanilla').modal();
	});
}

//Plantilla del detalle de ingresos de la planilla
function getIngresos(data) {
	var ingresosPlanillas = `
    <div class="col-lg-6">
       <div class="ibox-title">
            <h5>Ingresos de la Planilla</h5>
        </div>
		<div class="ibox-content">
		<div class="data-details" style="padding:5px">
            <table class="table table-bordered tbl-catalogos" id="child-`+ localStorage.getItem("idDatatableChild") + `">
                <thead>
                    <tr>
                        <th>Ingreso</th>
                    </tr>
                </thead>
                <tbody>`;
	$.each(data.ingresos, function (index, val) {
		ingresosPlanillas +=
			`
							<tr>
								<td>
									` +
			val.cin_DescripcionIngreso +
			`
								</td>
							</tr>
						`;
	});
	ingresosPlanillas += `</tbody>
				</table>
			</div>
        </div>
    </div>`;

	return ingresosPlanillas;
}

//Plantilla del detalle de deducciones de la planilla
function getDeducciones(data) {
	var deduccionesPlanilla = `
    <div class="col-lg-6">
       <div class="ibox-title">
            <h5>Deducciones de la Planilla</h5>
        </div>
		<div class="ibox-content">
		<div class="data-details" style="padding:5px">
            <table class="table table-bordered tbl-catalogos" id="child-`+ localStorage.getItem("idDatatableChild") + `">
                <thead>
                    <tr>
                        <th>Deducción</th>
                    </tr>
                </thead>
                <tbody>`;
	$.each(data.deducciones, function (index, val) {
		deduccionesPlanilla +=
			`
                        <tr>
                            <td>
                                ` +
			val.cde_DescripcionDeduccion +
			`
                            </td>
                        </tr>
                    `;
	});
	deduccionesPlanilla += `</tbody>
			</table>
			</div>
        </div>
    </div>`;

	return deduccionesPlanilla;
}

//Para desplegar los detalles de la planilla
function obtenerDetalles(id, handleData) {
	_ajax(
		null,
		'/CatalogoDePlanillas/getDeduccionIngresos/' + id,
		'GET',
		(data) => handleData(data),
		() => { }
	);
}
//#endregion

$(document).ready(() => {
	//Validar que no haya un /Index en la URL, si no falla el AJAX
	let ubicacionIndexUrl = URLactual.indexOf('/Index');

	if (ubicacionIndexUrl > 0) {
		location.href = URLactual.replace('/Index', '');
	}

	if (estaEnCrear() < 1 && estaEnEditar() < 1) {
		listar();
	} else {
		listarCatalogos();
	}

	if (estaEnDetalles() > 1) {

		$('.tbl-catalogos').DataTable({
			"language": {
				"paging": false,
				"sProcessing": spinner(),
				"sLengthMenu": "_MENU_",
				"sZeroRecords": "No se encontraron resultados",
				"sEmptyTable": "Ningún dato disponible en esta tabla",
				"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
				"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
				"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
				"sInfoPostFix": "",
				"sSearch": "&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;&nbsp; Buscar:</div> ",
				"sUrl": "",
				"sInfoThousands": ",",
				"sLoadingRecords": spinner(),
				"oPaginate": {
					"sFirst": "Primero",
					"sLast": "Último",
					"sNext": "Siguiente",
					"sPrevious": "Anterior"
				},
				"oAria": {
					"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
					"sSortDescending": ": Activar para ordenar la columna de manera descendente"
				}
			},
			"paging": false,
			responsive: false,
			dom: 'lft',
			"order": [[1, "asc"]]
		});
	}


	// Si esta en la pantalla de Create entonces vaciar todo
	if (estaEnCrear() > 1) {
		$('input[type="checkbox"]').prop('checked', false);
		inputDescripcionPlanilla.val('');
		inputFrecuenciaEnDias.val('');
	}

	//Validar la descripción de la planilla cuando se salga del input
	inputDescripcionPlanilla.blur(function () {
		if (
			$(this)
				.val()
				.trim() != ''
		) {
			validacionDescripcionPlanilla.hide();
			asteriscoDescripcionPlanilla.removeClass('text-danger');

		} else {
			asteriscoDescripcionPlanilla.addClass('text-danger');
			validacionDescripcionPlanilla.show();
		}
	});

	//Validar la frecuencia en dias cuando se salga del input
	inputFrecuenciaEnDias.blur(function () {
		if (
			inputFrecuenciaEnDias.val().trim() != '' &&
			inputFrecuenciaEnDias.val() != '0' &&
			inputFrecuenciaEnDias.val() > 0
		) {
			validacionFrecuenciaDias.hide();
			asteriscoFrecuenciaPago.removeClass('text-danger');
		} else {
			validacionFrecuenciaDias.show();
			asteriscoFrecuenciaPago.addClass('text-danger');
		}
	});
});

//#region CRUD
//Cuando de click en el botón de detalles
$(document).on('click', 'td.details-control', function () {
	var tr = $(this).closest('tr');
	var row = table.row(tr);
	var detail;
	//Que cierre el detalle
	if (row.child.isShown()) {
		// This row is already open - close it
		row.child.hide();
		tr.removeClass('shown');
	} else {
		//Que desplegue el detalle
		row.child([spinner()]).show();
		tr.addClass('shown');
		localStorage.setItem('idDatatableChild', row.data().idPlanilla)

		// Obtener los datos para el detalle
		obtenerDetalles(
			row.data().idPlanilla,
			(data) => {
				//Mostrar el detalle con sus datos
				row.child([getIngresos(data) + getDeducciones(data)]).show();
				tr.addClass('shown');
				detail = null;
				detail = new $('table tbody tr td #child-' + localStorage.getItem("idDatatableChild")).DataTable({
					destroy: true,
					"language": {
						"paging": false,
						"sProcessing": spinner(),
						"sLengthMenu": "_MENU_",
						"sZeroRecords": "No se encontraron resultados",
						"sEmptyTable": "Ningún dato disponible en esta tabla",
						"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
						"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
						"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
						"sInfoPostFix": "",
						"sSearch": "&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;&nbsp; Buscar: ",
						"sUrl": "",
						"sInfoThousands": ",",
						"sLoadingRecords": spinner(),
						"oPaginate": {
							"sFirst": "Primero",
							"sLast": "Último",
							"sNext": "Siguiente",
							"sPrevious": "Anterior"
						},
						"oAria": {
							"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
							"sSortDescending": ": Activar para ordenar la columna de manera descendente"
						}
					},
					"paging": true,
					responsive: false,
					dom: 'lftpi',
					"order": [[0, "asc"]],
					initComplete: function () {
					}
				});
			},
			() => { }
		);
	}
});

//Insetar
$(document).on(
	'click',
	'#btnGuardarCatalogoDePlanillasIngresosDeducciones',
	function () {
		table.search('').draw();
		table2.search('').draw();
		//Array de Ingresos y deducciones
		let arrayIngresos = [];
		let arrayDeducciones = [];
		//Obtener los valores del catalogo de planillas
		let descripcionPlanilla = inputDescripcionPlanilla.val();
		let frecuenciaDias = inputFrecuenciaEnDias.val();

		//Obtener las lista del catalogo de ingresos
		listaCatalogoIngresos(arrayIngresos);

		//Obtener las lista del catalogo de deducciones
		listaCatalogoDeducciones(arrayDeducciones);
		if (
			verificarCampos(
				descripcionPlanilla,
				frecuenciaDias,
				arrayIngresos,
				arrayDeducciones
			)
		) {
			crearEditar(false);
		}
	}
);

//Desplegar modal de que si desea editar
$('#btnEditarCatalogoDePlanillasIngresosDeducciones').click(function () {
	table.search('').draw();
	table2.search('').draw();
	//Array de Ingresos y deducciones
	let arrayIngresos = [];
	let arrayDeducciones = [];
	//Obtener los valores del catalogo de planillas
	let descripcionPlanilla = inputDescripcionPlanilla.val();
	let frecuenciaDias = inputFrecuenciaEnDias.val();

	//Obtener las lista del catalogo de ingresos
	listaCatalogoIngresos(arrayIngresos);

	//Obtener las lista del catalogo de deducciones
	listaCatalogoDeducciones(arrayDeducciones);

	//Insertar o editar
	if (
		verificarCampos(
			descripcionPlanilla,
			frecuenciaDias,
			arrayIngresos,
			arrayDeducciones
		)
	) {
		$('#modalConfirmacionEdit').modal();
	}

});

//Editar
$(document).on(
	'click',
	'#btnConfirmacionEdit',
	function () {
		crearEditar(true);
	}
);

//Inactivar
$('#inactivar').click(() => {
	$('#InactivarCatalogoDeducciones').modal();
});


$('#InactivarCatalogoDeducciones #btnInactivarPlanilla').click(() => {
	var id = inputIdPlanilla.val();
	mostrarSpinner($('#btnInactivarPlanilla'), cargandoEliminar);
	_ajax(
		{ id: id },
		'/CatalogoDePlanillas/Delete',
		'POST',
		(data) => {
			if (data == 'bien') {
				iziToast.success({
					title: 'Exito',
					message: '¡El registro se inactivó de forma exitosa!'
				});
				location.href = baseUrl;
			} else {
				iziToast.error({
					title: 'Error',
					message: 'No se inactivó el registro, contacte al administrador'
				});
			}
			ocultarSpinner($('#btnInactivarPlanilla'), cargandoEliminar);
		},
		(enviar) => { }
	);
});

$(document).on('click', '#btnActivarCatatalogoPlanilla', () => {
	let id = localStorage.getItem('id');
	mostrarSpinner($('#btnActivarCatatalogoPlanilla'), $('#cargandoActivar'));
	_ajax({ id: id },
		'/CatalogoDePlanillas/ActivarPlanilla',
		'POST',
		(data) => {
			if (data.response == 'bien') {
				iziToast.success({
					title: 'Éxito',
					message: '¡El registro se activó de forma exitosa!'
				});
				$('#frmActivarCatalogoPlanilla').modal('hide');

				table.clear();
				table.rows.add(data.data).draw();
			} else {
				iziToast.error({
					title: 'Éxito',
					message: 'No se activó el registro, contacte al administrador'
				});
			}
			ocultarSpinner($('#btnActivarCatatalogoPlanilla'), $('#cargandoActivar'));
		},
		() => {
		})
});
//#endregion
