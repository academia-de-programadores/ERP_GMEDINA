$(document).ready(function () {
    llenarTabla();
});


var id = 0;
//Funciones GET
function tablaEditar(ID) {
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
function tablaDetalles(ID) {
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
function llenarTabla() {
    _ajax(null,
        '/Nacionalidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
             
                tabla.row.add({
                    ID: value.nac_Id,
                    Nacionalidades: value.nac_Descripcion
                });
            });tabla.draw();           
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#nac_Descripcion").val("");
    $("#FormEditar").find("#nac_Descripcion").focus();
    modalnuevo.modal('show');
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Nacionalidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#nac_Descripcion").val(obj.nac_Descripcion);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#nac_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#nac_RazonInactivo").focus();
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
                    llenarTabla();
                    LimpiarControles(["nac_Descripcion"]);
                    MsgSuccess("¡Exito!", "Se ha agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + "Contacte al administrador(Verifique si el registro ya existe).");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto");
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
                    llenarTabla();
                    LimpiarControles(["nac_Descripcion", "nac_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ha Inhabilitado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + " contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto");
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
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ha actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + " Contacte al administrador(Verifique si el registro ya existe).");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto");
    }
});