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
                $("#ModalDetalles").find("#comp_Estado")["0"].innerText = obj.comp_Estado;
                $("#ModalDetalles").find("#comp_RazonInactivo")["0"].innerText = obj.comp_RazonInactivo;
                $("#ModalDetalles").find("#comp_FechaCrea")["0"].innerText = FechaFormato(obj.comp_FechaCrea);
                $("#ModalDetalles").find("#comp_FechaModifica")["0"].innerText = FechaFormato(obj.comp_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
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
            $.each(Lista, function (index, value) {
                console.log(value.comp_Descripcion);
                tabla.row.add([value.comp_Descripcion,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.comp_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.comp_Id + ")'>Editar</a>" +
                    "</div>"]).draw();
            });
        });
}
//function ClearTables() {
//    $('#IndexTable').dataTable().clear().draw();
//}
//Botones GET
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
$("#btnIncomplitar").click(function () {
    CierraPopups();
    $('#ModalIncomplitar').modal('show');
    $("#ModalIncomplitar").find("#comp_RazonInactivo").val("");
    $("#ModalIncomplitar").find("#comp_RazonInactivo").focus();
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
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
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
        data.comp_Id = id;
        data = JSON.stringify({ tbCompetencias: data });
        _ajax(data,
            '/Competencias/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["comp_Descripcion", "comp_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
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
        data.comp_Id = id;
        data = JSON.stringify({ tbCompetencias: data });
        _ajax(data,
            '/Competencias/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});