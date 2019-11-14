//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback) {
    debugger;
    $.ajax({
        url: uri,
        type: type,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(params),
        success: function (data) {
            callback(data);
        }
    });
}

// Si esta en la pantalla de Create entonces vaciar todo
var URLactual = window.location.toString();
if (URLactual.indexOf('Create') > 1) {
    $('input[type="checkbox"]').prop("checked", false);
    $('#cpla_DescripcionPlanilla').val('');
    $('#cpla_FrecuenciaEnDias').val('')
}

// Desplegar los detalles del catalogo de planillas (ingresos y deducciones)
$(document).on('click', '#tblCatalogoPlanillas tbody tr td #btnDesplegarIngresosDeducciones', function () {
    var ID = $(this).closest('tr').data('id');
    var fila = $('#fila-' + ID);
    if (fila.css('display') == 'none') {
        $(this).closest('tr').after('<tr style="display:table-row" id="fila-' + ID + '">' + $('#fila-' + ID).html() + '</tr>');
        $(fila).remove();
        $(this).text('-');
    }
    else {
        fila = $('#fila-' + ID);
        fila.hide();
        $(this).text('+');
    }
    fila = $('#fila-' + ID);
});

//Seleccionar todo 
$(function () {
    $('#catalogoDeIngresos #seleccionar-todos').change(function () {
        $('#listado > input[type=checkbox]').prop('checked', $(this).is(':checked'));
    });

    $('#catalogoDeDeducciones #seleccionar-todos').change(function () {
        $('#listado > input[type=checkbox]').prop('checked', $(this).is(':checked'));
    });
});

$(document).on('click', '#btnGuardarCatalogoDePlanillasIngresosDeducciones', function () {
    create();
});

$(document).on('click', '#btnEditarCatalogoDePlanillasIngresosDeducciones', function () {
    create('edit');
});

// Funcion de obtener datos
var create = function (edit) {
    //Array de Ingresos y deducciones
    var arrayIngresos = [];
    var arrayDeducciones = [];
    //Obtener los valores del catalogo de planillas
    var descripcionPlanilla = $('#cpla_DescripcionPlanilla').val();
    var frecuenciaDias = $('#cpla_FrecuenciaEnDias').val();
    var idPlanilla = $('form #cpla_IdPlanilla').val();
    //Obtener las lista del catalogo de ingresos
    $('#catalogoDeIngresos input[type="checkbox"]').each(function (index, val) {

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

    //Obtener las lista del catalogo de deducciones
    $('#catalogoDeDeducciones input[type="checkbox"]').each(function (index, val) {
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
    debugger;
    if (edit != 'edit')
        $.ajax({
            url: "/CatalogoDePlanillas/Create",
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                catalogoDePlanillas: [
                    descripcionPlanilla, frecuenciaDias
                ],
                catalogoIngresos: arrayIngresos,
                catalogoDeducciones: arrayDeducciones
            })
        });
    else
        $.ajax({
            url: "/CatalogoDePlanillas/Edit",
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                id: idPlanilla,
                catalogoDePlanillas: [
                    descripcionPlanilla, frecuenciaDias
                ],
                catalogoIngresos: arrayIngresos,
                catalogoDeducciones: arrayDeducciones
            })
        });


    console.log('Array de Ingresos:' + arrayIngresos);
    console.log('Array de Deducciones:' + arrayDeducciones);
    console.log(descripcionPlanilla);
    console.log(frecuenciaDias);
}

//inactivar
$('#inactivar').click(() => {
    $('#InactivarCatalogoDeducciones').modal();
});

$('#InactivarCatalogoDeducciones #btnInactivarPlanilla').click(() => {
    var id = $('form #cpla_IdPlanilla').val();
    _ajax({id: id}, '/CatalogoDePlanillas/Delete', 'POST', (data) => {
        if (data == "bien") {
            location.href = 'http://localhost:51144/';
        }
    });
});