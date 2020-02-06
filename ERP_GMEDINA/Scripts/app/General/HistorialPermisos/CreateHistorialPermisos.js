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

function compare_dates() {

    var fecha1 = $("#hper_FechaInicio").val();
    var fecha2 = $("#hper_FechaFin").val();
    var fechalimite = '01/01/1900';



    if (Date.parse(fecha1) < Date.parse(fechalimite) && Date.parse(fecha2) < Date.parse(fechalimite)) {
        MsgError("Error", "Fechas no válidas");
    }

    else if (Date.parse(fecha1) < Date.parse(fechalimite)) {
        MsgError("Error", "Fecha inicio no válida");
    }
    else if (Date.parse(fecha2) < Date.parse(fechalimite)) {
        MsgError("Error", "Fecha fin no válida");
    }

    else {

        return true;
    }
}
$(document).ready(function () {
    $("#ddlEmpleados").select2();
    $("#hper_PorcentajeIndemnizado").val(0);
    var date = new Date();

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = year + "-" + month + "-" + day;
    $("#hper_fechaInicio").attr("value", today);

    llenarDropDowlistEmpleados();
    llenarDropDowlistTipoPermisos();

    ChildTable = $(ChildDataTable).DataTable({
        "language": languageChild,
        pageLength: 3,
        lengthChange: false,
        columns:
            [
                { data: 'Empleados' },
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
function Add(Empleados, ver) {
    if (Empleados.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().length; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Empleados == '0' || Fila.Empleados == ver) {
                var span = $("#FormEmpleados").find("#errorEmpleados");
                $(span).addClass("text-warning");
                $(span).closest("div").addClass("has-warning");
                span.text('Seleccione otra opción');
                $("#FormEmpleados").find("#ddlEmpleados").focus();
                return null;
            }
            else {
                var span = $("#FormEmpleados").find("#errorddlEmpleados");
                $(span).removeClass("text-warning");
                span.text('');
            }
        }
        ChildTable.row.add(
            {
                Empleados: ver,
                emp_Id: Empleados
            }
        ).draw();
        //$("#FormEmpleados").find("#Razon").val("");
        $("#FormEmpleados").find("#ddlEmpleados").focus();
    }
}
function getJson() {
    list = new Array();
    for (var i = 0; i < ChildTable.data().length; i++) {
        var fila = ChildTable.rows().data()[i];
        var tbEmpleados =
        {
            Id: i,
            emp_Id: fila.emp_Id
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
function llenarDropDowlistTipoPermisos() {
    _ajax(null,
       '/HistorialPermisos/llenarDropDowlistTipoPermisos',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#" + id).append(new Option(value.Descripcion, value.Id));
               });
           });
       });
}
function llenarDropDowlistEmpleados() {
    _ajax(null,
        '/HistorialPermisos/llenarDropDowlistEmpleados',
        'POST',
        function (result) {
            $.each(result, function (id, Lista) {
                Lista.forEach(function (value, index) {
                    $("#ddl" + id).append(new Option(value.Descripcion, value.Id));
                });
            });
        });
}
$("#add").click(function () {
    var Id = $("#FormEmpleados").find("#ddlEmpleados").val();
    if (Id == 0) {
        var span = $("#FormEmpleados").find("#errorEmpleados");
        $(span).addClass("text-warning");
        $(span).closest("div").addClass("has-warning");
        span.text('Seleccione otra opción');
        $("#FormEmpleados").find("#ddlEmpleados").focus();
    }
    else {
        var Id = $("#FormEmpleados").find("#ddlEmpleados").val();
        var ver = $('#ddlEmpleados option:selected').html();
        var valores = Id + ver;
        for (var i = 0; i < valores.length; i++) {
            if (valores[i] == ">" || valores[i] == "<") {
                MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.");
                return null;
            }
        }
        Add(Id, ver);
        $("#FormEmpleados").validate();
    }
});
$("#FormCreate").submit(function (e) {
    e.preventDefault();
});

$("#hper_PorcentajeIndemnizado").focusout(function () {
    if ($("#hper_PorcentajeIndemnizado").val() > 100) {
        $("#hper_PorcentajeIndemnizado").val(100);
    }
});

$("#btnCrear").click(function () {
    if ($("#hper_fechaFin").val() <= $("#hper_fechaInicio").val()) {
        MsgError("Error", "Seleccione una fecha de regreso distinta.");
    } else if ($("#TipoPermisos").val() == 0) {
        MsgError("Error", "Es nesesario seleccionar el tipo de permiso.");
    } else if ($("#hper_PorcentajeIndemnizado").val() == "") {
        MsgError("Error", "Es nesesario especificar el porcentaje del sueldo del cual el colaborador gozará durante la duración del permiso.");
    } else {
        var tbHistorialPermisos =
        {
            tper_Id: $("#TipoPermisos").val(),
            hper_Observacion: $("#hper_Observacion").val(),
            hper_PorcentajeIndemnizado: $("#hper_PorcentajeIndemnizado").val(),
            hper_fechaInicio: $("#hper_fechaInicio").val(),
            hper_fechaFin: $("#hper_fechaFin").val(),
            hper_Justificado: $("#hper_Justificado").val()
        };
        var lista = getJson();
        if (lista == "") {
            MsgError("Error", "Es nesesario seleccionar al menos 1 colaborador.");
        }
        else if ($("#hper_fechaInicio").val() == "") {
            MsgError("Error", "Es nesesario seleccionar la fecha de salida.");
        } else if ($("#hper_fechaFin").val() == "") {
            MsgError("Error", "Es nesesario seleccionar la fecha de regreso.");
        }
        else {
            if (tbHistorialPermisos != null) {
                data = JSON.stringify({
                    tbHistorialPermisos: tbHistorialPermisos,
                    tbEmpleados: lista
                });
                _ajax(data,
                    '/HistorialPermisos/Create',
                    'POST',
                    function (obj) {
                        if (obj != "-1" && obj != "-2" && obj != "-3") {
                            MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                            setTimeout(function () { location.href = "/HistorialPermisos/Index"; }, 5000);
                            $("#btnCrear").attr("disabled", "disabled");
                        } else {
                            MsgError("Error","No se agregó el registro, contacte al administrador.");
                        }
                    });
            } else {
                MsgError("Error", "Por favor llene todas las cajas de texto.");
            }
        }
    }
});
$("#FormEmpleados").find("#ddlEmpleados").keypress(function (envet) {
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