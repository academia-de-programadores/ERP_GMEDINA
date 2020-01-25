function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Audiencias de Descargo</h5> <div align=right><button type="button" class="btn btn-primary btn-xs" onclick="Llamarmodalcreate(' + idEmpleado + ')">Registrar</button> </div> </div><div class="ibox-content"><div class="row">'
        + '<table class="table table-striped table-bordered table-hover dataTables-example" >'
        + '<thead>'
        + '<tr> <th>  Motivo  </th>'
        + '<th>Fecha</th>'
        + '<th>Testigo</th> '
        + '<th>Estado</th> '
         + '<th>Acciones</th> '
        + '</tr> </thead> ';
    obj.forEach(function (index, value) {
        var testigo = "";
        if (index.aude_Testigo == false)
            testigo = "No";
        else
            testigo = "Si";
        var Estado = "";
        if (index.aude_Estado == false)
            Estado = "Inactivo";
        else
            Estado = "Activo";
        div = div +
            '<tbody>' + '<tr>'
                + '<td>' + index.aude_Descripcion + '</td>'
                + '<td>' + FechaFormato(index.aude_FechaAudiencia).substring(0, 10) + '</td>'
                + '<td>' + testigo + '</td>'
               + '<td>' + Estado + '</td>'
        + '<td>';
        if (index.aude_Estado) {
            div += '<button type="button" class="btn btn-danger btn-xs" onclick="Llamarmodaldelete(' + index.aude_Id + ')" data-id="@item.cin_IdIngreso">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="Llamarmodaldetalle(' + index.aude_Id + ')" data-id="@item.cin_IdIngreso">Detalle</button>';
        }
        else {
            div += '<button type="button" class="btn btn-outline btn-primary btn-xs" onclick="llamarmodalhabilitar(' + index.aude_Id + ')" data-id="@item.cin_IdIngreso">Activar</button> <button type="button" class="btn btn-outline btn-primary btn-xs" onclick="Llamarmodaldetalle(' + index.aude_Id + ')" data-id="@item.cin_IdIngreso">Detalle</button>' + '</td>';
        }
        div += '</tr>' + '</tbody>'
        '</table>'
    });
    return div + '</div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/HistorialAudienciaDescargos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           if (validarDT(Lista)) {
               return null;
           }
           $.each(Lista, function (index, value) {
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
        idEmpleado = id
        hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/HistorialAudienciaDescargos/ChildRowData',
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


var idEditar = 0;

function Llamarmodaldetalle(ID) {
    idEditar = ID;
    var modalnuevo = $("#ModalDetalles");
    _ajax({ ID: parseInt(ID) },
        '/HistorialAudienciaDescargos/Edit/',
        'GET',
        function (obj) {

            if (obj != "-1" && obj != "-2" && obj != "-3") {
                //$("#ModalDetalles").find("#emp_Id")["0"].innerText = obj.NombreCompleto;
                $("#ModalDetalles").find("#aude_Descripcion")["0"].innerText = obj.aude_Descripcion;
                $("#ModalDetalles").find("#aude_FechaAudiencia")["0"].innerText = FechaFormato(obj.aude_FechaAudiencia).substring(0, 10);
                $("#ModalDetalles").find("#aude_Testigo")["0"].innerText = obj.aude_Testigo ? 'Si' : 'No';
                //$("#ModalDetalles").find("#aude_DireccionArchivo")["0"].innerText = obj.aude_DireccionArchivo;
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#aude_FechaCrea")["0"].innerText = FechaFormato(obj.aude_FechaCrea);
                //$("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#aude_FechaModifica")["0"].innerText = FechaFormato(obj.aude_FechaModifica).substring(0, 10);
                //$("#ModalDetalles").find("#hinc_FechaModifica")["0"].innerText = FechaFormato(obj.hinc_FechaModifica).substring(0, 10);
                $('#ModalDetalles').modal('show');

            }
        });

}

function Llamarmodalcreate() {

    var modalnuevo = $("#ModalNuevo");
    $("#aude_FechaAudiencia1").attr("min", Fecha());
    $("#ModalNuevo").find("#emp_Id").val(idEmpleado);
    modalnuevo.modal('show');
}

function compare_dates() {
    debugger
    var fecha1 = $("#ModalNuevo").find("#aude_FechaAudiencia").val();
    var fechalimite = '01/01/1900';
    if (Date.parse(fecha1) < Date.parse(fechalimite)) {
        $("#ModalNuevo").show();
        MsgError("Error", "Fecha no es valida");
    }
    else if ( fecha1 == "")
    {
        $("#ModalNuevo").show();
        MsgError("Error", "Campo fecha es requerido.");
    }
    else {
        return true;
    }
}



$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    data.aude_Testigo = $("#ModalNuevo").find("#aude_Testigo").val();
    if(compare_dates())
    if (data != null) {
        data = JSON.stringify({ tbHistorialAudienciaDescargo: data });
        if (compare_dates()) {
            _ajax(data,
                '/HistorialAudienciaDescargos/Create',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        CierraPopups();
                        llenarTabla();
                        // LimpiarControles(["aude_Descripcion", "aude_FechaAudiencia", "aude_DireccionArchivo", "emp_Id"]);
                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                        $("#ModalNuevo").find("#aude_FechaAudiencia").val("");
                        $("#ModalNuevo").find("#aude_Descripcion").val("");
                        $("#ModalNuevo").find("#aude_Testigo").prop("checked", false);
                        $("#ModalNuevo").find("#emp_Id").val("");
                    } else {
                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                    }
                })
        };
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});



function Llamarmodaldelete(ID) {

    var modalnuevo = $("#ModalInactivar");
    $("#ModalInactivar").find("#aude_Id").val(ID);
    idEditar = ID;
    modalnuevo.modal('show');
}


$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    //data = data.aude_Id.idEditar;
    if (data != null) {
        data = JSON.stringify({ tbHistorialAudienciaDescargo: data });
        _ajax(data,
            '/HistorialAudienciaDescargos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["aude_Id"]);
                    MsgSuccess("¡Éxito!", "El registro se ha inactivado de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto");
    }
});

$("#btnEditar").click(function (ID) {
    CierraPopups();
    var modalnuevo = $("#ModalEditar");
    $("#aude_FechaAudiencia2").attr("min", Fecha());
    $("#ModalEditar").find("#aude_Id").val(idEditar);
    _ajax({ ID: parseInt(idEditar) },
    '/HistorialAudienciaDescargos/Edit/',
    'GET',
    function (obj) {

        if (obj != "-1" && obj != "-2" && obj != "-3") {
            $("#ModalEditar").find("#aude_FechaAudiencia2").val(FechaFormatoSimple(obj.aude_FechaAudiencia).substring(0, 10));
        }
    });
    modalnuevo.modal('show');
});

$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAudienciaDescargo: data });
        _ajax(data,
            '/HistorialAudienciaDescargos/Edit2',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa");
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});

function Fecha() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return today = yyyy + '-' + mm + '-' + dd;
}
