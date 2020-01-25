var id = 0;
var fill = 0;
var Admin = false;
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
            Comp += '<label style="color:#585858">' + index.Descripcion.toString() + '</label><br>';
        else if(index.Relacion == "Habilidades")
            Hab += '<label style="color:#585858">' + index.Descripcion.toString() + '</label><br>';
        else if(index.Relacion == "Idiomas")
            Idi += '<label style="color:#585858">' + index.Descripcion.toString() + '</label><br>';
        else if (index.Relacion == "Requerimientos_Especiales")
            ReEs += '<label style="color:#585858">' + index.Descripcion.toString() + '</label><br>';
        else if (index.Relacion == "Titulos")
            Tit += '<label style="color:#585858">' + index.Descripcion.toString() + '</label><br>';
    });
    if (Comp.length == 0)
        Comp += '<label style="color:#585858">Sin datos que mostrar.*</label>';
    if (Hab.length == 0)
        Hab += '<label style="color:#585858">Sin datos que mostrar.*</label>';
    if (Idi.length == 0)
        Idi += '<label style="color:#585858">Sin datos que mostrar.*</label>';
    if (ReEs.length == 0)
        ReEs += '<label style="color:#585858">Sin datos que mostrar.*</label>';
    if (Tit.length == 0)
        Tit += '<label style="color:#585858">Sin datos que mostrar.*</label>';
    var TodoPersona = [Comp, Hab, Idi, ReEs, Tit];
    var Encabezados = ['Competencias', 'Habilidades', 'Idiomas', 'Requerimientos_Especiales', 'Títulos'];
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
function ModalInactivar(id) {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#per_Id").val(id);
    $("#ModalInactivar").find("#per_RazonInactivo").val("");
    $("#ModalInactivar").find("#per_RazonInactivo").focus();
};
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbPersonas: data });
        _ajax(data,
            '/Personas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["per_Descripcion", "per_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "El registro se ha inactivado de forma exitosa");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
function llenarTabla() {
    _ajax(null,
       '/Personas/llenarTabla',
       'POST',
       function (Lista) {
           if (validarDT(Lista)) {
               return null;
           }
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               var Acciones = value.per_Estado == 1
                    ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.Id + ")'>Detalles</a><a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.Id + ")'>Editar</a><a class='btn btn-danger btn-xs ' onclick='ModalInactivar("+value.Id+")'>Inactivar</a>"
                    : Admin ?
                    "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
               if(value.per_Estado > fill)
               tabla.row.add({
                   Estado: value.per_Estado ? 'Activo' : 'Inactivo',
                   "Número": value.Id,
                   ID: value.Id,
                   Identidad: value.Identidad,
                   NombreCompleto: value.Nombre,
                   CorreoElectrónico: value.CorreoElectronico,
                   Acciones: Acciones
               });
           });
           tabla.draw();
       });
}

$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
    sessionStorage.clear();
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
        '/Personas/Detalles/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#per_Identidad")["0"].innerText = obj[0].per_Identidad;
                $("#ModalDetalles").find("#per_Nombres")["0"].innerText = obj[0].per_Nombres + ' ' + obj[0].per_Apellidos;
                $("#ModalDetalles").find("#tbNacionalidades")["0"].innerText = obj[0].nac_Id;
                $("#ModalDetalles").find("#per_Edad")["0"].innerText = obj[0].per_Edad + ' años';
                $("#ModalDetalles").find("#per_TipoSangre")["0"].innerText = obj[0].per_TipoSangre;
                $("#ModalDetalles").find("#per_Direccion")["0"].innerText = obj[0].per_Direccion;
                $("#ModalDetalles").find("#per_Telefono")["0"].innerText = obj[0].per_Telefono;
                $("#ModalDetalles").find("#per_CorreoElectronico")["0"].innerText = obj[0].per_CorreoElectronico;
                $("#ModalDetalles").find("#per_FechaCrea")["0"].innerText = FechaFormato(obj[0].per_FechaCrea);
                $("#ModalDetalles").find("#per_FechaModifica")["0"].innerText = FechaFormato(obj[0].per_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj[0].per_UsuarioCrea;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj[0].per_UsuarioModifica;


                $('#ModalDetalles').modal('show');
            }
        });
}
function tablaEditar(ID) {
    id = ID;
    sessionStorage.setItem("IdPersona", id);
    window.location.href = "/Personas/Edit";
    //_ajax(null,
    //    '/Personas/Edit/',
    //    'GET',
    //    function (obj) {
    //        if (obj != "-1" && obj != "-2" && obj != "-3") {
    //        }
    //    });
}
