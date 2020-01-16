var eqem_Id = 0;
$(document).ready(function () {
    llenarTabla();
});
function format(obj, eqem_Id) {
    var div = '<div class="ibox"><div class="ibox-title"><strong class="mr-auto m-l-sm">Equipo de trabajo</strong><div class="btn-group pull-right">' +
        '<button id = "btnAgregar" data-id="' + eqem_Id + '" data-toggle="ModalNuevo" class="btn btn-outline btn-primary btn-xs" onClick = "showmodal(this)"> Asignar Equipo </button>' +
        '</div></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
              '<div class="panel panel-default">' +
                '<div class="panel-heading">' +
                  '<h5>' + index.eqtra_Codigo + '</h5>' +
                '</div>' +
                '<div class="panel-body">' +
                  'Descripción: ' + index.eqtra_Descripcion +
                  '<br> Observación: ' + index.eqtra_Observacion +
                  '<br> Fecha de Entrega: ' + FechaFormatoSimple(index.eqem_Fecha) +
                '</div>' +
                '<div class="modal-footer">' +
                  '<button id = "btnDetalle" data-id="' + index.eqtra_Id + '" data-toggle="ModalDetalles" class="btn btn-primary btn-xs pull-right" onClick = "showmodalDetalle(this)"> Detalles </button>' +
                  '<button id = "btnEditar" data-id="' + index.eqtra_Id + '" data-toggle="ModalEditar" class="btn btn-defaults btn-xs pull-right" onClick = "showmodaledit(this)"> Editar </button>' +
                '</div>' +
              '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null, '/EquipoEmpleados/llenarTabla', 'POST',
        function (lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(lista)) {
                return null;
            }

            $.each(lista, function (index, value) {
                tabla.row.add
                    ({
                        ID: value.eqem_Id,
                        Empleado: value.Empleado[0],
                        Correo: value.Correo[0],
                        Telefono: value.Telefono[0]
                    })
            });
            tabla.draw();
        });
}
$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);



    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().ID;
        t = row.data().t;
        row.child(htmlSpiner).show();
        tr.addClass('shown');
        _ajax({ id: parseInt(id) },
            '/EquipoEmpleados/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    //desaparecemos el spiner
                    row.child.hide();
                    tr.removeClass('shown');
                    //dibuja el childRow
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                } else {
                    row.child.hide();
                    tr.removeClass('shown');
                    //dibuja el childRow
                    row.child("Error de conexion").show();
                    tr.addClass('shown');
                }
            });
    }
});
$(document).on("click", "#IndexTable tbody tr td buttton#btnAgregar", function () {
    var Id = $(this).data('id');
    console.log(Id)
})