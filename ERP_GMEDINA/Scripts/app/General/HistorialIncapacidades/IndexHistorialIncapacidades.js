
var fill = 0;

$(document).ready(function () {
    //fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

function format(obj) {


    var div = '<div class="ibox"><div class="ibox-title"><h5>Incapacidades</h5> <div align=right><button href="Create" type="button" class="btn btn-primary btn-xs" onclick="tablaEditar(' + idEmpleado + ')" id="nuevo" data-id="@item.cin_IdIngreso">Nueva Incapacidad</button> </div> </div><div class="ibox-content"><div class="row">'
        + '<table class="table table-striped table-bordered table-hover dataTables-example" >'
        + '<thead>'
        + '<tr> <th> Número   </th>'
        + '<th>Incapacidad</th>'
        + '<th>Días de retiro</th>'
        + '<th>Centro Medico</th> '
         + '<th>Diagnostico</th> '
         + '<th>Fecha Inicio</th> '
         + '<th>Fecha Fin</th> '
         + '<th>Estado</th> '
         + '<th>Acciones</th> '
        + '</tr> </thead> ';
    obj.forEach(function (index, value) {
        //var mostrarboton = index.hinc_Estado == 1 ? null : '<button type="button" class="btn btn-primary btn-xs" onclick="Llamarmodalhabilitar(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Habilitar</button>';
        //if (value.hinc_Estado > fill) {
        var Estado = "";
        if (index.hinc_Estado == false)
            Estado = "Inactivo";
        else
            Estado = "Activo";
        div = div +
            '<tbody>' + '<tr>'
                + '<td>' + index.hinc_Id + '</td>'
                + '<td>' + index.ticn_Descripcion + '</td>'
                + '<td>' + index.hinc_Dias + '</td>'
                + '<td>' + index.hinc_CentroMedico + '</td>'
                + '<td>' + index.hinc_Diagnostico + '</td>'
                + '<td>' + FechaFormato(index.hinc_FechaInicio).substring(0, 10) + '</td>'
                + '<td>' + FechaFormato(index.hinc_FechaFin).substring(0, 10) + '</td>'
                + '<td>' + Estado + '</td>'
                + '<td>';
        if (index.hinc_Estado) {
            div += '<button type="button" class="btn btn-danger btn-xs" onclick="Llamarmodaldelete(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="Llamarmodaldetalle(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Detalle</button>';
        }
        else {
            div += '<button type="button" class="btn btn-primary btn-xs" onclick="Llamarmodalhabilitar(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Activar</button> <button type="button" class="btn btn-default btn-xs" onclick="Llamarmodaldetalle(' + index.hinc_Id + ')" data-id="@item.cin_IdIngreso">Detalle</button>' + '</td>';
        }
        div += '</tr>' +
                  '</tbody>'
        '</table>'

        //}
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
               empleado = value.Empleado
               tabla.row.add({
                   Id: value.emp_Id,
                   "Número": value.emp_Id,
                   Empleado: value.Empleado,
                   Cargo: value.Cargo,
                   Departamento: value.Departamento
               });
           });
           tabla.draw();
       });
}




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









//function Llamarmodalcreate() {

//    var modalnuevo = $("#ModalNuevo");
//    $("#ModalNuevo").find("#emp_Id").val(idEmpleado);
//    modalnuevo.modal('show');
//}


function tablaEditar(ID) {
    id = ID;
    debugger
    sessionStorage.setItem("IdPersona", ID);
    window.location.href = "/HistorialIncapacidades/Create";

    //_ajax(null,
    //    '/Personas/Edit/',
    //    'GET',
    //    function (obj) {
    //        if (obj != "-1" && obj != "-2" && obj != "-3") {
    //        }
    //    });
}



//$("#btnGuardar").click(function () {
//    var data = $("#FormNuevo").serializeArray();
//    data = serializar(data);
//    if (data != null) {
//        data = JSON.stringify({ tbHistorialIncapacidades: data });
//        _ajax(data,
//            '/HistorialIncapacidades/Create',
//            'POST',
//            function (obj) {
//                if (obj != "-1" && obj != "-2" && obj != "-3") {
//                    CierraPopups();
//                    llenarTabla();
//                    LimpiarControles(["emp_Id", "ticn_Id", "hinc_Dias", "hinc_CentroMedico", "hinc_Doctor", "hinc_Diagnostico", "hinc_FechaInicio", "hinc_FechaFin"]);
//                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
//                } else {
//                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
//                }
//            });
//    } else {
//        MsgError("Error", "por favor llene todas las cajas de texto");
//    }
//});






$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialIncapacidades: data });
        _ajax(data,
            '/HistorialIncapacidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalInhabilitar").modal("hide");
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["hinc_Id"]);
                    MsgWarning("¡Éxito!", "El registro se ha inactivado de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});





Admin = true;


function Llamarmodalhabilitar(ID) {

    var modalhabilitar = $("#ModalHabilitar");
    Id = $("#ModalHabilitar").find("#hinc_Id").val(ID);
    modalhabilitar.modal('show');


}



//Esta funcion llama al modal de Habilitar
//function hablilitar(btn) {
//    var tr = $(btn).closest('tr');
//    var row = tabla.row(tr);
//    var id = row.data().ID;
//    $("#txtIdRestore").val(id);
//    $('#ModalHabilitar').modal('show');
//}

//Cambiar el controlador para ejecutar el UDP de restaurar




//$("#btnActivar").click(function () {

//    debugger
//    var data = $("#FormActivar").serializeArray();
//    data = serializar(data);
//    _ajax(JSON.stringify({ id: data }), // <<<<<<===================================
//        '/HistorialIncapacidades/habilitar/',
//        'POST',
//        function (obj) {
//            if (obj != "-1" && obj != "-2" && obj != "-3") {
//                MsgWarning("¡Exito!", "Se ah Activado el registro");
//                llenarTabla(-1);
//            }
//          else {
//          MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
//}
//        });


//});


