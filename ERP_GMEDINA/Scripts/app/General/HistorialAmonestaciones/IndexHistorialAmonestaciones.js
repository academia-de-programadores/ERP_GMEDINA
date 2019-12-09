function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Amonestaciones</h5></div><div class="ibox-content"><div class="row">';
    debugger
    obj.forEach(function (index, value) {
        div = div +
            '<table class="table table-striped table-borderef table-hover dataTables-example"> <thead>' +
            '<tr>' +
                '<th>' + 'Tipo Amostacion' + '</th>'+
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                '<th>' + 'Fecha Amonestacion' + '</th>'+
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                '<th>'+ 'Obsevarcion Amonestacion' + '</th>'+
                '</tr>'+
                '</thead>'+
                '<tbody>'+'<tr>'+
                '<td>'+ index.tamo_Descripcion + '</td>'+
                '<td>'+ index.hamo_Fecha + '</td>'+
                '<td>'+ index.hamo_Observacion + '</td>'+
                '</tr>'+ '</tbody>'+
                '</table>'
         
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/HistorialAmonestaciones/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id: value.emp_Id,
                   Empleado: value.Empleado,
                   Cargo: value.Cargo,
                   Departamento: value.Departamento
               });
           });
           tabla.draw();
       });
}
$(document).ready(function () {
    llenarTabla();
});

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/HistorialAmonestaciones/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }
});