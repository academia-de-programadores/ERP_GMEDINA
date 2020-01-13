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
            "Jornadas"
            
        ];
    var Empleado =[
                "Empleados",
                "Sueldos",
                "HistorialHorasTrabajadas",
                "HistorialCargos"
            ]
    var ACCIONES = {
        Vacaciones:[
                "HistorialVacaciones"
        ],
        Amonestaciones: [
                "TipoAmonestaciones",
                "HistorialAudienciaDescargos",
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
                "HistorialIncapacidades"
        ],
        Reclutamiento: [
                "Competencias",
                "Habilidades",
                "Titulos",
                "FasesReclutamiento",
                "Personas",
                "Requisiciones",
                "SeleccionCandidatos",
                "RequerimientosEspeciales"
        ]
    };
    var url = window.location.href;
    var pieces = url.split("/");
    var controler = ''; //pieces[pieces.length - 1];
    pieces.forEach(function (value, index) {
        controler = value;
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