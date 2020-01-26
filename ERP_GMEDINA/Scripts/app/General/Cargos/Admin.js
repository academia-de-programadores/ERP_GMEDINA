﻿Admin = true;

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
    var Id = $("#txtIdRestore").val();
    _ajax(JSON.stringify({ id: Id }), // <<<<<<===================================
        '/Cargos/hablilitar/',
        'POST',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                MsgSuccess("¡Exito!", "El registro se ha activado de forma exitosa");
                llenarTabla(-1);
            }

            else {
                MsgError("Error", "No se logró activar el registro, contacte al administrador");
            }
        });
    CierraPopups();
});