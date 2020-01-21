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
                var botonDetalles = ListaDeduccionAFP[i].dafp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleDeduccionAFP" data-id="' + ListaDeduccionAFP[i].dafp_Id + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaDeduccionAFP[i].dafp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarDeduccionAFP" data-id="' + ListaDeduccionAFP[i].dafp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionAFP[i].dafp_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarDeduccionAFP" dafpid="' + ListaDeduccionAFP[i].dafp_Id + '" data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblDeduccionAFP').dataTable().fnAddData([
                    ListaDeduccionAFP[i].dafp_Id,
                    ListaDeduccionAFP[i].per_Nombres + ' ' + ListaDeduccionAFP[i].per_Apellidos,
                    ListaDeduccionAFP[i].dafp_AporteLps,
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
    document.getElementById("btnActivarRegistroDeduccionAFP").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('dafpid');

    localStorage.setItem('id', ID);

    $("#ActivarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
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
    OcultarValidacionesCrear();
    //CARGAR INFORMACIÓN DEL DROPDOWNLIST EMPLEADO PARA EL MODAL
    $.ajax({
        url: "/DeduccionAFP/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        .done(function (data) {
            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
            $("#Crear #emp_Id").empty();
            $("#Crear #emp_Id").append("<option value='0'>Selecione una opción...</option>");
            //LLENAR EL DROPDOWNLIST
            $.each(data, function (i, iter) {
                $("#Crear #emp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });

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
    $("#Crear #emp_Id").val("0");
    $("#dafp_AporteLps").val('');
    $("#Crear #afp_Id").val("0");
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREACION
function ValidarCamposCrear(val1, val2, val3){
    var pasoValidacion = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);

    //VALIDAR EL DDL DE EMPLEADO ID
    if (val1 == "" || val1 == 0 || val1 == "0") {
        pasoValidacion = false;
        $("#Crear #validatione1d").css("display", "");
        $("#Crear #Asterisco1").addClass("text-danger");
    }
    else {
        $("#Crear #validatione1d").css("display", "none");
        $("#Crear #Asterisco1").removeClass("text-danger");
    }

    //VALIDAR EL DDL DE AFP
    if (val3 == "" || val3 == 0 || val3 == "0") {
        pasoValidacion = false;
        $("#Crear #validatione3d").css("display", "");
        $("#Crear #Asterisco3").addClass("text-danger");
    } else {
        $("#Crear #validatione3d").css("display", "none");
        $("#Crear #Asterisco3").removeClass("text-danger");
    }

    //VALIDAR EL CAMPO APORTE
    if (val2 == "" || val2 == null || val2 == undefined) {
        pasoValidacion = false;
        $("#Crear #validatione2d").css("display", "");
        $("#Crear #validatione2d2").css("display", "none");
        $("#Crear #validatione2d3").css("display", "none");
        $("#Crear #Asterisco2").addClass("text-danger");
    } else {
        $("#Crear #validatione2d").css("display", "none");
        $("#Crear #Asterisco2").removeClass("text-danger");
        if (val2 <= 0) {
            pasoValidacion = false;
            $("#Crear #validatione2d3").css("display", "");
            $("#Crear #validatione2d").css("display", "none");
            $("#Crear #validatione2d2").css("display", "none");
            $("#Crear #Asterisco2").addClass("text-danger");
        } else {
            $("#Crear #validatione2d3").css("display", "none");
            $("#Crear #Asterisco2").removeClass("text-danger");
            if (expreg.test(val2)) {
                $("#Crear #validatione2d").css("display", "none");
                $("#Crear #validatione2d2").css("display", "none");
                $("#Crear #validatione2d3").css("display", "none");
                $("#Crear #Asterisco2").removeClass("text-danger");
            } else {
                pasoValidacion = false;
                $("#Crear #validatione2d2").css("display", "");
                $("#Crear #validatione2d").css("display", "none");
                $("#Crear #validatione2d3").css("display", "none");
                $("#Crear #Asterisco2").addClass("text-danger");
            }
        }
    }
    return pasoValidacion;
}

//FUNCION: OCULTAR LAS VALIDACIONES DEL MODAL DE CREAR
function OcultarValidacionesCrear() {
    $("#Crear #validatione1d").css("display", "none");
    $("#Crear #validatione2d").css("display", "none");
    $("#Crear #validatione2d2").css("display", "none");
    $("#Crear #validatione2d3").css("display", "none");
    $("#Crear #validatione3d").css("display", "none");
    $("#Crear #Asterisco1").removeClass("text-danger");
    $("#Crear #Asterisco2").removeClass("text-danger");
    $("#Crear #Asterisco3").removeClass("text-danger");

}

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccionAFP').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    var val1 = $("#Crear #emp_Id").val();
    var val2 = $("#Crear #dafp_AporteLps").val();
    var val3 = $("#Crear #afp_Id").val();

    if(ValidarCamposCrear(val1, val2, val3)){
        document.getElementById("btnCreateRegistroDeduccionAFP").disabled = true;

        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmCreateDeduccionAFP").serializeArray();

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
                document.getElementById("btnCreateRegistroDeduccionAFP").disabled = false;

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });

                $("#Crear #emp_Id").val("0");
                $("#Crear #dafp_AporteLps").val('');
                $("#Crear #afp_Id").val("0");
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
        });
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
    OcultarValidacionesCrear();
});

$("#Editar #validatione1").css("display", "none");

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnEditarDeduccionAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/DeduccionAFP/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #dafp_Id").val(data.dafp_Id);
                $("#Editar #dafp_AporteLps").val(data.dafp_AporteLps);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION

                var SelectedIdEmpleado = data.emp_Id;
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
                        $("#Editar #emp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedIdEmpleado ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


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

            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
});

//FUNCION: VALIDAR LOS CAMPOS DE EDITAR
function ValidarCamposEditar(vale3) {
    var pasoValidacion = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);
    if (vale3 == "" || vale3 == null || vale3 == undefined) {
        pasoValidacion = false;
        $("#Editar #Validation_descripcion1").css("display", "");
        $("#Editar #Validation_descripcion2").css("display", "none");
        $("#Editar #Validation_descripcion3").css("display", "none");
        $("#Editar #e_Asterisco2").addClass("text-danger");
    } else {
        $("#Editar #Validation_descripcion1").css("display", "none");
        $("#Editar #e_Asterisco2").removeClass("text-danger");
        if (vale3 <= 0) {
            pasoValidacion = false;
            $("#Editar #Validation_descripcion2").css("display", "");
            $("#Editar #Validation_descripcion1").css("display", "none");
            $("#Editar #Validation_descripcion3").css("display", "none");
            $("#Editar #e_Asterisco2").addClass("text-danger");
        } else {
            $("#Editar #Validation_descripcion2").css("display", "none");
            $("#Editar #e_Asterisco2").removeClass("text-danger");
            if (expreg.test(vale3)) {
                $("#Editar #Validation_descripcion1").css("display", "none");
                $("#Editar #Validation_descripcion2").css("display", "none");
                $("#Editar #Validation_descripcion3").css("display", "none");
                $("#Editar #e_Asterisco2").removeClass("text-danger");
            } else {
                pasoValidacion = false;
                $("#Editar #Validation_descripcion3").css("display", "");
                $("#Editar #Validation_descripcion1").css("display", "none");
                $("#Editar #Validation_descripcion2").css("display", "none");
                $("#Editar #e_Asterisco2").addClass("text-danger");
            }
        }
    }
    return pasoValidacion;
}

//FUNCION: OCULTAR VALIDACIONES EDITAR
function OcultarValidacionesEditar() {
    $("#Editar #Validation_descripcion1").css("display", "none");
    $("#Editar #Validation_descripcion2").css("display", "none");
    $("#Editar #Validation_descripcion3").css("display", "none");
    $("#Editar #e_Asterisco2").removeClass("text-danger");
}

$("#btnEditDeduccionAFP").click(function () {
    var vale3 = $("#Editar #dafp_AporteLps").val();

    if (ValidarCamposEditar(vale3)) {
        $("#EditarDeduccionAFP").modal('hide');
        document.getElementById("btnEditDeduccionAFPConfirmar").disabled = false;
        $("#EditarDeduccionAFPConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }

    $("#EditarDeduccionAFP").submit(function (e) {
        return false;
    });

});

$(document).on("click", "#btnRegresar", function () {
    $("#EditarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
    $("#EditarDeduccionAFPConfirmacion").modal('hide');
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditDeduccionAFPConfirmar").click(function () {
    document.getElementById("btnEditDeduccionAFPConfirmar").disabled = true;

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEditDeduccionAFP").serializeArray();

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
            document.getElementById("btnEditDeduccionAFPConfirmar").disabled = true;
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
            document.getElementById("btnEditDeduccionAFPConfirmar").disabled = true;
            iziToast.error({
                title: 'Error',
                message: 'No se editó el registro, contacte al administrador',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditDeduccionAFP").submit(function (e) {
        return false;
    });
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    OcultarValidacionesEditar();
    $("#EditarDeduccionAFP").modal('hide');
});

//Detalles//
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnDetalleDeduccionAFP", function () {
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
                var FechaCrea = FechaFormato(data[0].dafp_FechaCrea);
                var FechaModifica = FechaFormato(data[0].dafp_FechaModifica);
                $("#Detalles #dafp_Id").html(data[0].dafp_Id);
                $("#Detalles #emp_Id").html(data[0].emp_Id);
                $("#Detalles #per_Nombres + #per_Apellidos").html(data[0].per_Nombres + data[0].per_Apellidos);
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
});

//Inactivar//
$(document).on("click", "#btnBack", function () {
    $("#EditarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
    $("#InactivarDeduccionAFP").modal('hide');
});

$(document).on("click", "#btnInactivarDeduccionAFP", function () {
    $("#EditarDeduccionAFP").modal('hide');
    document.getElementById("btnInactivarRegistroDeduccionAFP").disabled = false;
    $("#InactivarDeduccionAFP").modal({ backdrop: 'static', keyboard: false });
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
