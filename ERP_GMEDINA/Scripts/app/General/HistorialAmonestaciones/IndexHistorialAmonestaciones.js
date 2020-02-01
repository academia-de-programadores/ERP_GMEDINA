var fill = 0;
var UltimaAmonestacion;
var i = 0;
function format(obj) {
    UltimaAmonestacion = null;
    var div = '<div class="ibox"><div class="ibox-title"><h5>Amonestaciones</h5><div align=right> <button type="button" class="btn btn-primary btn-xs" onclick="llamarmodal(' + IdEmpleado + ')">Agregar Amonestación</button> <button type="button" class="btn btn-primary btn-xs" id="btnAudienciaDescargo" onclick="redireccionar()">Audiencia Descargo</button></div></div><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                '<th>' + 'Tipo Amonestación' + '</th>' +
                '<th>' + 'Fecha' + '</th>' +
                '<th>' + 'Obsevarción' + '</th>' +
                 '<th>' + 'Estado' + '</th>' +
                '<th>' + 'Acciones' + '</th>' +
                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        var Estado = "";
        if (index.hamo_Estado == false)
            Estado = "Inactivo";
        else
            Estado = "Activo";
        div = div +
                '<tbody>' +
                '<tr>' +
                '<td>' + index.tamo_Descripcion + '</td>' +
                '<td>' + FechaFormato(index.hamo_Fecha).substring(0, 10) + '</td>' +
                '<td>' + index.hamo_Observacion + '</td>' +
                '<td>' + Estado + '</td>' +
                '<td>';
        debugger
        if(i == 0)
        {
            UltimaAmonestacion = index.hamo_Fecha;
        }
        else if (UltimaAmonestacion < index.hamo_Fecha)
        {
            UltimaAmonestacion = index.hamo_Fecha;
        }
        i++;
        if (index.hamo_Estado)
        {
            div += ' <button type="button" class="btn btn-danger btn-xs" onclick="llamarmodaldelete(' + index.hamo_Id + ')" data-id="@item.hamo_Id">Inactivar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles(' + index.hamo_Id + ')"data-id="@item.hamo_Id">Detalles</button>';
        }
        else
            {
            div += '<button type="button" class="btn btn-primary btn-xs" onclick="llamarmodalhabilitar(' + index.hamo_Id + ')"data-id="@item.hamo_Id">Activar</button> <button type="button" class="btn btn-default btn-xs" onclick="llamarmodaldetalles(' + index.hamo_Id + ')"data-id="@item.hamo_Id">Detalles</button>' + '</td>';
            }
        div += '</tr>' +
                    '</tbody>'
            '</table>'
        //}
    });
    i = 0;
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
//    fill = Admin = undefined ? 0 : -1;
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
                    tr.removeClass('loading');
                    tr.addClass('shown');
                }
            });
    }
});


function llamarmodal() {
    debugger
    _ajax({ ID: parseInt(IdEmpleado) },
        '/HistorialAmonestaciones/Fecha/',
        'GET',
        function (obj) {
            debugger
            $("#msjerror").html(obj);

        });

    var modalnuevo = $("#ModalNuevo");
    $("#ModalNuevo").find("#emp_Id").val(IdEmpleado);
    modalnuevo.modal('show');
}
function llamarmodaldelete(ID) {
    var modaldelete = $("#ModalInhabilitar");
    $("#ModalInhabilitar").find("#hamo_Id").val(ID);
    modaldelete.modal('show');
}

function llamarmodaldetalles(ID) {
    var modaldetalle = $("#ModalDetalles");
    _ajax({ ID: parseInt(ID) },
        '/HistorialAmonestaciones/Edit/',
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#hamo_Observacion")["0"].innerText = obj.hamo_Observacion;
                $("#ModalDetalles").find("#tbTipoAmonestaciones_tamo_Descripcion")["0"].innerText = obj.tbTipoAmonestaciones.tamo_Descripcion;
                $("#ModalDetalles").find("#hamo_Fecha")["0"].innerText = FechaFormato(obj.hamo_Fecha);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#hamo_FechaCrea")["0"].innerText = FechaFormato(obj.hamo_FechaCrea);
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
                    $("#ModalInhabilitar").modal("hide");
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    LimpiarControles(["hamo_Id"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
function validarddl() {
    if ($("#ModalNuevo").find("#tamo_Id").val() == 0) {
        MsgError("Error", "El campo amonestación es requerido.");
    }
    else {
        return true;
    }
}

$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if(validarddl())
    if (data != null) {
        data = JSON.stringify({ tbHistorialAmonestaciones: data });
        if (validarddl()) {
            _ajax(data,
                '/HistorialAmonestaciones/Create',
                'POST',
                function (obj) {
                    debugger
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        CierraPopups();
                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                        LimpiarControles(["emp_Id", "tamo_Id", "hamo_Fecha", "hamo_Observacion"]);
                        llenarTabla();
                    } else {
                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                    }
                })
        };
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});

var Audiencia =  $("#btnAudienciaDescargo").val();
function redireccionar(Audiencia)
{
   window.location.href = "/HistorialAudienciaDescargos/Index/";
}
