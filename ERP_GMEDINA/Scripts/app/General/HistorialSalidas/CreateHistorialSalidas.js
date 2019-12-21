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
function Add(Empleados, Razon ,ver) {
    if (Empleados.trim().length != 0 && Razon.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().length; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Empleados == Empleados || Fila.Razon == Razon) {
                //if (Fila.Razon == Razon) {
                //    var span = $("#FormEmpleados").find("#erroremp_RazonInactivo");
                //    $(span).addClass("text-warning");
                //    $(span).closest("div").addClass("has-warning");
                //    span.text('El cargo "' + car_Descripcion + '" ya existe');
                //    $("#FormDepartamentos").find("#car_Descripcion").focus();
                //}
                if (Fila.Empleados == Empleados) {
                    var span = $("#FormEmpleados").find("#errorEmpleados");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('El empleado "' + Empleados + '" ya existe');
                    $("#FormEmpleados").find("#Empleados").focus();
                }
                return null;
            }
        }
        ChildTable.row.add(
            {
                Empleados: ver,//Empleados.trim(),
                Razon: Razon
            }
        ).draw();
        $("#FormEmpleados").find("#Empleados").val("");
        $("#FormEmpleados").find("#Razon").val("");
        $("#FormEmpleados").find("#Empleados").focus();
    } else {
        if (Razon.trim().length == 0) {
            var txt_required = $("#FormEmpleados").find("#Razon").data("val-required");
            var span = $("#FormEmpleados").find("#erroremp_RazonInactivo");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormEmpleados").find("#Razon").focus();
        }
        if (Empleados.trim().length == 0) {
            var txt_required = $("#FormEmpleados").find("#Empleados").data("val-required");
            var span = $("#FormEmpleados").find("#errorEmpleados");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormEmpleados").find("#Empleados").focus();
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
            tbEmpleados: fila.Empleados,
            tbEmpleados: fila.Razon
            //,tbCargos: { car_Descripcion: fila.Cargo }
        };
        list.push(tbEmpleados);
    }
    return list;
}
function Remover(btn) {
    ChildTable
        .row($(btn).parents('tr'))
        .remove()
        .draw();
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
            { data: 'Empleados' },
            { data: 'Razon' },
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
    var Id = $("#FormEmpleados").find("#Empleados").val();    
    var Razon = $("#FormEmpleados").find("#Razon").val();
    var ver = $('#Empleados option:selected').html();
    console.log(Id);
    console.log(Razon);
    console.log(ver);
    var valores = Id + Razon + ver;
    for (var i = 0; i < valores.length; i++) {
        if (valores[i] == ">" || valores[i] == "<") {
            MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.");
            return null;
        }
    }
    Add(Id, Razon ,ver);

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
        tbCargos:{Razon: $("#Razon").val()},
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
$("#FormEmpleados").find("#Empleados").keypress(function (envet) {
    if (alerta($(this).closest("div"))) {
        return null;
    }
    var id = $(this).attr("id");
    var form = $(this).closest("form");
    limpiarSpan(id, form);
});
$("#FormEmpleados").find("#Razon").keypress(function (envet) {
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