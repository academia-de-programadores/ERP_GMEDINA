
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
                $("#FormEditar").find("#emp_Id").val(obj[0].Id_Empleado);
                $("#FormEditar").find("#maximo").val(obj[0].Sueldo_Maximo);
                $("#FormEditar").find("#minimo").val(obj[0].Sueldo_Minimo);
                $("#FormEditar").find("#tmon_Id").val(obj[0].Id_Amonestacion);
                $("#FormEditar").find("#sue_Cantidad").val(obj[0].Sueldo);
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
                   Fecha_Modifica: value.Fecha_Modifica,
                   Sueldo_Maximo: value.Sueldo_Maximo,
                   Sueldo_Minimo: value.Sueldo_Minimo

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
                $("#ModalDetalles").find("#sue_Cantidad")["0"].innerText = obj[0].Sueldo;
                //$("#ModalDetalles").find("#sue_UsuarioCrea")["0"].innerText = obj[0].Usuario_Crea;
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj[0].Usuario_Nombre;
                $("#ModalDetalles").find("#sue_FechaModifica")["0"].innerText = FechaFormato(obj[0].Fecha_Modifica);
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
    var data = $('#FormEditar').serializeArray();
    data = serializar(data);

    if (data != null) {
        let a = parseFloat(data.sue_Cantidad);
        let b = parseFloat(data.minimo);
        let c = parseFloat(data.maximo);
        console.log(a, b, c);
        if (a >= b) {
            if (a <= c) {
                data = JSON.stringify({ tbsueldos: data });

                _ajax(data,
                    '/Sueldos/Edit',
                    'POST',
                    function (obj) {

                        if (obj != "-1" && obj != "-2" && obj != "-3") {
                            CierraPopups();
                            llenarTabla();
                            LimpiarControles(["sue_Id", "sue_Cantidad", "maximo", "minimo", "tmod_Id", "emp_Id"]);
                            MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa");
                        } else {
                            MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe.)");
                        }
                    });
            }
            else {
                MsgError("Error", "Ingrese un valor menor.");
            }

        }
        else {
            MsgError("Error", "Ingrese un valor mayor.");
        }

    } else {
        MsgError("Error", "por favor llene todas las cajas de texto.");
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
                $("#FormEditar").find("#sue_Id").val(ID);
                $("#FormEditar").find("#emp_Id").val(obj[0].Id_Empleado);
                $("#FormEditar").find("#maximo").val(obj[0].Sueldo_Maximo);
                $("#FormEditar").find("#minimo").val(obj[0].Sueldo_Minimo);
                $("#FormEditar").find("#tmon_Id").val(obj[0].Id_Amonestacion);
                $("#FormEditar").find("#sue_Cantidad").val(obj[0].Sueldo);

                $("#ModalEditar").find("#sue_Cantidad").focus();

            }



        });
});
