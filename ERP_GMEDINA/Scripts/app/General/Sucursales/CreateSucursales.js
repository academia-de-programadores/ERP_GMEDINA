$("#btnGuardar").click(function () {
    $("#suc_Correo").removeClass("error");
    $("#suc_Correo").closest("div").removeClass("has-error");
    $("#suc_Correo").removeClass("text-danger");
    var Correo = validarEmail($("#suc_Correo").val());
    var data = $("#tbSucursales").find("select, textarea, input").serializeArray();
    data = serializar(data);
    if (Correo != " ")
    {
        if (data != null) {
            data = JSON.stringify({ tbSucursales: data });
            _ajax(data,
                '/Sucursales/Create',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        $("#btnGuardar").attr("disabled", "disabled");
                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.")
                        setTimeout(function () { window.location.href = "/Sucursales/Index"; }, 3000);
                    } else {
                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                    }
                });
        } else {
            MsgError("Error", "No se editó el registro, contacte al administrador.");
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
    ///var select = document.getElementById("dep_Codigo");
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
            if (result != "-1" && result != "-2" && result != "-3") 
                $("#mun_Codigo").append('<option value="0">' + '**Seleccione una opción**' + '</option>');
                $.each(result, function (value, index) {
                    $("#mun_Codigo").append('<option value="' + index.mun_Codigo + '">' + index.mun_Nombre.toString() + '</option>');
                });
       });
};