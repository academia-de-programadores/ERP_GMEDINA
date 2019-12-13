function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Sueldos</h5><div align=right> <button type="button" class="btn btn-primary btn-xs" onclick="llamarmodal(' + IdSueldo + ')">Editar Sueldo</button></div></div><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                '<th>' + 'Sueldo Anterior' + '</th>' +
                '<th>' + 'Id del empleado' + '</th>' +
                '<th>' + 'Cuenta Bancaria' + '</th>' +
                '<th>' + 'Cargo' + '</th>' +
                '<th>' + 'Acciones' + '</th>' +
                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        div = div +
                '<tbody>' +
                '<tr>' +
                '<td>' + index.Sueldo_Anterior + '</td>' +
                '<td>' + index.Identidad + '</td>' +
                '<td>' + index.Cuenta + '</td>' +
                '<td>' + index.Cargo + '</td>' +
                '<td>' + ' <button type="button" class="btn btn-danger btn-xs" onclick="llamarmodaldelete(' + index.Id + ')" data-id="@item.Id">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles(' + index.Id + ')"data-id="@item.Id">Detalle</button>' + '</td>' +
                '</tr>' +
                '</tbody>'
        '</table>'
    });
    return div + '</div></div>';
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
                   Id : value.Id,
                   Identidad: value.Identidad,
                   Nombre : value.Nombre,
                   Sueldo : value.Sueldo,
                   Tipo_Moneda : value.Tipo_Moneda,
                   Cuenta : value.Cuenta,
                   Sueldo_Anterior: value.Sueldo_Anterior,
                   Area : value.Area,
                   Cargo : value.Cargo,
                   Usuario_Nombre : value.Usuario_Nombre,
                   Usuario_Crea : value.Usuario_Crea,
                   Fecha_Crea : value.Fecha_Crea,
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
var IdSueldo = 0;
var vemp_Id = 0;
var vtmon_Id = 0;
$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        IdSueldo = id;
        vemp_Id = row.data().
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




function llamarmodal() {
    var modalEditar = $("#ModalEditar");
    $("#ModalEditar").find("#Id").val(IdSueldo);
    modalEditar.modal('show');
}


$("#btnActualizar").click(function () {
    console.log("sf");  
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    
    if (data != null) {
        data = JSON.stringify({ Sueldos: data });
        debugger
        _ajax(data,
            '/Sueldos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["sue_Cantidad"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});






function llamarmodaldelete(ID) {
    console.log("aja");
    var modaldelete = $("#ModalInhabilitar");
    $("#ModalInhabilitar").find("#Id").val(IdSueldo);
    modaldelete.modal('show');
}



function llamarmodaldetalles() {
    var modaldetalle = $("#ModalDetalles");
    id = IdSueldo;
    debugger
    _ajax(null,
        '/Sueldos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {


                $("#ModalDetalles").find("#Nombre")["0"].innerText = obj.Nombre;
                $("#ModalDetalles").find("#Cuenta")["0"].innerText = obj.Cuenta;
                $("#ModalDetalles").find("#Sueldo_Anterior")["0"].innerText = obj.Sueldo_Anterior;
                $("#ModalDetalles").find("#Usuario_Crea")["0"].innerText = obj.Usuario_Crea;
                $("#ModalDetalles").find("#Fecha_Crea")["0"].innerText = obj.Fecha_Crea;

               



                $('#ModalDetalles').modal('show');
            }
        });
}



$("#InActivar").click(function () {
    console.log("Lupe")
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAmonestaciones: data });
        _ajax(data,
            '/Sueldos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["sue_Id", "sue_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});




