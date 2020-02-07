var eqem_Id = 0;
var emp_Id = 0;
$(document).ready(function () {
    llenarTabla();
    RefreshEquipos();
});
function format(obj, emp_Id) {
    var div = '<div class="ibox"><div class="ibox-title"><strong class="mr-auto m-l-sm">Equipo de trabajo</strong><div class="btn-group pull-right">' +
        '<button id = "btnAgregar" data-id="' + emp_Id + '" data-toggle="ModalNuevo" class="btn btn-outline btn-primary btn-xs" onClick = "ShowModalCreate(this)"> Asignar Equipo </button>' +
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
                  '<button id = "btnDetalle" data-id="' + index.eqem_Id + '" data-toggle="ModalInactivar" class="btn btn-primary btn-xs pull-right" onClick = "Inactivar(this)"> Inactivar </button>' +
                '</div>' +
              '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    var validacionPermiso = userModelState("EquipoEmpleados/Index");
    if (validacionPermiso.status == true) {
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
                        emp_Id: value.emp_Id,
                        ID: value.emp_Id,
                        Empleado: value.Empleado,
                        Correo: value.Correo,
                        Telefono: value.Telefono
                    })
            });
            tabla.draw();
        });
    }
}
function ShowModalCreate(btn) {
    var validacionPermiso = userModelState("EquipoEmpleados/Create");
    if (validacionPermiso.status == true) {
        emp_Id = $(btn).data('id');
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#eqtra_Id").focus();
        $(modalnuevo).find("#eqtra_Id").val("");
        $(modalnuevo).find("#eqem_Fecha").val("");
    }    
}

function RefreshEquipos() {
    $("#ModalNuevo").find("#eqtra_Id").empty()

    _ajax(null, '/EquipoEmpleados/RefreshEquipos', 'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalNuevo").find("#eqtra_Id").append(
                        '<option value="">' + "**Seleccione una opción**" + '</option>'
                    );
                obj.forEach(function (index, value) {
                    $("#ModalNuevo").find("#eqtra_Id").append(
                        '<option value="' + index.eqtra_Id + '">' + index.eqtra_Descripcion + '</option>'
                    );
                });
            }
            else {
                MsgError("Error", "Código:" + obj + ". contacte al administrador. (Verifique si el registro ya existe)");
            }
        });
}

$("#btnGuardar").click(function () {
        var data = $("#FormNuevo").serializeArray();
        data = serializar(data);
        data.emp_Id = emp_Id;
        if (data != null) {
            data = JSON.stringify({ tbEquipoEmpleados: data });
            _ajax(data, '/EquipoEmpleados/Create', 'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        CierraPopups();
                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                        LimpiarControles(["eqtra_Id", "eqem_Fecha"]);
                        $("#ModalNuevo").find("#eqtra_Id").empty();
                        llenarTabla();
                        RefreshEquipos();
                    }
                    else {
                        MsgError("Error", "Error", "No se agregó el registro, contacte al administrador.");
                    }
                });
        }
        else {
            MsgError("Error", "Por favor llene todas las cajas de texto.");
        }
})

function Inactivar(btn) {
    var validacionPermiso = userModelState("EquipoEmpleados/Delete");
    if (validacionPermiso.status == true) {
        Id = $(btn).data('id');
        $("#FormInactivar input").val(Id);
        CierraPopups();
        $('#ModalInactivar').modal('show');
    }    
};

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbEquipoEmpleados: data });
        _ajax(data,
            '/EquipoEmpleados/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    setTimeout(function () { window.location.href = "/EquipoEmpleados/Index"; }, 1000);
                }
                else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                }
            });
    }
});


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
                    row.child(format(obj, id)).show();
                    tr.addClass('shown');
                } else {
                    row.child.hide();
                    tr.removeClass('shown');
                    //dibuja el childRow
                    row.child("Error de conexion").show();
                    tr.addClass('shown');
                }
            });
        RefreshEquipos();
    }
});
$(document).on("click", "#IndexTable tbody tr td buttton#btnAgregar", function () {
    var Id = $(this).data('id');
    
})