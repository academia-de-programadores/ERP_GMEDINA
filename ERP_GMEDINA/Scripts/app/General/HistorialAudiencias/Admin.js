Admin = true;

function llamarmodalhabilitar(ID) {
    var validacionPermiso = userModelState("AudienciasDescargo/habilitar");
    if (validacionPermiso.status == true) {
        var modalhabilitar = $("#ModalHabilitar");
        $("#ModalHabilitar").find("#aude_Id").val(ID);
        modalhabilitar.modal('show');
    }
}


$("#btnActivar").click(function () {
    var data = $("#FormActivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialAudienciaDescargo: data });
        _ajax(data,
            '/HistorialAudienciaDescargos/habilitar/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgWarning("¡Éxito!", "El registro se activó de forma exitosa.");
                    LimpiarControles(["txtIdRestore"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se activó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});
