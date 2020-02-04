//
var modal = ["ModalNuevo", "ModalEditar", "ModalInactivar", "ModalDetalles"];
var formularios = ["FormNuevo", "FormEditar", "FormInactivar"];
var languageChild = {
 "sProcessing": "Procesando...",
 "sLengthMenu": "Mostrar _MENU_ registros",
 "sZeroRecords": "No se encontraron resultados",
 "sEmptyTable": "No hay registros para mostrar.",
 "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
 "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
 "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
 "sInfoPostFix": "",
 "sSearch": "Buscar:",
 "sUrl": "",
 "sInfoThousands": ",",
 "sLoadingRecords": "Cargando...",
 "oPaginate": {
  "sFirst": "Primero",
  "sLast": "Último",
  "sNext": "Siguiente",
  "sPrevious": "Anterior"
 }, "oAria": {
  "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
  "sSortDescending": ": Activar para ordenar la columna de manera descendente"
 }
};
var htmlSpiner =
    `<div id="ibox1" class="sk-spinner sk-spinner-wave">
                <div class="sk-rect1"></div>
                <div class="sk-rect2"></div>
                <div class="sk-rect3"></div>
                <div class="sk-rect4"></div>
                <div class="sk-rect5"></div>
             </div>`;
var language = {
 "sProcessing": "Procesando...",
 "sLengthMenu": "Mostrar _MENU_ registros",
 "sZeroRecords": "No se encontraron resultados",
 "sEmptyTable": htmlSpiner,
 "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
 "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
 "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
 "sInfoPostFix": "",
 "sSearch": "Buscar:",
 "sUrl": "",
 "sInfoThousands": ",",
 "sLoadingRecords": "Cargando...",
 "oPaginate": {
  "sFirst": "Primero",
  "sLast": "Último",
  "sNext": "Siguiente",
  "sPrevious": "Anterior"
 }, "oAria": {
  "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
  "sSortDescending": ": Activar para ordenar la columna de manera descendente"
 }
};
function CierraPopups() {
 $.each(modal, function (index, valor) {
  $("#" + valor).modal('hide');//ocultamos el modal
  $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
  $('.modal-backdrop').remove();//eliminamos el backdrop del modal
 });
}
$(document).on('ready', function () {
 var modal = $('.modal');
});
$(".modal").on("load", function () {
 alert("lolis");
});

function _POST(params, uri, callback) {
 $.post(uri, params)
 .done(callback)
 .error(function (e) {
  CierraPopups()
  MsgError("Error", "Verifique su conexión a internet. (Si el problema persiste contacte al administrador.)");
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
  MsgError("Error", "Verifique su conexión a internet. (Si el problema persiste contacte al administrador.)");
 });
}
function serializar(data) {
 var Modals = $(".modal");
 var modal;
 if (Modals.length == 0) {
  Modals = $("form");
  modal = Modals[0];
 }
 var primerInput = true;
 $.each(Modals, function (indice, valor) {
  if (valor.style.display == "block") {
   modal = valor;
  }
 });
 var Data = new Object();
 var verificacion = true;
 $.each(data, function (index, valor) {
  var value = valor.value.trim();
  if (value != "" && value != "0") {
   Data[valor.name] = value;
  } else {
   if (primerInput) {
    primerInput = false;
    $(modal).find("#" + valor.name).focus();
   }
   var input = $(modal).find("#" + valor.name)[0];
   if ($(input).hasClass("required")) {
    var div = $(input).closest(".form-group");
    var asterisco = $(div).find("font");
    var label = $(div).find("label")[0];
    var span = $(input).closest("div").find("span")[0];
    asterisco[0].color = "red";
    var txtlabel = label.innerText;
    //var span = input.offsetParent.children[1];
    var txtRequired = $(input).data("val-required")
    span.innerText = txtRequired == undefined ? 'El campo ' + txtlabel.replace("*", "") + ' es requerido' : txtRequired;
    $(input).addClass("error");
    $(span).addClass("text-danger");
    verificacion = false;
    if (input.type == "select-one") {
     primerInput = true;
    }
   }
  }
 });
 if (verificacion) {
  return Data;
 } else {
  $(modal).data('open', true);
  return null;
 }
}
function serializarChild(data, form) {
 var primerInput = true;
 var Modal = $("#" + form)[0];
 var Data = new Object();
 var verificacion = true;
 $.each(data, function (index, valor) {
  var value = valor.value.trim();
  if (value != "" && value != "0") {
   Data[valor.name] = value;
  } else {
   if (primerInput) {
    primerInput = false;
    $(Modal).find("#" + valor.name).focus();
   }
   var input = $(Modal).find("#" + valor.name)[0];
   if ($(input).hasClass("required")) {
    var div = $(input).closest(".form-group");
    var asterisco = $(div).find("font");
    var label = $(div).find("label")[0];
    var span = $(input).closest("div").find("span")[0];
    asterisco[0].color = "red";
    var txtlabel = label.innerText;
    //var span = input.offsetParent.children[1];
    var txtRequired = $(input).data("val-required")
    span.innerText = txtRequired == undefined ? 'El campo ' + txtlabel.replace("*", "") + ' es requerido' : txtRequired;
    $(input).addClass("error");
    $(span).addClass("text-danger");
    verificacion = false;
    if (input.type == "select-one") {
     primerInput = true;
    }
   }
  }
 });
 if (!verificacion) {
  return null;
 }
 return Data;
}
function serializarPro(data) {
 var Data = new Object();
 $.each(data, function (index, valor) {
  if (index == "")
   index = "per_Direccion";
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

function BinToCheckBox(BinVal) {
 if (BinVal == true)
  return '<input type="checkbox" checked disabled>'
 else if (BinVal == false)
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

function FechaFormatoSimpleAlt(pFecha) {
 if (pFecha != null && pFecha != undefined) {
  var fechaString = pFecha.substr(6);
  var fechaActual = new Date(parseInt(fechaString));
  var mes = pad2(fechaActual.getMonth() + 1);
  var dia = pad2(fechaActual.getDate());
  var anio = fechaActual.getFullYear();
  var hora = pad2(fechaActual.getHours());
  var minutos = pad2(fechaActual.getMinutes());
  var segundos = pad2(fechaActual.getSeconds().toString());
  var FechaFinal = dia + "/" + mes + "/" + anio;
  return FechaFinal;
 }
 return '';
}

function pad2(number) {
 return (number < 10 ? '0' : '') + number
}
function SetearClases(Id, Agregar, Remover, valorError) {
 modal.forEach(function (indice, value) {
  //var span = $("#" + indice).find("#error" + Id);
  var input = $("#" + indice).find("#" + Id);
  var span = $(input).closest("div").find("span");
  if (valorError == "") {
   span.text(valorError);
  } else {
   span.text(input.data(valorError));
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
  title: "¡Éxito!",//Titulo,
  message: Mensajes,
 });
}
function MsgWarning(Titulo, Mensajes) {
 //iziToast.warning({
 //    title: Titulo,
 //    message: Mensajes,
 //});
 iziToast.success({
  title: "¡Éxito!",
  message: Mensajes,
 });
}
function limpiarClases(form) {
 var div = null;
 var asterisco = null;
 var span = null;
 $(form).find(".required").each(function (indice, input) {
  div = $(input).closest(".form-group");
  asterisco = $(div).find("font")[0];
  input.nextElementSibling.innerText = '';
  $(div).find("div").removeClass("has-error has-warning");
  $(input).removeClass("error warning");
  asterisco.color = 'black';
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
 var label = $(input).closest(".form-group").find("label")[0];
 label.innerHTML = label.innerHTML + '<font color="black">*</font>';
 var modal = $(input).closest('.modal');
 var asterisco = $(label).find("font")[0];
 $(input).keyup(function (event) {
  key(event);
 });
 $(input).keypress(function (event) {
  key(event);
 });
 $(input).click(function () {
  var div = $(input).closest(".form-group")[0];
  var span = $(div).find("span")[0];
  $(div).removeClass("error");
  span.innerText = '';
  $(span).removeClass("text-danger");
  if (asterisco != undefined) {
   asterisco.color = "black";
  }
 });
 $(input).focusout(function () {
  //var span = $(form).find("#error" + id);
  var span = $(input).closest("div").find("span");
  if ($(input).val() == null || $(input).val() == 0 || $(input).val().trim() == "") {
   asterisco.color = "red";
   $(span).closest("div").addClass("has-error");
   span.text(txt_required);
   $(span).addClass("text-danger");
  } else {
   $(input).removeClass("error");
   asterisco.color = "black";
   $(span).closest("div").removeClass("has-error");
   $(span).removeClass("text-danger");
  }
 });
 function key(event) {
  $(input).removeClass("error");
  asterisco.color = "black";
  //var lol = $(form).find("#error" + id);
  //$(form).find("#error" + id)[0].innerText = '';
  //var span = $(form).find("#error" + id);
  var span = $(input).closest("div").find("span");
  span[0].innerText = '';
  $(span).closest("div").removeClass("has-error");
  $(span).removeClass("text-danger");
  if ($(input).val().length >= maxlength) {
   $(span).addClass("text-warning");
   $(span).closest("div").addClass("has-warning");
   span.text(txt_maxlength);
   event.preventDefault();
  } else {
   $(span).closest("div").removeClass("has-error has-warning");
   $(span).removeClass("text-danger text-warning");
   span[0].innerText = '';
   //$(form).find("#error" + id).text("");
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
 var regex = new RegExp("[A-Za-z��������������������������������������������� ]");
 var key = e.keyCode || e.which;
 key = String.fromCharCode(key);
 if (!regex.test(key)) {
  e.returnValue = false;
  if (e.preventDefault) {
   e.preventDefault();
  }
 }
};

function SinCaracteresEspeciales(e) {
 var regex = new RegExp("[A-Za-z���������������������������������������������1234567890 ]");
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
function validarDT(obj) {
 if (obj == "-2") {
  //$("#ibox1").find(".ibox-content").hide();
  //$("#ibox1").append('verifique su conexion a internet. (Sí el problema persiste llame al administrador)');
  var ventana = $('#IndexTable tbody td.dataTables_empty');
  ventana[0].innerHTML = "Verifique su conexión a internet.(Si el problema persiste contacte al administrador.)";
  MsgError("Error", "No se pudo cargar la información, contacte al administrador.");
  return true;
 } else {
  if (obj.Length == 0) {
   $("#ibox1").find(".ibox-content").hide();
   $(".dataTables_empty").append('No hay registros para mostrar.');
  } else {
   return false;
  }
  return true;
 }
}
function RestaurarDT() {
}

$('.modal').modal({ backdrop: 'static', keyboard: false });
$(".modal").modal('hide');//ocultamos el modal
$('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
$('.modal-backdrop').remove();//eliminamos el backdrop del modal