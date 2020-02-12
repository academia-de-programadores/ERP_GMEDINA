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

var dataTableAcumuladosISR;

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
                    ListaAcumuladosISR[i].per_Nombres + ' ' + ListaAcumuladosISR[i].per_Apellidos,
                    ListaAcumuladosISR[i].aisr_Descripcion,
                    (ListaAcumuladosISR[i].aisr_Monto % 1 == 0) ? ListaAcumuladosISR[i].aisr_Monto + ".00" : ListaAcumuladosISR[i].aisr_Monto,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
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

// validar empleado create
$('#Crear #emp_IdCrear').change(function () {
    var empleado = $("#Crear #emp_IdCrear").val()

    // si es distinto de cero
    if (empleado != null && empleado != "") {
        $('#Crear #AsteriscoEmpleado').removeClass('text-danger');
        $("#Crear #validation_emp_Id").css('display', 'none');
    }
    else {
        $('#Crear #AsteriscoEmpleado').addClass("text-danger");
        $("#Crear #validation_emp_Id").css('display', '');
    }
});

// validar empleado create
$('#Editar #emp_IdEditar').change(function () {
    var empleado = $("#Editar #emp_IdEditar").val();

    // si es distinto de cero
    if (empleado != null && empleado != "") {
        $('#Editar #AsteriscoEmpleadoEditar').removeClass('text-danger');
        $("#Editar #validation_emp_Id").css('display', 'none');
    }
    else {
        $('#Editar #AsteriscoEmpleadoEditar').addClass("text-danger");
        $("#Editar #validation_emp_Id").css('display', '');
    }
});



// modal create 
$(document).on("click", "#btnAgregarAcumuladosISR", function () {

        // validar informacion del usuario
        var validacionPermiso = userModelState("AcumuladosISR/Create");

        if (validacionPermiso.status == true) {

            // * empleado
            $('#AsteriscoEmpleado').removeClass('text-danger');

            // mensaje empleado requerido
            $("#Crear #validation_emp_Id").css('display', 'none');

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


            //checkbox desmarcado
            $('#Crear #aisr_DeducirISR').prop('checked', false);

            // habilitar boton 
            $('#btnCreateAcumuladosISR').attr('disabled', false);

            $("#Crear #emp_IdCrear").val('').trigger('change.select2');
            //mostrar modal
            $("#AgregarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });

        }

   
});

// crear acumulados isr
$('#btnCreateAcumuladosISR').click(function () {    
    var empleadoid = $("#Crear #emp_IdCrear").val();
    var descripcion = $("#Crear #aisr_Descripcion").val();
    var aisr_Monto = $("#Crear #aisr_Monto").val();
    var empId = $("#Crear #emp_IdCrear").val();
    var ModelState = true;

    //empleado requerido
    if (empleadoid != 0 || empleadoid != "0" || empleadoid != '0') {
        $('#AsteriscoEmpleado').removeClass('text-danger');
        $("#Crear #validation_emp_Id").css('display', 'none');
    }
    else {
        $('#AsteriscoEmpleado').addClass("text-danger");
        $("#Crear #validation_emp_Id").css('display', '');
        ModelState = false;
    }

    if (empId == null || empId == "") {
        ModelState = false;
        $("#Crear #validation_emp_Id").css("display", "");
        $("#Crear #AsteriscoEmpleado").addClass('text-danger');
    } else {
        $("#Crear #validation_emp_Id").css("display", "none");
        $("#Crear #AsteriscoEmpleado").removeClass('text-danger');
    }

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

    //Obtener valor del checkbox
    if ($('#Crear #aisr_DeducirISR').is(':checked')) {
        aisr_DeducirISR = true;
    }
    else {
        aisr_DeducirISR = false;
    }


    if (ModelState == true) {
        $('#btnCreateAcumuladosISR').attr('disabled', true);
        let deducirISR = false;
        if ($('#Crear #aisr_DeducirISR').is(':checked')) {
            deducirISR = true;
        }
        else {
            deducirISR = false;
        }

        //serializar formulario
        var data = $("#frmAcumuladosISRCreate").serializeArray();

        let descripcion = $('#Crear #aisr_Descripcion').val();
        let monto = $('#Crear #aisr_Monto').val().replace(/,/g, '');
        let idEmpleado = $('#Crear #emp_IdCrear').val();
        let token = $('#Crear input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: "/AcumuladosISR/Create",
            method: "POST",
            data: {
                aisr_Descripcion: descripcion,
                aisr_Monto: monto,
                aisr_DeducirISR: deducirISR,
                emp_ID: idEmpleado,
                __RequestVerificationToken: token
            }
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
});


// ------ Editar ------ //

// variable de edicion
var Data_Edit = "";

//edit 1
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnEditarAcumuladosISR", function () {
    //validar informacion del usuario
   
        // validar informacion del usuario
        var validacionPermiso = userModelState("AcumuladosISR/Edit");

        if (validacionPermiso.status == true) {

            let itemEmpleado = localStorage.getItem('idEmpleado');

            let dataEmp = dataTableAcumuladosISR.row($(this).parents('tr')).data(); //obtener la data de la fila seleccionada

            if (itemEmpleado != null) {
                $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove();
                localStorage.removeItem('idEmpleado');
            }

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
                    console.log(data.redirect);
                    if (data.aisr_DeducirISR) {
                        $('#Editar #aisr_DeducirISREdit').prop('checked', true);
                    }
                    else {
                        $('#Editar #aisr_DeducirISREdit').prop('checked', false);
                    }
                    if (data) {
                        let idEmp = data.emp_Id;
                        let nombreEmp = dataEmp[2];

                        $('#Editar #emp_IdEditar').val(idEmp).trigger('change');

                        let valor = $('#Editar #emp_IdEditar').val();

                        if (valor == null) {
                            $("#Editar #emp_IdEditar").prepend("<option value='" + idEmp + "' selected>" + nombreEmp + "</option>").trigger('change');
                            localStorage.setItem('idEmpleado', idEmp);
                        }

                        $("#Editar #aisr_Id").val(data.aisr_Id);
                        $("#Editar #aisr_FechaCrea").val(data.aisr_FechaCrea);
                        $("#Editar #aisr_UsuarioCrea").val(data.aisr_UsuarioCrea);
                        $("#Editar #aisr_Descripcion").val(data.aisr_Descripcion);
                        $("#Editar #aisr_Monto").val(data.aisr_Monto);
                        $("#Editar #aisr_DeducirISREdit").val(data.aisr_DeducirISR);

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
                }).error(function (data) {
                if (data.status == 200) {
                    sessionStorage.clear();
                    timeOut = true;
                    window.location = '/Login/CerrarSesion';
                }
            });
        }
    
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
    var empleado = $("#Editar #emp_IdEditar").val();
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

    if (empleado != null && empleado != "") {
        $('#Editar #AsteriscoEmpleadoEditar').removeClass('text-danger');
        $("#Editar #validation_emp_Id").css('display', 'none');
    }
    else {
        ModelState = false;
        $('#Crear #AsteriscoEmpleadoEditar').addClass("text-danger");
        $("#Editar #validation_emp_Id").css('display', '');
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

    let deducirISR = false;
    if ($('#Editar #aisr_DeducirISREdit').is(':checked')) {
        deducirISR = true;
    }
    else {
        deducirISR = false;
    }

    var data = $("#frmEditAcumuladosISR").serializeArray();

    // el indice 5 es el monto, hay que parsearlo a decimal porque se serializa como string
    var stringDecimal = data[5].value;
    data[5].value = stringDecimal.replace(/,/g, '');

    let id = $('#Editar #aisr_Id').val();
    let descripcion = $('#Editar #aisr_Descripcion').val();
    let monto = $('#Editar #aisr_Monto').val().replace(/,/g, '');;
    let idEmpleado = $('#Editar #emp_IdEditar').val();

    $.ajax({
        url: "/AcumuladosISR/Edit",
        method: "POST",
        data: { aisr_Id: id, aisr_Descripcion: descripcion, aisr_Monto: monto, aisr_DeducirISR: deducirISR, emp_ID: idEmpleado }
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
    //validar informacion del usuario

        var validacionPermiso = userModelState("AcumuladosISR/Details");

        if (validacionPermiso.status == true) {

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
                        console.table(data)
                        if (data[0].aisr_DeducirISR) {
                            $("#Detalles #aisr_DeducirISRDetails").html("Si");
                        }
                        else {
                            $("#Detalles #aisr_DeducirISRDetails").html("No");
                        }

                        var FechaCrea = FechaFormato(data[0].aisr_FechaCrea);
                        var FechaModifica = FechaFormato(data[0].aisr_FechaModifica);
                        $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);

                        $("#Detalles #aisr_Descripcion").html(data[0].aisr_Descripcion);
                        $("#Detalles #aisr_Monto").html(data[0].aisr_Monto);

                        $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);
                        $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                        $("#Detalles #aisr_FechaCrea").html(FechaCrea);
                        $("#Detalles #emp_Id").html(data[0].per_Nombres + ' ' + data[0].per_Apellidos);

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
        }  
});


// ------ Inactivar ------ //

var InactivarID = 0;

// modal inactivar
$(document).on("click", "#btnInactivarAcumuladosISR", function () {
    // validar informacion del usuario
    var validacionPermiso = userModelState("AcumuladosISR/Inactivar");

    if (validacionPermiso.status == true) {
        $('#btnInactivarAcumuladosISREjecutar').attr('disabled', false);

        $("#EditarAcumuladosISR").modal('hide');
        $("#InactivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
    }
    
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
    //validar informacion del usuario
    var validacionPermiso = userModelState("AcumuladosISR/Activar");

    if (validacionPermiso.status == true) {

        activarID = $(this).data('id');
        $("#ActivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
        $('#btnActivarAcumuladosISREjecutar').attr('disabled', false);
    }


   
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
    dataTableAcumuladosISR = $('.dataTables-AcumuladosISR').DataTable({
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

    //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
    $.ajax({
        url: "/AcumuladosISR/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        .done(function (data) {

            $('#Crear #emp_IdCrear').select2({
                dropdownParent: $('#Crear'),
                placeholder: 'Seleccione un empleado',
                allowClear: true,
                language: {
                    noResults: function () {
                        return 'Resultados no encontrados.';
                    },
                    searching: function () {
                        return 'Buscando...';
                    }
                },
                data: data.results
            });

            $('#Editar #emp_IdEditar').select2({
                dropdownParent: $('#Editar'),
                placeholder: 'Seleccione un empleado',
                allowClear: true,
                language: {
                    noResults: function () {
                        return 'Resultados no encontrados.';
                    },
                    searching: function () {
                        return 'Buscando...';
                    }
                },
                data: data.results
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

    })
    .fail(function (jqxhr, settings, exception) {

    });



