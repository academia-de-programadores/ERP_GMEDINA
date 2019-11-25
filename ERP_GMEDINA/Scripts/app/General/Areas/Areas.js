function format(obj) {
    // `d` is the original data object for the row
    var tableChild = '<table class="table"><thead><tr>' +
        '<th>Departamento:</th>' +
        '<th>Encargado:</th>' +
        '<th>Telefono:</th>' +        
    '</tr></thead>' +
    '<tbody>' ;
    obj.forEach(function (index,value) {
        tableChild = tableChild + 
        '<tr><td>' + index.depto_Descripcion + '</td>' +
      '<td>' + index.per_NombreCompleto + '</td>' +
        '<td>' + index.per_Telefono + '</td>' +
    '</tr>';
    });
    
    tableChild = tableChild + '</tbody></table>';
    console.log(tableChild);
      return tableChild;
}
var tabla = null;
$(document).ready(function () {
    IndexTable = $('.IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        columns: [
            {
                "className": 'details-control',
                "orderable": false,
                "data": '',
                "defaultContent": ''
            },
            { "data": "id", "visible": false },
            { "data": "area_Descripcion" },
            { "data": "Acciones" }
        ],
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
        order: [[1, 'asc']]
    });    
});
$('.IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = IndexTable.row(tr);

    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = tr.data("id");
        _ajax({ id: parseInt(id) },
            '/Areas/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {                        
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });       
    }
});
