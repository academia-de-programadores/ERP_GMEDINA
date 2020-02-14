// variable para reconocer la planilla actual de la tabla
var planillaId = '';
var nombrePlanilla = '';

// modal de generar planilla
$('#btnPlanilla').click(function () {

    $('#fechaInicio').val('');
    $('#fechaFin').val('');
    
    $("#configuracionTasasDeCambio").css('display', 'none');

    // dropdownlist para la moneda de las deducciones fiscales
    $.ajax({
        url: "/Planilla/getMonedas/",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        // llendar ddl con la data obtenida
        .done(function (data) {
            $("#tmon_IdMonedaDeduccionesDePlanilla").empty();
            $("#tmon_IdMonedaDeduccionesDePlanilla").append("<option value='0'>Selecione una moneda</option>");
            $.each(data, function (i, iter) {
                $("#tmon_IdMonedaDeduccionesDePlanilla").append("<option value='" + iter.tmon_Id + "'>" + iter.tmon_Descripcion + "</option>");
            });
        })
    .fail(function () {
        iziToast.error({
            title: 'Error',
            message: 'No se pudieron cargar los tipos de moneda',
        });
    });
    

    $('#ConfigurarGenerarPlanilla').modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "auto");
});

// ejecutar previsualización de planilla
$('#btnPrevisualizarPlanilla').click(function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Planilla/Index");

    if (validacionPermiso.status == true) {

        var ID = planillaId;
        var modelState = true;
        var GenerarExcel = false, GenerarPDF = false, GenerarCSV = false, EnviarEmailBool = false;
        EnviarEmailBool = $('#EnviarEmail').is(":checked");
        GenerarExcel = $('#Excel').is(":checked");
        GenerarPDF = $('#PDF').is(":checked");
        GenerarCSV = $('#CSV').is(":checked");

        // validar fecha inicio    
        var date = $('#fechaInicio').val();
        var dateFin = $('#fechaFin').val();
        var monedaDeducciones = $("#tmon_IdMonedaDeduccionesDePlanilla").val();

        if (monedaDeducciones == 0 || monedaDeducciones == '' || monedaDeducciones == null || monedaDeducciones == undefined) {
            modelState = false;
            $("#Validation_MonedaDeduccionPlanillaRequerido").css('display', '');
        }

        // validar que sea mayor que 1920
        if (date < '1920-01-01') {
            $("#validation_FechaInicioMenor1920").css('display', '');
            $("#AsteriscoFechaInicio").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaInicioMenor1920").css('display', 'none');
            $("#AsteriscoFechaInicio").removeClass('text-danger');
        }

        // validar que no sea mayor que el rango fin
        if (date >= dateFin) {

            $("#validation_FechaInicioRequerida").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');

            $("#validation_FechaFinMenorFechaInicio").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

            if (date >= '1920-01-01') {
                $("#AsteriscoFechaInicio").removeClass('text-danger');
            }

            $("#AsteriscoFechaFin").removeClass("text-danger");
        }


        // validar que no esté vacío
        if (date == '') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaInicioMenor1920").css('display', 'none');
            $("#validation_FechaInicioRequerida").css('display', '');
            $("#AsteriscoFechaInicio").addClass("text-danger");
            modelState = false;
        }
        else {

            $("#validation_FechaInicioRequerida").css('display', 'none');
            if (date >= '1920-01-01') {
                $("#AsteriscoFechaInicio").removeClass('text-danger');
            }
        }

        // validar fecha fin 

        // validar que sea mayor que 1920
        if (dateFin < '1920-01-01') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');
            $("#validation_FechaFinMenor192").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#AsteriscoFechaFin").removeClass('text-danger');
        }

        // validar que no sea mayor que el rango fin
        if (date >= dateFin) {

            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');
            $("#validation_FechaFinMenorFechaInicio").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

            if (dateFin >= '1920-01-01') {
                $("#AsteriscoFechaFin").removeClass('text-danger');
            }
        }


        // validar que no esté vacío
        if (dateFin == '') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {

            $("#validation_FechaFinRequerida").css('display', 'none');
            if (dateFin >= '1920-01-01') {
                $("#AsteriscoFechaFin").removeClass('text-danger');
            }
        }

        if (parseInt(cantidadColaboradores) == 0) {
            modelState = false;
            iziToast.error({
                title: 'Error',
                message: 'No hay colaboradores registrados en planilla',
            });
        }

        // validar montos de las tasas de cambio
        var monedasArray = new Array();
        $("#tblTasasCambio TBODY TR").each(function () {
            var row = $(this);
            var DC = {};
            DC.tmon_Id = row.find("TD input[name=tmon_Id]").val();
            DC.tmon_Descripcion = row.find("TD input[name=tmon_Descripcion]").val();
            DC.tmon_Cambio = row.find("TD input[name=tmon_Cambio]").val();
            monedasArray.push(DC);
        });

        if (modelState == true) {
            $('#Modal').modal({ backdrop: 'static', keyboard: false });
            $("html, body").css("overflow", "hidden");
            $("html, body").css("overflow", "auto");
            $('#ConfigurarGenerarPlanilla').modal('hide');
            $('#btnPlanilla').css('display', 'none');
            $('#Cargando').css('display', '');
            $('#confirmarGenerarPlanilla').hide();
            _ajax({
                ID: planillaId,
                enviarEmail: EnviarEmailBool,
                fechaInicio: date,
                fechaFin: dateFin,
                monedas: monedasArray,
                tmon_IdMonedaDeduccionesDePlanilla: parseInt(monedaDeducciones)
            },
            '/Planilla/PrevisualizarPlanilla/',
            'POST',
            (data) => {
                $('#btnPlanilla').css('display', '');
                $('#Cargando').css('display', 'none');
                $('#Modal').modal('hide');
                var nombresArchivos = nombrePlanilla == '' ? 'Planilla general' : 'Planilla ' + nombrePlanilla;

                //generar csv
                GenerarCSV == true ? JSONToCSVConvertor(data.Data, nombresArchivos, true) : '';

                //generar excel
                if (GenerarExcel == true) {
                    $("#dvjson").excelexportjs({
                        containerid: "dvjson"
                           , datatype: 'json'
                           , dataset: data.Data
                           , columns: getColumns(data.Data)
                    });
                }

                if (data.Response.Tipo == 'success') {
                    iziToast.success({
                        title: data.Response.Encabezado,
                        message: data.Response.Response,
                    });
                }
                else if (data.Response.Tipo == 'error') {
                    iziToast.error({
                        title: data.Response.Encabezado,
                        message: data.Response.Response,
                    });
                }
                else if (data.Response.Tipo == 'warning') {
                    iziToast.warning({
                        title: data.Response.Encabezado,
                        message: data.Response.Response,
                    });
                }

                $('.modal-backdrop').css('display', 'none');
                $('.fade').css('display', 'none');
                $('.in').css('display', 'none');
            }
        );
        }
    }
});

// ejecutar generación de planilla
$('#btnGenerarPlanilla').click(function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Planilla/Index");

    if (validacionPermiso.status == true) {

        var ID = planillaId;
        var modelState = true;
        var GenerarExcel = false, GenerarPDF = false, GenerarCSV = false, EnviarEmailBool = false;
        EnviarEmailBool = $('#EnviarEmail').is(":checked");
        GenerarExcel = $('#Excel').is(":checked");
        GenerarPDF = $('#PDF').is(":checked");
        GenerarCSV = $('#CSV').is(":checked");

        // validar fecha inicio    
        var date = $('#fechaInicio').val();
        var dateFin = $('#fechaFin').val();

        // validar la moneda de las deducciones de planila
        var monedaDeducciones = $("#tmon_IdMonedaDeduccionesDePlanilla").val();        

        // validar que sea mayor que 1920
        if (date < '1920-01-01') {
            $("#validation_FechaInicioMenor1920").css('display', '');
            $("#AsteriscoFechaInicio").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaInicioMenor1920").css('display', 'none');
            $("#AsteriscoFechaInicio").removeClass('text-danger');
        }

        // validar que no sea mayor que el rango fin
        if (date >= dateFin) {

            $("#validation_FechaInicioRequerida").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');

            $("#validation_FechaFinMenorFechaInicio").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

            if (date >= '1920-01-01') {
                $("#AsteriscoFechaInicio").removeClass('text-danger');
            }

            $("#AsteriscoFechaFin").removeClass("text-danger");
        }


        // validar que no esté vacío
        if (date == '') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaInicioMenor1920").css('display', 'none');
            $("#validation_FechaInicioRequerida").css('display', '');
            $("#AsteriscoFechaInicio").addClass("text-danger");
            modelState = false;
        }
        else {

            $("#validation_FechaInicioRequerida").css('display', 'none');
            if (date >= '1920-01-01') {
                $("#AsteriscoFechaInicio").removeClass('text-danger');
            }
        }

        // validar fecha fin 

        // validar que sea mayor que 1920
        if (dateFin < '1920-01-01') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');
            $("#validation_FechaFinMenor192").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#AsteriscoFechaFin").removeClass('text-danger');
        }

        // validar que no sea mayor que el rango fin
        if (date >= dateFin) {

            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', 'none');
            $("#validation_FechaFinMenorFechaInicio").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {
            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

            if (dateFin >= '1920-01-01') {
                $("#AsteriscoFechaFin").removeClass('text-danger');
            }
        }

        // validar que no esté vacío
        if (dateFin == '') {

            $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
            $("#validation_FechaFinMenor192").css('display', 'none');
            $("#validation_FechaFinRequerida").css('display', '');
            $("#AsteriscoFechaFin").addClass("text-danger");
            modelState = false;
        }
        else {

            $("#validation_FechaFinRequerida").css('display', 'none');
            if (dateFin >= '1920-01-01') {
                $("#AsteriscoFechaFin").removeClass('text-danger');
            }
        }

        if (parseInt(cantidadColaboradores) == 0) {
            modelState = false;
            iziToast.error({
                title: 'Error',
                message: 'No hay colaboradores registrados en planilla',
            });
        }

        if (monedaDeducciones == 0 || monedaDeducciones == '' || monedaDeducciones == null || monedaDeducciones == undefined) {
            modelState = false;
            $("#Validation_MonedaDeduccionPlanillaRequerido").css('display', '');
        }

        debugger;

        // validar montos de las tasas de cambio
        var monedasArray = new Array();
        $("#tblTasasCambio TBODY TR").each(function () {
            var row = $(this);
            var DC = {};
            DC.tmon_Id = parseInt(row.find("TD input[name=tmon_Id]").val());
            DC.tmon_Descripcion = row.find("TD input[name=tmon_Descripcion]").val();
            DC.tmon_Cambio = parseFloat((row.find("TD input[name=tmon_Cambio]").val()).replace(/,/g,''));
            monedasArray.push(DC);
        });

        debugger;

        if (modelState == true) {
            $('#Modal').modal({ backdrop: 'static', keyboard: false });
            $('#ConfigurarGenerarPlanilla').modal('hide');
            $('#btnPlanilla').css('display', 'none');
            $('#Cargando').css('display', '');
            $('#confirmarGenerarPlanilla').hide();

            $.ajax({
                url: "/Planilla/GenerarPlanilla",
                method: "POST",
                data: {
                    ID: planillaId,
                    enviarEmail: EnviarEmailBool,
                    fechaInicio: date,
                    fechaFin: dateFin,
                    monedas: monedasArray,
                    tmon_IdMonedaDeduccionesDePlanilla: parseInt(monedaDeducciones)
                }
            }).done(function (data) {
                
                    $('#btnPlanilla').css('display', '');
                    $('#Cargando').css('display', 'none');
                    $('#Modal').modal('hide');
                    var nombresArchivos = nombrePlanilla == '' ? 'Planilla general' : 'Planilla ' + nombrePlanilla;

                    //generar csv
                    GenerarCSV == true ? JSONToCSVConvertor(data.Data, nombresArchivos, true) : '';

                    //generar excel
                    if (GenerarExcel == true) {
                        $("#dvjson").excelexportjs({
                            containerid: "dvjson"
                               , datatype: 'json'
                               , dataset: data.Data
                               , columns: getColumns(data.Data)
                        });
                    }


                    if (data.listaDeErrores != '') {
                        $("#dvjson").excelexportjs({
                            containerid: "dvjson"
                               , datatype: 'json'
                               , dataset: data.listaDeErrores
                               , columns: getColumns(data.listaDeErrores)
                        });
                    }



                    if (data.Response.Tipo == 'success') {
                        iziToast.success({
                            title: data.Response.Encabezado,
                            message: data.Response.Response,
                        });
                    }
                    else if (data.Response.Tipo == 'error') {
                        iziToast.error({
                            title: data.Response.Encabezado,
                            message: data.Response.Response,
                        });
                    }
                    else if (data.Response.Tipo == 'warning') {
                        iziToast.warning({
                            title: data.Response.Encabezado,
                            message: data.Response.Response,
                        });
                    }

                    $('.modal-backdrop').css('display', 'none');
                    $('.fade').css('display', 'none');
                    $('.in').css('display', 'none');
                
                
            });
        }
    }

});

// validar fecha inicio
$('#fechaInicio').on("keyup change", function () {

    var date = $(this).val();
    var dateFin = $('#fechaFin').val();

    // validar que sea mayor que 1920
    if (date < '1920-01-01') {
        $("#validation_FechaInicioMenor1920").css('display', '');
        $("#AsteriscoFechaInicio").addClass("text-danger");
    }
    else {
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#AsteriscoFechaInicio").removeClass('text-danger');
    }

    // validar que no sea mayor que el rango fin
    if (date >= dateFin) {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        $("#validation_FechaFinRequerida").css('display', 'none');

        $("#validation_FechaFinMenorFechaInicio").css('display', '');
        $("#AsteriscoFechaFin").addClass("text-danger");
    }
    else {
        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

        if (date >= '1920-01-01') {
            $("#AsteriscoFechaInicio").removeClass('text-danger');
        }

        $("#AsteriscoFechaFin").removeClass("text-danger");
    }


    // validar que no esté vacío
    if (date == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaInicioMenor1920").css('display', 'none');
        $("#validation_FechaInicioRequerida").css('display', '');
        $("#AsteriscoFechaInicio").addClass("text-danger");
    }
    else {

        $("#validation_FechaInicioRequerida").css('display', 'none');
        if (date >= '1920-01-01') {
            $("#AsteriscoFechaInicio").removeClass('text-danger');
        }
    }

});

// validar fecha inicio
$('#fechaFin').on("keyup change", function () {

    var date = $('#fechaInicio').val();
    var dateFin = $(this).val();

    // validar que sea mayor que 1920
    if (dateFin < '1920-01-01') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaFinRequerida").css('display', 'none');
        $("#validation_FechaFinMenor192").css('display', '');
        $("#AsteriscoFechaFin").addClass("text-danger");
    }
    else {
        $("#validation_FechaFinMenor192").css('display', 'none');
        $("#AsteriscoFechaFin").removeClass('text-danger');
    }

    // validar que no sea mayor que el rango fin
    if (date >= dateFin) {

        $("#validation_FechaFinMenor192").css('display', 'none');
        $("#validation_FechaFinRequerida").css('display', 'none');
        $("#validation_FechaFinMenorFechaInicio").css('display', '');
        $("#AsteriscoFechaFin").addClass("text-danger");
    }
    else {
        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');

        if (dateFin >= '1920-01-01') {
            $("#AsteriscoFechaFin").removeClass('text-danger');
        }
    }


    // validar que no esté vacío
    if (dateFin == '') {

        $("#validation_FechaFinMenorFechaInicio").css('display', 'none');
        $("#validation_FechaFinMenor192").css('display', 'none');
        $("#validation_FechaFinRequerida").css('display', '');
        $("#AsteriscoFechaFin").addClass("text-danger");
    }
    else {

        $("#validation_FechaFinRequerida").css('display', 'none');
        if (dateFin >= '1920-01-01') {
            $("#AsteriscoFechaFin").removeClass('text-danger');
        }
    }

});

// cargar planilla en específico
$('.cargarPlanilla').click(function () {

    $('#btnPlanilla').attr('disabled', true);
    
    var ID = $(this).data('id');

    _ajax(null,
        '/Planilla/GetPlanilla/' + ID,
        'GET',
        (data) => {


            if (data.length == 0) {
                //Validar si se genera un error al cargar la data de la planilla especifica
                $('#btnPlanilla').css('display', '');
                $('#Cargando').css('display', 'none');
                $('#Modal').modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var PlanillaSeleccionada = data, template = '';



            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblPreviewPlanilla').DataTable().clear();
            for (var i = 0; i < PlanillaSeleccionada.length; i++) {
                //AGREGAR EL ROW AL DATATABLE
                $('#tblPreviewPlanilla').dataTable().fnAddData([
                    PlanillaSeleccionada[i].Nombres,
                    PlanillaSeleccionada[i].per_Identidad,
                    PlanillaSeleccionada[i].salarioBase,
                    PlanillaSeleccionada[i].tmon_Descripcion
                ]);
            }


            ID == '' ? planillaId = null : planillaId = data[0].cpla_IdPlanilla;
            nombrePlanilla = data[0].cpla_DescripcionPlanilla;
            ID == '' ? $('#nombrePlanilla').html('') : $('#nombrePlanilla').html(data[0].cpla_DescripcionPlanilla);
            //$('#btnPlanilla').css('display', '');
            //$('#Cargando').css('display', 'none');
            //$('#Modal').modal('hide');

            $('#btnPlanilla').attr('disabled', false);
        });
});

// mostrar div de configuración de tasas de cambio
$("#tmon_IdMonedaDeduccionesDePlanilla").on("change", function () {
    
    if ($("#tmon_IdMonedaDeduccionesDePlanilla").val() != '0') {

        //$("#configuracionTasasDeCambio").css('display', 'none');

        $("#cargarSpinner").css('display','');

        // ocultar mensaje de moneda requerida
        $("#Validation_MonedaDeduccionPlanillaRequerido").css('display', 'none');

        // id de la planilla seleccionada por el usuario (en caso de haber una)
        var ID = planillaId;

        // moneda seleccionada para las deducciones fiscales
        var idMonedaSeleccionada = $("#tmon_IdMonedaDeduccionesDePlanilla").val();

        // contador de monedas a las que se les tiene que ingresar la tasa de cambio
        var contadorTasaDeCambio = 0;

        // traer data para las tasas de cambio
        $.ajax({
            url: "/Planilla/getMonedasPlanilla/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: { cpla_IdPlanilla: ID }
        })
            // llendar ddl con la data obtenida
            .done(function (data) {                

                // llenar tbody de la tabla de tasas de cambio
                $("#tbBodyTasasDeCambio").empty();
                $.each(data, function (i, iter) {

                    // si la moneda del iterador es diferente a la moneda seleccionada para las deducciones fiscales, dibujarla para que se ingrese su respectiva tasa de cambio
                    if (iter.tmon_Id != idMonedaSeleccionada)
                    {
                        $("#tbBodyTasasDeCambio").append("<tr>" +
                                                     "'<td>" + iter.tmon_Id + "</td>" +
                                                     "'<td>" + iter.tmon_Descripcion + "</td>" +
                                                     "<td>" +
                                                     "<input class='montoTasaCambio soloNumeroPlanilla MascaraCantidadPlanilla text-success text-line' name='tmon_Cambio' id='tmon_Cambio' value='0.00' />" +
                                                     "<input type='hidden' name='tmon_Id' id='tmon_Id' value='" + iter.tmon_Id + "' />" +
                                                     "<input type='hidden' name='tmon_Descripcion' id='tmon_Descripcion' value='" + iter.tmon_Descripcion + "' />" +
                                                     "</td></tr>");
                        contadorTasaDeCambio++;
                    }
                    
                    $("#cargarSpinner").css('display', 'none');

                    // si las monedas a las que se les tiene que hacer tasa de cambio es mayor a cero, mostrar el div
                    if (contadorTasaDeCambio > 0)
                    {
                        // mostrar el div donde se configuran los tipos de cambio
                        $("#descripcionMoneda").html($("#tmon_IdMonedaDeduccionesDePlanilla option:selected").text());
                        $("#configuracionTasasDeCambio").css('display', 'block');
                    }
                    else {
                        $("#configuracionTasasDeCambio").css('display', 'none');
                    }

                    
                });

                // mascara para los inputs del tbody
                $(".MascaraCantidadPlanilla").inputmask("decimal", {
                    alias: 'numeric',
                    groupSeparator: ',',
                    digits: 3,
                    integerDigits: 18,
                    digitsOptional: false,
                    placeholder: '0',
                    radixPoint: ".",
                    autoGroup: true
                });
                $(".soloNumeroPlanilla").ForceNumericOnly();

            })
        .fail(function () {
            iziToast.error({
                title: 'Error',
                message: 'No se pudieron cargar los tipos de moneda para las tasas de cambio',
            });
            $("#cargarSpinner").css('display', 'none');
        });
       
    }
    else {
        $("#configuracionTasasDeCambio").css('display', 'none');

        $("#Validation_MonedaDeduccionPlanillaRequerido").css('display', '');
    }
});




// ============================= FUNCIONES  ================================

var cantidadColaboradores = 0;
$(document).ready(function () {

    cantidadColaboradores = $("#cantidad").val();
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('#datepicker .input-group.date')
		.datepicker({
		    todayBtn: 'linked',
		    keyboardNavigation: false,
		    forceParse: false,
		    calendarWeeks: true,
		    autoclose: true,
		    format: 'yyyy/mm/dd'
		})

});

// cargar serialize date 
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {

  })
  .fail(function (jqxhr, settings, exception) {

  });

// cargar escript para generar excel
$.getScript("../Scripts/app/General/excelexportjs.js")
  .done(function (script, textStatus) {

  })
  .fail(function (jqxhr, settings, exception) {

  });

// función genérica 
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        type: type,
        data: params,
        success: function (data) {
            callback(data);
        }
    });
}

function s2ab(s) {
    var buf = new ArrayBuffer(s.length); //convert s to arrayBuffer
    var view = new Uint8Array(buf);  //create uint8array as viewer
    for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF; //convert to octet
    return buf;
}

// exportar a csv
function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "Planilla";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}