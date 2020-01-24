
function llenarTabla() {
    _ajax(null,
       '/HistorialHorasTrabajadas/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id: value.htra_Id,
                   "Número": value.htra_Id,
                   Nombres: value.Empleado,
                   Jornada: value.Jornadas,
                   Hora: value.tiho_Descripcion,
                   Cantidad:value.Cantidad,
                   Recargo: value.tiho_Recargo,
                   Fecha: FechaFormato(value.Fecha).substring(0, 10)
                   
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
            '/HistorialHorasTrabajadas/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});
