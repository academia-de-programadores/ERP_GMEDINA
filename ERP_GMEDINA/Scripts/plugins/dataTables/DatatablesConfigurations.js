$(document).ready(function () {
    var Cols = ColCount();

    var IndexTable = $('#IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        responsive: true,
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
        pageLength: 2,
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
   IndexTable = IndexTable.DataTable({pageLength: 2 });
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