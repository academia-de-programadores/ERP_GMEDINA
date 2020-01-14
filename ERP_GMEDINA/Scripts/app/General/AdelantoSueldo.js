﻿//VARIABLE GLOBAL PARA INACTIVAR
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
            var ListaAdelantos = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAdelantos.length; i++) {
                var FechaAdelanto = FechaFormato(ListaAdelantos[i].adsu_FechaAdelanto);
                var Deducido = ListaAdelantos[i].adsu_Deducido == true ? 'Deducido en planilla' : 'Sin deducir';
                var Activo = ListaAdelantos[i].adsu_Activo == true ? 'Activo' : 'Inactivo';
                UsuarioModifica = ListaAdelantos[i].adsu_UsuarioModifica == null ? 'Sin modificaciones' : ListaAdelantos[i].adsu_UsuarioModifica;

                var botonDetalles = ListaAdelantos[i].adsu_Activo == true ? '<button data-id = "' + ListaAdelantos[i].cb_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleAdelantoSueldo">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAdelantos[i].adsu_Activo == true ? '<button data-id = "' + ListaAdelantos[i].cb_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarAdelantoSueldo">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAdelantos[i].adsu_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAdelantos[i].cb_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarRegistroAdelantos">Activar</button>' : '' : '';
                var dataId = ListaAdelantos[i].adsu_IdAdelantoSueldo;

                template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '</td>' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' + Activo + '</td>' +
                    '<td>' +
                    botonDetalles +
                    botonEditar +
                    botonActivar +
                    '</td>' +
                    '</tr>';
            }
            
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyAdelantoSueldo').html(template);
            FullBody();
        });
   
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarAdelanto", function () {
    $("#Crear #adsu_RazonAdelanto").val("");
    $("#Crear #adsu_Monto").val("");
    $("#Crear #emp_IdEmpleado").val(0);
    $("#Crear #adsu_FechaAdelanto").val("");

    $.ajax({
        url: "/AdelantoSueldo/EmpleadoGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
        $("#Crear #emp_IdEmpleado").empty();
        //LLENAR EL DROPDOWNLIST
        $("#Crear #emp_IdEmpleado").append("<option value=0>Selecione una opción...</option>");
        $.each(data, function (i, iter) {
            $("#Crear #emp_IdEmpleado").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
        });
    });
    $("#AgregarAdelantos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
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

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAdelantos').click(function () {
    var Razon =  $("#Crear #adsu_RazonAdelanto").val();
    var Monto = $("#Crear #adsu_Monto").val();
    var IdEmp = $("#Crear #emp_IdEmpleado").val();
    var Fecha = $("#Crear #adsu_FechaAdelanto").val();
    
    
    if ($("#Crear #adsu_Monto").val() <= MaxSueldoCreate
        && IdEmp != 0
        && Razon != "" && Razon != null && Razon != undefined
        && Monto != "" && Monto != null && Monto != undefined && Monto > 0
        && Fecha != "" && Fecha != null && Fecha != undefined)
    {
        //MOSTRAR EL SPINNER DE CARGA
        mostrarCargandoCrear();
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmEmpleadoAdelantos").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/AdelantoSueldo/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarAdelantos").modal('hide');
            ocultarCargandoCrear();
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
            else {
                OcultarValidaciones();
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
    else {
        //VALIDAR LOS TIPOS DE ERRORES EN LOS CAMPOS
        ValidarCamposCrear(Razon, Monto, IdEmp, Fecha);
    }
});

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(Razon, Monto, IdEmp, Fecha) {

    if (IdEmp == 0) {
        iziToast.error({
            title: 'Error',
            message: 'Ingrese un colaborador válido',
        });
    } else if (Razon == "" || Razon == null || Razon == undefined) {
        iziToast.error({
            title: 'Error',
            message: 'Campo Razón requerido',
        });
    } else if (Monto == "" || Monto == null || Monto == undefined) {
        iziToast.error({
            title: 'Error',
            message: 'Campo Monto requerido',
        });
    } else if (Monto <= 0) {
        iziToast.error({
            title: 'Error',
            message: 'El campo Monto debe ser mayor que cero',
        });
    } else if (Fecha == "" || Fecha == null || Fecha == undefined) {
        iziToast.error({
            title: 'Error',
            message: 'Campo Fecha requerido',
        });
    }
    else if ($("#Crear #adsu_Monto").val() > MaxSueldoCreate) {
        //MENSAJE DE EEROR EN CASO QUE EL MONTO SEA MAYOR AL SUELDO PROMEDIO 
        iziToast.error({
            title: 'Error',
            message: 'El monto ingresado es mayor que el sueldo promedio del colaborador',
        });
        //IGUALAR EL MONTO AL SUELDO PROMEDIO
        $("#Crear #adsu_Monto").val('');
        //$("#Crear #adsu_Monto").placeholder = 'El monto debe ser menor o igual que ' + MaxSueldoCreate;
        document.getElementById("adsu_Monto").placeholder = 'El sueldo promedio es ' + MaxSueldoCreate;
        //$("#Crear #adsu_Monto").val(MaxSueldoCreate);
    }

    if (Razon == "" || Razon == null || Razon == undefined) {
        $("#Crear #Validation_descripcion1").css("display", "");
    } else {
        $("#Crear #Validation_descripcion1").css("display", "none");
    }
    if (Monto == "" || Monto == null || Monto == undefined || Monto <= 0) {
        $("#Crear #Validation_descripcion2").css("display", "");
    } else {
        $("#Crear #Validation_descripcion2").css("display", "none");
    }
    if (Fecha == "" || Fecha == null || Fecha == undefined) {
        $("#Crear #Validation_descripcion3").css("display", "");
    } else {
        $("#Crear #Validation_descripcion3").css("display", "none");
    }
}

//FUNCION: OCULTAR LOS LABEL DE MENSAJE DE ERROR EN MODAL DE CREAR
function OcultarValidaciones(){
    $("#Crear #Validation_descripcion1").css("display", "none");
    $("#Crear #Validation_descripcion2").css("display", "none");
    $("#Crear #Validation_descripcion3").css("display", "none");
    document.getElementById("adsu_Monto").placeholder = '';
}

//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE CREAR
$('#btnCerrarCrearAdelanto').click(function () {
    OcultarValidaciones()
});
$('#IconCerrar').click(function () {
    OcultarValidaciones()
});
$("#AgregarAdelantos").on('hidden.bs.modal', function () {
    OcultarValidaciones()
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnEditarAdelantoSueldo", function () {
    $("#Editar #emp_Id").empty();
    var ID = $(this).closest('tr').data('id');
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
        idEmpSelect = data.emp_Id;
        NombreSelect = data.per_Nombres;
        //LLENAR EL DROPDOWNLIST
        $("#Editar #emp_Id").append("<option value='" + idEmpSelect + "' selected>" + NombreSelect + "</option>");

        $.ajax({
            url: "/AdelantoSueldo/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ID })
        }).done(function (data) {
            if (data) {
                //HABILITAR O INHABILITAR EL BOTON DE EDITAR SI ESTA DEDUCIDO O NO 
                if (data.adsu_Deducido) {
                    document.getElementById("btnUpdateAdelantos").disabled = true;
                } else {
                    document.getElementById("btnUpdateAdelantos").disabled = false;
                }
                var SelectedIdEmp = data.emp_Id;

                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/AdelantoSueldo/EmpleadoGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                }).done(function (data) {
                    //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                    $.each(data, function (i, iter) {
                        $("#Editar #emp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                    });
                });
                $("#Editar #adsu_IdAdelantoSueldo").val(data.adsu_IdAdelantoSueldo);
                $("#Editar #adsu_RazonAdelanto").val(data.adsu_RazonAdelanto);
                $("#Editar #adsu_Monto").val(data.adsu_Monto);

                //MOSTRAR EL MODAL Y BLOQUEAR EL FONDO
                $("#EditarAdelantoSueldo").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");

            } else if (data.adsu_Deducido) {
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
});

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
    ocultarCargandoEditar();
    $("#EditarAdelantoSueldo").modal('hide');
    $("#ConfirmarEdicion").modal();
});

//FUNCION: EJECUTAR EDICION DE REGISTROS
$("#btnConfirmarEditar").click(function () {
    mostrarCargandoEditar();

    //OBTENER EL ID DEL EMPLEADO 
    var IdEmp = $("#frmAdelantosEdit #emp_Id").val();
    //RECUPERAR EL MONTO MAXIMO PARA ADELANTO DE SUELDO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IdEmp })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        //EJECUTAR LA VALIDACION PARA LA EDICIÓN
        if(ValidarCamposEditar($('#EditarAdelantoSueldo #emp_Id'), $('#EditarAdelantoSueldo #adsu_RazonAdelanto'), $('#EditarAdelantoSueldo #adsu_Monto')))
        if ($("#Editar #adsu_Monto").val() <= data) {
            var data = $('#frmAdelantosEdit').serializeArray();
            $.ajax({
                url: "/AdelantoSueldo/Edit",
                method: "POST",
                data: data
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
                    FullBody();
                    $("#ConfirmarEdicion").modal('hide');
                    ocultarCargandoEditar();
                    //Setear la variable de SueldoAdelantoMaximo a cero 
                    MaxSueldoEdit = 0;
                    //Mensaje de exito de la edicion
                    iziToast.success({
                        title: 'Éxito',
                        message: '¡El registro se editó de forma exitosa!',
                    });
                }
            });
        }
        else {
            //MENSAJE DE EEROR EN CASO QUE EL MONTO SEA MAYOR AL SUELDO PROMEDIO 
            iziToast.error({
                title: 'Error',
                message: 'El monto ingresado es mayor que el sueldo promedio del colaborador',
            });
            $("#ConfirmarEdicion").modal('hide');
            ocultarCargandoEditar();
            //IGUALAR EL MONTO AL SUELDO PROMEDIO
            $("#Editar #adsu_Monto").val('');
            //$("#Editar #adsu_Monto").val(data);
            document.getElementById("adsu_Monto").placeholder = 'El sueldo promedio es ' + data;
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

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(colaborador, razon, monto) {
    var pasoValidacion = true;

    if (colaborador.val() == '') {
        pasoValidacion = false;
        //Codigo para mostrar el span de validacion
        //Hacerle focus al input
        $("#ConfirmarEdicion").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'Ha ocurrido un problema con el campo colaborador',
        });

        $(colaborador).focus();
    }

    if (razon == null || razon.val() == '') {
        pasoValidacion = false;
        $('#adsu_RazonAdelantoValidacion').show();
        razon.focus();
    } else {
        $('#adsu_RazonAdelantoValidacion').hide();
    }

    if (monto == null || monto.val() == '') {
        pasoValidacion = false;
        $('#adsu_MontoValidacion').show();
        monto.focus();
    } else {
        $('#adsu_MontoValidacion').hide();
    }

    return pasoValidacion;
}

//FUNCION: CERRAR EL MODAL DE CONFIRMACION AL EDITAR, (CON EL BOTON DE CERRAR)
$("#btnCerrarConfirmarEditar").click(function () {
    $("#ConfirmarEdicion").modal('hide');
    //document.getElementById("adsu_Monto").placeholder = '';
});

//FUNCION: OCULTAR MODAL DE EDICION CON EL BOTON CERRAR DEL MODAL DE EDITAR
$("#btnCerrarEditar").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
    document.getElementById("adsu_Monto").placeholder = '';
    FullBody();
});

//FUNCION: CERRAR EL MODAL DE EDITAR CON EL BOTON DE X
$("#IconCerrarEditar").click(function () {
    document.getElementById("adsu_Monto").placeholder = '';
    FullBody();
});

//FUNCION: MOSTRAR EL MODAL DE DETALLES
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnDetalleAdelantoSueldo", function () {
    var ID = $(this).closest('tr').data('id');
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

                var FechaRegistro = FechaFormato(data.adsu_FechaAdelanto).toString();
                var FechaCrea = FechaFormato(data.adsu_FechaCrea);
                var FechaModifica = FechaFormato(data.adsu_FechaModifica);

                if (data.adsu_Deducido) {
                    $("#Detalles #adsu_Deducido").html("Si");
                } else {
                    $("#Detalles #adsu_Deducido").html("No");
                }

                $("#Detalles #per_Nombres").html(data.per_Nombres);
                $("#Detalles #adsu_FechaAdelanto").html(FechaRegistro);
                $("#Detalles #adsu_RazonAdelanto").html(data.adsu_RazonAdelanto);
                $("#Detalles #adsu_Monto").html(data.adsu_Monto);

                $("#Detalles #UsuarioCrea").html(data.UsuarioCrea);
                $("#Detalles #adsu_FechaCrea").html(FechaCrea);
                $("#Detalles #UsuarioModifica").html(data.UsuarioModifica);
                $("#Detalles #adsu_FechaModifica").html(FechaModifica);

                $("#DetallesAdelantoSueldo").modal();
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

//FUNCION: MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarAdelantoSueldo", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarAdelantoSueldo").modal('hide');
    $("#InactivarAdelantoSueldo").modal();
});

//FUNCION: PRIMERA FASE DE INACTIVACION DE REGISTROS, MOSTRAR MODAL CON MENSAJE DE CONFIRMACION
$("#btnInactivarAdelantos").click(function () {
    $("#EditarAdelantos").modal('hide');
    $("#InactivarAdelantos").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroAdelantos").click(function () {
    mostrarCargandoInactivar();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AdelantoSueldo/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
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
            ocultarCargandoInactivar();
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
    FullBody();
});

//FUNCION: MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnActivarRegistroAdelantos", function () {
    IDActivar = $(this).closest('tr').data('id');
    $("#ActivarAdelantoSueldo").modal();
});

//EJECUTAR ACTIVACION DEL REGISTRO EN EL MODAL
$("#btnActivarRegistroAdelantosModal").click(function () {
    mostrarCargandoActivar();
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
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarAdelantoSueldo").modal('hide');
            ocultarCargandoActivar();
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
    FullBody();
});

//---------------------------------------FUNCIONES SPINNER---------------------------------------
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

//MODAL AGREGAR
function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

const btnGuardar = $('#btnCreateRegistroAdelantos'),


cargandoCrearcargandoCrear = $('#cargandoCrear')


cargandoCrear = $('#cargandoCrear')//Div que aparecera cuando se le de click en crear


//FUNCIONES SPINNER MODAL EDITAR
function mostrarCargandoEditar() {
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarCargandoEditar() {
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

const btnEditar = $('#btnConfirmarEditar'),


cargandoEditarcargandoEditar = $('#cargandoEditar')


cargandoEditar = $('#cargandoEditar')//Div que aparecera cuando se le de click en crear

//FUNCIONES SPINNER MODAL INACTIVAR
function mostrarCargandoInactivar() {
    btnInactivar.hide();
    cargandoInactivar.html(spinner());
    cargandoInactivar.show();
}

function ocultarCargandoInactivar() {
    btnInactivar.show();
    cargandoInactivar.html('');
    cargandoInactivar.hide();
}
const btnInactivar = $('#btnInactivarRegistroAdelantos'),


cargandoInactivarcargandoInactivar = $('#cargandoInactivar')


cargandoInactivar = $('#cargandoInactivar')//Div que aparecera cuando se le de click en crear


//FUNCIONES SPINNER MODAL ACTIVAR
function mostrarCargandoActivar() {
    btnActivar.hide();
    cargandoActivar.html(spinner());
    cargandoActivar.show();
}

function ocultarCargandoActivar() {
    btnActivar.show();
    cargandoActivar.html('');
    cargandoActivar.hide();
}

const btnActivar = $('#btnActivarRegistroAdelantosModal'),


cargandoActivarcargandoActivar = $('#cargandoActivar')


cargandoActivar = $('#cargandoActivar')//Div que aparecera cuando se le de click en crear