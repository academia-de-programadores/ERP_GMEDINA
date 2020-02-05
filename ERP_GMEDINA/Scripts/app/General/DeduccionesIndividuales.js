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
            $('#Tabla').DataTable().clear();

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
                $('#Tabla').dataTable().fnAddData([
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
$(document).on("click", "#Tabla tbody tr td #btnActivarDeduccionesIndividuales", function () {
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

function ValidarPagasiempreCrear(PagaSiempre) {
    let checked = $('#Crear #dei_PagaSiempre').prop('checked');
    if (checked == false) {
        $("#Crear #dei_Monto").removeAttr("readonly");
        $("#Crear #dei_Monto").removeClass("readOnly");
        $("#Crear #dei_NumeroCuotas").removeAttr("readonly");
        $("#Crear #dei_NumeroCuotas").removeClass("readOnly");
        $("#Crear #dei_MontoCuota").attr("readonly", "readonly");
        $("#Crear #dei_MontoCuota").addClass("readOnly");
        $("#Crear #dei_MontoCuota").val('');
        $("#Crear #dei_Monto").val('');
        $("#Crear #dei_NumeroCuotas").val('');
        $("#Crear #valMontoCuota").css("display", "none");
        $("#Crear #valMontoCuotaMayor").css("display", "none");
        $("#Crear #astMontoCuota").css("color", "black");
    }
    else {
        $("#Crear #dei_MontoCuota").val('');
        $("#Crear #dei_Monto").val('');
        $("#Crear #dei_NumeroCuotas").val('');
        $("#Crear #dei_Monto").attr("readonly", "readonly");
        $("#Crear #dei_Monto").addClass("readOnly");
        $("#Crear #dei_NumeroCuotas").attr("readonly", "readonly");
        $("#Crear #dei_NumeroCuotas").addClass("readOnly");
        $("#Crear #dei_MontoCuota").removeAttr("readonly");
        $("#Crear #dei_MontoCuota").removeClass("readOnly");
        $("#Crear #valMontoRequerido").css("display", "none");
        $("#Crear #valMontoCuotaMayor").css("display", "none");
        $("#Crear #valNumeroCuotasRequerido").css("display", "none");
        $("#Crear #astMonto").css("color", "black");
        $("#Crear #astNumeroCuotas").css("color", "black");
    }
}

function ValidarPagasiempreEditar(PagaSiempre) {
    let checked = $('#Editar #dei_PagaSiempre').prop('checked');
    console.log(checked);
    if (checked == false) {
        $("#Editar #dei_Monto").removeAttr("readonly");
        $("#Editar #dei_Monto").removeClass("readOnly");
        $("#Editar #dei_NumeroCuotas").removeAttr("readonly");
        $("#Editar #dei_NumeroCuotas").removeClass("readOnly");
        $("#Editar #dei_MontoCuota").attr("readonly", "readonly");
        $("#Editar #dei_MontoCuota").addClass("readOnly");
        $("#Editar #valMontoCuota").css("display", "none");
        $("#Editar #valMontoCuotaMayor").css("display", "none");
        $("#Editar #astMontoCuota").css("color", "black");
    }
    else {
        $("#Editar #dei_Monto").attr("readonly", "readonly");
        $("#Editar #dei_Monto").addClass("readOnly");
        $("#Editar #dei_NumeroCuotas").attr("readonly", "readonly");
        $("#Editar #dei_NumeroCuotas").addClass("readOnly");
        $("#Editar #dei_MontoCuota").removeAttr("readonly");
        $("#Editar #dei_MontoCuota").removeClass("readOnly");
        $("#Editar #valMontoRequerido").css("display", "none");
        $("#Editar #valMontoCuotaMayor").css("display", "none");
        $("#Editar #valNumeroCuotasRequerido").css("display", "none");
        $("#Editar #astMonto").css("color", "black");
        $("#Editar #astNumeroCuotas").css("color", "black");
    }
}


function ValidarCrearDeduccionIndividual(Motivo, IdEmp, Monto, NumeroCuotas, MontoCuota) {
    pasoValidacionCrear = true;

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Crear #dei_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Crear #dei_MontoCuota").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoCuotaFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoCuotaFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoCuotaFormateado = parseFloat(MontoCuotaFormateado);




    //VALIDAR MONTO POR COLABORADOR
    if (IdEmp == 0)
        $("#Crear #dei_Monto").attr("disabled", true);   //DESBLOQUEAR EL CAMPO MONTO
    else
        $("#Crear #dei_Monto").attr("disabled", false);  //DESBLOQUEAR EL CAMPO MONTO

    if (IdEmp != "-1") {
        if (IdEmp == 0 || IdEmp == null) {
            pasoValidacionCrear = false;
            $("#Crear #valEmpId").css("display", "");
            $("#Crear #astEmpId").css("color", "red");
        } else {
            $("#Crear #valEmpId").css("display", "none");
            $("#Crear #astEmpId").css("color", "black");
        }
    }

    if (Motivo != "-1") {
        var LengthString = Motivo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Motivo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #dei_Motivo").val(Motivo.substring(0, FirstChar + 1));
        }
        if (Motivo == "" || Motivo == " " || Motivo == "  " || Motivo == null || Motivo == undefined) {
            if (Motivo == ' ')
                $("#Crear #dei_Motivo").val("");
            pasoValidacionCrear = false;
            $("#Crear #valMotivo").css("display", "");
            $("#Crear #astMotivo").css("color", "red");
        } else {
            $("#Crear #valMotivo").css("display", "none");
            $("#Crear #astMotivo").css("color", "black");
        }
    }

    if (Monto != "-1") {
        var MontoDivC = $("#Crear #dei_Monto").val();
        var numeroDivC = $("#Crear #dei_NumeroCuotas").val();
        var MontoCuotatotalC = MontoFormateado / numeroDivC;
        $("#Crear #dei_MontoCuota").val(MontoCuotatotalC);
        let checked = $('#Crear #dei_PagaSiempre').prop('checked');
        if (checked == false) {
            if (MontoFormateado == "" || MontoFormateado == null || MontoFormateado == undefined || isNaN(MontoFormateado)) {
                pasoValidacionCrear = false;
                $("#Crear #valMontoRequerido").html('Campo Monto Requerido');
                $("#Crear #valMontoRequerido").css("display", "");
                $("#Crear #astMonto").css("color", "red");
            } else {
                $("#Crear #valMontoRequerido").css("display", "none");
                $("#Crear #astMonto").css("color", "black");
                if (MontoFormateado == 0.00 || MontoFormateado < 0) {
                    pasoValidacionCrear = false;
                    $("#Crear #valMonto").html('El campo Monto no puede ser menor o igual que cero');
                    $("#Crear #valMonto").css("display", "block");
                    $("#Crear #valMonto").css("color", "red");
                }
                else {
                    $("#Crear #valMonto").css("display", "none");
                    $("#Crear #valMonto").css("color", "black");
                }
            }
        }
    }

    if (NumeroCuotas != "-1") {
        var MontoDivC = $("#Crear #dei_Monto").val();
        var numeroDivC = $("#Crear #dei_NumeroCuotas").val();
        var MontoCuotatotalC = MontoFormateado / numeroDivC;
        $("#Crear #dei_MontoCuota").val(MontoCuotatotalC);
        let checked = $('#Crear #dei_PagaSiempre').prop('checked');
        if (checked == false) {
            if (NumeroCuotas == "" || NumeroCuotas == null || NumeroCuotas == undefined) {
                pasoValidacionCrear = false;
                $("#Crear #valNumeroCuotasRequerido").css("display", "");
                $("#Crear #valNumeroCuotasRequerido").css("display", "");
                $("#Crear #astNumeroCuotas").css("color", "red");
            } else {
                $("#Crear #valNumeroCuotasRequerido").css("display", "none");
                $("#Crear #astNumeroCuotas").css("color", "black");
                if (NumeroCuotas == 0 || NumeroCuotas < 0) {
                    pasoValidacionCrear = false;
                    $("#Crear #valNumeroCuotasteMayor").css("display", "block");
                    $("#Crear #valNumeroCuotasMayor").css("display", "block");
                    $("#Crear #astNumeroCuotas").css("color", "red");

                }
                else {
                    $("#Crear #valNumeroCuotasMayor").css("display", "none");
                    $("#Crear #astNumeroCuotas").css("color", "black");
                }
            }
        }
    }

    if (MontoCuota != "-1") {
        let checked = $('#Crear #dei_PagaSiempre').prop('checked');
        if (checked == true) {
            if (MontoCuotaFormateado == "" || MontoCuotaFormateado == null || MontoCuotaFormateado == undefined || isNaN(MontoCuotaFormateado)) {
                $("#Crear #astMontoCuota").css("color", "red");
                $("#Crear #valMontoCuota").css("display", "");
                pasoValidacionCrear = false;
            } else {
                $("#Crear #valMontoCuota").css("display", "none");
                $("#Crear #astMontoCuota").css("color", "black");
            }
            if (MontoCuotaFormateado == 0.00 || MontoCuotaFormateado < 0) {
                pasoValidacionCrear = false;
                $("#Crear #valMontoCuotaMayor").css("display", "");
                $("#Crear #valMontoCuota").css("display", "none");
                $("#Crear #astMontoCuota").css("color", "red");
            }
            else {
                $("#Crear #valMontoCuotaMayor").css("display", "none");
                $("#Crear #astMontoCuota").css("color", "black");
            }
        }
    }

    return pasoValidacionCrear;
}

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


//#region Crear
$("#btnCerrarCrear").click(function () {
    //Ocultar validaciones span
    limpiarSpan("Crear");
    //Asteriscos
    limpiarAsteriscos("Crear");
    $("#Crear #emp_IdCreate").val('').trigger('change.select2');
    $("#Crear #dei_Motivo").val('');
    $("#Crear #dei_Monto").val('');
    $("#Crear #dei_NumeroCuotas").val('');
    $("#Crear #dei_MontoCuota").val('');
    $("#Crear #dei_PagaSiempre").prop('checked', false);
    $("#Crear #dei_DeducirISR").prop('checked', false);
    $("#AgregarDeduccionesIndividuales").modal('hide');
    $("#Crear #dei_Monto").removeAttr("readonly");
    $("#Crear #dei_Monto").removeClass("readOnly");
    $("#Crear #dei_NumeroCuotas").removeAttr("readonly");
    $("#Crear #dei_NumeroCuotas").removeClass("readOnly");
    $("#Crear #dei_MontoCuota").attr("readonly", "readonly");
    $("#Crear #dei_MontoCuota").addClass("readOnly");
    $("#Crear #dei_MontoCuota").val('');
    $("#Crear #dei_Monto").val('');
    $("#Crear #dei_NumeroCuotas").val('');
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
    $("#emp_IdCreate").val("0");
    $("#dei_Monto").val('');
    $("#dei_NumeroCuotas").val('');
    $("#dei_MontoCuota").val('');
    $('#dei_PagaSiempre').prop('checked', false);
    $('#dei_DeducirISR').prop('checked', false);
    $("#Crear #dei_Monto").removeAttr("readonly");
    $("#Crear #dei_Monto").removeClass("readOnly");
    $("#Crear #dei_NumeroCuotas").removeAttr("readonly");
    $("#Crear #dei_NumeroCuotas").removeClass("readOnly");
    $("#Crear #dei_MontoCuota").attr("readonly", "readonly");
    $("#Crear #dei_MontoCuota").addClass("readOnly");
    $("#Crear #dei_MontoCuota").val('');
    $("#Crear #dei_Monto").val('');
    $("#Crear #dei_NumeroCuotas").val('');
    //$("#Crear #NumeroCuotasCrear").css("display", "none");
});




function ValidarEditarDeduccionIndividual(Motivo, IdEmp, Monto, NumeroCuotas, MontoCuota) {
    pasoValidacionCrear = true;

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Editar #dei_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Editar #dei_MontoCuota").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoCuotaEditFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoCuotaEditFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoCuotaEditFormateado = parseFloat(MontoCuotaEditFormateado);


    //VALIDAR MONTO POR COLABORADOR
    if (IdEmp == 0)
        $("#Editar #dei_Monto").attr("disabled", true);   //DESBLOQUEAR EL CAMPO MONTO
    else
        $("#Editar #dei_Monto").attr("disabled", false);  //DESBLOQUEAR EL CAMPO MONTO

    if (IdEmp != "-1") {
        if (IdEmp == 0 || IdEmp == null) {
            pasoValidacionCrear = false;
            $("#Editar #valEmpId").css("display", "");
            $("#Editar #astEmpId").css("color", "red");
        } else {
            $("#Editar #valEmpId").css("display", "none");
            $("#Editar #astEmpId").css("color", "black");
        }
    }

    if (Motivo != "-1") {
        var LengthString = Motivo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Motivo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #dei_Motivo").val(Motivo.substring(0, FirstChar + 1));
        }
        if (Motivo == "" || Motivo == " " || Motivo == "  " || Motivo == null || Motivo == undefined) {
            if (Motivo == ' ')
                $("#Editar #dei_Motivo").val("");
            pasoValidacionCrear = false;
            $("#Editar #valMotivo").css("display", "");
            $("#Editar #astMotivo").css("color", "red");
        } else {
            $("#Editar #valMotivo").css("display", "none");
            $("#Editar #astMotivo").css("color", "black");
        }
    }

    if (Monto != "-1") {
        var MontoDiv = $("#Editar #dei_Monto").val();
        var numeroDiv = $("#Editar #dei_NumeroCuotas").val();
        var MontoCuotatotal = MontoFormateado / numeroDiv;
        $("#Editar #dei_MontoCuota").val(MontoCuotatotal);
        let checked = $('#Editar #dei_PagaSiempre').prop('checked');
        if (checked == false) {
            if (MontoFormateado == "" || MontoFormateado == null || MontoFormateado == undefined || isNaN(MontoFormateado)) {
                pasoValidacionCrear = false;
                $("#Editar #valMontoRequerido").html('Campo Monto Requerido');
                $("#Editar #valMontoRequerido").css("display", "");
                $("#Editar #astMonto").css("color", "red");
            } else {
                $("#Editar #valMontoRequerido, #Editar #valMontoRequerido").css("display", "none");
                $("#Editar #astMonto, #Editar #astMonto").css("color", "black");
                if (MontoFormateado == 0.00 || MontoFormateado < 0) {
                    pasoValidacionCrear = false;
                    $("#Editar #valMonto").html('El campo Monto no puede ser menor o igual que cero');
                    $("#Editar #valMonto").css("display", "block");
                    $("#Editar #valMonto").css("color", "red");
                }
                else {
                    $("#Editar #valMonto").css("display", "none");
                    $("#Editar #valMonto").css("color", "black");
                }
            }
        }
    }

    if (NumeroCuotas != "-1") {
        var MontoDiv = $("#Editar #dei_Monto").val();
        var numeroDiv = $("#Editar #dei_NumeroCuotas").val();
        var MontoCuotatotal = MontoFormateado / numeroDiv;
        $("#Editar #dei_MontoCuota").val(MontoCuotatotal);
        let checked = $('#Editar #dei_PagaSiempre').prop('checked');
        if (checked == false) {
            if (NumeroCuotas == "" || NumeroCuotas == null || NumeroCuotas == undefined) {
                pasoValidacionCrear = false;
                $("#Editar #valNumeroCuotasRequerido").css("display", "");
                $("#Editar #valNumeroCuotasRequerido").css("display", "");
                $("#Editar #astNumeroCuotas").css("color", "red");
            } else {
                $("#Editar #valNumeroCuotasRequerido").css("display", "none");
                $("#Editar #astNumeroCuotas").css("color", "black");
                if (NumeroCuotas == 0 || NumeroCuotas < 0) {
                    pasoValidacionCrear = false;
                    $("#Editar #valNumeroCuotasteMayor").css("display", "block");
                    $("#Editar #valNumeroCuotasMayor").css("display", "block");
                    $("#Editar #astNumeroCuotas").css("color", "red");

                }
                else {
                    $("#Editar #valNumeroCuotasMayor").css("display", "none");
                    $("#Editar #astNumeroCuotas").css("color", "black");
                }
            }
        }
    }

    if (MontoCuota != "-1") {
        let checked = $('#Editar #dei_PagaSiempre').prop('checked');
        if (checked == true) {
            if (MontoCuotaEditFormateado == "" || MontoCuotaEditFormateado == null || MontoCuotaEditFormateado == undefined) {
                pasoValidacionCrear = false;
                $("#Editar #valMontoCuota").css("display", "");
                $("#Editar #astMontoCuota").css("color", "red");
            } else {
                $("#Editar #valMontoCuota").css("display", "none");
                $("#Editar #astMontoCuota").css("color", "black");
                if (MontoCuotaEditFormateado == 0.00 || MontoCuotaEditFormateado < 0) {
                    pasoValidacionCrear = false;
                    $("#Editar #valMontoCuotaMayor").css("display", "block");
                    $("#Editar #valMontoCuota").css("display", "none");
                    $("#Editar #astMontoCuota").css("color", "red");

                }
                else {
                    $("#Editar #valMontoCuotaMayor").css("display", "none");
                    $("#Editar #astMontoCuota").css("color", "black");
                }
            }
        }
    }
    return pasoValidacionCrear;
}

    //Create POST
    $('#btnCreateRegistroDeduccionIndividual').click(function () {

        //#region Declaracion de variables
        let emp_Id = $("#Crear #emp_IdCreate").val();
        let dei_Motivo = $("#Crear #dei_Motivo").val();
        let dei_Monto = $("#Crear #dei_Monto").val();
        let dei_NumeroCuotas = $("#Crear #dei_NumeroCuotas").val();
        let dei_MontoCuota = $("#Crear #dei_MontoCuota").val();
        let dei_PagaSiempre = $("#Crear #dei_PagaSiempre").val();
        let dei_DeducirISR = $("#Crear #dei_DeducirISR").val();
        //#endregion

        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Crear #dei_Monto").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);

        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Crear #dei_MontoCuota").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateadoMontoCuota = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateadoMontoCuota += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateadoMontoCuota = parseFloat(MontoFormateadoMontoCuota);

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

        //#region  POST Create
        if (ValidarCrearDeduccionIndividual(dei_Motivo, emp_Id, MontoFormateado, dei_NumeroCuotas, MontoFormateadoMontoCuota)) {
            document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = true;

            let checked = $('#Crear #dei_PagaSiempre').prop('checked');
            if (checked == true) {
                MontoFormateado = 0.00;
                dei_NumeroCuotas = 0;
            }

            var data = { dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_Monto: MontoFormateado, dei_NumeroCuotas: dei_NumeroCuotas, dei_MontoCuota: MontoFormateadoMontoCuota, dei_PagaSiempre: dei_PagaSiempre, dei_DeducirISR: dei_DeducirISR };

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
        } else {
            document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = false;
            //VALIDAR LOS TIPOS DE ERRORES EN LOS CAMPOS
            ValidarCrearDeduccionIndividual(dei_Motivo, emp_Id, MontoFormateado, dei_NumeroCuotas, MontoFormateadoMontoCuota);
        }
        //#endSregion

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


    $(document).on("click", "#Tabla tbody tr td #btnEditarDeduccionesIndividuales", function () {
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
                    $("#Editar #dei_Monto").val(data.dei_Monto);
                    $("#Editar #dei_NumeroCuotas").val(data.dei_NumeroCuotas);
                    $("#Editar #dei_MontoCuota").val(data.dei_MontoCuota);
                    $("#Editar #dei_PagaSiempre").val(data.dei_PagaSiempre);
                    $("#Editar #dei_DeducirISR").val(data.dei_DeducirISR);
                    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
                    document.getElementById("btnEditDeduccionIndividual").disabled = false;
                    $("#Editar #dei_PagaSiempre").attr("disabled", true);
                    let checked = $('#Editar #dei_PagaSiempre').prop('checked');
                    if (checked == true) {
                        $("#Editar #dei_Monto").attr("readonly", "readonly");
                        $("#Editar #dei_Monto").addClass("readOnly");
                        $("#Editar #dei_NumeroCuotas").attr("readonly", "readonly");
                        $("#Editar #dei_NumeroCuotas").addClass("readOnly");
                        $("#Editar #dei_MontoCuota").removeAttr("readonly");
                        $("#Editar #dei_MontoCuota").removeClass("readOnly");
                        $("#Editar #valMontoRequerido").css("display", "none");
                        $("#Editar #valMontoCuotaMayor").css("display", "none");
                        $("#Editar #valNumeroCuotasRequerido").css("display", "none");
                        $("#Editar #astMonto").css("color", "black");
                        $("#Editar #astNumeroCuotas").css("color", "black");
                    }


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
        document.getElementById("btnEditDeduccionIndividual").disabled = true;
        // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        var dei_IdDeduccionesIndividuales = $("#Editar #dei_IdDeduccionesIndividuales").val();
        var emp_Id = $("#Editar #emp_Id").val();
        var dei_Motivo = $("#Editar #dei_Motivo").val();
        var dei_Monto = $("#Editar #dei_Monto").val();
        var dei_NumeroCuotas = $("#Editar #dei_NumeroCuotas").val();
        var dei_MontoCuota = $("#Editar #dei_MontoCuota").val();
        var dei_PagaSiempre = $("#Editar #dei_PagaSiempre").val();
        var dei_DeducirISR = $("#Editar #dei_DeducirISR").val();

        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Editar #dei_Monto").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);


        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Editar #dei_MontoCuota").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateadoMontoCuota = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateadoMontoCuota += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateadoMontoCuota = parseFloat(MontoFormateadoMontoCuota);

        if (ValidarEditarDeduccionIndividual(dei_Motivo, emp_Id, MontoFormateado, dei_NumeroCuotas, MontoFormateadoMontoCuota)) {
            $("#EditarDeduccionesIndividuales").modal('hide');
            $("#EditarDeduccionesIndividualesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        }
        else {
            document.getElementById("btnEditDeduccionIndividual").disabled = false;
            //VALIDAR LOS TIPOS DE ERRORES EN LOS CAMPOS
            ValidarEditarDeduccionIndividual(dei_Motivo, emp_Id, MontoFormateado, dei_NumeroCuotas, MontoFormateadoMontoCuota);
        }

    });

    //EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
    $("#btnEditDeduccionIndividual2").click(function () {
        var dei_PagaSiempre = false;
        var dei_IdDeduccionesIndividuales = $("#Editar #dei_IdDeduccionesIndividuales").val();
        var emp_Id = $("#Editar #emp_Id").val();
        var dei_Motivo = $("#Editar #dei_Motivo").val();
        var dei_Monto = $("#Editar #dei_Monto").val();
        var dei_NumeroCuotas = $("#Editar #dei_NumeroCuotas").val();
        var dei_MontoCuota = $("#Editar #dei_MontoCuota").val();
        var dei_PagaSiempre = $("#Editar #dei_PagaSiempre").val();
        var dei_DeducirISR = $("#Editar #dei_DeducirISR").val();
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Editar #dei_Monto").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);


        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = $("#Editar #dei_MontoCuota").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateadoMontoCuota = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateadoMontoCuota += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateadoMontoCuota = parseFloat(MontoFormateadoMontoCuota);

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

        let checked = $('#Editar #dei_PagaSiempre').prop('checked');
        if (checked == true) {
            MontoFormateado = 0.00;
            dei_NumeroCuotas = 0;
        }

        var data = { dei_IdDeduccionesIndividuales: dei_IdDeduccionesIndividuales, dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_Monto: MontoFormateado, dei_NumeroCuotas: dei_NumeroCuotas, dei_MontoCuota: MontoFormateadoMontoCuota, dei_PagaSiempre: dei_PagaSiempre, dei_DeducirISR: dei_DeducirISR };

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
    $(document).on("click", "#Tabla tbody tr td #btnDetalleDeduccionesIndividuales", function () {
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