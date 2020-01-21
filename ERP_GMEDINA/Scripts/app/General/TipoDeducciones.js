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
function cargarGridTipoDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/TipoDeducciones/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se pudo cargar la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaTipoDeducciones = data;
            $('#tblTipoDeducciones').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTipoDeducciones.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaTipoDeducciones[i].tde_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaTipoDeducciones[i].tde_Activo == true ? '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-primary btn-xs" style="margin-right:3px;" id="btnDetalleTipoDeducciones">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaTipoDeducciones[i].tde_Activo == true ? '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-default btn-xs"  id="btnEditarTipoDeducciones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaTipoDeducciones[i].tde_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarTipoDeducciones">Activar</button>' : '' : '';


                var FechaCrea = FechaFormato(ListaTipoDeducciones[i].tde_FechaCrea);
                var FechaModifica = FechaFormato(ListaTipoDeducciones[i].tde_FechaModifica);

                UsuarioModifica = ListaTipoDeducciones[i].tde_UsuarioModifica == null ? 'Sin modificaciones' : ListaTipoDeducciones[i].NombreUsuarioModifica;

                $('#tblTipoDeducciones').dataTable().fnAddData([
                                ListaTipoDeducciones[i].tde_IdTipoDedu,
                                ListaTipoDeducciones[i].tde_Descripcion,
                                estadoRegistro,
                                  botonDetalles + botonEditar + botonActivar]
                                );
            }
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarTipoDeducciones", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #Validation_descipcion").css("display", "none");
    $("#Crear input[type=text]").val('');
    //$("#AgregarTipoDeducciones").modal();
    $("#AgregarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
    
    
});

$("#btnCerrarCrear").click(function () {
    $("#frmTipoDeduccionCreate #Validation_Descripcion").css("display", "none");
    $("#Crear .asterisco").removeClass("text-danger");
});

$("#btnCerrarEditar").click(function () {
    $("#Editar #Validation_DescripcionE").css("display", "none");
    $("#Editar .asterisco").removeClass("text-danger");
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroTipoDeducciones').click(function () {
    var Descripcion = $('#Crear #tde_Descripcion').val();
    if ($('#Crear #tde_Descripcion').val() != "")
    {
        // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        $("#Crear #Validation_descripcion").css("display", "block");
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmTipoDeduccionCreate").serializeArray();
        //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
        if ($("#Crear #tde_Descripcion").val()) {
            //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
            $.ajax({
                url: "/TipoDeducciones/Create",
                method: "POST",
                data: data
            }).done(function (data) {
                //CERRAR EL MODAL DE AGREGAR
                if ($("#tde_Descripcion").val())
                    $("#AgregarTipoDeducciones").modal('hide');
                //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
                if (data == "error") {
                }
                else {
                    cargarGridTipoDeducciones();
                    // Mensaje de exito cuando un registro se ha guardado bien
                    iziToast.success({
                        title: 'Éxito',
                        message: '¡El registro se agregó de forma exitosa!',
                    });
                }
            });
        }
    }
    else{
        if (Descripcion == "") {
            $("#Crear #Validation_Descripcion").css("display", "block");
            $("#AsteriskDescripcion").addClass("text-danger");
        }
        else {
            $("#Crear #Validation_Descripcion").css("display", "none");
            $("#AsteriskDescripcion").removeClass("text-danger");
        }
    }
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
    $(document).on("click", "#tblTipoDeducciones tbody tr td #btnEditarTipoDeducciones", function () {
        var ID = $(this).data('id');
        inactivar = ID;
        $.ajax({
            url: "/TipoDeducciones/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    //debugger;
                    $.each(data, function (i, iter) {
                        $("#Editar #tde_IdTipoDedu").val(iter.tde_IdTipoDedu);
                        $("#Editar #tde_Descripcion").val(iter.tde_Descripcion);
                    });
                    //$("#EditarTipoDeducciones").modal();
                    $("#EditarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
                    
                    
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

$("#btnUpdateTipoDeducciones").click(function () {
    var DescripcionE = $("#Editar #tde_Descripcion").val();
    if ($("#Editar #tde_Descripcion").val() != '') {
        $("#EditarTipoDeducciones").modal('hide');
        //$("#EditarTipoDeduccionConfirmacion").modal();
        $("#EditarTipoDeduccionConfirmacion").modal({ backdrop: 'static', keyboard: false });
        
        
    }
    else {
        if (DescripcionE == "") {
            $("#Editar #Validation_DescripcionE").css("display", "block");
            $("#AsteriskDescripcionE").addClass("text-danger");
        }
        else {
            $("#Editar #Validation_DescripcionE").css("display", "none");
            $("#AsteriskDescripcionE").removeClass("text-danger");
        }
        $("#Editar #tde_Descripcion").focus();
    }

});

////////MODAAAAAAAAL DE CONFIRMACION DE EDITAR------
//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarTipoDedu").click(function () {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmTipoDeduccionEdit").serializeArray();
        var descedit = $("#Editar #tde_Descripcion").val();
        //VALIDAMOS LOS CAMPOS
        if (descedit != "" && descedit != null && descedit != undefined && isNaN(descedit) == true) {
            mostrarcargandoEditar();
            //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
            $.ajax({
                url: "/TipoDeducciones/Edit",
                method: "POST",
                data: data
            }).done(function (data) {

                if (data != "error") {

                    //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                    $("#EditarTipoDeducciones").modal('hide');
                    $("#EditarTipoDeduccionConfirmacion").modal('hide');
                    cargarGridTipoDeducciones();
                    iziToast.success({
                        title: 'Éxito',
                        message: '¡El registro se editó de forma exitosa!',
                    });
                    ocultarcargandoEditar();
                }
            });
            // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
            $("#frmTipoDeduccionEdit").submit(function (e) {
                return false;
            });
        }
    else {                
        //$("#EditarTipoDeduccionConfirmacion").modal('hide');
        $("#EditarTipoDeduccionConfirmacion").modal({ backdrop: 'static', keyboard: false });
        
        
    }
});

const btneditar = $('#btnEditarTipoDedu'),

    cargandoEditar = $('#cargandoEditar')//Div que aparecera cuando se le de click en crear

function mostrarcargandoEditar() {
    btneditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarcargandoEditar() {
    btneditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
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




////EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
//$("#btnUpdateTipoDeducciones").click(function () {
//    $("#Editar #Validation_descripcion").css("display", "block");
//    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
//    var data = $("#frmTipoDeduccionEdit").serializeArray();
//    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
//    if ($("#Editar #tde_Descripcion").val()) {   //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
//        $.ajax({
//            url: "/TipoDeducciones/Edit",
//            method: "POST",
//            data: data
//        }).done(function (data) {
//            if (data == "error") {
//                //Cuando traiga un error del backend al guardar la edicion
//                iziToast.error({
//                    title: 'Error',
//                    message: 'No se pudo editar el registro, contacte al administrador',
//                });
//            }
//            else {
//                // REFRESCAR UNICAMENTE LA TABLA
//                cargarGridTipoDeducciones();
//                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
//                $("#EditarTipoDeducciones").modal('hide');
//                //Mensaje de exito de la edicion
//                iziToast.success({
//                    title: 'Exito',
//                    message: 'El registro fue editado de forma exitosa!',
//                });
//            }
//        });
//    }
//});

$(document).on("click", "#tblTipoDeducciones tbody tr td #btnDetalleTipoDeducciones", function () {
    var ID = $(this).data('id');
    //console.log(ID);
    $.ajax({
        url: "/TipoDeducciones/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log(data);
                var FechaCrea = FechaFormato(data[0].tde_FechaCrea);
                var FechaModifica = FechaFormato(data[0].tde_FechaModifica);
                $("#Detalles #tde_UsuarioCrea").val(data[0].tde_UsuarioCrea);
                $("#Detalles #tde_IdTipoDedu").val(data[0].tde_IdTipoDedu);
                $("#Detalles #tde_Descripcion").html(data[0].tde_Descripcion);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #tde_FechaCrea").html(FechaCrea);
                $("#Detalles #tde_UsuarioModifica").html(data.tde_UsuarioModifica);
                $("#Detalles #tde_FechaModifica").html(FechaModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION

                //$("#DetailsTipoDeducciones").modal();
                $("#DetailsTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
                
                
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA MENSAJE DE CONFIRMACION
$("#btnInactivarTipoDeducciones").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    //$("#InactivarTipoDeducciones").modal();
    $("#InactivarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
    
    

});

//FUNCION: SEGUNDA FASE DE EDICION DE REGISTROS, REALIZAR LA EJECUCION PARA INACTIVAR EL REGISTRO
$("#btnInactivarRegistroTipoDeducciones").click(function () {
    $.ajax({
        url: "/TipoDeducciones/Inactivar/" + inactivar,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: inactivar })
    }).done(function (data) {
        $("#InactivarTipoDeducciones").modal('hide');
        //Refrescar la tabla de TipoDeducciones
        cargarGridTipoDeducciones();
        //Mensaje de error si no hay data
        iziToast.success({
            title: 'Éxito',
            message: '¡El registro se inactivó de forma exitosa!',
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

// activar
$(document).on("click", "#tblTipoDeducciones tbody tr td #btnActivarTipoDeducciones", function () {
    activarID = $(this).data('id');
    console.log(activarID);
    //$("#ActivarTipoDeducciones").modal();
    $("#ActivarTipoDeducciones").modal({ backdrop: 'static', keyboard: false });
    
    
});

//FUNCION: SEGUNDA FASE DE EDICION DE REGISTROS, REALIZAR LA EJECUCION PARA INACTIVAR EL REGISTRO
$("#btnActivarRegistroTipoDeducciones").click(function () {
    $.ajax({
        url: "/TipoDeducciones/Activar/" + activarID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: { id: activarID }
    }).done(function (data) {
        $("#ActivarTipoDeducciones").modal('hide');
        //Refrescar la tabla de TipoDeducciones
        cargarGridTipoDeducciones();
        console.log(data);
        //Mensaje de error si no hay data
        iziToast.success({
            title: 'Exito',
            message: '¡El registro se activó de forma exitosa!',
        });
    });
});
