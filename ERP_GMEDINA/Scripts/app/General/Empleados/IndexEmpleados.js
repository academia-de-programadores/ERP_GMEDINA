
function format(obj) {
    
    var div = '<div class="ibox"><div class="ibox-title"> <i class="fa fa-newspaper-o"> </i><strong class="mr-auto m-l-sm">Datos de Empleado</strong><div class="btn-group pull-right"><button data-toggle="dropdown" class="btn btn-outline btn-primary btn-xs dropdown-toggle"><i class="fa fa-paste"></i> Reportes </button><ul class="dropdown-menu"><li><a class="dropdown-item" href="#">Horas Trabajadas</a></li><li><a class="dropdown-item" href="#">Perfil Profesional</a></li></ul> </div></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-5">' +
                //'<div class="panel panel-default">' +
                //  '<div class="panel-heading">' +
                //     '<h5></h5>' +
                //'</div>' +
                //'<div class="panel-body">' +
                '<p><label><strong>Cargo:' + '\xa0' + ' </strong></label>'
                     + index.car_Descripcion + '</p>'+
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                     '<p><label><strong>Area:' + '\xa0' + ' </strong></label>'
                    +index.area_Descripcion + '</p>' +
                    '<p><label><strong>Jornada:' + '\xa0' + ' </strong></label>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    //\xa0=Espacio en texto.
                    index.jor_Descripcion + '</p>'+
                    //'</div>' +
            //    '</div>' +
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
                   id: value.Id,
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
        id = row.data().id;
        hola = row.data();
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
