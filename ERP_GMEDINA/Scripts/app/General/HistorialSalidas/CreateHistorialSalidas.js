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
function Add(Empleados, Razon, ver) {
    debugger
    if (Empleados.trim().length != 0 || Razon.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().length; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Empleados == ver || Fila.Empleados == 0) {
                if (Fila.Empleados == ver) {
                    var span = $("#FormEmpleados").find("#errorDDOWNEmpleados");
                    $(span).addClass("text-warning");
                    $(span).closest("div").addClass("has-warning");
                    span.text('Seleccione otra opción');
                    $("#FormEmpleados").find("#select2-DDOWNEmpleados-container").focus();
                }
                return null;
            }
            else {
                var span = $("#FormEmpleados").find("#errorDDOWNEmpleados");
                $(span).removeClass("text-warning");
                span.text('');
            }
        }
        ChildTable.row.add(
            {
                Empleados: ver,//Empleados.trim(),
                emp_Id: Empleados,
                Razon: Razon
            }
        ).draw();
        $("#FormEmpleados").find("#Razon").val("");
        $("#FormEmpleados").find("#DDOWNEmpleados").focus();
    } else {
        if (Razon.trim().length == 0) {
            var txt_required = $("#FormEmpleados").find("#Razon").data("val-required");
            var span = $("#FormEmpleados").find("#erroremp_RazonInactivo");
            $(span).addClass("text-danger");
            $(span).closest("div").addClass("has-error");
            span.text(txt_required);
            $("#FormEmpleados").find("#Razon").focus();
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
            emp_Id: fila.emp_Id,
            emp_RazonInactivo: fila.Razon
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
                    $("#DDOWN" + id).append(new Option(value.Descripcion, value.Id));
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
    $("#DDOWNEmpleados").select2();

    llenarDropDowlistEmpleados();
    llenarDropDowlistTipoSalida();
    llenarDropDowlistRazonSalida();

    ChildTable = $(ChildDataTable).DataTable({
  "language": {
   "sProcessing": "Procesando...",
   "sLengthMenu": "Mostrar _MENU_ registros",
   "sZeroRecords": "No se encontraron resultados",
   "sEmptyTable": "•Es necesario agregar al menos un empleado para liquidar",
   "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
   "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 colaboradores por liquidar",
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
   },
   "oAria": {
    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
   }
  },
        pageLength: 4,
        lengthChange: false,
     columns:
         [
             { data: 'Empleados' },
             { data: 'Razon' },
             {
                data: 'emp_Id',
                "visible": false
             },
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
    var Id = $("#FormEmpleados").find("#DDOWNEmpleados").val();
    if (Id == 0) {
        var span = $("#FormEmpleados").find("#errorDDOWNEmpleados");
        $(span).addClass("text-warning");
        $(span).closest("div").addClass("has-warning");
        span.text('Seleccione otra opción');
        $("#FormEmpleados").find("#select2-DDOWNEmpleados-container").focus();
    }
    else {
    var Id = $("#FormEmpleados").find("#DDOWNEmpleados").val();
    var Razon = $("#FormEmpleados").find("#Razon").val();
    var ver = $('#DDOWNEmpleados option:selected').html();
    var valores = Id + Razon + ver;
    for (var i = 0; i < valores.length; i++) {
        if (valores[i] == ">" || valores[i] == "<") {
            MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.");
            return null;
        }
    }
    Add(Id, Razon, ver);    
}
});
$("#FormCreate").submit(function (e) {
    e.preventDefault();
});
$("#btnCrear").click(function () {
    //declaramos el objeto principal de nuestra tabla y asignamos sus valores
    if ($("#TipoSalidas").val() == 0) {
        MsgError("Error", "Es nesesario seleccionar el tipo de la salida.");
    } else if ($("#RazonSalidas").val() == 0) {
        MsgError("Error", "Es nesesario seleccionar la razón de la salida.");
    } else if ($("#hsal_FechaSalida").val() == "") {
        MsgError("Error", "Es necesario seleccionar la fecha en la cual se desocupo el puesto de trabajo.");
    } else {
    //declaramos el objeto principal de nuestra tabla y asignamos sus valores
    var tbHistorialSalidas =
    {
        tsal_Id: $("#TipoSalidas").val(),
        rsal_Id: $("#RazonSalidas").val(),
        hsal_Observacion: $("#hsal_Observacion").val(),
        hsal_FechaSalida: $("#hsal_FechaSalida").val()
    };
        var lista = getJson();
        if (lista == "") {
            MsgError("Error", "Es nesesario seleccionar al menos 1 colaborador.");
        } else {
            if (tbHistorialSalidas != null) {
                data = JSON.stringify({
                    tbHistorialSalidas: tbHistorialSalidas,
                    tbEmpleados: lista
                });
                _ajax(data,
                    '/HistorialSalidas/Create',
                    'POST',
                    function (obj) {
                        if (obj != "-1" && obj != "-2" && obj != "-3") {
                            //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
                            MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                            setTimeout(function () { location.href = "/HistorialSalidas/Index"; }, 5000);
                            $("#btnCrear").attr("disabled", "disabled");
                        } else {
                            MsgError("Error", "No se agregó el registro, contacte al administrador.");
                        }
                    });
            } else {
                MsgError("Error", "Por favor llene todas las cajas de texto.");
            }
        }
}
});
$("#FormEmpleados").find("#DDOWNEmpleados").keypress(function (envet) {
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
