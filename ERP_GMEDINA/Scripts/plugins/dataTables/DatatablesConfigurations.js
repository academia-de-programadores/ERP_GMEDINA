var tabla = null;
var botones = [];
$(document).ready(function () {
    var columnas = [];
    var col = 0;
    var contador = -1;
    var head= $("#IndexTable thead tr").find("th").each(function (indice, valor) {
        contador = contador + 1;
        campo = valor.innerText;
        if (campo == "") {
            columnas.push({
                className: 'details-control',
                orderable: false,
                data: null,
                defaultContent: ''
            });
            col = col + 1;
        } else if (campo.toUpperCase() == "ID") {
            columnas.push({
                data: campo,
                visible: false
            });
            col = col + 1;
        } else if (campo == "Acciones") {
            columnas.push({
                data: null,
                orderable: false,
                defaultContent: "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(this)' >Detalles</a>" +
                                    "<a class='btn btn-default btn-xs ' onclick='tablaEditar(this)'>Editar</a>" +
                                "</div>"
            });
        } else {
            columnas.push({ data: campo });
            botones.push(contador);
        }
    });
    tabla = $('#IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        responsive: true,
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
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
        columns: columnas,
        order: [[col, 'asc']],
    });   
});