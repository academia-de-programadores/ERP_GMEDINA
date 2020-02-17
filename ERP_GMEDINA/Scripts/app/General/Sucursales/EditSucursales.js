var municipio ;
function llenarDropDownList() {
    _ajax(null,
       '/Sucursales/llenarDropDowlist',
       'POST',
      function (result) {
          $.each(result, function (id, Lista) {
              Lista.forEach(function (value, index) {
                  $("#empr_Id").append('<option value="' + value.empr_Id + '">' + value.empr_Nombre + '</option>');
              });
          });
      });
}
$(document).ready(function () {
    llenarDropDownList();


    id = sessionStorage.getItem("idSucursal");

    _ajax(null,
            '/Sucursales/Edit/' + id,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#tbSucursales").find("#suc_Id").val(obj[0].suc_Id);
                    $("#tbSucursales").find("#empr_Id").val(obj[0].empr_Id);
                    $("#tbSucursales").find("#bod_id").val(obj[0].bod_Id);
                    $("#tbSucursales").find("#pemi_Id").val(obj[0].pemi_Id);
                    $("#tbSucursales").find("#suc_Descripcion").val(obj[0].suc_Descripcion);
                    $("#tbSucursales").find("#suc_Correo").val(obj[0].suc_Correo);
                    $("#tbSucursales").find("#suc_Direccion").val(obj[0].suc_Direccion);
                    $("#tbSucursales").find("#suc_Telefono").val(obj[0].suc_Telefono);
                    $("#tbSucursales").find("#dep_Codigo").val(obj[0].dep_Codigo);
                    //municipio = obj[0].mun_Codigo;
                    id = obj[0].dep_Codigo;
                    console.log(id);
                    _ajax(null,
                        '/Sucursales/MunicipiosDDl/' + id,
                        'GET',
                        function (result) {
                            if (result != "-1" && result != "-2" && result != "-3") {
                                $.each(result, function (value, index) {
                                    $("#mun_Codigo").append('<option value="' + index.mun_Codigo + '">' + index.mun_Nombre.toString() + '</option>');
                                });
                                $("#tbSucursales").find("#mun_Codigo").val(obj[0].mun_Codigo);
                            }
                        });
                }
            });
    
});

$("#btnGuardar").click(function () {
    var span = $("#suc_Correo").closest("div").find("span");
    $(span).removeClass("error");
    $(span).closest("div").removeClass("has-error");
    $("#suc_Correo").removeClass("text-danger");
    var Correo = validarEmail($("#suc_Correo").val());
    var data = $("#tbSucursales").find("select, textarea, input").serializeArray();
    data = serializar(data);
    if (Correo != " ")
    {
        if (data != null) {
            data = JSON.stringify({ tbSucursales: data });
            _ajax(data,
                '/Sucursales/Edit',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        $("#btnGuardar").attr("disabled", "disabled");
                        MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                        setTimeout(function () { window.location.href = "/Sucursales/Index"; }, 3000);
                    } else {
                        MsgError("Error", "No se editó el registro, contacte al administrador.");
                    }
                });
        } else {
            MMsgError("Error", "Por favor llene todas las cajas de texto.");
        }
    }
    else
    {
        var span = $("#suc_Correo").closest("div").find("span");
        $(span).closest("div").addClass("has-error");
        $("#suc_Correo").focus();
        $("#suc_Correo").data("val-required");
        MsgError("Error", "Correo electrónico inválido.");
        $("#suc_Correo").addClass("text-danger");
    }
});

function LlenaMunicipios(sel) {
    var select = document.getElementById("mun_Codigo");
    var i;
    for (i = select.options.length - 1 ; i >= 0 ; i--) {
        select.remove(i);
    }

    id = sel.value.toString();
    console.log(id);
    _ajax(null,
        '/Sucursales/MunicipiosDDl/' + id,
        'GET',
        function (result) {
            if (result != "-1" && result != "-2" && result != "-3") {
                $("#mun_Codigo").append('<option value="0">' + '**Selecciones una opción**' + '</option>');
                $.each(result, function (value, index) {
                    $("#mun_Codigo").append('<option value="' + index.mun_Codigo + '">' + index.mun_Nombre.toString() + '</option>');
                });
                $("#tbSucursales").find("#mun_Codigo").val(0);
            }
        });
};