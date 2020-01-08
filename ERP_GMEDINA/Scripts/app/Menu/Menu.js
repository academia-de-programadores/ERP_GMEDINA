$(document).on("ready", function () {
    var General = [
            "Cargos",
            "Empresas",
            "Idiomas",
            "TipoMonedas",
            "Nacionalidades",
            "Jornadas",
            "Areas",
            "TipoHoras",
        ];
    var Empleado =[
                "Empleados",
                "Sueldos",
                "HistorialHorasTrabajadas",
                "HistorialCargos"
            ]
    var ACCIONES = {
        Amonestaciones: [
                "TipoAmonestaciones",
                "HistorialAmonestaciones"
        ],
        Salidas: [
                "RazonSalidas",
                "TipoSalidas",
                "HistorialSalidas"
        ],
        Permisos: [
                "TipoPermisos",
                "HistorialPermisos"
        ],
        Incapacidades: [
                "TipoIncapacidades",
                "HistorialPermisos"
        ],
        Reclutamiento: [
                "Competencias",
                "Habilidades",
                "Titulos",
                "FasesReclutamiento",
                "Personas",
                "SeleccionCandidatos",
                "RequerimientosEspeciales"
        ]
    };
    var url = window.location.href;
    var pieces = url.split("/");
    var controler=pieces[pieces.length-1]    
    //$.each(menu, function (index,value) {
    //    $.each(value, function (indice, valor) {
    //        if (valor == controler) {
    //            $("#" + index).addClass("active");
    //            $("#" + index).find("."+index).addClass("in");
    //        }
    //    });
    //});
    //$.each(sub_Menu, function (index, value) {
    //    $.each(value, function (indice, valor) {
    //        if (buscarItem(controler,valor)) {
    //            $.each(valor, function (indice1, valor1) {
    //                if (valor1 == controler) {
    //                    $("#"+index).addClass("active");
    //                    $("#" + index).find("." + index).addClass("in");
    //                    $("#" + indice).addClass("active");
    //                    $("#" + indice).find("." + indice).addClass("in");
    //                    console.log(index);
    //                    console.log(indice);
    //                    console.log(controler);
    //                }
    //            });
    //        }
    //    });
    //});

    if (buscarItem(controler, General)) {
        $("#General").addClass("active");
        $("#General").find(".General").addClass("in");
    } else if (buscarItem(controler, Empleado)) {
        $("#General").addClass("active");
        $("#General").find(".General").addClass("in");
        $("#Empleado").addClass("active");
        $("#Empleado").find(".Empleado").addClass("in");
    } else {
        $.each(ACCIONES, function (indice, valor) {
            if (buscarItem(controler, valor)) {
                $("#ACCIONES").addClass("active");
                $("#ACCIONES").find(".ACCIONES").addClass("in");

                //$("#" + index).addClass("active");
                //$("#" + index).find("." + index).addClass("in");

                $("#" + indice).addClass("active");
                $("#" + indice).find("." + indice).addClass("in");
            }
        });
    }

});
function buscarItem(item, lista) {
    var valor = false;
    $.each(lista, function (index, value) {
        if (item == value) {
            valor= true;
        }
    });
    return valor;
}