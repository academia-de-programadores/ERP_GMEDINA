﻿$(document).on("ready", function () {
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
            
    var Empleados= [
          "Empleados",
            "Sueldos",
            "HistorialVacaciones",
            "HistorialHorasTrabajadas",
            "HistorialCargos"
    ];

    var Amonestaciones= [
            "TipoAmonestaciones",
            "HistorialAudienciaDescargos",
            "HistorialAmonestaciones"
    ];
    var Salidas= [
            "RazonSalidas",
            "TipoSalidas",
            "HistorialSalidas"
    ];
    var Permisos= [
            "TipoPermisos",
            "HistorialPermisos"
    ];
    var Incapacidades= [
            "TipoIncapacidades",
            "HistorialIncapacidades"
    ];
    var Reclutamiento = [
           
            "Personas",
            "Requisiciones",
            "SeleccionCandidatos"
           
    ];
    var Datos_Profesionales = [
         "Competencias",
            "Habilidades",
            "Titulos",
            "FasesReclutamiento",
             "RequerimientosEspeciales"
    ]
    
    var url = window.location.href;
    var pieces = url.split("/");
    var controler = ''; //pieces[pieces.length - 1];
    pieces.forEach(function (value, index) {
        controler = value;
        if (buscarItem(controler, General)) {
            $("#General").addClass("active");
            $("#General").find(".General").addClass("in");}
        else if (buscarItem(controler, Empleados)) {
            $("#Empleados").addClass("active");
            $("#Empleados").find(".Empleados").addClass("in");
        }
        else if (buscarItem(controler, Amonestaciones)) {
            $("#Amonestaciones").addClass("active");
            $("#Amonestaciones").find(".Amonestaciones").addClass("in");
        }
        else if (buscarItem(controler, Salidas)) {
            $("#Salidas").addClass("active");
            $("#Salidas").find(".Salidas").addClass("in");
        }
        else if (buscarItem(controler, Permisos)) {
            $("#Permisos").addClass("active");
            $("#Permisos").find(".Permisos").addClass("in");
        }
        else if (buscarItem(controler, Incapacidades)) {
            $("#Incapacidades").addClass("active");
            $("#Incapacidades").find(".Incapacidades").addClass("in");
        }
        else if (buscarItem(controler, Reclutamiento)) {
            $("#Reclutamiento").addClass("active");
            $("#Reclutamiento").find(".Reclutamiento").addClass("in");
        }
        else if (buscarItem(controler, Datos_Profesionales)) {
            $("#Datos_Profesionales").addClass("active");
            $("#Datos_Profesionales").find(".Datos_Profesionales").addClass("in");
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