$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;

    llenarTabla();
});
var fill = 0;
var id = 0;
//Format Dibujar ChildRow
function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Historial Fases de Reclutamiento</h5><div align=right> </div></div><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                '<th>' + 'Número' + '</th>' +
                '<th>' + 'Fase de reclutamiento' + '</th>' +
                '<th>' + 'Fecha' + '</th>' +
                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        div = div +
                '<tbody>' +
                '<tr>' +
                '<td>' + index.fsel_Id+ '</td>' +
                '<td>' + index.Fase + '</td>' +
                '<td>' + FechaFormato(index.Fecha).substring(0, 10)+ '</td>' +
                '<td>';

        div += '</tr>' +
                    '</tbody>'
        '</table>'
    });
    return div + '</div></div>';
}
//VALIDACIÓN FECHA


function compare_dates() {
    var Fecha = $("#scan_Fecha").val();
    var fechalimite = '01/01/1900';
    var fechalimite2 = '01/01/2199';
    if (Date.parse(Fecha) < Date.parse(fechalimite)) {
        MsgError("Error", "Fecha no válida.");
    }
    else if (Date.parse(Fecha) > Date.parse(fechalimite2)) {
        MsgError("Error", "Fecha no válida.");
        
    }
    else {
        return true;
    }
}
//LLENAR INDEX////////////////////////////////////////////////////////////////////////////////////////
var scan_Id = 0;
function llenarTabla() {
    _ajax(null,
       '/SeleccionCandidatos/llenarTabla',
       'POST',
         function (Lista) {
             tabla.clear().draw();
             if (validarDT(Lista)) {
                 return null;
             }
             $.each(Lista, function (index, value) {
                 var Acciones = value.Estado == 1
                   ?null:
                   "<div>" +
                       "<a class='btn btn-outline btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                       "<a class='btn btn-outline btn-primary btn-xs' style= 'min-width: 70px;' onclick='CallDetalles(this)' >Detalles</a>" +
                   "</div>";
                 if (value.Estado > fill) {
                     tabla.row.add({
                         ID: value.Id,
                         "Número": value.Id,
                         Identidad: value.Identidad,
                         Nombre: value.Nombre,
                         "FaseActual": value.Fase,
                         "PlazaSolicitada": value.Plaza_Solicitada,
                         Fecha: FechaFormato(value.Fecha).substring(0, FechaFormato(value.Fecha).length - 8),
                         Estado: value.Estado ? "Activo" : "Inactivo",
                         Acciones: Acciones,
                         per_Id: value.per_Id
                 }).draw();
                 }
             });       
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
            '/SeleccionCandidatos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }
});


//EDITAR///////////////////////////////////////////////////////////////////////////////////////////////////////
function tablaEditar(id) {
    scan_Id = id;
    
            _ajax(null,
                '/SeleccionCandidatos/Edit/' + id,
               'GET',
               function (obj) {
                   if (obj != "-1" && obj != "-2" && obj != "-3") {
                       CierraPopups();
                       $("#ModalEditar").find("#tbPersonas_per_Identidad").val(obj.tbPersonas.per_Identidad + " - " + obj.tbPersonas.per_Nombres + " " + obj.tbPersonas.per_Apellidos);
                       $("#ModalEditar").find("#fare_Id").val(obj.fare_Id);
                       $("#ModalEditar").find("#req_Id").val(obj.req_Id);
                       if (FechaFormato(obj.scan_Fecha).substring(5, 6) == "/") {
                           $("#ModalEditar").find("#scan_Fecha").val(FechaFormato(obj.scan_Fecha).substring(6, 10) + "-" + FechaFormato(obj.scan_Fecha).substring(3, 5) + "-" + FechaFormato(obj.scan_Fecha).substring(0, 2));
                       }
                       else {
                           $("#ModalEditar").find("#scan_Fecha").val(FechaFormato(obj.scan_Fecha).substring(5, 9) + "-0" + FechaFormato(obj.scan_Fecha).substring(3, 4) + "-" + FechaFormato(obj.scan_Fecha).substring(0, 2));

                       }
                       $('#ModalEditar').modal('show');

                   }
               })
        ;

}

function CallEditar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    tablaEditar(id);

}


$("#btnActualizar").click(function () {
    debugger
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    var Fechaeditar = $("#ModalEditar").find("#scan_Fecha").val();
    var fechalimite = '01/01/1900';
    var fechalimite2 = '01/01/2199';
    if (Date.parse(Fechaeditar) < Date.parse(fechalimite) || Date.parse(Fechaeditar) > Date.parse(fechalimite2)) {
        MsgError("Error", "Fecha no válida.");
    }
    else{
        if (data != null) {
            data = JSON.stringify({ tbSeleccionCandidatos: data });
           
            _ajax(data,
                '/SeleccionCandidatos/Edit',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        CierraPopups();
                        llenarTabla();
                        MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                    } else {
                        MsgError("Error","No se pudo editar el registro, contacte al administrador.");
                    }
                });
            
        } 

        else {
            MsgError("Error", "Por favor llene todas las cajas de texto.");
        }
    }
});

//DETALLES///////////////////////////////////////////////////////////////////////////////////////////////////

function tablaDetalles(ID) {
    scan_Id = ID;
    _ajax(null,
        '/SeleccionCandidatos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#tbPersonas")["0"].innerText = obj.tbPersonas.per_Identidad;
                $("#ModalDetalles").find("#tbPersonasNombre")["0"].innerText = obj.tbPersonas.per_Nombres + " " + obj.tbPersonas.per_Apellidos;
                $("#ModalDetalles").find("#tbFasesReclutamiento")["0"].innerText = obj.tbFasesReclutamiento.fare_Descripcion;
                $("#ModalDetalles").find("#tbRequisiciones")["0"].innerText = obj.tbRequisiciones.req_Descripcion;
                $("#ModalDetalles").find("#scan_Fecha")["0"].innerText = FechaFormato(obj.scan_Fecha).substring(0, FechaFormato(obj.scan_Fecha).length - 8);;;
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#scan_FechaCrea")["0"].innerText = FechaFormato(obj.scan_FechaCrea);
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#scan_FechaModifica")["0"].innerText = FechaFormato(obj.scan_FechaModifica);
              
                $('#ModalDetalles').modal('show');
            }
        });
}

//AGREGAR///////////////////////////////////////////////////////////////////////////////////////////
function btnAgregar() {
    var modalnuevo = $("#ModalNuevo");
    modalnuevo.modal('show');
}

$("#btnGuardar").click(function () {
    var data1 = $("#FormNuevo").serializeArray();
    data = serializar(data1);
    if(compare_dates())
    if (data != null) {
        data = JSON.stringify({ tbSeleccionCandidatos: data });
        if (compare_dates()) {
            _ajax(data,
                '/SeleccionCandidatos/Create',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        $("#ModalNuevo").find("#per_Id").find("option[value='" + $("#ModalNuevo").find("#per_Id").val() + "']").remove();
                        CierraPopups();
                        llenarTabla();
                        LimpiarControles(["per_Id", "fare_Id", "scan_Fecha", "req_Id"]);

                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                    } else {
                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                    }
                });
        }
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }

});


//INACIVAR//////////////////////////////////////////////////////////////////////////////
function CallEliminar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;

    CierraPopups();
    $("#ModalInhabilitar").find("#scan_Id").val(id);
    _ajax(null,
        '/SeleccionCandidatos/Edit/' + id,
       'GET',
       function (obj) {
           if (obj != "-1" && obj != "-2" && obj != "-3") {
               $("#ModalInhabilitar").find("#per_Id").val(obj.per_Id);
               $("#ModalInhabilitar").find("#per_Descripcion").val(obj.tbPersonas.per_Identidad + " - " + obj.tbPersonas.per_Nombres + " " + obj.tbPersonas.per_Apellidos);
               $('#ModalInhabilitar').modal('show');
           }
       });


}

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);

    $("#ModalNuevo").find("#per_Id").append('<option value="' + data.per_Id + '">' + data.per_Descripcion + '</option>');
    if (data != null) {
        data = JSON.stringify({ tbSeleccionCandidatos: data });
        _ajax(data,
            '/SeleccionCandidatos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $('#ModalInhabilitar').modal('hide');
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["scan_RazonInactivo"]);
                    MsgWarning("¡Éxito!", "El registro se ha inactivado de forma exitosa.");
                } else {
                    MsgError("Error", "No se logró inactivar el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});


function btnInactivar() {
    var modalnuevo = $("#ModalInactivar");
    modalnuevo.modal('show');
}

$("#btnEditar").click(function () {
    
        
        _ajax(null,
            '/SeleccionCandidatos/Edit/' + scan_Id,
            'GET',

            function (obj) {
              
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        CierraPopups();
                        $("#ModalEditar").find("#tbPersonas_per_Identidad").val(obj.tbPersonas.per_Identidad + " - " + obj.tbPersonas.per_Nombres + " " + obj.tbPersonas.per_Apellidos);
                        $("#ModalEditar").find("#fare_Id").val(obj.fare_Id);
                        $("#ModalEditar").find("#req_Id").val(obj.req_Id);
                        if (FechaFormato(obj.scan_Fecha).substring(5, 6) == "/") {
                            $("#ModalEditar").find("#scan_Fecha").val(FechaFormato(obj.scan_Fecha).substring(6, 10) + "-" + FechaFormato(obj.scan_Fecha).substring(3, 5) + "-" + FechaFormato(obj.scan_Fecha).substring(0, 2));
                        }
                        else {
                            $("#ModalEditar").find("#scan_Fecha").val(FechaFormato(obj.scan_Fecha).substring(5, 9) + "-0" + FechaFormato(obj.scan_Fecha).substring(3, 4) + "-" + FechaFormato(obj.scan_Fecha).substring(0, 2));

                        }
                        $('#ModalEditar').modal('show');

                    }
                }
            ) ;
});


//EMPLEADO 
function CallContratar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var scan_Id = row.data().ID;
    var per_id = row.data().per_Id;
    debugger
    var Identidad = row.data().Identidad;
    var Nombre = row.data().Nombre;
    sessionStorage.setItem("scan_Id", scan_Id);
    sessionStorage.setItem("per_Id", per_id);
    sessionStorage.setItem("per_Descripcion", Identidad + " - " + Nombre);
    
    $(location).attr('href', "/SeleccionCandidatos/Contratar/" + scan_Id);
}
