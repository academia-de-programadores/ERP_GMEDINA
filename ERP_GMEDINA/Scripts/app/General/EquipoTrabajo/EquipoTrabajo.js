﻿var ID = 0;
var fill = 0;

//Funciones GET
function tablaEditar(id) {
    //var tr = $(btn).closest("tr");
    //var row = tabla.row(tr);
    //id = ID;
    _ajax(null,
        '/EquipoTrabajo/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                ID = obj.eqtra_Id;
                $("#FormEditar").find("#eqtra_Codigo").val(obj.eqtra_Codigo);
                $("#FormEditar").find("#eqtra_Descripcion").val(obj.eqtra_Descripcion);
                $("#FormEditar").find("#eqtra_Observacion").val(obj.eqtra_Observacion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(id) {
    //var tr = $(btn).closest("tr");
    //var row = tabla.row(tr);
    //id = ID;
    _ajax(null,
        '/EquipoTrabajo/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#eqtra_Codigo")["0"].innerText = obj.eqtra_Codigo;
                $("#ModalDetalles").find("#eqtra_Descripcion")["0"].innerText = obj.eqtra_Descripcion;
                $("#ModalDetalles").find("#eqtra_Observacion")["0"].innerText = obj.eqtra_Observacion;
              
                $("#ModalDetalles").find("#eqtra_FechaCrea")["0"].innerText = FechaFormato(obj.eqtra_FechaCrea);
                $("#ModalDetalles").find("#eqtra_FechaModifica")["0"].innerText = FechaFormato(obj.eqtra_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
               
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/EquipoTrabajo/llenarTabla',
        'POST',
        function (Lista) {
            if (validarDT(Lista)) {
                return null;
            }
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                var Acciones = value.eqtra_Estado == 1
                  ? null :
                  "<div>" +
                      "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Habilitar</a>" +
                  "</div>";
                tabla.row.add({
                    Número:value.Número,    
                    ID: value.eqtra_Id,
                    Codigo: value.eqtra_Codigo,
                    Equipo: value.eqtra_Descripcion,
                    Observacion: value.eqtra_Observacion,
                    Estado:value.eqtra_Estado ? "Activo":"Inactivo",
                    Acciones:Acciones
                });
            });
            tabla.draw();
        });
}
$(document).ready(function () {
    llenarTabla();
    fill = Admin == undefined ? 0 : -1;

});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#eqtra_Codigo").val("");
    $(modalnuevo).find("#eqtra_Codigo").focus();
    $(modalnuevo).find("#eqtra_Descripcion").val("");
    $(modalnuevo).find("#eqtra_Observacion").val("");
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/EquipoTrabajo/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                ID = obj.eqtra_Id;
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#eqtra_Codigo").val(obj.eqtra_Codigo);
                $("#ModalEditar").find("#eqtra_Codigo").focus();
                $("#ModalEditar").find("#eqtra_Descripcion").val(obj.eqtra_Descripcion);
                $("#ModalEditar").find("#eqtra_Observacion").val(obj.eqtra_Observacion);
            }
        });
});
$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#eqtr_RazonInactivo").val("");
    $("#ModalInactivar").find("#eqtr_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["eqtra_Codigo", "eqtra_Descripcion", "eqtra_Observacion"]);
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
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
        data.eqtra_Id = ID;
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["eqtra_Codigo", "eqtra_Descripcion", "eqtra_Observacion"]);
                    MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró Inactivar el registro, contacte al administrador");
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
        data.eqtra_Id = ID;
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("El registro se editó de forma exitosa");
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
