Admin = true;

//Esta funcion llama al modal de Habilitar
function hablilitar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    $("#txtIdRestore").val(id);
    $('#ModalHabilitar').modal('show');
}

//Cambiar el controlador para ejecutar el UDP de restaurar
$("#btnActivar").click(function () {
    var validacionPermiso = userModelState("Nacionalidades/Habilitar");
    if (validacionPermiso.status == true) {
    var Id = $("#txtIdRestore").val();
    _ajax(JSON.stringify({ id: Id }), // <<<<<<===================================
        '/Nacionalidades/hablilitar/',
        'POST',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                MsgSuccess("¡Éxito!", "El registro se activó  de forma exitosa.");
                llenarTabla();
            } else {
                MsgError("Error", "No se inactivó el registro, contacte al administrador.");
            }
        });
    CierraPopups();
    }
});
