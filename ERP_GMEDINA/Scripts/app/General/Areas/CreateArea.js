var ChildTable = null;
var list = [];
function Remove(Id, lista) {
 var list = [];
 lista.forEach(function (value, index) {
  if (value.Id != Id) {
   list.push(value);
  }
 });
 return list;
}
function Add(Areas) {
 ChildTable.row.add(
            {
             Descripcion: Areas.depto_Descripcion.trim(),
             Cargo: Areas.car_Descripcion.trim(),
             car_SalarioMinimo: Areas.car_SalarioMinimo,
             car_SalarioMaximo: Areas.car_SalarioMaximo
            }
            ).draw();
 $("#FormDepartamentos").find(".required").val("");
 $("#FormDepartamentos span").text("");
 $("#FormDepartamentos").find("#depto_Descripcion").focus();
}
function getJson() {
 //declaramos una lista para recuperar en un formato
 //especifico el json de datatable.
 list = new Array();

 for (var i = 0; i < ChildTable.data().length; i++) {
  var fila = ChildTable.rows().data()[i];
  //tbDepartamentos.depto_Descripcion = fila.Descripcion;
  //tbDepartamentos.tbCargos.car_Descripcion = fila.Cargo;
  var tbDepartamentos = {
   depto_Descripcion: fila.Descripcion,
   tbCargos:
    {
     car_Descripcion: fila.Cargo,
     car_SalarioMinimo: fila.car_SalarioMinimo,
     car_SalarioMaximo: fila.car_SalarioMaximo
    }
  };
  list.push(tbDepartamentos);
 }
 return list;
}
function Remover(btn) {
 ChildTable
        .row($(btn).parents('tr'))
        .remove()
        .draw();
}
function llenarDropDownList() {
 _ajax(null,
    '/Areas/llenarDropDowlist',
    'POST',
    function (result) {
     $.each(result, function (id, Lista) {
      Lista.forEach(function (value, index) {
       $("#" + id).append(new Option(value.Descripcion, value.Id));
      });
     });
    });
}
$(document).ready(function () {
 llenarDropDownList();
 ChildTable = $(ChildDataTable).DataTable({
  "language": languageChild,
  pageLength: 10,
  lengthChange: false,
  columns:
   [
         { data: 'Descripcion' },
         { data: 'Cargo' },
         {
          data: 'Acciones',
          defaultContent: '<div>' +
                                 //'<input type="button" class="btn btn-white btn-xs" onclick="Remover(this)" value="Editar" />'+
                                 '<input type="button" class="btn btn-danger btn-xs" onclick="Remover(this)" value="Remover" />' +
                             '</div>'
         }
   ],
  order: [[0, 'asc']]
 });
 RestaurarDT();
});
$("#add").click(function () {
 var area = { cargo: $("#FormNuevo").find("#car_Descripcion").val() };
 var data = $("#FormDepartamentos").serializeArray();
 data = serializarChild(data, "FormDepartamentos");

 var lista = getJson();
 var valid = true;
 lista.forEach(function (value, index) {
  if (value.depto_Descripcion == data.depto_Descripcion) {
   var input = $("#FormDepartamentos").find("#depto_Descripcion")[0];
   var div = $(input).closest(".form-group")[0];
   var span = $(div).find("span")[0];

   $(div).addClass("error");
   span.innerText = 'Cambie la descripcion.';
   $(span).addClass("text-danger");
   valid = false;
  }
  if (value.car_Descripcion == data.car_Descripcion) {
   var input = $("#FormDepartamentos").find("#car_Descripcion")[0];
   var div = $(input).closest(".form-group")[0];
   var span = $(div).find("span")[0];

   $(div).addClass("error");
   span.innerText = 'Cambie la descripcion.';
   $(span).addClass("text-danger");
   valid = false;
  }
 });
 if (!valid) {
  return null;
 }
 if (data.car_SalarioMinimo > data.car_SalarioMaximo) {
  var input = $("#FormDepartamentos #car_SalarioMaximo")[0];
  $(input).focusout();
  return null;
 }
 if (area.cargo == data.car_Descripcion) {
  var span = $("#FormDepartamentos").find("#errorcar_Descripcion")[0];
  span.innerText = "Cargo reservado, por favor cambiar.";
  $(span).addClass("text-danger");
  return null;
 }
 if (data == null) {
  return null;
 }
 _ajax(JSON.stringify({
  Descripcion: data.depto_Descripcion,
  Cargo: data.car_Descripcion
 }),
       "/Areas/Validar/",
       "POST",
       function (obj) {
        if (obj == "-2") {
         MsgError("Error", "Verifique su conexión a internet");
        } else if (obj.length > 0) {
         obj.forEach(function (value, index) {
          var input = $("#FormDepartamentos").find("#" + value.input)[0];
          var div = $(input).closest(".form-group")[0];
          var span = $(div).find("span")[0];

          $(div).addClass("error");
          span.innerText = 'Cambie la descripcion.';
          $(span).addClass("text-danger");
         });
        } else {
         var Areas = {};
         Add(data);
        }
       }
 );
});
$("#FormCreate").submit(function (e) {
 e.preventDefault();
});
$("#btnCrear").click(function () {
 //declaramos el objeto principal de nuestra tabla y asignamos sus valores
 var tbAreas = $("#FormNuevo").serializeArray();
 tbAreas = serializar(tbAreas);
 var lista = getJson();
 var valid = true;
 lista.forEach(function (value, indice) {
  if (value.tbCargos.car_Descripcion == tbAreas.car_Descripcion) {
   valid = false;
  }
 });
 if (!valid) {
  var span = $("#FormNuevo").find("#errorcar_Descripcion")[0];
  span.innerText = "Cargo reservado, por favor cambiar.";
  $(span).addClass("text-danger");
  return null;
 }
 if (tbAreas.car_SalarioMinimo > tbAreas.car_SalarioMaximo) {
  var input = $("#FormNuevo #car_SalarioMaximo")[0];
  $(input).focusout();
  return null;
 }
 if (tbAreas != null) {
  data = JSON.stringify({
   tbAreas: tbAreas,
   tbDepartamentos: lista
  });
  _ajax(data,
      '/Areas/Create',
      'POST',
      function (obj) {
       if (obj.codigo != "-1" && obj.codigo != "-2" && obj.codigo != "-3") {
        //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.")
        ChildTable.clear().draw();
        $("#FormAreas").find("#suc_Id option[value='0']").attr("selected", true);
        $("#FormAreas").find("#area_Descripcion").val("");
        $("#FormAreas").find("#car_Descripcion").val("");
        setTimeout(function () { window.location.href = "/Areas/Index"; }, 3000);
       } else {
        if (obj.input != undefined) {
         var input = $("#FormNuevo").find("#" + obj.input)[0];
         var div = $(input).closest(".form-group")[0];
         var asterisco = $(div).find("label font")[0];
         var span = $(div).find("span")[0];

         $(div).addClass("error");
         asterisco.color = "red";
         span.innerText = 'ya existe, por favor cambie la descripcion';
         $(span).addClass("text-danger");
        }
        MsgError("Error", "No se agregó el registro, contacte al administrador.");
       }
      });
 } else {
  MsgError("Error", "Por favor llene todas las cajas de texto.");
 }
});
$("#FormDepartamentos").find("#depto_Descripcion").keypress(function (envet) {
 if (alerta($(this).closest("div"))) {
  return null;
 }
 var id = $(this).attr("id");
 var form = $(this).closest("form");
 limpiarSpan(id, form);
});
$("#FormDepartamentos").find("#car_Descripcion").keypress(function (envet) {
 if (alerta($(this).closest("div"))) {
  return null;
 }
 var id = $(this).attr("id");
 var form = $(this).closest("form");
 limpiarSpan(id, form);
});
function limpiarSpan(id, form) {
 var span = $(form).find("#error" + id);
 $(span).closest("div").removeClass("has-error has-warning");
 $(span).removeClass("text-danger text-warning");
 $(form).find("#error" + id).text("");
}
function alerta(div) {
 var val_maxlength = $(div).find("input").data("val-maxlength-max");
 if ($(div).find("input").val().trim().length >= val_maxlength) {
  var txt_maxlength = $(div).find("input").data("val-maxlength");
  var span = $(div).find("span");
  $(span).addClass("text-warning");
  $(span).closest("div").addClass("has-warning");
  span.text(txt_maxlength);
  event.preventDefault();
  return true;
 }
 return false;
}
var forms = $("form");
$.each(forms, function (value, index) {
 var form = this;
 $(form).find("#car_SalarioMaximo").focusout(function () {
  var valor_minimo = $(form).find("#car_SalarioMinimo").val()
  if (valor_minimo > $(this).val()) {
   var div = $(this).closest("div")[0];
   var span = $(div).find("span")[0];
   span.innerText = "el sueldo máximo debe se mayor ó igual al sueldo minimo "
   $(span).addClass("text-warning");
   $(this).val(valor_minimo);
  }
 });
});
