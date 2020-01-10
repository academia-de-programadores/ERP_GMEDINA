//VARIABLE ENCARGADA DE VALIDAR EL REGISTRO DE LA LIQUIDACIÓN
var Registrar = false;

//Funcion generica para reutilizar AJAX
function _ajax(params, uri, type, callback, enviar) {
    $.ajax({
        url: uri,
        type: type,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(params),
        beforeSend: function() {
            enviar();
        },
        success: function(data) {
            callback(data);
        }
    });
}

//#region Variables Globales
var fecha = new Date();
// Inputs divs y ddl
const inputFechaFin = $('#fechaFin'),
	ddlEmpleados = $('#cmbxEmpleados'),
    ddlMotivos = $('#cmbxMotivos'),
	validacionSelectFechaFin = $('#validacionSelectFechaFin'),
	validacionSelectEmpleado = $('#validacionSelectEmpleado'),
    validacionSelectMotivo = $('#validacionSelectMotivo'),
	cargarSpinnerDatosColaborador = $('#cargarSpinnerDatosColaborador'),
	cargarSpinnerSalarios = $('#cargarSpinnerSalarios'),
	divSalarios = $('#Salarios'),
    divConceptosAdicionales = $('#ConceptosAdicionales'),
	divDatosColaborador = $('#datosColaborador'),
	// Datos del empleado
	spanDiasLaborados = $('#spanDiasLaborados'),
	spanMesesLaborados = $('#spanMesesLaborados'),
	spanAniosLaborados = $('#spanAniosLaborados'),
	spanNombreEmpleado = $('#spanNombreEmpleado'),
	spanApellidoEmpleado = $('#spanApellidoEmpleado'),
	spanEdadEmpleado = $('#spanEdadEmpleado'),
	spanSexoEmpleado = $('#spanSexoEmpleado'),
	spanDepartamentoEmpleado = $('#spanDepartamentoEmpleado'),
	spanIdentidadEmpleado = $('#spanIdentidadEmpleado'),
	spanSueldoEmpleado = $('#spanSueldoEmpleado'),
	spanCargoEmpleado = $('#spanCargoEmpleado'),
	spanFechaIngresoEmpleado = $('#spanFechaIngresoEmpleado'),
	// Salarios
	spanSalarioOrdinarioMensual = $('#spanSalario'),
	spanSalarioPromedioMensual = $('#spanSalarioOrdinarioDiario'),
	spanSalarioOrdinarioDiario = $('#spanSalarioOrdinarioPromedioDiario'),
	spanSalarioPromedioDiario = $('#spanSalarioPromedioDiario'),
    //Conceptos de Liquidacion
    spanPreaviso = $('#spanPreaviso'),
    spanCesantia = $('#spanCesantia'),
    spanDecimotercer = $('#spanDecimotercer'),
    spanDecimocuarto = $('#spanDecimocuarto'),
    spanVacaciones = $('#spanVacaciones'),
    spanTotalLiquidacion = $('#MontoTotalLiquidacion');
//const  = $('#');
//#endregion

//Mostrar el spinner
function spinner() {
    return `<div class="sk-spinner sk-spinner-wave">
                <div class="sk-rect1"></div>
                <div class="sk-rect2"></div>
                <div class="sk-rect3"></div>
                <div class="sk-rect4"></div>
                <div class="sk-rect5"></div>
             </div>`;
}

//DETECTAR LOS CAMBIOS DEL DDL EMPLEADOS PARA LA EJECUCION
$(ddlEmpleados).change(() => {
    vaciarConceptosAdicionales();
    const ddlEmpleadosLleno = ddlEmpleados.val() != '';
    if (ddlEmpleadosLleno) {
        validacionSelectEmpleado.hide();
        GetDatosColaborador();
    }
});

//VARIABLE DE CONTENCION DE DOBLE EJECUCION PARA EL INPUT FECHA
var InputFechaBool = true;

//DETECTAR LOS CAMBIOS DE INPUT DE FECHA PARA LA EJECUCION
$(inputFechaFin).change(() => {
    vaciarConceptosAdicionales();
    if (inputFechaFinLlena()) {
        validacionSelectFechaFin.hide();
        //SET: ALTERNAR EL ESTADO DE InputFechaBool PARA QUE NO VAYA DOS VECES AL SERVIDOR
        InputFechaBool = (InputFechaBool) ? false : true;
        if(!InputFechaBool){
            GetDatosColaborador();
        }
    }
});

//DETECTAR LOS CAMBIOS DE INPUT DE MOTIVOS PARA LA EJECUCION
$(ddlMotivos).change(() => {
    vaciarConceptosAdicionales();
    if (inputMotivosLleno()) {
        validacionSelectMotivo.hide();
        GetDatosColaborador();
    }
});

//DEVUELVE TRUE EN CASO QUE LA FECHA ESTE LLENA
function inputFechaFinLlena() {
    return inputFechaFin.val() != '';
}

//DEVUELVE TRUE EN CASO QUE EL MOTIVO ESTE LLENO
function inputMotivosLleno() {
    return ddlMotivos.val() >= 1;
}

//Ejecutar peticion de datos del colaborador
function GetDatosColaborador()
{
    //Captura del Input de fecha
    const fechaFinVal = inputFechaFin.val();
    //Captura del Input del DDL de Empleados
    const ddlEmpleadosVal = ddlEmpleados.val();
    //Captura del Input del DDL de Motivos
    const ddlMotivosVal = ddlMotivos.val();
    //Validar que los campos no esten vacios
    if (ddlEmpleados.val() != '' && inputFechaFinLlena() && ddlMotivosVal >= 1){
        if (validarCampos()) {
            Registrar = true;
            obtenerDatosEmpleados(ddlEmpleadosVal, fechaFinVal, ddlMotivosVal);
        }
        else {
            Registrar = false;
        }
    }
}

$(document).ready(function() {
    validacionSelectEmpleado.val('');
    $('#datepicker .input-group.date')
		.datepicker({
		    todayBtn: 'linked',
		    keyboardNavigation: false,
		    forceParse: false,
		    calendarWeeks: true,
		    autoclose: true,
		    format: 'yyyy/mm/dd'
		})
		.on('changeDate', function(e) {
		    //if (validarCampos()) {
		    //    const fechaFinVal = inputFechaFin.val();
		    //    const ddlEmpleadosVal = ddlEmpleados.val();
		    //    const ddlMotivosVal = ddlMotivos.val();
		    //    GetDatosColaborador();
		    //}
		});

    //Llengar DDL Areas con Empleados
    _ajax(
		null,
		'/Liquidacion/GetEmpleadosAreas',
		'GET',
		(data) => {
		    $('#cmbxEmpleados').select2({
		        placeholder: 'Seleccione un empleado',
		        allowClear: true,
		        language: {
		            noResults: function() {
		                return 'Resultados no encontrados.';
		            },
		            searching: function() {
		                return 'Buscando...';
		            }
		        },
		        data: data.results
		    });
		},
		() => {}
	);

    //LLENAR EL DDL DE MOTIVOS 
    $.ajax({
        url: "/Liquidacion/GetMotivoLiquidacion",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        $("#cmbxMotivos").empty();
        $("#cmbxMotivos").append("<option value='0'>Selecione un motivo...</option>");
        $.each(data, function (i, iter) {
            $("#cmbxMotivos").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
        });
    });
});


function obtenerDatosEmpleados(idEmpleado, fechaFin, IdMotivo) {
    _ajax(
		{
		    idEmpleado: idEmpleado,
		    fechaFin: fechaFin,
		    IdMotivo : IdMotivo
		},
		'/Liquidacion/Obtener_Informacion_Empleado',
		'POST',
		(data) => {
		    console.log(data);
		    mostrarDatosColaborador(
				data.consulta[0].nombreEmpleado,
				data.consulta[0].apellidoEmpleado,
				data.consulta[0].numeroIdentidad,
				data.consulta[0].sexoEmpleado,
				data.consulta[0].edadEmpleado,
				data.consulta[0].cantidadSueldo,
				data.consulta[0].descripcionCargo,
				data.consulta[0].descripcionDepartamento,
				data.consulta[0].descripcionMoneda,
				data.consulta[0].fechaIngreso,
				data.anios,
				data.meses,
				data.dias,
				data.salarios.SalarioOrdinarioMensual,
				data.salarios.SalarioPromedioMensual,
				data.salarios.SalarioOrdinarioDiario,
				data.salarios.SalarioPromedioDiario,
                data.Preaviso,
                data.Cesantia,
				data.DecimoTercer,
				data.DecimoCuarto,
				data.Vacaciones,
                data.Total_Liquidacion
			);
		    SaveHiddenFor(
                data.salarios.SalarioOrdinarioMensual,
				data.salarios.SalarioPromedioMensual,
				data.salarios.SalarioOrdinarioDiario,
				data.salarios.SalarioPromedioDiario,
                data.Preaviso,
                data.Cesantia,
				data.DecimoTercer,
				data.DecimoCuarto,
				data.Vacaciones,
                data.Total_Liquidacion);
		    cargarSpinnerDatosColaborador.html('');
		    cargarSpinnerDatosColaborador.hide();
		    divDatosColaborador.show();
		    cargarSpinnerSalarios.html('');
		    cargarSpinnerSalarios.hide();
		    divSalarios.show();
		    divConceptosAdicionales.show();
		},
		() => {
		    divDatosColaborador.hide();
		    cargarSpinnerDatosColaborador.html(spinner());
		    cargarSpinnerDatosColaborador.show();
		    divSalarios.hide();
		    cargarSpinnerSalarios.html(spinner());
		    cargarSpinnerSalarios.show();
		}
	);
}


function validarCampos() {
    var todoBien = true;
    //Validar que el drop down list tenga seleccionado un empleado.
    if (ddlEmpleados.val() <= 0) {
        todoBien = false;
        validacionSelectEmpleado.show();
    }
    //Validar que no este vacio el campo de fecha de despido.
    if (inputFechaFin.val() == '') {
        todoBien = false;
        validacionSelectFechaFin.show();
    }
    //Validar que no este vacio el campo de Motivo.
    if (ddlMotivos.val() <= 0) {
        todoBien = false;
        validacionSelectMotivo.show();
    }
    return todoBien;
}

function mostrarDatosColaborador(
	nombreEmpleado = '',
	apellidoEmpleado = '',
	numeroIdentidad = '',
	sexoEmpleado = '',
	edadEmpleado = '',
	cantidadSueldo = '',
	descripcionCargo = '',
	descripcionDepartamento = '',
	descripcionMoneda = '',
	fechaIngreso = '',
	Anios = '',
	Meses = '',
	Dias = '',
	SalarioOrdinarioMensual = '',
	SalarioPromedioMensual = '',
	SalarioOrdinarioDiario = '',
	SalarioPromedioDiario = '',
    Preaviso = '',
    Cesantia = '',
    Decimotercer = '',
    Decimocuarto = '',
    Vacaciones = '', 
    Total_Liquidacion = ''
) {
	spanDiasLaborados.html(Dias);
    spanMesesLaborados.html(Meses);
    spanAniosLaborados.html(Anios);
    spanNombreEmpleado.html(nombreEmpleado);
    spanApellidoEmpleado.html(apellidoEmpleado);
    spanEdadEmpleado.html(edadEmpleado);
    spanSexoEmpleado.html(sexoEmpleado);
    spanDepartamentoEmpleado.html(descripcionDepartamento);
    spanIdentidadEmpleado.html(numeroIdentidad);
    spanSueldoEmpleado.html(cantidadSueldo + ' ' + descripcionMoneda);
    spanCargoEmpleado.html(descripcionCargo);
    spanFechaIngresoEmpleado.html(fechaIngreso);
    spanSalarioOrdinarioMensual.html(SalarioOrdinarioMensual);
    spanSalarioPromedioMensual.html(SalarioPromedioMensual);
    spanSalarioOrdinarioDiario.html(SalarioOrdinarioDiario);
    spanSalarioPromedioDiario.html(SalarioPromedioDiario);
    spanPreaviso.html((Preaviso == 0) ? 0.00 : Preaviso);
    spanCesantia.html((Cesantia == 0) ? 0.00 : Cesantia);
    spanDecimotercer.html((Decimotercer == 0) ? 0.00 : Decimotercer);
    spanDecimocuarto.html((Decimocuarto == 0) ? 0.00 : Decimocuarto);
    spanVacaciones.html((Vacaciones == 0) ? 0.00 : Vacaciones);
    spanTotalLiquidacion.val((Total_Liquidacion == 0) ? 0.00 : Total_Liquidacion);
}    

function SaveHiddenFor(SalarioOrdinarioMensual_Liq, SalarioPromedioMensual_Liq,
                       SalarioOrdinarioDiario_Liq, SalarioPromedioDiario_Liq,
                       Preaviso_Liq, Cesantia_Liq,
                       DecimoTercerMesProporcional_Liq,DecimoCuartoMesProporcional_Liq,
                       VacacionesPendientes_Liq)
{
    $("#SalarioOrdinarioMensual_Liq").val(SalarioOrdinarioMensual_Liq);
    $("#SalarioPromedioMensual_Liq").val(SalarioPromedioMensual_Liq);
    $("#SalarioOrdinarioDiario_Liq").val(SalarioOrdinarioDiario_Liq);
    $("#SalarioPromedioDiario_Liq").val(SalarioPromedioDiario_Liq);
    $("#Preaviso_Liq").val(Preaviso_Liq);
    $("#Cesantia_Liq").val(Cesantia_Liq);
    $("#DecimoTercerMesProporcional_Liq").val(DecimoTercerMesProporcional_Liq);
    $("#DecimoCuartoMesProporcional_Liq").val(DecimoCuartoMesProporcional_Liq);
    $("#VacacionesPendientes_Liq").val(VacacionesPendientes_Liq);
}

$("#btnConceptosAdicionales").click(function(){
    var data = $("#frmConceptosAdicionales").serializeArray();
    var VacacionesPendientes = $("#SalariosAdeudados").val();
    var Form = [];
    Form = {
        SalariosAdeudados : $("#SalariosAdeudados").val(),
        OtrosPagos : $("#OtrosPagos").val(),
        PagoHEPendiente : $("#PagoHEPendiente").val(),
        ValorBonoEducativo : $("#ValorBonoEducativo").val(),
        PagoSeptimoDia : $("#PagoSeptimoDia").val(),
        BonoPorVacaciones : $("#BonoPorVacaciones").val(),
        ReajusteSalarial : $("#ReajusteSalarial").val(),
        DecimoTercerMesAdeudado : $("#DecimoTercerMesAdeudado").val(),
        DecimoCuartoMesAdeudado : $("#DecimoCuartoMesAdeudado").val(),
        BonificacionVacaciones : $("#BonificacionVacaciones").val(),
        PagoPorEmbarazo : $("#PagoPorEmbarazo").val(),
        PagoPorLactancia : $("#PagoPorLactancia").val(),
        PrePosNatal : $("#PrePosNatal").val(),
        PagoPorDiasFeriado : $("#PagoPorDiasFeriado").val()
    };
    console.log(Form);
    console.log(data);
    $.ajax({
        url: "Liquidacion/CalcularLiquidacion",
        type: "POST",
        dataType:"json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(Form)
    }).done(function (data){
        console.log(data);
        $("#MontoTotalLiquidacion").val(data.MontoTotalLiquidacion);
    });
})

//VACIAR LOS CONCEPTOS AGREGADOS
function vaciarConceptosAdicionales()
{
    $("#SalariosAdeudados").val('');
    $("#OtrosPagos").val('');
    $("#PagoHEPendiente").val('');
    $("#ValorBonoEducativo").val('');
    $("#PagoSeptimoDia").val('');
    $("#BonoPorVacaciones").val('');
    $("#ReajusteSalarial").val('');
    $("#DecimoTercerMesAdeudado").val('');
    $("#DecimoCuartoMesAdeudado").val('');
    $("#BonificacionVacaciones").val('');
    $("#PagoPorEmbarazo").val('');
    $("#PagoPorLactancia").val('');
    $("#PrePosNatal").val('');
    $("#PagoPorDiasFeriado").val('');
    $("#MontoTotalLiquidacion").val('');
}

//REALIZAR INSERCION

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#RegistrarLiquidacion").click(function () {
    var Registrar_Verificar = (Registrar)? 1 : 0;
    console.log(Registrar);
    Registrar = false;
    if(Registrar_Verificar == 1){
        //REALIZAR LA PETICIÓN PARA LA INSERCION
        $.ajax({
            url: "Liquidacion/RegistrarLiquidacion",
            type: "GET",
            dataType:"json",
            contentType: 'application/json; charset=utf-8'
        }).done(function (data){
            console.log(data);
            if(data == "error")
            {
                iziToast.error({
                    title: 'Error',
                    message: 'Ocurrio un error al registrar la liquidación',
                });
            }
            else{
                iziToast.success({
                    title: 'Éxito',
                    message: 'Se ha registrado la liquidación',
                });
            }
        }).fail(function (data){
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al registrar la liquidación',
            });
        });;
    }
    else{
        if(!validarCampos())
        {
            //Mensaje de error si no hay data
            iziToast.error({
                title: 'Error',
                message: 'Debe cargar los datos de un colaborador para registrar la liquidación.',
            });
        }
    }  
});

//VALIDAR LAS ENTRADAS DE LOS CONCEPTOS AGREGADOS
$('.ValidarCaracteres').bind('keypress', function (event) { 
    console.log("hola");
    //var regex = new RegExp("^[a-zA-Z0-9]+$"); 
    var regex = new RegExp("^[0-9]?[.]"); 
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode); 
    if (!regex.test(key)) 
    { 
        event.preventDefault(); 
        return false; 
    } 
});

