//
//Obtención de Script para Formateo de Fechas
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
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AFP/GetData',
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
            var ListaAFP = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblAFP').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAFP.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaAFP[i].afp_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaAFP[i].afp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleAFP" data-id = "' + ListaAFP[i].afp_Id + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAFP[i].afp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarAFP" data-id = "' + ListaAFP[i].afp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAFP[i].afp_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarAFP" afpid="' + ListaAFP[i].afp_Id + '" data-id = "' + ListaAFP[i].afp_Id + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblAFP').dataTable().fnAddData([
                    ListaAFP[i].afp_Id,
                    ListaAFP[i].afp_Descripcion,
                    ListaAFP[i].afp_AporteMinimoLps,
                    ListaAFP[i].afp_InteresAporte,
                    ListaAFP[i].afp_InteresAnual,
                    ListaAFP[i].tde_Descripcion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
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

//Activar
$(document).on("click", "#tblAFP tbody tr td #btnActivarAFP", function () {
    document.getElementById("btnActivarRegistroAFP").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('afpid');
    localStorage.setItem('id', ID);
   //Mostrar el Modal
    $("#ActivarAFP").modal();
});

$("#btnActivarRegistroAFP").click(function () {
    document.getElementById("btnActivarRegistroAFP").disabled = true;
    let ID = localStorage.getItem('id')

    $.ajax({
        url: "/AFP/Activar",
        method: "POST",
        data: { id: ID }
    }).done(function (data) {
        $("#ActivarAFP").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else{
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });

});

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$("#btnCerrarCrear").click(function () {
    document.getElementById("btnCreateRegistroAFP").disabled = false;
    $("#afp_Descripcion").val('');
    $("#afp_AporteMinimoLps").val('');
    $("#afp_InteresAporte").val('');
    $("#afp_InteresAnual").val('');
    $("#Crear #tde_IdTipoDedu").val("0");
    $("#validation1").css("display", "none");
    $("#validation2").css("display", "none");
    $("#validation3").css("display", "none");
    $("#validation4").css("display", "none");
    $("#validation5").css("display", "none");
    $("#Crear #ast1").css("color", "black");
    $("#Crear #ast2").css("color", "black");
    $("#Crear #ast3").css("color", "black");
    $("#Crear #ast4").css("color", "black");
    $("#Crear #ast5").css("color", "black");
    $("#AgregarAFP").modal('hide');
});

//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
const btnGuardar = $('#btnCreateRegistroAFP')

//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

$(document).on("click", "#btnAgregarAFP", function () {
    document.getElementById("btnCreateRegistroAFP").disabled = false;
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/AFP/EditGetTipoDeduccionDDL",
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
    $("#AgregarAFP").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

    $("#afp_Descripcion").val('');
    $("#afp_AporteMinimoLps").val('');
    $("#afp_InteresAporte").val('');
    $("#afp_InteresAnual").val('');
    $("#Crear #tde_IdTipoDedu").val("0");

});



//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAFP').click(function () {
    debugger;
    //Para que no entre directamente a hacer la peticion o la acción al servidor
    var TOF = true;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);
    var val1 = $("#Crear #afp_Descripcion").val();
    var val2 = $("#Crear #afp_AporteMinimoLps").val();
    var val3 = $("#Crear #afp_InteresAporte").val();
    var val4 = $("#Crear #afp_InteresAnual").val();
    var val5 = $("#Crear #tde_IdTipoDedu").val();

    if (val1 == "" || val1 == null) {
        $("#Crear #validation1").css("display", "");
        $("#Crear #ast1").css("color", "red");
        TOF = false;
    }
    else{
        $("#Crear #validation1").css("display", "none");
        $("#Crear #ast1").css("color", "black");
    }
    //--
    if (val2 != "" || val2 != null || val2 != undefined) {
        $("#Crear #validation2").css("display", "none");
        $("#Crear #ast2").css("color", "black");
    }
    else {
        $("#Crear #validation2").css("display", "");
        $("#Crear #ast2").css("color", "red");
        TOF = false;
    }
    if (expreg.test(val2)) {
        $("#Crear #validation2").css("display", "none");
        $("#Crear #ast2").css("color", "black");
    }
    else {
        $("#Crear #validation2").css("display", "");
        $("#Crear #ast2").css("color", "red");
        TOF = false;
    }
    //--
    if (val3 != "" || val3 != null || val3 != undefined) {
        $("#Crear #validation3").css("display", "none");
        $("#Crear #ast3").css("color", "black");
    }
    else {
        $("#Crear #validation3").css("display", "");
        $("#Crear #ast3").css("color", "red");
        TOF = false;
    }
    if (expreg.test(val3)) {
        $("#Crear #validation3").css("display", "none");
        $("#Crear #ast3").css("color", "black");
    }
    else {
        $("#Crear #validation3").css("display", "");
        $("#Crear #ast3").css("color", "red");
        TOF = false;
    }
    //--
    if (val4 != "" || val4 != null || val4 != undefined) {
        $("#Crear #validation4").css("display", "none");
        $("#Crear #ast4").css("color", "black");
    }
    else {
        $("#Crear #validation4").css("display", "");
        $("#Crear #ast4").css("color", "red");
        TOF = false;
    }
    if (expreg.test(val4)) {
        $("#Crear #validation4").css("display", "none");
        $("#Crear #ast4").css("color", "black");
    }
    else {
        $("#Crear #validation4").css("display", "");
        $("#Crear #ast4").css("color", "red");
        TOF = false;
    }
    //--
    if (val5 == "" || val5 == 0 || val5 == "0" || val5 == null) {
        $("#Crear #validation5").css("display", "");
        $("#Crear #ast5").css("color", "red");
        TOF = false;
    }
    else {
        $("#Crear #validation5").css("display", "none");
        $("#Crear #ast5").css("color", "black");
    }

    if (TOF) {

        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmCreateAFP").serializeArray();

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/AFP/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                document.getElementById("btnCreateRegistroAFP").disabled = true;
                cargarGridDeducciones();

                $("#Crear #afp_Descripcion").val('');
                $("#Crear #afp_AporteMinimoLps").val('');
                $("#Crear #afp_InteresAporte").val('');
                $("#Crear #afp_InteresAnual").val('');
                $("#Crear #tde_IdTipoDedu").val("0");

                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarAFP").modal('hide');

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

    
    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateAFP").submit(function (e) {
        return false;
    });

});



//Editar//
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#validationes1").css("display", "none");
    $("#validationes2").css("display", "none");
    $("#validationes3").css("display", "none");
    $("#validationes4").css("display", "none");
    $("#Editar #aste1").css("color", "black");
    $("#Editar #aste2").css("color", "black");
    $("#Editar #aste3").css("color", "black");
    $("#Editar #aste4").css("color", "black");
    $("#EditarAFP").modal('hide');
});

const btnEditar = $('#btnEditAFP')

//Div que aparecera cuando se le de click en crear
cargandoEditar = $('#cargandoEditar')

function ocultarCargandoEditar() {
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

function mostrarCargandoEditar() {
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

$(document).on("click", "#tblAFP tbody tr td #btnEditarAFP", function () {
    document.getElementById("btnEditAFPConfirmar").disabled = false;
    var ID = $(this).data('id');
    $.ajax({
        url: "/AFP/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #afp_Id").val(data.afp_Id);
                $("#Editar #afp_Descripcion").val(data.afp_Descripcion);
                $("#Editar #afp_AporteMinimoLps").val(data.afp_AporteMinimoLps);
                $("#Editar #afp_InteresAporte").val(data.afp_InteresAporte);
                $("#Editar #afp_InteresAnual").val(data.afp_InteresAnual);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;

                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/AFP/EditGetTipoDeduccionDDL",
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
                $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
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

$("#btnEditAFP").click(function () {
    var TOF = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);

    var vale1 = $("#Editar #afp_Descripcion").val();
    var vale2 = $("#Editar #afp_AporteMinimoLps").val();
    var vale3 = $("#Editar #afp_InteresAporte").val();
    var vale4 = $("#Editar #afp_InteresAnual").val();
    debugger;
    if (vale1 == "" || vale1 == null) {
        $("#validationes1").css("display", "");
        $("#Editar #aste1").css("color", "red");
        TOF = false;
    }
    else{
        $("#validationes1").css("display", "none");
        $("#Editar #aste1").css("color", "black");
    }
    //--
    if (vale2 != "" || vale2 != null || vale2 != undefined) {
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2").css("display", "");
        $("#Editar #aste2").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale2)) {
        $("#validationes2").css("display", "none");
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2").css("display", "");
        $("#Editar #aste2").css("color", "red");
        TOF = false;
    }
    //--
    if (vale3 != "" || vale3 != null || vale3 != undefined) {
        $("#Editar #aste3").css("color", "black");
    }
    else {
        $("#validationes3").css("display", "");
        $("#Editar #aste3").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale3)) {
        $("#validationes3").css("display", "");
        $("#Editar #aste3").css("color", "black");
    }
    else {
        $("#validationes3").css("display", "");
        $("#Editar #aste3").css("color", "red");
        TOF = false;
    }
    //--
    if (vale4 != "" || vale4 != null || vale4 != undefined) {
        $("#Editar #aste4").css("color", "black");
    }
    else {
        $("#validationes4").css("display", "");
        $("#Editar #aste4").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale4)) {
        $("#validationes4").css("display", "none");
        $("#Editar #aste4").css("color", "black");
    }
    else {
        $("#validationes4").css("display", "");
        $("#Editar #aste4").css("color", "red");
        TOF = false;
    }
    //--
    if (TOF) {
        $("#EditarAFP").modal('hide');
        $("#EditarAFPConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("html, body").css("overflow", "hidden");
        $("html, body").css("overflow", "scroll");
    }

    $("#EditarAFP").submit(function (e) {
        return false;
    });
});

$(document).on("click", "#btnRegresar", function () {
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#EditarAFPConfirmacion").modal('hide');
    document.getElementById("btnEditAFPConfirmar").disabled = false;
});


//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditAFPConfirmar").click(function () {

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEditarAFP").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AFP/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {
            document.getElementById("btnEditAFPConfirmar").disabled = true;
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarAFPConfirmacion").modal('hide');
            $("#EditarAFP").modal('hide');
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();

            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
        else {
            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
        }
        
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditarAFP").submit(function (e) {
        return false;
    });

});




//Detalles//
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblAFP tbody tr td #btnDetalleAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/AFP/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].afp_FechaCrea);
                var FechaModifica = FechaFormato(data[0].afp_FechaModifica);
                $(".field-validation-error").css('display', 'none');
                $("#Detalles #afp_Id").html(data[0].afp_Id);
                $("#Detalles #afp_Descripcion").html(data[0].afp_Descripcion);
                $("#Detalles #afp_AporteMinimoLps").html(data[0].afp_AporteMinimoLps);
                $("#Detalles #afp_InteresAporte").html(data[0].afp_InteresAporte);
                $("#Detalles #afp_InteresAnual").html(data[0].afp_InteresAnual);
                $("#Detalles #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #afp_UsuarioCrea").html(data[0].afp_UsuarioCrea);
                $("#Detalles #afp_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #afp_UsuarioModifica").html(data[0].afp_UsuarioModifica);
                $("#Detalles #afp_FechaModifica").html(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/AFP/EditGetTipoDeduccionDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        //$("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        //$("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            //$("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            if (iter.Id == SelectedId) {
                                $("#Detalles #tde_IdTipoDedu").html(iter.Descripcion);
                            }
                        });
                    });
                $("#DetallesAFP").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
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
///////////////////////////////////////////////////////////////////////////////////////////////////



//Inactivar//
$(document).on("click", "#btnBack", function () {
    document.getElementById("btnInactivarRegistroAFP").disabled = false;
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#InactivarAFP").modal('hide');
});

$(document).on("click", "#btnBa", function () {
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#InactivarAFP").modal('hide');
});

$(document).on("click", "#btnInactivarAFP", function () {
    document.getElementById("btnInactivarRegistroAFP").disabled = false;
    $("#EditarAFP").modal('hide');    
    $("#InactivarAFP").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

const btnInhabilitar = $('#btnInactivarRegistroAFP')

//Div que aparecera cuando se le de click en crear
cargandoInhabilitar = $('#cargandoInhabilitar')

function ocultarCargandoInhabilitar() {
    btnInhabilitar.show();
    cargandoInhabilitar.html('');
    cargandoInhabilitar.hide();
}

function mostrarCargandoInhabilitar() {
    btnInhabilitar.hide();
    cargandoInhabilitar.html(spinner());
    cargandoInhabilitar.show();
}


//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroAFP").click(function () {
    document.getElementById("btnInactivarRegistroAFP").disabled = true;
    var data = $("#frmInactivarAFP").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AFP/Inactivar",
        method: "POST",
        data: data
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
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarAFP").modal('hide');
            $("#EditarAFP").modal('hide');

            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmInactivarAFP").submit(function (e) {
        return false;
    });

});