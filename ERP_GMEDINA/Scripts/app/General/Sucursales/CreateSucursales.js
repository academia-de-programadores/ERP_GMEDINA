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
                        MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa.");
                        setTimeout(function () { window.location.href = "/Sucursales/Index"; }, 3000);
                    } else {
                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                    }
                });
        } else {
            MsgError("Error", "por favor llene todas las cajas de texto.");
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