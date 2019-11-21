//
var modal = ["ModalNuevo", "ModalEditar", "ModalInhabilitar", "ModalDetalles"];
function CierraPopups() {
    $.each(modal, function (index, valor) {
        $("#" + valor).modal('hide');//ocultamos el modal
        $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    });
}
function _ajax(params, uri, type, callback, fError) {
    $.ajax({
        url: uri,
        method: type,
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        data: params,
        success: callback
    }).fail(function (request, status, error) {
        CierraPopups()
        MsgError("Error", "Verifique su conexion a internet.(si el problema persistecontacte al administrador.)");
    });
}
function serializar(data) {
    var Data = new Object();
    var verificacion = true;
    $.each(data, function (index, valor) {
        var value = valor.value.trim();
        if (value != "") {
            Data[valor.name] = value;
        } else {
            SetearClases(valor.name, "error", "valid", "val-required");
            verificacion = false;
        }
    });
    if (verificacion) {
        return Data;
    } else {
        return null;
    }
}
function FechaFormato(pFecha) {
    if (pFecha != null && pFecha != undefined) {
        var fechaString = pFecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = fechaActual.getMonth() + 1;
        var dia = pad2(fechaActual.getDate());
        var anio = fechaActual.getFullYear();
        var hora = pad2(fechaActual.getHours());
        var minutos = pad2(fechaActual.getMinutes());
        var segundos = pad2(fechaActual.getSeconds().toString());
        var FechaFinal = dia + "/" + mes + "/" + anio + " " + hora + ":" + minutos + ":" + segundos;
        return FechaFinal;
    }
    return '';
}
function pad2(number) {
    return (number < 10 ? '0' : '') + number
}
function SetearClases(Id, Agregar, Remover, valorError) {
    modal.forEach(function (indice, value) {
        var spam = $("#" + indice).find("#error" + Id);
        var input = $("#" + indice).find("#" + Id);
        if (valorError == "") {
            spam.text(valorError);
        } else {
            spam.text(input.data(valorError));
        }
        input.addClass(Agregar);
        input.removeClass(Remover);
    });
}
function LimpiarControles(Controles) {
    $.each(Controles, function (index, value) {
        $("#" + value).val("");
    });
}
function MsgError(Titulo, Mensajes) {
    iziToast.error({
        title: Titulo,
        message: Mensajes,
    });
}
function MsgInfo(Titulo, Mensajes) {
    iziToast.info({
        title: Titulo,
        message: Mensajes,
    });
}
function MsgSuccess(Titulo, Mensajes) {
    iziToast.success({
        title: Titulo,
        message: Mensajes,
    });
}
function MsgWarning(Titulo, Mensajes) {
    iziToast.warning({
        title: Titulo,
        message: Mensajes,
    });
}
//$("#btnPrueva").click()
//form.validate()
