﻿var id = 0;
var fill = 0;
var Admin = false;
//Funciones GET

$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/FasesReclutamiento/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#fare_Descripcion").val(obj.fare_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/FasesReclutamiento/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#fare_Descripcion")["0"].innerText = obj.fare_Descripcion;
                //$("#ModalDetalles").find("#fare_Estado")["0"].innerText = obj.fare_Estado;
                //$("#ModalDetalles").find("#fare_RazonInactivo")["0"].innerText = obj.fare_RazonInactivo;
                $("#ModalDetalles").find("#fare_FechaCrea")["0"].innerText = FechaFormato(obj.fare_FechaCrea);
                $("#ModalDetalles").find("#fare_FechaModifica")["0"].innerText = FechaFormato(obj.fare_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/FasesReclutamiento/llenarTabla',
        'POST',
        function (Lista) {
            var tabla = $("#IndexTable").DataTable();
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.fare_Estado == 1
                    ? null :
                    "<div>" +
                    "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Habilitar</a>" +
                    "</div>";
                if (value.fare_Estado > fill) {
                tabla.row.add({
                    ID: value.fare_Id,
                    "Número": value.fare_Id,
                    Descripción: value.fare_Descripcion,
                    Estado: value.fare_Estado ? "Activo" : "Inactivo",
                    Acciones: Acciones
                });
                } 
                tabla.draw();
            });
        });
}

//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#fare_Descripcion").val("");
    $(modalnuevo).find("#fare_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/FasesReclutamiento/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#fare_Descripcion").val(obj.fare_Descripcion);
                $("#ModalEditar").find("#fare_Descripcion").focus();
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#fare_RazonInactivo").val("");
    $("#ModalInactivar").find("#fare_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data!=null) {
        data = JSON.stringify({ tbFasesReclutamiento: data });
        _ajax(data,
            '/FasesReclutamiento/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["fare_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error","por favor llene todas las cajas de texto");
    }    
});
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.habi_Id = id;
        data = JSON.stringify({ tbFasesReclutamiento: data });
        _ajax(data,
            '/FasesReclutamiento/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["fare_Descripcion", "fare_RazonInactivo"]);
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
    if (data!=null) {
        data.habi_Id = id;
        data = JSON.stringify({ tbFasesReclutamiento: data });
        _ajax(data,
            '/FasesReclutamiento/Edit',
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