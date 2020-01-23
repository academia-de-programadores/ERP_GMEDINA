﻿//#region Region Obtención de Script para Formateo de Fechas
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
    });
//#endregion
var inactivarID = 0;


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

$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
})

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/DeduccionesIndividuales/GetData',
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
            var ListaDeduccionIndividual = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#IndexTable').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeduccionIndividual.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeduccionIndividual[i].dei_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = ListaDeduccionIndividual[i].dei_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleDeduccionesIndividuales" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaDeduccionIndividual[i].dei_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarDeduccionesIndividuales" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionIndividual[i].dei_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarDeduccionesIndividuales" deiid="' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '" data-id = "' + ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#IndexTable').dataTable().fnAddData([
                    ListaDeduccionIndividual[i].dei_IdDeduccionesIndividuales,
                    ListaDeduccionIndividual[i].dei_Motivo,
                    ListaDeduccionIndividual[i].per_Nombres + ' ' + ListaDeduccionIndividual[i].per_Apellidos,
                    ListaDeduccionIndividual[i].dei_MontoInicial,
                    ListaDeduccionIndividual[i].dei_MontoRestante,
                    ListaDeduccionIndividual[i].dei_Cuota,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
}

//#region  Activar
$(document).on("click", "#IndexTable tbody tr td #btnActivarDeduccionesIndividuales", function () {
    document.getElementById("btnActivarRegistroDeduccionIndividual").disabled = false;
    var id = $(this).data('id');

    var id = $(this).attr('deiid');
    localStorage.setItem('id', id);
    //Mostrar el Modal
    $("#ActivarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$("#btnActivarRegistroDeduccionIndividual").click(function () {
    document.getElementById("btnActivarRegistroDeduccionIndividual").disabled = true;
    let id = localStorage.getItem('id')

    $.ajax({
        url: "/DeduccionesIndividuales/Activar",
        method: "POST",
        data: { id: id }
    }).done(function (data) {
        $("#ActivarDeduccionesIndividuales").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });

});
//#endregion

//#region Funciones

function limpiarAsteriscosCrear() {
    $("#Crear #astMotivo").css("color", "black");
    $("#Crear #astEmpId").css("color", "black");
    $("#Crear #astMontoInicial").css("color", "black");
    $("#Crear #astMontoRestante").css("color", "black");
    $("#Crear #astCuota").css("color", "black");
}

function limpiarSpanCrear() {
    $("#Crear #valMotivo").css("display", "none");
    $("#Crear #dei_Motivo").css("display", "none");
    $("#Crear #valMontoInicial").css("display", "none");
    $("#Crear #valMontoInicial").css("display", "none");
    $("#Crear #validation4").css("display", "none");
    $("#Crear #valMontoRestante").css("display", "none");
    $("#Crear #valCuota").css("display", "none");
}

function estaTodoValidado(modal) {
    //#region Declaracion de variables
    var estaBien = true;
    let dei_Motivo = $("#" + modal + " #dei_Motivo").val();
    let dei_MontoInicial = $("#" + modal + " #dei_MontoInicial").val();
    let dei_MontoRestante = $("#" + modal + " #dei_MontoRestante").val();
    let dei_Cuota = $("#" + modal + " #dei_Cuota").val();
    //#endregion

    //#region Validar DDL Motivo 
    if (dei_Motivo == null || dei_Motivo == "") {
        $("#" + modal + " #valMotivo").css("display", "");
        $("#" + modal + " #astMotivo").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + " #valMotivo").css("display", "none");
        $("#" + modal + " #astMotivo").css("color", "black");
    }
    //#endregion

    //#region Validar DDL Empleados
    if (emp_Id == "" || emp_Id == 0 || emp_Id == "0") {
        $("#" + modal + " #valEmpId").css("display", "");
        $("#" + modal + " #astEmpId").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + " #valEmpId").css("display", "none");
        $("#" + modal + " #astEmpId").css("color", "black");
    }
    //#endregion

    //#region Validar monto Inicial
    if (dei_MontoInicial == "" || dei_MontoInicial == null || dei_MontoInicial == undefined || dei_MontoInicial <= 0) {
        $("#" + modal + " #valMontoInicial").css("display", "block");
        $("#" + modal + " #astMontoInicial").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + " #valMontoInicial").css("display", "none");
        $("#" + modal + " #astMontoInicial").css("color", "black");
    }
    //#endregion

    //#region Validar monto Restante
    if (dei_MontoRestante != 0) {
        $("#" + modal + " #valMontoRestante").css("display", "none");
        $("#" + modal + " #astMontoRestante").css("color", "black");
    }
    else {
        $("#Crear #valMontoRestante").css("display", "");
        $("#astMontoRestante").css("color", "red");
        estaBien = false;
    }
    //#endregion

    //#region Validar si monto restante es mayor que monto inicial
    if (dei_MontoRestante > dei_MontoInicial) {
        $("#" + modal + "#valMontoRestante").css("display", "");
        $("#" + modal + "#astMontoInicial").css("color", "red");
        estaBien = false;
    }
    else {
        $("#" + modal + "#valMontoRestante").css("display", "none");
        $("#" + modal + "#astMontoInicial").css("color", "black");

    }
    //#endregion

    //#region Validar cuota
    if (dei_Cuota != "" || dei_Cuota != null || dei_Cuota != undefined) {
        $("#" + modal + " #valCuota").css("display", "none");
        $("#" + modal + " #astCuota").css("color", "black");
    }
    else {
        $("#" + modal + " #valCuota").css("display", "");
        $("#" + modal + " #astCuota").css("color", "red");
        estaBien = false;
    }
    //#endregion

    return estaBien;
}

//#endregion

//#region Crear
$("#btnCerrarCrear").click(function () {
    //Ocultar validaciones span
    limpiarSpanCrear();
    //Asteriscos
    limpiarAsteriscosCrear();


    $("#emp_Id").val("0");
    $("#dei_Motivo").val('');
    $("#dei_MontoInicial").val('');
    $("#dei_MontoRestante").val('');
    $("#dei_Cuota").val('');
    $("#dei_PagaSiempre").prop('checked', false);
    $("#AgregarDeduccionesIndividuales").modal('hide');
});


//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE

//Pedir data para llenar el DDL
$(document).on("click", "#btnAgregarDeduccionIndividual", function () {
    document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = false;
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #emp_Id").empty();
            $("#Crear #emp_Id").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #emp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("#Crear #emp_Id").val("0");
    $("#dei_Motivo").val('');
    $("#dei_MontoInicial").val('');
    $("#dei_MontoRestante").val('');
    $("#dei_Cuota").val('');
    $('#dei_PagaSiempre').prop('checked', false);
    $("#Crear #MontoRestanteCrear").css("display", "none");
});

//Create POST
$('#btnCreateRegistroDeduccionIndividual').click(function () {

    //#region Declaracion de variables
    let emp_Id = $("#Crear #emp_Id").val();
    let dei_Motivo = $("#Crear #dei_Motivo").val();
    let dei_MontoInicial = $("#Crear #dei_MontoInicial").val();
    let dei_MontoRestante = $("#frmCreateDeduccionIndividual #dei_MontoRestante").val();
    let dei_Cuota = $("#Crear #dei_Cuota").val();
    let dei_PagaSiempre = $("#Crear #dei_PagaSiempre").val();
    //#endregion

    //Obtener valor del checkbox
    if ($('#Crear #dei_PagaSiempre').is(':checked')) {
        dei_PagaSiempre = true;
    }
    else {
        dei_PagaSiempre = false;
    }

    //#region  POST Create
    if (estaTodoValidado()) {
        document.getElementById("btnCreateRegistroDeduccionIndividual").disabled = true;

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionesIndividuales/Create",
            method: "POST",
            data: { dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_MontoInicial: dei_MontoInicial, dei_MontoRestante: dei_MontoRestante, dei_Cuota: dei_Cuota, dei_PagaSiempre: dei_PagaSiempre }
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {

                cargarGridDeducciones();

                $("#Crear #dei_Motivo").val('');
                $("#Crear #dei_MontoInicial").val('');
                $("#Crear #dei_MontoRestante").val('');
                $("#Crear #dei_Cuota").val('');
                $('#Crear #dei_PagaSiempre').prop('checked', false);
                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarDeduccionesIndividuales").modal('hide');

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
    //#endregion

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateDeduccionIndividual").submit(function (e) {
        return false;
    });

});

$('#frmCreateDeduccionIndividual #dei_Motivo').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#ast1').css('color', 'black');
    }
});

$('#frmCreateDeduccionIndividual #emp_Id').on('change', function () {
    if (this.value != 0) {
        $('#ast2').css('color', 'black');
        $("#Crear #validation2").css("display", "none");
    }
    else {
        $('#ast2').css('color', 'red');
        $("#Crear #validation2").css("display", "");
    }
});

$('#frmCreateDeduccionIndividual #dei_MontoInicial').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#ast3').css('color', 'black');
    }
});

$('#frmCreateDeduccionIndividual #dei_MontoRestante').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#asteriscoNo4').css('color', 'black');
    }
});

$('#frmCreateDeduccionIndividual #dei_Cuota').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#ast5').css('color', 'black');
    }
});
//#endregion

//#region Editar
//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#validatione1").css("display", "none");
    $("#validatione3").css("display", "none");
    $("#validatione4").css("display", "none");
    $("#validatione4").css("display", "none");
    $("#Editar #aste1").css("color", "black");
    $("#Editar #aste2").css("color", "black");
    $("#Editar #aste3").css("color", "black");
    $("#Editar #aste4").css("color", "black");
    $("#Editar #aste5").css("color", "black");
    $("#EditarDeduccionesIndividuales").modal('hide');
});


$(document).on("click", "#IndexTable tbody tr td #btnEditarDeduccionesIndividuales", function () {
    document.getElementById("btnEditDeduccionIndividual2").disabled = false;
    var id = $(this).data('id');
    inactivarID = id;
    $.ajax({
        url: "/DeduccionesIndividuales/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                if (data.dei_PagaSiempre) {
                    $('#Editar #dei_PagaSiempre').prop('checked', true);
                }
                else {
                    $('#Editar #dei_PagaSiempre').prop('checked', false);
                }

                $("#Editar #dei_IdDeduccionesIndividuales").val(data.dei_IdDeduccionesIndividuales);
                $("#Editar #dei_Motivo").val(data.dei_Motivo);
                $("#Editar #dei_MontoInicial").val(data.dei_MontoInicial);
                $("#Editar #dei_MontoRestante").val(data.dei_MontoRestante);
                $("#Editar #dei_Cuota").val(data.dei_Cuota);
                $("#Editar #dei_PagaSiempre").val(data.dei_PagaSiempre);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.emp_Id;

                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #emp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });

                $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
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

$("#btnEditDeduccionIndividual").click(function () {
    var TOF = true;
    var expr = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);

    var vale2 = $("#Editar #dei_Motivo").val();
    var vale3 = $("#Editar #dei_MontoInicial").val();
    var vale4 = $("#Editar #dei_MontoRestante").val();
    var vale5 = $("#Editar #dei_Cuota").val();

    if (vale2 == "" || vale2 == null || vale2 == 0 || vale2 == 0.00) {
        $("#Editar #validatione1").css("display", "");
        $("#Editar #1editaste").css("color", "red");
        TOF = false;
    }
    else {
        $("#Editar #validatione1").css("display", "none");
        $("#Editar #1editaste").css("color", "black");
    }
    //--

    if (vale3 == "" || vale3 == null || vale3 == undefined || vale3 <= 0) {
        $("#Editar #validatione3").css("display", "block");
        $("#Editar #3editaste").css("color", "red");
        TOF = false;
    }
    else {
        $("#Editar #validatione3").css("display", "none");
        $("#Editar #3editaste").css("color", "black");
        if (expr.test(vale3)) {
            $("#Editar #validatione3").css("display", "none");
            $("#Editar #3editaste").css("color", "black");
        }
        else {
            $("#Editar #validatione3").css("display", "");
            $("#Editar #3editaste").css("color", "red");
            TOF = false;
        }

    }

    if (vale4 > vale3) {
        $("#Editar #MontoRestanteEditar").css("display", "");
        $("#Editar #4editaste").css("color", "red");
        TOF = false;
    }
    else {
        $("#Editar #MontoRestanteEditar").css("display", "none");
        $("#Editar #4editaste").css("color", "black");
    }
    //--
    if (vale4 != "" || vale4 != null || vale4 != undefined) {
        $("#Editar #validatione4").css("display", "none");
        $("#Editar #4editaste").css("color", "black");
    }
    else {
        $("#Editar #validatione4").css("display", "");
        $("#Editar #4editaste").css("color", "red");
        TOF = false;
    }
    if (expr.test(vale4)) {
        $("#Editar #validatione4").css("display", "none");
        $("#Editar #4editaste").css("color", "black");
    }
    else {
        $("#Editar #validatione4").css("display", "");
        $("#Editar #4editaste").css("color", "red");
        TOF = false;
    }
    //--
    if (vale5 != "" || vale5 != null || vale5 != undefined) {
        $("#Editar #validatione5").css("display", "none");
        $("#Editar #5editaste").css("color", "black");
    }
    else {
        $("#Editar #validatione5").css("display", "");
        $("#Editar #5editaste").css("color", "red");
        TOF = false;
    }
    if (expr.test(vale5)) {
        $("#Editar #validatione5").css("display", "none");
        $("#Editar #5editaste").css("color", "black");
    }
    else {
        $("#Editar #validatione5").css("display", "");
        $("#Editar #5editaste").css("color", "red");
        TOF = false;
    }

    if (TOF) {
        $("#EditarDeduccionesIndividuales").modal('hide');
        $("#EditarDeduccionesIndividualesConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }


    $("#EditarDeduccionesIndividuales").submit(function (e) {
        return false;
    });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditDeduccionIndividual2").click(function () {

    var dei_PagaSiempre = false;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var dei_IdDeduccionesIndividuales = $("#Editar #dei_IdDeduccionesIndividuales").val();
    var emp_Id = $("#Editar #emp_Id").val();
    var dei_Motivo = $("#Editar #dei_Motivo").val();
    var dei_MontoInicial = $("#Editar #dei_MontoInicial").val();
    var dei_MontoRestante = $("#Editar #dei_MontoRestante").val();
    var dei_Cuota = $("#Editar #dei_Cuota").val();
    var dei_PagaSiempre = $("#Editar #dei_PagaSiempre").val();

    if ($('#Editar #dei_PagaSiempre').is(':checked')) {
        dei_PagaSiempre = true;
    }
    else {
        dei_PagaSiempre = false;
    }
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesIndividuales/Edit",
        method: "POST",
        data: { dei_IdDeduccionesIndividuales: dei_IdDeduccionesIndividuales, dei_Motivo: dei_Motivo, emp_Id: emp_Id, dei_MontoInicial: dei_MontoInicial, dei_MontoRestante: dei_MontoRestante, dei_Cuota: dei_Cuota, dei_PagaSiempre: dei_PagaSiempre }
    }).done(function (data) {
        if (data != "error") {
            document.getElementById("btnEditDeduccionIndividual2").disabled = true;
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
            $("#EditarDeduccionesIndividuales").modal('hide');
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
        else {
            $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditarDeduccionIndividual").submit(function (e) {
        return false;
    });

});

$('#frmEditarDeduccionIndividual #dei_Motivo').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#1editaste').css('color', 'black');
    }
});

$('#frmEditarDeduccionIndividual #emp_Id').on('change', function () {
    if (this.value != 0) {
        $('#2editaste').css('color', 'black');
        $("#Editar #validation2").css("display", "none");
    }
    else {
        $('#2editaste').css('color', 'red');
        $("#Editar #validation2").css("display", "");
    }
});

$('#frmEditarDeduccionIndividual #dei_MontoInicial').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#3editaste').css('color', 'black');
    }
});

$('#frmEditarDeduccionIndividual #dei_MontoRestante').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#4editaste').css('color', 'black');
    }
});

$('#frmEditarDeduccionIndividual #dei_Cuota').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $('#5editaste').css('color', 'black');
    }
});

$(document).on("click", "#btnRegresar", function () {
    document.getElementById("btnEditDeduccionIndividual2").disabled = false;
    $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});


$(document).on("click", "#btnReg", function () {
    $("#EditarDeduccionesIndividualesConfirmacion").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});
//#endregion

//#region Detalles
$(document).on("click", "#IndexTable tbody tr td #btnDetalleDeduccionesIndividuales", function () {
    var id = $(this).data('id');
    $.ajax({
        url: "/DeduccionesIndividuales/Details/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                if (data[0].dei_PagaSiempre) {
                    $("#Detalles #dei_PagaSiempre").html("Si");
                }
                else {
                    $("#Detalles #dei_PagaSiempre").html("No");
                }

                var FechaCrea = FechaFormato(data[0].dei_FechaCrea);
                var FechaModifica = FechaFormato(data[0].dei_FechaModifica);
                $(".field-validation-error").css('display', 'none');
                $("#Detalles #dei_IdDeduccionesIndividuales").html(data[0].dei_IdDeduccionesIndividuales);
                $("#Detalles #dei_Motivo").html(data[0].dei_Motivo);
                $("#Detalles #dei_MontoInicial").html(data[0].dei_MontoInicial);
                $("#Detalles #dei_MontoRestante").html(data[0].dei_MontoRestante);
                $("#Detalles #dei_Cuota").html(data[0].dei_Cuota);
                $("#Detalles #emp_Id").html(data[0].emp_Id);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #dei_UsuarioCrea").html(data[0].dei_UsuarioCrea);
                $("#Detalles #dei_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #dei_UsuarioModifica").html(data[0].dei_UsuarioModifica);
                $("#Detalles #dei_FechaModifica").html(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].emp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/DeduccionesIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        //$("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        //$("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            //$("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            if (iter.Id == SelectedId) {
                                $("#Detalles #emp_Id").html(iter.Descripcion);
                            }
                        });
                    });
                $("#DetallesDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
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
//#endregion

//#region Inactivar
//Inactivar//
$(document).on("click", "#btnBack", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#InactivarDeduccionesIndividuales").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnBa", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#InactivarDeduccionesIndividuales").modal('hide');
    $("#EditarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", "#btnInactivarDeduccionesIndividuales", function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = false;
    $("#EditarDeduccionesIndividuales").modal('hide');
    $("#InactivarDeduccionesIndividuales").modal({ backdrop: 'static', keyboard: false });
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccionIndividual").click(function () {
    document.getElementById("btnInactivarRegistroDeduccionIndividual").disabled = true;
    debugger;
    var data = { dei_IdDeduccionesIndividuales: inactivarID }
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesIndividuales/Inactivar",
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
            $("#InactivarDeduccionesIndividuales").modal('hide');
            $("#EditarDeduccionesIndividuales").modal('hide')
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
});
//#endregion