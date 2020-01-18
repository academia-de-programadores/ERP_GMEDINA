$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;

    llenarTabla();
});
var fill = 0;
var id = 0;

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
                       "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Habilitar</a>" +
                   "</div>";
                 if (value.Estado > fill) {
                     tabla.row.add({
                         ID: value.Id,
                         "Número": value.Id,
                         Identidad: value.Identidad,
                         Nombre: value.Nombre,
                         Fase: value.Fase,
                         Plaza_Solicitada: value.Plaza_Solicitada,
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
        id = row.data().Id;
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
       });

}

function CallEditar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    tablaEditar(id);

}



$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbSeleccionCandidatos: data });
        _ajax(data,
            '/SeleccionCandidatos/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Éxito!", "Se ha actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
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
    if (data != null) {
        data = JSON.stringify({ tbSeleccionCandidatos: data });
        _ajax(data,
            '/SeleccionCandidatos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    $("#ModalNuevo").find("#per_Id").find("option[value='" + $("#ModalNuevo").find("#per_Id").val() + "']").remove();
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["per_Id", "fare_Id", "scan_Fecha", "req_Id"]);

                    MsgSuccess("¡Éxito!", "Se ha agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
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
                    MsgWarning("¡Éxito!", "Se ha Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
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
        });
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
