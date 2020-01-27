// funcion generica de AJAX
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

// Cargar grid
function cargarGridAcumuladosISR() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AcumuladosISR/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            if (data == "Error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            var ListaAcumuladosISR = data, template = '';
            //Recorrer la data y crear el template que se pondrá en el tbody

            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblAcumuladosISR').DataTable().clear();
            for (var i = 0; i < ListaAcumuladosISR.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaAcumuladosISR[i].aisr_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetalleAcumuladosISR">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaAcumuladosISR[i].aisr_Activo == true ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarAcumuladosISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaAcumuladosISR[i].aisr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnActivarAcumuladosISR">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblAcumuladosISR').dataTable().fnAddData([
                    ListaAcumuladosISR[i].aisr_Id,
                    ListaAcumuladosISR[i].aisr_Descripcion,
                    (ListaAcumuladosISR[i].aisr_Monto % 1 == 0) ? ListaAcumuladosISR[i].aisr_Monto + ".00" : ListaAcumuladosISR[i].aisr_Monto,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
    FullBody();
}


// ------ Create ------ //

// validar descripcion  create
$('#Crear #aisr_Descripcion').keyup(function () {

    var descripcion = $("#Crear #aisr_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcionAISR').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcionAISR').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcionAISR').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
    }
        // si es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcionAISR').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

});

// validar monto create
$('#Crear #aisr_Monto').keyup(function () {

    // si es menor o igual que cero
    if (parseInt($("#Crear #aisr_Monto").val()) > 0) {

        $('#AsteriscoMontoAISR').removeClass('text-danger');
        $("#Crear #validation_MontoMayorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoMontoAISR').addClass("text-danger");
        $("#Crear #validation_MontoMayorACero").css('display', '');
    }
});

// modal create 
$(document).on("click", "#btnAgregarAcumuladosISR", function () {

    // * descripcion 
    $('#AsteriscoDescripcionAISR').removeClass('text-danger');

    // mesanje descripcion requerida
    $("#Crear #validation_DescripcionRequerida").css('display', 'none');

    // mesanje descripcion no es numerico
    $("#Crear #validation_DescripcionNumerico").css('display', 'none');

    // * monto
    $('#AsteriscoMontoAISR').removeClass('text-danger');

    // mensaje monto debe ser mayo que cero
    $("#Crear #validation_MontoMayorACero").css('display', 'none');

    // vaciar cajas de texto
    $('#Crear input[type=text], input[type=number]').val('');

    // habilitar boton 
    $('#btnCreateAcumuladosISR').attr('disabled', false);

    //mostrar modal
    $("#AgregarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
});

// crear acumulados isr
$('#btnCreateAcumuladosISR').click(function () {
    $('#btnCreateAcumuladosISR').attr('disabled', true);

    var descripcion = $("#Crear #aisr_Descripcion").val();
    var aisr_Monto = $("#Crear #aisr_Monto").val();
    var ModelState = true;

    // descripcion requerida
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcionAISR').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcionAISR').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
        $("#Crear #aisr_Descripcion").focus();
        ModelState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcionAISR').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
        $("#Crear #aisr_Descripcion").focus();
        ModelState = false;
    }
    // si no es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcionAISR').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

    // si el monto es menor o igual que cero
    if (parseInt($("#Crear #aisr_Monto").val()) > 0) {

        $('#AsteriscoMontoAISR').removeClass('text-danger');
        $("#Crear #validation_MontoMayorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoMontoAISR').addClass("text-danger");
        $("#Crear #validation_MontoMayorACero").css('display', '');
        ModelState = false;
    }


    if (ModelState == true) {

        //serializar formulario
        var data = $("#frmAcumuladosISRCreate").serializeArray();
        // el indice 5 es el monto, hay que parsearlo a decimal porque se serializa como string
        var stringDecimal = data[5].value;
        data[5].value = stringDecimal.replace(/,/, '');

        $.ajax({
            url: "/AcumuladosISR/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            // validar respuesta del servidor
            if (data == "error") {
                
                // habilitar boton
                $('#btnCreateAcumuladosISR').attr('disabled', false);

                // mensaje de error
                iziToast.error({
                    title: 'Error',
                    message: 'No guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {

                $("#AgregarAcumuladosISR").modal('hide');
                cargarGridAcumuladosISR();

                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
    $('#btnCreateAcumuladosISR').attr('disabled', false);
});


// ------ Editar ------ //

// variable de edicion
var Data_Edit = "";

//edit 1
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnEditarAcumuladosISR", function () {
    
    var ID = $(this).data('id');
    InactivarID = ID;

    $.ajax({
        url: "/AcumuladosISR/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            
            if (data) {
                // llenar modal del formulario

                $("#Editar #aisr_Id").val(data.aisr_Id);
                $("#Editar #aisr_FechaCrea").val(data.aisr_FechaCrea);
                $("#Editar #aisr_UsuarioCrea").val(data.aisr_UsuarioCrea);
                $("#Editar #aisr_Descripcion").val(data.aisr_Descripcion);
                $("#Editar #aisr_Monto").val(data.aisr_Monto);

                // * descripcion 
                $('#AsteriscoDescripcionEditAISR').removeClass('text-danger');

                // mesanje descripcion requerida
                $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');

                // mesanje descripcion no es numerico
                $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');

                // * monto
                $('#AsteriscoMontoEditAISR').removeClass('text-danger');

                // mensaje monto debe ser mayo que cero
                $("#Editar #validation_EditarMontoMayorACero").css('display', 'none');
                

                $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
                $('#btnUpdateAISR2').attr('disabled', false);
            }
            else {

                // mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No cargó la información, contacte al administrador',
                });
            }
        });
});

// validar descripcion  edit
$('#Editar #aisr_Descripcion').keyup(function () {

    var descripcion = $("#Editar #aisr_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcionEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcionEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcionEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionNumerico").css('display', '');
    }
        // si es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcionEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

});

// validar monto edit
$('#Editar #aisr_Monto').keyup(function () {

    // si es menor o igual que cero
    if (parseInt($('#Editar #aisr_Monto').val()) > 0) {

        $('#AsteriscoMontoEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarMontoMayorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoMontoEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarMontoMayorACero").css('display', '');
    }
});

// edit 2
$("#btnEditarAcumulado").click(function () {

    var descripcion = $("#Editar #aisr_Descripcion").val();
    var aisr_Monto = $("#Editar #aisr_Monto").val();
    var ModelState = true;


    // descripcion requerida
    if (descripcion.trim() != '') {

        $('#AsteriscoDescripcionEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#AsteriscoDescripcionEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
        $("#Editar #aisr_Descripcion").focus();
        ModelState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#AsteriscoDescripcionEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionNumerico").css('display', '');
        $("#Editar #aisr_Descripcion").focus();
        ModelState = false;
    }
        // si no es un número
    else if (isNaN(descripcion) == true) {

        $('#AsteriscoDescripcionEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

    // si el monto es menor o igual que cero
    if (parseInt($("#Editar #aisr_Monto").val()) > 0) {

        $('#AsteriscoMontoEditAISR').removeClass('text-danger');
        $("#Editar #validation_EditarMontoMayorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoMontoEditAISR').addClass("text-danger");
        $("#Editar #validation_EditarMontoMayorACero").css('display', '');
        $("#Editar #aisr_Monto").focus();
        ModelState = false;
    }
   
    if (ModelState == true) {
        // oculat modal edición
        $("#EditarAcumuladosISR").modal('hide');

        // mostrar modal confirmacion
        $("#EditarAISRConfirmacion").modal({ backdrop: 'static', keyboard: false });

        // habilitar boton de confirmacion
        $('#btnUpdateAISR2').attr('disabled', false);
    }
});

// edit 3 ejecutar
$("#btnUpdateAISR2").click(function () {

    $('#btnUpdateAISR2').attr('disabled', true);

    var data = $("#frmEditAcumuladosISR").serializeArray();

    console.log(data[0])
    // el indice 5 es el monto, hay que parsearlo a decimal porque se serializa como string
    var stringDecimal = data[5].value;
    data[5].value = stringDecimal.replace(/,/, '');
    

    $.ajax({
        url: "/AcumuladosISR/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {

            // cerrar modales y cargar grid
            $("#EditarAcumuladosISR").modal('hide');
            $("#EditarAISRConfirmacion").modal('hide');
            cargarGridAcumuladosISR();

            // mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
        else {

            iziToast.error({
                title: 'Error',
                message: 'No se editó el registro, contacte al administrador',
            });

            // habilitar boton y volver al modal de edicion
            $('#btnUpdateAISR2').attr('disabled', false);
            $("#EditarAISRConfirmacion").modal('hide');
        }
    });
    $('#btnUpdateAISR2').attr('disabled', false);
});

// no confirmar edicion
$("#btnNoConfirmarEditAISR").click(function () {

    // ocultar modal confirmacion
    $("#EditarAISRConfirmacion").modal('hide');

    // mostrar modal edición
    $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });

});


// ------ Detalles ------ 
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnDetalleAcumuladosISR", function () {

    var ID = $(this).data('id');

    $.ajax({
        url: "/AcumuladosISR/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            // llenar formulario
            if (data) {
                var FechaCrea = FechaFormato(data[0].aisr_FechaCrea);
                var FechaModifica = FechaFormato(data[0].aisr_FechaModifica);
                $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);

                $("#Detalles #aisr_Descripcion").html(data[0].aisr_Descripcion);
                $("#Detalles #aisr_Monto").html(data[0].aisr_Monto);

                $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #aisr_FechaCrea").html(FechaCrea);

                $("#Detalles #aisr_UsuarioModifica").html(data.aisr_UsuarioModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #aisr_FechaModifica").html(FechaModifica);
                $("#DetailsAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                // error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }

            if (data == "Error") {

                // mensaje de error
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});


// ------ Inactivar ------ //

var InactivarID = 0;

// modal inactivar
$(document).on("click", "#btnInactivarAcumuladosISR", function () {
    $("#EditarAcumuladosISR").modal('hide');
    $('#btnInactivarAcumuladosISREjecutar').attr('disabled', false);
    $("#InactivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
});

// inactivar ejecutar
$("#btnInactivarAcumuladosISREjecutar").click(function () {

    // inhabilitar boton
    $('#btnInactivarAcumuladosISREjecutar').attr('disabled', true);


    var data = $("#frmInactivarAcumuladosISR").serializeArray();
    $.ajax({
        url: "/AcumuladosISR/Inactivar/" + InactivarID,
        method: "POST",
        data: data
    }).done(function (data) {

        // validar respuesta del servidor
        if (data == "error") {
            
            iziToast.error({
                title: 'Error',
                message: 'No inactivó el registro, contacte al administrador',
            });

            $('#btnInactivarAcumuladosISREjecutar').attr('disabled', false);
        }
        else {

            $("#InactivarAcumuladosISR").modal('hide');
            cargarGridAcumuladosISR();
            
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    InactivarID = 0;
});

// no inactivar
$("#btnNoInactivar").click(function () {
    $("#InactivarAcumuladosISR").modal('hide');
    $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
});


// ------ Activar ------ //

var activarID = 0;

// activar
$(document).on("click", "#btnActivarAcumuladosISR", function () {
    activarID = $(this).data('id');
    $("#ActivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
    $('#btnActivarAcumuladosISREjecutar').attr('disabled', false);
});

// activar ejecutar
$("#btnActivarAcumuladosISREjecutar").click(function () {

    $('#btnActivarAcumuladosISREjecutar').attr('disabled', true);

    $.ajax({
        url: "/AcumuladosISR/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
            $('#btnActivarAcumuladosISREjecutar').attr('disabled', false);
        }
        else {
            $("#ActivarAcumuladosISR").modal('hide');
            cargarGridAcumuladosISR();

            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});

// datatable
$(document).ready(function () {
    $(document).ready(function () {
        $('.dataTables-AcumuladosISR').DataTable({
            "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
            responsive: true,
            pageLength: 10,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'copy',
                    text: '<i class="fa fa-copy btn-xs"></i>',
                    titleAttr: 'Copiar',
                    exportOptions: {
                        columns: [0, 1],
                    },
                    className: 'btn btn-primary'

                },

                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o btn-xs"></i>',
                    titleAttr: 'Excel',
                    exportOptions: {
                        columns: [0, 1],
                    },
                    className: 'btn btn-primary',
                    title: 'Acumulados ISR'
                }

            ]
        });
    });
});

// evitar postbacks
$("#frmEditAcumuladosISR").submit(function (e) {
    e.preventDefault();
});

$("#frmAcumuladosISRCreate").submit(function (e) {
    e.preventDefault();
});

// script formatos fechas
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
    });
