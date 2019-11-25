var id = 0;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Cargos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#car_Descripcion").val(obj.car_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Cargos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#car_Descripcion")["0"].innerText = obj.car_Descripcion;
                $("#ModalDetalles").find("#car_Estado")["0"].innerText = obj.car_Estado;
                $("#ModalDetalles").find("#car_RazonInactivo")["0"].innerText = obj.car_RazonInactivo;
                $("#ModalDetalles").find("#car_FechaCrea")["0"].innerText = FechaFormato(obj.car_FechaCrea);
                $("#ModalDetalles").find("#car_FechaModifica")["0"].innerText = FechaFormato(obj.car_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/Cargos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            $.each(Lista, function (index, value) {
                console.log(value.car_Descripcion);
                tabla.row.add([value.car_Descripcion,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.car_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.car_Id + ")'>Editar</a>" +
                    "</div>"]);
            });
        });
}
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#car_Descripcion").val("");
    $("#FormEditar").find("#car_Descripcion").focus();
    modalnuevo.modal('show');
});

$("#btnEditar").click(function () {
    _ajax(null,
        '/Cargos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();       
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#car_Descripcion").val(obj.car_Descripcion);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#car_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#car_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                   
                    LimpiarControles(["car_Descripcion", "car_RazonInactivo"]);
                   
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
        data.car_Id = id;
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                
                    LimpiarControles(["car_Descripcion", "car_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "Se ah Inactivado el registro");
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
        data.car_Id = id;
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                   
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});