////FUNCION GENERICA PARA REUTILIZAR AJAX
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
var InactivarID = 0;

//formato fecha
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });

// evitar postbacks
$("#frmEditISR").submit(function (e) {
    return false;
});
$("#frmISRCreate").submit(function (e) {
    return false;
});

//cargar grid
function cargarGridISR() {
    var esAdministrador = $("#rol_Usuario").val();
    $.ajax({
        url: "/ISR/GetData",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {

        if (data.length == 0) {
            iziToast.error({
                title: 'Error',
                message: 'No se cargó la información, contacte al administrador',
            });
        }
        else {
            var ListaISR = data, template = '';
            //limpiar datatable
            $('#tblISR').DataTable().clear();
            //recorrer data obtenida del backend
            for (var i = 0; i < ListaISR.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaISR[i].isr_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetalleISR">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaISR[i].isr_Activo == true ? '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnModalEditarISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaISR[i].isr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnActivarISRModal">Activar</button>' : '' : '';

                //agregar el row al datatable
                $('#tblISR').dataTable().fnAddData([
                    ListaISR[i].isr_Id,
                    (ListaISR[i].isr_RangoInicial % 1 == 0) ? ListaISR[i].isr_RangoInicial + ".00" : ListaISR[i].isr_RangoInicial,
                    (ListaISR[i].isr_RangoFinal % 1 == 0) ? ListaISR[i].isr_RangoFinal + ".00" : ListaISR[i].isr_RangoFinal,
                    (ListaISR[i].isr_Porcentaje % 1 == 0) ? ListaISR[i].isr_Porcentaje + ".00" : ListaISR[i].isr_Porcentaje,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        }
    });
    FullBody();
}

//crear isr
$(document).on("click", "#btnAgregarISR", function () {

    var validacionPermiso = userModelState("ISR/Create");

    if (validacionPermiso.status == true) {

        //OCULTAR VALIDACIONES
        Vaciar_ModalCrear();
        //llenar ddls
        $.ajax({
            url: "/ISR/EditGetDDL",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            //llenar los dropdownlists
            .done(function (data) {
                $("#Crear #tde_IdTipoDedu").empty();
                $("#Crear #tde_IdTipoDedu").append("<option value='0'>Selecione una opción...</option>");
                $.each(data, function (i, iter) {
                    $("#Crear #tde_IdTipoDedu").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });
        //DESPLEGAR MODAL DE CREACION
        $("#AgregarISR").modal({ backdrop: 'static', keyboard: false });
    }
});

//crear nuevo rango isr
$('#btnCreateISR').click(function () {

    var rangoInicial = $("#Crear #isr_RangoInicial").val();
    var rangoFinal = $("#Crear #isr_RangoFinal").val();
    var tipoDeduccion = $("#Crear #tde_IdTipoDedu").val();
    var porcentaje = $("#Crear #isr_Porcentaje").val();

    if (DataAnnotationsCrear(rangoInicial, rangoFinal, tipoDeduccion, porcentaje)) {
        $('#btnCreateISR').attr('disabled', true);


        //var data = $("#frmISRCreate").serializeArray();
        var data = {
            isr_RangoInicial: FormatearMonto($("#Crear #isr_RangoInicial").val()),
            isr_RangoFinal: FormatearMonto($("#Crear #isr_RangoFinal").val()),
            tde_IdTipoDedu: $("#Crear #tde_IdTipoDedu").val(),
            isr_Porcentaje: FormatearMonto($("#Crear #isr_Porcentaje").val())
        };

        $.ajax({
            url: "/ISR/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //validar respuesta del backend
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                $("#AgregarISR").modal('hide');
                cargarGridISR();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
        $('#btnCreateISR').attr('disabled', false);
    }

});








//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblISR tbody tr td #btnModalEditarISR", function () {

    var validacionPermiso = userModelState("ISR/Edit");

    if (validacionPermiso.status == true) {

        //DESBLOQUEAR BOTON DE EDITAR
        $('#btnEditarISR').attr('disabled', false);
        //CAPTURAR EL ID
        var ID = $(this).data('id');
        $('#frmEditISR #Validation_tde_IdTipoDedu').css('display', 'none');
        $('#frmEditISR .messageValidation').css('display', 'none');
        $('#frmEditISR .asterisco').removeClass('text-danger');
        //SETEAR LA VARIABLE GLOBAL DE INACTIVAR
        InactivarID = ID;
        //OCULTAR VALIDACIONES
        Vaciar_ModalEditar();
        //EJECUTAR LA PETICION AL SERVIDOR
        $.ajax({
            url: "/ISR/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                if (data) {
                    $("#Editar #isr_Id").val(data.isr_Id);
                    $("#Editar #isr_RangoInicial").val((data.isr_RangoInicial % 1 == 0) ? data.isr_RangoInicial + ".00" : data.isr_RangoInicial);
                    $("#Editar #isr_RangoFinal").val((data.isr_RangoFinal % 1 == 0) ? data.isr_RangoFinal + ".00" : data.isr_RangoFinal);
                    $("#Editar #isr_Porcentaje").val((data.isr_Porcentaje % 1 == 0) ? data.isr_Porcentaje + ".00" : data.isr_Porcentaje);
                    $("#Editar #tde_IdTipoDedu").val(data.tde_IdTipoDedu);
                    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
                    $(".rangoInicial").focus();
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedId = data.tde_IdTipoDedu;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                    $.ajax({
                        url: "/ISR/EditGetDDL",
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
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se cargó la información, contacte al administrador',
                    });
                }
            });
    }

});

$('#btnEditarISR').click(function () {
    $('#btnEditarISR').attr('disabled', false);
    $('#btnEditISR2').attr('disabled', false);
    var rangoInicial = $("#Editar #isr_RangoInicial").val();
    var rangoFinal = $("#Editar #isr_RangoFinal").val();
    var tipoDeduccion = $("#Editar #tde_IdTipoDedu").val();
    var porcentaje = $("#Editar #isr_Porcentaje").val();

    if (DataAnnotationsEditar(rangoInicial, rangoFinal, tipoDeduccion, porcentaje)) {
        $("#EditarISR").modal('hide');
        $("#EditarISRConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $('#btnEditarISR').attr('disabled', true);
    }
});

$('#btnRegresar').click(function () {
    $("#EditarISRConfirmacion").modal('hide');
    $('#btnEditISR2').attr('disabled', false);
    $('#btnEditarISR').attr('disabled', false);
    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditISR2").click(function () {
    $('#btnEditISR2').attr('disabled', true);
    var rangoInicial = $("#Editar #isr_RangoInicial").val();
    var rangoFinal = $("#Editar #isr_RangoFinal").val();
    var tipoDeduccion = $("#Editar #tde_IdTipoDedu").val();
    var porcentaje = $("#Editar #isr_Porcentaje").val();

    if (DataAnnotationsEditar(rangoInicial, rangoFinal, tipoDeduccion, porcentaje)) {
        //BLOQUEAR BOTON DE EDITAR
        $('#btnEditarISR').attr('disabled', true);
        //SERIALIZAR EL FORMULARIO
        //var data = $("#frmEditISR").serializeArray();

        var data = {
            isr_Id: $("#Editar #isr_Id").val(),
            isr_RangoInicial: FormatearMonto($("#Editar #isr_RangoInicial").val()),
            isr_RangoFinal: FormatearMonto($("#Editar #isr_RangoFinal").val()),
            tde_IdTipoDedu: $("#Editar #tde_IdTipoDedu").val(),
            isr_Porcentaje: FormatearMonto($("#Editar #isr_Porcentaje").val())
        };

        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/ISR/Edit",
            method: "POST",
            data: data
        }).done(function (data) {
            //DESBLOQUEAR BOTON DE EDITAR
            $('#btnEditarISR').attr('disabled', false);
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }
            else {
                //BLOQUEAR BOTON DE EDITAR
                $('#btnEditarISR').attr('disabled', true);
                //REFRESCAR LA DATA DEL DATATABLE
                cargarGridISR();
                //OCULTAR MODAL DE EDICION
                $("#EditarISRConfirmacion").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
        });
    }
});


//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    //OCULTAR MODAL DE EDITAR
    $("#EditarISR").modal('hide');
});


//DESPLEGAR MODAL DE INACTIVACION
$(document).on("click", "#btnModalInactivarISR", function () {
    var validacionPermiso = userModelState("ISR/Inactivar");

    if (validacionPermiso.status == true) {

        //DESBLOQUEAR BOTON
        $("#btnInactivarISR").attr("disabled", false);
        //OCULTAR EL MODAL DE EDICION
        $("#EditarISR").modal('hide');
        //MOSTRAR MODAL DE INACTIVACION
        $("#InactivarISR").modal({ backdrop: 'static', keyboard: false });
    }
});

//CERRAR MODAL DE INACTIVACION
$(document).on("click", "#btnBack", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarISR").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
});

//Inactivar registro Techos Deducciones
$("#btnInactivarISR").click(function () {
    //BLOQUEAR BOTON
    $("#btnInactivarISR").attr("disabled", true);
    var data = $("#frmInactivarISR").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/ISR/Inactivar/" + InactivarID,
        method: "POST",
        data: { id: InactivarID }
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR BOTON
            $("#btnInactivarISR").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            $("#InactivarISR").modal('hide');
            cargarGridISR();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    InactivarID = 0;
});

//DECLARAR LA VARIABLE DE ACTIVACION
var ActivarID = 0;
//DESPLEGAR MODAL DE ACTIVACION
$(document).on("click", "#tblISR tbody tr td #btnActivarISRModal", function () {

    var validacionPermiso = userModelState("ISR/Activar");

    if (validacionPermiso.status == true) {

        //CAPTURAR EL ID DEL REGISTRO
        ActivarID = $(this).data('id');
        //DESBLOQUEAR BOTON
        $("#btnActivarISR").attr("disabled", false);
        //MOSTRAR MODAL DE ACTIVACION
        $("#ActivarISR").modal({ backdrop: 'static', keyboard: false });
    }
});

//CERRAR MODAL DE ACTIVACION
$(document).on("click", "#btnBackActivar", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#ActivarISR").modal('hide');
});

//ACTIVAR REGISTRO
$("#btnActivarISR").click(function () {
    //BLOQUEAR BOTON
    $("#btnActivarISR").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/ISR/Activar/" + ActivarID,
        method: "POST",
        data: { id: ActivarID }
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR BOTON
            $("#btnActivarISR").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            $("#ActivarISR").modal('hide');
            cargarGridISR();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    ActivarID = 0;
});





//DETALLES
$(document).on("click", "#tblISR tbody tr td #btnDetalleISR", function () {
    var validacionPermiso = userModelState("ISR/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        $.ajax({
            url: "/ISR/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    var FechaCrea = FechaFormato(data[0].isr_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].isr_FechaModifica);
                    $("#Detalles #isr_Id").html(data[0].isr_Id);
                    $("#Detalles #isr_RangoInicial").html((data[0].isr_RangoInicial % 1 == 0) ? data[0].isr_RangoInicial + ".00" : data[0].isr_RangoInicial);
                    $("#Detalles #isr_RangoFinal").html((data[0].isr_RangoFinal % 1 == 0) ? data[0].isr_RangoFinal + ".00" : data[0].isr_RangoFinal);
                    $("#Detalles #isr_Porcentaje").html((data[0].isr_Porcentaje % 1 == 0) ? data[0].isr_Porcentaje + ".00" : data[0].isr_Porcentaje);
                    $("#Detalles #tde_IdTipoDedu").html(data[0].tde_Descripcion);
                    $("#Detalles #isr_UsuarioCrea").html(data[0].isr_UsuarioCrea);
                    $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#FechaCrea").html(FechaCrea);
                    $("#isr_UsuarioModifica").html(data.isr_UsuarioModifica);
                    data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #isr_FechaModifica").html(FechaModifica);
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedId = data[0].tde_IdTipoDedu;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                    $.ajax({
                        url: "/ISR/EditGetDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            $("#Detalles #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
                        });
                    $("#DetailsISR").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No cargó la información, contacte al administrador',
                    });
                }
            });
    }
});




//FUNCION: OCULTAR VALIDACIONES DE CREACION
function Vaciar_ModalCrear() {
    //VACIADO DE INPUTS
    $("#Crear #isr_RangoInicial").val("");
    $("#Crear #isr_RangoFinal").val("");
    $("#Crear #tde_IdTipoDedu").val("0");
    $("#Crear #isr_Porcentaje").val("");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #isr_RangoInicialValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #AsteriscoRangoInicial").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #isr_RangoFinalValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #AsteriscoRangoFinal").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #isr_TipoDeduccionValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #AsteriscoTipoDeduccion").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Crear #isr_PorcentajeValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");

}

//FUNCION: OCULTAR VALIDACIONES DE EDICION
function Vaciar_ModalEditar() {
    //VACIADO DE INPUTS
    $("#Editar #isr_RangoInicial").val("");
    $("#Editar #isr_RangoFinal").val("");
    $("#Editar #tde_IdTipoDedu").val("0");
    $("#Editar #isr_Porcentaje").val("");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #isr_RangoInicialValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoRangoInicial").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #isr_RangoFinalValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoRangoFinal").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #isr_TipoDeduccionValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoTipoDeduccion").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #isr_PorcentajeValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsCrear(RangoInicial, RangoFinal, TipoDeduccion, Porcentaje) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (RangoInicial != "-1") {
        //RANGO INICIAL

        if (parseFloat(FormatearMonto(RangoInicial)) >= parseFloat(FormatearMonto($("#Crear #isr_RangoFinal").val()))) {
            $("#Crear #AsteriscoRangoFinal").addClass("text-danger");
            $("#Crear #isr_RangoFinalValidacion").empty();
            $("#Crear #isr_RangoFinalValidacion").html("El campo Rango Final debe ser mayor que el rango inicial.");
            $("#Crear #isr_RangoFinalValidacion").show();
            ModelState = false;
        }
        else {
            $("#Crear #AsteriscoRangoFinal").removeClass("text-danger");
            $("#Crear #isr_RangoFinalValidacion").empty();
            $("#Crear #isr_RangoFinalValidacion").html("El campo Rango Final es requerido.");
            $("#Crear #isr_RangoFinalValidacion").hide();
        }

        if (RangoInicial == "" || RangoInicial == null || RangoInicial == undefined || RangoInicial == 0 || RangoInicial == 0.00) {
            $("#Crear #AsteriscoRangoInicial").addClass("text-danger");
            $("#Crear #isr_RangoInicialValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoRangoInicial").removeClass("text-danger");
            $("#Crear #isr_RangoInicialValidacion").hide();
            if (parseInt(RangoInicial) < 0 || parseFloat(RangoInicial) < 0.00) {

                $("#Crear #AsteriscoRangoInicial").addClass("text-danger");
                $("#Crear #isr_RangoInicialValidacion").empty();
                $("#Crear #isr_RangoInicialValidacion").html("El campo Rango Inicial no puede ser menor que cero.");
                $("#Crear #isr_RangoInicialValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #isr_RangoInicialValidacion").empty();
                $("#Crear #isr_RangoInicialValidacion").html("El campo Rango Inicial es requerido.");
                $("#Crear #AsteriscoRangoInicial").removeClass("text-danger");
                $("#Crear #isr_RangoInicialValidacion").hide();
            }
        }
    }


    if (RangoFinal != "-1") {
        //RANGO FINAL
        if (RangoFinal == "" || RangoFinal == null || RangoFinal == undefined) {
            $("#Crear #AsteriscoRangoFinal").addClass("text-danger");
            $("#Crear #isr_RangoFinalValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoRangoFinal").removeClass("text-danger");
            $("#Crear #isr_RangoFinalValidacion").hide();

            //Validacion rango final comentada
            // if (parseFloat(FormatearMonto(RangoFinal)) <= parseFloat(FormatearMonto($("#Crear #isr_RangoInicial").val())) || parseFloat(FormatearMonto(RangoFinal)) == 0) {
            //     $("#Crear #AsteriscoRangoFinal").addClass("text-danger");
            //     $("#Crear #isr_RangoFinalValidacion").empty();
            //     $("#Crear #isr_RangoFinalValidacion").html("El campo Rango Final debe ser mayor que el rango inicial.");
            //     $("#Crear #isr_RangoFinalValidacion").show();
            //     ModelState = false;
            // } else {
            //     $("#Crear #isr_RangoFinalValidacion").empty();
            //     $("#Crear #isr_RangoFinalValidacion").html("El campo Rango Final es requerido.");
            //     $("#Crear #AsteriscoRangoFinal").removeClass("text-danger");
            //     $("#Crear #isr_RangoFinalValidacion").hide();
            // }

        }
    }


    if (TipoDeduccion != "-1") {
        //Telefono
        if (TipoDeduccion == "" || TipoDeduccion == "0" || TipoDeduccion == 0 || TipoDeduccion == null) {
            $("#Crear #tde_IdTipoDedu").val("0");
            //MOSTRAR DATAANNOTATIONS
            $("#Crear #isr_TipoDeduccionValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #AsteriscoTipoDeduccion").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #isr_TipoDeduccionValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #AsteriscoTipoDeduccion").removeClass("text-danger");
        }
    }


    if (Porcentaje != "-1") {
        //PORCENTAJE
        if (Porcentaje == "" || Porcentaje == null || Porcentaje == undefined) {
            $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
            $("#Crear #isr_PorcentajeValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
            $("#Crear #isr_PorcentajeValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Porcentaje = parseFloat(FormatearMonto(Porcentaje));
            if (parseInt(Porcentaje) < 0 || parseFloat(Porcentaje) < 0.00) {
                $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
                $("#Crear #isr_PorcentajeValidacion").empty();
                $("#Crear #isr_PorcentajeValidacion").html("El campo Porcentaje no puede ser menor a 0.");
                $("#Crear #isr_PorcentajeValidacion").show();
                ModelState = false;
            } else if (Porcentaje > 100) {
                $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
                $("#Crear #isr_PorcentajeValidacion").empty();
                $("#Crear #isr_PorcentajeValidacion").html("El campo Porcentaje no puede ser mayor a 100.");
                $("#Crear #isr_PorcentajeValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #isr_PorcentajeValidacion").empty();
                $("#Crear #isr_PorcentajeValidacion").html("El campo Porcentaje es requerido.");
                $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
                $("#Crear #isr_PorcentajeValidacion").hide();
            }
        }
    }

    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsEditar(RangoInicial, RangoFinal, TipoDeduccion, Porcentaje) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (RangoInicial != "-1") {

        // if (parseFloat(FormatearMonto(RangoInicial)) >= parseFloat(FormatearMonto($("#Editar #isr_RangoFinal").val()))) {
        //     $("#Editar #AsteriscoRangoFinal").addClass("text-danger");
        //     $("#Editar #isr_RangoFinalValidacion").empty();
        //     $("#Editar #isr_RangoFinalValidacion").html("El campo Rango Final debe ser mayor que el rango inicial.");
        //     $("#Editar #isr_RangoFinalValidacion").show();
        //     ModelState = false;
        // }
        // else {
        //     $("#Editar #AsteriscoRangoFinal").removeClass("text-danger");
        //     $("#Editar #isr_RangoFinalValidacion").empty();
        //     $("#Editar #isr_RangoFinalValidacion").html("El campo Rango Final es requerido.");
        //     $("#Editar #isr_RangoFinalValidacion").hide();
        // }

        //RANGO INICIAL
        if (RangoInicial == "" || RangoInicial == null || RangoInicial == undefined || RangoInicial == 0 || RangoInicial == 0.00) {
            $("#Editar #AsteriscoRangoInicial").addClass("text-danger");
            $("#Editar #isr_RangoInicialValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoRangoInicial").removeClass("text-danger");
            $("#Editar #isr_RangoInicialValidacion").hide();
            if (parseInt(RangoInicial) < 0 || parseFloat(RangoInicial) < 0.00) {

                $("#Editar #AsteriscoRangoInicial").addClass("text-danger");
                $("#Editar #isr_RangoInicialValidacion").empty();
                $("#Editar #isr_RangoInicialValidacion").html("El campo Rango Final no puede ser menor que cero.");
                $("#Editar #isr_RangoInicialValidacion").show();
                ModelState = false;
            } else {

                $("#Editar #AsteriscoRangoInicial").removeClass("text-danger");
                $("#Editar #isr_RangoInicialValidacion").hide();
            }
        }
    }


    if (RangoFinal != "-1") {
        //RANGO FINAL
        if (RangoFinal == "" || RangoFinal == null || RangoFinal == undefined) {
            $("#Editar #AsteriscoRangoFinal").addClass("text-danger");
            $("#Editar #isr_RangoFinalValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoRangoFinal").removeClass("text-danger");
            $("#Editar #isr_RangoFinalValidacion").hide();

            //        //Validacion rango final comentada
            //        // if (parseFloat(FormatearMonto(RangoFinal)) <= parseFloat(FormatearMonto($("#Editar #isr_RangoInicial").val())) || parseFloat(FormatearMonto(RangoFinal)) == 0) {

            //        //     $("#Editar #AsteriscoRangoFinal").addClass("text-danger");
            //        //     $("#Editar #isr_RangoFinalValidacion").empty();
            //        //     $("#Editar #isr_RangoFinalValidacion").html("El campo Rango Final debe ser mayor que el rango inicial.");
            //        //     $("#Editar #isr_RangoFinalValidacion").show();
            //        //     ModelState = false;
            //        // } else {
            //        //     $("#Editar #isr_RangoFinalValidacion").empty();
            //        //     $("#Editar #isr_RangoFinalValidacion").html("El campo Rango Final es requerido.");
            //        //     $("#Editar #AsteriscoRangoFinal").removeClass("text-danger");
            //        //     $("#Editar #isr_RangoFinalValidacion").hide();
            //        // }

        }
    }


    if (TipoDeduccion != "-1") {
        //Telefono
        if (TipoDeduccion == "" || TipoDeduccion == "0" || TipoDeduccion == 0) {
            //MOSTRAR DATAANNOTATIONS
            $("#Editar #isr_TipoDeduccionValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #AsteriscoTipoDeduccion").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #isr_TipoDeduccionValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #AsteriscoTipoDeduccion").removeClass("text-danger");
        }
    }


    if (Porcentaje != "-1") {
        //PORCENTAJE
        if (Porcentaje == "" || Porcentaje == null || Porcentaje == undefined) {
            $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
            $("#Editar #isr_PorcentajeValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
            $("#Editar #isr_PorcentajeValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Porcentaje = parseFloat(FormatearMonto(Porcentaje));
            if (Porcentaje < 0 || Porcentaje < 0.00) {
                $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
                $("#Editar #isr_PorcentajeValidacion").empty();
                $("#Editar #isr_PorcentajeValidacion").html("El campo Porcentaje no puede ser menor a 0.");
                $("#Editar #isr_PorcentajeValidacion").show();
                ModelState = false;
            } else if (Porcentaje > 100) {
                $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
                $("#Editar #isr_PorcentajeValidacion").empty();
                $("#Editar #isr_PorcentajeValidacion").html("El campo Porcentaje no puede ser mayor a 100.");
                $("#Editar #isr_PorcentajeValidacion").show();
                ModelState = false;
            } else {
                $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
                $("#Editar #isr_PorcentajeValidacion").hide();
            }
        }
    }

    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}

$('#Crear #tde_IdTipoDedu').blur(function () {
    let tde_IdTipoDedu = $(this).val();
    if (tde_IdTipoDedu == "" || tde_IdTipoDedu == 0 || tde_IdTipoDedu == "0") {
        $("#Crear #tde_IdTipoDedu").val("0");
        //MOSTRAR DATAANNOTATIONS
        $("#Crear #isr_TipoDeduccionValidacion").show();
        //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
        $("#Crear #AsteriscoTipoDeduccion").addClass("text-danger");
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #isr_TipoDeduccionValidacion").hide();
        //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
        $("#Crear #AsteriscoTipoDeduccion").removeClass("text-danger");
    }
});

//FUNCION: FORMATEAR MONTOS A DECIMAL
function FormatearMonto(StringValue) {
    //SEGMENTAR LA CADENA DE MONTO
    var indices = StringValue.split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i <= indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);
    //RETORNAR MONTO FORMATEADO
    return MontoFormateado;
}
