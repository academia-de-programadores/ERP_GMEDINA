//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });
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
            }
        });
});  

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeducciones").submit(function (e) {
    return false;
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
    $("#Crear #cde_PorcentajeColaborador").val('');
    $("#Crear #cde_PorcentajeEmpresa").val('');
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccion').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var val1 = $("#Crear #cde_Descripcion").val();
    var val2 = $("#ddltipodededu").val();

    if (val1 == "" && val1 == null && val1 == undefined) {
        $("#Crear #cde_DescripcionDeduccion").css("display", "");
    }
    else if (val2 == "" && val2 == null && val2 == undefined && val2 <= 0) {
        $("#Crear #ddltipodededu").css("display", "");
    }
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
            $("#Crear #cde_DescripcionDeduccion").val('');
            $("#Crear #cde_PorcentajeColaborador").val('');
            $("#Crear #cde_PorcentajeEmpresa").val('');

            //ocultar el modal
            $("#AgregarCatalogoDeducciones").modal('hide');

            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });     
});

// EVITAR POSTBACK DE FORMULARIOS
$("#frmCatalogoDeduccionesCreate").submit(function (e) {
    return false;
});

///////////////////////////////////////////////////////////////////////////////////////////////////
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
                $("#Detalles #cde_IdDeducciones").val(data[0].cde_IdDeducciones);
                $("#Detalles #cde_DescripcionDeduccion").val(data[0].cde_DescripcionDeduccion);
                $("#Detalles #cde_PorcentajeColaborador").val(data[0].cde_PorcentajeColaborador);
                $("#Detalles #cde_PorcentajeEmpresa").val(data[0].cde_PorcentajeEmpresa);
                $("#Detalles #cde_UsuarioCrea").val(data[0].cde_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detalles #cde_FechaCrea").val(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detalles #cde_UsuarioModifica").val(data[0].cde_UsuarioModifica);
                $("#Detalles #cde_FechaModifica").val(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].tde_IdTipoDedu;
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
                    $("#Detalles #tde_IdTipoDedu").append("<option value='0'>Selecione una opción...</option>");
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
///////////////////////////////////////////////////////////////////////////////////////////////////




$(document).on("click", "#btnmodalInactivarCatalogoDeducciones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarCatalogoDeducciones").modal();
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


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrear").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    $("#Validation_descipcion4").css("display", "none");
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarCreate").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    $("#Validation_descipcion4").css("display", "none");
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnCreateRegistroDeduccion").click(function () {
    var cde_DescripcionDeduccion = $("#cde_DescripcionDeduccion").val();
    var tde_IdTipoDedu = $("#tde_IdTipoDedu").val();
    var cde_PorcentajeColaborador = $("#cde_PorcentajeColaborador").val();
    var cde_PorcentajeEmpresa = $("#cde_PorcentajeEmpresa").val();


    if (cde_DescripcionDeduccion == "") {
        $("#Validation_descipcion").css("display", "");
    }
    else {
        $("#Validation_descipcion").css("display", "none");
    }


    if (tde_IdTipoDedu == "0" || tde_IdTipoDedu == null || tde_IdTipoDedu == undefined) {
        $("#Validation_descipcion2").css("display", "");
    }
    else if (tde_IdTipoDedu != null)
        {
        $("#Validation_descipcion2").css("display", "none");
    }


    if (cde_PorcentajeColaborador == "0.00") {
        $("#Validation_descipcion3").css("display", "");
    }
    else {
        $("#Validation_descipcion3").css("display", "none");
    }

    if (cde_PorcentajeEmpresa == "0.00") {
        $("#Validation_descipcion4").css("display", "");
    }
    else {
        $("#Validation_descipcion4").css("display", "none");
    }

});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarEditar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarEdit").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnUpdateDeduccion").click(function () {
    var cde_DescripcionDeduccionE = $("#cde_DescripcionDeduccion").val();
    var tde_IdTipoDeduE = $("#tde_IdTipoDedu").val();
    var cde_PorcentajeColaboradorE = $("#cde_PorcentajeColaborador").val();
    var cde_PorcentajeEmpresaE = $("#cde_PorcentajeEmpresa").val();


    if (cde_DescripcionDeduccionE == "") {
        $("#Validation_descipcion").css("display", "");
    }
    else {
        $("#Validation_descipcion").css("display", "none");
    }

    if (cde_PorcentajeColaboradorE == "" || cde_PorcentajeColaboradorE == null || cde_PorcentajeColaboradorE == "") {
        $("#Validation_descipcion2").css("display", "");
    }
    else {
        $("#Validation_descipcion2").css("display", "none");
    }

    if (cde_PorcentajeEmpresaE == "" || cde_PorcentajeEmpresaE == null || cde_PorcentajeEmpresaE == undefined) {
        $("#Validation_descipcion3").css("display", "");
    }
    else {
        $("#Validation_descipcion3").css("display", "none");
    }

});





//Mascara para Inputs
//$ (documento).ready(function (){
////$ ("# teléfono"). inputmask ("máscara", {"máscara": "(99) 9999-99999"});//Mascara para Telefono
////$ ("# zip"). inputmask ("máscara", {"máscara": "99999-999"});//Mascara Codigo Postal
////$ ("# birth"). inputmask ("máscara", {"máscara": "99/99/9999"});//Mascara Fecha
//$ ("#precio"). inputmask ("máscara", {"máscara": "999,999.99"}, {reverso: true});//Mascara Precio
////$ ("#value"). inputmask ("mask", {"mask": "#. ## 9.99"}, {reverse: true});//Mascara Valor
////$ ("# ip"). inputmask ("máscara", {"máscara": "999,999,999,999"});//Mascara IP
//});

// PROBANDO LOS IZITOAST
//$(document).ready(function () {
//    console.log('cargado JS');
//    iziToast.show({
//        title: 'Hola',
//        message: 'Estoy probando los iziToast'
//    });
//});
