function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
            '<td>Descripción:</td>' +
            '<td>Hora inicio:</td>' +
            '<td>Hora fin:</td>' +
            '<td>Hora inicio:</td>' +
        '</tr>' +
        '<tr>' +
            '<td>' + "Descripcion" + '</td>' +
            '<td>' + "Hora_inicio" + '</td>' +
            '<td>' + "Hora_fin" + '</td>' +
            '<td>' + "cant_horas" + '</td>' +
        '</tr>' +
    '</table>';
}

$(document).ready(function () {
    var Cols = ColCount();

    var IndexTable = $('#IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        pageLength: 25,
        columns: [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {"data":"Jor_Id"},
            {"data":"Jor_Desc"},
            { "data": "Acciones"}
        ],
        order: [[1, 'asc']],
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'copy' },
            { extend: 'csv' },
            {
                extend: 'excel', title: 'ExampleFile',
                exportOptions: {
                    columns: [Cols]
                }
            },
            {
                extend: 'pdf', title: 'ExampleFile',
                exportOptions: {
                    columns: [Cols]
                }
            },
            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                },
                exportOptions: {
                    columns: [Cols]
                }
            }
        ]
    });


$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = IndexTable.row(tr);
    console.log(tr.data("id"));
    if ( row.child.isShown() ) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        // Open this row
        row.child( format(row.data()) ).show();
        tr.addClass('shown');
    }
} );
});

function ColCount() {
    var col = document.getElementById('IndexTable').rows[0].cells.length;
    console.log(col);
    var RtrStr = "0";

    for (var i = 1; i < col - 1; i++)
    {
        RtrStr += ", " + i.toString();
    }
    return RtrStr;
};
