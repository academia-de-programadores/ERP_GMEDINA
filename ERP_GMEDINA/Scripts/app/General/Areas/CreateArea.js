var ChildTable = null;
var list = [];
function getJson() {
 //declaramos una lista para recuperar en un formato 
 //especifico el json de datatable.
     list = new Array();
 //declaramos el objeto que ira dentro de la vista
     var tbDepartamentos = new Object();
     tbDepartamentos.depto_Descripcion = '';
     tbDepartamentos.tbCargos = new Object();
     tbDepartamentos.tbCargos.car_Descripcion = '';

    for (var i = 0; i < ChildTable.data().length; i++) {
     var fila=ChildTable.rows().data()[i];
     tbDepartamentos.depto_Descripcion = fila.Descripcion;
     tbDepartamentos.tbCargos.car_Descripcion = fila.Cargo;
     list.push(tbDepartamentos);
    }
    return list;
}
function llenarDropDownList() {
    _ajax(null,
       '/Areas/llenarDropDowlist',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               console.log(id);
               console.log(Lista);
               Lista.forEach(function (value,index) {
                   $("#" + id).append(new Option(value.Descripcion, value.Id));
               });
           });
       });
}
function Remover(btn) {
 ChildTable
        .row($(btn).parents('tr'))
        .remove()
        .draw();
}
$(document).ready(function () {
    llenarDropDownList();
    ChildTable = $(ChildDataTable).DataTable({
        pageLength: 3,
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
});
$("#add").click(function () {
    var Descripcion = $("#FormDepartamentos").find("#depto_Descripcion").val();    
    var Cargo = $("#FormDepartamentos").find("#car_Descripcion").val();
    var valores=Descripcion + Cargo;
    for (var i = 0; i < valores.length; i++) {
        if (valores[i] == ">" || valores[i] == "<") {
            MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.");
            return null;
        }
    }
 //$("#Sucursales").val("0").change();
 ChildTable.row.add(
  {
   Descripcion: Descripcion.trim(),
   Cargo: Cargo
  }
 ).draw();
});
$("#FormCreate").submit(function (e) {
    e.preventDefault();
});
$("#btnCrear").click(function () {
 //declaramos el objeto principal de nuestra tabla y asignamos sus valores
var tbAreas =
    {
        area_Descripcion: $("#area_Descripcion").val(),
        tbCargos:{car_Descripcion: $("#car_Descripcion").val()},
    };
 var lista = getJson();
 if (tbAreas != null) {
     data = JSON.stringify({
      tbAreas: tbAreas,
      tbDepartamentos: lista
     });
     _ajax(data,
         '/Areas/Create',
         'POST',
         function (obj) {
          if (obj != "-1" && obj != "-2" && obj != "-3") {
           //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
           MsgSuccess("¡Exito!", "Se ah agregado el registro");
          } else {
           MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
          }
         });
    } else {
     MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
