$(document).ready(function () {
    llenarTabla();
});

function tablaEditar(ID)
{
    id = ID;
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

function tablaDetalles(ID) {
    id = ID;
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
                console.log(value.titu_Descripcion);
                tabla.row.add({
                    ID: value.titu_Id,
                    Titulos: value.titu_Descripcion
                }).draw();

            });
        });
}

$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#titu_Descripcion").val("");
    $("#FormEditar").find("#titu_Descripcion").focus();
    modalnuevo.modal('show');
});



$("#btnEditar").click(function () {
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
});

$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#titu_Descripcion").val("");
    $("#ModalInhabilitar").find("titu_Descripcion").focus();
});


$("#btnGuardar").click(function() {
    console.log("dfsdf");
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
                    LimpiarControles(["titu_Descripcion", "titu_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "Se ha agregado el registro");
                }
                else {
                    MsgError("Error", "Codigo:" + obj + " Contacte al administrador(Verifique si el registro ya existe).");
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
                    MsgWarning("¡Exito!", "Se ha Inhabilitado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + " Contacte al administrador.");
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
        data.titu_Id = id;
        data = JSON.stringify({ tbTitulos: data });
        _ajax(data,
            '/Titulos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + " contacte al administrador(Verifique si el registro ya existe).");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto");
    }
});
