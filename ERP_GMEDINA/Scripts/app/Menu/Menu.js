$(document).on("ready", function () {
    var menu =
    {
        General : [
            "Cargos",
            "Empresas",
            "Idiomas",
            "TipoMonedas",
            "Nacionalidades",
            "Jornadas",
            "Areas",
            "TipoHoras",
        ],
        Empleado :[
            "Empleados",
            "Sueldos",
            "HistorialHorasTrabajadas",
            "HistorialCargos"
        ]
    };
    var sub_Menu = {
        "Acciones de Personal" :{
                    Amonestaciones :[
                            "TipoAmonestaciones",
                            "HistorialAmonestaciones"
                    ],
                    Salidas :[
                            "RazonSalidas",
                            "TipoSalidas",
                            "HistorialSalidas"
                    ],
                    Permisos :[
                            "TipoPermisos",
                            "HistorialPermisos"
                    ],
                    Incapacidades :[
                            "TipoIncapacidades",
                            "HistorialPermisos"
                    ],
                    Reclutamiento :[
                            "Competencias",
                            "Habilidades",
                            "Titulos",
                            "FasesReclutamiento",
                            "Personas",
                            "SeleccionCandidatos",
                            "RequerimientosEspeciales"
                    ]
                }
    };
    var url = window.location.href;
    var pieces = url.split("/");
    var controler=pieces[pieces.length-1]
    //pieces = pieces[1].split("/");
    $(".active").removeClass("active");
    $.each(menu, function (index,value) {
        $.each(value, function (indice, valor) {
            if (valor == controler) {
                $("#" + index).addClass("active");
                $("#" + index).find("."+index).addClass("in");
            }
        });
    });
    $.each(sub_Menu, function (index, value) {
        $.each(value, function (indice, valor) {
            if (buscarItem(controler,valor)) {
                $.each(valor, function (indice1, valor1) {
                    if (valor1 == controler) {
                        $("#ACCIONES").addClass("active");
                        $("#ACCIONES").find(".ACCIONES").addClass("in");

                        $("#" + indice).addClass("active");
                        $("#" + indice).find("." + indice).addClass("in");
                        console.log(index);
                        console.log(indice);
                        console.log(controler);
                    }
                });
            }
        });
    });
});
function buscarItem(item, lista) {
    lista.foreach(function (indice,value) {
        if(valor==value){
            return true;
        }
        return false;
    });
}