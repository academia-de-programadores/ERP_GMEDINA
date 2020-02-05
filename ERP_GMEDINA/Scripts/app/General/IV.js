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
$("#frmEditIV").submit(function (e) {
    return false;
});
$("#frmIVCreate").submit(function (e) {
    return false;
});

$("#EditarIV").submit(function (e) {
    return false;
});
$("#ActivarIV").submit(function (e) {
    return false;
});

$("#InactivarIV").submit(function (e) {
    return false;
});

//cargar grid
function cargarGridIV() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        "/TechoImpuestoVecinal/GetData",
        "GET",        
    (data) => {
        if (data.length == 0) {
            iziToast.error({
                title: 'Error',
                message: 'No se cargó la información, contacte al administrador',
            });
        }
        else {
            var LitaIV = data;
            //limpiar datatable
            $('#tblIV').DataTable().clear();
            //recorrer data obtenida del backend
            for (var i = 0; i < LitaIV.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = LitaIV[i].timv_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + LitaIV[i].timv_IdTechoImpuestoVecinal + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetalleIV">Detalles</button>';

                //variable boton editar
                var botonEditar = LitaIV[i].timv_Activo == true ? '<button data-id = "' + LitaIV[i].timv_IdTechoImpuestoVecinal + '" type="button" class="btn btn-default btn-xs"  id="btnModalEditarIV">Editar</button>' : '';

                //variable boton activar
                var botonActivar = LitaIV[i].timv_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + LitaIV[i].timv_IdTechoImpuestoVecinal + '" type="button" class="btn btn-default btn-xs"  id="btnActivarIVModal">Activar</button>' : '' : '';

                //agregar el row al datatable
                $('#tblIV').dataTable().fnAddData([
                    LitaIV[i].timv_IdTechoImpuestoVecinal,
                    LitaIV[i].mun_Nombre,
                    LitaIV[i].tde_Descripcion,
                    (LitaIV[i].timv_RangoInicio % 1 == 0) ? LitaIV[i].timv_RangoInicio + ".00" : LitaIV[i].timv_RangoInicio,
                    (LitaIV[i].timv_RangoFin % 1 == 0) ? LitaIV[i].timv_RangoFin + ".00" : LitaIV[i].timv_RangoFin,
                    (LitaIV[i].timv_Rango % 1 == 0) ? LitaIV[i].timv_Rango + ".00" : LitaIV[i].timv_Rango,
                    (LitaIV[i].timv_Impuesto % 1 == 0) ? LitaIV[i].timv_Impuesto + ".00" : LitaIV[i].timv_Impuesto,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        }
    });
    FullBody();
}


//cargar grid calculo impuesto vecinal
function cargarGridCalculoIV() {

    var esAdministrador = $("#rol_Usuario").val();
    $.ajax({
        url: "/TechoImpuestoVecinal/GetDataCalculo",
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
            var LitaCalculoIV = data;
            //limpiar datatable
            $('#tblCalculoIV').DataTable().clear();
            //recorrer data obtenida del backend
            for (var i = 0; i < LitaCalculoIV.length; i++) {
        
                //agregar el row al datatable
                $('#tblCalculoIV').dataTable().fnAddData([
                    LitaCalculoIV[i].dimv_Id,
                    LitaCalculoIV[i].per_Nombres + ' ' + LitaCalculoIV[i].per_Apellidos,
                    (LitaCalculoIV[i].dimv_MontoTotal % 1 == 0) ? LitaCalculoIV[i].dimv_MontoTotal + ".00" : LitaCalculoIV[i].dimv_MontoTotal,
                    (LitaCalculoIV[i].dimv_CuotaAPagar % 1 == 0) ? LitaCalculoIV[i].dimv_CuotaAPagar + ".00" : LitaCalculoIV[i].dimv_CuotaAPagar,
                ]);
            }
        }
    });
    FullBody();
}


//crear iv
//crear iv
$(document).on("click", "#btnAgregarIV", function () {
    //OCULTAR VALIDACIONES
    Vaciar_ModalCrear();

    //llenar ddls
    $.ajax({
        url: "/TechoImpuestoVecinal/EditGetDDLTipoDedu",
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

    $.ajax({
        url: "/TechoImpuestoVecinal/EditGetDDLMuni",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //llenar los dropdownlists
        .done(function (data) {
            $("#Crear #mun_Codigo").empty();
            $("#Crear #mun_Codigo").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #mun_Codigo").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //DESPLEGAR MODAL DE CREACION
    $("#AgregarIV").modal({ backdrop: 'static', keyboard: false });
});

//crear nuevo rango iv
$('#btnCreateIV').click(function () {

    var rangoInicio1 = $("#Crear #timv_RangoInicio").val();
    var rangoFin1 = $("#Crear #timv_RangoFin").val();
    var codmuni1 = $("#Crear #mun_Codigo").val();
    var rango1 = $("#Crear #timv_Rango").val();
    var tipoDeduccion1 = $("#Crear #tde_IdTipoDedu").val();
    var impuesto1 = $("#Crear #timv_Impuesto").val();

    $('#btnCreateIV').attr('disabled', false);
    //var data = $("#frmIVCreate").serializeArray();
    if (DataAnnotationsCrear(codmuni1, tipoDeduccion1, rangoInicio1, rangoFin1, rango1, impuesto1)) {
        // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)        
        //var dataSinFormato = $("#frmIVCreate").serializeArray();
        //console.log(dataSinFormato);
        var __RequestVerificati1onToken1 = $("input[name=__RequestVerificationToken]").val();        
        var data = {
            __RequestVerificati1onToken: __RequestVerificati1onToken1,
            mun_Codigo: codmuni1,
            tde_IdTipoDedu: tipoDeduccion1,
            timv_RangoInicio: FormatearMonto(rangoInicio1),
            timv_RangoFin: FormatearMonto(rangoFin1),
            timv_Rango: FormatearMonto(rango1),
            timv_Impuesto: FormatearMonto(impuesto1)
        };
        $.ajax({
            url: "/TechoImpuestoVecinal/Create",
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
                cargarGridIV();
                $("#AgregarIV").modal('hide');
               
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
        $('#btnCreateIV').attr('disabled', true);
    }
});
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblIV tbody tr td #btnModalEditarIV", function () {
    //DESBLOQUEAR BOTON DE EDITAR
    $('#btnEditarIV').attr('disabled', false);
    //CAPTURAR EL ID
    var ID = $(this).data('id');
    $('#frmIVEdit #Validation_tde_IdTipoDedu').css('display', 'none');
    $('#frmIVEdit .messageValidation').css('display', 'none');
    $('#frmIVEdit .asterisco').removeClass('text-danger');
    //SETEAR LA VARIABLE GLOBAL DE INACTIVAR
    InactivarID = ID;
    //OCULTAR VALIDACIONES
    Vaciar_ModalEditar();
    //EJECUTAR LA PETICION AL SERVIDOR
    $.ajax({
        url: "/TechoImpuestoVecinal/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            if (data) {
                $("#Editar #timv_IdTechoImpuestoVecinal").val(data.timv_IdTechoImpuestoVecinal);
                $("#Editar #timv_RangoInicio").val((data.timv_RangoInicio % 1 == 0) ? data.timv_RangoInicio + ".00" : data.timv_RangoInicio);
                $("#Editar #timv_RangoFin").val((data.timv_RangoFin % 1 == 0) ? data.timv_RangoFin + ".00" : data.timv_RangoFin);
                $("#Editar #timv_Rango").val((data.timv_Rango % 1 == 0) ? data.timv_Rango + ".00" : data.timv_Rango);
                $("#Editar #timv_Impuesto").val((data.timv_Impuesto % 1 == 0) ? data.timv_Impuesto + ".00" : data.timv_Impuesto);
                $("#Editar #tde_IdTipoDedu").val(data.tde_IdTipoDedu);
                $("#Editar #mun_Nombre").val(data.mun_Nombre);
                $(".rangoInicial").focus();
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/TechoImpuestoVecinal/EditGetDDLTipoDedu",
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

                var SelectedIdMuni = data.mun_Codigo;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/TechoImpuestoVecinal/EditGetDDLMuni",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #mun_Codigo").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Editar #mun_Codigo").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Editar #mun_Codigo").append("<option" + (iter.Id == SelectedIdMuni ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarIV").modal({ backdrop: 'static', keyboard: false });
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

$("#btnEditarIV").click(function () {
    var rangoInicial1 = $("#Editar #timv_RangoInicio").val();
    var rangoFinal1 = $("#Editar #timv_RangoFin").val();
    var rango1 = $("#Editar #timv_Rango").val();
    var tipoDeduccion1 = $("#Editar #tde_IdTipoDedu").val();
    var municipio1 = $("#Editar #mun_Codigo").val();
    var impuesto1 = $("#Editar #timv_Impuesto").val();

    if (DataAnnotationsEditar(municipio1, tipoDeduccion1, rangoInicial1, rangoFinal1, rango1, impuesto1)) {
        $("#EditarIV").modal('hide');
        $("#EditarIVConfirmacion").modal();
    }
});

$('#btnRegresarIV').click(function () {
    $("#EditarIVConfirmacion").modal('hide');
    $("#EditarIV").modal();
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditIVConfirmacion").click(function () {
    //BLOQUEAR BOTON DE EDITAR
    $('#btnEditIVConfirmacion').attr('disabled', true);
    //SERIALIZAR EL FORMULARIO

    var timv_IdTechoImpuestoVecinal1 = $("#Editar #timv_IdTechoImpuestoVecinal").val();
    var timv_RangoInicio1 = $("#Editar #timv_RangoInicio").val();
    var timv_RangoFin1 = $("#Editar #timv_RangoFin").val();
    var tde_IdTipoDedu1 = $("#Editar #tde_IdTipoDedu").val();
    var timv_Impuesto1 = $("#Editar #timv_Impuesto").val();
    var timv_Rango1 = $("#Editar #timv_Rango").val();
    var mun_Codigo1 = $("#Editar #mun_Codigo").val();
    


    var data_array = {
            timv_IdTechoImpuestoVecinal: timv_IdTechoImpuestoVecinal1,
            mun_Codigo: mun_Codigo1,
            tde_IdTipoDedu: tde_IdTipoDedu1,
            timv_RangoInicio: FormatearMonto(timv_RangoInicio1),
            timv_RangoFin: FormatearMonto(timv_RangoFin1),
            timv_Rango: FormatearMonto(timv_Rango1),
            timv_Impuesto: parseFloat(timv_Impuesto1.replace(/,/g,""))
    };
    var Matriz = {
        tbTechoImpuestoVecinal: data_array,
        Impuesto: timv_Impuesto1
    };

    console.log(data_array);
    console.log(Matriz);


    if (DataAnnotationsEditar(mun_Codigo1, tde_IdTipoDedu1, timv_RangoInicio1, timv_RangoFin1, timv_Rango1, timv_Impuesto1)) {
        //, decimal 
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/TechoImpuestoVecinal/Edit",
            method: "POST",
            data: Matriz
        }).done(function (data) {
            //DESBLOQUEAR BOTON DE EDITAR
            $('#btnEditIVConfirmacion').attr('disabled', false);
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }
            else {
                //BLOQUEAR BOTON DE EDITAR
                $('#btnEditarIV').attr('disabled', true);
                //REFRESCAR LA DATA DEL DATATABLE
                cargarGridIV();
                //OCULTAR MODAL DE EDICION
                $("#EditarIVConfirmacion").modal('hide');
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
    $("#EditarIV").modal('hide');
});

//DESPLEGAR MODAL DE INACTIVACION
$(document).on("click", "#btnModalInactivarIV", function () {
    //DESBLOQUEAR BOTON
    $("#btnInactivarIV").attr("disabled", false);
    //OCULTAR EL MODAL DE EDICION
    $("#EditarIV").modal('hide');
    //MOSTRAR MODAL DE INACTIVACION
    $("#InactivarIV").modal({ backdrop: 'static', keyboard: false });
});

//CERRAR MODAL DE INACTIVACION
$(document).on("click", "#btnBackIV", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#InactivarIV").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarIV").modal({ backdrop: 'static', keyboard: false });
});

//Inactivar registro Techos Deducciones
$("#btnInactivarIV").click(function () {
    //BLOQUEAR BOTON
    $("#btnInactivarIV").attr("disabled", true);
    var data = $("#frmInactivarIV").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TechoImpuestoVecinal/Inactivar/" + InactivarID,
        method: "POST",
        data: { id: InactivarID }
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR BOTON
            $("#btnInactivarIV").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridIV();

            $("#InactivarIV").modal('hide');
            
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
$(document).on("click", "#tblIV tbody tr td #btnActivarIVModal", function () {
    //CAPTURAR EL ID DEL REGISTRO
    ActivarID = $(this).data('id');
    //DESBLOQUEAR BOTON
    $("#btnActivarIV").attr("disabled", false);
    //MOSTRAR MODAL DE ACTIVACION
    $("#ActivarIV").modal({ backdrop: 'static', keyboard: false });
});

//CERRAR MODAL DE ACTIVACION
$(document).on("click", "#btnBackActivar", function () {
    //OCULTAR MODAL DE INACTIVACION
    $("#ActivarIV").modal('hide');
});

//ACTIVAR REGISTRO
$("#btnActivarIV").click(function () {
    //BLOQUEAR BOTON
    $("#btnActivarIV").attr("disabled", true);
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TechoImpuestoVecinal/Activar/" + ActivarID,
        method: "POST",
        data: { id: ActivarID }
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR BOTON
            $("#btnActivarIV").attr("disabled", false);
            //MOSTRAR MENSAJE DE ERROR
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridIV();
            $("#ActivarIV").modal('hide');
           
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
$(document).on("click", "#tblIV tbody tr td #btnDetalleIV", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/TechoImpuestoVecinal/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {

            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].timv_FechaCrea);
                var FechaModifica = FechaFormato(data[0].timv_FechaModifica);
                $("#DetailsIV #mun_Codigo").html(data[0].mun_Nombre);
                $("#DetailsIV #tde_IdTipoDedu").html(data[0].tde_Descripcion);
                $("#DetailsIV #timv_RangoInicio").html((data[0].timv_RangoInicio % 1 == 0) ? data[0].timv_RangoInicio + ".00" : data[0].timv_RangoInicio);
                $("#DetailsIV #timv_RangoFin").html((data[0].timv_RangoFin % 1 == 0) ? data[0].timv_RangoFin + ".00" : data[0].timv_RangoFin);
                $("#DetailsIV #timv_Rango").html((data[0].timv_Rango % 1 == 0) ? data[0].timv_Rango + ".00" : data[0].timv_Rango);
                $("#DetailsIV #timv_Impuesto").html((data[0].timv_Impuesto % 1 == 0) ? data[0].timv_Impuesto + ".00" : data[0].timv_Impuesto);
                $("#DetailsIV #timv_UsuarioCrea").html(data[0].timv_UsuarioCrea);
                $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#FechaCrea").html(FechaCrea);
                $("#timv_UsuarioModifica").html(data.timv_UsuarioModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#DetailsIV #timv_FechaModifica").html(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/TechoImpuestoVecinal/EditGetDDLTipoDedu",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        $("#DetailsIV #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
                    });
                $("#DetailsIV").modal({ backdrop: 'static', keyboard: false });

                var SelectedIdmuni = data[0].mun_Codigo;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
                $.ajax({
                    url: "/TechoImpuestoVecinal/EditGetDDLMuni",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            if (iter.Id == SelectedIdmuni) {
                                $("#DetailsIV #mun_Codigo").html(iter.mun_Codigo);
                            }
                        });
                    });

                $("#DetailsIV").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No cargó la información, contacte al administrador',
                });
            }
        });
});




//FUNCION: OCULTAR VALIDACIONES DE CREACION
function Vaciar_ModalCrear() {
    //VACIADO DE INPUTS
    $("#Crear #isr_RangoInicial").val("");
    $("#Crear #isr_RangoFinal").val("");
    $("#Crear #tde_IdTipoDedu").val(0);
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
    $("#Crear #timv_PorcentajeValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");

}

//FUNCION: OCULTAR VALIDACIONES DE EDICION
function Vaciar_ModalEditar() {
    //VACIADO DE INPUTS
    $("#Editar #timv_RangoInicio").val("");
    $("#Editar #timv_RangoFin").val("");
    $("#Editar #timv_Rango").val("");
    $("#Editar #tde_IdTipoDedu").val(0);
    $("#Editar #mun_Nombre").val(0);
    $("#Editar #timv_Porcentaje").val("");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #timv_RangoInicioValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoRangoInicio").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #timv_RangoFinValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoRangoFin").removeClass("text-danger");

    //OCULTAR DATAANNOTATIONS
    $("#Editar #timv_RangoValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoRango").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #tde_TipoDeduccionValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoTipoDeduccion").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #mun_NombreValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoMunicipio").removeClass("text-danger");

    //
    //OCULTAR DATAANNOTATIONS
    $("#Editar #timv_PorcentajeValidacion").hide();
    //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
    $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsCrear(Municipio, TipoDeduccion, RangoInicio, RangoFin, Rango, Impuesto) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (Municipio != "") {
        //Telefono
        if (Municipio == "" || Municipio == null) {
            //MOSTRAR DATAANNOTATIONS
            $("#Crear #mun_CodigoValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #AsteriscoMunicipio").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #mun_CodigoValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #AsteriscoMunicipio").removeClass("text-danger");
        }
    }

    if (TipoDeduccion != "-1") {
        //Telefono
        if (TipoDeduccion == "" || TipoDeduccion == "0" || TipoDeduccion == 0) {
            //MOSTRAR DATAANNOTATIONS
            $("#Crear #timv_TipoDeduccionValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Crear #AsteriscoTipoDeduccion").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Crear #timv_TipoDeduccionValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Crear #AsteriscoTipoDeduccion").removeClass("text-danger");
        }
    }

    if (RangoInicio != "-1") {
        //RANGO INICIAL

        if (parseFloat(FormatearMonto(RangoInicio)) >= parseFloat(FormatearMonto($("#Crear #timv_RangoFin").val()))) {
            $("#Crear #AsteriscoRangoInicio").addClass("text-danger");
            $("#Crear #timv_RangoInicioValidacion").empty();
            $("#Crear #timv_RangoInicioValidacion").html("El campo Rango Inicio debe ser mayor que el rango fin.");
            $("#Crear #timv_RangoInicioValidacion").show();
            ModelState = false;
        }
        else {
            $("#Crear #AsteriscoRangoInicio").removeClass("text-danger");
            $("#Crear #timv_RangoInicioValidacion").empty();
            $("#Crear #timv_RangoInicioValidacion").html("El campo Rango Final es requerido.");
            $("#Crear #timv_RangoInicioValidacion").hide();
        }

        if (RangoInicio == "" || RangoInicio == null || RangoInicio == undefined) {
            $("#Crear #AsteriscoRangoInicio").addClass("text-danger");
            $("#Crear #timv_RangoInicioValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoRangoInicio").removeClass("text-danger");
            $("#Crear #timv_RangoInicioValidacion").hide();
            if (parseInt(RangoInicio) < 0 || parseFloat(RangoInicio) < 0.00) {

                $("#Crear #AsteriscoRangoInicio").addClass("text-danger");
                $("#Crear #timv_RangoInicioValidacion").empty();
                $("#Crear #timv_RangoInicioValidacion").html("El campo Rango Inicial no puede ser menor que cero.");
                $("#Crear #timv_RangoInicioValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #timv_RangoInicioValidacion").empty();
                $("#Crear #timv_RangoInicioValidacion").html("El campo Rango Inicial es requerido.");
                $("#Crear #AsteriscoRangoInicio").removeClass("text-danger");
                $("#Crear #timv_RangoInicioValidacion").hide();
            }
        }
    }


    if (RangoFin != "-1") {
        //RANGO FINAL
        if (RangoFin == "" || RangoFin == null || RangoFin == undefined) {
            $("#Crear #AsteriscoRangoFin").addClass("text-danger");
            $("#Crear #timv_RangoFinValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoRangoFinal").removeClass("text-danger");
            $("#Crear #timv_RangoFinValidacion").hide();

            if (parseFloat(FormatearMonto(RangoFin)) <= parseFloat(FormatearMonto($("#Crear #timv_RangoInicio").val())) || parseFloat(FormatearMonto(RangoFin)) == 0) {
                $("#Crear #AsteriscoRangoFin").addClass("text-danger");
                $("#Crear #timv_RangoFinValidacion").empty();
                $("#Crear #timv_RangoFinValidacion").html("El campo Rango Fin debe ser mayor que el rango inicio.");
                $("#Crear #timv_RangoFinValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #timv_RangoFinValidacion").empty();
                $("#Crear #timv_RangoFinValidacion").html("El campo Rango Final es requerido.");
                $("#Crear #AsteriscoRangoFin").removeClass("text-danger");
                $("#Crear #timv_RangoFinValidacion").hide();
            }

        }
    }


    if (Rango != "-1") {
        //PORCENTAJE
        if (Rango == "" || Rango == null || Rango == undefined) {
            $("#Crear #AsteriscoRango").addClass("text-danger");
            $("#Crear #timv_RangoValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoRango").removeClass("text-danger");
            $("#Crear #timv_RangoValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Rango = parseFloat(FormatearMonto(Rango));
            if (parseInt(Rango) < 0 || parseFloat(Rango) < 0.00) {
                $("#Crear #AsteriscoRango").addClass("text-danger");
                $("#Crear #timv_RangoValidacion").empty();
                $("#Crear #timv_RangoValidacion").html("El campo Porcentaje no puede ser menor a 0.");
                $("#Crear #timv_RangoValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #timv_RangoValidacion").empty();
                $("#Crear #timv_RangoValidacion").html("El campo Porcentaje es requerido.");
                $("#Crear #AsteriscoRango").removeClass("text-danger");
                $("#Crear #timv_RangoValidacion").hide();
            }
        }
    }

    if (Impuesto != "-1") {
        //PORCENTAJE
        if (Impuesto == "" || Impuesto == null || Impuesto == undefined) {
            $("#Crear #AsteriscoImpuesto").addClass("text-danger");
            $("#Crear #timv_RangoValidacion").show();

            ModelState = false;
        } else {
            $("#Crear #AsteriscoImpuesto").removeClass("text-danger");
            $("#Crear #timv_RangoValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Impuesto = parseFloat(FormatearMonto(Impuesto));
            if (parseInt(Impuesto) < 0 || parseFloat(Impuesto) < 0.00) {
                $("#Crear #AsteriscoImpuesto").addClass("text-danger");
                $("#Crear #timv_ImpuestoValidacion").empty();
                $("#Crear #timv_ImpuestoValidacion").html("El campo Porcentaje no puede ser menor a 0.");
                $("#Crear #timv_ImpuestoValidacion").show();
                ModelState = false;
            } else {
                $("#Crear #timv_ImpuestoValidacion").empty();
                $("#Crear #timv_ImpuestoValidacion").html("El campo Porcentaje es requerido.");
                $("#Crear #AsteriscoImpuesto").removeClass("text-danger");
                $("#Crear #timv_ImpuestoValidacion").hide();
            }
        }
    }
    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}

//FUNCION PARA MOSTRAR O QUITAR DATAANNOTATIONS
function DataAnnotationsEditar(Municipio, TipoDeduccion, RangoInicio, RangoFin, Rango, Impuesto) {

    //VARIABLE DE VALIDACION DEL MODELO
    var ModelState = true;

    if (Municipio != "") {
        //Telefono
        if (Municipio == "" || Municipio == null) {
            //MOSTRAR DATAANNOTATIONS
            $("#Editar #mun_CodigoValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #AsteriscoMunicipio").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #mun_CodigoValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #AsteriscoMunicipio").removeClass("text-danger");
        }
    }

    if (TipoDeduccion != "-1") {
        //Telefono
        if (TipoDeduccion == "" || TipoDeduccion == "0" || TipoDeduccion == 0) {
            //MOSTRAR DATAANNOTATIONS
            $("#Editar #timv_TipoDeduccionValidacion").show();
            //CAMBIAR EL COLOR DEL ASTERISCO A ROJO
            $("#Editar #AsteriscoTipoDeduccion").addClass("text-danger");
            ModelState = false;
        }
        else {
            //OCULTAR DATAANNOTATIONS
            $("#Editar #timv_TipoDeduccionValidacion").hide();
            //CAMBIAR EL COLOR DEL ASTERISCO A NEGRO
            $("#Editar #AsteriscoTipoDeduccion").removeClass("text-danger");
        }
    }

    if (RangoInicio != "-1") {
        //RANGO INICIAL

        if (parseFloat(FormatearMonto(RangoInicio)) >= parseFloat(FormatearMonto($("#Editar #timv_RangoFin").val()))) {
            $("#Editar #AsteriscoRangoInicio").addClass("text-danger");
            $("#Editar #timv_RangoInicioValidacion").empty();
            $("#Editar #timv_RangoInicioValidacion").html("El campo Rango Inicio debe ser mayor que el rango fin.");
            $("#Editar #timv_RangoInicioValidacion").show();
            ModelState = false;
        }
        else {
            $("#Editar #AsteriscoRangoInicio").removeClass("text-danger");
            $("#Editar #timv_RangoInicioValidacion").empty();
            $("#Editar #timv_RangoInicioValidacion").html("El campo Rango Final es requerido.");
            $("#Editar #timv_RangoInicioValidacion").hide();
        }

        if (RangoInicio == "" || RangoInicio == null || RangoInicio == undefined) {
            $("#Editar #AsteriscoRangoInicio").addClass("text-danger");
            $("#Editar #timv_RangoInicioValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoRangoInicio").removeClass("text-danger");
            $("#Editar #timv_RangoInicioValidacion").hide();
            if (parseInt(RangoInicio) < 0 || parseFloat(RangoInicio) < 0.00) {

                $("#Editar #AsteriscoRangoInicio").addClass("text-danger");
                $("#Editar #timv_RangoInicioValidacion").empty();
                $("#Editar #timv_RangoInicioValidacion").html("El campo Rango Inicial no puede ser menor que cero.");
                $("#Editar #timv_RangoInicioValidacion").show();
                ModelState = false;
            } else {
                $("#Editar #timv_RangoInicioValidacion").empty();
                $("#Editar #timv_RangoInicioValidacion").html("El campo Rango Inicial es requerido.");
                $("#Editar #AsteriscoRangoInicio").removeClass("text-danger");
                $("#Editar #timv_RangoInicioValidacion").hide();
            }
        }
    }

    if (RangoFin != "-1") {
        //RANGO FINAL
        if (RangoFin == "" || RangoFin == null || RangoFin == undefined || parseFloat(FormatearMonto(RangoFin)) == 0) {
            $("#Editar #AsteriscoRangoFin").addClass("text-danger");
            $("#Editar #timv_RangoFinValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoRangoFinal").removeClass("text-danger");
            $("#Editar #timv_RangoFinValidacion").hide();

            if (parseFloat(FormatearMonto(RangoFin)) <= RangoInicio) {
                $("#Editar #AsteriscoRangoFin").addClass("text-danger");
                $("#Editar #timv_RangoFinValidacion").empty();
                $("#Editar #timv_RangoFinValidacion").html("El campo Rango Final debe ser mayor que el rango inicial.");
                $("#Editar #timv_RangoFinValidacion").show();
                ModelState = false;
            } else {
                $("#Editar #timv_RangoFinValidacion").empty();
                $("#Editar #timv_RangoFinValidacion").html("El campo Rango Final es requerido.");
                $("#Editar #AsteriscoRangoFin").removeClass("text-danger");
                $("#Editar #timv_RangoFinValidacion").hide();
            }

        }
    }


    if (Rango != "-1") {
        //PORCENTAJE
        if (Rango == "" || Rango == null || Rango == undefined) {
            $("#Editar #AsteriscoRango").addClass("text-danger");
            $("#Editar #timv_RangoValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoRango").removeClass("text-danger");
            $("#Editar #timv_RangoValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Rango = parseFloat(FormatearMonto(Rango));
            if (parseInt(Rango) < 0 || parseFloat(Rango) < 0.00) {
                $("#Editar #AsteriscoRango").addClass("text-danger");
                $("#Editar #timv_RangoValidacion").empty();
                $("#Editar #timv_RangoValidacion").html("El campo Rango no puede ser menor a 0.");
                $("#Editar #timv_RangoValidacion").show();
                ModelState = false;
            } else {
                $("#Editar #timv_RangoValidacion").empty();
                $("#Editar #timv_RangoValidacion").html("El campo Rango es requerido.");
                $("#Editar #AsteriscoRango").removeClass("text-danger");
                $("#Editar #timv_RangoValidacion").hide();
            }
        }
    }

    if (Impuesto != "-1") {
        //PORCENTAJE
        if (Impuesto == "" || Impuesto == null || Impuesto == undefined) {
            $("#Editar #AsteriscoImpuesto").addClass("text-danger");
            $("#Editar #timv_RangoValidacion").show();

            ModelState = false;
        } else {
            $("#Editar #AsteriscoImpuesto").removeClass("text-danger");
            $("#Editar #timv_RangoValidacion").hide();
            //CONVERTIR EN DECIMAL EL PORCENTAJE
            Impuesto = parseFloat(FormatearMonto(Impuesto));
            if (parseInt(Impuesto) < 0 || parseFloat(Impuesto) < 0.00) {
                $("#Editar #AsteriscoImpuesto").addClass("text-danger");
                $("#Editar #timv_ImpuestoValidacion").empty();
                $("#Editar #timv_ImpuestoValidacion").html("El campo Impuesto no puede ser menor a 0.");
                $("#Editar #timv_ImpuestoValidacion").show();
                ModelState = false;
            } else {
                $("#Editar #timv_ImpuestoValidacion").empty();
                $("#Editar #timv_ImpuestoValidacion").html("El campo Impuesto es requerido.");
                $("#Editar #AsteriscoImpuesto").removeClass("text-danger");
                $("#Editar #timv_ImpuestoValidacion").hide();
            }
        }
    }
    //RETURN DEL ESTADO DEL MODELO
    return ModelState;
}

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
