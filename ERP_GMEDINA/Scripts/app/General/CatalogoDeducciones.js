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
function cargarGridDeducciones() {
    _ajax(null,
        '/CatalogoDeDeducciones/GetData',
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
            var ListaDeducciones = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeducciones.length; i++) {
                template += '<tr data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '">' +
                    '<td>' + ListaDeducciones[i].cde_DescripcionDeduccion + '</td>' +
                    '<td>' + ListaDeducciones[i].cde_PorcentajeColaborador + '</td>' +
                    '<td>' + ListaDeducciones[i].cde_PorcentajeEmpresa + '</td>' +
                    '<td>' + ListaDeducciones[i].tde_Descripcion + '</td>' +
                    '<td>' +
                    '<button type="button" data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" class="btn btn-primary btn-xs" id="btnEditarCatalogoDeducciones">Editar</button>' +
                    '<button type="button" data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" class="btn btn-default btn-xs" id="btnDetalleCatalogoDeducciones">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDeducciones').html(template);
        });
}

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
///////////////////////////////////////////////////////////////////////////////////////////////////

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccion').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

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
  //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
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
            // REFRESCAR UNICAMENTE LA TABLA
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


// PROBANDO LOS IZITOAST
//$(document).ready(function () {
//    console.log('cargado JS');
//    iziToast.show({
//        title: 'Hola',
//        message: 'Estoy probando los iziToast'
//    });
//});


