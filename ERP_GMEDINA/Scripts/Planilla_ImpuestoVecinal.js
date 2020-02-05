//
//DECLARACIONES GENERALES

//
//EVENTOS GENERALES

//
//FUNCIONES GENERALES

//FUNCION: OBTENER SCRIPT DE SERIALIZACIÓN DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });

//FUNCION: GRID DE PLANILLA DE IMPUESTO VECINAL
function cargarGridCalculoIV() {
    var esAdministrador = $("#rol_Usuario").val();
    $.ajax({
        url: "/TechoImpuestoVecinal/GetDataCalculo",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {

        if (data.length == 0) {
            iziToast.error({
                title: 'Error',
                message: 'No se cargó la información, contacte al administrador',
            });
        }
        else {
            var LitaCalculoIV = data;
            //limpiar datatable
            $('#tblCalculoIV').DataTable().clear();
            //recorrer data obtenida del backend
            for (var i = 0; i < LitaCalculoIV.length; i++) {

                //agregar el row al datatable
                $('#tblCalculoIV').dataTable().fnAddData([
                    LitaCalculoIV[i].dimv_Id,
                    LitaCalculoIV[i].per_Nombres + ' ' + LitaCalculoIV[i].per_Apellidos,
                    (LitaCalculoIV[i].dimv_MontoTotal % 1 == 0) ? LitaCalculoIV[i].dimv_MontoTotal + ".00" : LitaCalculoIV[i].dimv_MontoTotal,
                    (LitaCalculoIV[i].dimv_CuotaAPagar % 1 == 0) ? LitaCalculoIV[i].dimv_CuotaAPagar + ".00" : LitaCalculoIV[i].dimv_CuotaAPagar,
                ]);
            }
        }
    });
    FullBody();
}

