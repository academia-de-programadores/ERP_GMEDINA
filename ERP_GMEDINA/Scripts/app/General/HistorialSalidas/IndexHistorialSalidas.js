var id = 0;
var fill = 0;
//var Admin = false;

$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/HistorialSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                // $("#FormEditar").find("#tiho_Id").val(obj.habi_Descripcion);
                $("#FormEditar").find("#hsal_Observacion").val(obj.hsal_Observacion);
                $('#ModalEditar').modal('show');
            }
        });
}

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/HistorialSalidas/Edit/' + ID,
        'GET',
        function (obj) {
            var o = obj.hsal_Observacion == null ? "Ninguna" : obj.hsal_Observacion;
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetallesAX").find("#hsal_Observacion")["0"].innerText = o;
                $("#ModalDetallesAX").find("#hsal_FechaCrea")["0"].innerText = FechaFormato(obj.hsal_FechaCrea);
                $("#ModalDetallesAX").find("#hsal_FechaModifica")["0"].innerText = FechaFormato(obj.hsal_FechaModifica);
                $("#ModalDetallesAX").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesAX").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/HistorialSalidas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.hsal_Estado == 1
                    ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hsal_Id + ")'>Detalles</a><a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.hsal_Id + ")'>Editar</a>"
                    : Admin ?
                        "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
                if (value.hsal_Estado > fill) {
                    tabla.row.add({
                        Id: value.hsal_Id,
                        ID: value.hsal_Id,
                        "Número": value.hsal_Id,
                        tsal_Id: value.tsal_Id,
                        TipoSalida: value.tsal_Descripcion,

                        rsal_Id: value.rsal_Id,
                        rsal_Descripcion: value.rsal_Descripcion,
                        NombreCompleto: value.per_Nombres,
                        per_CorreoElectronico: value.per_CorreoElectronico,
                        per_Telefono: value.per_Telefono,
                        per_Direccion: value.per_Direccion,
                        per_Edad: value.per_Edad,
                        per_EstadoCivil: value.per_EstadoCivil,
                        hsal_Observacion: value.hsal_Observacion,
                        hsal_Estado: value.hsal_Estado,
                        hsal_FechaSalida: value.hsal_FechaSalida,
                        Acciones: Acciones,
                        Estado: value.hsal_Estado ? "Activo" : "Inactivo"
                        //
                    });
                }
            });
            tabla.draw();
        });
}
$("#btnEditar").click(function () {
    _ajax(null,
        '/HistorialSalidas/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#hsal_Observacion").val(obj.hsal_Observacion);
            }
        });
});

$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#hsal_RazonInactivo").val("");
    $("#ModalInactivar").find("#hsal_RazonInactivo").focus();
});

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        //data.tiho_Id = id;
        // data = JSON.stringify({ tbTipoHoras: data });
        $.post("/HistorialSalidas/Delete", data).done(function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                LimpiarControles(["hsal_Observacion", "hsal_RazonInactivo"]);
                llenarTabla();
            } else {
                MsgError("Error", "No se inactivó el registro, contacte al administrador.");
            }
        });
    } else {
        MsgError(" ", "La eliminación de información debe ser justificada.");
    }
});

$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    //data = serializar(data);
    if (data != null) {
        data.tiho_Id = id;
        //data = JSON.stringify({ tbTipoHoras: data });
        $.post("/HistorialSalidas/Edit", data).done(function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                LimpiarControles(["hsal_Observacion"]);
                llenarTabla();
            } else {
                MsgError("Error", "No se editó el registro, contacte al administrador.");
            }
        });
    }
});
//aqui estaba
function format(obj) {
    var EstadoCivil = '';
    var meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", ];
    var div = '<div class="ibox"><div class="ibox-title"><h5>Información personal y de contacto: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        index.per_EstadoCivil.toUpperCase() == ('S') ? EstadoCivil = 'Soltero(a)'
    : index.per_EstadoCivil.toUpperCase() == ('C') ? EstadoCivil = 'Casado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('D') ? EstadoCivil = 'Divorciado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('V') ? EstadoCivil = 'Viudo'
    : 'Union Libre';
        div = div
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
    div += '<div class="ibox"><div class="ibox-title"><h5>Información sobre la salida: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        fecha = new Date(parseInt(index.hsal_FechaSalida.replace("/Date(", "").replace(")/", ""), 10));
        var dia = fecha.getDate();
        var mes = meses[fecha.getMonth()];
        var annio = fecha.getFullYear();
        var hora = fecha.getHours();
        var fechamnsj = dia + " de " + mes + " del " + annio;
        var observa = index.hsal_Observacion == null ? "Ninguna" : index.hsal_Observacion;
        div = div
            + '<div class="col-md-2"><b>Tipo de salida: </b></div><div class="col-md-10">' + index.rsal_Descripcion + '</div>'
            + '<div class="col-md-2"><b>Razón salida: </b></div><div class="col-md-10">' + index.tsal_Descripcion + '</div>'
            + '<div class="col-md-2"><b>Observaciones: </b></div><div class="col-md-10">' + observa + '</div>'
            + '<div class="col-md-2"><b>Fecha salida: </b></div><div class="col-md-10">' + fechamnsj + '</div>'
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
            '/HistorialSalidas/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});
