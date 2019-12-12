$(document).ready(function () {
    llenarTabla();
});
var id = 0;

function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Sueldos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#Sueldo").val(obj.Sueldo);
                $("#ModalEditar").modal('show');
            }
        });
}



function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().Id;
    $(location).attr('href', "/Sueldos/Edit/" + id);
}
function tablaEditar(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    $(location).attr('href',"/Sueldos/Edit/" + id);
}
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Sueldos</h5></div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index, value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="panel panel-default">' +
                  '<div class="panel-heading">' +
                     '<h5>' + index.Cargo + '</h5>' +
                '</div>' +
                '<div class="panel-body">' +
                    '<h5>' + index.Sueldo_Anterior + '</h5>'
                    //'<span class="fa fa-user-o m-r-xs"></span>' +
                    + index.Tipo_Moneda + '<br>' +
                    //'<span class="fa fa-phone m-r-xs"></span>' +
                    index.Cuenta + '</div>' +
                '</div>' +
            '</div>'
    });
    return div + '</div></div></div>';
}


function llenarTabla() {
    _ajax(null,
       '/Sueldos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   Id:value.Id,
                   Identidad :value.Identidad,
                   Nombre: value.Nombre,
                   Sueldo :value.Sueldo,
                   Tipo_Moneda:value.Tipo_Moneda,
                   Cuenta :value.Cuenta,
                   Sueldo_Anterior: value.Sueldo_Anterior,
                   Area: value.Area,
                   Cargo: value.Cargo,
                   Usuario_Nombre : value.Usuario_Nombre,
                   Usuario_Crea : value.Usuario_Crea,
                   Fecha_Crea: value.Fecha_Crea,
                   Usuario_Modifica : value.Usuario_Modifica,
                   Fecha_Modifica: value.Fecha_Modifica
               });
           });
           tabla.draw();
       });
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
            '/Sueldos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});
function CallEditar() {
    var modaleditar = $("#ModalEditar");
    modaleditar.modal('show');
}
$("#btnEditar").click(function () {
    _ajax(null,
        '/Sueldos/Edit'+ id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#FormEditar").find("#Sueldo").val(obj.Sueldo);

            }
        });
});




function CallDetalles() {
    var modalnuevo = $("#ModalDetalles");
    modalnuevo.modal('show');

}




$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.Id = id;
        data = JSON.stringify({ V_Sueldos: data });
        _ajax(data,
            '/Sueldos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});







