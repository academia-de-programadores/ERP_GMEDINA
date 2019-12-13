function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/HistorialSalidas/Edit/" + id);
}
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/HistorialSalidas/Edit/" + id);
}
function format(obj) {
    var EstadoCivil = '';
    var div = '<div class="ibox"><div class="ibox-title"><h5>Informacion personal y de contacto: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index,value) {
        index.per_EstadoCivil.toUpperCase() == ('S') ? EstadoCivil = 'Soltero(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('C') ? EstadoCivil = 'Casado(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('D') ? EstadoCivil = 'Divorciado(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('V') ? EstadoCivil = 'Viudo'
    : 'Union Libre';
        div = div +
        '<div class="col-md-5"><b>Numero de identidad: </b>' + index.per_Identidad + '</div>'
        + '<div class="col-md-5"><B>Correo electrónico: </b>' + index.per_CorreoElectronico + '</div>'
        + '<div class="col-md-5"><b>Edad: </b>' + index.per_Edad + '</div>'
        + '<div class="col-md-5"><b>Dirección: </b>' + index.per_Direccion + '</div>'
        + '<div class="col-md-5"><b>Estado civil: </b>' + EstadoCivil + '</div>'
        + '<div class="col-md-5"><b>Teléfono: </b>' + index.per_Telefono + '</div>'
        + '<div class="col-md-10"><h3><b>•Razon salida: </b>' + index.rsal_Descripcion + '</h3></div>'
        + '</div>' +
        '</div>' +
        '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    console.log('Prueba');
    _ajax(null,
       '/HistorialSalidas/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id : value.hsal_Id,
                   tsal_Id : value.tsal_Id,
                   TipoSalida : value.tsal_Descripcion,
                   rsal_Id : value.rsal_Id,
                   rsal_Descripcion : value.rsal_Descripcion,
                   NombreCompleto: value.per_Nombres,
                   per_CorreoElectronico : value.per_CorreoElectronico,
                   per_Telefono : value.per_Telefono,
                   per_Direccion : value.per_Direccion,
                   per_Edad : value.per_Edad,
                   per_EstadoCivil : value.per_EstadoCivil,
                   hsal_Observacion : value.hsal_Observacion
               });
           });
           tabla.draw();
       });
}
$(document).ready(function () {
    llenarTabla();
});

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    console.log('Casi');
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        hola = row.data().hola;
        _ajax({ id : parseInt(id) },
            '/HistorialSalidas/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {                        
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });       
    }

});
