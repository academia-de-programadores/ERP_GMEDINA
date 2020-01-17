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
    debugger
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

    var tbSueldos =
        {
            sue_Cantidad: $("#sue_Cantidad").val(),
            tmon_Id: document.getElementById("tmon_Id").value,
        };
    var tbRequisiciones =
        {
            req_Id: $("#req_Id").val(),

        };
    }
    catch(Exception)
    {

    }
    if (tbEmpleados.car_Id != null && tbEmpleados.area_Id != null && tbEmpleados.depto_Id != null && tbEmpleados.jor_Id != null &&
        tbEmpleados.cpla_IdPlanilla != null && tbEmpleados.fpa_IdFormaPago != null && emp_CuentaBancaria.area_Id != "" && tbEmpleados.emp_Fechaingreso != "" &&
        tbEmpleados.emp_CuentaBancaria != "" && tbSueldos.tmon_Id != null && tbSueldos.suc_Cantidad != "" && tbRequisiciones.req_Id != null) {
        data = JSON.stringify({
            tbSeleccionCandidatos: tbSeleccionCandidatos,
            tbEmpleados: tbEmpleados,
            tbSueldos: tbSueldos,
            tbRequisiciones: tbRequisiciones
        });
        _ajax(data,
            '/SeleccionCandidatos/Contratar',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Exito!", "Se ah contratado el candidato");
                    sessionStorage.clear();
                    $(location).attr('href', "/SeleccionCandidatos/Index");

                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});

