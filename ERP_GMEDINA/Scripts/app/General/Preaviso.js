﻿var IDInactivar = 0;

const btnGuardar = $('#btnCrearPreavisoConfirmar'),
cargandoCrear = $('#cargandoCrear') //Div que aparecera cuando se le de click en crear
//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridPreaviso() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/Preaviso/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaPreaviso = data;
            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblPreaviso').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaPreaviso.length; i++) {
                var FechaCrea = FechaFormato(ListaPreaviso[i].prea_FechaCrea);
                var FechaModifica = FechaFormato(ListaPreaviso[i].prea_FechaModifica);
                UsuarioModifica = ListaPreaviso[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListaPreaviso[i].NombreUsuarioModifica;
                //variable para verificar el estado del registro
                var estadoRegistro = ListaPreaviso[i].prea_Activo == false ? 'Inactivo' : 'Activo'
                //variable boton detalles
                var botonDetalles = ListaPreaviso[i].prea_Activo == true ? '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetallePreaviso">Detalles</button>' : '';
                //variable boton editar
                var botonEditar = ListaPreaviso[i].prea_Activo == true ? '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-default btn-xs"  id="btnEditarPreaviso">Editar</button>' : '';
                //variable donde está el boton activar
                var botonActivar = ListaPreaviso[i].prea_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarPreaviso">Activar</button>' : '' : '';
                //AGREGAR FILA AL DATATABLE POR ITERACIÓN DEL CICLO
                $('#tblPreaviso').dataTable().fnAddData([
                     ListaPreaviso[i].prea_IdPreaviso,
                     ListaPreaviso[i].prea_RangoInicioMeses,
                     ListaPreaviso[i].prea_RangoFinMeses,
                     ListaPreaviso[i].prea_DiasPreaviso,
                     estadoRegistro,
                     botonDetalles + botonEditar + botonActivar]
                 );
            }
        });
    FullBody();
}

function DataAnnotations(ToF) {
    if (ToF) {
        //TRUE PARA OCULTAR DATAANNOTATIONS
        $("#Editar #RangoInicio_Validation_descripcion").css("display", "none");
        $("#Editar #RangoFin_Validation_descripcion").css("display", "none");
        $("#Editar #Dias_descripcion").css("display", "none");
    }
    else {
        //FALSE PARA MOSTRAR DATAANNOTATIONS
        $("#Editar #RangoInicio_Validation_descripcion").css("display", "block");
        $("#Editar #RangoFin_Validation_descripcion").css("display", "block");
        $("#Editar #Dias_descripcion").css("display", "block");
    }
}

function ValidarForm() {
    //VARIABLE DE RETORNO
    var Retorno = true;
    //DECLARACION DE OBJETOS 
    var RangoInicio = $("#Editar #prea_RangoInicioMeses").val('');
    var RangoFin = $("#Editar #prea_RangoFinMeses").val('');
    var Dias = $("#Editar #prea_DiasPreaviso").val('');
    //VALIDAR QUE LOS CAMPOS SEAN MAYORES QUE CERO
    if (!RangoInicio >= 0 || RangoInicio != '')
        Retorno = false;
    if (!RangoFin >= 0 || RangoFin != '')
        Retorno = false;
    if (!Dias >= 0 || Dias != '')
        Retorno = false;
    //RETORNO DE FUNCION
    return Retorno;
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarPreaviso", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#CrearPreaviso #prea_RangoInicioMeses").val('');
    $("#CrearPreaviso #prea_RangoFinMeses").val('');
    $("#CrearPreaviso #prea_DiasPreaviso").val('');
    $("#CrearPreaviso").modal();
    DataAnnotations(true);
});

//FUNCION: CREAR UN NUEVO REGISTRO
$('#btnCrearPreavisoConfirmar').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    $("#CrearPreaviso #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreatePreaviso").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if (ValidarForm()) {
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/Preaviso/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                $("#CrearPreaviso").modal('hide');
                cargarGridPreaviso();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
        });
    }
    else {
        //MOSTRAR DATAANNOTATIONS
        DataAnnotations(false);
    }
});


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblPreaviso tbody tr td #btnEditarPreaviso", function () {

    //OCULTAR DATAANNOTATIONS 
    $("#Validation_descipcion").css("display", "block");
    $("#Validation_descipcion1").css("display", "block");
    $("#Validation_descipcion2").css("display", "block");
    //REALIZAR LA PETICION AL SERVIDOR
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/Preaviso/Edit/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {
                    $("#Editar #prea_IdPreaviso").val(iter.prea_IdPreaviso);
                    $("#Editar #prea_RangoInicioMeses").val(iter.prea_RangoInicioMeses);
                    $("#Editar #prea_RangoFinMeses").val(iter.prea_RangoFinMeses);
                    $("#Editar #prea_DiasPreaviso").val(iter.prea_DiasPreaviso);
                });
                $("#EditarPreaviso").modal();
            }
        });
});

$("#btnUpdatePreaviso").click(function () {

    var prea_RangoInicioMeses = $("#Editar #prea_RangoInicioMeses").val();
    var prea_RangoFinMeses = $("#Editar #prea_RangoFinMeses").val();
    var prea_DiasPreaviso = $("#Editar #prea_DiasPreaviso").val();

    if (prea_RangoInicioMeses == "0" || prea_RangoInicioMeses == "") {
        $("#Validation_descipcion").css("display", "block");
    }
    else if (prea_RangoFinMeses == "0" || prea_RangoFinMeses == "") {
        $("#Validation_descipcion1").css("display", "block");
    }
    else if (prea_DiasPreaviso == "0" || prea_DiasPreaviso == "") {
        $("#Validation_descipcion2").css("display", "block");
    }
    else {
        $("#EditarPreaviso").modal('hide');
        $("#ConfirmarEdicion").modal();
    }

});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarConfirmarEditar", function () {
    //OCULTAR EL MODAL DE EDICION
    $("#ConfirmarEdicion").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarPreaviso").modal();
});

//GUARADAR LA EDICION DEL REGISTRO
$(document).on("click", "#btnConfirmarEditar", function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON

    $("#CrearPreaviso #Validation_descripcion").css("display", "block");

    if ($("#EditarPreaviso #Editar #prea_RangoInicioMeses").val() != "" || $("#EditarPreaviso #Editar #prea_RangoInicioMeses").val() != "0.00" || $("#EditarPreaviso #Editar #prea_RangoFinMeses").val() != "" || $("#EditarPreaviso #Editar #prea_RangoFinMeses").val() != "0.00" || $("#EditarPreaviso #Editar #prea_DiasPreaviso").val() != "") {
        var data = $("#frmEditPreaviso").serializeArray();
        $.ajax({
            url: "/Preaviso/Editar",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data != "error") {
                cargarGridPreaviso();
                
                $("#ConfirmarEdicion").modal('hide');
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            } else {
                $("#ConfirmarEdicion").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
    }
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarPreaviso", function () {
    //OCULTAR EL MODAL DE EDICION
    $("#EditarPreaviso").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarPreaviso").modal();
});

//CERRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR EL MODAL DE INACTIVACION
    $("#InactivarPreaviso").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarPreaviso").modal();
});

//CONFIRMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPreavisoConfirmar").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Preaviso/Inactivar/" + IDInactivar,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridPreaviso();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarPreaviso").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});



// Activar
var activarID = 0;
$(document).on("click", "#btnActivarPreaviso", function () {
    activarID = $(this).data('id');
    $("#frmActivarPreavis").modal();
});

//activar ejecutar
$("#btnActivarPreavis").click(function () {

    $.ajax({
        url: "/Preaviso/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            cargarGridPreaviso();
            $("#frmActivarPreavis").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;


});
//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPreaviso tbody tr td #btnDetallePreaviso", function () {
    var ID = $(this).data('id');
    IDInactivar = ID;
    $.ajax({
        url: "/Preaviso/Details/" + ID,
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {

                    var FechaCrea = FechaFormato(data[0].prea_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].prea_FechaModifica);
                    $("#Detalles #prea_IdPreaviso").html(iter.prea_IdPreaviso);
                    $("#Detalles #prea_RangoInicioMeses").html(iter.prea_RangoInicioMeses);
                    $("#Detalles #prea_RangoFinMeses").html(iter.prea_RangoFinMeses);
                    $("#Detalles #prea_DiasPreaviso").html(iter.prea_DiasPreaviso);
                    data[0].prea_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #prea_UsuarioCrea").html(iter.prea_UsuarioCrea);
                    $("#Detalles #prea_FechaCrea").html(FechaCrea);
                    data[0].prea_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #prea_UsuarioModifica").html(data[0].prea_UsuarioModifica);
                    $("#Detalles #prea_FechaModifica").html(FechaModifica);
                });
                $("#DetallarPreaviso").modal();
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

//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#CrearPreaviso").modal("hide");
});

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearPreaviso #Validation_descripcion").css("display", "none");
    $("#CrearPreaviso").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreatePreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditPreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarPreaviso", function () {
    $("#DetallarPreaviso").modal('hide');
    $("#InactivarPreaviso").modal();
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarPreaviso").modal('hide');
});
