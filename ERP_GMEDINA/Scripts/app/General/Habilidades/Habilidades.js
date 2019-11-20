var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Habilidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Habilidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#habi_Descripcion")["0"].innerText = obj.habi_Descripcion;
                $("#ModalDetalles").find("#habi_Estado")["0"].innerText = obj.habi_Estado;
                $("#ModalDetalles").find("#habi_RazonInactivo")["0"].innerText = obj.habi_RazonInactivo;
                $("#ModalDetalles").find("#habi_FechaCrea")["0"].innerText = FechaFormato(obj.habi_FechaCrea);
                $("#ModalDetalles").find("#habi_FechaModifica")["0"].innerText = FechaFormato(obj.habi_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Habilidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.habi_Descripcion);
                tabla.row.add([value.habi_Descripcion,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.habi_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.habi_Id + ")'>Editar</a>" +
                    "</div>"]).draw();
            });
        });
}
//$("#FormNuevo").on('submit', function (evt) {
//    //evt.preventDefault();
//    // tu codigo aqui
//    var data = $("#FormNuevo").serializeArray();
//    data = serializar(data);
//    if (data != null) {
//        data = JSON.stringify({ tbHabilidades: data });
//        _ajax(data,
//            '/Habilidades/Create',
//            'POST',
//            function (obj) {
//                if (obj != "-1" && obj != "-2" && obj != "-3") {
//                    CierraPopups();
//                    llenarTabla();
//                    LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
//                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
//                } else {
//                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
//                }
//            });
//    } else {
//        MsgError("Error", "por favor llene todas las cajas de texto");
//    }
//});
//Modals
$("#ModalNuevo").on('hidden.bs.modal', function () {
    SetearClases("habi_Descripcion", "valid", "error");
});
$("#ModalEditar").on('hidden.bs.modal', function () {
    SetearClases("habi_Descripcion", "valid", "error");
});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#habi_Descripcion").val("");
    modalnuevo.modal('show');
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Habilidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $("#FormEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
});
//botones POST
//$("#btnGuardar").click(function () {
//    var data = $("#FormNuevo").serializeArray();
//    data = serializar(data);
//    if (data!=null) {
//        data = JSON.stringify({ tbHabilidades: data });
//        _ajax(data,
//            '/Habilidades/Create',
//            'POST',
//            function (obj) {
//                if (obj != "-1" && obj != "-2" && obj != "-3") {
//                    CierraPopups();
//                    llenarTabla();
//                    LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
//                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
//                } else {
//                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
//                }
//            });
//    } else {
//        MsgError("Error","por favor llene todas las cajas de texto");
//    }    
//});
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.habi_Id = id;
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
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
    if (data!=null) {
        data.habi_Id = id;
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Edit',
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