var tabla = null;
$(document).ready(function () {
    var columnas = [];
    var col = 0;
    $("#IndexTable thead tr").find("th").each(function (indice,valor) {
        campo = valor.innerText;
        if (campo=="") {
            columnas.push({
                className: 'details-control',
                orderable: false,
                data: null,
                defaultContent: ''
            });
            col = col+1;
        } else if (campo=="Id") {
            columnas.push({
                data: campo,
                visible: false
            });
            col = col + 1;
        }else if (campo == "Acciones") {
            columnas.push({
                data: "Acciones",
                orderable: false
                });
            } else {
            columnas.push({ data: campo });
            }
    });
    tabla = $('#IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        responsive: true,
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

            {
                extend: 'print',
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