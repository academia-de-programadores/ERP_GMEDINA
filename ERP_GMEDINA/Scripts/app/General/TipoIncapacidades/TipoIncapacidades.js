$(document).ready(function () {
    llenarTabla();
});

var id = 0;

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        //Nombre del Controlador
        '/TipoIncapacidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                //
                $("#FormEditar").find("#ticn_Descripcion").val(obj.ticn_Descripcion);
                $("#ModalEditar").modal('show');
            }
        });
}

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/TipoIncapacidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#ticn_Descripcion")["0"].innerText = obj.ticn_Descripcion;
                $("#ModalDetalles").find("#ticn_Estado")["0"].innerText = obj.ticn_Estado;
                $("#ModalDetalles").find("#ticn_RazonInactivo")["0"].innerText = obj.ticn_RazonInactivo;
                $("#ModalDetalles").find("#ticn_FechaCrea")["0"].innerText = obj.FechaCrea;
                $("#ModalDetalles").find("#ticn_FechaModifica")["0"].innerText = obj.FechaModifica;
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}

function llenarTabla() {
    _ajax(null,
        '/TipoIncapacidades/llenarTabla',
        'POST',
        function (lista) {
            tabla.clear();
            tabla.draw();
            $.each(lista, function (index, value) {
                console.log(value.ticn_Descripcion);
                tabla.row.add({
                    ID:value.ticn_Id,
                    Descripción:value.ticn_Descripcion
                    }).draw();

            });
        });
}

$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#ticn_Descripcion").val("");
    $("#FormEditar").find("#ticn_Descripcion").focus();
    modalnuevo.modal('show');
});



$("#btnEditar").click(function () {
    _ajax(null,
        'TipoIncapacidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#ticn_Descripcion").val(obj.ticn_Descripcion);

            }
        });
});

$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#ticn_Descripcion").val("");
    $("#ModalInhabilitar").find("ticn_Descripcion").focus();
});


$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["ticn_Descripcion", "ticn_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                }
                else {
                    MsgError("Error", "Codigo:" + obj + ".contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de textos");
    }
});


$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.ticn_Id = id;
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["ticn_Descripcion", "ticn_RazonInactivo"]);
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
        data.ticn_Id = id;
        data = JSON.stringify({ tbTipoIncapacidades: data });
        _ajax(data,
            '/TipoIncapacidades/Edit',
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


