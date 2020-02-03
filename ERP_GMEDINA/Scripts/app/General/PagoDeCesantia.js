var dataTable;
var listadoCesantia = new Array();
var GenerarCSV = false;



//OBTENER EL OBJETO DEL SPINNER
SpinnerElement = $("#SpinnerData");

//FUNCION: RETORNAR EL CONTENT DEL SPINNER
function spinner() {
    return `<div class="sk-spinner sk-spinner-wave">
                <div class="sk-rect1"></div>
                <div class="sk-rect2"></div>
                <div class="sk-rect3"></div>
                <div class="sk-rect4"></div>
                <div class="sk-rect5"></div>
             </div>`;
}

//FUNCION: CARGAR GRID
function cargarGridCesantia(data) {
    //CAPTURAR LA DATA EN UNA VARIABLE DE CONTEXTO LOCAL
    var ListaCesantia = data;
    //LIMPIAR EL DATATABLE
    $('#TablaPagoDeCesantiaDetalle').DataTable().clear();
    //ITERAR LA LISTA CON LOS REGISTROS
    for (var i = 0; i < ListaCesantia.length; i++) {
        //AGREGAR DATA AL ROW DEL DATATABLE
        $('#TablaPagoDeCesantiaDetalle').dataTable().fnAddData([
                ListaCesantia[i].IdCesantia,
                ListaCesantia[i].NoIdentidad,
                ListaCesantia[i].NombreCompleto,
                ListaCesantia[i].DiasPagados,
                (ListaCesantia[i].SueldoBrutoDiario % 1 == 0) ? ListaCesantia[i].SueldoBrutoDiario + ".00" : ListaCesantia[i].SueldoBrutoDiario,
                (ListaCesantia[i].TotalCesantiaPRO % 1 == 0) ? ListaCesantia[i].TotalCesantiaPRO + ".00" : ListaCesantia[i].TotalCesantiaPRO,
                ListaCesantia[i].NoDeCuenta]
            );
    }
}

//BOTON: CARGAR LA DATA DEL DATATABLE
$("#ProcesarCalculo").click(function () {
    //MOSTRAR EL SPINNER
    SpinnerElement.html(spinner());
    //REALIZAR LA PETICION DE LA DATA
    $.ajax({
        url: "/PagoDeCesantiaDetalle/ObtenerListaDePagoCesantia",
        method: "GET"
    }).done(function (data) {
        //VALIDAR LA RESPUESTA DEL SERVIDOR
        if(data == "error")
        {
            //OCULTAR EL SPINNER
            SpinnerElement.empty();
            //LANZAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al recuperar los registros, contacte al administrador.',
            });
        }
        else
        {
            //ALMACENAR LA LISTA EN UNA VARIABLE GLOBAL
            listadoCesantia = data;
            //CARGAR EL DATATABLE
            cargarGridCesantia(data);
            //OCULTAR EL SPINNER
            SpinnerElement.empty();
            //DESBLOQUEAR EL BOTON DE GENERAR
            $("#btnGenerarCesantia").attr("disabled", false);
            //DESBLOQUEAR EL BOTON DE IMPRIMIR
            $("#btnImprimirExcel").attr("disabled", false);
        }
    }).fail(function () {
        //OCULTAR SPINNER 
        SpinnerElement.empty();
        //LANZAR MENSAJE DE ERROR
        iziToast.error({
            title: 'Error',
            message: 'Ocurrio un error al recuperar los registros, contacte al administrador.',
        });
    });
});

//BOTON: IMPRIMIR EXCEL
$('#btnImprimirExcel').click(function () {
	var data = FormatearData(listadoCesantia);
    //GENERAR EXCEL
	ImprimirExcel(data);

});

//REALIZAR INSERCIÓN
$("#btnGenerarCesantia").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnGenerarCesantia").attr("disabled", true);
    //MOSTRAR EL SPINNER
    SpinnerElement.html(spinner());
    //REALIZAR LA PETICION DE LA DATA
    $.ajax({
        url: "/PagoDeCesantiaDetalle/ProcesarCesantia",
        method: "POST"
    }).done(function (data) {
        if (data == "SinCargar")
        {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: 'Alerta',
                message: 'El cálculo de cesantia continua siendo procesado, por favor espere.',
            });
        }
        else if (data.Obj_response == "error")
        {
            //DESBLOQUEAR EL BOTON
            $("#btnGenerarCesantia").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al insertar los registros, contacte al administrador.',
            });
        }
        else if(data.Obj_response == "bien")
        {
            //SETEAR EL LISTADO GLOBAL DE CESANTÍAS
            listadoCesantia = data.data;
            //LIMPIAR EL DATATABLE
            $('#TablaPagoDeCesantiaDetalle').DataTable().clear();
            //LLAMAR FUNCION DE IMPRIMIR EXCEL
            ImprimirExcel(data.data);
            //MENSAJE EN CASO DE ÉXITO
            iziToast.success({
                title: 'Éxito',
                message: '¡Los registros se agregaron de forma exitosa!',
            });
            //SetTimeOut
            setTimeout(function () {
            	window.location.href = '/PagoDeCesantiaDetalle/Index';
            }, 3500);
        }
        //OCULTAR EL SPINNER
        SpinnerElement.empty();

    }).fail(function () {
        //DESBLOQUEAR EL BOTON
        $("#btnGenerarCesantia").attr("disabled", false);
        //OCULTAR SPINNER 
        SpinnerElement.empty();
        //LANZAR MENSAJE DE ERROR
        iziToast.error({
            title: 'Error',
            message: 'Ocurrio un error al insertar los registros, contacte al administrador.',
        });
    });
});

//FUNCION: IMPRIMIR EXCEL
function ImprimirExcel(data) {

    //generar csv
    //GenerarCSV == true ? JSONToCSVConvertor(data.Data, nombresArchivos, true) : '';

    //generar excel
    //if (GenerarExcel) {
        $("#dvjson").excelexportjs({
            containerid: "dvjson"
               , datatype: 'json'
               , dataset: data
               , columns: getColumns(data)
        });
    //}
}

//FORMATEAR A TEXTO LOS CAMPOS DE LA LISTA
function FormatearData(ListaCesantia)
{
	for (var i = 0; i < ListaCesantia.length; i++) {
		
                ListaCesantia[i].IdCesantia
                ListaCesantia[i].NoIdentidad = "'" + ListaCesantia[i].NoIdentidad;
                ListaCesantia[i].NombreCompleto;
                ListaCesantia[i].DiasPagados;
                ListaCesantia[i].SueldoBrutoDiario;
                ListaCesantia[i].TotalCesantiaPRO;
                ListaCesantia[i].NoDeCuenta = "'" + ListaCesantia[i].NoDeCuenta;
	}

	return ListaCesantia;
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
