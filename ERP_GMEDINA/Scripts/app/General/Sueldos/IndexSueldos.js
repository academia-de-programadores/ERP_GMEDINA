
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

                $("#FormEditar").find("#sue_Id").val(ID);
                $("#FormEditar").find("#emp_Id").val(obj.emp_Id);
                $("#FormEditar").find("#tmon_Id").val(obj.tmon_Id);
                $("#FormEditar").find("#sue_Cantidad").val(obj.sue_Cantidad);
                $("#ModalEditar").modal('show');



            }
        });
}


function format(obj) {
    var div = '<div class="ibox"><div class="ibox-title"><h5>Historial de Sueldos del Empleado</h5><div align=right><div class="ibox-content"><div class="row">' + '<table class="table table-striped table-borderef table-hover dataTables-example"> ' +
        '<thead>' +
            '<tr>' +
                '<th>' + 'Sueldo Anterior' + '</th>' +
                '<th>' + 'Id del empleado' + '</th>' +
                '<th>' + 'Cuenta Bancaria' + '</th>' +
                '<th>' + 'Cargo' + '</th>' +
                '<th>' + 'Fecha Modifica' + '</th>' +



                '</tr>' +
                '</thead>';
    obj.forEach(function (index, value) {
        div = div +
                '<tbody>' +
                '<tr>' +
                '<td>' + index.Sueldo + '</td>' +
                '<td>' + index.Identidad + '</td>' +
                '<td>' + index.Cuenta + '</td>' +
                '<td>' + index.Cargo + '</td>' +
                '<td>' + FechaFormato(index.Fecha_Modifica).substring(0, 10) + '</td>' +


                '</tr>' +
                '</tbody>'
        '</table>'
    });
    return div + '</div></div>';
}
function llenarTabla() {
    _ajax(null,
       '/Sueldos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           if (validarDT(Lista)) {
               return null;
           }
           $.each(Lista, function (index, value) {
               tabla.row.add({

                   ID: value.Id,
                   "Número": value.Id,
                   Identidad: value.Identidad,
                   Id_Empleado: value.Id_Empleado,
                   Id_Amonestacion: value.Id_Amonestacion,
                   Nombre: value.Nombre,
                   Sueldo: value.Sueldo,
                   Tipo_Moneda: value.Tipo_Moneda,
                   Cuenta: value.Cuenta,
                   Sueldo_Anterior: value.Sueldo_Anterior,
                   Area: value.Area,
                   "Área": value.Area,
                   Cargo: value.Cargo,
                   Usuario_Nombre: value.Usuario_Nombre,
                   Usuario_Crea: value.Usuario_Crea,
                   Fecha_Crea: value.Fecha_Crea,
                   Usuario_Modifica: value.Usuario_Modifica,
                   Fecha_Modifica: value.Fecha_Modifica

               })
               .draw();

           });
       });
}


function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Sueldos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#sue_Cantidad")["0"].innerText = obj.sue_Cantidad;
                //$("#ModalDetalles").find("#sue_Estado")["0"].innerText = obj.sue_Estado;
                //$("#ModalDetalles").find("#sue_RazonInactivo")["0"].innerText = obj.sue_RazonInactivo;
                $("#ModalDetalles").find("#sue_UsuarioCrea")["0"].innerText = obj.sue_UsuarioCrea;
                $("#ModalDetalles").find("#sue_FechaCrea")["0"].innerText = FechaFormato(obj.sue_FechaCrea);
                //$("#ModalDetalles").find("#sue_UsuarioModifica")["0"].innerText = obj.sue_UsuarioModifica;
                //$("#ModalDetalles").find("#sue_FechaModifica")["0"].innerText = FechaFormato(obj.sue_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                //$("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;

                $('#ModalDetalles').modal('show');
            }
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
        id = row.data().Id_Empleado;
        hola = row.data().hola;
        _ajax({ id: parseInt(id) },
            '/Sueldos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.removeClass('loading,gif');
                    tr.addClass('shown');
                }
            });
    }
});






$("#btnActualizar").click(function () {
    if ( $('#sue_Cantidad').val() > 0) {
            var data = $('#FormEditar').serializeArray();
            data = serializar(data);
            data.sue_Cantidad = parseFloat(data.sue_Cantidad);
            if (data != null) {

                data = JSON.stringify({ tbsueldos: data });
                _ajax(data,
                    '/Sueldos/Edit',
                    'POST',
                    function (obj) {
                        if (obj != "-1" && obj != "-2" && obj != "-3") {
                            CierraPopups();
                            llenarTabla();
                            LimpiarControles(["sue_Id", "sue_Cantidad"]);
                            MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa");
                        } else {
                            MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe.)");
                        }
                    });
            } else {
                MsgError("Error", "por favor llene todas las cajas de texto.");
            }
        } else {
        MsgError("Error", "Por favor ingrese  solo valores numéricos positivos. ");
    }
});






$("#btnEditar").click(function tablaEditar() {
    _ajax(null,
        '/Sueldos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $("#ModalEditar").modal('show');
                $("#ModalEditar").find("#sue_Id").val(id);
                $("#ModalEditar").find("#emp_Id").val(obj.emp_Id);
                $("#ModalEditar").find("#tmon_Id").val(obj.tmon_Id);
                $("#ModalEditar").find("#sue_Cantidad").val(obj.sue_Cantidad);
                $("#ModalEditar").find("#sue_Cantidad").focus();

            }



        });
});
