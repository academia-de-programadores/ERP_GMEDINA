﻿//VARIABLE GLOBAL PARA INACTIVAR
var inactivar = 0;
const btnGuardar = $('#btnCreateRegistroTipoDeducciones'),
cargandoCrearcargandoCrear=$('#cargandoCrear'),
cargandoCrear=$('#cargandoCrear')//Div que aparecera cuando se le de click en crear



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
    _ajax(null,
        '/TipoDeducciones/GetData',
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
            var ListaTipoDeducciones = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTipoDeducciones.length; i++) {
                var FechaCrea = FechaFormato(ListaTipoDeducciones[i].tde_FechaCrea);

                var FechaModifica = FechaFormato(ListaTipoDeducciones[i].tde_FechaModifica);

                UsuarioModifica = ListaTipoDeducciones[i].tde_UsuarioModifica == null ? 'Sin modificaciones' : ListaTipoDeducciones[i].NombreUsuarioModifica;

                template += '<tr data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '">' +
                    '<td>' + ListaTipoDeducciones[i].tde_Descripcion + '</td>' +       
                    '<td>' +
                    '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-primary btn-xs" id="btnEditarTipoDeducciones">Editar</button>' +
                    '<button data-id = "' + ListaTipoDeducciones[i].tde_IdTipoDedu + '" type="button" class="btn btn-default btn-xs" id="btnDetalleTipoDeducciones">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyTipoDeducciones').html(template);
        });
    FullBody();
}

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarTipoDeducciones", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #Validation_descipcion").css("display", "none");
    $("#Crear input[type=text]").val('');
    $("#AgregarTipoDeducciones").modal();
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroTipoDeducciones').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    $("#Crear #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmTipoDeduccionCreate").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#Crear #tde_Descripcion").val())
    {
        console.log('asddd');
        mostrarCargandoCrear();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/TipoDeducciones/Create",
            method: "POST",
            data: data
        }).done(function (data) {
                
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else {
                cargarGridTipoDeducciones();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
                //CERRAR EL MODAL DE AGREGAR
                if ($("#tde_Descripcion").val())
                    $("#AgregarTipoDeducciones").modal('hide');
                ocultarCargandoCrear();                
            }
        });
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
                $("#EditarTipoDeducciones").modal();
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
$("#btnUpdateTipoDeducciones").click(function () {
    $("#Editar #Validation_descripcion").css("display", "block");
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmTipoDeduccionEdit").serializeArray();
    //SE VALIDA QUE EL CAMPO DESCRIPCION ESTE INICIALIZADO PARA NO IR AL SERVIDOR INNECESARIAMENTE
    if ($("#Editar #tde_Descripcion").val())
    {   //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
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
                cargarGridTipoDeducciones();
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

$(document).on("click", "#tblTipoDeducciones tbody tr td #btnDetalleTipoDeducciones", function () {
    var ID = $(this).data('id');
    console.log(ID);
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
                var FechaCrea = FechaFormato(data[0].tde_FechaCrea);
                var FechaModifica = FechaFormato(data[0].tde_FechaModifica);
                $("#Detalles #tde_UsuarioCrea").val(data[0].tde_UsuarioCrea);
                $("#Detalles #tde_IdTipoDedu").val(data[0].tde_IdTipoDedu);
                $("#Detalles #tde_Descripcion").val(data[0].tde_Descripcion);
                $("#Detalles #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detalles #tde_FechaCrea").val(FechaCrea);
                $("#Detalles #tde_UsuarioModifica").val(data.tde_UsuarioModifica);
                $("#Detalles #tde_FechaModifica").val(FechaModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);                
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                //var SelectedId = data[0].cde_IdDeducciones;
                ////CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                //$.ajax({
                //    url: "/TechosDeducciones/EditGetDDL",
                //    method: "GET",
                //    dataType: "json",
                //    contentType: "application/json; charset=utf-8",
                //    data: JSON.stringify({ ID })
                //})
                    //.done(function (data) {
                    //    //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                    //    $("#Detalles #cde_IdDeducciones").empty();
                    //    //LLENAR EL DROPDOWNLIST
                    //    $("#Detalles #cde_IdDeducciones").append("<option value=0>Selecione una opción...</option>");
                    //    $.each(data, function (i, iter) {
                    //        $("#Detalles #cde_IdDeducciones").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                    //    });
                    //});
                $("#DetailsTipoDeducciones").modal();
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

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA MENSAJE DE CONFIRMACION
$("#btnInactivarTipoDeducciones").click(function () {
    $("#EditarTipoDeducciones").modal('hide');
    $("#InactivarTipoDeducciones").modal();
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
            title: 'Exito',
            message: 'Se ha inactivado el registro',
        });
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

// validar si un string contiene caracteres numericos
var numeros = "0123456789";

function tiene_numeros(texto) {
    for (i = 0; i < texto.length; i++) {
        if (numeros.indexOf(texto.charAt(i), 0) != -1) {
            return 1;
        }
    }
    return 0;
}