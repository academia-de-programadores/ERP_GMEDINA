//function tablaDetalles(btn) {
//    var tr = $(btn).closest("tr");
//    var row = tabla.row(tr);
//    id = row.data().Id;
//    $(location).attr('href', "/Areas/Edit/" + id);
//}
//function tablaEditar(btn) {
//    var tr = $(btn).closest("tr");
//    var row = tabla.row(tr);
//    id = row.data().Id;
//    $(location).attr('href', "/Areas/Edit/" + id);
//}
//function format(obj) {
//    var div = '<div class="ibox"><div class="ibox-title"><h5>Horarios</h5></div><div class="ibox-content"><div class="row">';
//    obj.forEach(function (index, value) {
//        div = div +
//            '<div class="col-md-3">' +
//                '<div class="panel panel-default">' +
//                  '<div class="panel-heading">' +
//                     '<h5>' + index.hor_descripcion + '</h5>' +
//                '</div>' +
//                '<div class="panel-body">' + 'Hora Inicio: '
//                    + index.hor_HoraInicio.toString() + '<br> Hora Fin: ' +
//                    index.hor_HoraFin + '</div>' +
//                '</div>' +
//            '</div>'
//    });
//    return div + '</div></div></div>';
//}
//function llenarTabla() {
//    _ajax(null,
//       '/Jornadas/llenarTabla',
//       'POST',
//       function (Lista) {
//           tabla.clear();
//           tabla.draw();
//           $.each(Lista, function (index, value) {
//               tabla.row.add({
//                   ID: value.jor_Id,
//                   Descripcion: value.jor_Descripcion
//               });
//           });
//           tabla.draw();
//       });
//}
//$(document).ready(function () {
//    llenarTabla();
//});
//$('#IndexTable tbody').on('click', 'td.details-control', function () {
//    var tr = $(this).closest('tr');
//    var row = tabla.row(tr);

//    if (row.child.isShown()) {
//        row.child.hide();
//        tr.removeClass('shown');
//    }
//    else {
//        id = row.data().ID;
//        hola = row.data().hola;
//        tr.addClass('loading');
//        _ajax({ id: parseInt(id) },
//            '/Jornadas/ChildRowData',
//            'GET',
//            function (obj) {
//                if (obj != "-1" && obj != "-2" && obj != "-3") {
//                    row.child(format(obj)).show();
//                    tr.removeClass('loading');
//                    tr.addClass('shown');
//                }
//            });
//    }

//});


//function tablaDetalles(btn) {
//    var tr = $(btn).closest("tr");
//    var row = tabla.row(tr);
//    id = row.data().Id;
//    $(location).attr('href', "/Areas/Edit/" + id);
//}
//function tablaEditar(btn) {
//    var tr = $(btn).closest("tr");
//    var row = tabla.row(tr);
//    id = row.data().Id;
//    $(location).attr('href', "/Areas/Edit/" + id);
//}


$(document).ready(function () {
    llenarTabla();
});
function llenarTabla() {
    _ajax(null,
       '/Jornadas/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   ID: value.jor_Id,
                   Descripcion: value.jor_Descripcion
               });
           });
           tabla.draw();
       });
}
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Jornadas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#jor_Descripcion").val(obj.jor_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Jornadas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#jor_Descripcion")["0"].innerText = obj.jor_Descripcion;
                $("#ModalDetalles").find("#jor_Estado")["0"].innerText = obj.jor_Estado;
                $("#ModalDetalles").find("#jor_RazonInactivo")["0"].innerText = obj.jor_RazonInactivo;
                $("#ModalDetalles").find("#jor_FechaCrea")["0"].innerText = FechaFormato(obj.jor_FechaCrea);
                $("#ModalDetalles").find("#jor_FechaModifica")["0"].innerText = FechaFormato(obj.jor_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Horarios</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5>' + index.hor_descripcion + '</h5>' +
                '</div>' +
                '<div class="panel-body">' + 'Hora Inicio: '
                    + index.hor_HoraInicio.toString() + '<br> Hora Fin: ' +
                    index.hor_HoraFin + '</div>' +
                '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
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
        hola = row.data().hola;
        tr.addClass('loading');
        _ajax({ id: parseInt(id) },
            '/Jornadas/ChildRowData',
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
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#jor_Descripcion").val("");
    $(modalnuevo).find("#jor_Descripcion").focus();
})
$("#btnEditar").click(function () {
    _ajax(null,
        '/Jornadas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#jor_Descripcion").val(obj.jor_Descripcion);
                $("#ModalEditar").find("#jor_Descripcion").focus();
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#jor_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#jor_RazonInactivo").focus();
});


$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbJornadas: data });
        _ajax(data,
            '/Jornadas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["jor_Descripcion"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.jor_Id = id;
        data = JSON.stringify({ tbJornadas: data });
        _ajax(data,
            '/Jornadas/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
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
    if (data != null) {
        data.jor_Id = id;
        data = JSON.stringify({ tbJornadas: data });
        _ajax(data,
            '/Jornadas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["jor_Descripcion", "jor_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ha Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});

