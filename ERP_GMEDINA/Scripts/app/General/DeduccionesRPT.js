function dtDeducciones()
{
    var table = $("#tblDeduccionesRPT").DataTable({
        destroy: true,
        responsive: true,
        ajax: {
            method: "POST",
            url: "ReportesPlanilla/getDeducciones",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (d) {

                return JSON.stringify(d);
            },
            dataSrc: "d.data"
        },
        columns: [
            {"data" : "id"}
        ]

    });

};

$("#boton").on("click"), function () {
    dtDeducciones();
};