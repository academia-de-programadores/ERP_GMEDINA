﻿var ChildTable = null;
var Admin = false;
var list = [];
var inactivar = [];
var dRow = null;
var Entidad = '';
var hablilitar = function () {

};
function Add(depto_Descripcion, car_Descripcion) {
 ChildTable.row.add(
            {
             Descripcion: depto_Descripcion.trim(),
             Cargo: car_Descripcion
            }
            ).draw();
 $("#FormDepartamentos").find(".required").val("");
 $("#FormDepartamentos").find("#depto_Descripcion").focus();
}
function getJson() {
 //declaramos una lista para recuperar en un formato
 //especifico el json de datatable.
 list = new Array();
 //declaramos el objeto que ira dentro de la vista
 var info = ChildTable.rows().data();
 for (var i = 0; i < ChildTable.data().length; i++) {
  var fila = info[i];
  var tbDepartamentos =
   {
    depto_Id: info[i].Id,
    depto_Descripcion: fila.Descripcion,
    car_Descripcion: fila.Cargo,
    Accion: fila.Accion
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
function Edit(btn) {
 var tr = $(btn).closest('tr');
 var row = ChildTable.row(tr);
 var datos = row.data();
 dRow = row;
 depto_Id = datos.Id;
 $('#ModalEditar').find("#depto_Descripcion").val(datos.Descripcion);
 $('#ModalEditar').find("#car_Descripcion").val(datos.Cargo);

 $('#ModalEditar').modal('toggle');
 $('#ModalEditar').modal('show');
 //$('#ModalEditar').modal('hide');
}
function llenarChild() {
 _ajax(JSON.stringify({ id: area_Id }),
     '/Areas/cargarChild',
     'POST',
     function (data) {
      ChildTable.clear();
      ChildTable.draw();
      data.forEach(function (valor, indice) {
       Acciones = valor.depto_Estado ? null : Admin ?
        "<div>" +
            "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
        "</div>" :
        '';
       ChildTable.row.add(
           {
            Id: valor.depto_Id,
            Descripcion: valor.depto_Descripcion,
            Cargo: valor.car_Descripcion,
            Acciones: Acciones
           });
      });
      ChildTable.draw();
     });
}
$(document).ready(function () {
 if (Admin) {
  hablilitar = function (btn) {
   var tr = $(btn).closest('tr');
   var row = ChildTable.row(tr);
   var datosDepartamento = row.data();
   ChildTable.row(tr).remove();
   ChildTable.row.add(
   {
    Id: datosDepartamento.Id,
    Descripcion: datosDepartamento.Descripcion,
    Cargo: datosDepartamento.Cargo,
    Acciones: '<div>' +
                '<input type="button" class="btn btn-danger btn-xs" onclick="Remover(this)" value="Remover" />' +
            '</div>',
    Accion: 'a'
   }
   ).draw();
  }
 }
 ChildTable = $(ChildDataTable).DataTable({
  "language": language,
  pageLength: 3,
  lengthChange: false,
  columns:
   [
         { data: 'Descripcion' },
         { data: 'Cargo' },
         {
          data: 'Acciones',
          defaultContent: '<div>' +
                                 '<input type="button" class="btn btn-white btn-xs" onclick="Edit(this)" value="Editar" />' +
                             '</div>'
         }
   ],
  order: [[0, 'asc']]
 });
 llenarChild();
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
         MsgError("Error", "Verifique su conexión a internet.");
        } else if (obj.length > 0) {
         obj.forEach(function (value, index) {
          var input = $("#FormDepartamentos").find("#" + value.input)[0];
          var div = $(input).closest(".form-group")[0];
          var span = $(div).find("span")[0];

          $(div).addClass("error");
          span.innerText = 'Cambie la descripción.';
          $(span).addClass("text-danger");
         });
        } else {
         Add(data.depto_Descripcion, data.car_Descripcion);
        }
       }
 );
});
$("#FormCreate").submit(function (e) {
 e.preventDefault();
});
$("#btnCrear").click(function () {
 var tbAreas = $("#FormAreas").serializeArray();
 tbAreas = serializar(tbAreas);
 var lista = getJson();
  if (tbAreas != null) {
  data = JSON.stringify({
   cAreas: tbAreas,
   Departamentos: lista,
   inactivar: inactivar
  });
  _ajax(data,
      '/Areas/Edit',
      'POST',
      function (obj) {
       var input = $("#FormNuevo").find("#area_Descripcion")[0];
       var div = $(input).closest(".form-group")[0];
       var asterisco = $(div).find("label font")[0];
       if (obj.codigo == "-2") {
           MsgError("Error", "No se editó el registro, contacte al administrador.");
       }
       if (obj.codigo == "-3") {
        console.log("Violación de la seguridad.");
       }

       if (obj.codigo != "-1" && obj.codigo != "-2" && obj.codigo != "-3") {
        setTimeout(function () { window.location.href = "/Areas/Index"; }, 3000);
       } else {
        $(div).addClass("error");
        asterisco.color = "red";
        span.innerText = 'Ya existe, por favor cambie la descripción.';
        $(span).addClass("text-danger");
       }
      });
 } else {
      MsgError("Error", "Por favor llene todas las cajas de texto.");
 }
});
//$("#FormDepartamentos").find("#depto_Descripcion").keypress(function (envet) {
// if (alerta($(this).closest("div"))) {
//  return null;
// }
// var id = $(this).attr("id");
// var form = $(this).closest("form");
// limpiarSpan(id, form);
//});
//$("#FormDepartamentos").find("#car_Descripcion").keypress(function (envet) {
// if (alerta($(this).closest("div"))) {
//  return null;
// }
// var id = $(this).attr("id");
// var form = $(this).closest("form");
// limpiarSpan(id, form);
//});

$("#ModalEditar").find("#btnActualizar").on("click", function () {
 var area = { cargo: $("#FormAreas").find("#car_Descripcion").val() };
 var depto =
     {
      Id: dRow.data().Id,
      Cargo: dRow.data().Cargo,
      Descripcion: $('#ModalEditar').find("#depto_Descripcion").val(),
      Accion: 'e'
     }
 if (depto.Cargo == area.cargo) {
  var span = $("#ModalEditar").find("#errorcar_Descripcion")[0];
  span.innerText = "Cargo reservado, por favor cambiar.";
  $(span).addClass("text-danger");
  return null;
 }
 _ajax(JSON.stringify({
  Descripcion: depto.Descripcion
 }),
      "/Areas/ValidarDepto/",
      "POST",
      function (obj) {
       if (obj == "-2") {
        MsgError("Error", "Verifique su conexión a internet.");
       } else if (obj.length > 0) {
        var input = $("#ModalEditar").find("#" + obj[0].input)[0];
        var div = $(input).closest(".form-group")[0];
        var span = $(div).find("span")[0];

        $(div).addClass("error");
        span.innerText = 'Cambie la descripcion.';
        $(span).addClass("text-danger");
       } else {
        ChildTable
        .row(dRow)
        .remove();

        ChildTable
        .row
        .add(depto)
        .draw();
        $('#ModalEditar').modal('hide');
        dRow = null;
       }
      }

);
});
$("#ModalInactivar").find("#InActivar").on("click", function () {
 if (Entidad == 'Depto') {
  var depto =
 {
  depto_Id: dRow.data().Id,
  depto_RazonInactivo: $("#ModalInactivar").find("#depto_RazonInactivo").val(),
 };
  if (depto.depto_RazonInactivo.trim() == '') {
   return null;
  }
  inactivar.push(depto);
  ChildTable
      .row(dRow)
      .remove()
      .draw();
  dRow = null;
  $('#ModalInactivar').modal('hide');
 } else {
  var area_Razoninactivo = $("#ModalInactivar").find("#depto_RazonInactivo").val()
  _ajax(JSON.stringify({ area_Razoninactivo: area_Razoninactivo }),
      '/Areas/Delete',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
        //MsgSuccess("¡Exito!", "Se ah Eliminado el Area");
        $(location).attr('href', '/Areas/Index');
       } else {
           MsgError("Error", "No se inactivó el registro, contacte al administrador.");
       }
      });
 }
});
$("#btnInactivar").on("click", function () {
 $("#depto_RazonInactivo").val("");
 $('#ModalEditar').modal('hide');
 $('#ModalInactivar').modal('toggle');
 $('#ModalInactivar').modal('show');
 $("#ModalInactivar").find("#depto_RazonInactivo").focus();
 Entidad = "Depto";
});
$("#btnInactivarArea").on("click", function () {
 $('#ModalInactivar').modal('toggle');
 $('#ModalInactivar').modal('show');
 $("#ModalInactivar").find("#depto_RazonInactivo").focus();
 Entidad = "Area";
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
