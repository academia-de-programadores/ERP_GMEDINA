function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/HistorialPermisos/Edit/" + id);
}
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/HistorialPermisos/Edit/" + id);
}
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Permisos</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5>' + index.hper_fechaInicio + '</h5>' +
                '</div>' +
                '<div class="panel-body">' + 'Hora Inicio: '
                    + index.hper_fechaInicio.toString() + '<br> Hora Fin: ' +
                    index.hper_fechaFin + '</div>' +
                '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/HistorialPermisos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id: value.hper_Id,
                   tper_Id: value.Id_Permiso,
                   tper_Descripcion: value.Descripcion_Permiso,
                   hper_fechaInicio: value.Fecha_Inicial,
                   hper_fechaFin: value.Fecha_Fin,
                   hper_Duracion: value.Duracion,
                   hper_Justificado: value.Justificado,
                   per_Nombres: value.Nombre_Completo,
                   //Descripcion: value.area_Descripcion,
                   //Encargado: value.Encargado.length == 0 ? 'Sin Asignar' : value.Encargado[0]
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
        id = row.data().ID;
        hola = row.data().hola;
        tr.addClass('loading');
        _ajax({ id: parseInt(id) },
            '/HistorialPermisos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.removeClass('loading');
                    tr.addClass('shown');
                }
            });
    }

});