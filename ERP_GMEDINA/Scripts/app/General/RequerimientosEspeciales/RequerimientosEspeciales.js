
var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/RequerimientosEspeciales/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#resp_Descripcion").val(obj.resp_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/RequerimientosEspeciales/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#resp_Descripcion")["0"].innerText = obj.resp_Descripcion;
              
                $("#ModalDetalles").find("#resp_FechaCrea")["0"].innerText = FechaFormato(obj.resp_FechaCrea);
                $("#ModalDetalles").find("#resp_FechaModifica")["0"].innerText = FechaFormato(obj.resp_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/RequerimientosEspeciales/llenarTabla',
        'POST',
        function (Lista) {
            var tabla = $("#IndexTable").DataTable();
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                console.log(value.resp_Descripcion);
                tabla.row.add({
                    ID: value.resp_Id,
                    Descripción: value.resp_Descripcion,
                }).draw();
            });
        });
}
$(document).ready(function () {
    llenarTabla();
});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#resp_Descripcion").val("");
    $(modalnuevo).find("#resp_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/RequerimientosEspeciales/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#resp_Descripcion").val(obj.resp_Descripcion);
                $("#ModalEditar").find("#resp_Descripcion").focus();
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#resp_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#resp_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbRequerimientosEspeciales: data });
        _ajax(data,
            '/RequerimientosEspeciales/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["resp_Descripcion", "resp_RazonInactivo"]);
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
        data.habi_Id = id;
        data = JSON.stringify({ tbRequerimientosEspeciales: data });
        _ajax(data,
            '/RequerimientosEspeciales/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["resp_Descripcion", "resp_RazonInactivo"]);
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
        data.habi_Id = id;
        data = JSON.stringify({ tbRequerimientosEspeciales: data });
        _ajax(data,
            '/RequerimientosEspeciales/Edit',
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