$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});



var fill = 0



$(document).ready(function () {
    llenarTabla();
});
var id = 0;
//Funciones GET



function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Competencias/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#comp_Descripcion").val(obj.comp_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Competencias/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#comp_Descripcion")["0"].innerText = obj.comp_Descripcion;
                $("#ModalDetalles").find("#comp_FechaCrea")["0"].innerText = FechaFormato(obj.comp_FechaCrea);
                $("#ModalDetalles").find("#comp_FechaModifica")["0"].innerText = FechaFormato(obj.comp_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Competencias/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.comp_Estado == 1
                  ? null :
                   "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.comp_Estado > fill) {
                    tabla.row.add({
                        ID: value.comp_Id,
                        "Número": value.comp_Id,
                        Descripcion: value.comp_Descripcion,
                        "Descripción": value.comp_Descripcion,
                        Estado: value.comp_Estado ? "Activo" : "Inactivo",
                        Acciones: Acciones
                    }).draw();
                }
            });
        });
}




$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#comp_Descripcion").val("");
    $(modalnuevo).find("#comp_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Competencias/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#comp_Descripcion").val(obj.comp_Descripcion);
                $("#ModalEditar").find("#comp_Descripcion").focus();
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#comp_RazonInactivo").val("");
    $("#ModalInactivar").find("#comp_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbCompetencias: data });
        _ajax(data,
            '/Competencias/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["comp_Descripcion", "comp_RazonInactivo"]);
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
        data.comp_Id = id;
        data = JSON.stringify({ tbCompetencias: data });
        _ajax(data,
            '/Competencias/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["comp_Descripcion"]);
                    MsgSuccess("¡Exito!", "El registro se ha inactivado de forma exitosa.");
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
        data.comp_Id = id;
        data = JSON.stringify({ tbCompetencias: data });
        _ajax(data,
            '/Competencias/Edit',
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