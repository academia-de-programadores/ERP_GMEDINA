﻿var id = 0;
var fill = 0;
//var Admin = false;
$(document).ready(function () {
    llenarTabla();
    fill = Admin == undefined ? 0 : -1;
    $('.clockpicker').clockpicker();
});
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoPermisos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#tper_Descripcion").val(obj.tper_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoPermisos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#tper_Descripcion")["0"].innerText = obj.tper_Descripcion;
                //$("#ModalDetalles").find("#tper_Estado")["0"].innerText = obj.tper_Estado;
                //$("#ModalDetalles").find("#tper_RazonInactivo")["0"].innerText = obj.tper_RazonInactivo;
                $("#ModalDetalles").find("#tper_FechaCrea")["0"].innerText = FechaFormato(obj.tper_FechaCrea);
                $("#ModalDetalles").find("#tper_FechaModifica")["0"].innerText = FechaFormato(obj.tper_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoPermisos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.tper_Estado == 1
                    ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.tper_Id + ")'>Detalles</a><a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.tper_Id + ")'>Editar</a>"
                    : Admin ?
                        "<div>" +
                        "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                        "</div>" : '';
                if (value.tper_Estado > fill) {
                    tabla.row.add({
                        Id: value.tper_Id,
                        ID: value.tper_Id,
                        Permiso: value.tper_Descripcion,
                        "Número": value.tper_Id,
                        Acciones: Acciones,
                        Estado: value.tper_Estado ? "Activo" : "Inactivo"
                        //
                    });
                }
            });
            tabla.draw();

        });
}
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#tper_Descripcion").val("");
    $(modalnuevo).find("#tper_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoPermisos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#tper_Descripcion").val(obj.tper_Descripcion);
                $("#ModalEditar").find("#tper_Descripcion").focus();
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#tper_RazonInactivo").val("");
    $("#ModalInactivar").find("#tper_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoPermisos: data });
        _ajax(data,
            '/TipoPermisos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tper_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se agrego el registro, contacte al administrador");
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
        data.tper_Id = id;
        data = JSON.stringify({ tbTipoPermisos: data });
        _ajax(data,
            '/TipoPermisos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tper_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se ha inactivado de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador");
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
        data.tper_Id = id;
        data = JSON.stringify({ tbTipoPermisos: data });
        _ajax(data,
            '/TipoPermisos/Edit',
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
