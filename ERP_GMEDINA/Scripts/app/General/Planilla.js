﻿// validar rangos de fecha

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


//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      
  })
  .fail(function (jqxhr, settings, exception) {
      
  });

//OBTENER SCRIPT DE EXPORTAR A EXCEL
$.getScript("../Scripts/app/General/excelexportjs.js")
  .done(function (script, textStatus) {
      
  })
  .fail(function (jqxhr, settings, exception) {
      
  });

//FUNCION GENERICA PARA REUTILIZAR AJAX
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
//variable para reconocer la planilla actual de la tabla
var planillaId = '';
var nombrePlanilla = '';


//FUNCION: CARGAR DATA DE UNA PLANILLA ESPECIFICA Y REFRESCAR LA TABLA DEL INDEX ================================
$('.cargarPlanilla').click(function () {
    $('#btnPlanilla').attr('disabled', true);
    //$('#Cargando').css('display', '');
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


//CONFIRMAR GENERAR PLANILLA =======================================================================================
$('#btnPlanilla').click(function () {
    $('#fechaInicio').val('');
    $('#fechaFin').val('');
    $('#ConfigurarGenerarPlanilla').modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "auto");
});


//GENERAR PLANILLA
$('#btnPrevisualizarPlanilla').click(function () {

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
            fechaFin: dateFin
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
            if (1==1) {
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
});

//GENERAR PLANILLA
$('#btnGenerarPlanilla').click(function () {

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


    if (modelState == true) {
        $('#Modal').modal({ backdrop: 'static', keyboard: false });
        $('#ConfigurarGenerarPlanilla').modal('hide');
        $('#btnPlanilla').css('display', 'none');
        $('#Cargando').css('display', '');
        $('#confirmarGenerarPlanilla').hide();
        _ajax({
            ID: planillaId,
            enviarEmail: EnviarEmailBool,
            fechaInicio: date,
            fechaFin: dateFin
        },
        '/Planilla/GenerarPlanilla/',
        'POST',
        (data) => {
            $('#btnPlanilla').css('display', '');
            $('#Cargando').css('display', 'none');
            $('#Modal').modal('hide');
            var nombresArchivos = nombrePlanilla == '' ? 'Planilla general' : 'Planilla ' + nombrePlanilla;

            //generar csv
            GenerarCSV == true ? JSONToCSVConvertor(data.Data, nombresArchivos, true) : '';

            //generar excel
            if (GenerarExcel) {
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
        }
    );
    }
});

// =====================================================================================================

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