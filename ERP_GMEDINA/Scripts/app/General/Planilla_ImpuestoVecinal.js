///
//*******DECLARACIONES*******

//ALMACENAMIENTO DE LA LISTA DE CESANTIAS EN EL LADO DEL CLIENTE
var ListaImpuestoVecinal = new Array();
//Bool PARA CONFIRMAR LA GENERACIÓN DE CSV
var GenerarCSV = false;
//VARIABLE DE CONFIRMACION DEL PROCESAMIENTO
var Procesando = false;
//VARIABLE DE CONFIRMACION DEL PROCESAMIENTO COMPLETADO
var ProcesadoCompleto = false;
//OBTENER EL OBJETO DEL SPINNER
SpinnerElement = $("#SpinnerData");

//
//*******FUNCIONES*******

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
function cargarGridImpuestoVecinal(data) {
    //CAPTURAR LA DATA EN UNA VARIABLE DE CONTEXTO LOCAL
    var ListaImpuestoVecinal = data;
    
    //LIMPIAR EL DATATABLE
    $('#tblPlanillaImpuestoVecinal').DataTable().clear();
    //ITERAR LA LISTA CON LOS REGISTROS
    for (var i = 0; i < ListaImpuestoVecinal.length; i++) {
        //AGREGAR DATA AL ROW DEL DATATABLE
        $('#tblPlanillaImpuestoVecinal').dataTable().fnAddData([
                ListaImpuestoVecinal[i].No,
                ListaImpuestoVecinal[i].NoIdentidad.replace(/--/g, '-'),
                ListaImpuestoVecinal[i].NombreCompleto,
               (ListaImpuestoVecinal[i].DeduccionMensual % 1 == 0) ? ListaImpuestoVecinal[i].DeduccionMensual + ".00" : ListaImpuestoVecinal[i].DeduccionMensual,
               (ListaImpuestoVecinal[i].Total_ImpuestoVecinal % 1 == 0) ? ListaImpuestoVecinal[i].Total_ImpuestoVecinal + ".00" : ListaImpuestoVecinal[i].Total_ImpuestoVecinal,
               ListaImpuestoVecinal[i].NoDeCuenta
        ]);
    }
}

//FUNCION: IMPRIMIR EXCEL
function ImprimirExcel(data) {
    $("#dvjson").excelexportjs({
        containerid: "dvjson"
           , datatype: 'json'
           , dataset: data
           , columns: getColumns(data)
    });
}

//FUNCION: FORMATEAR A TEXTO LOS CAMPOS DE LA LISTA
function FormatearData(ListaImpuesto) {
    //ALMACENAR LA DATA EN UNA VARIABLE DE CONTEXTO LOCAL
    var LocalListaImpuesto = ListaImpuesto;
    //FORMATEAR LA DATA
    for (var i = 0; i < ListaImpuesto.length; i++) {

        LocalListaImpuesto[i].No;
        LocalListaImpuesto[i].NoIdentidad = LocalListaImpuesto[i].NoIdentidad.replace(/--/g, '-');
        LocalListaImpuesto[i].NombreCompleto;
        (LocalListaImpuesto[i].Total_ImpuestoVecinal % 1 == 0) ? "'" + LocalListaImpuesto[i].Total_ImpuestoVecinal + ".00" : "'" + LocalListaImpuesto[i].Total_ImpuestoVecinal;
        (LocalListaImpuesto[i].DeduccionMensual % 1 == 0) ? "'" + LocalListaImpuesto[i].DeduccionMensual + ".00" : "'" + LocalListaImpuesto[i].DeduccionMensual;
        LocalListaImpuesto[i].NoDeCuenta = "'" + LocalListaImpuesto[i].NoDeCuenta;
    }
    return LocalListaImpuesto;
}

//FUNCION: EXPORTAR A CSV
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

//
//*******EVENTOS*******

//CLICK: CARGAR LA DATA DEL DATATABLE
$("#ProcesarProyeccion").click(function () {
//validar informacion del usuario
    //SETEAR LA VARIABLE DEL PROCESAMIENTO
    Procesando = true;
    //MOSTRAR EL SPINNER
    SpinnerElement.html(spinner());
    //REALIZAR LA PETICION DE LA DATA
    $.ajax({
        url: "/PlanillaImpuestoVecinal/ProcesarPlanillaImpuestoVecinal",
        method: "GET"
    }).done(function (data) {
        //VALIDAR LA RESPUESTA DEL SERVIDOR
        if (data == "error") {
            //SETEAR LA VARIABLE DEL PROCESAMIENTO
            ProcesadoCompleto = false;
            //SETEAR LA VARIABLE DEL PROCESAMIENTO
            Procesando = false;
            //OCULTAR EL SPINNER
            SpinnerElement.empty();
            //LANZAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al procesar la proyección, contacte al administrador.',
            });
        }
        else {
            //SETEAR LA VARIABLE DE PROCESADO COMPLETO
            ProcesadoCompleto = true;
            //SETEAR LA VARIABLE DEL PROCESAMIENTO
            Procesando = false;
            //ALMACENAR LA LISTA EN UNA VARIABLE GLOBAL
            ListaImpuestoVecinal = data;
            //CARGAR EL DATATABLE
            
            cargarGridImpuestoVecinal(data);
            //OCULTAR EL SPINNER
            SpinnerElement.empty();
        }
    }).fail(function () {
        //SETEAR LA VARIABLE DEL PROCESAMIENTO
        ProcesadoCompleto = false;
        //SETEAR LA VARIABLE DEL PROCESAMIENTO
        Procesando = false;
        //OCULTAR SPINNER 
        SpinnerElement.empty();
        //LANZAR MENSAJE DE ERROR
        iziToast.error({
            title: 'Error',
            message: 'Ocurrio un error al conectar con el servidor, contacte al administrador.',
        });
    });
});

//CLICK: IMPRIMIR EXCEL
$('#btnImprimirExcel').click(function () {
        //VALIDAR QUE NO ESTE SIENDO PROCESADO
        if (Procesando == true) {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: '¡Advertencia!',
                message: 'Continua siendo procesado, por favor espere...',
            });
        }
        else if (ProcesadoCompleto == false) {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: '¡Advertencia!',
                message: 'Debe iniciar el proceso antes de imprimir la proyección...',
            });
        }
        else if (ListaImpuestoVecinal.length == 0) {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: '¡Advertencia!',
                message: 'Debe iniciar el proceso antes de imprimir la proyección...',
            });
        }
        else {
            //DECLARACIÓN DE UNA LISTA LOCAL PARA FORMATEAR LA DATA
            var Lista_Local = FormatearData(ListaImpuestoVecinal);
            //GENERAR EXCEL
            ImprimirExcel(Lista_Local);
        }
});

//CLICK: GENERAR INSERCIÓN
$("#btnGenerarImpuestoVecinal").click(function () {  
        //VALIDAR QUE NO ESTE SIENDO PROCESADO
        if (Procesando == true) {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: '¡Advertencia!',
                message: 'Continua siendo procesado, por favor espere...',
            });
        }
        else if (ProcesadoCompleto == false) {
            //LANZAR EL MENSAJE DE ERROR
            iziToast.warning({
                title: '¡Advertencia!',
                message: 'Debe iniciar el proceso de proyección antes de generar...',
            });
        }
        else {
            //MOSTRAR EL SPINNER
            SpinnerElement.html(spinner());
            //REALIZAR LA PETICION DE LA DATA
            $.ajax({
                url: "/PlanillaImpuestoVecinal/GenerarPlanillaImpV",
                method: "POST"
            }).done(function (data) {
                if (data == "error") {
                    //DESBLOQUEAR EL BOTON
                    $("#btnGenerarCesantia").attr("disabled", false);
                    //MOSTRAR MENSAJE DE ERROR
                    iziToast.error({
                        title: 'Error',
                        message: 'Ocurrio un error al crear la proyección, contacte al administrador.',
                    });
                }
                else if (data == "bien") {
                    //LIMPIAR EL DATATABLE
                    $('#TablaPagoDeCesantiaDetalle').DataTable().clear();
                    //DECLARACIÓN DE UNA LISTA LOCAL PARA FORMATEAR LA DATA
                    var Lista_Local = FormatearData(ListaImpuestoVecinal);
                    //LLAMAR FUNCION DE IMPRIMIR EXCEL
                    ImprimirExcel(Lista_Local);
                    //MENSAJE EN CASO DE ÉXITO
                    iziToast.success({
                        title: 'Éxito',
                        message: 'La proyección se creo de forma exitosa!',
                    });
                    //SetTimeOut
                    setTimeout(function () {
                        window.location.href = '/PlanillaImpuestoVecinal/Index';
                    }, 3500);
                }
                //OCULTAR EL SPINNER
                SpinnerElement.empty();

            }).fail(function () {
                //OCULTAR SPINNER 
                SpinnerElement.empty();
                //LANZAR MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: 'Ocurrio un error al conectar con el servidor, contacte al administrador.',
                });
            });
        }    
});
