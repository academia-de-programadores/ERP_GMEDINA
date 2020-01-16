Admin = true;

function llamarmodalhabilitar(ID) {
    var modalhabilitar = $("#ModalHabilitar");
    $("#ModalHabilitar").find("#hamo_Id").val(ID);
    modalhabilitar.modal('show');
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
                    llenarTabla();
                    LimpiarControles(["txtIdRestore"]);
                    MsgWarning("¡Exito!", "Se ha activado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});