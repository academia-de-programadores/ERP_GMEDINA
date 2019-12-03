$(document).ready(function () {
    llenarTabla();
});
var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoAmonestaciones/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#tamo_Descripcion").val(obj.tamo_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoAmonestaciones/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#tamo_Descripcion")["0"].innerText = obj.tamo_Descripcion;
                $("#ModalDetalles").find("#tamo_Estado")["0"].innerText = obj.tamo_Estado;
                $("#ModalDetalles").find("#tamo_RazonInactivo")["0"].innerText = obj.tamo_RazonInactivo;
                $("#ModalDetalles").find("#tamo_FechaCrea")["0"].innerText = FechaFormato(obj.tamo_FechaCrea);
                $("#ModalDetalles").find("#tamo_FechaModifica")["0"].innerText = FechaFormato(obj.tamo_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoAmonestaciones/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                tabla.row.add({
                    ID:value.tamo_Id,
                    Descripcion:value.tamo_Descripcion
                   }).draw();
            });
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#tamo_Descripcion").val("");
    modalnuevo.modal('show');
    $("#FormNuevo").find("#tamo_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoAmonestaciones/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#tamo_Descripcion").val(obj.tamo_Descripcion);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#tamo_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#tamo_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
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
        data.tamo_Id = id;
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tamo_Descripcion", "tamo_RazonInactivo"]);
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
        data.tamo_Id = id;
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Edit',
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