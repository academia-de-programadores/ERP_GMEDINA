$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
});
$(".tablaEditar").click(function () {
    $('#ModalEditar').modal('show');
});
$("#btnInhabilitar").click(function () {
    CierraPopup();
    $('#ModalDelete').modal('show');
});
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    console.log(data);
    _ajax(data,
        '/Habilidades/Create',
        'POST',
        (data) => {
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            if (data != "-1" || data != "-2" || data != "-3") {
                CierraPopup();
            }
        });
});
$("#InActivar").click(function () {
    CierraPopup();
});
$("#btnActualizar").click(function () {
    CierraPopup();
});
function CierraPopup() {
    var modal = ["ModalNuevo", "ModalEditar", "ModalDelete"];
    $.each(modal, function(index,valor) {
        $("#" + valor).modal('hide');//ocultamos el modal
        $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    })        
}
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        type: type,
        data: { params },
        success: function (data) {
            callback(data);
        }
    });
}
function serializar(data) {
    var Data = new Object();
    $.each(data, function (index,valor) {
        Data[valor.name] = valor.value;
    });
    return Data;
}
