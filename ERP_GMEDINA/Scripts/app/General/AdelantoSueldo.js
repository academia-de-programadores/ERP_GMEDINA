//VARIABLE GLOBAL PARA INACTIVAR
var IDInactivar = 0;
var IDActivar = 0;
//OBJETO CONSTANTE DEL DDL DE EMPLEADOS
var cmbEmpleado = $("#emp_IdEmpleado");
//VARIABLE GLOBAL CON EL VALOR MAXIMO DEL SUELDO EN LA CREACION
var MaxSueldoCreate = 0;
//OBJETO CONSTANTE DEL DDL DE EMPLEADOS
var cmbEmpleadoEdit = $("#frmAdelantosEdit #emp_Id");
//VARIABLE GLOBAL CON EL VALOR MAXIMO DEL SUELDO EN LA EDICION
var MaxSueldoEdit = 0;

//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
    });

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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridAdelantos() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AdelantoSueldo/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaAdelantos = data;

            //limpiar la data del datatable
            $('#tblAdelantoSueldo').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAdelantos.length; i++) {
                // var FechaAdelanto = FechaFormato(ListaAdelantos[i].adsu_FechaAdelanto);
                var Deducido = ListaAdelantos[i].adsu_Deducido == true ? 'Deducido en planilla' : 'Sin deducir';
                var Activo = ListaAdelantos[i].adsu_Activo == true ? 'Activo' : 'Inactivo';
                UsuarioModifica = ListaAdelantos[i].adsu_UsuarioModifica == null ? 'Sin modificaciones' : ListaAdelantos[i].adsu_UsuarioModifica;

                var botonDetalles = '<button style="margin-right:3px;" data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleAdelantoSueldo">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaAdelantos[i].adsu_Activo == true ? '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs"  id="btnEditarAdelantoSueldo">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAdelantos[i].adsu_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs"  id="btnActivarRegistroAdelantos">Activar</button>' : '' : '';
                var dataId = ListaAdelantos[i].adsu_IdAdelantoSueldo;

                $('#tblAdelantoSueldo').dataTable().fnAddData([
                    ListaAdelantos[i].adsu_IdAdelantoSueldo,
                    ListaAdelantos[i].empleadoNombre,
                    ListaAdelantos[i].adsu_RazonAdelanto,
                    ListaAdelantos[i].adsu_Monto.toFixed(2),
                    //(ListaAdelantos[i].adsu_Monto % 1 == 0) ? ListaAdelantos[i].adsu_Monto + ".00" : ListaAdelantos[i].adsu_Monto,
                    ListaAdelantos[i].adsu_FechaAdelanto,
                    Deducido,
                    Activo,
                    botonDetalles +
                    botonEditar +
                    botonActivar
                ]);
            }
        });

}



//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarAdelanto", function () {

    var validacionPermiso = userModelState("AdelantoSueldo/Create");

    if (validacionPermiso.status == true) {

        // crear
        let valCreate = $("#Crear #emp_IdEmpleado").val();
        if (valCreate != null && valCreate != "")
            $("#Crear #emp_IdEmpleado").val('').trigger('change');
        //OCULTAR TODAS LAS VALIDACIONES
        OcultarValidacionesCrear();
        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCreateRegistroAdelantos").attr("disabled", false);
        $("#AgregarAdelantos").modal({ backdrop: 'static', keyboard: false });
        // termina crear
    }


});

$(document).ready(function () {
    $.ajax({
        url: "/AdelantoSueldo/EmpleadoGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $('#Crear #emp_IdEmpleado').select2({
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

        $('#Editar #emp_Id').select2({
            dropdownParent: $('#Editar'),
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
    });
});

//DETECTAR LOS CAMBIOS EN EL DDL DE EMPLEADOS EN LA CREACION
$(cmbEmpleado).change(() => {
    //CAPTURAR EL ID DEL EMPLEADO SELECCIONADO
    var IdEmp = parseInt(cmbEmpleado.val());
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IdEmp })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        MaxSueldoCreate = data;
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#AgregarAdelantos").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se recuperó el sueldo neto promedio, contacte al administrador',
        });
    });
});

$('#Crear #adsu_FechaAdelanto').blur(function () {
    var campo = $(this).val();

    var hoy = new Date();
    let fechaEscogida = campo.split('-');
    let anioActual = hoy.getFullYear();
    let mesActual = hoy.getMonth() + 1;
    let diaActual = hoy.getDate();
    let anioEscogido = parseInt(fechaEscogida[0]);
    let mesEscogido = parseInt(fechaEscogida[1]);
    let diaEscogido = parseInt(fechaEscogida[2]);

    if (anioEscogido >= anioActual && mesEscogido >= mesActual && diaEscogido >= diaActual) {
        $('#Crear #Validation_adsu_FechaAdelantoMenor').hide();
        $("#Crear #AsteriscoFecha").removeClass("text-danger");
    } else {
        $('#Crear #Validation_adsu_FechaAdelantoMenor').show();
        $("#Crear #AsteriscoFecha").addClass("text-danger");
    }
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAdelantos').click(function () {
    var Razon = $("#Crear #adsu_RazonAdelanto").val();
    var Monto = $("#Crear #adsu_Monto").val();
    var IdEmp = $("#Crear #emp_IdEmpleado").val();
    var Fecha = $("#Crear #adsu_FechaAdelanto").val();

    if (ValidarCamposCrear(Razon, Monto, IdEmp, Fecha)) {
        //BLOQUEAR EL BOTON
        $("#btnCreateRegistroAdelantos").attr("disabled", true);

        //SEGMENTAR LA CADENA DE MONTO
        var indices = $("#Crear #adsu_Monto").val().split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i <= indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);

        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = {
            emp_Id: IdEmp,
            adsu_FechaAdelanto: Fecha,
            adsu_RazonAdelanto: Razon,
            adsu_Monto: MontoFormateado
        };
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/AdelantoSueldo/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarAdelantos").modal('hide');
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
                //DESBLOQUEAR EL BOTON DE CREAR
                $("#btnCreateRegistroAdelantos").attr("disabled", false);
            }
            else {
                cargarGridAdelantos();
                //Setear la variable de SueldoAdelantoMaximo a cero
                MaxSueldoCreate = 0;
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(Razon, Monto, IdEmp, Fecha) {
    var Local_modelState = true;
    //VALIDAR MONTO POR COLABORADOR
    if (IdEmp == 0)
        $("#Crear #adsu_Monto").attr("disabled", true);   //DESBLOQUEAR EL CAMPO MONTO
    else
        $("#Crear #adsu_Monto").attr("disabled", false);  //DESBLOQUEAR EL CAMPO MONTO

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Crear #adsu_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i <= indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);


    //VALIDACIONES DEL CAMPO EMP_ID
    if (IdEmp != "-1") {
        if (IdEmp == null || IdEmp == "") {
            Local_modelState = false;
            $("#Crear #AsteriscoColaborador").addClass("text-danger");
            $("#Crear #Span_emp_Id").css("display", "");
        }
        else {
            $("#Crear #AsteriscoColaborador").removeClass("text-danger");
            $("#Crear #Span_emp_Id").css("display", "none");
        }
    }

    //VALIDACIONES DEL CAMPO RAZON
    if (Razon != "-1") {

        //VALIDACION DE DOBLE ESPACIO
        var LengthString = Razon.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Razon.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #adsu_RazonAdelanto").val(Razon.substring(0, FirstChar + 1));
        }//FIN DE VALIDACION DE DOBLE ESPACIO

        if (Razon == "" || Razon == " " || Razon == "  " || Razon == null || Razon == undefined) {
            if (Razon == ' ')
                $("#Crear #adsu_RazonAdelanto").val("");
            Local_modelState = false;
            $("#Crear #AsteriscoRazon").addClass("text-danger");
            $("#Crear #adsu_RazonAdelantoValidacion").show();

        } else {
            $("#Crear #AsteriscoRazon").removeClass("text-danger");
            $("#Crear #adsu_RazonAdelantoValidacion").hide();
        }
    }
    //VALIDACIONES DEL CAMPO MONTO
    if (Monto != "-1") {

        //VALIDACIONES DE ENTRADA POR EL CAMPO MONTO
        if (MontoFormateado > MaxSueldoCreate && MontoFormateado != '' && MontoFormateado != undefined && MontoFormateado != null && IdEmp != 0) {

            //MOSTRAR VALIDACIONES
            var Decimal_Sueldo = (MaxSueldoCreate % 1 == 0) ? MaxSueldoCreate + ".00" : MaxSueldoCreate;
            $("#Crear #SueldoPromedioCrear").html('El monto máximo de adelanto es ' + Decimal_Sueldo);
            $("#Crear #SueldoPromedioCrear").show();
            //$("#Crear #AsteriscoMonto").css("display", "");
            $("#Crear #AsteriscoMonto").addClass("text-danger");
            Local_modelState = false;

        } else {
            //OCULTAR VALIDACIONES
            $("#Crear #AsteriscoMonto").removeClass("text-danger");
            $("#Crear #SueldoPromedioCrear").hide();

            //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
            if (Monto == "" || Monto == null || Monto == undefined) {
                $("#Crear #AsteriscoMonto").addClass("text-danger");
                $("#Crear #adsu_MontoValidacion").show();

                Local_modelState = false;
            } else {
                $("#Crear #AsteriscoMonto").removeClass("text-danger");
                $("#Crear #adsu_MontoValidacion").hide();
                if (MontoFormateado <= 0) {
                    pasoValidacion = false;
                    $('#Crear #SueldoPromedioCrear').hide();
                    $('#Crear #adsu_MontoValidacion').hide();
                    $("#Crear #MontoAsterisco").addClass("text-danger");
                    $('#Crear #adsu_MontoValidacion2').show();

                } else {
                    $("#Crear #MontoAsterisco").removeClass("text-danger");
                    $('#Crear #adsu_MontoValidacion2').hide();
                }
            }
        }

    }

    //VALIDACIONES DEL CAMPO FECHA
    if (Fecha != "-1") {
        if (Fecha == "" || Fecha == null || Fecha == undefined) {
            $('#Crear #Validation_adsu_FechaAdelantoMenor').hide();
            $("#Crear #AsteriscoFecha").addClass("text-danger");
            $("#Crear #Validation_adsu_FechaAdelanto").empty();
            $("#Crear #Validation_adsu_FechaAdelanto").html("El campo Fecha es requerido.");
            $("#Crear #Validation_adsu_FechaAdelanto").show();
            Local_modelState = false;

        } else {
            $("#Crear #AsteriscoFecha").removeClass("text-danger");
            $("#Crear #Validation_adsu_FechaAdelanto").empty();
            $("#Crear #Validation_adsu_FechaAdelanto").hide();

            var hoy = new Date();
            let fechaEscogida = Fecha.split('-');
            let anioActual = hoy.getFullYear();
            let mesActual = hoy.getMonth() + 1;
            let diaActual = hoy.getDate();
            let anioEscogido = parseInt(fechaEscogida[0]);
            let mesEscogido = parseInt(fechaEscogida[1]);
            let diaEscogido = parseInt(fechaEscogida[2]);

            if (anioEscogido >= anioActual && mesEscogido >= mesActual && diaEscogido >= diaActual) {
                $('#Crear #Validation_adsu_FechaAdelantoMenor').hide();
                $("#Crear #AsteriscoFecha").removeClass("text-danger");
            } else {
                Local_modelState = false;
                $('#Crear #Validation_adsu_FechaAdelantoMenor').show();
                $("#Crear #AsteriscoFecha").addClass("text-danger");
            }
        }
    }

    return Local_modelState;
}

//FUNCION: OCULTAR LOS MENSAJES DE ERROR DE VALIDACIONES, AL CERRAR EL MODAL
function OcultarValidacionesCrear() {



    //SETEAR LOS CAMPOS
    $("#Crear #adsu_RazonAdelanto").val("");
    $("#Crear #adsu_Monto").val("");
    $("#Crear #adsu_FechaAdelanto").val("");
    $("#Crear #SueldoPromedioCrear").hide();

    //OCULTAR VALIDACIONES DE EMP_ID
    $('#Crear #Span_emp_Id').hide();
    $("#Crear #AsteriscoColaborador").removeClass("text-danger");

    //OCULTAR VALIDACIONES DE RAZON
    $('#Crear #adsu_RazonAdelantoValidacion').hide();
    $("#Crear #AsteriscoRazon").removeClass("text-danger");

    //OCULTAR VALIDACIONES DE RAZON
    $("#Crear #AsteriscoMonto").removeClass("text-danger");
    $("#Crear #SueldoPromedioEditar").hide();
    $('#Crear #adsu_MontoValidacion').hide();
    $('#Crear #adsu_MontoValidacion2').hide();

    //OCULTAR VALIDACIONES DE FECHA
    $("#Crear #AsteriscoFecha").removeClass("text-danger");
    $("#Crear #Validation_adsu_FechaAdelanto").hide();

    $('#Crear #Validation_adsu_FechaAdelantoMenor').hide();
    $("#Crear #AsteriscoFecha").removeClass("text-danger");

}

//FUNCION: OCULTAR EL MODAL DE CREAR
$('#btnCerrarCrearAdelanto').click(function () {
    //OCULTAR MODAL DE CREACION
    $("#AgregarAdelantos").modal("hide");
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnEditarAdelantoSueldo", function () {

    var validacionPermiso = userModelState("AdelantoSueldo/Edit");

    if (validacionPermiso.status == true) {
        let itemEmpleado = localStorage.getItem('idEmpleado');

        if (itemEmpleado != null) {
            $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove();
            localStorage.removeItem('idEmpleado');
        }

        //OCULTAR VALIDACIONES
        OcultarValidacionesEditar();

        var ID = $(this).data('id');
        IDInactivar = ID;

        var idEmpSelect = "";
        var NombreSelect = "";

        $.ajax({
            url: "/AdelantoSueldo/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        }).done(function (data) {

            //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
            $.ajax({
                url: "/AdelantoSueldo/GetSueldoNetoProm",
                method: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: data.emp_Id })
            }).done(function (dataEmpleado) {
                //ACCIONES EN CASO DE EXITO
                MaxSueldoCreate = dataEmpleado;
            }).fail(function (data) {
                //ACCIONES EN CASO DE ERROR
                $("#AgregarAdelantos").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'No se recuperó el sueldo neto promedio, contacte al administrador',
                });
            });
            idEmpSelect = data.emp_Id;
            NombreSelect = data.per_Nombres;
            $.ajax({
                url: "/AdelantoSueldo/Edit/" + ID,
                method: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: ID })
            }).done(function (dataAdelantoSueldo) {
                if (dataAdelantoSueldo) {
                    //HABILITAR O INHABILITAR EL BOTON DE EDITAR SI ESTA DEDUCIDO O NO
                    if (dataAdelantoSueldo.adsu_Deducido) {
                        document.getElementById("btnUpdateAdelantos").disabled = true;
                    } else {
                        $("#btnUpdateAdelantos").attr('disabled', false);
                    }

                    $("#Editar #emp_Id").select2("val", "");

                    $('#Editar #emp_Id').val(idEmpSelect).trigger('change');

                    let valor = $('#Editar #emp_Id').val();

                    if (valor == null) {
                        $("#Editar #emp_Id").prepend("<option value='" + idEmpSelect + "' selected>" + NombreSelect + "</option>").trigger('change');
                        localStorage.setItem('idEmpleado', idEmpSelect);
                    }

                    $("#Editar #adsu_IdAdelantoSueldo").val(dataAdelantoSueldo.adsu_IdAdelantoSueldo);
                    $("#Editar #adsu_RazonAdelanto").val(dataAdelantoSueldo.adsu_RazonAdelanto);
                    $("#Editar #adsu_Monto").val(dataAdelantoSueldo.adsu_Monto);

                    //MOSTRAR EL MODAL Y BLOQUEAR EL FONDO
                    $("#EditarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });

                } else if (dataAdelantoSueldo.adsu_Deducido) {
                    iziToast.error({
                        title: 'Error',
                        message: 'No puede editar un registro deducido',
                    });
                }
            })

        }).fail(function (jqXHR, textStatus, error) {
            iziToast.error({
                title: 'Error',
                message: 'No se cargó la información del colaborador, contacte al administrador',
            });
        });
    }

});

$('#Crear #emp_IdEmpleado').change(() => {
    let IdEmpCreate = $('#emp_IdEmpleado').val();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IdEmpCreate })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        MaxSueldoCreate = data;
        let Decimal_SueldoCreate = (MaxSueldoCreate % 1 == 0) ? MaxSueldoCreate + ".00" : MaxSueldoCreate;
        $("#Crear #SueldoPromedioCrear").html('El monto máximo de adelanto es ' + Decimal_SueldoCreate);
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#EditarAdelantoSueldo").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se recuperó el sueldo neto promedio, contacte al administrador',
        });
    });
})

//DETECTAR LOS CAMBIOS EN EL DDL DE EMPLEADOS EN LA EDICION
$(cmbEmpleadoEdit).change(() => {
    //CAPTURAR EL ID DEL EMPLEADO SELECCIONADO
    var IdEmp = cmbEmpleadoEdit.val();

    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IdEmp })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        MaxSueldoEdit = data;
        var Decimal_Sueldo = (MaxSueldoEdit % 1 == 0) ? MaxSueldoEdit + ".00" : MaxSueldoEdit;
        $("#Editar #SueldoPromedioEditar").html('El monto máximo de adelanto es ' + Decimal_Sueldo);

        if (MontoFormateado != "")
            if (MontoFormateado > MaxSueldoEdit) {
                $("#Editar #SueldoPromedioEditar").show();
                $("#Editar #MontoAsterisco").addClass("text-danger");
            } else {
                $("#Editar #MontoAsterisco").removeClass("text-danger");
                $("#Editar #SueldoPromedioEditar").hide();
            }
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#EditarAdelantoSueldo").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se recuperó el sueldo neto promedio, contacte al administrador',
        });
    });
});

//FUNCION: OCULTAR EL MODAL DE EDITAR Y MOSTRAR EL MODAL DE CONFIRMACION
$("#btnUpdateAdelantos").click(function () {
    //OBTENER EL ID DEL EMPLEADO
    var Razon = $("#Editar #adsu_RazonAdelanto").val();
    var Monto = $("#Editar #adsu_Monto").val();
    var IdEmp = $("#Editar #emp_Id").val();

    //DESBLOQUEAR EL BOTON DE EDICION
    $("#btnConfirmarEditar").attr("disabled", false);
    //VALIDAR EL FORMULARIO
    if (ValidarCamposEditar(IdEmp, Razon, Monto)) {
        //OCULTAR EL MODAL DE EDICION
        $("#EditarAdelantoSueldo").modal('hide');
        //DESPLEGAR EL MODAL DE CONFIRMACION
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });

    }
});

//FUNCION: EJECUTAR EDICION DE REGISTROS
$("#btnConfirmarEditar").click(function () {
    document.getElementById("btnConfirmarEditar").disabled = true;

    $.ajax({
        url: "/AdelantoSueldo/Edit/" + IDInactivar,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IDInactivar })
    }).done(function (data) {
        if (data) {
            //HABILITAR O INHABILITAR EL BOTON DE EDITAR SI ESTA DEDUCIDO O NO
            if (!data.adsu_Deducido) {



                var IdEmp = $('#Editar #emp_Id').val();
                var Razon = $('#Editar #adsu_RazonAdelanto').val();

                //SEGMENTAR LA CADENA DE MONTO
                var indices = $("#EditarAdelantoSueldo #adsu_Monto").val().split(",");
                //VARIABLE CONTENEDORA DEL MONTO
                var MontoFormateado = "";
                //ITERAR LOS INDICES DEL ARRAY MONTO
                for (var i = 0; i <= indices.length; i++) {
                    //SETEAR LA VARIABLE DE MONTO
                    MontoFormateado += indices[i];
                }
                //FORMATEAR A DECIMAL
                MontoFormateado = parseFloat(MontoFormateado);

                //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
                var data_valida = {
                    adsu_IdAdelantoSueldo: data.adsu_IdAdelantoSueldo,
                    emp_Id: IdEmp,
                    adsu_RazonAdelanto: Razon,
                    adsu_Monto: MontoFormateado
                };



                $.ajax({
                    url: "/AdelantoSueldo/Edit",
                    method: "POST",
                    data: data_valida
                }).done(function (data) {
                    if (data == "error") {
                        //Cuando traiga un error del backend al guardar la edicion
                        iziToast.error({
                            title: 'Error',
                            message: 'No se editó el registro, contacte al administrador',
                        });
                    }
                    else {
                        // REFRESCAR UNICAMENTE LA TABLA
                        cargarGridAdelantos();
                        //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                        $("#ConfirmarEdicion").modal('hide');
                        document.getElementById("btnConfirmarEditar").disabled = false;
                        //Setear la variable de SueldoAdelantoMaximo a cero
                        MaxSueldoEdit = 0;
                        //Mensaje de exito de la edicion
                        iziToast.success({
                            title: 'Éxito',
                            message: '¡El registro se editó de forma exitosa!',
                        });
                    }
                });
            } else {
                $("#ConfirmarEdicion").modal('hide');
                //DESBLOQUEAR EL BOTON DE EDICION
                $("#btnConfirmarEditar").attr("disabled", false);
                iziToast.error({
                    title: 'Error',
                    message: 'No puede editar un registro deducido',
                });
            }

        }

    });
});

var MontoFormateado = "";
//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(colaborador, razon, monto) {
    var pasoValidacion = true;

    if (colaborador != "-1") {

        if (colaborador == null || colaborador == "") {
            pasoValidacion = false;
            $('#Editar #Span_emp_Id').show();
            $("#Editar #AsteriscoColaborador").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Span_emp_Id').hide();
            $("#Editar #AsteriscoColaborador").removeClass("text-danger");
        }
    }

    if (razon != "-1") {
        var LengthString = razon.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = razon.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #adsu_RazonAdelanto").val(razon.substring(0, FirstChar + 1));
        }
        if (razon == null || razon == '' || razon == ' ' || razon == '  ') {
            pasoValidacion = false;
            if (razon == ' ')
                $("#Editar #adsu_RazonAdelanto").val("");
            $('#Editar #adsu_RazonAdelantoValidacion').show();
            $("#Editar #RazonAsterisco").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #adsu_RazonAdelantoValidacion').hide();
            $("#Editar #RazonAsterisco").removeClass("text-danger");
        }
    }


    //VALIDACION DEL MONTO
    if (monto != "-1") {
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = monto.split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);

        //VALIDACIONES QUE EL MONTO NO SEA MAYOR QUE EL SUELDO NETO PROMEDIO
        if (MontoFormateado > MaxSueldoEdit) {

            //MOSTRAR VALIDACIONES
            var Decimal_Sueldo = (MaxSueldoEdit % 1 == 0) ? MaxSueldoEdit + ".00" : MaxSueldoEdit;
            $("#Editar #SueldoPromedioEditar").html('El monto máximo de adelanto es ' + Decimal_Sueldo);
            $("#Editar #SueldoPromedioEditar").show();
            $("#Editar #MontoAsterisco").addClass("text-danger");
            pasoValidacion = false;
        } else {
            //OCULTAR VALIDACIONES
            $("#Editar #MontoAsterisco").removeClass("text-danger");
            $("#Editar #SueldoPromedioEditar").hide();

            //VALIDACIONES DE INDEFINIDO Y MENOR QUE CERO
            if (MontoFormateado == null || MontoFormateado == '' || MontoFormateado == undefined) {
                pasoValidacion = false;
                $('#Editar #SueldoPromedio').hide();
                $('#Editar #adsu_MontoValidacion2').hide();
                $('#Editar #adsu_MontoValidacion').show();
                $("#Editar #MontoAsterisco").addClass("text-danger");
            } else {
                $('#Editar #adsu_MontoValidacion').hide();
                $("#Editar #MontoAsterisco").removeClass("text-danger");
                if (MontoFormateado <= 0) {
                    pasoValidacion = false;
                    $('#Editar #SueldoPromedioEditar').hide();
                    $('#Editar #adsu_MontoValidacion').hide();
                    $("#Editar #MontoAsterisco").addClass("text-danger");
                    $('#Editar #adsu_MontoValidacion2').show();
                } else {
                    $("#Editar #MontoAsterisco").removeClass("text-danger");
                    $('#Editar #adsu_MontoValidacion2').hide();
                }
            }
        }

    }

    return pasoValidacion;
}

//FUNCION: OCULTAR LOS MENSAJES DE ERROR DE VALIDACIONES
function OcultarValidacionesEditar() {

    //SETEAR LOS CAMPOS
    $("#Editar #adsu_RazonAdelanto").val("");
    $("#CreEditarar #adsu_Monto").val("");

    //OCULTAR VALIDACIONES DE EMP_ID
    $('#Editar #Span_emp_Id').hide();
    $("#Editar #AsteriscoColaborador").removeClass("text-danger");

    //OCULTAR VALIDACIONES DE RAZON
    $('#Editar #adsu_RazonAdelantoValidacion').hide();
    $("#Editar #RazonAsterisco").removeClass("text-danger");

    //OCULTAR VALIDACIONES DE RAZON
    $("#Editar #MontoAsterisco").removeClass("text-danger");
    $("#Editar #SueldoPromedioEditar").hide();
    $('#Editar #adsu_MontoValidacion').hide();
    $('#Editar #adsu_MontoValidacion2').hide();

}

//FUNCION: CERRAR EL MODAL DE CONFIRMACION AL EDITAR, (CON EL BOTON DE CERRAR)
$("#btnCerrarConfirmarEditar").click(function () {
    $("#ConfirmarEdicion").modal('hide');
    $("#EditarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: CERRAR MODAL DE EDICION CON EL BOTON CERRAR DEL MODAL DE EDITAR
$("#btnCerrarEditar").click(function () {
    //OCULTAR MODAL DE EDITAR
    $("#EditarAdelantoSueldo").modal('hide');
    document.getElementById("adsu_Monto").placeholder = '';
});

//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnDetalleAdelantoSueldo", function () {

    var validacionPermiso = userModelState("AdelantoSueldo/Details");

    if (validacionPermiso.status == true) {
        ActivarID = $(this).data('id');
        var ID = $(this).data('id');
        $.ajax({
            url: "/AdelantoSueldo/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {

                    var FechaCrea = FechaFormato(data.adsu_FechaCrea);
                    var FechaModifica = FechaFormato(data.adsu_FechaModifica);
                    var FechaRegistro = FechaFormato(data.adsu_FechaAdelanto);
                    if (data.adsu_Deducido) {
                        $("#Detalles #adsu_Deducido").html("Si");
                    } else {
                        $("#Detalles #adsu_Deducido").html("No");
                    }

                    $("#Detalles #per_Nombres").html(data.per_Nombres);
                    $("#Detalles #adsu_FechaAdelanto").html(FechaRegistro);
                    $("#Detalles #adsu_RazonAdelanto").html(data.adsu_RazonAdelanto);
                    $("#Detalles #adsu_Monto").html((data.adsu_Monto % 1 == 0) ? data.adsu_Monto + ".00" : data.adsu_Monto);

                    $("#Detalles #UsuarioCrea").html(data.UsuarioCrea);
                    $("#Detalles #adsu_FechaCrea").html(FechaCrea);
                    $("#Detalles #UsuarioModifica").html(data.UsuarioModifica);
                    $("#Detalles #adsu_FechaModifica").html(FechaModifica);

                    $("#DetallesAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se cargó la información, contacte al administrador',
                    });
                }
            });
    }
});

//FUNCION: MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarAdelantoSueldo", function () {

    var validacionPermiso = userModelState("AdelantoSueldo/Inactivar");
    if (validacionPermiso.status == true) {
        //MOSTRAR EL MODAL DE INACTIVAR
        $("#EditarAdelantoSueldo").modal('hide');
        $("#InactivarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
    }
});

//FUNCION: PRIMERA FASE DE INACTIVACION DE REGISTROS, MOSTRAR MODAL CON MENSAJE DE CONFIRMACION
$("#btnInactivarAdelantos").click(function () {
    $("#EditarAdelantos").modal('hide');
    $("#InactivarAdelantos").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroAdelantos").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnInactivarRegistroAdelantos").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AdelantoSueldo/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        //DESPLEGAR MODAL DE EDITAR OTRA VEZ
        $("#InactivarAdelantos").modal('hide');
        $("#EditarAdelantos").modal({ backdrop: 'static', keyboard: false });
        //BLOQUEAR EL BOTON
        $("#btnInactivarRegistroAdelantos").attr("disabled", false);
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarAdelantoSueldo").modal('hide');
            document.getElementById("btnInactivarRegistroAdelantos").disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//FUNCION: OCULTAR MODAL DE INACTIVACION
$("#btnCerrarInactivar").click(function () {
    $("#InactivarAdelantoSueldo").modal('hide');
    $("#EditarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnActivarRegistroAdelantos", function () {

    var validacionPermiso = userModelState("AdelantoSueldo/Activar");
    if (validacionPermiso.status == true) {
        IDActivar = $(this).data('id');
        $("#ActivarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
    }
});

//EJECUTAR ACTIVACION DEL REGISTRO EN EL MODAL
$("#btnActivarRegistroAdelantosModal").click(function () {
    document.getElementById("btnActivarRegistroAdelantosModal").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AdelantoSueldo/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
            $("#ActivarAdelantoSueldo").modal('hide');
            document.getElementById("btnActivarRegistroAdelantosModal").disabled = false;
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarAdelantoSueldo").modal('hide');
            document.getElementById("btnActivarRegistroAdelantosModal").disabled = false;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});

//FUNCION: OCULTAR MODAL DE ACTIVACION
$("#btnCerrarActivar").click(function () {
    $("#ActivarAdelantoSueldo").modal('hide');
});
