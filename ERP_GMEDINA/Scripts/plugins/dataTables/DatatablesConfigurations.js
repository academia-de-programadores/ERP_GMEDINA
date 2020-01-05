var tabla = null;
var botones = [];
var htmlSpiner = '<div class="ibox-content sk-loading">' +
'<div class="sk-spinner sk-spinner-double-bounce">' +
'<div class="sk-double-bounce1"></div>' +
'<div class="sk-double-bounce2"></div>' +
'</div>' +
'<h1 class="title">Cargando...</h1>'
'</div>'
$(document).ready(function () {
    var columnas = [];
    var col = 0;
    var contador = -1;
    //Al cargar el documento, esta funcion identificara y nombrara los paramtros de la tabla
    var head = $("#IndexTable thead tr").find("th").each(function (indice, valor) {
        contador = contador + 1;
        campo = valor.innerText;
        //Quita los espacios del enacabezado.
        //El nombre del campo en el Json sera el DisplayName de la clase parcial SIN espacios, respetando mayusculas.
        campo = campo.replace(/\s/g, '');
        //Si la primera columna no tiene encabezado, sera la columna de botones de expandir
        if (campo == "") {
            columnas.push({
                className: 'details-control',
                orderable: false,
                data: null,
                defaultContent: ''
            });
            col = col + 1;
        }
            //Si la columna tiene el nombre de ID se ocultara al usuario, la informacion seguira en el Json de DataTables
        else if (campo.toUpperCase() == "ID") {
            columnas.push({
                data: campo,
                visible: false
            });
            col = col + 1;
        }

            //Si la columa tiene el nombre de "Acciones", automaticamente insertara los botones de Detalles y Editar
        else if (campo == "Acciones") {
            columnas.push({
                data: null,
                orderable: false,
                defaultContent: "<div>" +
                                    "<a class='btn btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                                    "<a class='btn btn-default btn-xs ' onclick='CallEditar(this)'>Editar</a>" +
                                "</div>"
            });
        }
        else if (campo == "Info") {
            columnas.push({
                data: null,
                orderable: false,
                defaultContent: "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                                    "<a class='btn btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +

                                "</div>"
            });
        }
        else {
            columnas.push({ data: campo });
            botones.push(contador);
        }
    });
    tabla = $('#IndexTable').DataTable({
        //"language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": htmlSpiner,
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
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
        responsive: true,
        pageLength: 25,
        "scrollX": true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                exportOptions: {
                    columns: botones
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: botones
                }
            },
            {
                extend: 'excel',
                exportOptions: {
                    columns: botones
                },
                title: 'ExampleFile'
            },
            {
                extend: 'pdf',
                exportOptions: {
                    columns: botones
                },
                title: 'nadaaa'
            },

            {
                extend: 'print',
                exportOptions: {
                    columns: botones
                },
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');



                    $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                }
            }
        ],
        //Aqui se le pasa al DataTables la estructura de la tabla con sus parametros correspondientes
        columns: columnas,
        order: [[col, 'asc']],

    });
});


function CallDetalles(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;

    tablaDetalles(id);
}

function CallEditar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;

    tablaEditar(id);
}
