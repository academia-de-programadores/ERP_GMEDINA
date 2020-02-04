$(document).ready(function () {
    llenarTabla();
});
function llenarTabla() {
    _ajax(null,
       '/HistorialCargos/llenarTabla',
       'POST',
       function (Lista) {
           tabla.clear();
           tabla.draw();
           $.each(Lista, function (index, value) {
               var Accion = value.PuedeDeshacer == 0 //value.
           ? null : 
           "<div>" +
              "<a class='btn  btn-danger btn-xs' onclick='CallDeshacer(this)' >Deshacer</a>" +
          "</div>";
               tabla.row.add({
                   Id: value.hcar_Id,
                   "Número": value.hcar_Id,
                   Encargado: value.Encargado,
                   Anterior: value.car_Anterior,
                   Nuevo: value.car_Nuevo,
                   Fecha: FechaFormato(value.hcar_Fecha).substring(0, 10),
                   "Acción": Accion
               });
           });
           tabla.draw();
       });
}

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
            '/HistorialCargos/ChildRowData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    row.child(format(obj)).show();
                    tr.addClass('shown');
                }
            });
    }

});
//Promoción
function btnAgregar() {
    $(location).attr('href', "/HistorialCargos/Promover");
}
$("#btnGuardar").click(function () {
    var data1 = $("#FormNuevo").serializeArray();
    data = serializar(data1);
    if (data != null) {
        data = JSON.stringify({ tbEmpleados: data });
        
        _ajax(data,
            '/HistorialCargos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["sue_Cantidad"]);

                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                } else {
                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }

});

//Degradar
function CallDeshacer(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().Id;
    $('#ModalDeshacer').modal('show');
    
    $("#ModalDeshacer").find("#hcar_Id").val(id);
    
    if (row.data().Fecha.substring(5, 6) == "/") {
        $("#ModalDeshacer").find("#hcar_FechaAntigua").val(row.data().Fecha.substring(6, 10) + "-" + row.data().Fecha.substring(3, 5) + "-" + row.data().Fecha.substring(0, 2));
    }
    else {
        $("#ModalDeshacer").find("#hcar_FechaAntigua").val(row.data().Fecha.substring(5, 9) + "-0" + row.data().Fecha.substring(3, 4) + "-" + row.data().Fecha.substring(0, 2));

    }


}



$("#btnDeshacer").click(function () {
    //declaramos el objeto principal de nuestra tabla y asignamos sus valores
    debugger
  
    var data = $("#FormDeshacer").serializeArray();
    var hcar_Id = $("#ModalDeshacer").find("#hcar_Id").val();
    var hcar_RazonPromocion = $("#ModalDeshacer").find("#hcar_RazonPromocion").val();
    var emp_Fechaingreso = $("#ModalDeshacer").find("#emp_Fechaingreso").val();


    data = serializar(data);
    if (data != null) {
        if (emp_Fechaingreso > $("#ModalDeshacer").find("#hcar_FechaAntigua").val())
        {

       
            if ($("#emp_Fechaingreso").val() > '01/01/1900') {
                data = JSON.stringify({
                    hcar_RazonPromocion: hcar_RazonPromocion,
                    emp_Fechaingreso: emp_Fechaingreso,
                    hcar_Id: hcar_Id
                });
                _ajax(data,
                    '/HistorialCargos/Deshacer',
                    'POST',
                    function (obj) {
                        if (obj != "-1" && obj != "-2" && obj != "-3") {
                            MsgSuccess("¡Éxito!", "Acción exitosa.");
                            $('#ModalDeshacer').modal('hide');
                            LimpiarControles(["hcar_RazonPromocion", "emp_Fechaingreso"]);
                            llenarTabla();

                        } else {
                            MsgError("Error", "No se actualizó el registro, contacte con el administrador.");
                        }
                    });
            }
            else {
                MsgError("Error", "Fecha no válida.");
            }
        }
        else
        {
            MsgError("Error", "La fecha no puede ser menor a la fecha de promoción.");
        }
    }
    else {
        MsgError("Error", "Por favor llene todas las cajas de texto.");
    }
});