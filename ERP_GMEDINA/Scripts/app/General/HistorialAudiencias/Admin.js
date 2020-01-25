Admin = true;

function llamarmodalhabilitar(ID) {
    var modalhabilitar = $("#ModalHabilitar");
    $("#ModalHabilitar").find("#aude_Id").val(ID);
    modalhabilitar.modal('show');
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
                    llenarTabla();
                    LimpiarControles(["txtIdRestore"]);
                    MsgWarning("¡Éxito!", "Se registro se activó de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró activar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});