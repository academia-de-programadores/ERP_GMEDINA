Admin = true;



function llamarmodalhabilitar(ID) {
    var modalhabilitar = $("#ModalHabilitar");
    $("#ModalHabilitar").find("#hvac_Id").val(ID);
    modalhabilitar.modal('show');
}




$("#btnActivar").click(function () {
    var data = $("#FormActivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbHistorialVacaciones: data });
        _ajax(data,
            '/HistorialVacaciones/habilitar/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgWarning("¡Éxito!", "El registro se activó de forma exitosa.");
                    LimpiarControles(["hvac_Id"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se activó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});