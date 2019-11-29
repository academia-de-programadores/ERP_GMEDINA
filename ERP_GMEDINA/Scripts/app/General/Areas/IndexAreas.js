function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Departamentos</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index,value) {
        div = div +
            '<div class="col-md-3">'+
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5>' + index.depto_Descripcion + '</h5>' +
                '</div>'+
                '<div class="panel-body">' +
                    '<h5>' + index.car_Descripcion + '</h5>'
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                    + index.per_NombreCompleto + '<br>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    index.per_Telefono + '</div>' +
                '</div>'+
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/Areas/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id: value.area_Id,
                   Descripcion: value.area_Descripcion
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
