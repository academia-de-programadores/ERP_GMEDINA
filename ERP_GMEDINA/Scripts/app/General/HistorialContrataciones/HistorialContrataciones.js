function llenarTabla() {
    _ajax(null,
       '/HistorialContrataciones/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           if (validarDT(Lista)) {
               return null;
           }
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id: value.hcon_Id,
                   "Número": value.hcon_Id,
                   Nombre: value.Nombre,
                   Departamento: value.dep_Descripcion,
                   Area: value.area_Descripcion,
                   "Área": value.area_Descripcion,
                   Cargo: value.car_Descripcion,
                   FechaSeleccion: FechaFormato(value.scan_Fecha).substring(0, 10),
                   FechaContratado: FechaFormato(value.scan_Fecha).substring(0, 10)
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
            '/HistorialContrataciones/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});