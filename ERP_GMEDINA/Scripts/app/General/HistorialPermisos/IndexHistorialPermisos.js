var id = 0;
var fill = 0;

$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});
//Funciones GET
function tablaEditar(ID) {
    var validacionPermiso = userModelState("HistorialPermisos/Edit");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/HistorialPermisos/Edit/' + ID,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    // $("#FormEditar").find("#tiho_Id").val(obj.habi_Descripcion);
                    //$("#FormEditar").find("#hper_Observacion").val(obj.hper_Observacion);
                    $('#ModalInactivar').modal('show');
                    $("#ModalInactivar").find("#hper_RazonInactivo").val("");
                    $("#ModalInactivar").find("#hper_RazonInactivo").focus();
                }
            });
    }
}

function tablaDetalles(ID) {
    var validacionPermiso = userModelState("HistorialPermisos/details");
    if (validacionPermiso.status == true) {
        id = ID;
        _ajax(null,
            '/HistorialPermisos/Edit/' + ID,
            'GET',
            function (obj) {
                var o = obj.hper_Observacion == null ? "Ninguna" : obj.hper_Observacion;
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalDetallesAX").find("#hper_Observacion")["0"].innerText = o;
                    $("#ModalDetallesAX").find("#hper_FechaCrea")["0"].innerText = FechaFormato(obj.hper_FechaCrea);
                    $("#ModalDetallesAX").find("#hper_fechaInicio")["0"].innerText = FechaFormato(obj.hper_fechaInicio).substring(0, 10);
                    $("#ModalDetallesAX").find("#hper_fechaFin")["0"].innerText = FechaFormato(obj.hper_fechaFin).substring(0, 10);
                    $("#ModalDetallesAX").find("#hper_Duracion")["0"].innerText = obj.hper_Duracion;
                    $("#ModalDetallesAX").find("#hper_PorcentajeIndemnizado")["0"].innerText = obj.hper_PorcentajeIndemnizado + "%";



                    //$("#ModalDetallesAX").find("#hper_FechaModifica")["0"].innerText = FechaFormato(obj.hper_FechaModifica);
                    $("#ModalDetallesAX").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                    //$("#ModalDetallesAX").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                    //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                    $('#ModalDetalles').modal('show');
                }
            });
    }
}


function llenarTabla() {
    _ajax(null,
        '/HistorialPermisos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.hper_Estado == 1
                ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hper_Id + ")'>Detalles</a><a class='btn btn-danger btn-xs ' onclick='tablaEditar(" + value.hper_Id + ")'>Inactivar</a>"
                   : Admin ?
                       "<div>" +
                       "<a class='btn-primary btn-xs' onclick='tablaDetalles(" + value.hper_Id + ")' >Detalles</a>" +
                       "<a class='btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
                tabla.row.add({
                    Id: value.hper_Id,
                    "Número": value.hper_Id,
                    tper_Id: value.tper_Id,
                    TipoPermiso: value.tper_Descripcion,
                    NombreCompleto: value.per_Nombres,
                    per_CorreoElectronico: value.per_CorreoElectronico,
                    per_Telefono: value.per_Telefono,
                    per_Direccion: value.per_Direccion,
                    per_Edad: value.per_Edad,
                    per_EstadoCivil: value.per_EstadoCivil,
                    hper_Observacion: value.hper_Observacion,
                    FechaInicio: value.FechaInicio,
                    Estado: value.hper_Estado ? 'Activo' : 'Inactivo',
                    Justificado: value.hper_Justificado ? 'Si' : 'No',
                    Acciones: Acciones
                    //Accion: "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hper_Id + ")'>Detalles</a><a class='btn btn-danger btn-xs ' onclick='tablaEditar(" + value.hper_Id + ")'>Inactivar</a>"
                });
            });
            tabla.draw();
        });
}




$("#btnEditar").click(function () {
    var validacionPermiso = userModelState("Areas/details");
    if (validacionPermiso.status == true) {
        _ajax(null,
            '/HistorialPermisos/Edit/' + id,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    $('#ModalEditar').modal('show');
                    $("#FormEditar").find("#hper_Observacion").val(obj.hper_Observacion);
                }
            });
    }
});


$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#hper_RazonInactivo").val("");
    $("#ModalInactivar").find("#hper_RazonInactivo").focus();
});
//llamado
$("#InActivar").click(function () {
    var validacionPermiso = userModelState("HistorialPermisos/Delete");
    if (validacionPermiso.status == true) {
        var data = $("#FormInactivar").serializeArray();
        data = serializar(data);
        if (data != null) {
            //data.tiho_Id = id;
            // data = JSON.stringify({ tbTipoHoras: data });
            $.post("/HistorialPermisos/Delete", data).done(function (obj) {
                if (obj != "-1") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["hper_Observacion", "hper_RazonInactivo"]);
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                } else {
                    MsgError("Error", "No se inactivó el registro, contacte al administrador.");
                }
            });
        } else {
            MsgError(" ", "La eliminación de información debe ser justificada.");
        }
    }
});

$("#btnActualizar").click(function () {
          var data = $("#FormEditar").serializeArray();
          //data = serializar(data);
          if (data != null) {
              data.tper_Id = id;
              //data = JSON.stringify({ tbTipoHoras: data });
              $.post("/HistorialPermisos/Edit", data).done(function (obj) {
                  if (obj != "-1" && obj != "-2" && obj != "-3") {
                      CierraPopups();
                      llenarTabla();
                      LimpiarControles(["hper_Observacion"]);
                      MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                  } else {
                      MsgError("Error", "No se editó el registro, contacte al administrador.");
                  }
              });
          }
});
//aqui estaba
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Información personal y de contacto: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        index.per_EstadoCivil.toUpperCase() == ('S') ? EstadoCivil = 'Soltero(a)'
    : index.per_EstadoCivil.toUpperCase() == ('C') ? EstadoCivil = 'Casado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('D') ? EstadoCivil = 'Divorciado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('V') ? EstadoCivil = 'Viudo'
    : 'Union Libre';
        div = div
                    + '<div class="col-md-5"><b>Número: </b>' + index.emp_Id + '</div>'

        + '<div class="col-md-5"><b>Número de identidad: </b>' + index.per_Identidad + '</div>'

        + '<div class="col-md-5"><B>Correo electrónico: </b>' + index.per_CorreoElectronico + '</div>'
        //+ '<div class="col-md-5"><b>Edad: </b>' + index.per_Edad + '</div>'
        //+ '<div class="col-md-5"><b>Dirección: </b>' + index.per_Direccion + '</div>'
        + '<div class="col-md-5"><b>Estado civil: </b>' + EstadoCivil + '</div>'
        + '<div class="col-md-5"><b>Teléfono: </b>' + index.per_Telefono + '</div>'
        + '</div>' +
        '</div>' +
        '</div>'
    });
    return div + '</div></div></div>';
}

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/HistorialPermisos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});
