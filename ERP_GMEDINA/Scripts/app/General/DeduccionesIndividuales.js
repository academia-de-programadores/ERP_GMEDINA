//#region Region Obtención de Script para Formateo de Fechas
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });
//#endregion
var inactivarID = 0;

//TODO: Validar Cuotas
//var campo

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

    $.ajax({
        url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $('#Crear #emp_IdCreate').select2({
            dropdownParent: $('#Crear'),
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

        $('#Editar #emp_Id')
            .select2({
                dropdownParent: $('#Editar'),
                placeholder: 'Seleccione un empleado',
                allowClear: true,
                debug: true,
                cache: false,
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
    });

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
})

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/DeduccionesIndividuales/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaDeduccionIndividual = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#IndexTabla').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeduccionIndividual.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeduccionIndividual[i].dei_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleDeduccionesIndividuales" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaDeduccionIndividual[i].dei_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarDeduccionesIndividuales" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionIndividual[i].dei_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnActivarDeduccionesIndividuales" deiid="' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#IndexTabla').dataTable().fnAddData([
                    ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales,
                    ListaDeduccionIndividual[i].dei_Motivo,
                    ListaDeduccionIndividual[i].per_Nombres + ' ' + ListaDeduccionIndividual[i].per_Apellidos,
                    ListaDeduccionIndividual[i].dei_Monto,
                    ListaDeduccionIndividual[i].dei_NumeroCuotas,
                    ListaDeduccionIndividual[i].dei_MontoCuota,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
}

//#region  Activar
$(document).on("click", "#IndexTabla tbody tr td #btnActivarDeduccionesIndividuales", function () {
    document.getElementById("btnActivarRegistroDeduccionIndividual").disabled = false;
    var id = $(this).data('id');

    var id = $(this).attr('deiid');
    localStorage.setItem('id', id);
    //Mostrar el Modal
    $("#ActivarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$("#btnActivarRegistroDeduccionIndividual").click(function () {
    document.getElementById("btnActivarRegistroDeduccionIndividual").disabled = true;
    let id = localStorage.getItem('id')

    $.ajax({
        url: "/DeduccionesIndividuales/Activar",
        method: "POST",
        data: { id: id }
    }).done(function (data) {
        $("#ActivarDeduccionesIndividuales").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });

});
//#endregion

//#region blur

$('#Crear #dei_Motivo, #Editar #dei_Motivo').blur(function () {
    if (
        $(this)
            .val()
            .trim() == ''
    ) {
        $("#Crear #valMotivo, #Editar #valMotivo").css("display", "");
        $("#Crear #astMotivo, #Editar #astMotivo").css("color", "red");

    } else {
        $("#Crear #valMotivo, #Editar #valMotivo").css("display", "none");
        $("#Crear #astMotivo, #Editar #astMotivo").css("color", "black");
    }
});

$('#Crear #emp_Id, #Editar #emp_Id').blur(function () {
    let emp_Id = $(this).val();
    if (emp_Id == "" || emp_Id == 0 || emp_Id == "0") {
        $("#Crear #valEmpId, #Editar #valEmpId").css("display", "");
        $("#Crear #astEmpId, #Editar #astEmpId").css("color", "red");

    } else {
        $("#Crear #valEmpId, #Editar #valEmpId").css("display", "none");
        $("#Crear #astEmpId, #Editar #astEmpId").css("color", "black");
    }
});

$('#Crear #dei_Monto, #Editar #dei_Monto').blur(function () {
    let dei_Monto = $(this).val();
    let hayAlgo = false;
    if (dei_Monto == "" || dei_Monto == null || dei_Monto == undefined) {
        $("#Crear #valMontoRequerido, #Editar #valMontoRequerido").html('Campo Monto Requerido');
        $("#Crear #valMontoRequerido, #Editar #valMontoRequerido").css("display", "");
        $("#Crear #astMonto, #Editar #astMonto").css("color", "red");

    } else {
        hayAlgo = true;
        $("#Crear #valMontoRequerido, #Editar #valMontoRequerido").css("display", "none");
        $("#Crear #astMonto, #Editar #astMonto").css("color", "black");
    }

    if (hayAlgo)
        if (dei_Monto == 0.00 || dei_Monto < 0) {
            $("#Crear #valMonto, #Editar #valMonto").html('El campo Monto inicial no puede ser menor o igual que cero');
            $("#Crear #valMonto, #Editar #valMonto").css("display", "block");
            $("#Crear #valMonto, #Editar #valMonto").css("color", "red");
            estaBien = false;
        }
        else {
            $("#Crear #valMonto, #Editar #valMonto").css("display", "none");
            $("#Crear #valMonto, #Editar #valMonto").css("color", "black");
        }
});
//Numero Cuotas Crear
$('#Crear #dei_NumeroCuotas').blur(function () {
    let dei_NumeroCuotas = $(this).val().replace(/,/g, "");
    let hayAlgo = false;

    if (dei_NumeroCuotas == "" || dei_NumeroCuotas == null || dei_NumeroCuotas == undefined) {
        $("#Crear #valNumeroCuotasRequerido").html('Campo # Cuotas Requerido');
        $("#Crear #valNumeroCuotasRequerido").css("display", "");
        $("#Crear #astNumeroCuotas").css("color", "red");
    } else {
        hayAlgo = true
        $("#Crear #valNumeroCuotasRequerido").css("display", "none");
        $("#Crear #astNumeroCuotas").css("color", "black");
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (dei_NumeroCuotas == 0 || dei_NumeroCuotas < 0) {
            $("#Crear #valNumeroCuotasteMayor").html('El campo # Cuotas no puede ser menor o igual que cero.');
            $("#Crear #valNumeroCuotasMayor").css("display", "block");
            $("#Crear #astNumeroCuotas").css("color", "red");

        }
        else {
            esMayorCero = true;
            $("#Crear #valNumeroCuotasMayor").css("display", "none");
            $("#Crear #astNumeroCuotas").css("color", "black");
        }

        let mr = parseFloat(dei_NumeroCuotas).toFixed(2);

    }
});
//Numero Cuotas Editar
$('#Editar #dei_NumeroCuotas').blur(function () {
    let dei_NumeroCuotas = $(this).val().replace(/,/g, "");
    let hayAlgo = false

    if (dei_NumeroCuotas == "" || dei_NumeroCuotas == null || dei_NumeroCuotas == undefined) {
        $("#Editar #valNumeroCuotasRequerido").html('Campo # Cuotas Requerido');
        $("#Editar #valNumeroCuotasRequerido").css("display", "");
        $("#Editar #astNumeroCuotas").css("color", "red");
    } else {
        hayAlgo = true
        $("#Editar #valNumeroCuotasRequerido").css("display", "none");
        $("#Editar #astNumeroCuotas").css("color", "black");
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (dei_NumeroCuotas == 0 || dei_NumeroCuotas < 0) {
            $("#Editar #valNumeroCuotasMayor").html('El campo # Cuotas no puede ser menor o igual que cero.');
            $("#Editar #valNumeroCuotasMayor").css("display", "block");
            $("#Editar #astNumeroCuotas").css("color", "red");

        }
        else {
            esMayorCero = true;
            $("#Editar #valNumeroCuotasMayor").css("display", "none");
            $("#Editar #astNumeroCuotas").css("color", "black");
        }

        let mr = parseFloat(dei_NumeroCuotas).toFixed(2);

    }

});
//Cuota Crear
$('#Crear #dei_MontoCuota').blur(function () {
    let valor = $(this).val().replace(/,/g, "");
    let hayAlgo = false;


    if (valor == "" || valor == null || valor == undefined) {
        $("#Crear #valCuota").css("display", "");
        $("#Crear #astCuota").css("color", "red");

    } else {
        hayAlgo = true;
        $("#Crear #valMontoCuota").css("display", "none");
        $("#Crear #astMontoCuota").css("color", "black");
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (valor == 0 || valor < 0) {
            $("#Crear #valMontoCuota").html('Campo Monto Cuota no puede ser menor o igual que cero');
            $("#Crear #valMontoCuota").css("display", "");
            $("#Crear #astMontoCuota").css("color", "red");

        } else {
            esMayorCero = true;
            $("#Crear #valMontoCuota").css("display", "none");
            $("#Crear #astMontoCuota").css("color", "black");
        }

        let cuo = parseFloat(valor).toFixed(2);
    }
});
//Cuota Editar
$('#Editar #dei_MontoCuota').blur(function () {
    let valor = $(this).val().replace(/,/g, "");
    let hayAlgo = false;


    if (valor == "" || valor == null || valor == undefined) {
        $("#Editar #valMontoCuota").css("display", "");
        $("#Editar #astMontoCuota").css("color", "red");

    } else {
        hayAlgo = true;
        $("#Editar #valMontoCuota").css("display", "none");
        $("#Editar #astMontoCuota").css("color", "black");
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (valor == 0 || valor < 0) {
            $("#Editar #valMontoCuota").html('Campo Monto Cuota no puede ser menor o igual que cero');
            $("#Editar #valMontoCuota").css("display", "");
            $("#Editar #astMontoCuota").css("color", "red");

        } else {
            esMayorCero = true;
            $("#Editar #valMonotoCuota").css("display", "none");
            $("#Editar #astMontoCuota").css("color", "black");
        }

        let cuo = parseFloat(valor).toFixed(2);
    }
});
//#endregion

//#region Funciones

function limpiarAsteriscos(modal) {
    $("#" + modal + " #astMotivo").css("color", "black");
    $("#" + modal + " #astEmpId").css("color", "black");
    $("#" + modal + " #astMonto").css("color", "black");
    $("#" + modal + " #astNumeroCuotas").css("color", "black");
    $("#" + modal + " #astMontoCuota").css("color", "black");
}

function limpiarSpan(modal) {
    $("#" + modal + " #valMotivo").css("display", "none");
    $("#" + modal + " #valEmpId").css("display", "none");
    $("#" + modal + " #valMonto").css("display", "none");
    $("#" + modal + " #valMontoRequerido").css("display", "none");
    $("#" + modal + " #valNumeroCuotasRequerido").css("display", "none");
    $("#" + modal + " #valNumeroCuotasMayor").css("display", "none");
    $("#" + modal + " #valMontoCuota").css("display", "none");
    $("#" + modal + " #valMontoCuotaMayor").css("display", "none");
}

function estaTodoValidado(modal) {
    //#region Declaracion de variables
    var estaBien = true;
    let dei_Motivo = $("#" + modal + " #dei_Motivo").val();
    let dei_Monto = $("#" + modal + " #dei_Monto").val();
    let dei_NumeroCuotas = $("#" + modal + " #dei_NumeroCuotas").val();
    let dei_MontoCuota = $("#" + modal + " #dei_MontoCuota").val();
    let emp_Id = $("#" + modal + " #emp_Id").val();
    //#endregion

    //#region Validar Motivo 
    if (dei_Motivo == "" || dei_Motivo.trim() == "") {
        $("#" + modal + " #valMotivo").css("display", "");
        $("#" + modal + " #astMotivo").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + " #valMotivo").css("display", "none");
        $("#" + modal + " #astMotivo").css("color", "black");
    }
    //#endregion

    //#region Validar DDL Empleados
    if (emp_Id == "" || emp_Id == 0 || emp_Id == "0") {
        $("#" + modal + " #valEmpId").css("display", "");
        $("#" + modal + " #astEmpId").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + " #valEmpId").css("display", "none");
        $("#" + modal + " #astEmpId").css("color", "black");
    }
    //#endregion

    //#region Validar monto Inicial
    let hayAlgoEnMonto = false;
    if (dei_Monto == "") {
        $("#" + modal + " #valMontoRequerido").css("display", "block");
        $("#" + modal + " #astMonto").css("color", "red");
        estaBien = false;
    }
    else {
        hayAlgoEnMonto = true;
        $("#" + modal + " #valMontoRequerido").css("display", "none");
        $("#" + modal + " #astMonto").css("color", "black");
    }

    if (hayAlgoEnMonto)
        if (dei_Monto == 0.00 || dei_Monto < 0) {
            $("#" + modal + " #valMonto").css("display", "block");
            $("#" + modal + " #astMonto").css("color", "red");
            estaBien = false;
        }
        else {
            hayAlgoEnMonto = true;
            $("#" + modal + " #valMonto").css("display", "none");
            $("#" + modal + " #astMonto").css("color", "black");
        }
    //#endregion


    //#region Validar monto Restante
    let hayAlgoEnNumeroCuotas = false;
    if (dei_NumeroCuotas != 0) {
        hayAlgoEnNumeroCuotas = true;
        $("#" + modal + " #valNumeroCuotasRequerido").css("display", "none");
        $("#" + modal + " #astNumeroCuotas").css("color", "black");
    }
    else {
        $("#Crear #valNumeroCuotasRequerido").css("display", "");
        $("#astNumeroCuotas").css("color", "red");
        estaBien = false;
    }

    //Validar si monto restante es mayor que monto inicial
    //#endregion

    let hayAlgoMontoCuota = false;
    //#region Validar cuota
    if (dei_MontoCuota != "") {
        hayAlgoMontoCuota = true;
        $("#" + modal + " #valMontoCuota").css("display", "none");
        $("#" + modal + " #astMontoCuota").css("color", "black");
    }
    else {
        $("#" + modal + " #valMontoCuota").html('Campo Monto Cuota Requerido');
        $("#" + modal + " #valMontoCuota").css("display", "");
        $("#" + modal + " #astMontoCuota").css("color", "red");
        estaBien = false;
    }

    if (hayAlgoMontoCuota)
        if (!(dei_MontoCuota <= 0)) {
            $("#" + modal + " #valMontoCuota").css("display", "none");
            $("#" + modal + " #astMontoCuota").css("color", "black");
        }
        else {
            $("#" + modal + " #valMontoCuota").html('Campo Monto Cuota no puede ser menor o igual que cero');
            $("#" + modal + " #valMontoCuota").css("display", "");
            $("#" + modal + " #astMontoCuota").css("color", "red");
            estaBien = false;
        }
    //#endregion

    return estaBien;
}

//#endregion

//#region Crear
$("#btnCerrarCrear").click(function () {
    //Ocultar validaciones span
    limpiarSpan("Crear");
    //Asteriscos
    limpiarAsteriscos("Crear");
    $("#emp_Id").val("0");
    $("#dei_Motivo").val('');
    $("#dei_Monto").val('');
    $("#dei_NumeroCuotas").val('');
    $("#dei_MontoCuota").val('');
    $("#dei_PagaSiempre").prop('checked', false);
    $("#AgregarDeduccionesIndividuales").modal('hide');
});


//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE

//Pedir data para llenar el DDL
$(document).on("click", "#btnAgregarDeduccionIndividual", function () {
    let valCreate = $("#Crear #emp_IdCreate").val();
    if (valCreate != null && valCreate != "")
        $("#Crear #emp_IdCreate").val('').trigger('change');

    //Ocultar validaciones span
    limpiarSpan("Crear");
    //Asteriscos
    limpiarAsteriscos("Crear");
    document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = false;

    $("#AgregarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("#dei_Motivo").val('');
    $("#dei_Monto").val('');
    $("#dei_NumeroCuotas").val('');
    $("#dei_MontoCuota").val('');
    $('#dei_PagaSiempre').prop('checked', false);
    $('#dei_DeducirISR').prop('checked', false);
    $("#Crear #MontoRestanteCrear").css("display", "none");
});

//Create POST
$('#btnCreateRegistroDeduccionIndividual').click(function () {

    //#region Declaracion de variables
    let emp_Id = $("#Crear #emp_Id").val();
    let dei_Motivo = $("#Crear #dei_Motivo").val();
    let dei_Monto = $("#Crear #dei_Monto").val();
    let dei_NumeroCuotas = $("#frmCreateDeduccionIndividual #dei_NumeroCuotas").val();
    let dei_MontoCuota = $("#Crear #dei_MontoCuota").val();
    let dei_PagaSiempre = $("#Crear #dei_PagaSiempre").val();
    let dei_DeducirISR = $("#Crear #dei_DeducirISR").val();
    //#endregion

    //Obtener valor del checkbox
    if ($('#Crear #dei_PagaSiempre').is(':checked')) {
        dei_PagaSiempre = true;
    }
    else {
        dei_PagaSiempre = false;
    }

    if ($('#Crear #dei_DeducirISR').is(':checked')) {
        dei_DeducirISR = true;
    }
    else {
        dei_DeducirISR = false;
    }
    debugger;
    //#region  POST Create
    if (estaTodoValidado("Crear")) {
        document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = true;

        var data = { dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_Monto: dei_Monto, dei_NumeroCuotas: dei_NumeroCuotas, dei_MontoCuota: dei_MontoCuota, dei_PagaSiempre: dei_PagaSiempre, dei_DeducirISR: dei_DeducirISR };

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionesIndividuales/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                cargarGridDeducciones();
                $("#Crear #dei_Motivo").val('');
                $("#Crear #dei_Monto").val('');
                $("#Crear #dei_NumeroCuotas").val('');
                $("#Crear #dei_MontoCuota").val('');
                $('#Crear #dei_PagaSiempre').prop('checked', false);
                $('#Crear #dei_DeducirISR').prop('checked', false);
                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarDeduccionesIndividuales").modal('hide');

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
        });
    }
    //#endregion

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateDeduccionIndividual").submit(function (e) {
        return false;
    });

});

//#endregion

//#region Editar
//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    limpiarAsteriscos("Editar");
    limpiarSpan("Editar");
    $("#EditarDeduccionesIndividuales").modal('hide');
});


$(document).on("click", "#IndexTabla tbody tr td #btnEditarDeduccionesIndividuales", function () {
    let dataEmp = table.row($(this).parents('tr')).data(); //obtener la data de la fila seleccionada

    let itemEmpleado = localStorage.getItem('idEmpleado');
    if (itemEmpleado != null) {
        $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove().trigger('change.select2');
        localStorage.removeItem('idEmpleado');
    }

    limpiarAsteriscos("Editar");
    limpiarSpan("Editar");
    document.getElementById("btnEditDeduccionIndividual2").disabled = false;
    var id = $(this).data('id');
    inactivarID = id;
    $.ajax({
        url: "/DeduccionesIndividuales/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            if (data.dei_PagaSiempre) {
                $('#Editar #dei_PagaSiempre').prop('checked', true);
            }
            else {
                $('#Editar #dei_PagaSiempre').prop('checked', false);
            }

            if (data.dei_DeducirISR) {
                $('#Editar #dei_DeducirISR').prop('checked', true);
            }
            else {
                $('#Editar #dei_DeducirISR').prop('checked', false);
            }

            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                if ($('#Editar #emp_Id').hasClass("select2-hidden-accessible")) {
                    $('#Editar #emp_Id').select2('destroy').trigger('change');
                    $('#Editar #emp_Id').empty();
                }

                $.ajax({
                    url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                }).done(function (data) {
                    $('#Editar #emp_Id').select2({
                        destroy: true,
                        dropdownParent: $('#Editar'),
                        placeholder: 'Seleccione un empleado',
                        allowClear: true,
                        debug: true,
                        cache: false,
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
                });

                let idEmpSelect = data.emp_Id;
                let NombreSelect = dataEmp[2]; //asignar data del row seleccionado


                $('#Editar #emp_Id').val(idEmpSelect).trigger('change').trigger('clear.select2');
                let valor = $('#Editar #emp_Id').val();
                if (valor == null) {
                    $("#Editar #emp_Id").prepend(`<option value='` + idEmpSelect + `' selected>` + NombreSelect + `</option>`).trigger('change');
                    localStorage.setItem('idEmpleado', idEmpSelect);
                }


                $("#Editar #dei_IdDeduccionesIndividuales").val(data.dei_IdDeduccionesIndividuales);
                $("#Editar #dei_Motivo").val(data.dei_Motivo);
                $("#Editar #dei_Monto").val(data.dei_MontoInicial);
                $("#Editar #dei_NumeroCuotas").val(data.dei_MontoRestante);
                $("#Editar #dei_MontoCuota").val(data.dei_Cuota);
                $("#Editar #dei_PagaSiempre").val(data.dei_PagaSiempre);
                $("#Editar #dei_DeducirISR").val(data.dei_DeducirISR);
                $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});

$("#btnEditDeduccionIndividual").click(function () {
    if (estaTodoValidado("Editar")) {
        $("#EditarDeduccionesIndividuales").modal('hide');
        $("#EditarDeduccionesIndividualesConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditDeduccionIndividual2").click(function () {

    var dei_PagaSiempre = false;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var dei_IdDeduccionesIndividuales = $("#Editar #dei_IdDeduccionesIndividuales").val();
    var emp_Id = $("#Editar #emp_Id").val();
    var dei_Motivo = $("#Editar #dei_Motivo").val();
    var dei_Monto = $("#Editar #dei_Monto").val().replace(/,/g, '');;
    var dei_NumeroCuotas = $("#Editar #dei_NumeroCuotas").val().replace(/,/g, '');;
    var dei_MontoCuota = $("#Editar #dei_MontoCuota").val().replace(/,/g, '');;
    var dei_PagaSiempre = $("#Editar #dei_PagaSiempre").val();
    var dei_DeducirISR = $("#Editar #dei_DeducirISR").val();

    if ($('#Editar #dei_PagaSiempre').is(':checked')) {
        dei_PagaSiempre = true;
    }
    else {
        dei_PagaSiempre = false;
    }

    if ($('#Editar #dei_DeducirISR').is(':checked')) {
        dei_DeducirISR = true;
    }
    else {
        dei_DeducirISR = false;
    }

    var data = { dei_IdDeduccionesIndividuales: dei_IdDeduccionesIndividuales, dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_Monto: dei_Monto, dei_NumeroCuotas: dei_NumeroCuotas, dei_MontoCuota: dei_MontoCuota, dei_PagaSiempre: dei_PagaSiempre, dei_DeducirISR: dei_DeducirISR };

    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesIndividuales/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {
            document.getElementById("btnEditDeduccionIndividual2").disabled = true;
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
            $("#EditarDeduccionesIndividuales").modal('hide');
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
        else {
            $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditarDeduccionIndividual").submit(function (e) {
        return false;
    });

});
$("#EditarDeduccionesIndividuales").submit(function (e) {
    return false;
});
$(document).on("click", "#btnRegresar", function () {
    document.getElementById("btnEditDeduccionIndividual2").disabled = false;
    $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});


$(document).on("click", "#btnReg", function () {
    $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});
//#endregion

//#region Detalles
$(document).on("click", "#IndexTabla tbody tr td #btnDetalleDeduccionesIndividuales", function () {
    var id = $(this).data('id');
    $.ajax({
        url: "/DeduccionesIndividuales/Details/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                if (data[0].dei_PagaSiempre) {
                    $("#Detalles #dei_PagaSiempre").html("Si");
                }
                else {
                    $("#Detalles #dei_PagaSiempre").html("No");
                } 

                if (data[0].dei_DeducirISR) {
                    $("#Detalles #dei_DeducirISR").html("Si");
                }
                else {
                    $("#Detalles #dei_DeducirISR").html("No");
                }

                var FechaCrea = FechaFormato(data[0].dei_FechaCrea);
                var FechaModifica = FechaFormato(data[0].dei_FechaModifica);
                $("#Detalles #dei_IdDeduccionesIndividuales").html(data[0].dei_IdDeduccionesIndividuales);
                $("#Detalles #dei_Motivo").html(data[0].dei_Motivo);
                $("#Detalles #dei_Monto").html(data[0].dei_MontoInicial);
                $("#Detalles #dei_NumeroCuotas").html(data[0].dei_MontoRestante);
                $("#Detalles #dei_MontoCuota").html(data[0].dei_Cuota);
                $("#Detalles #emp_Id").html(data[0].emp_Id);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #dei_UsuarioCrea").html(data[0].dei_UsuarioCrea);
                $("#Detalles #dei_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #dei_UsuarioModifica").html(data[0].dei_UsuarioModifica);
                $("#Detalles #dei_FechaModifica").html(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].emp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        //$("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        //$("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            //$("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            if (iter.Id == SelectedId) {
                                $("#Detalles #emp_Id").html(iter.Descripcion);
                            }
                        });
                    });
                $("#DetallesDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});
//#endregion

//#region Inactivar
//Inactivar//
$(document).on("click", "#btnBack", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#InactivarDeduccionesIndividuales").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnBa", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#InactivarDeduccionesIndividuales").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnInactivarDeduccionesIndividuales", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#EditarDeduccionesIndividuales").modal('hide');
    $("#InactivarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccionIndividual").click(function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = true;
    var data = { dei_IdDeduccionesIndividuales: inactivarID }
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesIndividuales/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarDeduccionesIndividuales").modal('hide');
            $("#EditarDeduccionesIndividuales").modal('hide')
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
});
//#endregion