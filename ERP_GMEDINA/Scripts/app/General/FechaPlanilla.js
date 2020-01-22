$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
    });

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

var table;
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
function listar() {
    table = $('#tblHistorialPlanillas').DataTable({
        //Con este metodo se le dan los estilos y funcionalidades de datatable a la tabla
        ajax: {
            //Hacer la peticion asíncrona y obtener los datos que se mostraran en el datatable
            method: 'GET',
            url: 'FechaPlanilla/getFechaPlanilla',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        },
        'columns': [
            {
                //Columna 1: el boton de desplegar
                orderable: false,
                className: 'details-control', //Estos estilos estan en: Content/app/General
                data: null,
                defaultContent: ''
            },
            {
                'data': 'Anio'
            },
        ],
        language: {
            sProcessing: spinner(),
            sLengthMenu: 'Mostrar _MENU_ registros',
            sZeroRecords: 'No se encontraron resultados',
            sEmptyTable: 'Ningún dato disponible en esta tabla',
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
                sNext:
                    'Siguiente',
                sPrevious:
                    'Anterior'
            },
            oAria: {
                sSortAscending:
                    ': Activar para ordenar la columna de manera ascendente',
                sSortDescending:
                    ': Activar para ordenar la columna de manera descendente'
            }
        }, //Con esto se hace la traducción al español del datatables
        responsive: false,
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgtpi', //Darle los elementos del DOM que deseo
        buttons: [
            //Poner los botones que quiero que aparezcan
            {
                extend: 'copy',
                title: 'Fecha de Planilla',
                titleAttr: 'Copiar',
                exportOptions: {
                    columns: [1]
                },
                className: 'btn btn-primary'
            }
        ]
    });
}

function obtenerDetalles(id, handleData) {
    _ajax(
        { anio: id },
        '/FechaPlanilla/getTipoPlanilla',
        'POST',
        (data) => {
            handleData(data);
        },
        () => { }
    );
}

//Cuando de click en el botón de detalles
$(document).on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = table.row(tr);
    var data = table.row($(this).parents('tr')).data();

    //Que cierre el detalle
    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    } else {
        //Que desplegue el detalle
        row.child([spinner()]).show();
        tr.addClass('shown');
        let anio = data.Anio;

        // Obtener los datos para el detalle
        obtenerDetalles(
            anio,
            (data) => {
                console.table(data);
                //Mostrar el detalle con sus datos
                row.child([getTipoPlanilla(data)]).show();
                tr.addClass('shown');
            },
            () => { }
        );
    }
});


$(document).on('click', '#btnDetalle', function () {
    console.log(this);
})

$(document).on('submit', '#btnDetalle', function () {
    let data = $(this).data("idplanilla");
    let anio = $(this).data("anioplanilla");
    console.log(data);
    console.log(anio);
    //_ajax({ ID: data, anio: anio }, 'http://localhost:51144/FechaPlanilla/ComprobanteEmpleadoEncabezado', 'POST', (data) => { console.log(data); }, () => { });

});

function getTipoPlanilla(data) {
    var ingresosPlanillas = `
    <div class="col-lg-6">
       <div class="ibox-title">
            <h5>Planillas Pagadas</h5>
        </div>
        <div class="ibox-content">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th></th>
                    </tr>
                </thead>
                <tbody>`;
    $.each(data.data, function (index, val) {
        console.log("Id:" + val.idPlanilla + " Desc: " + val.DescripcionPlanilla);
        ingresosPlanillas +=
            `  <tr>
                    <td>
                         <form method="POST" action="FechaPlanilla/ComprobanteEmpleadoEncabezado">
                            <input name="ID" id="idPlanilla" value="`+ val.idPlanilla + `" hidden>
                            <input name="anio" id="anioPlanilla" value="` + val.anioPlanilla + `" hidden>
                            ` + val.DescripcionPlanilla + `
                            <input type="submit" class ="btn btn-primary btn-xs pull-right" data-idPlanilla= "`+ val.idPlanilla + `"  data-anioPlanilla= "` + data.fecha + `"  id="btnDetalle" value="Detalle"/>
                     </form>
                    </td>
                </tr>

                `;
    });
    ingresosPlanillas += `</tbody>
            </table>
        </div>
    </div>`;

    return ingresosPlanillas;
}


$(document).on('click', '#btnDetalleCatalogoDeducciones', function () {
    let id = $(this).data("id");
    var getUrl = window.location, baseUrl =
        getUrl.protocol + '//' + getUrl.host + '/' + getUrl.pathname.split('/')[1];

    window.location = baseUrl + '/ReporteDeHistorialDeIngresos/?id=' + id;
});

$(document).on('click', '#btnDetalleCatalogoDeducciones2', function () {
    let id = $(this).data("id");
    var getUrl = window.location, baseUrl =
        getUrl.protocol + '//' + getUrl.host + '/' + getUrl.pathname.split('/')[1];

    window.location = baseUrl + '/ReporteDeHistorialDeDeducciones/?id=' + id;

});

$(document).ready(() => {
    console.log('carfgdsgsdg');
    listar();
}); 