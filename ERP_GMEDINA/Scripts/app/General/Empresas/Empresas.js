$(document).ready(function () {
    llenarTabla();
});
function llenarTabla() {
    _ajax(null,
        '/Empresas/llenarTabla',
        'POST',
        function (Lista) {
            var tabla = $("#IndexTable").DataTable();
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.empr_Nombre);
                tabla.row.add({
                    ID: value.empr_Id,   
                    Empresa:value.empr_Nombre,
                    }).draw();
            });
        });
}

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Empresas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#empr_Nombre").val(obj.empr_Nombre);
                $('#ModalEditar').modal('show');
            }
        });
}

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Empresas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#empr_Nombre")["0"].innerText = obj.empr_Nombre;
                $("#ModalDetalles").find("#empr_FechaCrea")["0"].innerText = FechaFormato(obj.empr_FechaCrea);
                $("#ModalDetalles").find("#empr_FechaModifica")["0"].innerText = FechaFormato(obj.empr_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}


//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#empr_Nombre").val("");
    $(modalnuevo).find("#empr_Nombre").focus();
})

$("#btnEditar").click(function () {
    _ajax(null,
        '/Empresas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#empr_Nombre").val(obj.empr_Nombre);
                $("#ModalEditar").find("#empr_Nombre").focus();
            }
        });
});

$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#empr_RazonInactivo").val("");
    $("#ModalInactivar").find("#empr_RazonInactivo").focus();
});

//Botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbEmpresas: data });
        _ajax(data,
            '/Empresas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["empr_Nombre"]);
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
        data.empr_Id = id;
        data = JSON.stringify({ tbEmpresas: data });
        _ajax(data,
            '/Empresas/Edit',
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

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.empr_Id = id;
        data = JSON.stringify({ tbEmpresas: data });
        _ajax(data,
            '/Empresas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["empr_Nombre", "empr_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró Inactivar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});