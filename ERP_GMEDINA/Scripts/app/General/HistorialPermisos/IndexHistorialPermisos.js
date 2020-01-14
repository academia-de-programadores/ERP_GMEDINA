var id = 0;

//Funciones GET
function tablaEditar(ID) {
    //alert(ID);
    id = ID;
    _ajax(null,
        '/HistorialPermisos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                // $("#FormEditar").find("#tiho_Id").val(obj.habi_Descripcion);
                //$("#FormEditar").find("#hper_Observacion").val(obj.hper_Observacion);
                $('#ModalInhabilitar').modal('show');
                $("#ModalInhabilitar").find("#hper_RazonInactivo").val("");
                $("#ModalInhabilitar").find("#hper_RazonInactivo").focus();
            }
        });
}

function tablaDetalles(ID) {
    //alert(ID);
    id = ID;
    _ajax(null,
        '/HistorialPermisos/Edit/' + ID,
        'GET',
        function (obj) {
            var o = obj.hper_Observacion == null ? "Ninguna" : obj.hper_Observacion;
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetallesAX").find("#hper_Observacion")["0"].innerText = o;
                $("#ModalDetallesAX").find("#hper_FechaCrea")["0"].innerText = FechaFormato(obj.hper_FechaCrea);
                $("#ModalDetallesAX").find("#hper_fechaInicio")["0"].innerText = FechaFormato(obj.hper_fechaInicio);
                $("#ModalDetallesAX").find("#hper_fechaFin")["0"].innerText = FechaFormato(obj.hper_fechaFin);
                $("#ModalDetallesAX").find("#hper_Duracion")["0"].innerText = obj.hper_Duracion;
             

                //$("#ModalDetallesAX").find("#hper_FechaModifica")["0"].innerText = FechaFormato(obj.hper_FechaModifica);
                $("#ModalDetallesAX").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                //$("#ModalDetallesAX").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}






















//function llenarTabla() {
//    _ajax(null,
//        '/HistorialSalidas/llenarTabla',
//        'POST',
//        function (Lista) {
//            tabla.clear();
//            tabla.draw();
//            $.each(Lista, function (index, value) {
//                tabla.row.add({
//                    Id: value.hsal_Id,
//                    tsal_Id: value.tsal_Id,
//                    TipoSalida: value.tsal_Descripcion,
//                    rsal_Id: value.rsal_Id,
//                    rsal_Descripcion: value.rsal_Descripcion,
//                    NombreCompleto: value.per_Nombres,
//                    per_CorreoElectronico: value.per_CorreoElectronico,
//                    per_Telefono: value.per_Telefono,
//                    per_Direccion: value.per_Direccion,
//                    per_Edad: value.per_Edad,
//                    per_EstadoCivil: value.per_EstadoCivil,
//                    hsal_Observacion: value.hsal_Observacion,
//                    hsal_FechaSalida: value.hsal_FechaSalida,
//                    Accion: "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hsal_Id + ")'>Detalles</a><a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.hsal_Id + ")'>Editar</a>"
//                });
//            });
//            tabla.draw();
//        });
//}
function llenarTabla() {
    _ajax(null,
        '/HistorialPermisos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                tabla.row.add({
                    Id: value.hper_Id,
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
                    Accion: "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hper_Id + ")'>Detalles</a><a class='btn btn-danger btn-xs ' onclick='tablaEditar(" + value.hper_Id + ")'>Inhabilitar</a>"
                });
            });
            tabla.draw();
        });
}













$("#btnEditar").click(function () {
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
});

$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#hper_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#hper_RazonInactivo").focus();
});
//llamado
$("#InActivar").click(function () {
    //alert($("#hper_RazonInactivo").val());
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
                MsgSuccess("¡Exito!", "Se ha inhabilitado el registro");
            } else {
                MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
            }
        });
    } else {
        MsgWarning(" ", "la eliminación de información debe ser justificada");
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
                llenarTabla();
                LimpiarControles(["hper_Observacion"]);
                MsgSuccess("¡Exito!", "Se ha editado el registro");
            } else {
                MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
            }
        });
    }
});
//aqui estaba
function format(obj) {
    //var EstadoCivil = '';
    //var meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", ];
    var div = '<div class="ibox"><div class="ibox-title"><h5>Informacion personal y de contacto: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        index.per_EstadoCivil.toUpperCase() == ('S') ? EstadoCivil = 'Soltero(a)'
    : index.per_EstadoCivil.toUpperCase() == ('C') ? EstadoCivil = 'Casado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('D') ? EstadoCivil = 'Divorciado(a)'
    : index.per_EstadoCivil.toUpperCase() == ('V') ? EstadoCivil = 'Viudo'
    : 'Union Libre';
        div = div
        + '<div class="col-md-5"><b>Numero de identidad: </b>' + index.per_Identidad + '</div>'
        + '<div class="col-md-5"><B>Correo electrónico: </b>' + index.per_CorreoElectronico + '</div>'
        + '<div class="col-md-5"><b>Edad: </b>' + index.per_Edad + '</div>'
        + '<div class="col-md-5"><b>Dirección: </b>' + index.per_Direccion + '</div>'
        + '<div class="col-md-5"><b>Estado civil: </b>' + EstadoCivil + '</div>'
        + '<div class="col-md-5"><b>Teléfono: </b>' + index.per_Telefono + '</div>'
        + '</div>' +
        '</div>' +
        '</div>'
    });
    //div += '<div class="ibox"><div class="ibox-title"><h5>Informacion sobre la salida: </h5></div><div class="ibox-content"><div class="row">';
    //obj.forEach(function (index, value) {
    //    fecha = new Date(parseInt(index.hper_FechaInicio.replace("/Date(", "").replace(")/", ""), 10));
    //    var dia = fecha.getDate();
    //    var mes = meses[fecha.getMonth()];
    //    var annio = fecha.getFullYear();
    //    var hora = fecha.getHours();
    //    var fechamnsj = dia + " de " + mes + " del " + annio;
    //    var observa = index.hper_Observacion == null ? "Ninguna" : index.hper_Observacion;
    //    div = div
    //        //+ '<div class="col-md-2"><b>Tipo de salida: </b></div><div class="col-md-10">' + index.rsal_Descripcion + '</div>'
    //        //+ '<div class="col-md-2"><b>Razon salida: </b></div><div class="col-md-10">' + index.tsal_Descripcion + '</div>'
    //        + '<div class="col-md-2"><b>Observaciones: </b></div><div class="col-md-10">' + observa + '</div>'
    //        + '<div class="col-md-2"><b>Fecha salida: </b></div><div class="col-md-10">' + fechamnsj + '</div>'
    //        + '</div>' +
    //        '</div>' +
    //        '</div>'
    //});
    return div + '</div></div></div>';
}

$(document).ready(function () {
    llenarTabla();
});

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
