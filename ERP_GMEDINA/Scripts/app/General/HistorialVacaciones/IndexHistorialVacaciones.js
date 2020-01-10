


function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Vacaciones</h5><div align=right> <button type="button" class="btn btn-primary btn-xs" onclick="llamarmodal(' + IdEmpleado + ')">Registrar vacación</button> </div></div><div class="ibox-content"><div class="row">' + '<table id="IndexTable" class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                
                '<th>' + 'Fecha inicio' + '</th>' +
                '<th>' + 'Fecha fin' + '</th>' +
                '<th>' + 'Cantidad dias' + '</th>' +
                '<th>' + 'Mes vacaciones' + '</th>' +
                '<th>' + 'Año vacaciones' + '</th>' +
                '<th>' + 'Acciones' + '</th>' +
                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        div = div +
                '<tbody>' +
                '<tr>' +
                
                '<td>' + FechaFormato(index.hvac_FechaInicio).substring(0, 10) + '</td>' +
                '<td>' + FechaFormato(index.hvac_FechaFin).substring(0, 10) + '</td>' +
                '<td>' + index.hvac_CantDias + '</td>' +
                '<td>' + index.hvac_MesVacaciones + '</td>' +
                '<td>' + index.hvac_AnioVacaciones + '</td>' +
                '<td>' + ' <button type="button" class="btn btn-danger btn-xs" onclick="llamarmodaldelete(' + index.hvac_Id + ')" data-id="@item.hvac_Id">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles(' + index.hvac_Id + ')"data-id="@item.hvac_Id">Detalles</button>' + '</td>' +
                '</tr>' +
                '</tbody>'
        '</table>'

    });
    return div + '</div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/HistorialVacaciones/llenarTabla',
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
            '/HistorialVacaciones/ChildRowData',
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
    $("#ModalInhabilitar").find("#hvac_Id").val(ID);
    modaldelete.modal('show');
}

//function llamarmodaldetalles(ID) {
//    var modaldetalle = $("#ModalDetalles");
//    $("#ModalDetalles").find("#hvac_Id").val(ID);
//    id = ID;

function llamarmodaldetalles(ID) {
   
    //var modaldetalle = $("#ModalDetalles");
    //$('#Prueba tbody tr').on('click', function () {
    //    var tr = $(this).closest('tr');
    //    var row = tabla.row(tr);
    //    var id = row.data().id;
    //});
    id = ID;
    
    _ajax({ id: parseInt(id) },
        '/HistorialVacaciones/Detalles',
        'GET',
        function (obj) {
            $('#ModalDetalles').modal('show');
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#hvac_FechaInicio")["0"].innerText = FechaFormato(obj[0].hvac_FechaInicio).substring(0, 10);
                $("#ModalDetalles").find("#hvac_FechaFin")["0"].innerText = FechaFormato(obj[0].hvac_FechaFin).substring(0, 10);
                $("#ModalDetalles").find("#hvac_CantDias")["0"].innerText = obj[0].hvac_CantDias;
                $("#ModalDetalles").find("#hvac_DiasPagados")["0"].innerText = obj[0].hvac_DiasPagados;
                $("#ModalDetalles").find("#hvac_MesVacaciones")["0"].innerText = obj[0].hvac_MesVacaciones;
                $("#ModalDetalles").find("#hvac_AnioVacaciones")["0"].innerText = obj[0].hvac_AnioVacaciones;
                //$("#ModalDetalles").find("#hvac_Estado")["0"].innerText = obj[0].hvac_Estado;
                //$("#ModalDetalles").find("#hvac_RazonInactivo")["0"].innerText = obj[0].hvac_RazonInactivo;
                $("#ModalDetalles").find("#hvac_FechaCrea")["0"].innerText = FechaFormato(obj[0].hvac_FechaCrea).substring(0, 10);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj[0].hvac_UsuarioCrea;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj[0].hvac_UsuarioModifica;
                $("#ModalDetalles").find("#hvac_FechaModifica")["0"].innerText = FechaFormato(obj[0].hvac_FechaModifica).substring(0, 10);
                

            }
        });
}

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    
    if (data != null) {
        data = JSON.stringify({ tbHistorialVacaciones: data });
        _ajax(data,
            '/HistorialVacaciones/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["hvac_Id", "hvac_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ha Inactivado el registro");
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
        data = JSON.stringify({ tbHistorialVacaciones: data });
        _ajax(data,
            '/HistorialVacaciones/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["emp_Id", "hvac_FechaInicio", "hvac_FechaFin"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});



