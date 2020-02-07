﻿var fill = 0;
var id = 0;
var Admin = false;

//Funciones GET
function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/Areas/Edit/" + id);
}
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/Areas/Edit/" + id);
}
function format(obj) {
    var div = '<div class="ibox-title">'
+ '<h5>Datos del perfil</h5>'
+ '</div>';

    div += '<div class="ibox-content">'
   + '<div class="panel-body">'
     + '<div class="panel-group" id="accordion">'
       + '<div class="panel panel-default">'
         + '<div class="panel-heading">'
           + '<h5 class="panel-title">'
             + '<a data-toggle="collapse" data-parent="#accordion" href="#collapseCompetencias' + obj.req_Id + '" class="" aria-expanded="true">Competencias</a>'
           + '</h5>'
         + '</div>'
         + '<div id="collapseCompetencias' + obj.req_Id + '" class="panel-collapse in collapse" style="">'
           + '<div class="panel-body">';

    if (obj.Competencias.length > 0)
        obj.Competencias.forEach(function (index, value) {
            div = div + index.comp_Descripcion + '<br>';
        })
    else
        div += "<h5>Sin datos que mostrar.*</h5>";

    div += '</div>'
    + '</div>'
   + '</div>'
   + '<div class="panel panel-default">'
    + '<div class="panel-heading">'
      + '<h5 class="panel-title">'
        + '<a data-toggle="collapse" data-parent="#accordion" href="#collapseHabilidades' + obj.req_Id + '" class="" aria-expanded="true">Habilidades</a>'
      + '</h5>'
    + '</div>'
    + '<div id="collapseHabilidades' + obj.req_Id + '" class="panel-collapse in collapse" style="">'
      + '<div class="panel-body">';

    if (obj.Habilidades.length > 0)
        obj.Habilidades.forEach(function (index, value) {
            div = div + index.habi_Descripcion + '<br>';
        })
    else
        div += "<h5>Sin datos que mostrar.*</h5>";

    div += '</div>'
     + '</div>'
   + '</div>'
   + '<div class="panel panel-default">'
     + '<div class="panel-heading">'
       + '<h4 class="panel-title">'
         + '<a data-toggle="collapse" data-parent="#accordion" href="#collapseIdiomas' + obj.req_Id + '" class="" aria-expanded="true">Idiomas</a>'
       + '</h4>'
     + '</div>'
     + '<div id="collapseIdiomas' + obj.req_Id + '" class="panel-collapse collapse in">'
       + '<div class="panel-body">';

    if (obj.Idiomas.length > 0)
        obj.Idiomas.forEach(function (index, value) {
            div = div + index.idi_Descripcion + '<br>';
        })
    else
        div += "<h5>Sin datos que mostrar.*</h5>";

    div += '</div>'
  + '</div>'
+ '</div>'
+ '<div class="panel panel-default">'
  + '<div class="panel-heading">'
    + '<h4 class="panel-title">'
      + '<a data-toggle="collapse" data-parent="#accordion" href="#collapseRequisitos' + obj.req_Id + '"  class="" aria-expanded="true">Requisitos especiales</a>'
    + '</h4>'
  + '</div>'
  + '<div id="collapseRequisitos' + obj.req_Id + '" class="panel-collapse collapse in">'
    + '<div class="panel-body">';

    if (obj.ReqEspeciales.length > 0)
        obj.ReqEspeciales.forEach(function (index, value) {
            div = div + index.resp_Descripcion + '<br>';
        })
    else
        div += "<h5>Sin datos que mostrar.*</h5>";

    div += '</div>'
  + '</div>'
+ '</div>'
+ '<div class="panel panel-default">'
  + '<div class="panel-heading">'
    + '<h5 class="panel-title">'
      + '<a data-toggle="collapse" data-parent="#accordion" href="#collapseTitulos' + obj.req_Id + '" class="" aria-expanded="true">Titulos</a>'
    + '</h5>'
  + '</div>'
  + '<div id="collapseTitulos' + obj.req_Id + '" class="panel-collapse in collapse" style="">'
    + '<div class="panel-body">';

    if (obj.Titulos.length > 0)
        obj.Titulos.forEach(function (index, value) {
            div = div + index.titu_Descripcion + '<br>';
        })
    else
        div += "<h5>Sin datos que mostrar.*</h5>";

    div += '</div>'
        + '</div>'
      + '</div>'
    + '</div>'
  + '</div>'
+ '</div>';


    ///
    return div;
}

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);

    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().ID;
        hola = row.data().hola;
        tr.addClass('loading');
        _ajax({ id: parseInt(id) },
            '/Requisiciones/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.removeClass('loading');
                    tr.addClass('shown');
                }
            });
    }

});


function tablaDetalles(ID) {

    var validacionPermiso = userModelState("Requisiciones/Detalles");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/Requisiciones/Detalles/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetalles").find("#req_Experiencia")["0"].innerText = obj[0].req_Experiencia;
                    $("#ModalDetalles").find("#req_Sexo")["0"].innerText = obj[0].req_Sexo == "N" ? "Indiferente" : obj[0].req_Sexo == "M" ? "Masulino" : "Femenino";
                    $("#ModalDetalles").find("#req_Descripcion")["0"].innerText = obj[0].req_Descripcion;
                    $("#ModalDetalles").find("#req_EdadMinima")["0"].innerText = obj[0].req_EdadMinima;
                    $("#ModalDetalles").find("#req_EdadMaxima")["0"].innerText = obj[0].req_EdadMaxima;
                    $("#ModalDetalles").find("#req_EstadoCivil")["0"].innerText = obj[0].req_EstadoCivil == "N" ? "Indiferente" : obj[0].req_Sexo == "C" ? "Casado(a)" : "Soltero(a)";
                    $("#ModalDetalles").find("#req_EducacionSuperior")["0"].innerText = obj[0].req_EducacionSuperior == "true" ? "Si" : "No";
                    $("#ModalDetalles").find("#req_Permanente")["0"].innerText = obj[0].req_Permanente == "true" ? "Si" : "No";
                    $("#ModalDetalles").find("#req_Duracion")["0"].innerText = obj[0].req_Duracion == null ? "N/A" : obj[0].req_Duracion;
                    $("#ModalDetalles").find("#req_Vacantes")["0"].innerText = obj[0].req_Vacantes;
                    $("#ModalDetalles").find("#req_FechaRequisicion")["0"].innerText = FechaFormatoSimpleAlt(obj[0].req_FechaRequisicion);
                    $("#ModalDetalles").find("#req_FechaContratacion")["0"].innerText = FechaFormatoSimpleAlt(obj[0].req_FechaContratacion);
                    $("#ModalDetalles").find("#req_FechaCrea")["0"].innerText = FechaFormatoSimpleAlt(obj[0].req_FechaCrea);
                    $("#ModalDetalles").find("#req_FechaModifica")["0"].innerText = FechaFormatoSimpleAlt(obj[0].req_FechaModifica);
                    $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj[0].req_UsuarioCrea;
                    $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj[0].req_UsuarioModifica;

                    $('#ModalDetalles').modal('show');
                }
            });
    }
}


function llenarTabla() {
    _ajax(null,
        '/Requisiciones/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {

                var Acciones = value.req_Estado == 1
                    ? null :
                    "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.req_Estado > fill) {

                    tabla.row.add({
                        ID: value.req_Id,
                        "Número": value.req_Id,
                        Experiencia: value.req_Experiencia,
                        Sexo: value.req_Sexo,
                        "Descripción": value.req_Descripcion,
                        EdadMinima: value.req_EdadMinima,
                        EdadMaxima: value.req_EdadMaxima,
                        EstadoCivil: value.req_EstadoCivil,
                        EducacionSuperior: value.req_EducacionSuperior ? "Si" : "No",
                        Temporal: value.req_Permanente ? "Si" : "No",
                        "Duración": value.req_Duracion == null ? "N/A" : value.req_Duracion,
                        Vacantes: value.req_Vacantes,
                        FechaRequisicion: FechaFormatoSimpleAlt(value.req_FechaRequisicion),
                        "FechaContratación" : FechaFormatoSimpleAlt(value.req_FechaContratacion),
                        Acciones: Acciones,
                        Estado: value.req_Estado ? "Activo" : "Inactivo"
                    });
                }
                tabla.draw();
            });
        })
};

$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

$("#btnInactivar").click(function () {
    var validacionPermiso = userModelState("Requisiciones/Delete");
    if (validacionPermiso.status == true) {
        CierraPopups();
        $('#ModalInactivar').modal('show');
        $("#ModalInactivar").find("#req_RazonInactivo").val("");
        $("#ModalInactivar").find("#req_RazonInactivo").focus();
    }
});

//botones POST
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.req_Id = id;
        data = JSON.stringify({ Requisicion: data });
        _ajax(data,
            '/Requisiciones/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                     MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    LimpiarControles(["req_Experiencia", "req_Sexo", "req_Descripcion", "req_EdadMinima", "req_EdadMaxima", "req_EstadoCivil", "req_EducacionSuperior", "req_Permanente", "req_Duracion", "req_Vacantes", "req_FechaRequisicion", "req_FechaContratacion", "req__RazonInactivo"]);
                    llenarTabla();
                } else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
    $("#req_RazonInactivo").val = "";
});
$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.req__Id = id;
        data = JSON.stringify({ tbRequisiciones: data });
        _ajax(data,
            '/Requisiciones/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                    llenarTabla();
                } else {
                    MsgError("Error", "No se editó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});

function tablaEditar(ID) {
    id = ID;
    var validacionPermiso = userModelState("Requisiciones/Edit");
    if (validacionPermiso.status == true) {
        sessionStorage.setItem("IdRequisicion", id);
        window.location.href = "/Requisiciones/Edit/" + id;
    }
};
