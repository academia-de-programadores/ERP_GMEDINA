function llenarTabla() {
    _ajax(null,
        '/Empresas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.empr_Nombre);
                tabla.row.add([value.empr_Nombre,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.empr_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.empr_Id + ")'>Editar</a>" +
                    "</div>"]).draw();
            });
        });
}


//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#empr_Nombre").val("");
    $(modalnuevo).find("#empr_Nombre").focus();
})

//Botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbEmpresas: data });
        _ajax(data,
            '/Empresas/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["empr_Nombre"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});