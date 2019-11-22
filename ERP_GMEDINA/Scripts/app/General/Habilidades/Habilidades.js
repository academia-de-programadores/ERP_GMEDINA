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
                var permiso = false;
            Object.keys(obj).forEach(function (key) {
                console.log(key, obj[key])
                if (permiso)
                {
                    $("#ModalDetalles").find("#" + key + "")["0"].innerText = obj.key;
                    debugger
                }
                else {
                    permiso = true;
                }

            })

            debugger
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
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#habi_Descripcion").val("");
    $(modalnuevo).find("#habi_Descripcion").focus();
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Habilidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                $("#ModalEditar").find("#habi_Descripcion").focus();
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#habi_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#habi_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data!=null) {
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error","por favor llene todas las cajas de texto");
    }    
});
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