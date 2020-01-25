$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});
var fill = 0;
var id = 0;
//Funciones GET

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/RazonSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#rsal_Descripcion").val(obj.rsal_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/RazonSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#rsal_Descripcion")["0"].innerText = obj.rsal_Descripcion;             
                $("#ModalDetalles").find("#rsal_FechaCrea")["0"].innerText = FechaFormato(obj.rsal_FechaCrea);
                $("#ModalDetalles").find("#rsal_FechaModifica")["0"].innerText = FechaFormato(obj.rsal_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}

function llenarTabla() {
    _ajax(null,
        '/RazonSalidas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.rsal_Estado == 1
                   ?null:
                   "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.rsal_Estado > fill) {
                    tabla.row.add({
                        ID:value.rsal_Id,
                        "Número": value.rsal_Id,
                        "Descripción": value.rsal_Descripcion,
                        Estado: value.rsal_Estado ? "Activo" : "Inactivo",
                        Acciones:Acciones
                    }).draw();
                }
            });
        });         
}

//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#rsal_Descripcion").val("");
    modalnuevo.modal('show');
    $("#FormNuevo").find("#rsal_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/RazonSalidas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#rsal_Descripcion").val(obj.rsal_Descripcion);
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#rsal_RazonInactivo").val("");
    $("#ModalInactivar").find("#rsal_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
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
        data.rsal_Id = id;
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
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
        data.rsal_Id = id;
        data = JSON.stringify({ tbRazonSalidas: data });
        _ajax(data,
            '/RazonSalidas/Edit',
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