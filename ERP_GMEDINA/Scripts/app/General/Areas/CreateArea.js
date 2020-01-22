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
            $("#FormDepartamentos").find("#depto_Descripcion").val("");
            $("#FormDepartamentos").find("#car_Descripcion").val("");
            $("#FormDepartamentos").find("#depto_Descripcion").focus();
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
               Lista.forEach(function (value,index) {
                   $("#" + id).append(new Option(value.Descripcion, value.Id));
               });
           });
       });
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
    var depto_Descripcion=$("#FormDepartamentos").find("#depto_Descripcion").data("val-maxlength-max");
    var car_Descripcion=$("#FormDepartamentos").find("#car_Descripcion").data("val-maxlength-max");
    var Descripcion = $("#FormDepartamentos").find("#depto_Descripcion").val();
    var Cargo = $("#FormDepartamentos").find("#car_Descripcion").val();
    if (Descripcion.length > depto_Descripcion || Cargo.length > car_Descripcion) {
        MsgError("Error", "una caja de texto tiene muchos caracteres");
        return null;
    }
    var valores=Descripcion + Cargo;
    for (var i = 0; i < valores.length; i++) {
        if (valores[i] == ">" || valores[i] == "<") {
            MsgError("Error", "La cadena de entrada contiene caracteres no permitidos.('>' ó '<')");
            return null;
        }
    }
    Add(Descripcion, Cargo);
});
$("#FormCreate").submit(function (e) {
    e.preventDefault();
});
$("#btnCrear").click(function () {
    //declaramos el objeto principal de nuestra tabla y asignamos sus valores
    var tbAreas = $("#FormNuevo").serializeArray();
    tbAreas = serializar(tbAreas);
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
                  MsgSuccess("Exito","El registro se agregó de forma exitosa");
                  ChildTable.clear().draw();
                  $("#FormAreas").find("#suc_Id option[value='0']").attr("selected", true);
                  $("#FormAreas").find("#area_Descripcion").val("");
                  $("#FormAreas").find("#car_Descripcion").val("");
              } else {
                  MsgError("Error", "No se logro guardar el registro, contacte al administrador");
              }
             });
        } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
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
    limpiarSpan(id,form);
});
function limpiarSpan(id,form) {
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
