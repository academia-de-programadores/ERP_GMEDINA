Admin = true;
var idadmin = 0;
//Esta funcion llama al modal de Habilitar
function hablilitar(btn) {
    var validacionPermiso = userModelState("HistorialPermisos/hablilitar");
    if (validacionPermiso.status == true) {
        var tr = $(btn).closest('tr');
        var row = tabla.row(tr);
        var lolis = row.data();
        var id = row.data().Id;
        $("#txtIdRestore").val(id);
        idadmin = id;
        //
        $('#ModalHabilitar').modal('show');
    }
}

    //Cambiar el controlador para ejecutar el UDP de restaurar
    $("#btnActivar").click(function () {
        //var Id = $("#txtIdRestore").val();
        _ajax(JSON.stringify({ id: idadmin }), // <<<<<<===================================
            '/HistorialPermisos/hablilitar/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Éxito!", "El registro se activó de forma exitosa.");
                    llenarTabla(-1);
                } else {
                    MsgError("Error", "No se activó el registro, contacte al administrador.");
                }
            });
        CierraPopups();
    });

