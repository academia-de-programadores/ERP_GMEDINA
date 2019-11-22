function format(row, tr) {
    // `d` is the original data object for the row
    tablaChild = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
            '<td>Full name:</td>' +
            '<td></td>' +
        '</tr>' +
        '<tr>' +
            '<td>Extension number:</td>' +
            '<td></td>' +
        '</tr>' +
        '<tr>' +
            '<td>Extra info:</td>' +
            '<td>And any further details here (images etc)...</td>' +
        '</tr>' +
    '</table>';
    id = tr.find("a").data("id");
    _ajax(null,
        '/Habilidades/ChildRowData/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                row.child(tablaChild).show();
                tr.addClass('shown');
            }
        });
}
$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);

    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        // Open this row
        //row.child(format(row.data())).show();
        //tr.addClass('shown');
        format(row, tr);
    }
});