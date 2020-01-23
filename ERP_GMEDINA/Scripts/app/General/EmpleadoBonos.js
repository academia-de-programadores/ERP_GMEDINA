//VARIABLES PARA INACTIVACION Y ACTIVACION DE REGISTROS
var IDInactivar = 0, IDActivar = 0;
//VARIABLE PARA VALIDAR SI ESTA PAGADO
var varPagado;

//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
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

//EVITAR POSTBACK DE FORMULARIOS

$('#frmEmpleadoBonosCreate').submit(function (e) {
    return false;
});
$('#frmEmpleadoBonos').submit(function (e) {
    return false;
});

//FUNCION GENERICA PARA REUTILIZAR AJAX


//FUNCION: MOSTRAR ERRORES IZITOAST
function mostrarError(Mensaje) {
    iziToast.error({
        title: 'Error',
        message: Mensaje,
    });
}

//FUNCION: ESCUCHA EL CAMBIO EN EL CHECKBOX Y CAMBIA SU VALOR
$('#Editar #cb_Pagado').click(function () {
    if ($('#Editar #cb_Pagado').is(':checked')) {
        $('#Editar #cb_Pagado').val(true);
    }
    else {
        $('#Editar #cb_Pagado').val(false);
    }
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridBonos() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/EmpleadoBonos/GetData',
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
            var ListaBonos = data;
            $('#tblEmpleadoBonos').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaBonos.length; i++) {
                var FechaRegistro = FechaFormato(ListaBonos[i].cb_FechaRegistro);
                var Estado = ListaBonos[i].cb_Activo == true ? 'Activo' : 'Inactivo';

                var botonDetalles = ListaBonos[i].cb_Activo == true ? '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleEmpleadoBonos">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaBonos[i].cb_Activo == true ? '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarEmpleadoBonos">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaBonos[i].cb_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaBonos[i].cb_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarEmpleadoBonos">Activar</button>' : '' : '';

                //VALIDACION PARA RECARGAR LA TABLA SIN AFECTAR LOS CHECKBOX
                var Check = "";
                //ESTA VARIABLE GUARDA CODIGO HTML DE UN CHECKBOX, PARA ENVIARLO A LA TABLA
                if (ListaBonos[i].cb_Pagado == true) {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" checked disabled>'; //SE LLENA LA VARIABLE CON UN INPUT CHEQUEADO
                } else {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" disabled>'; //SE LLENA LA VARIABLE CON UN INPUT QUE NO ESTA CHEQUEADO
                }

                $('#tblEmpleadoBonos').dataTable().fnAddData([
                   ListaBonos[i].cb_Id,
                   ListaBonos[i].per_Nombres + ' ' + ListaBonos[i].per_Apellidos,
                   ListaBonos[i].cin_DescripcionIngreso,
                   ListaBonos[i].cb_Monto,
                   FechaRegistro,
                   Check,
                   Estado,
                   botonDetalles + botonEditar + botonActivar]
                   ); 
            }
        });
    (FullBody);
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoBonos", function () {


    $("#AsteriscoEmpleado").removeClass("text-danger");
    $("#AsteriscoBono").removeClass("text-danger");
    $("#AsteriscoMonto").removeClass("text-danger");

    document.getElementById("btnCreateRegistroBonos").disabled = false;
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DE EMPLEADOS DEL MODAL
    $("#Validation_descipcion7").css("display", "");
    $("#Validation_descipcion8").css("display", "");
    $("#Validation_descipcion9").css("display", "");
    $("#Validation_descipcion4").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion6").css("display", "none");
    $.ajax({
        url: "/EmpleadoBonos/EditGetDDLEmpleado",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #emp_IdEmpleado").empty();
            $("#Crear #emp_IdEmpleado").append("<option value='0'>Selecionar colaborador...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #emp_IdEmpleado").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });

    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DE INGRESO DEL MODAL
    $.ajax({
        url: "/EmpleadoBonos/EditGetDDLIngreso",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #cin_IdIngreso").empty();
            $("#Crear #cin_IdIngreso").append("<option value='0'>Selecionar bono...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #cb_Monto").val("");
    $("#AgregarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
    
    
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroBonos').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var IdEmpleado = $("#Crear #emp_IdEmpleado").val();
    var IdIngreso = $("#Crear #cin_IdIngreso").val();
    var Monto = $("#Crear #cb_Monto").val();
    var decimales = Monto.split(".");
    document.getElementById("btnCreateRegistroBonos").disabled = true;
    if (IdEmpleado != 0 && IdIngreso != 0 &&
        Monto != "" && Monto != null && Monto != undefined && Monto > 0
        && decimales[1] != null && decimales[1] != undefined) {

        $("#AsteriscoEmpleado").removeClass("text-danger");
        $("#AsteriscoBono").removeClass("text-danger");
        $("#AsteriscoMonto").removeClass("text-danger");

        document.getElementById("btnCreateRegistroBonos").disabled = true;
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmEmpleadoBonosCreate").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/EmpleadoBonos/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarEmpleadoBonos").modal('hide');
            $("#Crear #cb_Monto").val("");
            //$("#Validation_descripcion1").css("display", "none");
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
            else {
                cargarGridBonos();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });

    }
    else {
        if (IdEmpleado == "0") {
            $("#AsteriscoEmpleado").addClass("text-danger");
            $("#Crear #Validation_descipcion2").css("display", "");
            document.getElementById("btnCreateRegistroBonos").disabled = false;
        }
        else {
            $("#AsteriscoEmpleado").removeClass("text-danger");
            $("#Crear #Validation_descipcion2").css("display", "none");
           
        }
        if (IdIngreso == "0") {
            $("#AsteriscoBono").addClass("text-danger");
            $("#Crear #Validation_descipcion4").css("display", "");         
            document.getElementById("btnCreateRegistroBonos").disabled = false;
        }
        else {
            $("#AsteriscoBono").removeClass("text-danger");
            $("#Crear #Validation_descipcion4").css("display", "none");
 
        }
        if (Monto == "" || Monto == null || Monto == undefined || Monto <= "0" || Monto == "0") {
            AsteriscoMonto
            $("#AsteriscoMonto").addClass("text-danger");
            $("#Crear #Validation_descipcion6").css("display", "");
       
            document.getElementById("btnCreateRegistroBonos").disabled = false;
        }
        else if (decimales[1] == null && decimales[1] == undefined) {
            $("#AsteriscoMonto").addClass("text-danger");
            $("#Crear #Validation_descipcion6").css("display", "");
            document.getElementById("btnCreateRegistroBonos").disabled = false;
        }
        else {
            $("#AsteriscoMonto").removeClass("text-danger");         
            $("#Crear #Validation_descipcion6").css("display", "none");
        }
    } 
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(Razon, Monto, IdEmp, Fecha) {
    console.log("KeyUp");

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
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);
    //VALIDACIONES DE ENTRADA POR EL CAMPO MONTO
    if (MontoFormateado > MaxSueldoCreate && MontoFormateado != '' && MontoFormateado != undefined && MontoFormateado != null  && IdEmp != 0) {
        
        //MOSTRAR VALIDACIONES
        var Decimal_Sueldo = (MaxSueldoCreate % 1 == 0) ? MaxSueldoCreate + ".00" : MaxSueldoCreate;
        console.log("El monto máximo de adelanto es " + Decimal_Sueldo);
        $("#Crear #SueldoPromedioCrear").html('El monto máximo de adelanto es ' + Decimal_Sueldo);
        $("#Crear #SueldoPromedioCrear").show();
        //$("#Crear #AsteriscoMonto").css("display", "");
        $("#Crear #AsteriscoMonto").addClass("text-danger");

    } else {
        //OCULTAR VALIDACIONES
        $("#Crear #AsteriscoMonto").removeClass("text-danger");
        $("#Crear #SueldoPromedioCrear").hide();
        
    }

    //VALIDACIONES DEL CAMPO EMP_ID
    if (IdEmp != "-1") {
        if (IdEmp == 0) {

            $("#Crear #AsteriscoColaborador").addClass("text-danger");
            $("#Crear #Validation_descripcion0").css("display", "");
        }
        else {
            $("#Crear #AsteriscoColaborador").removeClass("text-danger");
            $("#Crear #Validation_descripcion0").css("display", "none");
        }
    }

    //VALIDACIONES DEL CAMPO RAZON
    if (Razon != "-1") {
        var LengthString = Razon.length;
        if (LengthString > 1)
        {
            var FirstChar = LengthString - 2;
            var LastChar = Razon.substring(FirstChar, LengthString);
            console.log(LastChar);
        }
        if (LastChar == "  ")
        {
            $("#Crear #adsu_RazonAdelanto").val(Razon.substring(0, FirstChar + 1));
        }
        if (Razon == "" || Razon == " " || Razon == "  " || Razon == null || Razon == undefined) {
            if (Razon == ' ')
                $("#Crear #adsu_RazonAdelanto").val("");
            $("#Crear #AsteriscoRazon").addClass("text-danger");
            $("#Crear #Validation_descripcion1").css("display", "");
        
        } else {
            $("#Crear #AsteriscoRazon").removeClass("text-danger");
            $("#Crear #Validation_descripcion1").css("display", "none");
        }
    }
    //VALIDACIONES DEL CAMPO MONTO
    if (Monto != "-1") {
        if (Monto == "" || Monto == null || Monto == undefined) {
            $("#Crear #AsteriscoMonto").removeClass("text-danger");
            $("#Crear #Validation_descripcion4").css("display", "none");

            $("#Crear #AsteriscoMonto").addClass("text-danger");
            //$('#AsteriscoMonto').show();
            $("#Crear #Validation_descripcion2").css("display", "");
        } else {
            $("#Crear #AsteriscoMonto").removeClass("text-danger");
            $("#Crear #Validation_descripcion2").css("display", "none");
            if (Monto <= 0) {
                $("#Crear #AsteriscoMonto").addClass("text-danger");
                $("#Crear #Validation_descripcion4").css("display", "");
            } else {
                $("#Crear #AsteriscoMonto").removeClass("text-danger");
                $("#Crear #Validation_descripcion4").css("display", "none");
            }
        }
    }

    //VALIDACIONES DEL CAMPO FECHA
    if (Fecha != "-1") {
        if (Fecha == "" || Fecha == null || Fecha == undefined) {

            $("#Crear #AsteriscoFecha").addClass("text-danger");
            $("#Crear #Validation_descripcion3").css("display", "");

        } else {
            $("#Crear #AsteriscoFecha").removeClass("text-danger");
            $("#Crear #Validation_descripcion3").css("display", "none");
        }
    }
}

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposCrear(colaborador, Ingreso, monto) {
    var pasoValidacion = true;

    if (colaborador != "-1") {

        if (colaborador <= 0 || isNaN(colaborador || colaborador == "0")) {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Crear #Validation_IdEmpleado').css("display", "block");
            $("#Crear #AsteriscoEmpleado").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_IdEmpleado').css("display", "none");
            $("#Crear #AsteriscoEmpleado").removeClass("text-danger");
        }
    }

    if (Ingreso != "-1") {

        if (Ingreso <= 0 || isNaN(Ingreso) || Ingreso == "0") {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Crear #Validation_IdIngreso').css("display", "block");
            $("#Crear #AsteriscoBono").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_IdIngreso').css("display", "none");
            $("#Crear #AsteriscoBono").removeClass("text-danger");
        }
    }

    if (monto != "-1") {
        var LengthString = monto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = monto.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #cb_Monto").val(monto.substring(0, FirstChar + 1));
        }

        //console.log(monto);
        //console.log(monto.substring(0, 1));
        ////VALIDAR QUE EL MONTO SEA MAYOR QUE CERO
        //if (monto == "0.00" || parseFloat(monto) == 0.00) {
        //    $('#Crear #Validation_Monto').empty();
        //    $('#Crear #Validation_Monto').html("El campo monto debe ser mayor que cero");
        //    $('#Crear #Validation_Monto').show();
        //    $("#Crear #AsteriscoMonto").addClass("text-danger");
        //}
        //else {
        //    $('#Crear #Validation_Monto').empty();
        //    $('#Crear #Validation_Monto').html("El campo monto es requerido");
        //    $("#Crear #AsteriscoMonto").removeClass("text-danger");
        //}

        if (monto == null || monto == '' || monto == ' ' || monto == '  ') {
            pasoValidacion = false;
            if (monto == ' ')
                $("#Crear #cb_Monto").val("");

            $('#Crear #Validation_Monto').show();
            $("#Crear #AsteriscoMonto").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Crear #Validation_Monto').hide();
            $("#Crear #AsteriscoMonto").removeClass("text-danger");
        }
    }

    return pasoValidacion;
}

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(colaborador, Ingreso, monto) {
    var pasoValidacion = true;

    console.log("entra");
    if (colaborador != "-1") {

        if (colaborador <= 0 || isNaN(colaborador)) {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Editar #Validation_IdEmpleado').css("display", "block");
            $("#Editar #AsteriscoEmpleado").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_IdEmpleado').css("display", "none");
            $("#Editar #AsteriscoEmpleado").removeClass("text-danger");
        }
    }

    if (Ingreso != "-1") {

        if (Ingreso <= 0 || isNaN(Ingreso)) {
            pasoValidacion = false;
            //MOSTRAR VALIDACIONES
            $('#Editar #Validation_IdIngreso').css("display", "block");
            $("#Editar #AsteriscoBono").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_IdIngreso').css("display", "none");
            $("#Editar #AsteriscoBono").removeClass("text-danger");
        }
    }

    if (monto != "-1") {
        var LengthString = monto.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = monto.substring(FirstChar, LengthString);
            console.log(LastChar);
        }
        if (LastChar == "  ") {
            $("#Editar #cb_Monto").val(monto.substring(0, FirstChar + 1));
        }
        if (monto == null || monto == '' || monto == ' ' || monto == '  ') {
            pasoValidacion = false;
            if (monto == ' ')
                $("#Editar #cb_Monto").val("");
            $('#Editar #Validation_Monto').show();
            $("#Editar #AsteriscoMonto").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #Validation_Monto').hide();
            $("#Editar #AsteriscoMonto").removeClass("text-danger");
        }
    }

    return pasoValidacion;
}


$("#btnCerrarCrearBono").click(function () {
    $("#Crear #Validation_descipcion1").hidden = true;
    $("#Crear #Validation_descipcion1").css("display", "none");

    $("#Crear #Validation_descipcion2").hidden = true;
    $("#Crear #Validation_descipcion2").css("display", "none");

    $("#Crear #Validation_descipcion3").hidden = true;
    $("#Crear #Validation_descipcion3").css("display", "none");

    $("#Crear #Validation_descipcion4").hidden = true;
    $("#Crear #Validation_descipcion4").css("display", "none");

    $("#Crear #Validation_descipcion5").hidden = true;
    $("#Crear #Validation_descipcion5").css("display", "none");

    $("#Crear #Validation_descipcion6").hidden = true;
    $("#Crear #Validation_descipcion6").css("display", "none");
   
   
});

$("#IconCerrar").click(function () {
    $("#Validation_descipcion1").hidden = true;
    $("#Validation_descipcion1").css("display", "none");

    $("#Validation_descipcion2").hidden = true;
    $("#Validation_descipcion2").css("display", "none");

    $("#Validation_descipcion3").hidden = true;
    $("#Validation_descipcion3").css("display", "none");

    $("#Validation_descipcion4").hidden = true;
    $("#Validation_descipcion4").css("display", "none");

    $("#Validation_descipcion5").hidden = true;
    $("#Validation_descipcion5").css("display", "none");

    $("#Validation_descipcion6").hidden = true;
    $("#Validation_descipcion6").css("display", "none");
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnEditarEmpleadoBonos", function () {
    $("#Editar #Validation_descipcion6").css("display", "none");
    $("#Editar #Validation_descipcion5").css("display", "none");
    $("#Editar #AsteriscoMonto").removeClass("text-danger");
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/EmpleadoBonos/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                if (data.cb_Pagado) {
                    varPagado = 1;
                    document.getElementById("btnUpdateBonos").disabled = true;
                } else {
                    varPagado = 0;
                    document.getElementById("btnUpdateBonos").disabled = false;
                }
                var FechaRegistro = FechaFormato(data.cb_FechaRegistro);

                //AQUI VALIDA EL CHECKBOX PARA PODER CARGARLO EN EL MODAL
                if (data.cb_Pagado) {
                    $('#Editar #cb_Pagado').prop('checked', true);
                } else {
                    $('#Editar #cb_Pagado').prop('checked', false);
                }
                
                $("#Editar #cb_Id").val(data.cb_Id);
                $("#Editar #cb_Monto").val(data.cb_Monto);
                $("#Editar #cb_FechaRegistro").val(FechaRegistro);
                $("#Editar #cb_Pagado").val(data.cb_Pagado);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedIdEmp = data.emp_Id;
                var SelectedIdCatIngreso = data.cin_IdIngreso;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/EmpleadoBonos/EditGetDDLEmpleado",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #emp_IdEmpleado").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });

                $.ajax({
                    url: "/EmpleadoBonos/EditGetDDLIngreso",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #cin_IdIngreso").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


                $("#EditarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
                
                
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            Check = "";
        });
});

//VALIDAR LOS CAMPOS DE EDITAR Y MOSTRAR EL MODAL DE CONFIRMACION
$("#btnUpdateBonos").click(function () {
    var Monto = $("#Editar #cb_Monto").val();
    var decimales = Monto.split(".");
    if (Monto == "" || Monto == null || Monto == undefined || Monto <= 0 || Monto == 0) {
        $("#Editar #cin_IdIngreso").focus;
        $("#Editar #AsteriscoMonto").addClass("text-danger");
        $("#Editar #Validation_descipcion6").css("display", "");
        document.getElementById("btnUpdateBonos").disabled = false;
    } else if (decimales[1] == null && decimales[1] == undefined) {
        $("#EditarEmpleadoBonosConfirmacion").modal('hide');
        $("#Editar #Validation_descipcion6").css("display", "");
        $("#Editar #AsteriscoMonto").addClass("text-danger");
        document.getElementById("btnUpdateBonos").disabled = false;
    }
    else {
        $("#EditarEmpleadoBonos").modal('hide');
        document.getElementById("btnUpdateBonos2").disabled = false;
        $("#Editar #Validation_descipcion6").css("display", "none");
        $("#EditarEmpleadoBonosConfirmacion").modal({ backdrop: 'static', keyboard: false });
        
        
        $("#Editar #AsteriscoMonto").removeClass("text-danger");       
}
});
//FUNCION: EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL DE CONFIRMACION
$("#btnUpdateBonos2").click(function () {
    if (varPagado == 0) {
        document.getElementById("btnUpdateBonos2").disabled = true;
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEmpleadoBonos").serializeArray();

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/EmpleadoBonos/edit",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data == "error") {
                //Cuando traiga un error del backend al guardar la edicion
                iziToast.error({

                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
                $("#EditarEmpleadoBonosConfirmacion").modal('hide');
            }
            else {
                // REFRESCAR UNICAMENTE LA TABLA
                cargarGridBonos();
                FullBody();
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarEmpleadoBonos").modal('hide');
                $("#EditarEmpleadoBonosConfirmacion").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
        });
    } else {
        $("#EditarEmpleadoBonosConfirmacion").modal('hide');
        iziToast.error({
            title: 'Error',
            message: '¡No puede editar un registro pagado!',
        });
    }  
});

//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnDetalleEmpleadoBonos", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/EmpleadoBonos/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id : ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaRegistro = FechaFormato(data.cb_FechaRegistro);
                var FechaCrea = FechaFormato(data.cb_FechaCrea);
                var FechaModifica = FechaFormato(data.cb_FechaModifica);
                var usuarioModifica = data.usuarioModifica == null ? 'Sin modificaciones' : data.usuarioModifica;
                var usuarioCrea = data.NombreUsarioCrea == null ? 'N/A' : data.NombreUsarioCrea;

                if (data.cb_Pagado) { 
                    $("#Detalles #cb_Pagado").html("Si");
                } else {
                    $("#Detalles #cb_Pagado").html("No");
                }
                $("#Detalles #cb_Id").val(data.cb_Id);
                $("#Detalles #cb_Monto").html(data.cb_Monto);
                $("#Detalles #cb_FechaRegistro").html(FechaRegistro);
                //$("#Detalles #cb_Pagado").val(data.cb_Pagado);
                $("#Detalles #cb_UsuarioCrea").html(data.cb_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(usuarioCrea);
                $("#Detalles #cb_FechaCrea").html(FechaCrea);
                $("#Detalles #cb_UsuarioModifica").html(data.cb_UsuarioModifica);
                $("#Detalles #tbUsuario1_usu_NombreUsuario").html(usuarioModifica);
                $("#Detalles #cb_FechaModifica").html(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedIdEmp = data.emp_Id;
                var SelectedIdCatIngreso = data.cin_IdIngreso;

                $.ajax({
                    url: "/EmpleadoBonos/EditGetDDLIngreso",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                }).done(function (data) {
                    //-----------------------------------------NO ENTRA EN ESTE each
                    $.each(data, function (i, iter) {
                        if (iter.Id == SelectedIdCatIngreso) {
                            $("#Detalles #cin_IdIngreso").html(iter.Descripcion);
                        }
                    });
                });
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/EmpleadoBonos/EditGetDDLEmpleado",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        //$("#Detalles #emp_IdEmpleado").empty();
                        //LLENAR EL DROPDOWNLIST
                        //$("#Detalles #emp_IdEmpleado").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            //$("#Detalles #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            if (iter.Id == SelectedIdEmp) {
                                $("#Detalles #emp_Id").html(iter.Descripcion);
                            }
                            
                        });
                    });
                
                $("#DetallesEmpleadoBonos").modal();
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


//FUNCION: MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarEmpleadoBonos", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    document.getElementById("btnInactivarRegistroBono").disabled = false;
    document.getElementById("btCerrarNo").disabled = false;
    $("#EditarEmpleadoBonos").modal('hide');
    $("#InactivarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
    
    
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroBono").click(function () {
    document.getElementById("btnInactivarRegistroBono").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Inactivar/" + IDInactivar,
        method: "POST"
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
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!!',
            });
        }
    });
    IDInactivar = 0;
});

//VOLVER AL MODAL DE EDITAR CERRANDO EL MODAL DE INACTIVAR CON EL BOTON 'CERRAR'
$("#btCerrarNo").click(function () {
    //document.getElementById("btCerrarNo").disabled = true;
    $("#EditarEmpleadoBonos").modal();
    $("#InactivarEmpleadoBonos").modal('hide');
})

//VOLVER AL MODAL DE EDITAR CERRANDO EL MODAL DE INACTIVAR CON EL BOTON X
$("#IconCerrarInactivar").click(function () {
    $("#Editar #AsteriscoMonto").removeClass("text-danger");
    $("#Editar #Validation_descipcion3").css("display", "");
    $("#Editar #Validation_descipcion6").css("display", "none");
    $("#Editar #Validation_descipcion5").css("display", "none");
    //document.getElementById("btCerrarNo").disabled = true;
    $("#EditarEmpleadoBonos").modal();
    $("#InactivarEmpleadoBonos").modal('hide');
})

//VOLVER AL MODAL DE EDITAR CERRANDO EL MODAL DE CONFIRMACION CON LA X
$("#IconCerrarEditarConfirmacion").click(function () {
    $("#Editar #Validation_descipcion3").css("display", "");
    $("#Editar #Validation_descipcion6").css("display", "none");
    $("#Editar #Validation_descipcion5").css("display", "none");
    //document.getElementById("btCerrarNo").disabled = true;
    $("#EditarEmpleadoBonos").modal();
    $("#InactivarEmpleadoBonos").modal('hide');
})

//VOLVER AL MODAL DE EDITAR CERRANDO EL MODAL DE CONFIRMACION CON EL BOTON 'NO'
$("#btCerrarEditar").click(function () {
    //document.getElementById("btCerrarEditar").disabled = true;
    $("#EditarEmpleadoBonos").modal();
    $("#EditarEmpleadoBonosConfirmacion").modal('hide');
})


//FUNCION: MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnActivarEmpleadoBonos", function () {
    IDActivar = $(this).data('id');
    document.getElementById("btnActivarRegistroBono").disabled = false;
    document.getElementById("btactivarNO").disabled = false;
    $("#ActivarEmpleadoBonos").modal({ backdrop: 'static', keyboard: false });
    
    
});

//EJECUTAR LA ACTIVACION DEL REGISTRO
$("#btnActivarRegistroBono").click(function () {
    document.getElementById("btnActivarRegistroBono").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});
btactivarNO
$("#btactivarNO").click(function () {
    document.getElementById("btactivarNO").disabled = true;
    $("#ActivarEmpleadoBonos").modal('hide');
})
 