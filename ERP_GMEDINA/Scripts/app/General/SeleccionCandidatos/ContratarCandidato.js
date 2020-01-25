$(document).ready(function () {


    $("#Candidato").val(sessionStorage.getItem("per_Descripcion"));
    llenarDropDownList()
});

function llenarDropDownList() {


    _ajax(null,
       '/SeleccionCandidatos/llenarDropDowlistTipoMonedas',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               var x = document.getElementById("tmon_Id");
               var option = document.createElement("option");
               option.text ="**Seleccione una opción**"
               x.add(option);
               Lista.forEach(function (value, index) {
                   var x = document.getElementById("tmon_Id");
                   var option = document.createElement("option");
                   option.text = value.Descripcion;
                   option.value = value.Id;
                   x.add(option);
               });
           });
       });

    _ajax(null,
   '/SeleccionCandidatos/llenarDropDowlistRequisicion',
   'POST',
   function (result) {
       var x = document.getElementById("req_Id");
       var option = document.createElement("option");
       option.text ="**Seleccione una opción**"
       x.add(option);
       $.each(result, function (id, Lista) {
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

    
        var tbSeleccionCandidatos =
    {
        scan_Id: sessionStorage.getItem("scan_Id"),
    };

    var tbEmpleados =
        {
            per_id:     sessionStorage.getItem("per_Id"),
            car_Id:     document.getElementById("car_Id").value,
            area_Id:    document.getElementById("area_Id").value,
            depto_Id:   document.getElementById("depto_Id").value,
            jor_Id:     document.getElementById("jor_Id").value,
            cpla_IdPlanilla: document.getElementById("cpla_IdPlanilla").value,
            fpa_IdFormaPago: document.getElementById("fpa_IdFormaPago").value,
            emp_CuentaBancaria: $("#emp_CuentaBancaria").val(),
            emp_Fechaingreso: $("#emp_Fechaingreso").val(),
        };
    var sue_Cantidad = $("#sue_Cantidad").val();
    var tmon_Id = document.getElementById("tmon_Id").value;

    var tbRequisiciones =
        {
            req_Id: $("#req_Id").val(),

        };
    }
    catch(Exception)
    {
        
    }
    if (tbEmpleados.car_Id != null && tbEmpleados.area_Id != null && tbEmpleados.depto_Id != null && tbEmpleados.jor_Id != null &&
        tbEmpleados.cpla_IdPlanilla != null && tbEmpleados.fpa_IdFormaPago != null && tbEmpleados.emp_Fechaingreso != "" &&
        tbEmpleados.emp_CuentaBancaria != "" && tmon_Id != null && sue_Cantidad != "" && tbRequisiciones.req_Id != null)
    {
        if(sue_Cantidad >= 0)
        {

        
        data = JSON.stringify({
            tbSeleccionCandidatos: tbSeleccionCandidatos,
            tbEmpleados: tbEmpleados,
            sue_Cantidad: sue_Cantidad,
            tmon_Id : tmon_Id,
            tbRequisiciones: tbRequisiciones
        });
        _ajax(data,
            '/SeleccionCandidatos/Contratar',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                    sessionStorage.clear();
                    $(location).attr('href', "/SeleccionCandidatos/Index");

                } else {
                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                }
            });
        }
        else
        {
        MsgError("Error", "Sueldo no puede ser negativo");
        }
    }
    else 
    {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});

