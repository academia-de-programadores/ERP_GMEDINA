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
function Add(Nombre, Observacion) {
    if (Nombre.trim().length != 0 && Observacion.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().length; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Nombre == Nombre || Fila.Observacion == Observacion) {            
                if (Fila.Observacion == Observacion) {
                    var span = $("#FormEmpleados").find("#erroremp_RazonInactivo");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('El cargo "' + Observacion + '" ya existe');
                    $("#FormEmpleados").find("#Observacion").focus();
                }
                if (Fila.Descripcion == Nombre) {
                    var span = $("#FormEmpleados").find("#errorNombre");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('La Nombre "' + Nombre + '" ya existe');
                    $("#FormEmpleados").find("#Nombre").focus();
                }            
                return null;
            }
        }
        ChildTable.row.add(
            {
                Descripcion: Nombre.trim(),
                Cargo: Observacion
            }
            ).draw();
    } else {
        if (Observacion.trim().length == 0) {
            var txt_required = $("#FormEmpleados").find("#Observacion").data("val-required");
            var span = $("#FormEmpleados").find("#erroremp_RazonInactivo");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormEmpleados").find("#Observacion").focus();
        }
        if (Nombre.trim().length == 0) {
            var txt_required = $("#FormEmpleados").find("#Nombre").data("val-required");
            var span = $("#FormEmpleados").find("#errorNombre");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormEmpleados").find("#Nombre").focus();
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

        var tbEmpleados =
         {
             Id: i,
             Nombre: fila.Descripcion,
             tbCargos: { Observacion: fila.Cargo }
         };
        list.push(tbEmpleados);
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
            { data: 'Nombre' },
            { data: 'Observacion' },
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
    var Descripcion = $("#FormEmpleados").find("#Nombre").val();    
    var Cargo = $("#FormEmpleados").find("#Observacion").val();
    var valores=Descripcion + Cargo;
    for (var i = 0; i < valores.length; i++) {
        if (valores[i] == ">" || valores[i] == "<") {
            MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.");
            return null;
        }
    }
 //Add(Descripcion, Cargo);

    $("#FormEmpleados").validate();
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
        tbCargos:{Observacion: $("#Observacion").val()},
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
$("#FormEmpleados").find("#Nombre").keypress(function (envet) {
    var id = $(this).attr("id");
    var form = $(this).closest("form");
    limpiarSpan(id,form);
});
$("#FormEmpleados").find("#Observacion").keypress(function (envet) {
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