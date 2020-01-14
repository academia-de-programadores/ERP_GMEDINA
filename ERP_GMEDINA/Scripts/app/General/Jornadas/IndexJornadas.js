var jor_Id = 0;
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
           if (validarDT(Lista)) {
               return null;
           }
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   ID: value.jor_Id,
                   Jornada: value.jor_Descripcion
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
                $("#ModalDetalles").find("#jor_FechaCrea")["0"].innerText = FechaFormato(obj.jor_FechaCrea);
                $("#ModalDetalles").find("#jor_FechaModifica")["0"].innerText = FechaFormato(obj.jor_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;                
                $('#ModalDetalles').modal('show');
            }
        });
}
function format(obj, jor_Id) {
    var div = '<div class="ibox"><div class="ibox-title"><strong class="mr-auto m-l-sm">Horarios</strong><div class="btn-group pull-right">' +
        '<button id = "btnAgregarHorarios" data-id="' + jor_Id + '" data-toggle="ModalNuevoHorarios" class="btn btn-outline btn-primary btn-xs" onClick = "showmodal(this)"> Agregar </button>' +
        '</div></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
              '<div class="panel panel-default">' +
                '<div class="panel-heading">' +
                  '<h5>' + index.hor_descripcion + '</h5>' +
                '</div>' +
                '<div class="panel-body">' +
                  'Hora Inicio: ' + index.hor_HoraInicio.toString() +
                  '<br> Hora Fin: ' + index.hor_HoraFin +
                '</div>' +
                '<div class="modal-footer">' +
                  '<button id = "btnDetalleHorarios" data-id="' + index.hor_Id + '" data-toggle="ModalDetallesHorario" class="btn btn-primary btn-xs pull-right" onClick = "showmodalDetalle(this)"> Detalle </button>' +
                  '<button id = "btnEditarHorarios" data-id="' + index.hor_Id + '" data-toggle="ModalEditarHorarios" class="btn btn-defaults btn-xs pull-right" onClick = "showmodaledit(this)"> Editar </button>' +
                '</div>' +
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
                    row.child(format(obj, id)).show();
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

function showmodal(btn) {
    jor_Id = $(btn).data('id');
    var modalnuevo = $('#ModalNuevoHorarios');
    modalnuevo.modal('show');    
    $(modalnuevo).find("#hor_Descripcion").val("");
    $(modalnuevo).find("#hor_Descripcion").focus();
    $(modalnuevo).find("#hor_HoraInicio").val("");
    $(modalnuevo).find("#hor_HoraFin").val("");
}
function showmodaledit(btn) {
    jor_Id = $(btn).data('id');
    _ajax(null,
        '/Jornadas/EditHorario/' + jor_Id,
        'GET',        
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                var hor_HoraInicio = obj.hor_HoraInicio.Hours + ":" + obj.hor_HoraInicio.Minutes + obj.hor_HoraInicio.Seconds;
                var hor_HoraFin = obj.hor_HoraFin.Hours + ":" + obj.hor_HoraFin.Minutes + obj.hor_HoraFin.Seconds;
                $('#ModalEditarHorarios').modal('show');
                $("#ModalEditarHorarios").find("#hor_Descripcion").val(obj.hor_Descripcion);
                $("#ModalEditarHorarios").find("#hor_Descripcion").focus();
                $("#ModalEditarHorarios").find("#hor_HoraInicio").val(hor_HoraInicio);
                $("#ModalEditarHorarios").find("#hor_HoraFin").val(hor_HoraFin);
            }
        });
}
function showmodalDelete(btn) {
    jor_Id = $(btn).data('id');
    var modalnuevo = $('#ModalInhabilitarHorario');
    modalnuevo.modal('show');
    $("#ModalEditarHorarios").modal('hide');//ocultamos el modal
    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
    $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    $(modalnuevo).find("#hor_RazonInactivo").val("");
    $(modalnuevo).find("#hor_RazonInactivo").focus();
}
function showmodalDetalle(btn) {
    jor_Id = $(btn).data('id');
    _ajax(null,
        '/Jornadas/EditHorario/' + jor_Id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();                
                var hor_HoraInicio = obj.hor_HoraInicio.Hours + ":" + obj.hor_HoraInicio.Minutes + obj.hor_HoraInicio.Seconds;
                var hor_HoraFin = obj.hor_HoraFin.Hours + ":" + obj.hor_HoraFin.Minutes + obj.hor_HoraFin.Seconds;
                $('#ModalDetallesHorario').modal('show');
                $("#ModalDetallesHorario").find("#hor_Descripcion")["0"].innerText = obj.hor_Descripcion;                
                $("#ModalDetallesHorario").find("#hor_HoraInicio")["0"].innerText = hor_HoraInicio;
                $("#ModalDetallesHorario").find("#hor_HoraFin")["0"].innerText = hor_HoraFin;
                $("#ModalDetallesHorario").find("#hor_CantidadHoras")["0"].innerText = obj.hor_CantidadHoras;                
                $("#ModalDetallesHorario").find("#hor_FechaCrea")["0"].innerText = FechaFormato(obj.hor_FechaCrea);
                $("#ModalDetallesHorario").find("#hor_FechaModifica")["0"].innerText = FechaFormato(obj.hor_FechaModifica);
                $("#ModalDetallesHorario").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesHorario").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;                
                $('#ModalDetallesHorario').modal('show');
            }
        });
}


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
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#btnGuardarHorario").click(function () {
    var data = $("#FormNuevoHorario").serializeArray();
    data = serializar(data);
    data.jor_Id = jor_Id;
    if (data != null) {
        data = JSON.stringify({ tbHorarios: data });
        _ajax(data,
            '/Jornadas/CreateHorario',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $('#btnCerrarModal').click();
                    llenarTabla();
                    LimpiarControles(["hor_Descripcion","hor_HoraInicio","hor_HoraFin"]);
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
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
                    MsgSuccess("¡Exito!", "El registro se editó de forma exitosa");
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#btnActualizarHorario").click(function () {
    var data = $("#FormEditarHorarios").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.hor_Id = id;
        data = JSON.stringify({ tbHorarios: data });
        _ajax(data,
            '/Jornadas/EditHorario',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalEditarHorarios").modal('hide');//ocultamos el modal
                    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
                    $('.modal-backdrop').remove();//eliminamos el backdrop del modal
                    llenarTabla();
                    MsgSuccess("¡Exito!", "El registro se editó de forma exitosa");
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador");
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
                    MsgWarning("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inhabilitar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#InActivarHorario").click(function () {
    var data = $("#FormInactivarHorario").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.hor_Id = id;
        data = JSON.stringify({ tbHorarios: data });
        _ajax(data,
            '/Jornadas/DeleteHorario',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalInhabilitarHorario").modal('hide');//ocultamos el modal
                    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
                    $('.modal-backdrop').remove();//eliminamos el backdrop del modal
                    llenarTabla();
                    LimpiarControles(["hor_Descripcion", "hor_RazonInactivo"]);
                    MsgWarning("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inhabilitar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});


$(document).on("click", "#IndexTable tbody tr td button#btnAgregarHorarios", function () {
    var Id = $(this).data('id');
    console.log(Id);
})
