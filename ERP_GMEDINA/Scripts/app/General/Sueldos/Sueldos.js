var id = 0;
//Funciones GET
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    _ajax(null,
        '/Sueldos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#emp_Id").val(obj.emp_Id);
                $("#FormEditar").find("#tmon_Id").val(obj.tmon_Id);
                $("#FormEditar").find("#sue_Cantidad").val(obj.sue_Cantidad);
                $("#FormEditar").find("#sue_SueldoAnterior").val(obj.sue_SueldoAnterior);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    _ajax(null,
        '/Sueldos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#emp_Id")["0"].innerText = obj.emp_Id;
                $("#ModalDetalles").find("#tmon_Id")["0"].innerText = obj.tmon_Id;
                $("#ModalDetalles").find("#sue_Cantidad")["0"].innerText = obj.sue_Cantidad;
                $("#ModalDetalles").find("#sue_SueldoAnterior")["0"].innerText = obj.sue_SueldoAnterior;               
                $("#ModalDetalles").find("#sue_FechaCrea")["0"].innerText = FechaFormato(obj.sue_FechaCrea);
                $("#ModalDetalles").find("#sue_FechaModifica")["0"].innerText = FechaFormato(obj.sue_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
          
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Sueldos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                tabla.row.add({
                    id: value.sue_Id,
                    d: value.emp_Id,
                    Id: value.tmon_Id,
                    antidad: value.sue_Cantidad,
                    ueldoAnterior: value.sue_SueldoAnterior
                });
            });
            tabla.draw();
        });
}
$(document).ready(function () {
    llenarTabla();
});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#emp_Id").val("");
    $(modalnuevo).find("#emp_Id").focus();
    $(modalnuevo).find("#tmon_Id").val("");
    $(modalnuevo).find("#sue_Cantidad").val("");
    $(modalnuevo).find("#sue_SueldoAnterior").val("");
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Sueldos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#emp_Id").val(obj.emp_Id);
                $("#ModalEditar").find("#emp_Id").focus();
                $("#ModalEditar").find("#tmon_Id").val(obj.tmon_Id);
                $("#ModalEditar").find("#sue_Cantidad").val(obj.sue_Cantidad);
                $("#ModalEditar").find("#sue_SueldoAnterior").val(obj.sue_SueldoAnterior);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#sue__RazonInactivo").val("");
    $("#ModalInhabilitar").find("#sue__RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbSueldos: data });
        _ajax(data,
            '/Sueldos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["emp_Id", "tmon_Id", "sue_Cantidad", "sue_SueldoAnterior", "sue__RazonInactivo"]);
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
        data.sue__Id = id;
        data = JSON.stringify({ tbSueldos: data });
        _ajax(data,
            '/Sueldos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["emp_Id", "tmon_Id", "sue_Cantidad", "sue_SueldoAnterior"]);
                    MsgWarning("¡Exito!", "El registro se inhabilitado  de forma exitosa");
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
        data.sue__Id = id;
        data = JSON.stringify({ tbSueldos: data });
        _ajax(data,
            '/Sueldos/Edit',
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
