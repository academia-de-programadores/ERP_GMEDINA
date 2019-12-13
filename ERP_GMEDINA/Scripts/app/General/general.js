//
var modal = ["ModalNuevo", "ModalEditar", "ModalInhabilitar", "ModalDetalles"];
var formularios = ["FormNuevo", "FormEditar", "FormInactivar"];
function CierraPopups() {
    $.each(modal, function (index, valor) {
        $("#" + valor).modal('hide');//ocultamos el modal
        $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    });
}
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        method: type,
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        data: params,
        success: callback
    }).fail(function (request, status, error) {
        CierraPopups()
        MsgError("Error", "Verifique su conexion a internet.(si el problema persiste contacte al administrador.)");
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
function limpiarClases(form) {
    var div = null;
    $(form).find(".required").each(function (indice, input) {
        //$(input).val("");
        div = $(input).closest("div");
        div.removeClass("has-error has-warning");
    });
    $(form).find("#form").serializeArray().forEach(function (id, valor) {
        div.find("#error" + id.name).text("");
    });
}
$("#ModalNuevo").on('hidden.bs.modal', function () {
    limpiarClases(this);
});
$("#ModalEditar").on('hidden.bs.modal', function () {
    limpiarClases(this);
});
$("#ModalInhabilitar").on('hidden.bs.modal', function () {
    limpiarClases(this);
});
$(".required").each(function (indice, input) {
    var maxlength = $(input).data("val-maxlength-max");
    var form = $(input).closest("form");
    var txt_maxlength = $(input).data("val-maxlength");
    var txt_required = $(input).data("val-required");
    var id = input.id;
    $(input).keyup(function (event) {
        key(event);
    });
    $(input).keypress(function (event) {
        key(event);
    });
    $(input).focusout(function () {
        var span = $(form).find("#error" + id);
        if ($(input).val() == null || $(input).val()==0 || $(input).val().trim() == "") {
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $(span).addClass("text-danger");
        } else {
            $(span).closest("div").removeClass("has-error");
            $(span).removeClass("text-danger");
        }
    });
    function key(event) {
        var span = $(form).find("#error" + id);
        $(span).closest("div").removeClass("has-error");
        $(span).removeClass("text-danger");
        if ($(input).val().length > maxlength) {
            $(span).addClass("text-warning");
            $(span).closest("div").addClass("has-warning");
            span.text(txt_maxlength);
            event.preventDefault();
        }else {
            $(span).closest("div").removeClass("has-error has-warning");
            $(span).removeClass("text-danger text-warning");
            $(form).find("#error" + id).text("");
        }
    }
});
formularios.forEach(function (formulario) {
 $("#" + formulario).submit(function (e) {
  e.preventDefault();
 });
});
