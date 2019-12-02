function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Informacion</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5></h5>' +
                '</div>' +
                '<div class="panel-body">' +
                    '<h5>' + index.tbCargos.car_id + '</h5>'
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                    + index.tbAreas.area_id + '<br>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    index.tbJornadas.jor_id + '</div>' +
                '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/Empleados/llenarTabla',
       'POST',
       function (Lista) {
           //var tabla = $("#IndexTable").DataTable();
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Identidad: value.per_Identidad,
                   Nombre: value.Nombre,
                   Departamento: value.depto_Descripcion,
                   Sexo: value.per_Sexo,
                   Edad: value.per_Edad,
                   Telefono: value.per_Telefono,
                   Correo: value.per_CorreoElectronico

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
            '/Empleados/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }
});
