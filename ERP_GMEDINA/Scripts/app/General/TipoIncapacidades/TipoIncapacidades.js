$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;

    llenarTabla();
});
var fill = 0;

var id = 0;

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoIncapacidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#ticn_Descripcion").val(obj.ticn_Descripcion);
                $("#ModalEditar").modal('show');
            }
        });
}

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoIncapacidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#ticn_Descripcion")["0"].innerText = obj.ticn_Descripcion;
                //Campos Estado y Razon Inactivo ya no se muestran en el modal de detalle
                //$("#ModalDetalles").find("#ticn_Estado")["0"].innerText = obj.ticn_Estado;
                //$("#ModalDetalles").find("#ticn_RazonInactivo")["0"].innerText = obj.ticn_RazonInactivo;
                $("#ModalDetalles").find("#ticn_FechaCrea")["0"].innerText = FechaFormato(obj.ticn_FechaCrea);
                $("#ModalDetalles").find("#ticn_FechaModifica")["0"].innerText = FechaFormato(obj.ticn_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}

function llenarTabla() {
    _ajax(null,
        '/TipoIncapacidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.ticn_Estado == 1
                ?null:
                "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.ticn_Estado > fill) {
                tabla.row.add({
                    ID: value.ticn_Id,
                    "Número": value.ticn_Id,
                    Descripción: value.ticn_Descripcion,
                    Estado:value.ticn_Estado ? "Activo":"Inactivo",
                    Acciones:Acciones
                    }).draw();
                   }
            });
        });
}

$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#ticn_Descripcion").val("");
    $("#FormEditar").find("#ticn_Descripcion").focus();
    modalnuevo.modal('show');
});

$("#btnEditar").click(function () {
    _ajax(null,
        'TipoIncapacidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#ticn_Descripcion").val(obj.ticn_Descripcion);

            }
        });
});

$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#ticn_Descripcion").val("");
    $("#ModalInactivar").find("ticn_Descripcion").focus();
});

$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["ticn_Descripcion"]);
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                } else {
                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de textos.");
    }
});

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.ticn_Id = id;
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["ticn_Descripcion"]);
                    MsgSuccess("¡Éxito!", "El registro se ha inactivado de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});

$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.ticn_Id = id;
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Edit',
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
