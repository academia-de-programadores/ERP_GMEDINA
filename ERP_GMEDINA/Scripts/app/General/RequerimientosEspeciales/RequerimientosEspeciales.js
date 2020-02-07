$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

var fill = 0;
var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    var validacionPermiso = userModelState("RequerimientosEspeciales/Edit");
    if (validacionPermiso.status == true) {
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
}
function tablaDetalles(ID) {
    id = ID;
    var validacionPermiso = userModelState("RequerimientosEspeciales/Edit");
    if (validacionPermiso.status == true) {
        _ajax(null,
            '/RequerimientosEspeciales/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetalles").find("#resp_Descripcion")["0"].innerText = obj.resp_Descripcion;

                    $("#ModalDetalles").find("#resp_FechaCrea")["0"].innerText = FechaFormato(obj.resp_FechaCrea);
                    $("#ModalDetalles").find("#resp_FechaModifica")["0"].innerText = FechaFormato(obj.resp_FechaModifica);
                    $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                    $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                    //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                    $('#ModalDetalles').modal('show');
                }
            });
    }
}
function llenarTabla() {
    _ajax(null,
        '/RequerimientosEspeciales/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.resp_Estado == 1
                  ?null:
                  "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.resp_Estado > fill) {
                tabla.row.add({
                    ID: value.resp_Id,
                    "Número" : value.resp_Id,
                    Descripción: value.resp_Descripcion,
                    Acciones: Acciones,
                    Estado: value.resp_Estado ? "Activo":"Inactivo"
                }).draw();
                }
            });
        });
}

//Botones GET
$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("RequerimientosEspeciales/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#resp_Descripcion").val("");
        $(modalnuevo).find("#resp_Descripcion").focus();
    }
});
$("#btnEditar").click(function () {
    var validacionPermiso = userModelState("RequerimientosEspeciales/Edit");
    if (validacionPermiso.status == true) {
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
    }
});
$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("RequerimientosEspeciales/Delete");
    if (validacionPermiso.status == true) {
        CierraPopups();
        $('#ModalInactivar').modal('show');
        $("#ModalInactivar").find("#resp_RazonInactivo").val("");
        $("#ModalInactivar").find("#resp_RazonInactivo").focus();
    }
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
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                    LimpiarControles(["resp_Descripcion"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
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
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                } else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por Pavor llene todas las cajas de texto.");
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
                    MsgSuccess("¡Exito!", "El registro se editó de forma exitosa");
                } else {
                    MsgError("Error", "No se editó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});