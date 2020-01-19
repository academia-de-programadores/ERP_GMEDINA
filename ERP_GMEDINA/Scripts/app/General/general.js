//
var modal = ["ModalNuevo", "ModalEditar", "ModalInactivar", "ModalDetalles"];
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
            verificacion = false;
        }
    });
    if (verificacion) {
        return Data;
    } else {
        return null;
    }
}

function serializarPro(data) {
    var Data = new Object();
    $.each(data, function (index, valor) {
        var value = valor.value.trim();
        if (value != "") {
            Data[valor.name] = value;
        } else {
            Data[valor.name] = "";
        }
    });

    return Data;
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

function BinToCheckBox(BinVal)
{
    if(BinVal == true)
        return '<input type="checkbox" checked disabled>'
    else if(BinVal == false)
        return '<input type="checkbox" disabled>'
}

function FechaFormatoSimple(pFecha) {
    if (pFecha != null && pFecha != undefined) {
        var fechaString = pFecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = pad2(fechaActual.getMonth() + 1);
        var dia = pad2(fechaActual.getDate());
        var anio = fechaActual.getFullYear();
        var hora = pad2(fechaActual.getHours());
        var minutos = pad2(fechaActual.getMinutes());
        var segundos = pad2(fechaActual.getSeconds().toString());
        var FechaFinal = anio + "-" + mes + "-" + dia;
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
    //iziToast.warning({
    //    title: Titulo,
    //    message: Mensajes,
    //});
    iziToast.success({
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
$("#ModalInactivar").on('hidden.bs.modal', function () {
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
    $(input).focusin(function () {
        var lol = $(form).find("#error" + id);
        $(form).find("#error" + id)[0].innerText = '';
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
        if ($(input).val().length >= maxlength) {
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

function Mayor18() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear() - 18;
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return today = yyyy + '-' + mm + '-' + dd;
}

function validarEmail(valor) {
    if (/^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i.test(valor)) {
        return valor;
    } else {
        return " ";
    }
}
function alphanumeric(e) {
    var regex = new RegExp("[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ ]");
    var key = e.keyCode || e.which;
    key = String.fromCharCode(key);
    if (!regex.test(key)) {
        e.returnValue = false;
        if (e.preventDefault) {
            e.preventDefault();
        }
    }
};

function Numericos(e) {
    var regex = new RegExp("[1234567890]");
    var key = e.keyCode || e.which;
    key = String.fromCharCode(key);
    if (!regex.test(key)) {
        e.returnValue = false;
        if (e.preventDefault) {
            e.preventDefault();
        }
    }
};



///Esta reemplazala
function FechaFormatoSimple(pFecha) {
    if (pFecha != null && pFecha != undefined) {
        var fechaString = pFecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = pad2(fechaActual.getMonth() + 1);
        var dia = pad2(fechaActual.getDate());
        var anio = fechaActual.getFullYear();
        var hora = pad2(fechaActual.getHours());
        var minutos = pad2(fechaActual.getMinutes());
        var segundos = pad2(fechaActual.getSeconds().toString());
        var FechaFinal = anio + "-" + mes + "-" + dia;
        return FechaFinal;
    }
    return '';
};
