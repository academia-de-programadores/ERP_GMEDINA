//FUNCION GENERICA PARA REUTILIZAR AJAX
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        type: type,
        data: { params },
        success: function (data) {
            callback(data);
        }
    });
}

//$(document).ready(function () {
//    console.clear();
//});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    _ajax(null,
        '/DecimoCuartoMesController/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaDecimoCuarto = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDecimoCuarto.length; i++) {
                template += '<tr data-id = "' + ListaDecimoCuarto[i].emp_id + '">' +
                    '<td>' + ListaDecimoCuarto[i].per_Nombres + '</td>' +
                    '<td>' + ListaDecimoCuarto[i].per_Apellidos + '</td>' +
                    '<td>' + ListaDecimoCuarto[i].car_Descripcion + '</td>' +
                    '<td>' + ListaDecimoCuarto[i].cpla_DescripcionPlanilla + '</td>' +
                    '<td>' + ListaDecimoCuarto[i].DecimoCuartoMes + '</td>' +
                    '<td>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDecimoTCuartoMes').html(template);
        });
    FullBody();
}

$("body").on("click", "#btnProcesar", function () {
    //Recorra las filas de la tabla y cree una matriz JSON.
    var DecimoCuarto = new Array();
    $("#tblDecimoCuartoMes TBODY TR").each(function () {
        var row = $(this);
        var DC = {};
        DC.emp_Id = row.find("TD").eq(0).html();
        DC.dcm_Monto = row.find("TD").eq(5).html();
        DecimoCuarto.push(DC);
    });
    console.log(DecimoCuarto);
    //Envíe la matriz JSON al controlador con AJAX.
    $.ajax({
        type: "POST",
        url: "/DecimoCuartoMes/InsertDecimoCuartoMes",
        data: JSON.stringify(DecimoCuarto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            iziToast.success({
                title: 'Decimo Cuarto Mes',
                message: "Registros procesados !"
            });
        },
        error: function (e) {
            iziToast.error({
                title: 'Decimo Cuarto Mes',
                message: "Los registros no se procesaron."
            });
        }
    });
});

$("body").on("click", "#btnProcesarFE", function () {
    //Recorra las filas de la tabla y cree una matriz JSON.
    var DecimoCuarto = new Array();
    $("#tblDecimoCuartoMesFE TBODY TR").each(function () {
        var row = $(this);
        var DC = {};
        DC.emp_Id = row.find("TD").eq(0).html();
        DC.dtm_Monto = row.find("TD").eq(5).html();
        DecimoCuarto.push(DC);
    });
    
    //Envíe la matriz JSON al controlador con AJAX.
    $.ajax({
        type: "POST",
        url: "/DecimoCuartoMes/InsertDecimoCuartoMes",
        data: JSON.stringify(DecimoCuarto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            iziToast.success({
                title: 'Decimo Cuarto Mes',
                message: "Registros procesados !"
            });
        },
        error: function (e) {
            iziToast.error({
                title: 'Decimo Cuarto Mes',
                message: "Los registros no se procesaron."
            });
        }
    });
});

// MOSTRAR MODAL DE FECHAS
$(document).on("click", "#btnFechaEspecifica", function () {
    $("#frmFechaDecimoCuarto").modal();
});

// OCULTAR MODAL DE FECHAS
$("#btnCerrarFecha").click(function () {
    $("#frmFechaDecimoCuarto").modal('hide');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarFecha").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnEnviarFecha").click(function () {
    var FechaInicial = $("#hipa_FechaInicio").val();
    var FechaFinal = $("#hipa_FechaFin").val();

    if (FechaInicial == "" || FechaFinal == "") {
        $("#Validation_descipcion").css("display", "");
        $("#Validation_descipcion2").css("display", "");
    }

});