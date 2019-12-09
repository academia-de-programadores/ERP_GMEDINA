function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Amonestaciones</h5><div align=right> <button type="button" class="btn btn-primary btn-xs" onclick="llamarmodal()">Agregar Amonestación</button> <button type="button" class="btn btn-primary btn-xs" id="btnAudienciaDescargo" data-id="@item.cin_IdIngreso">Audiecia Descargo</button></div></div><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                '<th>' + 'Tipo Amonestacion' + '</th>' +
                '<th>' + 'Fecha' + '</th>' +
                '<th>' + 'Obsevarcion' + '</th>' +
                '<th>' + 'Acciones' + '</th>' +
                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        div = div +
                '<tbody>' +
                '<tr>' +
                '<td>'+ index.tamo_Descripcion + '</td>'+
                '<td>' + FechaFormato(index.hamo_Fecha).substring(0,10) + '</td>' +
                '<td>' + index.hamo_Observacion + '</td>' +
                '<td>' + ' <button type="button" class="btn btn-danger btn-xs" onclick="llamarmodaldelete()" data-id="@item.hamo_Id">Eliminar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles()"data-id="@item.hamo_Id">Detalle</button>' + '</td>' +
                '</tr>' +
                '</tbody>' 
                '</table>'
         
    });
    return div + '</div></div>';
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


function llamarmodal() {
    var modalnuevo = $("#ModalNuevo");
    modalnuevo.modal('show');
}
function llamarmodaldelete() {
    var modaldelete = $("#ModalInhabilitar");
    modaldelete.modal('show');
}
function llamarmodaldetalles() {
    var modaldetalle = $("#ModalDetalles");
    modaldetalle.modal('show');
}

