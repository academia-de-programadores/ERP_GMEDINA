function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Amonestaciones</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                '<div class="panel-body">' +
                    '<h5>' + index.per_NombreCompleto + '</h5>'
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                    + index.hamo_AmonestacionAnterior + '<br>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    index.tamo_Descripcion + '</div>' +
                '</div>' +
            '</div>'
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
                   Id: value.hamo_Id,
                   Cargo: value.Cargo,
                   Empleado:value.Empleado.length == 0 ? 'Sin Asignar' : value.Empleado[0]
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