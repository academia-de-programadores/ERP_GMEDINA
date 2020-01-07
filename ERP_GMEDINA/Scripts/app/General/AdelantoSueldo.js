//VARIABLE GLOBAL PARA INACTIVAR
var IDInactivar = 0;

//OBTENER SCRIPT DE FORMATEO DE FECHA

$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
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

//EVITAR POSTBACK DEL FORMULARIO CREATE
$('#frmEmpleadoAdelantos').submit(function (e) {
    return false;
});


//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridAdelantos() {
    _ajax(null,
        '/AdelantoSueldo/GetData',
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
            var ListaAdelantos = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAdelantos.length; i++) {
                var FechaAdelanto = FechaFormato(ListaAdelantos[i].adsu_FechaAdelanto);
                var Deducido = ListaAdelantos[i].adsu_Deducido == true ? 'Deducido en planilla' : 'Sin deducir';
                UsuarioModifica = ListaAdelantos[i].adsu_UsuarioModifica == null ? 'Sin modificaciones' : ListaAdelantos[i].adsu_UsuarioModifica;

                //VALIDAR SI EL REGISTRO ESTA DEDUCIDO, SI LO ESTÁ, EL BOTON DE EDITAR ESTARÁ DESHABILITADO 
                if (ListaAdelantos[i].adsu_Deducido) {
                    template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-primary btn-xs" id="btnDetalleAdelantoSueldo">Detalles</button>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs" disabled id="btnEditarAdelantoSueldo">Editar</button>' +
                    '</td>' +
                    '</tr>';
                } else {
                    template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-primary btn-xs" id="btnDetalleAdelantoSueldo">Detalles</button>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs" id="btnEditarAdelantoSueldo">Editar</button>' +
                    '</td>' +
                    '</tr>';
                }
                
            }
            
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyAdelantoSueldo').html(template);
            FullBody();
        });
   
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarAdelanto", function () {
    $("#AgregarAdelantos").modal();
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
});

//OBJETO CONSTANTE DEL DDL DE EMPLEADOS 
var cmbEmpleado = $("#emp_IdEmpleado");

//DETECTAR LOS CAMBIOS EN EL DDL DE EMPLEADOS EN LA CREACION
$(cmbEmpleado).change(() => {
    //CAPTURAR EL ID DEL EMPLEADO SELECCIONADO
    var IdEmp = cmbEmpleado.val();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Emp_Id: IdEmp })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        $("#adsu_Monto").attr("max", data);
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#AgregarAdelantos").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se recuperar el sueldo neto promedio',
        });
    });
});

//OBJETO CONSTANTE DEL DDL DE EMPLEADOS 
var cmbEmpleadoEdit = $("#frmEmpleadoBonos #emp_Id");

//DETECTAR LOS CAMBIOS EN EL DDL DE EMPLEADOS EN LA EDICION
$(cmbEmpleadoEdit).change(() => {
    //CAPTURAR EL ID DEL EMPLEADO SELECCIONADO
    var IdEmp = cmbEmpleadoEdit.val();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA CONSULTA DE SALARIO PROMEDIO
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm/10",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO
        $("#frmEmpleadoBonos #adsu_Monto").attr("max", data);
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#EditarAdelantoSueldo").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se recuperar el sueldo neto promedio',
        });
    });
});


//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAdelantos').click(function () {
    var emp_IdEmpleado = $("#emp_IdEmpleado").val();
    var adsu_RazonAdelanto = $("#adsu_RazonAdelanto").val();
    var adsu_Monto = $("#adsu_Monto").val();
    var adsu_FechaAdelanto = $("#adsu_FechaAdelanto").val();
    //VALIDACIONES
    if (emp_IdEmpleado == 0) {
        $("#Validation_descripcion1").css("display", "");
    } else {
        $("#Validation_descripcion1").css("display", "none");
    }

    if (adsu_RazonAdelanto == null || adsu_RazonAdelanto == "") {
        $("#Validation_descripcion2").css("display", "");
    } else {
        $("#Validation_descripcion2").css("display", "none");
    }

    if (adsu_Monto == null || adsu_Monto == "") {
        $("#Validation_descripcion3").css("display", "");
    } else {
        $("#Validation_descripcion3").css("display", "none");
    }

    if (adsu_FechaAdelanto == null || adsu_FechaAdelanto == "") {
        $("#Validation_descripcion4").css("display", "");
    } else {
        $("#Validation_descripcion4").css("display", "none");
    }

    if(emp_IdEmpleado != 0
        && adsu_RazonAdelanto != null && adsu_RazonAdelanto != ""
        && adsu_Monto != null && adsu_Monto != ""
        && adsu_FechaAdelanto != null && adsu_FechaAdelanto != "") {
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
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo guardar el registro',
                });
            }
            else {
                cargarGridAdelantos();
                $("#Validation_descripcion1").css("display", "none");
                $("#Validation_descripcion2").css("display", "none");
                $("#Validation_descripcion3").css("display", "none");
                $("#Validation_descripcion4").css("display", "none");

                //LIMPIAR EL FORMULARIO
                document.getElementById("frmEmpleadoAdelantos").reset();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡Se ha registrado exitosamente!',
                });
            }
        });
    }
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnEditarAdelantoSueldo", function () {
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
        idEmpSelect = data.emp_Id;
        NombreSelect = data.per_Nombres;
    })

    $.ajax({
        url: "/AdelantoSueldo/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    }).done(function (data) {
        if (data) {
            if (!data.adsu_Deducido) {
                if(data.adsu_Activo){
                    document.getElementById("inactivar").hidden = false;
                    document.getElementById("activar").hidden = true;
                } else {
                    document.getElementById("activar").hidden = false;
                    document.getElementById("inactivar").hidden = true;
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
                $("#Editar #emp_Id").empty();
                //LLENAR EL DROPDOWNLIST

                $("#Editar #emp_Id").append("<option value='" + idEmpSelect + "' selected>" + NombreSelect + "</option>");
                $.each(data, function (i, iter) {
                    $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });
            $("#Editar #adsu_IdAdelantoSueldo").val(data.adsu_IdAdelantoSueldo);
            $("#Editar #adsu_RazonAdelanto").val(data.adsu_RazonAdelanto);
            $("#Editar #adsu_Monto").val(data.adsu_Monto);


            $("#EditarAdelantoSueldo").modal();
            }
        } else if (data.adsu_Deducido) {
            iziToast.error({
                title: 'Error',
                message: 'No se puede editar un registro ded',
            });
        }
    })
});

//FUNCION: EJECUTAR EDICION DE REGISTROS
$("#btnUpdateAdelantos").click(function () {
    var data = $('#frmEmpleadoBonos').serializeArray();
    $.ajax({
        url: "/AdelantoSueldo/Edit",
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
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            FullBody();
            $("#EditarAdelantoSueldo").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });
        }
    });
});

//FUNCION: OCULTAR MODAL DE EDICION
$("#btnCerrarEditar").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: DESPLEGAR MENSAJE ANTES DE EDITAR EL REGISTRO INHABILITADO
$("#btnUpdateAdelantos2").click(function () {
    iziToast.error({
        title: 'Error',
        message: 'Debe habilitar el registro antes de editarlo',
    });
});

//FUNCION: CERRAR EL MODAL CON EL SEGUNDO BOTON DE CERRAR
$("#btnCerrarEditar2").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
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
                    $('#Detalles #adsu_Deducido').prop('checked', true);
                } else {
                    $('#Detalles #adsu_Deducido').prop('checked', false);
                }

                $("#Detalles #per_Nombres").val(data.per_Nombres);
                $("#Detalles #adsu_FechaAdelanto").val(FechaRegistro);
                $("#Detalles #adsu_RazonAdelanto").val(data.adsu_RazonAdelanto);
                $("#Detalles #adsu_Monto").val(data.adsu_Monto);

                $("#Detalles #UsuarioCrea").val(data.UsuarioCrea);                
                $("#Detalles #adsu_FechaCrea").val(FechaCrea);
                $("#Detalles #UsuarioModifica").val(data.UsuarioModifica);
                $("#Detalles #adsu_FechaModifica").val(FechaModifica);

                $("#DetallesAdelantoSueldo").modal();
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
$(document).on("click", "#btnmodalInactivarAdelantoSueldo", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarAdelantoSueldo").modal('hide');
    $("#InactivarAdelantoSueldo").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroAdelantos").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AdelantoSueldo/Inactivar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo inhabilitar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarAdelantoSueldo").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue inhabilitado de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});

//FUNCION: MOSTRAR EL MODAL DE ACTIVAR
$(document).on("click", "#btnActivarAdelantoSueldo", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarAdelantoSueldo").modal('hide');
    $("#ActivarAdelantoSueldo").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnActivarRegistroAdelantos").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AdelantoSueldo/Activar/" + IDInactivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo habilitar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridAdelantos();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarAdelantoSueldo").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue habilitado de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});



//---------------------------------------------------------------------------------------
//FUNCION: PRIMERA FASE DE INACTIVACION DE REGISTROS, MOSTRAR MODAL CON LA MENSAJE DE CONFIRMACION
$("#btnInactivarAdelantos").click(function () {
    $("#EditarAdelantos").modal('hide');
    $("#InactivarAdelantos").modal();
});

//FUNCION: SEGUNDA FASE DE EDICION DE REGISTROS, REALIZAR LA EJECUCION PARA INACTIVAR EL REGISTRO
//$("#btnInactivarRegistroTipoDeducciones").click(function () {
//    $.ajax({
//        url: "/AdelantoSueldo/Inactivar/" + inactivar,
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ ID: inactivar })
//    }).done(function (data) {
//        $("#InactivarTipoDeducciones").modal('hide');
//        //Refrescar la tabla de TipoDeducciones
//        cargarGridAdelantos();
//        //Mensaje de error si no hay data
//        iziToast.success({
//            title: 'Exito',
//            message: 'Se ha inactivado el registro',
//        });
//    });
//});

//FUNCION: OCULTAR MODAL DE CREACION
$("#btnCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
    FullBody();
});


$("#btnCerrarCreate").click(function () {
    $("#AgregarAdelantos").modal('hide');
    $("#Validation_descripcion1").css("display", "none");
    $("#Validation_descripcion2").css("display", "none");
    $("#Validation_descripcion3").css("display", "none");
    $("#Validation_descripcion4").css("display", "none");
    FullBody();
});

//FUNCION: OCULTAR MODAL DE EDICION
$("#btnCerrarEditar").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
    FullBody();
});

//FUNCION: OCULTAR MODAL DE INACTIVACION
$("#btnCerrarInactivar").click(function () {
    $("#InactivarAdelantoSueldo").modal('hide');
    FullBody();
});

//FUNCION: OCULTAR MODAL DE ACTIVACION
$("#btnCerrarActivar").click(function () {
    $("#ActivarAdelantoSueldo").modal('hide');
    FullBody();
});

//FUNCION: OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
    FullBody();
});

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#Editar #Validation_descripcion").css("display", "none");
    FullBody();
});

//FUNCION: HABILITAR EL DATAANNOTATION AL DESPLEGAR EL MODAL
$("#btnCerrar").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
    FullBody();
});

//$("#frmTipoDeduccionCreate").submit(function (event) {
//    event.preventDefault();
//});

//$("#frmTipoDeduccionEdit").submit(function (event) {
//    event.preventDefault();
//});