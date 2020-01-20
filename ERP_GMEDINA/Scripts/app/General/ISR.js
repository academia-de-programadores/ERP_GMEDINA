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
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
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
                var botonDetalles = ListaISR[i].isr_Activo == true ? '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleISR">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaISR[i].isr_Activo == true ? '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnModalEditarISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaISR[i].isr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaISR[i].isr_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarISR">Activar</button>' : '' : '';

                //agregar el row al datatable
                $('#tblISR').dataTable().fnAddData([
                    ListaISR[i].isr_RangoInicial,
                    ListaISR[i].isr_RangoFinal,
                    ListaISR[i].isr_Porcentaje,
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
    document.getElementById("btnCreateISR").disabled = false;
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

    //mostrar modal
    $('#Crear input[type=text], input[type=number]').val('');
    $('#frmISRCreate #Validation_tde_IdTipoDedu').css('display', 'none');
    $('#frmISRCreate .messageValidation').css('display', 'none');    
    $('#frmISRCreate .asterisco').removeClass('text-danger');
    $("#AgregarISR").modal({ backdrop: 'static', keyboard: false });
    //$("html, body").css("overflow", "hidden");
    //$("html, body").css("overflow", "scroll");
});

//crear nuevo rango isr
$('#btnCreateISR').click(function () {
    var ModelState = true;
    var rangoInicial = $("#Crear #isr_RangoInicial").val().trim();
    var rangoFinal = $("#Crear #isr_RangoFinal").val().trim();
    var tipoDeduccion = $("#Crear #tde_IdTipoDedu").val().trim();
    var porcentaje = $("#Crear #isr_Porcentaje").val().trim();

    if (rangoInicial == null || rangoInicial == 0 || rangoInicial == '' || rangoInicial == undefined) {
        ModelState = false;
        $("#Crear #Validation_RangoInicial").css('display', '');
        $('#Crear #AsteriscoRangoInicialISR').addClass('text-danger');
    }
    if (rangoFinal == null || rangoFinal == 0 || rangoFinal == '' || rangoFinal == undefined) {
        ModelState = false;
        $("#Crear #Validation_RangoFinal").css('display', '');
        $('#Crear #AsteriscoRangoFinalISR').addClass('text-danger');
    }
    if (parseInt(rangoFinal) <= parseInt(rangoInicial) && rangoFinal != '') {
        ModelState = false;
        $("#validar_rangoMontos").css('display', '');
        $('#Crear #AsteriscoRangoFinalISR').addClass('text-danger');
        $("#Crear #isr_RangoFinal").focus();
    }

    if (tipoDeduccion == null || tipoDeduccion == 0 || tipoDeduccion == '' || tipoDeduccion == undefined) {
        ModelState = false;
        $("#Crear #Validation_tde_IdTipoDedu").css('display', '');
        $('#Crear #Asteriscotde_IdTipoDeduISR').addClass('text-danger');
    }

    if (porcentaje == null || porcentaje == 0 || porcentaje == '' || porcentaje == undefined) {
        ModelState = false;
        $("#Crear #Validation_Porcentaje").css('display', '');
        $('#Crear #Asteriscoisr_PorcentajeISR').addClass('text-danger');
    }

    if (parseInt(porcentaje) > 100) {
        ModelState = false;
        $("#Crear #porcentajeVali").css('display', '');
        $('#Crear #Asteriscoisr_PorcentajeISR').addClass("text-danger");
    }

    if (ModelState == true) {
        $('#btnCreateISR').attr('disabled',true);
        var data = $("#frmISRCreate").serializeArray();
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
                document.getElementById("btnCreateISR").disabled = true;
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


// validaciones AsteriscoRangoInicialISR
$('#frmISRCreate #isr_RangoInicial').keyup(function () {
    if ($("#frmISRCreate #isr_RangoInicial").val().trim() != '') {
        $('#frmISRCreate AsteriscoRangoInicialISR').removeClass('text-danger');
        $("#Crear #Validation_RangoInicial").css('display', 'none');
    }
    else {
        $('#frmISRCreate #AsteriscoRangoInicialISR').addClass("text-danger");
        $("#Crear #Validation_RangoInicial").css('display', '');
    }
});

// validaciones AsteriscoRangoFinalISR
$('#frmISRCreate #isr_RangoFinal').keyup(function () {
    if ($("#frmISRCreate #isr_RangoFinal").val().trim() != '') {
        $('#frmISRCreate #AsteriscoRangoFinalISR').removeClass('text-danger');
        $("#Crear #Validation_RangoFinal").css('display', 'none');
    }
    else {
        $('#frmISRCreate #AsteriscoRangoFinalISR').addClass("text-danger");
        $("#Crear #Validation_RangoFinal").css('display', '');
    }
    if (parseInt($("#Crear #isr_RangoFinal").val()) <= parseInt($("#Crear #isr_RangoInicial").val()) && $("#Crear #isr_RangoFinal").val() != '') {
        $("#validar_rangoMontos").css('display', '');
        $('#Crear #AsteriscoRangoFinalISR').addClass('text-danger');
        $("#Crear #isr_RangoFinal").focus();
    }
    else {
        $("#validar_rangoMontos").css('display', 'none');
        $('#Crear #AsteriscoRangoFinalISR').removeClass('text-danger');
    }
});

// validaciones Asteriscotde_IdTipoDeduISR
$('#frmISRCreate #tde_IdTipoDedu').on('change', function () {
    if (this.value != '0') {
        $('#frmISRCreate #Asteriscotde_IdTipoDeduISR').removeClass('text-danger');
        $('#frmISRCreate #Validation_tde_IdTipoDedu').css('display', 'none');
    }
    else {
        $('#frmISRCreate #Asteriscotde_IdTipoDeduISR').addClass("text-danger");
        $('#frmISRCreate #Validation_tde_IdTipoDedu').css('display','');
    }
});

// validaciones Asteriscoisr_PorcentajeISR
$('#frmISRCreate #isr_Porcentaje').keyup(function () {
    if ($("#frmISRCreate #isr_Porcentaje").val().trim() != '') {
        $('#frmISRCreate #Asteriscoisr_PorcentajeISR').removeClass('text-danger');
        $("#Crear #Validation_Porcentaje").css('display', 'none');
    }
    else {
        $('#frmISRCreate #Asteriscoisr_PorcentajeISR').addClass("text-danger");
        $("#Crear #Validation_Porcentaje").css('display', '');
    }

    if (parseInt($("#frmISRCreate #isr_Porcentaje").val()) > 100) {
        $("#Crear #porcentajeVali").css('display', '');
        $('#frmISRCreate #Asteriscoisr_PorcentajeISR').addClass("text-danger");
    }
    else if (parseInt($("#frmEditISR #isr_Porcentaje").val()) < 100) {
        $("#Crear #porcentajeVali").css('display', 'none');
    }

});







//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblISR tbody tr td #btnModalEditarISR", function () {
    document.getElementById("btnEditarISR").disabled = false;
    var ID = $(this).data('id');
    $('#frmEditISR #Validation_tde_IdTipoDedu').css('display', 'none');
    $('#frmEditISR .messageValidation').css('display', 'none');
    $('#frmEditISR .asterisco').removeClass('text-danger');
    InactivarID = ID;

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
                $("#Editar #isr_RangoInicial").val(data.isr_RangoInicial);
                $("#Editar #isr_RangoFinal").val(data.isr_RangoFinal);
                $("#Editar #isr_Porcentaje").val(data.isr_Porcentaje);
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
                        $("#Editar #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
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
});

$("#btnEditarISR").click(function () {
    var rangoInicial = $("#Editar #isr_RangoInicial").val().trim();
    var rangoFinal = $("#Editar #isr_RangoFinal").val().trim();
    var tipoDeduccion = $("#Editar #tde_IdTipoDedu").val().trim();
    var porcentaje = $("#Editar #isr_Porcentaje").val().trim();
    var ModelState = true;

    if (rangoInicial == null || rangoInicial == 0 || rangoInicial == '' || rangoInicial == undefined) {
        ModelState = false;
        $("#Editar #Validation_RangoInicialEdit").css('display', '');
        $('#Editar #AsteriscoRangoInicialISREdit').addClass('text-danger');
    }
    if (rangoFinal == null || rangoFinal == 0 || rangoFinal == '' || rangoFinal == undefined) {
        ModelState = false;
        $("#Editar #Validation_RangoFinalEdit").css('display', '');
        $('#Editar #AsteriscoRangoFinalISREdit').addClass('text-danger');
    }
    if (parseInt(rangoFinal) <= parseInt(rangoInicial) && rangoFinal != '') {
        ModelState = false;
        $("#validar_rangoMontosEdit").css('display', '');
        $('#Editar #AsteriscoRangoFinalISREdit').addClass('text-danger');
        $("#Editar #isr_RangoFinal").focus();
    }

    if (tipoDeduccion == null || tipoDeduccion == 0 || tipoDeduccion == '' || tipoDeduccion == undefined) {
        ModelState = false;
        $("#Editar #Validation_tde_IdTipoDeduEdit").css('display', '');
        $('#Editar #Asteriscotde_IdTipoDeduISREdit').addClass('text-danger');
    }

    if (porcentaje == null || porcentaje == 0 || porcentaje == '' || porcentaje == undefined) {
        ModelState = false;
        $("#Editar #Validation_PorcentajeEdit").css('display', '');
        $('#Editar #Asteriscoisr_PorcentajeISREdit').addClass('text-danger');
    }

    if (parseInt($("#frmEditISR #isr_Porcentaje").val()) > 100) {
        ModelState = false;
        $("#Editar #vPorcentaje").css('display', '');
        $('#PorcentajeAsterisco').addClass("text-danger");
    }


    if (ModelState == true) {
        $("#EditarISR").modal('hide');
        $("#EditarISRConfirmacion").modal({ backdrop: 'static', keyboard: false });
        document.getElementById("btnEditISR2").disabled = false;
    }

});

$(document).on("click", "#btnRegresar", function () {
    $("#EditarISRConfirmacion").modal('hide');
    document.getElementById("btnEditISR2").disabled = false;
    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditISR2").click(function () {
        $('#btnEditarISR').attr('disabled', true);
        var data = $("#frmEditISR").serializeArray();
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/ISR/Edit",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }
            else {
                document.getElementById("btnEditISR2").disabled = true;
                cargarGridISR();
                $("#EditarISRConfirmacion").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
        });
        $('#btnEditarISR').attr('disabled', false);
});


// validaciones AsteriscoRangoInicialISR
$('#frmEditISR #isr_RangoInicial').keyup(function () {
    if ($("#EditarISR #isr_RangoInicial").val().trim() != '') {

        $('#EditarISR #RangoInicialIAsterisco').removeClass('text-danger');
        $("#VRangoInicial").css('display', 'none');
    }
    else {
        $('#EditarISR #RangoInicialIAsterisco').addClass("text-danger");
        $("#VRangoInicial").css('display', '');
    }
});

// validaciones AsteriscoRangoFinalISR
$('#frmEditISR #isr_RangoFinal').keyup(function () {
    
    if ($("#EditarISR #isr_RangoFinal").val().trim() != '') {

        $("#EditarISR #RangoFinalAsterisco").removeClass('text-danger');
        $("#frmEditISR #Validation_RangoFinalEdit").css('display', 'none');
    }
    else {

        $("#EditarISR #RangoFinalAsterisco").addClass("text-danger");
        $("#Validation_RangoFinalEdit").css('display', '');
    }

    if (parseInt($("#frmEditISR #isr_RangoFinal").val()) <= parseInt($("#frmEditISR #isr_RangoInicial").val()) && $("#frmEditISR #isr_RangoFinal").val() != '') {

        $("#validar_rangoMontosEdit").css('display', '');
        $('#EditarISR #AsteriscoRangoFinalISREdit').addClass('text-danger');
        $("#EditarISR #isr_RangoFinal").focus();
    }
    else {

        $("#validar_rangoMontosEdit").css('display', 'none');
        $('#EditarISR #AsteriscoRangoFinalISREdit').removeClass('text-danger');
    }
});

// validaciones Asteriscotde_IdTipoDeduISR
$('#frmEditISR #tde_IdTipoDedu').on('change', function () {
    if (this.value != '0') {
        $('#frmEditISR #Asteriscotde_IdTipoDeduISREdit').removeClass('text-danger');
        $('#frmEditISR #Validation_tde_IdTipoDeduEdit').css('display', 'none');
    }
    else {
        $('#frmEditISR #Asteriscotde_IdTipoDeduISREdit').addClass("text-danger");
        $('#EditarISR #Validation_tde_IdTipoDeduEdit').css('display', '');
    }
});

// validaciones Asteriscoisr_PorcentajeISR
$('#frmEditISR #isr_Porcentaje').keyup(function () {
    if ($("#frmEditISR #isr_Porcentaje").val().trim() != '') {
        $('#PorcentajeAsterisco').removeClass('text-danger');
        $("#Editar #Validation_PorcentajeEdit").css('display', 'none');
    }
    else {
        $("#Validation_PorcentajeEdit").css('display', '');
        $('#PorcentajeAsterisco').addClass("text-danger");
        console.log($('#PorcentajeAsterisco'));
    }

    if (parseInt($("#frmEditISR #isr_Porcentaje").val()) > 100) {
        $("#Editar #vPorcentaje").css('display', '');
        $('#PorcentajeAsterisco').addClass("text-danger");
    }
    else if (parseInt($("#frmEditISR #isr_Porcentaje").val()) < 100) {
        $("#Editar #vPorcentaje").css('display', 'none');
    }
});





//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarISR").modal('hide');
    document.getElementById("btnEditISR2").disabled = false;
});

$(document).on("click", "#btnModalInactivarISR", function () {
    $("#EditarISR").modal('hide');
    document.getElementById("btnInactivarISR").disabled = false;
    $("#InactivarISR").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnBack", function () {
    $("#InactivarISR").modal('hide');
    document.getElementById("btnEditISR2").disabled = false;
    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
});



//Inactivar registro Techos Deducciones    
$("#btnInactivarISR").click(function () {
    var data = $("#frmInactivarISR").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/ISR/Inactivar/" + InactivarID,
        method: "POST",
        data: { id: InactivarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            document.getElementById("btnInactivarISR").disabled = true;
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


//DETALLES
$(document).on("click", "#tblISR tbody tr td #btnDetalleISR", function () {
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
                $("#Detalles #isr_RangoInicial").html(data[0].isr_RangoInicial);
                $("#Detalles #isr_RangoFinal").html(data[0].isr_RangoFinal);
                $("#Detalles #isr_Porcentaje").html(data[0].isr_Porcentaje);
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
});