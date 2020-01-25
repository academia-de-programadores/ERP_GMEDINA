var id = 0;
var fill = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/TipoSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#tsal_Descripcion").val(obj.tsal_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    //id = ID;
    _ajax(null,
        '/TipoSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#tsal_Descripcion")["0"].innerText = obj.tsal_Descripcion;
                $("#ModalDetalles").find("#tsal_FechaCrea")["0"].innerText = FechaFormato(obj.tsal_FechaCrea);
                $("#ModalDetalles").find("#tsal_FechaModifica")["0"].innerText = FechaFormato(obj.tsal_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoSalidas/llenarTabla',
        'POST',
        function (Lista) {
            if (validarDT(Lista)) {
                return null;
            }
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
             var Acciones = value.tsal_Estado == 1
                    ? null : Admin ?
                    "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
                   tabla.row.add(
                       {
                        Estado:value.tsal_Estado?'Activo':'Inactivo',
                           "Número": value.tsal_Id,
                           ID: value.tsal_Id,
                           Salidas: value.tsal_Descripcion,
                           Acciones: Acciones
                       });
             
            });
            tabla.draw();
        });
}
$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormEditar").find("#errortsal_Descripcion").val('');
    $("#FormNuevo").find("#tsal_Descripcion").val("");
    modalnuevo.modal('show');
    $("#FormNuevo").find("#tsal_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/TipoSalidas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#tsal_Descripcion").val(obj.tsal_Descripcion);
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#tsal_RazonInactivo").val("");
    $("#ModalInactivar").find("#tsal_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoSalidas: data });
        _ajax(data,
            '/TipoSalidas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tsal_Descripcion", "tsal_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se agrego el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.tsal_Id = id;
        data = JSON.stringify({ tbTipoSalidas: data });
        _ajax(data,
            '/TipoSalidas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["tsal_Descripcion", "tsal_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "El registro se ha inactivado de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador");
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
        data.tsal_Id = id;
        data = JSON.stringify({ tbTipoSalidas: data });
        _ajax(data,
            '/TipoSalidas/Edit',
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