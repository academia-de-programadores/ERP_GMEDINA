Admin = true;

//Esta funcion llama al modal de Habilitar
function hablilitar(btn) {
    var validacionPermiso = userModelState("TipoAmonestaciones/hablilitar");
    if (validacionPermiso.status == true) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    $("#txtIdRestore").val(id);
    $('#ModalHabilitar').modal('show');
    }
}

//Cambiar el controlador para ejecutar el UDP de restaurar
    $("#btnActivar").click(function () {
        var Id = $("#txtIdRestore").val();
        _ajax(JSON.stringify({ id: Id }), // <<<<<<===================================
            '/TipoAmonestaciones/hablilitar/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.")
                    llenarTabla(-1);
                } else {
                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                }
            });
   
        CierraPopups();
});