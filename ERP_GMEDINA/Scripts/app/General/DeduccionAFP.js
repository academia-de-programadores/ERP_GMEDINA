//Obtención de Script para Formateo de Fechas
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

$(document).ready(function () {
    $.ajax({
        url: "/DeduccionAFP/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $('#Crear #emp_IdCrear').select2({
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


//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/DeduccionAFP/GetData',
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
            var ListaDeduccionAFP = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblDeduccionAFP').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeduccionAFP.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeduccionAFP[i].dafp_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleDeduccionAFP" data-id="' + ListaDeduccionAFP[i].dafp_Id + '">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaDeduccionAFP[i].dafp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarDeduccionAFP" data-id="' + ListaDeduccionAFP[i].dafp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionAFP[i].dafp_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnActivarDeduccionAFP" dafpid="' + ListaDeduccionAFP[i].dafp_Id + '" data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblDeduccionAFP').dataTable().fnAddData([
                    ListaDeduccionAFP[i].dafp_Id,
                    ListaDeduccionAFP[i].per_Nombres + ' ' + ListaDeduccionAFP[i].per_Apellidos,
                    (ListaDeduccionAFP[i].dafp_AporteLps % 1 == 0) ? ListaDeduccionAFP[i].dafp_AporteLps + ".00" : ListaDeduccionAFP[i].dafp_AporteLps,
                    ListaDeduccionAFP[i].afp_Descripcion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
}

//Activar
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnActivarDeduccionAFP", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("DeduccionesAFP/Activar");

    if (validacionPermiso.status == true) {
        document.getElementById("btnActivarRegistroDeduccionAFP").disabled = false;
        var ID = $(this).closest('tr').data('id');

        var ID = $(this).attr('dafpid');

        localStorage.setItem('id', ID);

        $("#ActivarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
    }
   
})

$("#btnActivarRegistroDeduccionAFP").click(function () {
    document.getElementById("btnActivarRegistroDeduccionAFP").disabled = true;

    let ID = localStorage.getItem('id')

    $.ajax({
        url: "/DeduccionAFP/Activar",
        method: "POST",
        data: { id: ID }
    }).done(function (data) {
        $("#ActivarDeduccionAFP").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
})

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarDeduccionAFP", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("DeduccionesAFP/Create");

    if (validacionPermiso.status == true) {
        $("#Crear #emp_IdCrear").val('').trigger('change.select2');

        OcultarValidacionesCrear();
        OcultarValidacionesEdit();

        //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
        $.ajax({
            url: "/DeduccionAFP/EditGetAFPDDL",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (data) {
                //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                $("#Crear #afp_Id").empty();
                $("#Crear #afp_Id").append("<option value='0'>Selecione una opción...</option>");
                //LLENAR EL DROPDOWNLIST
                $.each(data, function (i, iter) {
                    $("#Crear #afp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });

        //MOSTRAR EL MODAL DE AGREGAR
        $("#AgregarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
        $("#dafp_AporteLps").val('');
        $("#Crear #afp_Id").val("0");
        $('#Crear #dafp_DeducirISR').prop('checked', false);
        document.getElementById("btnCreateRegistroDeduccionAFP").disabled = false;
    }
   
});

//Validar campos Crear y Editar
function ValidarCampos(empId, Aporte, AFP) {
    //VALIDACIONES DEL CAMPO FECHA
    var estabueno = true;
    let hayAlgoAporte = false;
    if (Aporte != "-1") {
        if (Aporte == "" || Aporte == null || Aporte == undefined) {
            estabueno = false;
            $("#Crear #validatione2d, #Editar #Validation_descripcion1").css("display", "");
            $("#Crear #validatione2d2, #Editar #Validation_descripcion2").css("display", "none");
            $("#Crear #validatione2d3, #Editar #Validation_descripcion3").css("display", "none");
            $("#Crear #Asterisco2, #Editar #e_Asterisco2").css("color", "red");
        } else {
            hayAlgoAporte = true;
            $("#Crear #validatione2d, #Editar #Validation_descripcion1").css("display", "none");
            $("#Crear #Asterisco2, #Editar #e_Asterisco2").css("color", "#676a6c");
        }
        if (hayAlgoAporte)
            if (Aporte == "0" || Aporte == "0.00") {
                estabueno = false;
                $("#Crear #Asterisco2, #Editar #e_Asterisco2").css("color", "red");
                $("#Crear #validatione2d3, #Editar #Validation_descripcion3").css("display", "");
            } else {
                $("#Crear #Asterisco2, #Editar #e_Asterisco2").css("color", "#676a6c");
                $("#Crear #validatione2d3, #Editar #Validation_descripcion3").css("display", "none");
            }
    }
    if (empId != "-1") {
        if (empId == null || emp_Id == "" || empId == 0) {
            estabueno = false;
            $("#Crear #validatione1d, #Editar #e_validatione1d").css("display", "");
            $("#Crear #Asterisco1, #Editar #e_Asterisco1").css("color", "red");
        } else {
            $("#Crear #validatione1d, #Editar #e_validatione1d").css("display", "none");
            $("#Crear #Asterisco1, #Editar #e_Asterisco1").css("color", "#676a6c");
        }
    }
    if (AFP != "-1") {
        if (AFP == 0 || AFP == "0") {
            estabueno = false;
            $("#Crear #validatione3d, #Editar #Validation_AFPEdit").css("display", "");
            $("#Crear #Asterisco3, #Editar #e_Asterisco3").css("color", "red");
        } else {
            $("#Crear #validatione3d, #Editar #Validation_AFPEdit").css("display", "none");
            $("#Crear #Asterisco3, #Editar #e_Asterisco3").css("color", "#676a6c");
        }
    }
    return estabueno;
}

//FUNCION: OCULTAR LAS VALIDACIONES DEL MODAL DE CREAR
function OcultarValidacionesCrear() {
    $("#Crear #validatione1d").css("display", "none");
    $("#Crear #validatione2d").css("display", "none");
    $("#Crear #validatione2d2").css("display", "none");
    $("#Crear #validatione2d3").css("display", "none");
    $("#Crear #validatione3d").css("display", "none");
    $("#Crear #Asterisco1").css("color", "#676a6c");
    $("#Crear #Asterisco2").css("color", "#676a6c");
    $("#Crear #Asterisco3").css("color", "#676a6c");
};

function OcultarValidacionesEdit() {
    $("#Editar #Validation_descripcion1").css("display", "none");
    $("#Editar #Validation_descripcion2").css("display", "none");
    $("#Editar #Validation_descripcion3").css("display", "none");
    $("#Editar #e_validatione1d").css("display", "none");
    $("#Editar #Validation_AFPEdit").css("display", "none");
    $("#Editar #e_Asterisco1").css("color", "#676a6c");
    $("#Editar #e_Asterisco2").css("color", "#676a6c");
    $("#Editar #e_Asterisco3").css("color", "#676a6c");
};


//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccionAFP').click(function () {
    document.getElementById("btnCreateRegistroDeduccionAFP").disabled = true;

    var empId = $("#Crear #emp_IdCrear").val();
    var Aporte = $("#Crear #dafp_AporteLps").val();
    var AFP = $("#Crear #afp_Id").val();
    var DeducirISR = $("#Crear #dafp_DeducirISR").val();
    //Obtener valor del checkbox
    if ($('#Crear #dafp_DeducirISR').is(':checked')) {
        DeducirISR = true;
    }
    else {
        DeducirISR = false;
    }

    if (ValidarCampos(empId, Aporte, AFP)) {
        // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)        
        var dataSinFormato = $("#frmCreateDeduccionAFP").serializeArray();
        var data = {
            __RequestVerificationToken: dataSinFormato[0].value,
            emp_Id: dataSinFormato[1].value,
            dafp_AporteLps: FormatearDecimal(dataSinFormato[2].value),
            afp_Id: dataSinFormato[3].value,
            dafp_DeducirISR: DeducirISR
        };
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionAFP/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {

                cargarGridDeducciones();

                $("#Crear #dafp_AporteLps").val('');

                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarDeduccionAFP").modal('hide');
                
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });

                $("#Crear #emp_IdCrear").val("0");
                $("#Crear #dafp_AporteLps").val('');
                $("#Crear #afp_Id").val("0");
                $('#Crear #dafp_DeducirISR').prop('checked', false);
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
                document.getElementById("btnCreateRegistroDeduccionAFP").disabled = true;
            }
        });
    }
    else {
        document.getElementById("btnCreateRegistroDeduccionAFP").disabled = false;
        ValidarCampos(empId, Aporte, AFP);
    }
});
// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#frmCreateDeduccionAFP").submit(function (e) {
    return false;
});

$("#btnCerrarAgregar").click(function () {
    $("#AgregarDeduccionAFP").modal('hide');
    $("#emp_Id").val("0");
    $("#dafp_AporteLps").val('');
    $("#afp_Id").val("0");
    $('#Crear #dafp_DeducirISR').prop('checked', false);
    OcultarValidacionesCrear();
    OcultarValidacionesEdit();
});
$("#Editar #validatione1").css("display", "none");

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnEditarDeduccionAFP", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("DeduccionesAFP/Edit");

    if (validacionPermiso.status == true) {
        let itemEmpleado = localStorage.getItem('idEmpleado');
        let dataEmp = table.row($(this).parents('tr')).data(); //obtener la data de la fila seleccionada
        if (itemEmpleado != null) {
            $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove();
            localStorage.removeItem('idEmpleado');
        }

        OcultarValidacionesCrear();
        OcultarValidacionesEdit();
        var ID = $(this).data('id');
        $.ajax({
            url: "/DeduccionAFP/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })

            .done(function (data) {
                if (data.dafp_DeducirISR) {
                    $('#Editar #dafp_DeducirISREdit').prop('checked', true);
                }
                else {
                    $('#Editar #dafp_DeducirISREdit').prop('checked', false);
                }
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {

                    let idEmpSelect = data.emp_Id;
                    let NombreSelect = dataEmp[1];
                    $("#Editar #dafp_Id").val(data.dafp_Id);
                    $("#Editar #dafp_AporteLps").val(data.dafp_AporteLps);
                    $("#Editar #dafp_DeducirISREdit").val(data.dafp_DeducirISR);
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION

                    $('#Editar #emp_Id').val(idEmpSelect).trigger('change');

                    let valor = $('#Editar #emp_Id').val();

                    if (valor == null) {
                        $("#Editar #emp_Id").prepend("<option value='" + idEmpSelect + "' selected>" + NombreSelect + "</option>").trigger('change');
                        localStorage.setItem('idEmpleado', idEmpSelect);
                    }

                    var SelectedIdAFP = data.afp_Id;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
                    $.ajax({
                        url: "/DeduccionAFP/EditGetAFPDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            $("#Editar #afp_Id").empty();
                            //LLENAR EL DROPDOWNLIST
                            $.each(data, function (i, iter) {
                                $("#Editar #afp_Id").append("<option" + (iter.Id == SelectedIdAFP ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });
                        });
                    $("#DetallesDeduccionAFP").modal('hide');
                    $("#EditarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
                    document.getElementById("btnEditDeduccionAFP").disabled = false;

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

//FUNCION: OCULTAR VALIDACIONES EDITAR
function OcultarValidacionesEditar() {
    $("#Editar #Validation_descripcion1").css("display", "none");
    $("#Editar #Validation_descripcion2").css("display", "none");
    $("#Editar #Validation_descripcion3").css("display", "none");
    $("#Editar #e_validatione1d").css("display", "none");
    $("#Editar #Validation_AFPEdit").css("display", "none");
    $("#Editar #e_Asterisco1").css("color", "#676a6c");
    $("#Editar #e_Asterisco2").css("color", "#676a6c");
    $("#Editar #e_Asterisco3").css("color", "#676a6c");
}

$("#btnEditDeduccionAFP").click(function () {
    var empId = $("#Editar #emp_Id").val();
    var Aporte = $("#Editar #dafp_AporteLps").val();
    var AFP = $("#Editar #afp_Id").val();
    if (ValidarCampos(empId, Aporte, AFP)) {
        $("#EditarDeduccionAFP").modal('hide');
        document.getElementById("btnEditDeduccionAFPConfirmar").disabled = false;
        $("#EditarDeduccionAFPConfirmacion").modal({ backdrop: 'static', keyboard: false });
        document.getElementById("btnEditDeduccionAFPConfirmar").disabled = false;
    }
    else {
        ValidarCampos(empId, Aporte, AFP);
    }
});

$("#EditarDeduccionAFP").submit(function (e) {
    return false;
});

$(document).on("click", "#btnRegresar", function () {
        $("#EditarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
        $("#EditarDeduccionAFPConfirmacion").modal('hide');
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditDeduccionAFPConfirmar").click(function () {
    document.getElementById("btnEditDeduccionAFPConfirmar").disabled = true;

    if ($('#Editar #dafp_DeducirISREdit').is(':checked')) {
        dafp_DeducirISREdit = true;
    }
    else {
        dafp_DeducirISREdit = false;
    }


    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var dataSinFormato = $("#frmEditDeduccionAFP").serializeArray();
    var data = {
        __RequestVerificationToken: dataSinFormato[0].value,
        dafp_Id: dataSinFormato[1].value,
        emp_Id: dataSinFormato[2].value,
        dafp_AporteLps: FormatearDecimal(dataSinFormato[3].value),
        afp_Id: dataSinFormato[4].value,
        dafp_DeducirISR: dafp_DeducirISREdit
    };
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionAFP/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {

            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarDeduccionAFPConfirmacion").modal('hide');
            $("#EditarDeduccionAFP").modal('hide');
            $("btnEditDeduccionAFPConfirmar").disabled = true;
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
        else {
            $("#EditarDeduccionAFPConfirmacion").modal('hide');
            $("#EditarDeduccionAFP").modal('hide');
            $("btnEditDeduccionAFPConfirmar").disabled = true;
            iziToast.error({
                title: 'Error',
                message: 'No se editó el registro, contacte al administrador',
            });
            document.getElementById("btnEditDeduccionAFPConfirmar").disabled = false;
        }
    });
});

// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#frmEditDeduccionAFP").submit(function (e) {
    return false;
});


//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    OcultarValidacionesEditar();
    OcultarValidacionesCrear();
    $("#EditarDeduccionAFP").modal('hide');
    $('#Editar #dafp_DeducirISR').prop('checked', false);
});

//Detalles//
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnDetalleDeduccionAFP", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("DeduccionesAFP/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        $.ajax({
            url: "/DeduccionAFP/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    if (data[0].dafp_DeducirISR) {
                        $("#Detalles #dafp_DeducirISRDetails").html("Si");
                    }
                    else {
                        $("#Detalles #dafp_DeducirISRDetails").html("No");
                    }

                    var FechaCrea = FechaFormato(data[0].dafp_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].dafp_FechaModifica);
                    $("#Detalles #dafp_Id").html(data[0].dafp_Id);
                    $("#Detalles #emp_Id").html(data[0].per_Nombres + ' ' + data[0].per_Apellidos);
                    $("#Detalles #emp_CuentaBancaria").html(data[0].emp_CuentaBancaria);
                    $("#Detalles #dafp_AporteLps").html(data[0].dafp_AporteLps);
                    $("#Detalles #afp_Id").html(data[0].afp_Id);
                    $("#Detalles #afp_Descripcion").html(data[0].afp_Descripcion);
                    $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #dafp_UsuarioCrea").html(data[0].dafp_UsuarioCrea);
                    $("#Detalles #dafp_FechaCrea").html(FechaCrea);
                    data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #dafp_UsuarioModifica").html(data[0].dafp_UsuarioModifica);
                    $("#Detalles #dafp_FechaModifica").html(FechaModifica);

                    var SelectedIdEmpleado = data[0].emp_Id;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST EMPLEADO PARA EL MODAL
                    $.ajax({
                        url: "/DeduccionAFP/EditGetEmpleadoDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                        })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            //LLENAR EL DROPDOWNLIST
                            $.each(data, function (i, iter) {
                                if (iter.Id == SelectedIdEmpleado) {
                                    $("#Detalles #emp_Id").html(iter.Descripcion);
                                }
                            });
                        });

                    var SelectedIdAFP = data[0].afp_Id;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
                    $.ajax({
                        url: "/DeduccionAFP/EditGetAFPDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                        })
                        .done(function (data) {
                            //LLENAR EL DROPDOWNLIST
                            $.each(data, function (i, iter) {
                                if (iter.Id == SelectedIdAFP) {
                                    $("#Detalles #afp_Id").html(iter.Descripcion);
                                }
                            });
                        });

                    $("#DetallesDeduccionAFP").modal({ backdrop: 'static', keyboard: false });

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

//Inactivar//
$(document).on("click", "#btnBack", function () {
    $("#EditarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
    $("#InactivarDeduccionAFP").modal('hide');
});

$(document).on("click", "#btnInactivarDeduccionAFP", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("DeduccionesAFP/Inactivar");

    if (validacionPermiso.status == true) {
        $("#EditarDeduccionAFP").modal('hide');
        document.getElementById("btnInactivarRegistroDeduccionAFP").disabled = false;
        $("#InactivarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
    }
  
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccionAFP").click(function () {
    document.getElementById("btnInactivarRegistroDeduccionAFP").disabled = true;
    var data = $("#frmDeduccionAFPInactivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionAFP/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            $("#InactivarDeduccionAFP").modal('hide');
            document.getElementById("btnInactivarRegistroDeduccionAFP").disabled = true;
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarDeduccionAFP").modal('hide');
            document.getElementById("btnInactivarRegistroDeduccionAFP").disabled = true;
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmDeduccionAFPInactivar").submit(function (e) {
        return false;
    });
});

function FormatearDecimal(StringValue) {
    //SEGMENTAR LA CADENA DE MONTO
    var indices = StringValue.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i <= indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    return MontoFormateado = parseFloat(MontoFormateado);
}