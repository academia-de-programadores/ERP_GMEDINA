var fill = 0;
var Admin = false;
function tablaDetalles(id) {
    var validacionPermiso = userModelState("Areas/details");
    if (validacionPermiso.status == true) {
        $(location).attr('href', "/Areas/details/" + id);
    }
}
function tablaEditar(id) {
    var validacionPermiso = userModelState("Areas/Edit");
    if (validacionPermiso.status == true) {
        $(location).attr('href', "/Areas/Edit/" + id);
    }
}
function format(obj) {
    var div = '<div class="ibox">' +
                '<div class="ibox-title"><h5>Departamentos</h5>' +
                '<div class="ibox-tools">' +
                        '<a class="collapse-link" onclick="flecha(this)" >' +
                        '<i class="fa fa-chevron-up"></i>' +
                        '</a>' +
                '</div>' +
                '</div><div class="ibox-content"><div class="row">';
    obj.forEach(function (index,value) {
        div = div +
            '<div class="col-md-3">' +
                '<div class="ibox">' +
                  '<div class="ibox-title">' +
                     '<h5>' + index.depto_Descripcion + '</h5>' +
                '</div>' +
                '<div class="ibox-content">' +
                    '<h5>' + index.car_Descripcion + '</h5>'+
                    '<div>Empleados:<strong class="pull-right">' + index.Empleados + '</strong></div>'+
                    '<h4>Encargado</h4>';

        if (index.persona.per_NombreCompleto[0] != undefined) {
            div = div +                
                '<i class="fa fa-user margin "></i>' + index.persona.per_NombreCompleto[0] + '<br>' +
                '<i class="fa fa-phone margin "></i>' + index.persona.per_Telefono[0] + '<br>' +
                '<a href="mailto:#"><i class="fa fa-envelope-square"></i>' + index.persona.per_CorreoElectronico[0] + '</a>'+   
                 '</div></div></div>';
        }
        else {
            div = div +
                '<i class="fa fa-user margin "></i>Sin asignar <br>' +
                '<i class="fa fa-phone margin "></i> No aplica <br>'+
                '<i class="fa fa-envelope-square"></i>No aplica <br></div></div></div>';
        }
                '</div>'+
            '</div>'
    });
    return div + '</div></div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/Areas/llenarTabla',
       'POST',
       function (Lista) {
            tabla.clear();
            tabla.draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
                var Acciones = value.area_Estado==1
                    ? null : Admin ?
                   "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
                tabla.row.add({
                    Estado:value.area_Estado?'Activo':'Inactivo',
                    "Número":value.area_Id,
                    ID: value.area_Id,
                    "Área": value.area_Descripcion,
                    Encargado: value.Encargado.length == 0 ? 'Sin Asignar' : value.Encargado[0],
                    Empleados:value.Empleados,
                    Sucursales: value.Sucursales,
                    Acciones: Acciones
                });
            });
            tabla.draw();
       });
}
function flecha(obj) {
    var ibox = $(obj).closest('div.ibox');
    var button = $(obj).find('i');
    var content = ibox.find('div.ibox-content');
    content.slideToggle(200);
    button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
    ibox.toggleClass('').toggleClass('border-bottom');
    setTimeout(function () {
        ibox.resize();
        ibox.find('[id^=map-]').resize();
    }, 50);
}
$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
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
        id = row.data().ID;
        hola = row.data().hola;
        //linea que hace aparecer el spiner
        //"htmlSpiner" es el nombre de la variable que contiene el spiner en html
        row.child(htmlSpiner).show();
        tr.addClass('shown');
        _POST({ id: id },
            '/Areas/ChildRowData',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    //desaparecemos el spiner
                    row.child.hide();
                    tr.removeClass('shown');
                    //dibuja el childRow
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                } else {
                    row.child.hide();
                    tr.removeClass('shown');
                    //dibuja el childRow
                    row.child("Error de conexion").show();
                    tr.addClass('shown');
                }
            });
    }
});
