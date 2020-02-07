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
        beforeSend: function () {
            enviar();
        },
        success: function (data) {
            callback(data);
        }
    });
}

//#region Variables Globales
// Inputs divs y ddl
const cargarSpinnerDatosColaborador = $('#cargarSpinnerDatosColaborador'),
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



//VALIDAR EL FORMULARIO
function validarCampos(colaborador, motivo, fecha, BoolConceptAddValidate) {
    //VARIABLE PARA VALIDAR EL ESTADO DEL MODELO
    var ModelStateForm = true;

    if (colaborador != "-1") {

        if (colaborador <= 0 || isNaN(colaborador || colaborador == "0")) {

            //SETEAR EL ESTADO DEL MODELO
            ModelStateForm = false;

            //MOSTRAR VALIDACIONES
            $('#validacionSelectEmpleado').show();

        } else {
            //OCULTAR VALIDACIONES
            $('#validacionSelectEmpleado').hide();
        }
    }

    if (motivo != "-1") {

        if (motivo <= 0 || isNaN(motivo) || motivo == "0") {

            //SETEAR EL ESTADO DEL MODELO
            ModelStateForm = false;

            //MOSTRAR VALIDACIONES
            $('#validacionSelectMotivo').show();

        } else {
            //OCULTAR VALIDACIONES
            $('#validacionSelectMotivo').hide();
        }

    }

    if (fecha != "-1") {

        if (fecha == "" || fecha == null || fecha == undefined) {
            //SETEAR EL ESTADO DEL MODELO
            ModelStateForm = false;

            //MOSTRAR VALIDACIONES
            $('#validacionSelectFechaFin').show();

        } else {
            //OCULTAR VALIDACIONES
            $('#validacionSelectFechaFin').hide();
        }

    }

    //SI SE LLAMA LA FUNCION DE VALIDAR CANPOS DESDE EL CLICK DE SUMAR ADICIONALES NO IR AL SERVIDOR
    if (BoolConceptAddValidate == false) {
        //VALIDAR QUE EL MODELO SEA VALIDO PARA EJECUTAR LA PETICIÓN AL SERVIDOR
        if (ModelStateForm) {

            //OBTENER EL ID DEL COLABORADOR SELECCIONADO
            var ddlEmpleadosVal = $("#cmbxEmpleados").val();
            //OBTENER EL ID DEL MOTIVO SELECCIONADO
            var ddlMotivosVal = $("#cmbxMotivos").val();
            //OBTENER EL VALOR DE LA FECHA
            var fechaFinVal = $("#fechaFin").val();

            //VALIDAR QUE EL DDL DE EMPLEADO ESTE INICIALIZADO
            if (ddlEmpleadosVal == null || isNaN(ddlEmpleadosVal) || ddlEmpleadosVal == 0 || ddlEmpleadosVal == "0")
                ModelStateForm = false;

            //VALIDAR QUE EL DDL DE MOTIVO ESTE INICIALIZADO
            if (ddlMotivosVal == null || isNaN(ddlMotivosVal) || ddlMotivosVal == 0 || ddlMotivosVal == "0")
                ModelStateForm = false;

            //VALIDAR QUE EL DATEPICKER DE FECHA ESTE INICIALIZADO
            if (fechaFinVal == null || fechaFinVal == "")
                ModelStateForm = false;

            if (ModelStateForm)
                obtenerDatosEmpleados(ddlEmpleadosVal, fechaFinVal, ddlMotivosVal);  //LLAMAR LA FUNCION DE OBTENER DATOS DE LOS EMPLEADOS

        }
    }

    return ModelStateForm;
}

$(document).ready(function () {
    $('#datepicker .input-group.date')
        .datepicker({
            todayBtn: 'linked',
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'yyyy/mm/dd'
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
                    noResults: function () {
                        return 'Resultados no encontrados.';
                    },
                    searching: function () {
                        return 'Buscando...';
                    }
                },
                data: data.results
            });
        },
        () => {

        }
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
    //VACIAR CONCEPTOS ADICIONALES 
    vaciarConceptosAdicionales();
    _ajax(
        {
            idEmpleado: idEmpleado,
            fechaFin: fechaFin,
            IdMotivo: IdMotivo
        },
        '/Liquidacion/Obtener_Informacion_Empleado',
        'POST',
        (data) => {
            mostrarDatosColaborador(
                data.consulta[0].nombreEmpleado,
                data.consulta[0].apellidoEmpleado,
                data.consulta[0].NúmeroIdentidad,
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

//EJECUTAR FUNCION DE OBTENER METODOS DEL COLABORADOR
function mostrarDatosColaborador(
    nombreEmpleado,
    apellidoEmpleado,
    NúmeroIdentidad,
    sexoEmpleado,
    edadEmpleado,
    cantidadSueldo,
    descripcionCargo,
    descripcionDepartamento,
    descripcionMoneda,
    fechaIngreso,
    Anios,
    Meses,
    Dias,
    SalarioOrdinarioMensual,
    SalarioPromedioMensual,
    SalarioOrdinarioDiario,
    SalarioPromedioDiario,
    Preaviso,
    Cesantia,
    Decimotercer,
    Decimocuarto,
    Vacaciones,
    Total_Liquidacion
) {
    //LLENAR CONCEPTOS ADICIONALES
    spanDiasLaborados.html(Dias);
    spanMesesLaborados.html(Meses);
    spanAniosLaborados.html(Anios);
    spanNombreEmpleado.html(nombreEmpleado);
    spanApellidoEmpleado.html(apellidoEmpleado);
    spanEdadEmpleado.html(edadEmpleado);
    spanSexoEmpleado.html(sexoEmpleado);
    spanDepartamentoEmpleado.html(descripcionDepartamento);
    spanIdentidadEmpleado.html(NúmeroIdentidad);
    spanSueldoEmpleado.html(((cantidadSueldo == 0) ? "0.00" : (cantidadSueldo % 1 == 0) ? cantidadSueldo + ".00" : cantidadSueldo) + ' ' + descripcionMoneda);
    spanCargoEmpleado.html(descripcionCargo);
    spanFechaIngresoEmpleado.html(fechaIngreso);
    spanSalarioOrdinarioMensual.html((SalarioOrdinarioMensual == 0) ? "0.00" : (SalarioOrdinarioMensual % 1 == 0) ? SalarioOrdinarioMensual + ".00" : SalarioOrdinarioMensual);
    spanSalarioPromedioMensual.html((SalarioPromedioMensual == 0) ? "0.00" : (SalarioPromedioMensual % 1 == 0) ? SalarioPromedioMensual + ".00" : SalarioPromedioMensual);
    spanSalarioOrdinarioDiario.html((SalarioOrdinarioDiario == 0) ? "0.00" : (SalarioOrdinarioDiario % 1 == 0) ? SalarioOrdinarioDiario + ".00" : SalarioOrdinarioDiario);
    spanSalarioPromedioDiario.html((SalarioPromedioDiario == 0) ? "0.00" : (SalarioPromedioDiario % 1 == 0) ? SalarioPromedioDiario + ".00" : SalarioPromedioDiario);
    spanPreaviso.html((Preaviso == 0) ? "0.00" : (Preaviso % 1 == 0) ? Preaviso + ".00" : Preaviso);
    spanCesantia.html((Cesantia == 0) ? "0.00" : (Cesantia % 1 == 0) ? Cesantia + ".00" : Cesantia);
    spanDecimotercer.html((Decimotercer == 0) ? "0.00" : (Decimotercer % 1 == 0) ? Decimotercer + ".00" : Decimotercer);
    spanDecimocuarto.html((Decimocuarto == 0) ? "0.00" : (Decimocuarto % 1 == 0) ? Decimocuarto + ".00" : Decimocuarto);
    spanVacaciones.html((Vacaciones == 0) ? "0.00" : (Vacaciones % 1 == 0) ? Vacaciones + ".00" : Vacaciones);
    spanTotalLiquidacion.val((Total_Liquidacion == 0) ? "0.00" : (Total_Liquidacion % 1 == 0) ? Total_Liquidacion + ".00" : Total_Liquidacion);
}

function SaveHiddenFor(SalarioOrdinarioMensual_Liq, SalarioPromedioMensual_Liq,
    SalarioOrdinarioDiario_Liq, SalarioPromedioDiario_Liq,
    Preaviso_Liq, Cesantia_Liq,
    DecimoTercerMesProporcional_Liq, DecimoCuartoMesProporcional_Liq,
    VacacionesPendientes_Liq) {
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


//SUMAR CONCEPTOS ADICIONALES
$("#btnConceptosAdicionales").click(function () {

    //OBTENER EL ID DEL COLABORADOR SELECCIONADO
    var ddlEmpleadosVal = $("#cmbxEmpleados").val();
    //OBTENER EL ID DEL MOTIVO SELECCIONADO
    var ddlMotivosVal = $("#cmbxMotivos").val();
    //OBTENER EL VALOR DE LA FECHA
    var fechaFinVal = $("#fechaFin").val();


    if (!validarCampos(ddlEmpleadosVal, ddlMotivosVal, fechaFinVal, true)) {
        //Mensaje de error si no hay data
        iziToast.error({
            title: 'Error',
            message: 'Debe cargar los datos de un colaborador para sumar conceptos adicionales.',
        });
    }
    else {
        var Form = {
            SalariosAdeudados: $("#SalariosAdeudados").val().replace(/,/g, ""),
            OtrosPagos: $("#OtrosPagos").val().replace(/,/g, ""),
            PagoHEPendiente: $("#PagoHEPendiente").val().replace(/,/g, ""),
            ValorBonoEducativo: $("#ValorBonoEducativo").val().replace(/,/g, ""),
            PagoSeptimoDia: $("#PagoSeptimoDia").val().replace(/,/g, ""),
            BonoPorVacaciones: $("#BonoPorVacaciones").val().replace(/,/g, ""),
            ReajusteSalarial: $("#ReajusteSalarial").val().replace(/,/g, ""),
            DecimoTercerMesAdeudado: $("#DecimoTercerMesAdeudado").val().replace(/,/g, ""),
            DecimoCuartoMesAdeudado: $("#DecimoCuartoMesAdeudado").val().replace(/,/g, ""),
            BonificacionVacaciones: $("#BonificacionVacaciones").val().replace(/,/g, ""),
            PagoPorEmbarazo: $("#PagoPorEmbarazo").val().replace(/,/g, ""),
            PagoPorLactancia: $("#PagoPorLactancia").val().replace(/,/g, ""),
            PrePosNatal: $("#PrePosNatal").val().replace(/,/g, ""),
            PagoPorDiasFeriado: $("#PagoPorDiasFeriado").val().replace(/,/g, "")
        };
        $.ajax({
            url: "/Liquidacion/CalcularLiquidacion",
            type: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(Form)
        }).done(function (data) {
            if (data != "error") {
                //EN CASO DE EXITO SUMAR EL MONTO TOTAL DE LIQUIDACIÓN
                $("#MontoTotalLiquidacion").val(data.MontoTotalLiquidacion);
                //SETEAR LA VARIABLE DE REGISTRO PARA INDICAR QUE ESTA LISTO PARA REGISTRAR
                Registrar = true;
            }
            else {
                //MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: 'Ocurrio un error al sumar los conceptos adicionales, verifique los datos ingresados.',
                });
            }
        }).fail(function (jqxhr, settings, exception) {
            //MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al sumar los conceptos adicionales, verifique los datos ingresados.',
            });
        });
    }
})


//VACIAR LOS CONCEPTOS AGREGADOS
function vaciarConceptosAdicionales() {

        //OCULTAR CONCEPTOS ADICIONALES
    divConceptosAdicionales.hide();
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


//REGISTRAR LA LIQUIDACIÓN.
$("#RegistrarLiquidacion").click(function () {

    //BLOQUEAR EL BOTON 
    $("#RegistrarLiquidacion").attr("disabled", true);

    //OBTENER EL ID DEL COLABORADOR SELECCIONADO
    var ddlEmpleadosVal = $("#cmbxEmpleados").val();
    //OBTENER EL ID DEL MOTIVO SELECCIONADO
    var ddlMotivosVal = $("#cmbxMotivos").val();
    //OBTENER EL VALOR DE LA FECHA
    var fechaFinVal = $("#fechaFin").val();

    //VALIDAR QUE EL FORMULARIO ESTE LISTO PARA REGISTRAR
    if (validarCampos(ddlEmpleadosVal, ddlMotivosVal, fechaFinVal, true)) {
        //REALIZAR LA PETICIÓN PARA LA INSERCION
        $.ajax({
            url: "/Liquidacion/RegistrarLiquidacion",
            type: "GET",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data == "error") {
                //DESBLOQUEAR EL BOTON
                $("#RegistrarLiquidacion").attr("disabled", false);
                
                //MOSTRAR MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: 'Ocurrio un error al registrar la liquidación, contacte al administrador.',
                });
            }
            else {
                
                //MOSTRAR MENSAJE DE ÉXITO
                iziToast.success({
                    title: 'Éxito',
                    message: 'El registro se agregó de forma exitosa!',
                });
                //SetTimeOut
                setTimeout(function () {
                    location.reload();
                }, 4500);

            }
        }).fail(function (data) {
            
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'Ocurrio un error al registrar la liquidación, contacte al administrador.',
            });

            //DESBLOQUEAR EL BOTON
            $("#RegistrarLiquidacion").attr("disabled", false);
        });;
    }
});

//VALIDAR LAS ENTRADAS DE LOS CONCEPTOS AGREGADOS
$('.ValidarCaracteres').bind('keypress', function (event) {
    //var regex = new RegExp("^[a-zA-Z0-9]+$"); 
    var regex = new RegExp("^[0-9]");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});


//EVITAR EL POSTBACK DEL FORMULARIO 
$("#frmConceptosAdicionales").submit(function (e) {
    e.preventDefault();
});
