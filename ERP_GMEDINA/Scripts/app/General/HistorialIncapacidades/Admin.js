Admin = true;
function Llamarmodalhabilitar(ID) {
    var validacionPermiso = userModelState("HistorialIncapacidades/habilitar");
    if (validacionPermiso.status == true) {
        var modalhabilitar = $("#ModalHabilitar");
        Id = $("#ModalHabilitar").find("#hinc_Id").val(ID);
        modalhabilitar.modal('show');
    }
}


//Cambiar el controlador para ejecutar el UDP de restaurar
$("#btnActivar").click(function () {
    var data = $("#FormActivar").serializeArray();
    data = serializar(data);
    debugger
    if (data != null) {
        data = JSON.stringify({ tbHistorialIncapacidades: data });
        _ajax(data,
            '/HistorialIncapacidades/habilitar',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se activó de forma exitosa.");
                    LimpiarControles(["txtIdRetore"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se activó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
