
var fill = 0;
var id = 0;
var Admin = false;
//Funciones GET
$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Nacionalidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {  //PENDIENTE REVISAR CANTIDAD***********
                $("#FormEditar").find("#nac_Descripcion").val(obj.nac_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Nacionalidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#nac_Descripcion")["0"].innerText = obj.nac_Descripcion;
                $("#ModalDetalles").find("#nac_FechaCrea")["0"].innerText = FechaFormato(obj.nac_FechaCrea);
                $("#ModalDetalles").find("#nac_FechaModifica")["0"].innerText = FechaFormato(obj.nac_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Nacionalidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.nac_Estado == 1
                  ? null :
                  "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.nac_Estado > fill) {
                    tabla.row.add({
                        ID: value.nac_Id,
                        "Número": value.nac_Id,
                        Estado: value.nac_Estado ? "Activo" : "Inactivo",
                        "Descripción": value.nac_Descripcion,
                        Acciones: Acciones
                    })
                }
            });
            tabla.draw();
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#nac_Descripcion").val("");
    modalnuevo.modal('show');
    $("#FormNuevo").find("#nac_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Nacionalidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#nac_Descripcion").val(obj.nac_Descripcion);
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#nac_RazonInactivo").val("");
    $("#ModalInactivar").find("#nac_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbNacionalidades: data });
        _ajax(data,
            '/Nacionalidades/Create',
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
        data.nac_Id = id;
        data = JSON.stringify({ tbNacionalidades: data });
        _ajax(data,
            '/Nacionalidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["nac_Descripcion"]);
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
        data.nac_Id = id;
        data = JSON.stringify({ tbNacionalidades: data });
        _ajax(data,
            '/Nacionalidades/Edit',
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