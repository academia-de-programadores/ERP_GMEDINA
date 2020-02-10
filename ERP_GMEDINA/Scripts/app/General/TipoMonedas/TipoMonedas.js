
$(document).ready(function () {
    llenarTabla();
    fill = Admin == undefined ? 0 : -1;
});
var fill = 0;

var id = 0;

//Funciones GET
function tablaEditar(ID) {
    var validacionPermiso = userModelState("TipoMonedas/Edit");
    if (validacionPermiso.status == true) {
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
}
function tablaDetalles(ID) {
    var validacionPermiso = userModelState("TipoMonedas/Edit");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/TipoMonedas/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetalles").find("#tmon_Descripcion")["0"].innerText = obj.tmon_Descripcion;
                    //$("#ModalDetalles").find("#tmon_Estado")["0"].innerText = obj.tmon_Estado;
                    //$("#ModalDetalles").find("#tmon_RazonInactivo")["0"].innerText = obj.tmon_RazonInactivo;
                    $("#ModalDetalles").find("#tmon_FechaCrea")["0"].innerText = FechaFormato(obj.tmon_FechaCrea);
                    $("#ModalDetalles").find("#tmon_FechaModifica")["0"].innerText = FechaFormato(obj.tmon_FechaModifica);
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
        '/TipoMonedas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.tmon_Estado == 1
                    ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.tmon_Id + ")'>Detalles</a><a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.tmon_Id + ")'>Editar</a>"
                    : Admin ?
                      "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
                if (value.tmon_Estado > fill) {
                    tabla.row.add({
                        Id: value.tmon_Id,
                        ID: value.tmon_Id,
                        Moneda: value.tmon_Descripcion,
                        "Número": value.tmon_Id,
                        Acciones: Acciones,
                        Estado: value.tmon_Estado ? "Activo" : "Inactivo"
                        //
                    });
                }
            });
            tabla.draw();

        });
}
$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("TipoMonedas/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#tmon_Descripcion").val("");
        $(modalnuevo).find("#tmon_Descripcion").focus();
    }
});
$("#btnEditar").click(function () {
    var validacionPermiso = userModelState("TipoMonedas/Edit");
    if (validacionPermiso.status == true) {
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
    }
});
$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("TipoMonedas/Delete");
    if (validacionPermiso.status == true) {
        CierraPopups();
        $('#ModalInactivar').modal('show');
        $("#ModalInactivar").find("#tmon_RazonInactivo").val("");
        $("#ModalInactivar").find("#tmon_RazonInactivo").focus();
    }
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
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                    LimpiarControles(["tmon_Descripcion"]);
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
    //var validacionPermiso = userModelState("TipoMonedas/Edit");
    //if (validacionPermiso.status == true) {
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
                        MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                        LimpiarControles(["tmon_Descripcion"]);
                        llenarTabla();
                    } else {
                        MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                    }
                });
        } else {
            MsgError("Error", "Por favor llene todas las cajas de texto.");
        }
    //}
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
                    MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                    llenarTabla();
                } else {
                    MsgError("Error", "No se editó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
