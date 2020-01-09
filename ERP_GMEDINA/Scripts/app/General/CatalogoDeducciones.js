const btnGuardar = $('#btnCreateRegistroDeduccion')
const btnGuardarEditar = $('#btnUpdateDeduccion')
cargandoCrearcargandoCrear = $('#cargandoCrear')
cargandoCrear = $('#cargandoCrear')//Div que aparecera cuando se le de click en crear

//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });

//VARIABLE PARA INACTIVAR
var InactivarID = 0;

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
                var estadoCDD = ListaDeducciones[i].cde_Activo == false ? "Inactivo" : "Activo";
                console.log(estadoCDD);
                template += '<tr data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '">' +
                  '<td>' + ListaDeducciones[i].cde_IdDeducciones + '</td>' +
                    '<td>' + ListaDeducciones[i].cde_DescripcionDeduccion + '</td>' +
                    '<td>' + ListaDeducciones[i].cde_PorcentajeColaborador + '</td>' +
                    '<td>' + ListaDeducciones[i].cde_PorcentajeEmpresa + '</td>' +
                    '<td>' + ListaDeducciones[i].tde_Descripcion + '</td>' +
                   '<td>' + estadoCDD + '</td>' +
                   '<td>' + ((ListaDeducciones[i].cde_Activo == true) ?
                   '<button type="button" class="btn btn-primary btn-xs" id="btnDetalleCatalogoDeducciones" data-id="' + ListaDeducciones[i].cde_IdDeducciones + '">Detalles</button>' +
                   '<button type="button" class="btn btn-default btn-xs" id="btnEditarCatalogoDeducciones" data-id="' + ListaDeducciones[i].cde_IdDeducciones + '">Editar</button>' :
                   '<button type="button" class="btn btn-primary btn-xs" id="btnActivarCatalogoDeducciones" data-id="' + ListaDeducciones[i].cde_IdDeducciones + '">Activar</button>') + '</td> </tr>';
            }

            //for (var i = 0; i < ListaDeducciones.length; i++) {
            //    template += '<tr data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '">' +
            //        '<td>' + ListaDeducciones[i].cde_IdDeducciones + '</td>' +
            //        '<td>' + ListaDeducciones[i].cde_DescripcionDeduccion + '</td>' +
            //        '<td>' + ListaDeducciones[i].cde_PorcentajeColaborador + '</td>' +
            //        '<td>' + ListaDeducciones[i].cde_PorcentajeEmpresa + '</td>' +
            //        '<td>' + ListaDeducciones[i].tde_Descripcion + '</td>' +
            //        '<td>' + ListaDeducciones[i].cde_Activo + '</td>' +
            //        '<td>' +
            //        '<button type="button" data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" class="btn btn-primary btn-xs" id="btnDetalleCatalogoDeducciones">Detalle</button>' +
            //        '<button type="button" data-id = "' + ListaDeducciones[i].cde_IdDeducciones + '" class="btn btn-default btn-xs" id="btnEditarCatalogoDeducciones">Editar</button>' +
            //        '</td>' +
            //        '</tr>';
            //}
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            FullBody();
            $('#tbodyDeducciones').html(template);
            
        });
}

//VALIDAR CREATE//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrear").click(function () {
    $("#Validation_descipcionA").css("display", "none");
    $("#Validation_descipcion2A").css("display", "none");
    $("#Validation_descipcion3A").css("display", "none");
    $("#Validation_descipcion4A").css("display", "none");
    $("#cde_DescripcionDeduccionA").val("");
    $("#cde_PorcentajeColaboradorA").val("");
    $("#cde_PorcentajeEmpresaA").val("");
    $("#tde_IdTipoDedu").val("0");
    ocultarCargandoCrear(); 
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarCreate").click(function () {
    $("#Validation_descipcionA").css("display", "none");
    $("#Validation_descipcion2A").css("display", "none");
    $("#Validation_descipcion3A").css("display", "none");
    $("#Validation_descipcion4A").css("display", "none");
    $("#cde_DescripcionDeduccionA").val("");
    $("#cde_PorcentajeColaboradorA").val("");
    $("#cde_PorcentajeEmpresaA").val("");
    $("#tde_IdTipoDedu").val("0");
    ocultarCargandoCrear();
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnCreateRegistroDeduccion").click(function () {
    var cde_DescripcionDeduccionA = $("#cde_DescripcionDeduccionA").val();
    var tde_IdTipoDedu = $("#tde_IdTipoDedu").val();
    var cde_PorcentajeColaboradorA = $("#cde_PorcentajeColaboradorA").val();
    var cde_PorcentajeEmpresaA = $("#cde_PorcentajeEmpresaA").val();

    if (cde_DescripcionDeduccionA == "") {
        $("#Validation_descipcionA").css("display", "");
    }
    else {
        $("#Validation_descipcionA").css("display", "none");
    }

    if (tde_IdTipoDedu == "0" || tde_IdTipoDedu == null) {
        $("#Validation_descipcion2A").css("display", "");
        $("#tde_IdTipoDedu").val("0");
    }
    else {
        $("#Validation_descipcion2A").css("display", "none");
    }

    if (cde_PorcentajeColaboradorA == "0.00" || cde_PorcentajeColaboradorA == null || cde_PorcentajeColaboradorA == undefined || cde_PorcentajeColaboradorA <= 0) {
        $("#Validation_descipcion3A").css("display", "");
    }
    else {
        $("#Validation_descipcion3A").css("display", "none");
    }

    if (cde_PorcentajeEmpresaA == "0.00" || cde_PorcentajeEmpresaA == null || cde_PorcentajeEmpresaA == undefined || cde_PorcentajeEmpresaA <= 0) {
        $("#Validation_descipcion4A").css("display", "");
    }
    else {
        $("#Validation_descipcion4A").css("display", "none");
    }

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
    $("#tde_IdTipoDedu").val("0");
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccion').click(function () {
    
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var cde_DescripcionDeduccionA = $("#cde_DescripcionDeduccionA").val();
    var cde_PorcentajeColaboradorA = $("#cde_PorcentajeColaboradorA").val();
    var cde_PorcentajeEmpresaA = $("#cde_PorcentajeEmpresaA").val();

    if (cde_DescripcionDeduccionA == "") {
        $("#Validation_descipcionA").css("display", "");
    }
    else {
        $("#Validation_descipcionA").css("display", "none");
    }
 
    var tde_IdTipoDeduc = $("#Crear #tde_IdTipoDedu").val();

    if (tde_IdTipoDeduc == "0" || tde_IdTipoDeduc == null) {
        $("#Validation_descipcion2A").css("display", "");
        $("#tde_IdTipoDedu").val("0");
    }
    else {

        mostrarCargandoCrear();
        $("#Validation_descipcion2A").css("display", "none");
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmCatalogoDeduccionesCreate").serializeArray();

        $.ajax({
            url: "/CatalogoDeDeducciones/Create",
            method: "POST",
            data: data
        })
        .done(function (data) {
            if (data != "error") {
                cargarGridDeducciones();

                //ocultar el modal
                $("#AgregarCatalogoDeducciones").modal('hide');

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue agregado de forma exitosa!',
                });

                ocultarCargandoCrear();

                $("#cde_DescripcionDeduccionA").val("");
                $("#cde_PorcentajeColaboradorA").val("");
                $("#cde_PorcentajeEmpresaA").val("");
                $("#tde_IdTipoDedu").val("0");
            }
        }); 
    }

    if (cde_PorcentajeColaboradorA == "0.00" || cde_PorcentajeColaboradorA == null || cde_PorcentajeColaboradorA == undefined || cde_PorcentajeColaboradorA <= 0) {
        $("#Validation_descipcion3A").css("display", "");
    }
    else {
        $("#Validation_descipcion3A").css("display", "none");
    }

    if (cde_PorcentajeEmpresaA == "0.00" || cde_PorcentajeEmpresaA == null || cde_PorcentajeEmpresaA == undefined || cde_PorcentajeEmpresaA <= 0) {
        $("#Validation_descipcion4A").css("display", "");
    }
    else {
        $("#Validation_descipcion4A").css("display", "none");
    }
});

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeduccionesCreate").submit(function (e) {
    return false;
});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarEditar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    ocultarCargandoEditar();
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarEdit").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    ocultarCargandoEditar();

});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnEditarCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
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
    var cde_DescripcionDeduccionE = $("#Editar #cde_DescripcionDeduccion").val();
    var cde_PorcentajeColaboradorE = $("#Editar #cde_PorcentajeColaborador").val();
    var cde_PorcentajeEmpresaE = $("#Editar #cde_PorcentajeEmpresa").val();

    if (cde_DescripcionDeduccionE == "" || cde_PorcentajeColaboradorE <= 0.00 || cde_PorcentajeColaboradorE == 0 || cde_PorcentajeEmpresaE <= 0.00 || cde_PorcentajeEmpresaE == 0)
    {
        $("#Validation_descipcion").css("display", "");
        $("#Validation_descipcion2").css("display", "");
        $("#Validation_descipcion3").css("display", "");
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos validos',
        });
    }
    else {
        $("#Validation_descipcion").css("display", "none");
        $("#Validation_descipcion2").css("display", "none");
        $("#Validation_descipcion3").css("display", "none");
        mostrarCargandoEditar();
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmCatalogoDeducciones").serializeArray();

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/CatalogoDeDeducciones/Edit",
            method: "POST",
            data: data
        })
        .done(function (data) {
            if (data != "error") {
                
                // REFRESCAR UNICAMENTE LA TABLA
                cargarGridDeducciones();

                //Ocultar el modal 
                $("#EditarCatalogoDeducciones").modal('hide');

                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue editado de forma exitosa!',
                });
                ocultarCargandoEditar();
            }
        });
    } 
});  

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeducciones").submit(function (e) {
    return false;
});


//FUNCTION: MOSTRAR DETALLE
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnDetalleCatalogoDeducciones", function () {
    var ID = $(this).data('id');
    console.log(ID);
    $.ajax({
        url: "/CatalogoDeDeducciones/Details/"+ ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            $("#DetallesCatalogoDeducciones").modal();
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log(data);
                var FechaCrea = FechaFormato(data[0].cde_FechaCrea);
                var FechaModifica = FechaFormato(data[0].cde_FechaModifica);
                $("#Detalles #cde_IdDeducciones").html(data[0].cde_IdDeducciones);
                $("#Detalles #cde_DescripcionDeduccion").html(data[0].cde_DescripcionDeduccion);
                $("#Detalles #cde_PorcentajeColaborador").html(data[0].cde_PorcentajeColaborador);
                $("#Detalles #cde_PorcentajeEmpresa").html(data[0].cde_PorcentajeEmpresa);
                $("#Detalles #cde_UsuarioCrea").html(data[0].cde_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #cde_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #cde_UsuarioModifica").val(data[0].cde_UsuarioModifica);
                $("#Detalles #cde_FechaModifica").html(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].TipoDedu;
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
                    $.each(data, function (i, iter) {
                        $("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                    });
                });

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



//MOSTRAR MODAL INACTIVAR
$(document).on("click", "#btnmodalInactivarCatalogoDeducciones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarCatalogoDeducciones").modal();

    //Ocultar el modal editar
    $("#EditarCatalogoDeducciones").modal('hide');
});


$("#btnCerrarInhabilitar").click(function () {
    $("#InactivarCatalogoDeducciones").modal('hide');
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccion").click(function () {

    var data = $("#frmCatalogoDeduccionesInactivar").serializeArray();
  //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeDeducciones/Inactivar/" + InactivarID,
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

function mostrarCargandoEditar() {
    btnGuardarEditar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoEditar() {
    btnGuardarEditar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

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





