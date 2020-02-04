$(document).ready(function () {


    $("#Candidato").val(sessionStorage.getItem("per_Descripcion"));
    llenarDropDownList()
});

function LLenarDepto(sel) {
    debugger
    var select = document.getElementById("depto_Id");
    var i;
    for (i = select.options.length - 1 ; i >= 0 ; i--) {
        select.remove(i);
    }

    _ajax(null,
       '/HistorialCargos/llenarDropDowlistDepartamentos/' + sel.value,
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               var x = document.getElementById("depto_Id");
               var option = document.createElement("option");
               option.text = "**Seleccione una opción**"
               option.value = "";
               x.add(option);
               Lista.forEach(function (value, index) {
                   var x = document.getElementById("depto_Id");
                   var option = document.createElement("option");
                   option.text = value.Descripcion;
                   option.value = value.Id;
                   x.add(option);
               });
           });
       });
}

function llenarDropDownList() {
    _ajax(null,
   '/HistorialCargos/llenarDropDowlistRequisicion',
   'POST',
   function (result) {
       $.each(result, function (id, Lista) {
           var x = document.getElementById("req_Id");
           var option = document.createElement("option");
           option.text = "**Seleccione una opción**"
           option.value = "";
           x.add(option);
           Lista.forEach(function (value, index) {
               var x = document.getElementById("req_Id");
               var option = document.createElement("option");
               option.text = value.Descripcion;
               option.value = value.Id;
               x.add(option);
           });
       });
   });
}



$("#btnGuardar").click(function () {
    //declaramos el objeto principal de nuestra tabla y asignamos sus valores
    
    try
    {

    var tbEmpleados =
        {
            emp_Id:     document.getElementById("emp_Id").value,
            car_Id:     document.getElementById("car_Id").value,
            area_Id:    document.getElementById("area_Id").value,
            depto_Id:   document.getElementById("depto_Id").value,
            jor_Id:     document.getElementById("jor_Id").value,
            emp_CuentaBancaria: $("#emp_CuentaBancaria").val(),
            emp_Fechaingreso: $("#emp_Fechaingreso").val(),
        };


    var sue_Cantidad = $("#sue_Cantidad").val();
    var hcar_RazonPromocion = $("#hcar_RazonPromocion").val();
    var tbRequisiciones =
        {
            req_Id: $("#req_Id").val(),

        };
    }
    catch(Exception)
    {

    }
    var data = $("#FormNuevo").serializeArray();

    data = serializar(data);
    if (data != null) {
        if(sue_Cantidad >= 0)
        {
            if ($("#emp_Fechaingreso").val() > '01/01/1900') {
        data = JSON.stringify({
            tbEmpleados: tbEmpleados,
            sue_Cantidad: sue_Cantidad,
            hcar_RazonPromocion: hcar_RazonPromocion,
            tbRequisiciones: tbRequisiciones
        });
        _ajax(data,
            '/HistorialCargos/PromoverGuardar',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Éxito!", "Promoción exitosa.");
                    sessionStorage.clear();
                    $(location).attr('href', "/HistorialCargos/Index");

                } else {
                    MsgError("Error", "No se promovió el registro, contacte con el administrador.");
                }
            });
        }
        else
        {
            MsgError("Error", "Fecha no válida.");
        }
        }         
        else {
            MsgError("Error", "Sueldo no puede ser negativo.");
        }
    }
    else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});


function filterFloat(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
function filter(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}


