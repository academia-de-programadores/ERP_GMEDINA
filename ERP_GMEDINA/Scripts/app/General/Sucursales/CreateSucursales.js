$("#btnGuardar").click(function () {
    var data = $("#tbSucursales").find("select, textarea, input").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbSucursales: data });
        _ajax(data,
            '/Sucursales/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#btnGuardar").attr("disabled", "disabled");
                    MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                    setTimeout(function () { window.location.href = "/Sucursales/Index"; }, 3000);
                } else {
                    MsgError("Error", "No se guardó el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});