Admin = true;
function Llamarmodalhabilitar(ID) {

    var modalhabilitar = $("#ModalHabilitar");
    Id = $("#ModalHabilitar").find("#hinc_Id").val(ID);
    modalhabilitar.modal('show');
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
                    llenarTabla();
                    LimpiarControles(["txtIdRetore"]);
                    MsgWarning("¡Exito!", "El registro se activó de forma exitosa  ");
                } else {
                    MsgError("Error", "No se logró activar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});