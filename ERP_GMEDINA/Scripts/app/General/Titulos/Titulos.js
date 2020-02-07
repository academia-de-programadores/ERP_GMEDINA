$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

var fill = 0;
var id = 0;

function tablaEditar(ID) {
    id = ID;
    var validacionPermiso = userModelState("Titulos/Edit");
    if (validacionPermiso.status == true) {

    _ajax(null,
    '/Titulos/Edit/' + ID,
    'GET',
    function (obj) {
        if (obj != "-1" && obj != "-2" && obj != "-3") {
            $("#FormEditar").find("#titu_Descripcion").val(obj.titu_Descripcion);
            $("#ModalEditar").modal('show');
        }
    });
}
}
function tablaDetalles(ID) {
   
    id = ID;
    var validacionPermiso = userModelState("Titulos/Edit");
    if (validacionPermiso.status == true) {
    _ajax(null,
        '/Titulos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#titu_Descripcion")["0"].innerText = obj.titu_Descripcion;
                $("#ModalDetalles").find("#titu_FechaCrea")["0"].innerText = FechaFormato(obj.titu_FechaCrea);
                $("#ModalDetalles").find("#titu_FechaModifica")["0"].innerText = FechaFormato(obj.titu_FechaModifica);
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
        '/Titulos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {

                var Acciones = value.titu_Estado == 1
                  ? null :
                 "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.titu_Estado > fill) {
                    tabla.row.add({
                        ID: value.titu_Id,
                        "Número": value.titu_Id,
                        Títulos: value.titu_Descripcion,
                        "Títulos": value.titu_Descripcion,
                        Acciones: Acciones,
                        Estado: value.titu_Estado ? "Activo" : "Inactivo"

                    }).draw();
                }
            });
        });
}





$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("Titulos/Create");
    if (validacionPermiso.status == true) {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#titu_Descripcion").val("");
    $("#FormEditar").find("#titu_Descripcion").focus();
    modalnuevo.modal('show');
    }
});



$("#btnEditar").click(function () {
    var validacionPermiso = userModelState("Titulos/Edit");
    if (validacionPermiso.status == true) {
    _ajax(null,
        '/Titulos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#titu_Descripcion").val(obj.titu_Descripcion);

            }
        });
    }
});

$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("Titulos/Delete");
    if (validacionPermiso.status == true) {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#titu_Descripcion").val("");
    $("#ModalInactivar").find("titu_Descripcion").focus();
         }
});


$("#btnGuardar").click(function () {
    
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTitulos: data });
        _ajax(data,
            '/Titulos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["titu_Descripcion"]);
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
        data.titu_Id = id;
        data = JSON.stringify({ tbTitulos: data });
        _ajax(data,
            '/Titulos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["titu_Descripcion", "titu_RazonInactivo"]);
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
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
        data.titu_Id = id;
        data = JSON.stringify({ tbTitulos: data });
        _ajax(data,
            '/Titulos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "El registro se editó de forma exitosa.");
                } else {
                    MsgError("Error", "No se editó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
