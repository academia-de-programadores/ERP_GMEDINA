$(document).ready(function () {
    llenarTabla();
});

var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/RazonSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#rsal_Descripcion").val(obj.rsal_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/RazonSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#rsal_Descripcion")["0"].innerText = obj.rsal_Descripcion;             
                $("#ModalDetalles").find("#rsal_FechaCrea")["0"].innerText = FechaFormato(obj.rsal_FechaCrea);
                $("#ModalDetalles").find("#rsal_FechaModifica")["0"].innerText = FechaFormato(obj.rsal_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/RazonSalidas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                tabla.row.add({
                    ID:value.rsal_Id,
                Razon:value.rsal_Descripcion
            }).draw();
            });
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#rsal_Descripcion").val("");
    modalnuevo.modal('show');
    $("#FormNuevo").find("#rsal_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/RazonSalidas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#rsal_Descripcion").val(obj.rsal_Descripcion);
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#rsal_RazonInactivo").val("");
    $("#ModalInactivar").find("#rsal_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
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
        data.rsal_Id = id;
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["rsal_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró Inactivar el registro, contacte al administrador");
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
        data.rsal_Id = id;
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Edit',
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