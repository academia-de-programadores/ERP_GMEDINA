//VARIABLE PARA INACTIVACION DE REGISTROS
var IDInactivar = 0;

//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
    });

//EVITAR POSTBACK DE FORMULARIOS

$('#frmEmpleadoBonosCreate').submit(function (e) {
    return false;
});
$('#frmEmpleadoBonos').submit(function (e) {
    return false;
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
            var ListaBonos = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaBonos.length; i++) {
                var FechaRegistro = FechaFormato(ListaBonos[i].cb_FechaRegistro);

                //VALIDACION PARA RECARGAR LA TABLA SIN AFECTAR LOS CHECKBOX
                var Check = "";
                //ESTA VARIABLE GUARDA CODIGO HTML DE UN CHECKBOX, PARA ENVIARLO A LA TABLA
                if (ListaBonos[i].cb_Pagado == true) {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" checked disabled>'; //SE LLENA LA VARIABLE CON UN INPUT CHEQUEADO
                } else {
                    Check = '<input type="checkbox" id="cb_Pagado" name="cb_Pagado" disabled>'; //SE LLENA LA VARIABLE CON UN INPUT QUE NO ESTA CHEQUEADO
                }

                template += '<tr data-id = "' + ListaBonos[i].cb_Id + '">' +
                    '<td>' + ListaBonos[i].per_Nombres + ' ' + ListaBonos[i].per_Apellidos + '</td>' +
                    '<td>' + ListaBonos[i].cin_DescripcionIngreso + '</td>' +
                    '<td>' + ListaBonos[i].cb_Monto + '</td>' +
                    '<td>' + FechaRegistro + '</td>' +
                    '<td>' + Check + '</td>' + //AQUI ENVIA LA VARIABLE
                    '<td>' +
                    '<button type="button" class="btn btn-primary btn-xs" id="btnEditarEmpleadoBonos">Editar</button>' +
                    '<button type="button" class="btn btn-default btn-xs" id="btnDetalleEmpleadoBonos">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyBonos').html(template);
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoBonos", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DE EMPLEADOS DEL MODAL
    $.ajax({
        url: "/EmpleadoBonos/EditGetDDLEmpleado",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #emp_IdEmpleado").empty();
            $("#Crear #emp_IdEmpleado").append("<option value='0'>Selecione una opción...</option>");
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
            $.each(data, function (i, iter) {
                $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarEmpleadoBonos").modal();
    $("#Crear #cb_Monto").val("");
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroBonos').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    var IdEmpleado = $("#Crear #emp_IdEmpleado").val();
    var Monto = $("#Crear #cb_Monto").val();

    if (IdEmpleado != 0 &&
        Monto != "" && Monto != null && Monto != undefined && Monto > 0) {

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
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else {
                cargarGridBonos();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue registrado de forma exitosa!',
                });
            }
        });

    }
    else {
        if (IdEmpleado == 0) {
            $("#Crear #emp_IdEmpleado").focus;
            mostrarError('Ingrese un colaborador válido');
        }

        if (Monto == "" || Monto == null || Monto == undefined || Monto <= 0) {
            $("#Crear #cb_Monto").focus;
            mostrarError('Ingrese un Monto válido');
        }
    }
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnEditarEmpleadoBonos", function () {
    var ID = $(this).closest('tr').data('id');
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
                        $("#Editar #cin_IdIngreso").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


                $("#EditarEmpleadoBonos").modal();
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

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateBonos").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    
    var Monto = $("#Editar #cb_Monto").val();

    if (Monto != "" && Monto != null && Monto != undefined && Monto > 0) {
    
    var data = $("#frmEmpleadoBonos").serializeArray();

    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });
        }
    });
    } else {
            $("#Editar #cb_Monto").focus;
            mostrarError('Ingrese un Monto válido');
     }
});

//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblEmpleadoBonos tbody tr td #btnDetalleEmpleadoBonos", function () {
    var ID = $(this).closest('tr').data('id');
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
                if (data.cb_Pagado) {
                    $('#Detalles #cb_Pagado').prop('checked', true);
                } else {
                    $('#Detalles #cb_Pagado').prop('checked', false);
                }

                $("#Detalles #cb_Id").val(data.cb_Id);
                $("#Detalles #cb_Monto").val(data.cb_Monto);
                $("#Detalles #cb_FechaRegistro").val(FechaRegistro);
                $("#Detalles #cb_Pagado").val(data.cb_Pagado);
                $("#Detalles #cb_UsuarioCrea").val(data.cb_UsuarioCrea);
                $("#Detalles #cb_FechaCrea").val(FechaCrea);
                $("#Detalles #cb_UsuarioModifica").val(data.cb_UsuarioModifica);
                $("#Detalles #cb_FechaModifica").val(FechaModifica);
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
                        $("#Detalles #emp_IdEmpleado").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #emp_IdEmpleado").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
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
                        $("#Detalles #cin_IdIngreso").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #cin_IdIngreso").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


                $("#DetallesEmpleadoBonos").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});


//FUNCION: MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarEmpleadoBonos", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarEmpleadoBonos").modal('hide');
    $("#InactivarEmpleadoBonos").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroBono").click(function () {
    console.log(IDInactivar);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoBonos/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo inactivar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridBonos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarEmpleadoBonos").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

