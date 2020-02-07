$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

var fill = 0;
var Admin = false;
var id = 0;
//Funciones GET
function tablaEditar(ID) {
    var validacionPermiso = userModelState("TipoHoras/Edit");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/TipoHoras/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    // $("#FormEditar").find("#tiho_Id").val(obj.habi_Descripcion);
                    $("#FormEditar").find("#tiho_Descripcion").val(obj.tiho_Descripcion);
                    $("#FormEditar").find("#tiho_Recargo").val(obj.tiho_Recargo);
                    $('#ModalEditar').modal('show');
                }
            });
    }
}
function tablaDetalles(ID) {
    //id = ID;
    var validacionPermiso = userModelState("TipoHoras/Edit");
    if (validacionPermiso.status == true) {
        _ajax(null,
            '/TipoHoras/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetallesR").find("#tiho_Descripcion")["0"].innerText = obj.tiho_Descripcion;
                    $("#ModalDetallesR").find("#tiho_Recargo")["0"].innerText = obj.tiho_Recargo;
                    $("#ModalDetallesR").find("#tiho_FechaCrea")["0"].innerText = FechaFormato(obj.tiho_FechaCrea);
                    $("#ModalDetallesR").find("#tiho_FechaModifica")["0"].innerText = FechaFormato(obj.tiho_FechaModifica);
                    $("#ModalDetallesR").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                    $("#ModalDetallesR").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                    //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                    $('#ModalDetalles').modal('show');
                }
            });
    }
}
function llenarTabla() {
    _ajax(null,
        '/TipoHoras/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                //
                var Acciones = value.tiho_Estado == 1
                       ?null:Admin?
                       "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : "";
          
                tabla.row.add({
                   
                    "Número": value.tiho_Id,
                      ID: value.tiho_Id,
                      Hora: value.tiho_Descripcion,
                      Recargo: value.tiho_Recargo,
                      Estado: value.tiho_Estado ? 'Activo' : 'Inactivo',
                      Acciones: Acciones
                  });
        });
        tabla.draw();

        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("TipoHoras/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $("#FormNuevo").find("#tiho_Descripcion").val("");
        $("#FormNuevo").find("#tiho_Recargo").val(0);
        $("#FormNuevo").find("#tiho_Descripcion").focus();
    }
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoHoras/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#tiho_Descripcion").val(obj.tiho_Descripcion);
                $("#FormEditar").find("#tiho_Recargo").val(obj.tiho_Recargo);
            }
        });
});
$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("TipoHoras/Delete");
    if (validacionPermiso.status == true) {
        CierraPopups();
        $('#ModalInactivar').modal('show');
        $("#ModalInactivar").find("#habi_RazonInactivo").val("");
        $("#ModalInactivar").find("#habi_RazonInactivo").focus();
    }
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    //
    if (data != null) {
        $.post("/TipoHoras/Create",data).done(function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.")
                LimpiarControles(["tiho_Descripcion"]);
                llenarTabla();
            } else {
                MsgError("Error", "No se agregó el registro, contacte al administrador.");
            }
        });
    }
});
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        //data.tiho_Id = id;
       // data = JSON.stringify({ tbTipoHoras: data });
        $.post("/TipoHoras/Delete", data).done(function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                LimpiarControles(["tiho_Descripcion", "tiho_Recargo", "tiho_RazonInactivo"]);
                llenarTabla();
            } else {
                MsgError("Error", "No se inactivó el registro, contacte al administrador.");
            }
        });
    }
});
$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    //data = serializar(data);
    if (data != null) {
        data.tiho_Id = id;
        //data = JSON.stringify({ tbTipoHoras: data });
        $.post("/TipoHoras/Edit", data).done(function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                LimpiarControles(["tiho_Descripcion", "tiho_Recargo"]);
                llenarTabla();
            } else {
                MsgError("Error", "No se editó el registro, contacte al administrador.");
            }
        });
    }
});