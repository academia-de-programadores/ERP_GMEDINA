//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback) {
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

//Obtener la URL
var URLactual = window.location.toString();
var getUrl = window.location
var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];

// Si esta en la pantalla de Create entonces vaciar todo
if (getUrl.toString().indexOf('Create') > 1) {
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

//Insetar
$(document).on('click', '#btnGuardarCatalogoDePlanillasIngresosDeducciones', function () {
    crearEditar();
});

//Editar
$(document).on('click', '#btnEditarCatalogoDePlanillasIngresosDeducciones', function () {
    crearEditar('edit');
});

function verificarCampos(descripcionPlanilla, frecuenciaDias, catalogoIngresos, catalogoDeducciones) {
    if (descripcionPlanilla == "" || frecuenciaDias == "" || catalogoIngresos.length == 0 || catalogoDeducciones.length == 0) {
        iziToast.error({
            title: 'Error',
            message: 'Faltan campos por ingresar',
        });
        return false;
    }
    return true;
}

// Funcion para crear y editar
var crearEditar = function (edit) {
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

    //Insertar o editar
    if (verificarCampos(descripcionPlanilla, frecuenciaDias, arrayIngresos, arrayDeducciones)) {
        if (edit != 'edit')
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
                    if (data == "bien"){
                        iziToast.success({
                            title: 'Exito',
                            message: 'El registro fue insertado de forma exitosa.',
                        });
                        location.href = baseUrl;
                    }
                    else
                        iziToast.error({
                            title: 'Error',
                            message: 'Hubo un error al insertar el registro',
                        });
                });
        else
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
                    if (data == "bien"){
                        iziToast.success({
                            title: 'Exito',
                            message: 'El registro fue editado de forma exitosa.',
                        });
                        location.href = baseUrl;
                    }
                    else
                        iziToast.error({
                            title: 'Error',
                            message: 'Hubo un error al editar el registro',
                        });
                });
    }
}

//Inactivar
$('#inactivar').click(() => {
    $('#InactivarCatalogoDeducciones').modal();
});

$('#InactivarCatalogoDeducciones #btnInactivarPlanilla').click(() => {
    var id = $('form #cpla_IdPlanilla').val();
    _ajax({ id: id }, '/CatalogoDePlanillas/Delete', 'POST', (data) => {
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
    });
});