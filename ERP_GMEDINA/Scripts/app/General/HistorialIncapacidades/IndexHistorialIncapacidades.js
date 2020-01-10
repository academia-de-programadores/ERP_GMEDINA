

function format(obj) {


    var div = '<div class="ibox"><div class="ibox-title"><h5>Incapacidades</h5> <div align=right><button type="button" class="btn btn-primary btn-xs" onclick="Llamarmodalcreate(' + idEmpleado + ')" data-id="@item.cin_IdIngreso">Nueva Incapacidad</button> </div> </div><div class="ibox-content"><div class="row">'
        + '<table class="table table-striped table-bordered table-hover dataTables-example" >'
        + '<thead>'
        + '<tr> <th>  Incapacidad  </th>'
        + '<th>Dias de retiro</th>'
        + '<th>Centro Medico</th> '
         + '<th>Diagnostico</th> '
         + '<th>Fecha Inicio</th> '
         + '<th>Fecha Fin</th> '
         + '<th>Acciones</th> '
        + '</tr> </thead> ';
    obj.forEach(function (index, value) {
        div = div +
            '<tbody>' + '<tr>'
                + '<td>' + index.ticn_Descripcion + '</td>'
                + '<td>' + index.hinc_Dias + '</td>'
                + '<td>' + index.hinc_CentroMedico + '</td>'
                + '<td>' + index.hinc_Diagnostico + '</td>'
                + '<td>' + FechaFormato(index.hinc_FechaInicio).substring(0, 10) + '</td>'
                + '<td>' + FechaFormato(index.hinc_FechaFin).substring(0, 10) + '</td>'
                + '<td>' + '<button type="button" class="btn btn-danger btn-xs" onclick="Llamarmodaldelete(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="Llamarmodaldetalle(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Detalle</button>' + '</td>'
                + '</tr>' + '</tbody>'
        '</table>'


    });
    return div + '</div></div>';
}








function llenarTabla() {
    _ajax(null,
       '/HistorialIncapacidades/llenarTabla',
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

var idEmpleado = 0;

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        idEmpleado = id
        hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/HistorialIncapacidades/ChildRowData',
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


function Llamarmodaldelete(ID) {

    var modalnuevo = $("#ModalInhabilitar");
    $("#ModalInhabilitar").find("#hinc_Id").val(ID);
    modalnuevo.modal('show');


}

function Llamarmodaldetalle(ID) {
    debugger
    var modalnuevo = $("#ModalDetalles");
    _ajax({ ID: parseInt(ID) },
        '/HistorialIncapacidades/Edit/',
        'GET',
        function (obj) {

            if (obj != "-1" && obj != "-2" && obj != "-3") {
                //$("#ModalDetalles").find("#emp_Id")["0"].innerText = obj.NombreCompleto;
                $("#ModalDetalles").find("#hinc_Dias")["0"].innerText = obj.hinc_Dias;
                $("#ModalDetalles").find("#tbTipoIncapacidades_ticn_Descripcion")["0"].innerText = obj.tbTipoIncapacidades.ticn_Descripcion;
                $("#ModalDetalles").find("#hinc_CentroMedico")["0"].innerText = obj.hinc_CentroMedico;
                $("#ModalDetalles").find("#hinc_Diagnostico")["0"].innerText = obj.hinc_Diagnostico;
                $("#ModalDetalles").find("#hinc_FechaInicio")["0"].innerText = FechaFormato(obj.hinc_FechaInicio).substring(0, 10);
                $("#ModalDetalles").find("#hinc_FechaFin")["0"].innerText = FechaFormato(obj.hinc_FechaFin).substring(0, 10);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#hinc_FechaCrea")["0"].innerText = FechaFormato(obj.hinc_FechaCrea).substring(0, 10);
                //$("#ModalDetalles").find("#hinc_UsuarioCrea")["0"].innerText = obj.hinc_UsuarioCrea;
                //$("#ModalDetalles").find("#hinc_UsuarioModifica")["0"].innerText = obj.hinc_UsuarioModifica;
                //$("#ModalDetalles").find("#hinc_FechaModifica")["0"].innerText = FechaFormato(obj.hinc_FechaModifica).substring(0, 10);
                $('#ModalDetalles').modal('show');

            }
        });

}


//id = ID;
//_ajax(null,
//    '/Cargos/Edit/' + ID,
//    'GET',
//    function (obj) {
//        if (obj != "-1" && obj != "-2" && obj != "-3") {
//            $("#ModalDetalles").find("#car_Descripcion")["0"].innerText = obj.car_Descripcion;
//            $("#ModalDetalles").find("#car_Estado")["0"].innerText = obj.car_Estado;
//            $("#ModalDetalles").find("#car_RazonInactivo")["0"].innerText = obj.car_RazonInactivo;
//            $("#ModalDetalles").find("#car_FechaCrea")["0"].innerText = FechaFormato(obj.car_FechaCrea);
//            $("#ModalDetalles").find("#car_FechaModifica")["0"].innerText = FechaFormato(obj.car_FechaModifica);
//            $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
//            $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
//            $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
//            $('#ModalDetalles').modal('show');
//        }









function Llamarmodalcreate() {

    var modalnuevo = $("#ModalNuevo");
    $("#ModalNuevo").find("#emp_Id").val(idEmpleado);
    modalnuevo.modal('show');
}









$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialIncapacidades: data });
        _ajax(data,
            '/HistorialIncapacidades/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["emp_Id", "ticn_Id", "hinc_Dias", "hinc_CentroMedico", "hinc_Doctor", "hinc_Diagnostico", "hinc_FechaInicio", "hinc_FechaFin"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    debugger
    if (data != null) {
        data = JSON.stringify({ tbHistorialIncapacidades: data });
        _ajax(data,
            '/HistorialIncapacidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    debugger
                    llenarTabla();
                    LimpiarControles(["hinc_Id", "hinc_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});


