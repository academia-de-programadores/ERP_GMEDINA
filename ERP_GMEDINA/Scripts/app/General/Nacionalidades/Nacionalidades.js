
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
    var validacionPermiso = userModelState("Nacionalidades/Edit");
    if (validacionPermiso.status == true) {
    id = ID;
    _ajax(null,
        '/Nacionalidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#nac_Descripcion").val(obj.nac_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
    }
}
function tablaDetalles(ID) {
    var validacionPermiso = userModelState("Nacionalidades/Edit");
    if (validacionPermiso.status == true) {
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
}
function llenarTabla() {
    _ajax(null,
        '/Nacionalidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.nac_Estado == 1
                  ? null :
                   "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.nac_Estado > fill) {
                    tabla.row.add({
                        ID: value.nac_Id,
                        "Número": value.nac_Id,
                        Descripcion: value.nac_Descripcion,
                        "Descripción": value.nac_Descripcion,
                        Estado: value.nac_Estado ? "Activo" : "Inactivo",
                        Acciones: Acciones
                    }).draw();
                }
            });
        });
}



$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("Nacionalidades/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#nac_Descripcion").val("");
        $(modalnuevo).find("#nac_Descripcion").focus();
    }
});
$("#btnEditar").click(function () {
    var validacionPermiso = userModelState("Nacionalidades/Edit");
    if (validacionPermiso.status == true) {
    _ajax(null,
        '/Nacionalidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#nac_Descripcion").val(obj.nac_Descripcion);
                $("#ModalEditar").find("#nac_Descripcion").focus();
            }
        });
    }
});
$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("Nacionalidades/Delete");
    if (validacionPermiso.status == true) {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#nac_RazonInactivo").val("");
    $("#ModalInactivar").find("#nac_RazonInactivo").focus();
    }
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
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                    LimpiarControles(["nac_Descripcion", "nac_RazonInactivo"]);
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
        data.nac_Id = id;
        data = JSON.stringify({ tbNacionalidades: data });
        _ajax(data,
            '/Nacionalidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    LimpiarControles(["nac_Descripcion"]);
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
        data.nac_Id = id;
        data = JSON.stringify({ tbNacionalidades: data });
        _ajax(data,
            '/Nacionalidades/Edit',
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
