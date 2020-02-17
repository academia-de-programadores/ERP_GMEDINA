var IDInactivar = 0;

// script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {

    });

// funcion generica ajax
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

// actualizar datatable
function cargarGridPeriodo() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/Periodos/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {

                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }

            var ListPeriodo = data;
            // limpiar datatable
            $('#tblPeriodo').DataTable().clear();

            for (var i = 0; i < ListPeriodo.length; i++) {
                var FechaCrea = FechaFormato(ListPeriodo[i].peri_FechaCrea);
                var FechaModifica = FechaFormato(ListPeriodo[i].peri_FechaModifica);
                UsuarioModifica = ListPeriodo[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListPeriodo[i].NombreUsuarioModifica;

                var SeptimoDia = (ListPeriodo[i].peri_RecibeSeptimoDia == null || ListPeriodo[i].peri_RecibeSeptimoDia == false) ? "No" : "Si";

                // variable para verificar el estado del registro
                var estadoRegistro = ListPeriodo[i].peri_Activo == false ? 'Inactivo' : 'Activo'

                // variable boton detalles
                var botonDetalles = '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetallePeriodo">Detalles</button>';

                // variable boton editar
                var botonEditar = ListPeriodo[i].peri_Activo == true ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-default btn-xs"  id="btnEditarPeriodo">Editar</button>' : '';

                // variable donde está el boton activar
                var botonActivar = ListPeriodo[i].peri_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListPeriodo[i].peri_IdPeriodo + '" type="button" class="btn btn-default btn-xs"  id="btnActivarPeriodos">Activar</button>' : '' : '';

                // agregar row
                $('#tblPeriodo').dataTable().fnAddData([
                    ListPeriodo[i].peri_IdPeriodo,
                    ListPeriodo[i].peri_DescripPeriodo,
                    ListPeriodo[i].peri_CantidadDias,
                    SeptimoDia,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

// create 1
$(document).on("click", "#btnAgregarPeriodo", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Periodos/Create");

    if (validacionPermiso.status == true) {
        // habilitar boton
        $("#btnCrearPeriodoConfirmar").attr("disabled", false);

        //OCULTAR VALIDACIONES
        OcultarValidacionesCrear();

        // modal crear
        $("#CrearPeriodo").modal({ backdrop: 'static', keyboard: false });
    }
});



// create 2 ejecutar
$('#btnCrearPeriodoConfirmar').click(function () {


    //OBTENER LEL VALOR DE LOS INPUTS
    var Descrip = $("#Crear #peri_DescripPeriodo").val();
    var Cantidad = $("#Crear #peri_CantidadDias").val();

    //REALIZAR LA VALIDACION
    if (ValidarCamposCrear(Descrip, Cantidad)) {
        // deshabilitar boton
        $("#btnCrearPeriodoConfirmar").attr("disabled", true);

        ////SERIALIZAR EL FORMULARIO
        //var data = $("#frmCreatePeriodo").serializeArray();

        var recibe = ($('#Crear #peri_RecibeSeptimoDia').is(':checked')) ? true : false;

        var data = {
            peri_DescripPeriodo: $("#Crear #peri_DescripPeriodo").val(),
            peri_CantidadDias: $("#Crear #peri_CantidadDias").val(),
            peri_RecibeSeptimoDia: RecibeCrear
        };

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/Periodos/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data == "error") {

                $("#btnCrearPeriodoConfirmar").attr("disabled", false);

                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
            else {
                $("#CrearPeriodo").modal('hide');
                cargarGridPeriodo();
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
    else {

        // habilitar boton
        $("#btnCrearPeriodoConfirmar").attr("disabled", false);
    }
});




// editar 1
$(document).on("click", "#tblPeriodo tbody tr td #btnEditarPeriodo", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Periodos/Edit");

    if (validacionPermiso.status == true) {
        //OCULTAR VALIDACIONES
        OcultarValidacionesEditar();

        var ID = $(this).data('id');
        IDInactivar = ID;

        $.ajax({
            url: "/Periodos/Edit/" + ID,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                if (data) {
                    $.each(data, function (i, iter) {
                        $("#Editar #peri_IdPeriodo").val(iter.peri_IdPeriodo);
                        $("#Editar #peri_DescripPeriodo").val(iter.peri_DescripPeriodo);
                        $("#Editar #peri_CantidadDias").val(iter.peri_CantidadDias);
                        if (iter.peri_RecibeSeptimoDia)
                            $("#Editar #peri_RecibeSeptimoDia").prop('checked', true);
                        else
                            $("#Editar #peri_RecibeSeptimoDia").prop('checked', false);
                    });
                    $("#EditarPeriodo").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    iziToast.error({
                        title: 'Error',
                        message: 'No se pudo cargar la información, contacte al administrador',
                    });
                }
            });
    }
});


$("#btnUpdatePeriodo").click(function () {

    //OBTENER LEL VALOR DE LOS INPUTS
    var Descrip = $("#Editar #peri_DescripPeriodo").val();
    var Cantidad = $("#Editar #peri_CantidadDias").val();

    //REALIZAR LA VALIDACION
    if (ValidarCamposEditar(Descrip, Cantidad)) {

        //OCULTAR MODAL DE EDICION
        $("#EditarPeriodo").modal('hide');
        //MOSTRAR MODAL DE CONFIRMACION
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
        //DESBLOQUEAR EL BOTON DE CONFIRMAR EDICION
        $("#btnConfirmarEditar").attr("disabled", false);
    }

});

$("#btnCerrarConfirmarEditar").click(function () {

    //ocultar modal de confirmación
    $("#ConfirmarEdicion").modal('hide');

    // habilitar boton
    $("#btnConfirmarEditar").attr('disabled', false);

    //modal de edicion
    $("#EditarPeriodo").modal({ backdrop: 'static', keyboard: false });
});

//editar 3 ejecutar 
$(document).on("click", "#btnConfirmarEditar", function () {
    //BLOQUEAR EL BOTON DE CONFIRMAR EDICION
    $("#btnConfirmarEditar").attr("disabled", true);

    var recibe = ($('#Editar #peri_RecibeSeptimoDia').is(':checked')) ? true : false;

    var data = {
        peri_IdPeriodo: IDInactivar,
        peri_DescripPeriodo: $("#Editar #peri_DescripPeriodo").val(),
        peri_CantidadDias: $("#Editar #peri_CantidadDias").val(),
        peri_RecibeSeptimoDia: RecibeEdit
    };

    //var data = $("#frmEditPeriodo").serializeArray();
    $.ajax({
        url: "/Periodos/Editar",
        method: "POST",
        data: data
    })
        .done(function (data) {

            if (data != 'error') {
                // actualizar datatable
                cargarGridPeriodo();
                $("#ConfirmarEdicion").modal('hide');

                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
            else {
                // HABILITAR BOTON
                $("#btnConfirmarEditar").attr("disabled", false);
                //OCULTAR MODAL DE CONFIRMACION
                $("#ConfirmarEdicion").modal('hide');
                //MOSTRAR MODAL DE EDICION
                $("#EditarPeriodo").modal({ backdrop: 'static', keyboard: false });
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarPeriodo", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Periodos/Inactivar");

    if (validacionPermiso.status == true) {

        //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
        $("#btnInactivarPeriodoConfirmar").attr("disabled", false);
        //OCULTAR MODAL DE EDICION
        $("#EditarPeriodo").modal('hide');
        //MOSTRAR MODAL DE INACTIVACION
        $("#InactivarPeriodo").modal({ backdrop: 'static', keyboard: false });
    }
});

//CERRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarPeriodo").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarPeriodo").modal({ backdrop: 'static', keyboard: false });
});

//CONFIRMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPeriodoConfirmar").click(function () {
    //BLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnInactivarPeriodoConfirmar").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Periodos/Inactivar/" + IDInactivar,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
            $("#btnInactivarPeriodoConfirmar").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridPeriodo();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarPeriodo").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});


//
//ACTIVAR
var ActivarID = 0;
$(document).on("click", "#btnActivarPeriodos", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Periodos/Activar");

    if (validacionPermiso.status == true) {
        //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
        $("#btnActivarPeriodoConfirm").attr("disabled", false);
        ActivarID = $(this).data('id');
        //DESPLEGAR EL MODAL DE ACTIVAR
        $("#ActivarPeriodo").modal({ backdrop: 'static', keyboard: false });
    }
});

//CONFIRMAR ACTIVACION DEL REGISTRO
$("#btnActivarPeriodoConfirm").click(function () {
    //BLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
    $("#btnActivarPeriodoConfirm").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Periodos/Activar/" + ActivarID,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE CONFIRMAR INACTIVACION
            $("#btnActivarPeriodoConfirm").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridPeriodo();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarPeriodo").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    ActivarID = 0
});

//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPeriodo tbody tr td #btnDetallePeriodo", function () {

    // validar informacion del usuario
    var validacionPermiso = userModelState("Periodos/Details");

    if (validacionPermiso.status == true) {

        var ID = $(this).data('id');
        IDInactivar = ID;
        $.ajax({
            url: "/Periodos/Details/" + ID,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        }).done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $.each(data, function (i, iter) {

                    if (iter.peri_RecibeSeptimoDia) {
                        $("#Detalles #peri_RecibeSeptimoDia").html("Si");
                    }
                    else {
                        $("#Detalles #peri_RecibeSeptimoDia").html("No");
                    }

                    var FechaCrea = FechaFormato(data[0].peri_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].peri_FechaModifica);
                    $("#Detalles #peri_IdPeriodo").html(iter.peri_IdPeriodo);
                    $("#Detalles #peri_DescripPeriodo").html(iter.peri_DescripPeriodo);
                    $("#Detalles #peri_CantidadDias").html(iter.peri_CantidadDias);
                    data[0].peri_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #peri_UsuarioCrea").html(iter.peri_UsuarioCrea);
                    $("#Detalles #peri_FechaCrea").html(FechaCrea);
                    data[0].peri_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #peri_UsuarioModifica").html(data[0].peri_UsuarioModifica);
                    $("#Detalles #peri_FechaModifica").html(FechaModifica);
                });
                $("#DetallarPeriodo").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
    }
});

//*****************CREAR******************//

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#CrearPeriodo #Validation_descripcion").css("display", "none");
    $("#CrearPeriodo").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreatePeriodo").submit(function (event) {
    event.preventDefault();
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


//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#EditarPeriodo #Validation_descripcion").css("display", "none");
    $("#EditarPeriodo").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#EditarPeriodo #Validation_descripcion").css("display", "none");
    $("#EditarPeriodo").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditPeriodo").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarPeriodo", function () {
    $("#DetallesFormaPago").modal('hide');
    $("#InactivarFormaPago").modal({ backdrop: 'static', keyboard: false });
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarPeriodo").modal('hide');
});


//*****************DETALLES******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarDetalles").click(function () {
    $("#DetallarPeriodo #Validation_descripcion").css("display", "none");
    $("#DetallarPeriodo").modal('hide');
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarDetails").click(function () {
    $("#DetallarPeriodo").modal('hide');
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE DETALLE
$("#frmDetailsPeriodo").submit(function (event) {
    event.preventDefault();
});

function mostrarError(Mensaje) {
    iziToast.error({
        title: 'Error',
        message: Mensaje,
    });
}




//*******************************VALIDACIONES********************//

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE CREAR
function ValidarCamposCrear(Descripcion, CantidadDias) {
    var Local_ModelState = true;

    if (Descripcion != "-1") {
        //VALIDACION DE DOBLE ESPACIO
        var LengthString = Descripcion.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Descripcion.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #peri_DescripPeriodo").val(Descripcion.substring(0, FirstChar + 1));
        }//FIN DE VALIDACION DE DOBLE ESPACIO

        if (Descripcion == "" || Descripcion == " " || Descripcion == "  " || Descripcion == null || Descripcion == undefined) {
            if (Descripcion == ' ')
                $("#Crear #peri_DescripPeriodo").val("");
            Local_modelState = false;
            $("#Crear #AsteriscoDescripPeriodo").addClass("text-danger");
            $("#Crear #Validar_peri_DescripPeriodo").show();

        } else {
            $("#Crear #AsteriscoDescripPeriodo").removeClass("text-danger");
            $("#Crear #Validar_peri_DescripPeriodo").hide();
        }
    }

    if (CantidadDias != "-1") {
        //VALIDAR EL TOTAL DE VENTA
        if (CantidadDias == null || CantidadDias == "") {
            $("#Crear #AsteriscoCantidadDias").addClass("text-danger");
            $("#Crear #Validar_peri_CantidadDias").empty();
            $("#Crear #Validar_peri_CantidadDias").html("Este campo es requerido.");
            $("#Crear #Validar_peri_CantidadDias").show();
            Local_ModelState = false;
        } else {
            $("#Crear #AsteriscoCantidadDias").removeClass("text-danger");
            $("#Crear #Validar_peri_CantidadDias").hide();
            if (CantidadDias <= 0) {
                $("#Crear #AsteriscoCantidadDias").addClass("text-danger");
                $("#Crear #Validar_peri_CantidadDias").empty();
                $("#Crear #Validar_peri_CantidadDias").html("Este campo no puede ser menor que cero.");
                $("#Crear #Validar_peri_CantidadDias").show();
                Local_ModelState = false;
            } else {
                $("#Crear #AsteriscoCantidadDias").removeClass("text-danger");
                $("#Crear #Validar_peri_CantidadDias").hide();
            }
        }
    }

    return Local_ModelState;
}

//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE CREAR
function OcultarValidacionesCrear() {
    //VACIAR LOS INPUTS
    $("#Crear #peri_DescripPeriodo").val("");
    $("#Crear #peri_CantidadDias").val("");

    //OCULTAR VALIDACIONES
    $("#Crear #AsteriscoDescripPeriodo").removeClass("text-danger");
    $("#Crear #Validar_peri_DescripPeriodo").hide();

    $("#Crear #AsteriscoCantidadDias").removeClass("text-danger");
    $("#Crear #Validar_peri_CantidadDias").hide();
}



//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(Descripcion, CantidadDias) {
    var Local_ModelState = true;

    if (Descripcion != "-1") {
        //VALIDACION DE DOBLE ESPACIO
        var LengthString = Descripcion.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Descripcion.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #peri_DescripPeriodo").val(Descripcion.substring(0, FirstChar + 1));
        }//FIN DE VALIDACION DE DOBLE ESPACIO

        if (Descripcion == "" || Descripcion == " " || Descripcion == "  " || Descripcion == null || Descripcion == undefined) {
            if (Descripcion == ' ')
                $("#Editar #peri_DescripPeriodo").val("");
            Local_modelState = false;
            $("#Editar #AsteriscoDescripPeriodo").addClass("text-danger");
            $("#Editar #Validar_peri_DescripPeriodo").show();

        } else {
            $("#Editar #AsteriscoDescripPeriodo").removeClass("text-danger");
            $("#Editar #Validar_peri_DescripPeriodo").hide();
        }
    }

    if (CantidadDias != "-1") {
        //VALIDAR EL TOTAL DE VENTA
        if (CantidadDias == null || CantidadDias == "") {
            $("#Editar #AsteriscoCantidadDias").addClass("text-danger");
            $("#Editar #Validar_peri_CantidadDias").empty();
            $("#Editar #Validar_peri_CantidadDias").html("Este campo es requerido.");
            $("#Editar #Validar_peri_CantidadDias").show();
            Local_ModelState = false;
        } else {
            $("#Editar #AsteriscoCantidadDias").removeClass("text-danger");
            $("#Editar #Validar_peri_CantidadDias").hide();
            if (CantidadDias <= 0) {
                $("#Editar #AsteriscoCantidadDias").addClass("text-danger");
                $("#Editar #Validar_peri_CantidadDias").empty();
                $("#Editar #Validar_peri_CantidadDias").html("Este campo no puede ser menor que cero.");
                $("#Editar #Validar_peri_CantidadDias").show();
                Local_ModelState = false;
            } else {
                $("#Editar #AsteriscoCantidadDias").removeClass("text-danger");
                $("#Editar #Validar_peri_CantidadDias").hide();
            }
        }
    }

    return Local_ModelState;
}


//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE EDITAR
function OcultarValidacionesEditar() {
    //VACIAR LOS INPUTS
    $("#Editar #peri_DescripPeriodo").val("");
    $("#Editar #peri_CantidadDias").val("");

    //OCULTAR VALIDACIONES
    $("#Editar #AsteriscoDescripPeriodo").removeClass("text-danger");
    $("#Editar #Validar_peri_DescripPeriodo").hide();

    $("#Editar #AsteriscoCantidadDias").removeClass("text-danger");
    $("#Editar #Validar_peri_CantidadDias").hide();
}


//VARIABLE GLOBAL DE EDICION DE RECIBE SEPTIMO DIA
var RecibeCrear = false;
//CONFIRMAR ACTIVACION DEL REGISTRO
$("#Crear #peri_RecibeSeptimoDia").click(function () {
    //SETEAR EL ESTADO DE RECIBE EDIT
    RecibeCrear = ($('#Crear #peri_RecibeSeptimoDia').is(':checked')) ? true : false;
});


//VARIABLE GLOBAL DE EDICION DE RECIBE SEPTIMO DIA 
var RecibeEdit = false;
//CONFIRMAR ACTIVACION DEL REGISTRO
$("#Editar #peri_RecibeSeptimoDia").click(function () {
    //SETEAR EL ESTADO DE RECIBE EDIT
    RecibeEdit = ($('#Editar #peri_RecibeSeptimoDia').is(':checked')) ? true : false;
});