$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});
var id = 0;
var fill = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Idiomas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#idi_Descripcion").val(obj.idi_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Idiomas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#idi_Descripcion")["0"].innerText = obj.idi_Descripcion;
                $("#ModalDetalles").find("#idi_FechaCrea")["0"].innerText = FechaFormato(obj.idi_FechaCrea);
                $("#ModalDetalles").find("#idi_FechaModifica")["0"].innerText = FechaFormato(obj.idi_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Idiomas/llenarTabla',
        'POST',
        function (Lista) {
            var tabla = $("#IndexTable").DataTable();
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.idi_Estado == 1
                  ?null:
                  "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.idi_Estado > fill) {
                    tabla.row.add({
                        ID: value.idi_Id,
                        "Número": value.idi_Id,
                        Descripción: value.idi_Descripcion,
                        Acciones : Acciones,
                        Estado: value.idi_Estado ? "Activo" : "Inactivo"
                    }).draw();
                }
            });
        });
}
//function ClearTables() {
//    $('#IndexTable').dataTable().clear().draw();
//}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#idi_Descripcion").val("");
    $(modalnuevo).find("#idi_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Idiomas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#idi_Descripcion").val(obj.idi_Descripcion);
                $("#ModalEditar").find("#idi_Descripcion").focus();
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#idi_RazonInactivo").val("");
    $("#ModalInactivar").find("#idi_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbIdiomas: data });
        _ajax(data,
            '/Idiomas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["idi_Descripcion"]);
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                }
                else {
                    MsgError("Error", "No se agrego el registro, contacte al administrador.");
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
        data.idi_Id = id;
        data = JSON.stringify({ tbIdiomas: data });
        _ajax(data,
            '/Idiomas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["idi_Descripcion", "idi_RazonInactivo"]);
                    MsgSuccess("¡Éxito!", "El registro se ha inactivado de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador.");
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
        data.idi_Id = id;
        data = JSON.stringify({ tbIdiomas: data });
        _ajax(data,
            '/Idiomas/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});