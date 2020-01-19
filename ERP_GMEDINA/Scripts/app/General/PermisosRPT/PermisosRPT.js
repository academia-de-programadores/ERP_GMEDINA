$("#btnPrevisualizarDeducciones").click(function () {
    if ($("#emp_Id").val() == '') {
        MsgError("¡", "Seleccione el empleado deseado");
        event.preventDefault();
    }
});


