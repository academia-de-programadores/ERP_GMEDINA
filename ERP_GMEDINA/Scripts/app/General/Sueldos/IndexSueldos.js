function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/Sueldos/Edit/" + id);
}
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    $(location).attr('href', "/Sueldos/Edit/" + id);
}
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Sueldos</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5>' + index.Cargo + '</h5>' +
                '</div>' +
                '<div class="panel-body">' +
                    '<h5>' + index.Sueldo_Anterior + '</h5>'
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                    + index.Tipo_Moneda + '<br>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    index.Cuenta + '</div>' +
                '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}


function llenarTabla() {
    _ajax(null,
       '/Sueldos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id:value.Id,
                   Identidad :value.Identidad,
                   Nombre: value.Nombre,
                   Sueldo :value.Sueldo,
                   Tipo_Moneda:value.Tipo_Moneda,
                   Cuenta :value.Cuenta,
                   Sueldo_Anterior: value.Sueldo_Anterior,
                   Area: value.Area,
                   Cargo: value.Cargo,
                   Usuario_Nombre : value.Usuario_Nombre,
                   Usuario_Crea : value.Usuario_Crea,
                   Usuario_Fecha : value.Usuario_Fecha,
                   Usuario_Modifica : value.Usuario_Modifica,
                   Fecha_Modifica: value.Fecha_Modifica
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
            '/Sueldos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});


function CallEditar() {
    var modalnuevo = $("#ModalEditar");
    modalnuevo.modal('show');
}


function CallDetalles() {
    var modalnuevo = $("#ModalDetalles");
    modalnuevo.modal('show');
}



