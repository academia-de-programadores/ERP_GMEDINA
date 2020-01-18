$(document).ready(function () {


    $("#Candidato").val(sessionStorage.getItem("per_Descripcion"));
    llenarDropDownList()
});

function llenarDropDownList() {



    _ajax(null,
   '/HistorialCargos/llenarDropDowlistRequisicion',
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

    var tbSueldos =
        {
            sue_Cantidad: $("#sue_Cantidad").val(),
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
        tbEmpleados.area_Id != "" && tbEmpleados.emp_Fechaingreso != "" &&
        tbSueldos.sue_Cantidad != "" && tbSueldos.sue_Cantidad >= 0 && tbRequisiciones.req_Id != null) {
        data = JSON.stringify({
            tbEmpleados: tbEmpleados,
            tbSueldos: tbSueldos,
            tbRequisiciones: tbRequisiciones
        });
        _ajax(data,
            '/HistorialCargos/PromoverGuardar',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    MsgSuccess("¡Exito!", "Se ah contratado el candidato");
                    sessionStorage.clear();
                    $(location).attr('href', "/HistorialCargos/Index");

                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});

