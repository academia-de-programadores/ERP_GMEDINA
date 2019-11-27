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

$(document).ready(function () {
    console.clear();
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    _ajax(null,
        '/DecimoTercerMesController/GetData',
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
            var ListaDecimoTercer = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDecimoTercer.length; i++) {
                template += '<tr data-id = "' + ListaDecimoTercer[i].emp_id + '">' +
                    '<td>' + ListaDecimoTercer[i].per_Nombres + '</td>' +
                    '<td>' + ListaDecimoTercer[i].per_Apellidos + '</td>' +
                    '<td>' + ListaDecimoTercer[i].car_Descripcion + '</td>' +
                    '<td>' + ListaDecimoTercer[i].cpla_DescripcionPlanilla + '</td>' +
                    '<td>' + ListaDecimoTercer[i].DecimoTercerMes + '</td>' +
                    '<td>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDecimoTercerMes').html(template);
        });
    FullBody();
}

$("body").on("click", "#btnProcesar", function () {
    //Recorra las filas de la tabla y cree una matriz JSON.
    var DecimoTercer = new Array();
    $("#tblDecimoTercerMes TBODY TR").each(function () {
        var row = $(this);
        var DC = {};
        DC.emp_Id = row.find("TD").eq(0).html();
        DC.dtm_Monto = row.find("TD").eq(5).html();
        DecimoTercer.push(DC);
    });

    //Envíe la matriz JSON al controlador con AJAX.
    $.ajax({
        type: "POST",
        url: "/DecimoTercerMes/InsertDecimoTercerMes",
        data: JSON.stringify(DecimoTercer),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            iziToast.success({
                title: 'Decimo Tercer Mes',
                message:"Registros procesados !"
            });         
        },
        error: function (e) {
            iziToast.error({
                title: 'Decimo Tercer Mes',
                message: "Los registros no se procesaron."
            });    
        }
    });
});

$("body").on("click", "#btnProcesarFE", function () {
    //Recorra las filas de la tabla y cree una matriz JSON.
    var DecimoTercer = new Array();
    $("#tblDecimoTercerMesFE TBODY TR").each(function () {
        var row = $(this);
        var DC = {};
        DC.emp_Id = row.find("TD").eq(0).html();
        DC.dtm_Monto = row.find("TD").eq(5).html();
        DecimoTercer.push(DC);
    });

    //Envíe la matriz JSON al controlador con AJAX.
    $.ajax({
        type: "POST",
        url: "/DecimoTercerMes/InsertDecimoTercerMes",
        data: JSON.stringify(DecimoTercer),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            iziToast.success({
                title: 'Decimo Tercer Mes',
                message: "Registros procesados !"
            });
        },
        error: function (e) {
            iziToast.error({
                title: 'Decimo Tercer Mes',
                message: "Los registros no se procesaron."
            });
        }
    });
});

// MOSTRAR MODAL DE FECHAS
$(document).on("click", "#btnFechaEspecifica", function () {    
    $("#frmFechaDecimoTercer").modal();    
});

// OCULTAR MODAL DE FECHAS
$("#btnCerrarFecha").click(function () {
    $("#frmFechaDecimoTercer").modal('hide');
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