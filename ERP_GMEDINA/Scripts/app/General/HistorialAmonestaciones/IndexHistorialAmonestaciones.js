function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Amonestaciones</h5><div align=right> <button type="button" class="btn btn-primary btn-xs" onclick="llamarmodal(' + IdEmpleado + ')">Agregar Amonestación</button> <button type="button" class="btn btn-primary btn-xs" id="btnAudienciaDescargo" data-id="@item.cin_IdIngreso">Audiecia Descargo</button></div></div><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
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
                    '<td>' + index.tamo_Descripcion + '</td>' +
                    '<td>' + FechaFormato(index.hamo_Fecha).substring(0, 10) + '</td>' +
                    '<td>' + index.hamo_Observacion + '</td>' +
                    '<td>' + ' <button type="button" class="btn btn-danger btn-xs" onclick="llamarmodaldelete(' + index.hamo_Id + ')" data-id="@item.hamo_Id">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles('+index.hamo_Id+')"data-id="@item.hamo_Id">Detalle</button>' + '</td>' +
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
var IdEmpleado = 0;
$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        IdEmpleado = id;
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
    $("#ModalNuevo").find("#emp_Id").val(IdEmpleado);
    modalnuevo.modal('show');
}
function llamarmodaldelete(ID) {
    var modaldelete = $("#ModalInhabilitar");
    $("#ModalInhabilitar").find("#hamo_Id").val(ID);
    modaldelete.modal('show');
}

function llamarmodaldetalles() {
    var modaldetalle = $("#ModalDetalles");
    id = IdEmpleado;
    debugger
    _ajax(null,
        '/HistorialAmonestaciones/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#hamo_AmonestacionAnterior")["0"].innerText = obj.hamo_AmonestacionAnterior;
                $("#ModalDetalles").find("#hamo_Observacion")["0"].innerText = obj.hamo_Observacion;
                $("#ModalDetalles").find("#tbTipoAmonestaciones")["0"].innerText = obj.tbTipoAmonestaciones.tamo_Descripcion;
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#hamo_FechaCrea")["0"].innerText = obj.hamo_FechaCrea;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#hamo_FechaModifica")["0"].innerText = obj.hamo_FechaModifica;
                debugger
                $('#ModalDetalles').modal('show');
            }
        });
}

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAmonestaciones: data });
        _ajax(data,
            '/HistorialAmonestaciones/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["hamo_Id", "hamo_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAmonestaciones: data });
        _ajax(data,
            '/HistorialAmonestaciones/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["emp_Id", "tamo_Id","hamo_Fecha","hamo_Observacion"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});



