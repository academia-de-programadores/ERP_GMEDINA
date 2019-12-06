//OBTENER SCRIPT DE FORMATEO DE FECHA
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
//variable para reconocer la planilla actual de la tabla
var planillaId = '';


//FUNCION: CARGAR DATA DE UNA PLANILLA ESPECIFICA Y REFRESCAR LA TABLA DEL INDEX ================================
$('.cargarPlanilla').click(function () {
    $('#btnPlanilla').css('display', 'none');
    $('#Cargando').css('display', '');
    var ID = $(this).data('id');
    console.log(ID);
    _ajax(null,
        '/Planilla/GetPlanilla/'+ID,
        'GET',
        (data) => {
            console.log(data);
            if (data.length == 0) {
                //Validar si se genera un error al cargar la data de la planilla especifica
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var PlanillaSeleccionada = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < PlanillaSeleccionada.length; i++) {
                template += '<tr data-id = "' + PlanillaSeleccionada[i].emp_Id + '">' +
                    '<td>' + PlanillaSeleccionada[i].Nombres + '</td>' +
                    '<td>' + PlanillaSeleccionada[i].per_Identidad + '</td>' +
                    '<td>' + PlanillaSeleccionada[i].salarioBase + '</td>' +
                    '<td>' + PlanillaSeleccionada[i].tmon_Descripcion + '</td>' +
                    '<td>' +
                    '<button type="button" data-id = "' + PlanillaSeleccionada[i].emp_Id + '" class="btn btn-default btn-xs" id="btnDetalleCatalogoDeducciones">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            ID=='' ? planillaId = null : planillaId =  data[0].cpla_IdPlanilla;
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyPreviewPlanilla').html(template);
            ID == '' ? $('#nombrePlanilla').html('') : $('#nombrePlanilla').html(data[0].cpla_DescripcionPlanilla);
            $('#btnPlanilla').css('display', '');
            $('#Cargando').css('display', 'none');
        });
});



//GENERAR PLANILLA =======================================================================================
$('#btnPlanilla').click(function () {
    $('#confirmarGenerarPlanilla').modal();
});
$('#btnGenerarPlanilla').click(function () {
    $('#btnPlanilla').css('display', 'none');
    $('#Cargando').css('display', '');
    $('#confirmarGenerarPlanilla').hide();
    var ID = planillaId;
    console.log('ID PLANILLA A GENERAR:  ' + planillaId);
    _ajax(null,
        '/Planilla/GenerarPlanilla/' + ID,
        'GET',
        (data) => {
            $('#btnPlanilla').css('display', '');
            $('#Cargando').css('display', 'none');
            console.log(data);
            if (data.Tipo=='success') {
                iziToast.success({
                    title: data.Encabezado,
                    message: data.Response,
                });
            }
            else if (data.Tipo == 'error') {
                iziToast.error({
                    title: data.Encabezado,
                    message: data.Response,
                });
            }
            else {
                iziToast.warning({
                    title: data.Encabezado,
                    message: data.Response,
                });
            }

            
            $('.modal-backdrop').css('display', 'none');
            $('.fade').css('display', 'none');
            $('.in').css('display', 'none');
        }
    );
});



// =====================================================================================================

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnEditarCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/CatalogoDeDeducciones/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log('Hla')
                $("#Editar #cde_IdDeducciones").val(data.cde_IdDeducciones);
                $("#Editar #cde_DescripcionDeduccion").val(data.cde_DescripcionDeduccion);
                $("#Editar #cde_PorcentajeColaborador").val(data.cde_PorcentajeColaborador);
                $("#Editar #cde_PorcentajeEmpresa").val(data.cde_PorcentajeEmpresa);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/CatalogoDeDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Editar #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Editar #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarCatalogoDeducciones").modal();
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
$("#btnUpdateDeduccion").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmCatalogoDeducciones").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeDeducciones/Edit",
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
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarCatalogoDeducciones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });
        }
    });
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarCatalogoDeducciones", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/CatalogoDeDeducciones/EditGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #tde_IdTipoDedu").empty();
            $("#Crear #tde_IdTipoDedu").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #tde_IdTipoDedu").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarCatalogoDeducciones").modal();
});

///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnDetalleCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/CatalogoDeDeducciones/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data.cde_FechaCrea);
                var FechaModifica = FechaFormato(data.cde_FechaModifica);
                $("#Detalles #cde_IdDeducciones").val(data.cde_IdDeducciones);
                $("#Detalles #cde_DescripcionDeduccion").val(data.cde_DescripcionDeduccion);
                $("#Detalles #cde_PorcentajeColaborador").val(data.cde_PorcentajeColaborador);
                $("#Detalles #cde_PorcentajeEmpresa").val(data.cde_PorcentajeEmpresa);
                $("#Detalles #cde_UsuarioCrea").val(data.cde_UsuarioCrea);
                $("#Detalles #cde_FechaCrea").val(FechaCrea);
                $("#Detalles #cde_UsuarioModifica").val(data.cde_UsuarioModifica);
                $("#Detalles #cde_FechaModifica").val(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/CatalogoDeDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#DetallesCatalogoDeducciones").modal();
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


//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccion').click(function () {
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCatalogoDeduccionesCreate").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/CatalogoDeDeducciones/Create",
        method: "POST",
        data: data
    }).done(function (data) {
        //CERRAR EL MODAL DE AGREGAR
        $("#AgregarCatalogoDeducciones").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo guardar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarCatalogoDeducciones").modal('hide');
});

$(document).on("click", "#btnmodalInactivarCatalogoDeducciones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarCatalogoDeducciones").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccion").click(function () {

    var data = $("#frmCatalogoDeduccionesInactivar").serializeArray();
    $.ajax({
        url: "/CatalogoDeDeducciones/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo inactivar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarCatalogoDeducciones").modal('hide');
            $("#EditarCatalogoDeducciones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
});


