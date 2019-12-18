var ChildTable = null;
var list = [];
function Remove(Id, lista) {
    var list = [];
    lista.forEach(function (value, index) {
        if (value.Id!=Id) {
            list.push(value);
        }
    });
    return list;
}
function Add(depto_Descripcion, car_Descripcion) {
    if (depto_Descripcion.trim().length != 0 && car_Descripcion.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().length; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Descripcion == depto_Descripcion || Fila.Cargo == car_Descripcion) {            
                if (Fila.Cargo == car_Descripcion) {
                    var span = $("#FormDepartamentos").find("#errorcar_Descripcion");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('El cargo "' + car_Descripcion + '" ya existe');
                    $("#FormDepartamentos").find("#car_Descripcion").focus();
                }
                if (Fila.Descripcion == depto_Descripcion) {
                    var span = $("#FormDepartamentos").find("#errordepto_Descripcion");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('La Descripcion "' + depto_Descripcion + '" ya existe');
                    $("#FormDepartamentos").find("#depto_Descripcion").focus();
                }            
                return null;
            }
        }
        ChildTable.row.add(
            {
                Descripcion: depto_Descripcion.trim(),
                Cargo: car_Descripcion
            }
            ).draw();
    } else {
        if (car_Descripcion.trim().length == 0) {
            var txt_required = $("#FormDepartamentos").find("#car_Descripcion").data("val-required");
            var span = $("#FormDepartamentos").find("#errorcar_Descripcion");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormDepartamentos").find("#car_Descripcion").focus();
        }
        if (depto_Descripcion.trim().length == 0) {
            var txt_required = $("#FormDepartamentos").find("#depto_Descripcion").data("val-required");
            var span = $("#FormDepartamentos").find("#errordepto_Descripcion");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormDepartamentos").find("#depto_Descripcion").focus();
        }
    }
}
function getJson() {
 //declaramos una lista para recuperar en un formato 
 //especifico el json de datatable.
     list = new Array();
 //declaramos el objeto que ira dentro de la vista     
    for (var i = 0; i < ChildTable.data().length; i++) {
        var fila = ChildTable.rows().data()[i];

        var tbDepartamentos =
         {
             Id: i,
             depto_Descripcion: fila.Descripcion,
             tbCargos: { car_Descripcion: fila.Cargo }
         };
        list.push(tbDepartamentos);
    }
    return list;
}
//Tipo de salida
function llenarDropDowlistTipoSalida() {
    _ajax(null,
       '/HistorialSalidas/llenarDropDowlistTipoSalida',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#" + id).append(new Option(value.Descripcion, value.Id));
               });
           });
       });
}
//Razon Salida
function llenarDropDowlistRazonSalida() {
    _ajax(null,
        '/HistorialSalidas/llenarDropDowlistRazonSalida',
        'POST',
        function (result) {
            $.each(result, function (id, Lista) {
                Lista.forEach(function (value, index) {
                    $("#" + id).append(new Option(value.Descripcion, value.Id));
                });
            });
        });
}
//Empleados
function llenarDropDowlistEmpleados() {
    _ajax(null,
        '/HistorialSalidas/llenarDropDowlistEmpleados',
        'POST',
        function (result) {
            $.each(result, function (id, Lista) {
                Lista.forEach(function (value, index) {
                    $("#" + id).append(new Option(value.Descripcion, value.Id));
                    //console.log(value.Id);
                    //console.log(value.Descripcion);
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
//Llamamos los dropdowns
$(document).ready(function () {
    llenarDropDowlistEmpleados();
    llenarDropDowlistTipoSalida();
    llenarDropDowlistRazonSalida();
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
 //Add(Descripcion, Cargo);

    $("#FormDepartamentos").validate();
});
$("#FormCreate").submit(function (e) {
    e.preventDefault();
});
$("#btnCrear").click(function () {
 //declaramos el objeto principal de nuestra tabla y asignamos sus valores
var tbAreas =
    {
        suc_Id: $("#Sucursales").val(),
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
$("#FormDepartamentos").find("#depto_Descripcion").keypress(function (envet) {
    var id = $(this).attr("id");
    var form = $(this).closest("form");
    limpiarSpan(id,form);
});
$("#FormDepartamentos").find("#car_Descripcion").keypress(function (envet) {
    var id = $(this).attr("id");
    var form = $(this).closest("form");
    limpiarSpan(id,form);
});
function limpiarSpan(id,form) {
    var span = $(form).find("#error" + id);
    $(span).closest("div").removeClass("has-error");
    $(span).removeClass("text-danger");
    $(span).closest("div").removeClass("has-warning");
    $(span).removeClass("text-warning");
    span["0"].innerText = "";
}