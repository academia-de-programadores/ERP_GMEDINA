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

//EVITAR POSTBACK DEL FORMULARIO
//$('#frmAdelantoSueldoEdit').submit(function (e) {
//    return false;
//});

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
                var Activo = ListaAdelantos[i].adsu_Activo == true ? 'Activo' : 'Inactivo';
                UsuarioModifica = ListaAdelantos[i].adsu_UsuarioModifica == null ? 'Sin modificaciones' : ListaAdelantos[i].adsu_UsuarioModifica;

                //VALIDAR SI EL REGISTRO ESTA DEDUCIDO, SI LO ESTÁ, EL BOTON DE EDITAR ESTARÁ DESHABILITADO 
                if (ListaAdelantos[i].adsu_Deducido) {
                    template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '</td>' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' + Activo + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-primary btn-xs" id="btnDetalleAdelantoSueldo">Detalle</button>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs" disabled id="btnEditarAdelantoSueldo">Editar</button>' +
                    '</td>' +
                    '</tr>';
                } else {
                    template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '</td>' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' + Activo + '</td>' +
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
    $("#Crear #adsu_RazonAdelanto").val("");
    $("#Crear #adsu_Monto").val("");
    $("#Crear #emp_IdEmpleado").val(0);
    $("#Crear #adsu_FechaAdelanto").val("");

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
//VARIABLE GLOBAL CON EL VALOR MAXIMO DEL SUELDO EN LA CREACION
var MaxSueldoCreate = 0;
//DETECTAR LOS CAMBIOS EN EL DDL DE EMPLEADOS EN LA CREACION
$(cmbEmpleado).change(() => {
    //CAPTURAR EL ID DEL EMPLEADO SELECCIONADO
    var IdEmp = parseInt(cmbEmpleado.val());
    console.log(IdEmp);
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
        //$("#adsu_Monto").attr("max", data);
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#AgregarAdelantos").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se pudo recuperar el sueldo neto promedio',
        });
    });
});

//OBJETO CONSTANTE DEL DDL DE EMPLEADOS 
var cmbEmpleadoEdit = $("#frmAdelantosEdit #emp_Id");
//VARIABLE GLOBAL CON EL VALOR MAXIMO DEL SUELDO EN LA EDICION
var MaxSueldoEdit = 0;
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
        //$("#frmAdelantosEdit #adsu_Monto").attr("max", data);
    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#EditarAdelantoSueldo").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se pudo recuperar el sueldo neto promedio',
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
                $("#Crear #Validation_descripcion1").css("display", "none");
                $("#Crear #Validation_descripcion2").css("display", "none");
                $("#Crear #Validation_descripcion3").css("display", "none");
                cargarGridAdelantos();
                //Setear la variable de SueldoAdelantoMaximo a cero 
                MaxSueldoCreate = 0;
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'Se ha registrado con exitosamente!',
                });
            }
        });
    }
    else {
        if (IdEmp == 0)
        {
            iziToast.error({
                title: 'Error',
                message: 'Ingrese un colaborador válido.',
            });
        } else if (Razon == "" || Razon == null || Razon == undefined) {
            iziToast.error({
                title: 'Error',
                message: 'Campo Razón requerido.',
            });
        }else if(Monto == "" || Monto == null || Monto == undefined || Monto == 0){
            iziToast.error({
                title: 'Error',
                message: 'Campo Monto requerido.',
            });
        } else if (Fecha == "" || Fecha == null || Fecha == undefined) {
            iziToast.error({
                title: 'Error',
                message: 'Campo Fecha requerido.',
            });
        }
        else if ($("#Crear #adsu_Monto").val() > MaxSueldoCreate) {
            //MENSAJE DE EEROR EN CASO QUE EL MONTO SEA MAYOR AL SUELDO PROMEDIO 
            iziToast.error({
                title: 'Error',
                message: 'El monto Ingresado es mayor que el sueldo promedio del colaborador',
            });
            //IGUALAR EL MONTO AL SUELDO PROMEDIO
            $("#Crear #adsu_Monto").val(MaxSueldoCreate);
        }

        if (Razon == "" || Razon == null || Razon == undefined) {
            $("#Crear #Validation_descripcion1").css("display", "");
        } else {
            $("#Crear #Validation_descripcion1").css("display", "none");
        }
        if (Monto == "" || Monto == null || Monto == undefined) {
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

});


function ValidarCamposEditar(colaborador, razon, monto){
    var pasoValidacion = true;

    console.log(colaborador.val());
    if(colaborador.val() == ''){
        pasoValidacion = false;
        //Codigo para mostrar el span de validacion
        //Hacerle focus al input
        console.log('No paso la validacion de campo colaborador')
        $(colaborador).focus();
    } else{

    }

    if(razon ==null || razon == ''){
        pasoValidacion = false;
        console.log('No paso la validacion de campo razon')
    }

    return pasoValidacion;
}


//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION
$('#btnCerrarCrearAdelanto').click(function () {
    $("#Crear #Validation_descripcion1").css("display", "none");
    $("#Crear #Validation_descripcion2").css("display", "none");
    $("#Crear #Validation_descripcion3").css("display", "none");
});
$('#IconCerrar').click(function () {
    $("#Crear #Validation_descripcion1").css("display", "none");
    $("#Crear #Validation_descripcion2").css("display", "none");
    $("#Crear #Validation_descripcion3").css("display", "none");
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
    //OBTENER EL ID DEL EMPLEADO 
    var IdEmp = $("#frmAdelantosEdit #emp_Id").val();
    //RECUPERAR EL MONTO MAXIMO PARA ADELANTO DE SUELDO
    if(ValidarCamposEditar($('#emp_Id'), $('#adsu_RazonAdelanto'), $('#adsu_Monto')))
    $.ajax({
        url: "/AdelantoSueldo/GetSueldoNetoProm",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: IdEmp })
    }).done(function (data) {
        //ACCIONES EN CASO DE EXITO

        //EJECUTAR LA VALIDACION PARA LA EDICIÓN
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
                        message: 'No se pudo editar el registro, contacte al administrador',
                    });
                }
                else {
                    // REFRESCAR UNICAMENTE LA TABLA
                    cargarGridAdelantos();
                    //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                    FullBody();
                    $("#EditarAdelantoSueldo").modal('hide');
                    //Setear la variable de SueldoAdelantoMaximo a cero 
                    MaxSueldoEdit = 0;
                    //Mensaje de exito de la edicion
                    iziToast.success({
                        title: 'Exito',
                        message: 'El registro fue editado de forma exitosa!',
                    });
                }
            });
        }
        else {
            //MENSAJE DE EEROR EN CASO QUE EL MONTO SEA MAYOR AL SUELDO PROMEDIO 
            iziToast.error({
                title: 'Error',
                message: 'El monto Ingresado es mayor que el sueldo promedio del colaborador',
            });
            //IGUALAR EL MONTO AL SUELDO PROMEDIO
            $("#Editar #adsu_Monto").val(data);
            //MaxSueldoEdit = 0;
        }

    }).fail(function (data) {
        //ACCIONES EN CASO DE ERROR
        $("#EditarAdelantoSueldo").modal('hide');
        iziToast.error({
            title: 'Error',
            message: 'No se pudo recuperar el sueldo neto promedio',
        });
    });

});

//FUNCION: OCULTAR MODAL DE EDICION EN LA INACTIVACION
$("#btnCerrarEditar").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE EDICION EN LA ACTIVACION
$("#btnCerrarEditar2").click(function () {
    $("#EditarAdelantoSueldo").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
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
                message: 'No se pudo inactivar el registro, contacte al administrador',
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
                message: 'El registro fue Inactivado de forma exitosa!',
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
                message: 'No se pudo inactivar el registro, contacte al administrador',
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
                message: 'El registro fue Inactivado de forma exitosa!',
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

//FUNCION: OCULTAR MODAL DE CREACION
$("#btnCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
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




