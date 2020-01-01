var id = 0;

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
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetallesR").find("#hsal_Observacion")["0"].innerText = obj.hsal_Observacion;
                $("#ModalDetallesR").find("#hsal_Estado")["0"].innerText = obj.hsal_Estado;
                $("#ModalDetallesR").find("#hsal_FechaCrea")["0"].innerText = FechaFormato(obj.hsal_FechaCrea);
                $("#ModalDetallesR").find("#hsal_FechaModifica")["0"].innerText = FechaFormato(obj.hsal_FechaModifica);
                $("#ModalDetallesR").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesR").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/TipoSalidas/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                IndexTable.row.add([
                    '<tr data-id = "' + value.hsal_Id + '">' +
                        value.per_Nombres,
                        value.tsal_Descripcion,
                        "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                        "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.tsal_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.tsal_Id + ")'>Editar</a>" +
                        "</div>"
                    ]).draw();
            });
        });
}
//function llenarTabla() {
//    _ajax(null,
//        '/HistorialSalidas/llenarTabla',
//        'POST',
//        function (Lista) {
//            var IndexTable = $('#IndexTable').DataTable();
//            IndexTable.clear();
//            IndexTable.draw();
//            $.each(Lista, function (index, value) {
//                //console.log(item.tiho_Descripcion);
//                IndexTable.row.add(['<tr data-id = "' + value.hsal_Id + '">' +
//                    value.per_Nombres + " " + value.per_Apellidos,
//                    value.tiho_Recargo,
//                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
//                    "<button type='button' class='btn btn-primary btn-xs tablaDetalle' id='btnDetalle' data-toggle='modal' onclick='tablaDetalles(" + value.hsal_Id + ")' data-target='#ModalDetalles'>Detalle</button>" +
//                "<button type='button' class='btn btn-default btn-xs tablaEditar' id='btnEditarR' data-toggle='modal' onclick='tablaEditar(" + value.hasal + ")' data-target='#ModalEditar'>Editar</button>" +
//                "</div>"]).draw();
//            });
//        });
//}



//function llenarTabla() {
//    console.log('Prueba');
//    _ajax(null,
//        '/HistorialSalidas/llenarTabla',
//        'POST',
//        function (Lista) {
//            tabla.clear();
//            tabla.draw();
//            $.each(Lista, function (index, value) {
//                tabla.row.add([
//                    value.hsal_Id,
//                    value.tsal_Id,
//                    value.tsal_Descripcion,
//                    value.rsal_Id,
//                    value.rsal_Descripcion,
//                    value.per_Nombres,
//                    value.per_CorreoElectronico,
//                    value.per_Telefono,
//                    value.per_Direccion,
//                    value.per_Edad,
//                    value.per_EstadoCivil,
//                    value.hsal_Observacion,
//                    value.hsal_FechaSalida,
//                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
//                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.hsal_Id + ")' >Detalles</a>" +
//                    "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.hsal_Id + ")'>Editar</a>" +
//                    "</div>"
//                ]).draw();
//            });
//        })
//}



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

$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#hsal_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#hsal_RazonInactivo").focus();
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
                llenarTabla();
                LimpiarControles(["hsal_Observacion", "hsal_RazonInactivo"]);
                MsgSuccess("¡Exito!", "Se ha inhabilitado el registro");
            } else {
                MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
            }
        });
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
                LimpiarControles(["hsal_Observacion"]);
                MsgSuccess("¡Exito!", "Se ha editado el registro");
            } else {
                MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
            }
        });
    }
});
//aqui estaba
function format(obj) {
    var EstadoCivil = '';
    var meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre",];
    var div = '<div class="ibox"><div class="ibox-title"><h5>Informacion personal y de contacto: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index,value) {
        index.per_EstadoCivil.toUpperCase() == ('S') ? EstadoCivil = 'Soltero(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('C') ? EstadoCivil = 'Casado(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('D') ? EstadoCivil = 'Divorciado(a)'
    :   index.per_EstadoCivil.toUpperCase() == ('V') ? EstadoCivil = 'Viudo'
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
    div += '<div class="ibox"><div class="ibox-title"><h5>Informacion sobre la salida: </h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        fecha = new Date(parseInt(index.hsal_FechaSalida.replace("/Date(", "").replace(")/", ""), 10));
        var dia = fecha.getDate();
        var mes = meses[fecha.getMonth()];
        var annio = fecha.getFullYear();
        var hora = fecha.getHours();
        var fechamnsj = dia + " de " + mes + " del " + annio;
        div = div
            + '<div class="col-md-2"><b>Tipo de salida: </b></div><div class="col-md-10">' + index.rsal_Descripcion + '</div>'
            + '<div class="col-md-2"><b>Razon salida: </b></div><div class="col-md-10">' + index.tsal_Descripcion + '</div>'
            + '<div class="col-md-2"><b>Observaciones: </b></div><div class="col-md-10">' + index.hsal_Observacion + '</div>'
            + '<div class="col-md-2"><b>Fecha salida: </b></div><div class="col-md-10">' + fechamnsj + '</div>'
            + '</div>' +
            '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}

$('#IndexTable tbody').on('click', 'td.details-control', function () {
    console.log('Casi');
    var tr = $(this).closest('tr');
    var row = tabla.row(tr);
    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        id = row.data().Id;
        hola = row.data().hola;
        _ajax({ id : parseInt(id) },
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
