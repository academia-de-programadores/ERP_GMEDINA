$(document).ready(function () {
    llenarTabla();
});

var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoMonedas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#tmon_Descripcion").val(obj.tmon_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoMonedas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#tmon_Descripcion")["0"].innerText = obj.tmon_Descripcion;
                $("#ModalDetalles").find("#tmon_FechaCrea")["0"].innerText = FechaFormato(obj.tmon_FechaCrea);
                $("#ModalDetalles").find("#tmon_FechaModifica")["0"].innerText = FechaFormato(obj.tmon_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoMonedas/llenarTabla',
        'POST',
        function (Lista) {
            var tabla = $("#IndexTable").DataTable();
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.tmon_Descripcion);
                tabla.row.add({
                    ID:value.tmon_Id,
                    Moneda:value.tmon_Descripcion
                }).draw();
            });
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#tmon_Descripcion").val("");
    $(modalnuevo).find("#tmon_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoMonedas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#tmon_Descripcion").val(obj.tmon_Descripcion);
                $("#ModalEditar").find("#tmon_Descripcion").focus();
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#tmon_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#tmon_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoMonedas: data });
        _ajax(data,
            '/TipoMonedas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tmon_Descripcion"]);
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
        data.tmon_Id = id;
        data = JSON.stringify({ tbTipoMonedas: data });
        _ajax(data,
            '/TipoMonedas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tmon_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inhabilitar el registro, contacte al administrador");
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
        data.tmon_Id = id;
        data = JSON.stringify({ tbTipoMonedas: data });
        _ajax(data,
            '/TipoMonedas/Edit',
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