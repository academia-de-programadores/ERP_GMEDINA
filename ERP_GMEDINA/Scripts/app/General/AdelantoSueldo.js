//VARIABLE GLOBAL PARA INACTIVAR
var inactivar = 0;

//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
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

                template += '<tr data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '">' +
                    '<td>' + ListaAdelantos[i].empleadoNombre + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_RazonAdelanto + '</td>' +
                    '<td>' + ListaAdelantos[i].adsu_Monto + '</td>' +
                    '<td>' + FechaAdelanto + '</td>' +
                    '<td>' + Deducido + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-primary btn-xs" id="btnEditarAdelantos">Editar</button>' +
                    '<button data-id = "' + ListaAdelantos[i].adsu_IdAdelantoSueldo + '" type="button" class="btn btn-default btn-xs" id="btnDetalleAdelantos">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyAdelantoSueldo').html(template);
        });
    FullBody();
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

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAdelantos').click(function () {
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
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'Se ha registrado con exitosamente!',
            });
        }
    });
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAdelantoSueldo tbody tr td #btnEditarAdelantos", function () {
    var ID = $(this).data('id');
    inactivar = ID;
    $.ajax({
        url: "/AdelantoSueldo/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    }).done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
        if (data) {
            var SelectedIdEmp = 0;
            $.each(data, function (i, iter) {
                SelectedIdEmp = iter.Emp_Id;
                    $("#Editar #adsu_IdAdelantoSueldo").val(iter.adsu_IdAdelantoSueldo);
                    $("#Editar #adsu_RazonAdelanto").val(iter.adsu_RazonAdelanto);
                    $("#Editar #adsu_Monto").val(iter.adsu_Monto);
                    $("#Editar #adsu_FechaAdelanto").val(FechaFormato(iter.adsu_FechaAdelanto));
                });

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
                    $("#Editar #emp_Id").append("<option value=0>Selecione una opción...</option>");
                    $.each(data, function (i, iter) {
                        $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                    });
                });

                $("#EditarAdelantos").modal();
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

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateAdelantos").click(function () {
    $("#Editar #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmTipoDeduccionEdit").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#Editar #tde_Descripcion").val()) {   //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/TipoDeducciones/Edit",
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
                $("#EditarTipoDeducciones").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue editado de forma exitosa!',
                });
            }
        });
    }
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA MENSAJE DE CONFIRMACION
$("#btnInactivarAdelantos").click(function () {
    $("#EditarAdelantos").modal('hide');
    $("#InactivarAdelantos").modal();
});

//FUNCION: SEGUNDA FASE DE EDICION DE REGISTROS, REALIZAR LA EJECUCION PARA INACTIVAR EL REGISTRO
$("#btnInactivarRegistroTipoDeducciones").click(function () {
    $.ajax({
        url: "/AdelantoSueldo/Inactivar/" + inactivar,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: inactivar })
    }).done(function (data) {
        $("#InactivarTipoDeducciones").modal('hide');
        //Refrescar la tabla de TipoDeducciones
        cargarGridAdelantos();
        //Mensaje de error si no hay data
        iziToast.success({
            title: 'Exito',
            message: 'Se ha inactivado el registro',
        });
    });
});

//FUNCION: OCULTAR MODAL DE CREACION
$("#btnCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE EDICION
$("#btnCerrarEditar").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
});

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#Editar #Validation_descripcion").css("display", "none");
});

//FUNCION: HABILITAR EL DATAANNOTATION AL DESPLEGAR EL MODAL
$("#btnCerrar").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    $("#Editar #Validation_descripcion").css("display", "none");
});

$("#frmTipoDeduccionCreate").submit(function (event) {
    event.preventDefault();
});

$("#frmTipoDeduccionEdit").submit(function (event) {
    event.preventDefault();
});