function format(obj) {
    var div = '<div class="col-lg-12  >';
    div += '<div class="row col-lg-12">';
    div += '<div class="ibox">';
    div += '<div class="ibox-content" style="">';
    div += '<div class="panel-body">';
    div += '<div class="panel-group" id="accordion">';
    var Id = "";   var Comp = ""   ;var Hab = ""   ;var ReEs = "";   var Idi = "";   var Tit = "";   var Id="";
    obj.forEach(function (index, value) {
        debugger
        Id = index.per_Id.toString();
        if(index.Relacion == "Competencias") 
            Comp += '<label>'+index.Descripcion.toString() + '</label><br>';
        else if(index.Relacion == "Habilidades")
            Hab += '<label>' + index.Descripcion.toString() + '</label><br>';
        else if(index.Relacion == "Idiomas")
            Idi += '<label>' + index.Descripcion.toString() + '</label><br>';
        else if (index.Relacion == "Requerimientoss_Especiales")
            ReEs += '<label>' + index.Descripcion.toString() + '</label><br>';
        else if (index.Relacion == "Titulos")
            Tit += '<label>' + index.Descripcion.toString() + '</label><br>';
    });
    if (Comp.length == 0)
        Comp += '<label>Sin Datos.</label>';
    if (Hab.length == 0)
        Hab += '<label>Sin Datos.</label>';
    if (Idi.length == 0)
        Idi += '<label>Sin Datos.</label>';
    if (ReEs.length == 0)
        ReEs += '<label>Sin Datos.</label>';
    if (Tit.length == 0)
        Tit += '<label>Sin Datos.</label>';
    var TodoPersona = [Comp, Hab, Idi, ReEs, Tit];
    var Encabezados = ['Competencias', 'Habilidades', 'Idiomas', 'Requerimientos_Especiales', 'Titulos'];
    for (i = 0 ; i < TodoPersona.length ; i++) {
        div += '<div class="panel panel-default">';
        div += '<div class="panel-heading" data-toggle="collapse" data-parent="#accordion' + Id + '" href="#' + Encabezados[i] + Id + '" class="collapsed" aria-expanded="false">';
        div += '<h5 class="panel-title"><a >' + Encabezados[i].replace("_", " "); + '</a></h5>';
        div += '</div>';
        div += '<div id="' + Encabezados[i] + Id + '" class="panel-collapse in collapse" style="true">';
        div += '<div class="panel-body">';
        div += '' + TodoPersona[i] + '';
        div += '</div>';
        div += '</div>';
        div += '</div>';
    }
    Id = "";
    div += '</div>';
    return div ;
}
function llenarTabla() {
    _ajax(null,
       '/Personas/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               tabla.row.add({
                   ID: value.Id,
                   Identidad: value.Identidad,
                   NombreCompleto: value.Nombre,
                   CorreoElectronico: value.CorreoElectronico
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
        id = row.data().ID;
       hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/Personas/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }
});

function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Personas/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#per_Id")["0"].innerText = obj.per_Id;
                $("#ModalDetalles").find("#per_Identidad")["0"].innerText = obj.per_Identidad;

                $('#ModalDetalles').modal('show');
            }
        });
}