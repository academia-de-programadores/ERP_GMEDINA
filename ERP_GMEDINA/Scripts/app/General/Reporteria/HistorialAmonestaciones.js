$(document).ready(function () {
    //$("#tamo_Id").val(1);
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    var before = now.getFullYear()-5 + "-" + (month) + "-" + (day);


    $('#Fecha1').val(today);
    $('#Fecha').val(before);
    $('#Identidad').select2();
});
$("#btnPrevisualizarDeducciones").click(function () {
    if ($("#tamo_Id").val() == '') {
        MsgError("¡", "Seleccione el tipo de amonestacion deseado");
        event.preventDefault();
    }
});