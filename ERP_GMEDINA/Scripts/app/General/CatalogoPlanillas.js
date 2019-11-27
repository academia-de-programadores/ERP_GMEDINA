//#region Variables
//Obtener las URL
var pathname = window.location.pathname + '/',
    URLactual = window.location.toString(),
    getUrl = window.location,
    baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1],
    urlProtocoloDominio = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : ''),
    table;//Almacenar la tabla

//Constantes
const btnGuardar = $('#btnGuardarCatalogoDePlanillasIngresosDeducciones'), //Boton para guardar el catalogo de planilla con sus detalles
    btnEditar = $('#btnEditarCatalogoDePlanillasIngresosDeducciones'), //Boton para editar editar el catalogo de planilla con sus detalles
    validacionDescripcionPlanilla = $('#validacionDescripcionPlanilla'), //Mensaje de validacion para la descripcion de la planilla
    validacionFrecuenciaDias = $('#validacionFrecuenciaDias'), //Mensaje de validación para la frecuencia en días
    validacionCatalogoIngresos = $('#catalogoDeIngresos #validacionCatalogoIngresos'), //Mensaje de validación de los ingresos
    htmlBody = $('html, body'), //Seleccionar el HTML y el body
    validacionCatalogoDeducciones = $('#catalogoDeDeducciones #validacionCatalogoDeducciones'), //Mensaje de validación de las deducciones
    inputDescripcionPlanilla = $('#cpla_DescripcionPlanilla'), //Seleccionar la descripción de la planilla
    inputFrecuenciaEnDias = $('#cpla_FrecuenciaEnDias'), //Seleccionar el campo de frecuencia en días
    inputIdPlanilla = $('form #cpla_IdPlanilla'), //Seleccionar el id de la planilla (esta oculto)
    cargandoCrear = $('#cargandoCrear'), //Div que aparecera cuando se le de click en crear
    cargandoEditar = $('#cargandoEditar'), //Div que aparecera cuando se de click en editar
    elementsSwitch = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
//#endregion

//#region Funciones
//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback, enviar) {
    $.ajax({
        url: uri,
        type: type,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
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
    var idPlanilla = inputIdPlanilla.val();

    //Obtener las lista del catalogo de ingresos
    listaCatalogoIngresos(arrayIngresos);

    //Obtener las lista del catalogo de deducciones
    listaCatalogoDeducciones(arrayDeducciones);

    //Insertar o editar
    if (verificarCampos(descripcionPlanilla, frecuenciaDias, arrayIngresos, arrayDeducciones)) {
        if (!edit) {
            mostrarCargandoCrear();

            _ajax({
                catalogoDePlanillas: [
                    descripcionPlanilla, frecuenciaDias
                ],
                catalogoIngresos: arrayIngresos,
                catalogoDeducciones: arrayDeducciones
            },
                "/CatalogoDePlanillas/Create",
                "POST",
                (data) => {
                    if (data == "bien") {
                        iziToast.success({
                            title: 'Exito',
                            message: 'El registro fue insertado de forma exitosa.',
                        });
                        location.href = baseUrl;
                    }
                    else {
                        iziToast.error({
                            title: 'Error',
                            message: 'Hubo un error al insertar el registro',
                        });

                        ocultarCargandoCrear();
                    }
                },
                enviar => { });
        }
        else {
            mostrarCargandoEditar();
            _ajax({
                id: idPlanilla,
                catalogoDePlanillas: [
                    descripcionPlanilla, frecuenciaDias
                ],
                catalogoIngresos: arrayIngresos,
                catalogoDeducciones: arrayDeducciones
            },
                "/CatalogoDePlanillas/Edit",
                "POST",
                (data) => {
                    if (data == "bien") {
                        iziToast.success({
                            title: 'Exito',
                            message: 'El registro fue editado de forma exitosa.',
                        });
                        location.href = baseUrl;
                    }
                    else {
                        iziToast.error({
                            title: 'Error',
                            message: 'Hubo un error al editar el registro',
                        });
                        ocultarCargandoEditar();
                    }
                },
                enviar => { });
        }
    }
}

function listaCatalogoDeducciones(arrayDeducciones) {
    $('#catalogoDeDeducciones input[type="checkbox"].i-checks').each(function (index, val) {
        //Obtener el atributo id del ckeckbox
        let checkIdDedu = $(val).attr('id');
        //Separar el id por el caracter "-"
        let arrDedu = checkIdDedu.split('-');
        //Obtener el id del checkbox para identificar el id de los ingresos a guardar
        let currentCheckboxIdDedu = arrDedu[1];
        //Ver si esta chequeado o no para guardar solo los que esten chequeados
        let isCheckedDedu = $('#catalogoDeDeducciones #check-' + currentCheckboxIdDedu).is(':checked', true);
        //Agregar a la lista de
        if (isCheckedDedu === true) {
            arrayDeducciones.push(currentCheckboxIdDedu);
        }
    });
}

function listaCatalogoIngresos(arrayIngresos) {
    $('#catalogoDeIngresos input[type="checkbox"].i-checks').each(function (index, val) {
        //Obtener el atributo id del ckeckbox
        let checkId = $(val).attr('id');
        //Separar el id por el caracter "-"
        let arr = checkId.split('-');
        //Obtener el id del checkbox para identificar el id de los ingresos a guardar
        let currentCheckboxIdIngreso = arr[1];
        //Ver si esta chequeado o no para guardar solo los que esten chequeados
        let isChecked = $('#catalogoDeIngresos #check-' + currentCheckboxIdIngreso).is(':checked', true);

        //Agregar a la lista de ingresos
        if (isChecked) {
            arrayIngresos.push(currentCheckboxIdIngreso);
        }
    });
}

function listaCatalogoDeduccionesFalse() {
    var hayUnoFalso = true;
    $('#catalogoDeDeducciones input[type="checkbox"].i-checks').each(function (index, val) {
        //Obtener el atributo id del ckeckbox
        let checkIdDedu = $(val).attr('id');
        //Separar el id por el caracter "-"
        let arrDedu = checkIdDedu.split('-');
        //Obtener el id del checkbox para identificar el id de los ingresos a guardar
        let currentCheckboxIdDedu = arrDedu[1];
        //Ver si esta chequeado o no para guardar solo los que esten chequeados
        let isCheckedDedu = $('#catalogoDeDeducciones #check-' + currentCheckboxIdDedu).prop("checked");
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
    $('#catalogoDeIngresos input[type="checkbox"].i-checks').each(function (index, val) {
        //Obtener el atributo id del ckeckbox
        let checkId = $(val).attr('id');
        //Separar el id por el caracter "-"
        let arr = checkId.split('-');
        //Obtener el id del checkbox para identificar el id de los ingresos a guardar
        let currentCheckboxIdIngreso = arr[1];
        //Ver si esta chequeado o no para guardar solo los que esten chequeados
        let isChecked = $('#catalogoDeIngresos #check-' + currentCheckboxIdIngreso).prop("checked");

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
             </div>`
}

function estaEnCrear() {
    return getUrl.toString().indexOf('Create');
}

function estaEnEditar() {
    return getUrl.toString().indexOf('Edit');
}

//Posicionaarse en la parte superior de la pagina cuando falle una validación
function scrollArriba() {
    htmlBody.animate({ scrollTop: 60 }, 300);
}

function ocultarCargandoEditar() {
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

function mostrarCargandoEditar() {
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

//Para editar o insertar utilizare esta función, para validar los campos
function verificarCampos(descripcionPlanilla, frecuenciaDias, catalogoIngresos, catalogoDeducciones) {
    var todoBien = true;
    //Validar que la descripción este bien
    if (descripcionPlanilla.trim() == "") {
        scrollArriba();
        validacionDescripcionPlanilla.show();
        todoBien = false;
    } else
        validacionDescripcionPlanilla.hide();
    //Validar que la frecuencia en días esté bien
    if (frecuenciaDias.trim() == "" || parseInt(frecuenciaDias) <= 0) {
        scrollArriba();
        validacionFrecuenciaDias.show();
        todoBien = false;
    } else
        validacionFrecuenciaDias.hide();

    //Validar que se haya seleccionado por lo menos un ingreso
    if (catalogoIngresos.length == 0) {
        scrollArriba();
        validacionCatalogoIngresos.parent().show();
        todoBien = false;
    } else {
        validacionCatalogoIngresos.parent().hide();
    }

    //Validar qeu por lo meno se halla seleccionado una deducción
    if (catalogoDeducciones.length == 0) {
        scrollArriba();
        validacionCatalogoDeducciones.parent().show();
        todoBien = false;
    } else
        validacionCatalogoDeducciones.parent().hide();

    return todoBien;
}

//Datatables
function listar() {
    //Almacenar la tabla creada
    table = $('.dataTables-example').DataTable({//Con este metodo se le dan los estilos y funcionalidades de datatable a la tabla
        'destroy': true, //Es para que pueda volver a inicializar el datatable, aunque ya este creado
        "ajax": { //Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
            'method': 'GET',
            'url': 'CatalogoDePlanillas/getPlanilla',
            'contentType': 'application/json; charset=utf-8',
            'dataType': 'json'
        },
        'columns': [
            {//Columna 1: el boton de desplegar
                "orderable": false,
                "className": 'details-control', //Estos estilos estan en: Content/app/General
                "data": null,
                "defaultContent": ''
            },
            { 'data': 'descripcionPlanilla' }, //Columna 2: descripción de la planilla, esto viene de la petición que se hizo al servidor
            { 'data': 'frecuenciaDias' }, //Columna 3: frecuencia en días de la planilla, esto viene de la petición que se hizo al servidor
            {//Columna 4: los botones que tendrá cada fila, editar y detalles de la planilla
                "orderable": false,
                "data": null,
                'defaultContent':
                    `
                        <button type="button" class="btn btn-primary btn-xs" id="btnEditarCatalogoDeducciones">Editar</button>
                        <button type="button" class="btn btn-default btn-xs" id="btnDetalleCatalogoDeducciones">Detalle</button>
                    `
            }
        ],
        "language": {
            "sProcessing": spinner(),
            "sLengthMenu": "Mostrar _MENU_ registros",
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
                "sNext": '<img src = "' + urlProtocoloDominio + '/Content/img/flecha-derecha.svg' + '" width = "25px" height = "15px"/>',
                "sPrevious": '<img src = "' + urlProtocoloDominio + '/Content/img/flecha-hacia-la-izquierda.svg' + '" width = "25px" height = "15px"/>'
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },//Con esto se hace la traducción al español del datatables
        responsive: false,
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp', //Darle los elementos del DOM que deseo
        buttons: [ //Poner los botones que quiero que aparezcan
            {
                extend: 'copy',
                title: 'Catalogo de Planillas',
                exportOptions: {
                    columns: [1, 2]
                }
            },
            {
                extend: 'csv',
                title: 'Catalogo de Planillas',
                exportOptions: {
                    columns: [1, 2]
                }
            },
            {
                extend: 'excelHtml5',
                title: 'Catalogo de Planillas',
                exportOptions: {
                    columns: [1, 2]
                }
            },
            {
                extend: 'pdfHtml5',
                title: 'Catalogo de Planillas',
                exportOptions: {
                    columns: [1, 2]
                }
            },
            {
                extend: 'print',
                title: 'Catalogo de Planillas',
                exportOptions: {
                    columns: [1, 2]
                },
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]
    });
    //Cuando le de click en detalles, o editar, le pasare el id
    obtenerIdDetallesEditar('#tblCatalogoPlanillas tbody', table);
}

//Redireccionar a Edit o Details
function obtenerIdDetallesEditar(tbody, table) {
    //Validar bien que la URL esté bien
    if (pathname == '//')
        pathname = baseUrl;

    if (pathname.toString().indexOf('CatalogoDePlanillas') < 1)
        pathname += 'CatalogoDePlanillas/';

    //Cuando de click en editar, que obtenga el id del tr, y que redireccione a la pantalla de Edit
    $(tbody).on('click', 'button#btnEditarCatalogoDeducciones', function () {
        var data = table.row($(this).parents('tr')).data();
        location.href = pathname + 'Edit/' + data.idPlanilla;
    });

    //Cuando de click en detalles, que obtenga el id del tr, y que redireccione a la pantalla de Details
    $(tbody).on('click', 'button#btnDetalleCatalogoDeducciones', function () {
        var data = table.row($(this).parents('tr')).data();
        location.href = pathname + 'Details/' + data.idPlanilla;
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
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Ingreso</th>
                    </tr>
                </thead>
                <tbody>`;
    $.each(data.ingresos, function (index, val) {
        ingresosPlanillas += `
                        <tr>
                            <td>
                                `+ val.cin_DescripcionIngreso + `
                            </td>
                        </tr>
                    `
    });
    ingresosPlanillas += `</tbody>
            </table>
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
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Deducción</th>
                    </tr>
                </thead>
                <tbody>`;
    $.each(data.deducciones, function (index, val) {
        deduccionesPlanilla += `
                        <tr>
                            <td>
                                `+ val.cde_DescripcionDeduccion + `
                            </td>
                        </tr>
                    `
    });
    deduccionesPlanilla += `</tbody>
            </table>
        </div>
    </div>`;

    return deduccionesPlanilla;
}

//Para desplegar los detalles de la planilla
function obtenerDetalles(id, handleData) {
    _ajax(null,
        '/CatalogoDePlanillas/getDeduccionIngresos/' + id,
        'GET',
        data => handleData(data),
        () => { }
    );
}
//#endregion

$(document).ready(() => {
    //Validar que no haya un /Index en la URL, si no falla el AJAX
    let ubicacionIndexUrl = URLactual.indexOf('/Index');
    let urlConSlash = URLactual.indexOf('CatalogoDePlanillas/');

    if (ubicacionIndexUrl > 0) {
        location.href = URLactual.replace('/Index', '');
    }

    if (estaEnCrear() < 1 && estaEnEditar() < 1) {
        listar();
    } else {
        const catalogoIngresosInputs = $("#catalogoDeIngresos input.i-checks");
        const catalogoDeduccionesInputs = $("#catalogoDeDeducciones input.i-checks");

        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
        });

        if (listaCatalogoIngresosFalse()) {
            $('#checkSeleccionarTodosIngresos').prop('checked', true);
        }

        if (listaCatalogoDeduccionesFalse()) {
            $('#checkSeleccionarTodasDeducciones').prop('checked', true);
        }

        elementsSwitch.forEach(function (html) {
            var switchery = new Switchery(html,
                {
                    color: '#18a689',
                    jackColor: '#fff',
                    size: 'small',
                    disabled: true
                });
        });

        var catalogoIngresosChangeCheckbox = document.querySelector('#catalogoDeIngresos .js-check-change');
        var catalogoDeduccionesChangeCheckbox = document.querySelector('#catalogoDeDeducciones .js-check-change');

        catalogoIngresosChangeCheckbox.onchange = function () {
            const seleccionarTodosLosIngresos = $('#seleccionarTodosLosIngresos');
            let seleccionarDeseleccionar = seleccionarTodosLosIngresos.html();
            if (catalogoIngresosChangeCheckbox.checked) {
                catalogoIngresosInputs.iCheck('check');
                seleccionarTodosLosIngresos.html(seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar'));
            } else {
                catalogoIngresosInputs.iCheck('uncheck');
                seleccionarTodosLosIngresos.html(seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar'));
            }
        };

        catalogoDeduccionesChangeCheckbox.onchange = function () {
            const seleccionarTodasLasDeducciones = $('#seleccionarTodasLasDeducciones');
            let seleccionarDeseleccionar = seleccionarTodasLasDeducciones.html();
            if (catalogoDeduccionesChangeCheckbox.checked) {
                catalogoDeduccionesInputs.iCheck('check');
                seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Seleccionar', 'Deseleccionar'));
            } else {
                seleccionarTodasLasDeducciones.html(seleccionarDeseleccionar.replace('Deseleccionar', 'Seleccionar'));
                catalogoDeduccionesInputs.iCheck('uncheck');
            }
        };
    }

    // Si esta en la pantalla de Create entonces vaciar todo
    if (estaEnCrear() > 1) {
        $('input[type="checkbox"]').prop("checked", false);
        inputDescripcionPlanilla.val('');
        inputFrecuenciaEnDias.val('')
    }

    //Validar la descripción de la planilla cuando se salga del input
    inputDescripcionPlanilla.blur(function () {
        if ($(this).val().trim() != "") {
            validacionDescripcionPlanilla.hide();
        } else {
            validacionDescripcionPlanilla.show();
        }
    });

    //Validar la frecuencia en dias cuando se salga del input
    inputFrecuenciaEnDias.blur(function () {
        if (inputFrecuenciaEnDias.val().trim() != "" && inputFrecuenciaEnDias.val() != "0" && inputFrecuenciaEnDias.val() > 0) {
            validacionFrecuenciaDias.hide();
        } else {
            validacionFrecuenciaDias.show();
        }
    });


});

//#region CRUD
//Cuando de click en el botón de detalles
$(document).on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = table.row(tr);

    //Que cierre el detalle
    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    } else { //Que desplegue el detalle
        row.child([spinner()]).show();
        tr.addClass('shown');

        // Obtener los datos para el detalle
        obtenerDetalles(row.data().idPlanilla, (data) => {
            //Mostrar el detalle con sus datos
            row.child([getIngresos(data) + getDeducciones(data)]).show();
            tr.addClass('shown');
        },
            () => { });
    }
});

//Insetar
$(document).on('click', '#btnGuardarCatalogoDePlanillasIngresosDeducciones', function () {
    crearEditar();
});

//Editar
$(document).on('click', '#btnEditarCatalogoDePlanillasIngresosDeducciones', function () {
    crearEditar(true);
});

//Inactivar
$('#inactivar').click(() => {
    $('#InactivarCatalogoDeducciones').modal();
});

$('#InactivarCatalogoDeducciones #btnInactivarPlanilla').click(() => {
    var id = inputIdPlanilla.val();
    _ajax({ id: id },
        '/CatalogoDePlanillas/Delete',
        'POST',
        (data) => {
            if (data == "bien") {
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue inactivado de forma exitosa.',
                });
                location.href = baseUrl;
            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'Ocurrió un error',
                });
            }
        },
        enviar => { });
});
//#endregion