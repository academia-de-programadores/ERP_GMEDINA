$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();

});

var fill = 0;
var Admin = false;
var id = 0;


////Funciones GET
//$(document).ready(function () {
//    llenarTabla();
//});

function tablaEditar(ID) {
    var validacionPermiso = userModelState("TipoAmonestaciones/Edit");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/TipoAmonestaciones/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#FormEditar").find("#tamo_Descripcion").val(obj.tamo_Descripcion);
                    $('#ModalEditar').modal('show');
                }
            });
    }
}
function tablaDetalles(ID) {
    //id = ID;
    var validacionPermiso = userModelState("TipoAmonestaciones/Edit");
    if (validacionPermiso.status == true) {
        _ajax(null,
            '/TipoAmonestaciones/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetalles").find("#tamo_Descripcion")["0"].innerText = obj.tamo_Descripcion;

                    $("#ModalDetalles").find("#tamo_FechaCrea")["0"].innerText = FechaFormato(obj.tamo_FechaCrea);
                    $("#ModalDetalles").find("#tamo_FechaModifica")["0"].innerText = FechaFormato(obj.tamo_FechaModifica);
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
        '/TipoAmonestaciones/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.tamo_Estado == 1
                  ? null : 
                  "<div>" +
                      "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                      "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                  "</div>" 
                if (value.tamo_Estado > fill) {
                    tabla.row.add({
                        ID: value.tamo_Id,
                        "Número": value.tamo_Id,
                        Estado: value.tamo_Estado ? "Activo" : "Inactivo",
                        Descripcion: value.tamo_Descripcion,
                        "Descripción": value.tamo_Descripcion,

                        Acciones: Acciones
                    })
                }
            });
            tabla.draw();
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("TipoAmonestaciones/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#tamo_Descripcion").val("");
        $(modalnuevo).find("#tamo_Descripcion").focus();
    }
});


$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoAmonestaciones/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#tamo_Descripcion").val(obj.tamo_Descripcion);
                $("#ModalEditar").find("#tamo_Descripcion").focus();
            }
        });
});
$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("TipoAmonestaciones/Delete");
    if (validacionPermiso.status == true) {
        CierraPopups();
        $('#ModalInactivar').modal('show');
        $("#ModalInactivar").find("#tamo_RazonInactivo").val("");
        $("#ModalInactivar").find("#tamo_RazonInactivo").focus();
    }
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.")
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
        data.tamo_Id = id;
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    LimpiarControles(["tamo_Descripcion"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
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
        data.tamo_Id = id;
        data = JSON.stringify({ tbTipoAmonestaciones: data });
        _ajax(data,
            '/TipoAmonestaciones/Edit',
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