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
    _ajax(null,
        '/ISR/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            var ListaISR = data, template = '';
            //limpiar datatable
            $('#tblISR').DataTable().clear();
            //recorrer data obtenida del backend
            for (var i = 0; i < ListaISR.length; i++) {                

                //variable para verificar el estado del registro
                var estadoRegistro = ListaISR[i].isr_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaISR[i].isr_Activo == true ? '<button data-id = "' + ListaISR[i].isr_Activo + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleISR">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaISR[i].isr_Activo == true ? '<button data-id = "' + ListaISR[i].isr_Activo + '" type="button" class="btn btn-default btn-xs"  id="btnModalEditarISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaISR[i].isr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaISR[i].isr_Activo + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarISR">Activar</button>' : '' : '';

                //agregar el row al datatable
                $('#tblAcumuladosISR').dataTable().fnAddData([
                    ListaISR[i].isr_RangoInicial,
                    ListaISR[i].isr_RangoFinal,
                    ListaISR[i].isr_Porcentaje,
                    ListaISR[i].tde_Descripcion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
    FullBody();
}

//crear isr
$(document).on("click", "#btnAgregarISR", function () {
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
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
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
    debugger;
    if (rangoFinal <= rangoInicial) {
        ModelState = false;
        $("#Crear #Validation_RangoFinalOtro").css('display', '');
        $('#Crear #AsteriscoRangoFinalISR').addClass('text-danger');
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

    if (ModelState == true) {
        var data = $("#frmISRCreate").serializeArray();
        $.ajax({
            url: "/ISR/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            $("#AgregarISR").modal('hide');
            //validar respuesta del backend
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                cargarGridISR();
                console.log(data);
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }

});


// validaciones AsteriscoRangoInicialISR
$('#frmISRCreate #isr_RangoInicial').keyup(function () {
    if ($("#frmISRCreate #isr_RangoInicial").val().trim() != '') {
        $('#AsteriscoRangoInicialISR').removeClass('text-danger');
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
});







//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblISR tbody tr td #btnModalEditarISR", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
    $.ajax({
        url: "/ISR/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #isr_Id").val(data.isr_Id);
                $("#Editar #isr_RangoInicial").val(data.isr_RangoInicial);
                $("#Editar #isr_RangoFinal").val(data.isr_RangoFinal);
                $("#Editar #isr_Porcentaje").val(data.isr_Porcentaje);
                $("#Editar #tde_IdTipoDedu").val(data.tde_IdTipoDedu);
                $(".field-validation-error").css('display', 'none');
                $("#EditarISR").modal();
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
                    message: 'No cargó la información, contacte al administrador',
                });
            }
        });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarISR").click(function () {

    var ModelState = true;
    $("#Editar #isr_Id").val() == "" ? ModelState = false : $("#Editar #isr_Id").val() == "0" ? ModelState = false : $("#Editar #isr_Id").val() == null ? ModelState = false : '';
    $("#Editar #isr_RangoInicial").val() == "" ? ModelState = false : $("#Editar #isr_RangoInicial").val() == "0.00" ? ModelState = false : $("#Editar #isr_RangoInicial").val() == null ? ModelState = false : '';
    $("#Editar #isr_RangoFinal").val() == "" ? ModelState = false : $("#Editar #isr_RangoFinal").val() == "0.00" ? ModelState = false : $("#Editar #isr_RangoFinal").val() == null ? ModelState = false : '';
    $("#Editar #isr_Porcentaje").val() == "" ? ModelState = false : $("#Editar #isr_Porcentaje").val() == "0" ? ModelState = false : $("#Editar #isr_Porcentaje").val() == null ? ModelState = false : '';
    $("#Editar #tde_IdTipoDedu").val() == "" ? ModelState = false : $("#Editar #tde_IdTipoDedu").val() == "0" ? ModelState = false : $("#Editar #tde_IdTipoDedu").val() == null ? ModelState = false : '';

    if (ModelState) {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditISR").serializeArray();
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/ISR/Edit",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data == "error") {
                //Cuando traiga un error del backend al guardar la edicion
                iziToast.error({
                    title: 'Error',
                    message: 'No editó el registro, contacte al administrador',
                });
            }
            else {
                cargarGridISR();
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarISR").modal('hide');
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
    $("#EditarISR").modal('hide');
});

$(document).on("click", "#btnModalInactivarISR", function () {
    $("#EditarISR").modal('hide');
    $("#InactivarISR").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnBack", function () {
    $("#InactivarISR").modal('hide');
    $("#EditarISR").modal({ backdrop: 'static', keyboard: false });
});


//Inactivar registro Techos Deducciones    
$("#btnInactivarISR").click(function () {
    var data = $("#frmInactivarISR").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/ISR/Inactivar/" + InactivarID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridISR();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivaISR").modal('hide');
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
                $("#Detalles #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
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