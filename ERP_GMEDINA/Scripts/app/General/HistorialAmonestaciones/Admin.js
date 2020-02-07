Admin = true;

function llamarmodalhabilitar(ID) {
    var validacionPermiso = userModelState("HistorialAmonestaciones/habilitar");
    if (validacionPermiso.status == true) {
        var modalhabilitar = $("#ModalHabilitar");
        $("#ModalHabilitar").find("#hamo_Id").val(ID);
        modalhabilitar.modal('show');
    }
}


$("#btnActivar").click(function () {
    var data = $("#FormActivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAmonestaciones: data });
        _ajax(data,
            '/HistorialAmonestaciones/habilitar/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se activó de forma exitosa.");
                    LimpiarControles(["txtIdRestore"]);
                    llenarTabla(-1);
                } else {
                    MsgError("Error", "No se activó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
