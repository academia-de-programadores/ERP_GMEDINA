var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoHoras/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
               // $("#FormEditar").find("#tiho_Id").val(obj.habi_Descripcion);
                $("#FormEditar").find("#tiho_Descripcion").val(obj.tiho_Descripcion);
                $("#FormEditar").find("#tiho_Recargo").val(obj.tiho_Recargo);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoHoras/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetallesR").find("#tiho_Descripcion")["0"].innerText = obj.tiho_Descripcion;
                $("#ModalDetallesR").find("#tiho_Recargo")["0"].innerText = obj.tiho_Recargo;
                $("#ModalDetallesR").find("#tiho_Estado")["0"].innerText = obj.tiho_Estado;
                $("#ModalDetallesR").find("#tiho_FechaCrea")["0"].innerText = FechaFormato(obj.tiho_FechaCrea);
                $("#ModalDetallesR").find("#tiho_FechaModifica")["0"].innerText = FechaFormato(obj.tiho_FechaModifica);
                $("#ModalDetallesR").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesR").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoHoras/llenarTabla',
        'POST',
        function (Lista) {
            var IndexTable = $('#IndexTable').DataTable();
            IndexTable.clear();
            IndexTable.draw();
            $.each(Lista, function (index, value) {
                //console.log(item.tiho_Descripcion);
                IndexTable.row.add(['<tr data-id = "' + item.tiho_Id + '">' +
                    item.tiho_Descripcion, item.tiho_Recargo,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<button type='button' class='btn btn-primary btn-xs tablaDetalle' id='btnDetalle' data-toggle='modal' onclick='tablaDetalle(" + item.tiho_Id + ")' data-target='#ModalDetalles'>Detalle</button>" +
                        "<button type='button' class='btn btn-default btn-xs tablaEditar' id='btnEditarR' data-toggle='modal' onclick='tablaEditar(" + item.tiho_Id + ")' data-target='#ModalEditar'>Editar</button>" +
                    "</div>"]).draw();
            });
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $("#FormNuevo").find("#tiho_Descripcion").val("");
    $("#FormNuevo").find("#tiho_Recargo").val(0);
    $("#FormNuevo").find("#tiho_Descripcion").focus();
    
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoHoras/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#tiho_Descripcion").val(obj.tiho_Descripcion);
                $("#FormEditar").find("#tiho_Recargo").val(obj.tiho_Recargo);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#habi_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#habi_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    //data = parseInt(data.tiho_Recargo);
    data = serializar(data);
    data.tiho_Recargo = parseInt(data.tiho_Recargo);
    if (data != null) {
        data = JSON.stringify({ tbTipoHoras: data }),
        _ajax(data,
            '/TipoHoras/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tiho_Descripcion", "tiho_Recargo"]);
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
    if (data != null) {
        data.tiho_Id = id;
        data = JSON.stringify({ tbTipoHoras: data });
        _ajax(data,
            '/TipoHoras/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tiho_Descripcion", "tiho_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
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
        data.tiho_Id = id;
        data = JSON.stringify({ tbTipoHoras: data });
        _ajax(data,
            '/TipoHoras/Edit',
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