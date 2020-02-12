// cargar grid, refrescar datatable
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AFP/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                // validar errores
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            var ListaAFP = data;

            //limpiar datatable
            $('#tblAFP').DataTable().clear();

            for (var i = 0; i < ListaAFP.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaAFP[i].afp_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleAFP" data-id = "' + ListaAFP[i].afp_Id + '">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaAFP[i].afp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarAFP" data-id = "' + ListaAFP[i].afp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAFP[i].afp_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnActivarAFP" afpid="' + ListaAFP[i].afp_Id + '" data-id = "' + ListaAFP[i].afp_Id + '">Activar</button>' : '' : '';

                //agregar row al datatble
                $('#tblAFP').dataTable().fnAddData([
                    ListaAFP[i].afp_Id,
                    ListaAFP[i].afp_Descripcion,
                    (ListaAFP[i].afp_AporteMinimoLps % 1 == 0) ? ListaAFP[i].afp_AporteMinimoLps + ".00" : ListaAFP[i].afp_AporteMinimoLps,
                    (ListaAFP[i].afp_InteresAporte % 1 == 0) ? ListaAFP[i].afp_InteresAporte + ".00" : ListaAFP[i].afp_InteresAporte,
                    (ListaAFP[i].afp_InteresAnual % 1 == 0) ? ListaAFP[i].afp_InteresAnual + ".00" : ListaAFP[i].afp_InteresAnual,
                    ListaAFP[i].tde_Descripcion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
            FullBody();
        });
}


// --- activar --- //

var activarID = 0;

// activar 1
$(document).on("click", "#tblAFP tbody tr td #btnActivarAFP", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AFP/Activar");

    if (validacionPermiso.status == true) {
        // habilitar boton inactivar
        $("#btnActivarRegistroAFP").attr('disabled', false);

        // obtener ID
        var ID = $(this).data('id');
        activarID = ID;

        // guardar ID en local storage
        localStorage.setItem('id', ID);

        //mostrar el Modal activar
        $("#ActivarAFP").modal();
    }
    
});

// activar 2
$("#btnActivarRegistroAFP").click(function () {

    // inhabilitar el boton
    $("#btnActivarRegistroAFP").attr('disabled', false);
    let ID = activarID;


    $.ajax({
        url: "/AFP/Activar",
        method: "POST",
        data: { id: ID }
    })
        .done(function (data) {

            $("#ActivarAFP").modal('hide');

            // validar errores
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se activó el registro, contacte al administrador!',
                });
            }
            else {

                // refrescar datatable
                cargarGridDeducciones();

                // mensaje de exito
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se activó de forma exitosa!',
                });
            }
        });
    activarID = 0;

});

// --- crear --- //

// cerrar modal create
$("#btnCerrarCrear").click(function () {

    $("#AgregarAFP").modal('hide');
});

// crear 1 mostrar modal
$(document).on("click", "#btnAgregarAFP", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AFP/Create");

    if (validacionPermiso.status == true) {
        //llenar DDL
        $.ajax({
            url: "/AFP/EditGetTipoDeduccionDDL",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (data) {

                // limpiar DDL
                $("#Crear #tde_IdTipoDedu").empty();
                $("#Crear #tde_IdTipoDedu").append("<option value='0'>Selecione una opción...</option>");

                // setear DDL
                $.each(data, function (i, iter) {
                    $("#Crear #tde_IdTipoDedu").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });

        //--
        // * descripcion 
        $('#asiteriscoDescripcion').removeClass('text-danger');

        // mesanje descripcion requerida
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');

        // mesanje descripcion no es numerico
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');

        //--
        // * aporte minimo LPIS
        $('#astericosAporteMinimoLps').removeClass('text-danger');

        // mensaje aporte minimo debe ser mayor que cero
        $("#Crear #validation_AporteMinimoMayorACero").css('display', 'none');

        //--
        // * interes aporte 
        $('#astericoInteresAporte').removeClass('text-danger');

        // mensaje interes aporte no puede ser menor que cero
        $("#Crear #validation_InteresAporteMenorACero").css('display', 'none');

        // mensaje interes aporte no puede ser mayor que cién
        $("#Crear #validation_InteresAporteMenorACien").css('display', 'none');

        // -- 
        // * interes anual 
        $('#astericosInteresAnual').removeClass('text-danger');

        // mensaje interes anual no puede ser menor que cero
        $("#Crear #validation_InteresAnualMenorACero").css('display', 'none');

        // mensaje interes anual no puede ser mayor que cién
        $("#Crear #validation_InteresAnualMenorACien").css('display', 'none');

        //--
        // * tipo de deduccion
        $('#astericosTipoDeduccion').removeClass('text-danger');

        // mensaje interes aporte no puede ser menor que cero
        $("#Crear #validation_TipoDeduccionRequerida").css('display', 'none');


        //--

        // vaciar cajas de texto
        $('#Crear input[type=text], input[type=number]').val('');

        // habilitar boton 
        $("#btnCreateRegistroAFP").attr('disabled', false);

        //mostrar modal
        $("#AgregarAFP").modal({ backdrop: 'static', keyboard: false });
    }
    

});

// crear 2 ejecutar
$('#btnCreateRegistroAFP').click(function () {

    // deshabilitar el boton
    $("#btnCreateRegistroAFP").attr('disabled', true);

    var modalState = true;
    var descripcion = $("#Crear #afp_Descripcion").val();

    // validar descripción
    if (descripcion.trim() != '') {

        $('#asiteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#asiteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
        modalState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#asiteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
        modalState = false;
    }
    // si no es un número
    else if (isNaN(descripcion) == true) {

        $('#asiteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

    //--

    // validar aporte minimo lps

    // si es menor o igual que cero
    if (parseInt($("#Crear #afp_AporteMinimoLps").val()) > 0) {

        $('#astericosAporteMinimoLps').removeClass('text-danger');
        $("#Crear #validation_AporteMinimoMayorACero").css('display', 'none');
    }
    else {
        $('#astericosAporteMinimoLps').addClass("text-danger");
        $("#Crear #validation_AporteMinimoMayorACero").css('display', '');
        modalState = false;
    }

    //--

    // validar interes aporte

    // si es menor que cero
    if (parseInt($("#Crear #afp_InteresAporte").val()) > 0) {

        $('#astericoInteresAporte').removeClass('text-danger');
        $("#Crear #validation_InteresAporteMenorACero").css('display', 'none');
    }
    else {
        $('#astericoInteresAporte').addClass("text-danger");
        $("#Crear #validation_InteresAporteMenorACero").css('display', '');
        modalState = false;
    }

    // si es mayor que cién
    var interesAporteSinComas = $("#Crear #afp_InteresAporte").val();
    interesAporteSinComas = interesAporteSinComas.replace(/,/g, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAporte").val()) > 0)
            $('#astericoInteresAporte').removeClass('text-danger');

        $("#Crear #validation_InteresAporteMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAporteSinComas).toFixed(2) > 100.00) {

        $('#astericoInteresAporte').addClass("text-danger");
        $("#Crear #validation_InteresAporteMenorACien").css('display', '');
        modalState = false;
    }

    //--

    // validar interes anual

    // si es menor que cero
    if (parseInt($("#Crear #afp_InteresAnual").val()) > 0) {

        $('#astericosInteresAnual').removeClass('text-danger');
        $("#Crear #validation_InteresAnualMenorACero").css('display', 'none');
    }
    else {
        $('#astericosInteresAnual').addClass("text-danger");
        $("#Crear #validation_InteresAnualMenorACero").css('display', '');
        modalState = false;
    }

    // si es mayor que cién
    var interesAnualSinComas = $("#Crear #afp_InteresAnual").val();
    interesAnualSinComas = interesAnualSinComas.replace(/,/g, '');

    if (parseFloat(interesAnualSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAnual").val()) > 0)
            $('#astericosInteresAnual').removeClass('text-danger');

        $("#Crear #validation_InteresAnualMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAnualSinComas).toFixed(2) > 100.00) {

        $('#astericosInteresAnual').addClass("text-danger");
        $("#Crear #validation_InteresAnualMenorACien").css('display', '');
        modalState = false;
    }

    //--

    // validar tipo de deduccion create 
    if ($("#Crear #tde_IdTipoDedu").val() != 0) {
        $("#Crear #validation_TipoDeduccionRequerida").css('display', 'none');
        $("#Crear #astericosTipoDeduccion").removeClass('text-danger');

    }
    else {
        $("#Crear #validation_TipoDeduccionRequerida").css('display', '');
        $("#Crear #astericosTipoDeduccion").addClass("text-danger");
        modalState = false;
    }


    if (modalState == true) {

        // serializar formulario
        var data = $("#frmCreateAFP").serializeArray();

        // quitar comas de los montos
        var aporteMinimoComas = data[3].value;
        data[3].value = aporteMinimoComas.replace(/,/g, '');

        // peticion
        $.ajax({
            url: "/AFP/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            // validar resultado del proceso
            if (data != "error") {

                cargarGridDeducciones();

                // cerrar modal
                $("#AgregarAFP").modal('hide');

                // mensaje de exito
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else {

                $("#btnCreateRegistroAFP").attr('disabled', false);
                // mensaje de error
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
        });
    }
    else {
        $("#btnCreateRegistroAFP").attr('disabled', false);
    }
});

// validaciones key up create

// validar descripcion create
$('#Crear #afp_Descripcion').keyup(function () {

    var descripcion = $("#Crear #afp_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#asiteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionRequerida").css('display', 'none');
    }
    else {
        $('#asiteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionRequerida").css('display', '');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#asiteriscoDescripcion').addClass("text-danger");
        $("#Crear #validation_DescripcionNumerico").css('display', '');
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#asiteriscoDescripcion').removeClass('text-danger');
        $("#Crear #validation_DescripcionNumerico").css('display', 'none');
    }

});

//validar aporte minimo create
$('#Crear #afp_AporteMinimoLps').keyup(function () {

    // si es menor o igual que cero
    if (parseInt($("#Crear #afp_AporteMinimoLps").val()) > 0) {

        $('#astericosAporteMinimoLps').removeClass('text-danger');
        $("#Crear #validation_AporteMinimoMayorACero").css('display', 'none');
    }
    else {
        $('#astericosAporteMinimoLps').addClass("text-danger");
        $("#Crear #validation_AporteMinimoMayorACero").css('display', '');
    }

});

// validar interes aporte create
$('#Crear #afp_InteresAporte').keyup(function () {

    // si es menor que cero
    if (parseInt($("#Crear #afp_InteresAporte").val()) > 0) {

        $('#astericoInteresAporte').removeClass('text-danger');
        $("#Crear #validation_InteresAporteMenorACero").css('display', 'none');
    }
    else {
        $('#astericoInteresAporte').addClass("text-danger");
        $("#Crear #validation_InteresAporteMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAporteSinComas = $("#Crear #afp_InteresAporte").val();
    interesAporteSinComas = interesAporteSinComas.replace(/,/g, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAporte").val()) >= 0)
            $('#astericoInteresAporte').removeClass('text-danger');

        $("#Crear #validation_InteresAporteMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAporteSinComas).toFixed(2) > 100.00) {

        $('#astericoInteresAporte').addClass("text-danger");
        $("#Crear #validation_InteresAporteMenorACien").css('display', '');
    }

});

// validar interes anual create
$('#Crear #afp_InteresAnual').keyup(function () {

    // si es menor que cero
    if (parseInt($("#Crear #afp_InteresAnual").val()) > 0) {

        $('#astericosInteresAnual').removeClass('text-danger');
        $("#Crear #validation_InteresAnualMenorACero").css('display', 'none');
    }
    else {
        $('#astericosInteresAnual').addClass("text-danger");
        $("#Crear #validation_InteresAnualMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAnualSinComas = $("#Crear #afp_InteresAnual").val();
    interesAnualSinComas = interesAnualSinComas.replace(/,/g, '');

    if (parseFloat(interesAnualSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAnual").val()) >= 0)
            $('#astericosInteresAnual').removeClass('text-danger');

        $("#Crear #validation_InteresAnualMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAnualSinComas).toFixed(2) > 100.00) {

        $('#astericosInteresAnual').addClass("text-danger");
        $("#Crear #validation_InteresAnualMenorACien").css('display', '');
    }

});

// validar tipo de deduccion create 
$('#Crear #tde_IdTipoDedu').blur(function () {

    if (this.value != 0) {
        $("#Crear #validation_TipoDeduccionRequerida").css('display', 'none');
        $("#Crear #astericosTipoDeduccion").removeClass('text-danger');

    }
    else {
        $("#Crear #validation_TipoDeduccionRequerida").css('display', '');
        $("#Crear #astericosTipoDeduccion").addClass("text-danger");
    }
});


// --- Editar ---//

// ocultar modal
$("#btnCerrarEditar").click(function () {

    $("#EditarAFP").modal('hide');
});


var inactivarID = 0;
var activarID = 0;

// editar 1
$(document).on("click", "#tblAFP tbody tr td #btnEditarAFP", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AFP/Edit");

    if (validacionPermiso.status == true) {
        $("#btnEditAFP").attr('disabled', false);
        var ID = $(this).data('id');
        inactivarID = ID;

        $.ajax({
            url: "/AFP/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                // obtener data y setear cajas de texto
                if (data) {
                    $("#Editar #afp_Id").val(data.afp_Id);
                    $("#Editar #afp_Descripcion").val(data.afp_Descripcion);
                    $("#Editar #afp_AporteMinimoLps").val((data.afp_AporteMinimoLps % 1 == 0) ? data.afp_AporteMinimoLps + ".00" : data.afp_AporteMinimoLps);
                    $("#Editar #afp_InteresAporte").val((data.afp_InteresAporte % 1 == 0) ? data.afp_InteresAporte + ".00" : data.afp_InteresAporte);
                    $("#Editar #afp_InteresAnual").val((data.afp_InteresAnual % 1 == 0) ? data.afp_InteresAnual + ".00" : data.afp_InteresAnual);

                    // ID selecciona en el DDL
                    var SelectedId = data.tde_IdTipoDedu;

                    // llenar DDLs
                    $.ajax({
                        url: "/AFP/EditGetTipoDeduccionDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                        })
                        .done(function (data) {

                            // limipiar ddl
                            $("#Editar #tde_IdTipoDedu").empty();

                            // llenar ddl
                            $.each(data, function (i, iter) {
                                $("#Editar #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });
                        });

                    //--
                    // * descripcion 
                    $('#EditarasiteriscoDescripcion').removeClass('text-danger');

                    // mesanje descripcion requerida
                    $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');

                    // mesanje descripcion no es numerico
                    $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');

                    //--
                    // * aporte minimo LPIS
                    $('#EditarastericosAporteMinimoLps').removeClass('text-danger');

                    // mensaje aporte minimo debe ser mayor que cero
                    $("#Editar #validation_EditarAporteMinimoMayorACero").css('display', 'none');

                    //--
                    // * interes aporte 
                    $('#EditarastericoInteresAporte').removeClass('text-danger');

                    // mensaje interes aporte no puede ser menor que cero
                    $("#Editar #validation_EditarInteresAporteMenorACero").css('display', 'none');

                    // mensaje interes aporte no puede ser mayor que cién
                    $("#Editar #validation_EditarInteresAporteMenorACien").css('display', 'none');

                    // -- 
                    // * interes anual 
                    $('#EditarastericosInteresAnual').removeClass('text-danger');

                    // mensaje interes anual no puede ser menor que cero
                    $("#Editar #validation_EditarInteresAnualMenorACero").css('display', 'none');

                    // mensaje interes anual no puede ser mayor que cién
                    $("#Editar #validation_EditarInteresAnualMenorACien").css('display', 'none');

                    //--
                    // * tipo de deduccion
                    $('#EditarastericosTipoDeduccion').removeClass('text-danger');

                    // mensaje interes aporte no puede ser menor que cero
                    $("#Editar #validation_EditarTipoDeduccionRequerida").css('display', 'none');

                    // habilitar boton 
                    $("#btnEditAFP").attr('disabled', false);


                    // mostrar modal editar
                    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
                }
                else {

                    // mostrar error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se cargó la información, contacte al administrador!',
                    });
                }
            });
    }
  
});

// editar 2
$("#btnEditAFP").click(function () {
    // validaciones

    // deshabilitar el boton
    $("#btnEditAFP").attr('disabled', true);

    var modalState = true;
    var descripcion = $("#Editar #afp_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#EditarasiteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#EditarasiteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
        modalState = false;
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#EditarasiteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionNumerico").css('display', '');
        modalState = false;
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditarasiteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

    //--

    // validar aporte minimo lps

    // si es menor o igual que cero
    if (parseInt($("#Editar #afp_AporteMinimoLps").val()) > 0) {

        $('#EditarastericosAporteMinimoLps').removeClass('text-danger');
        $("#Editar #validation_EditarAporteMinimoMayorACero").css('display', 'none');
    }
    else {
        $('#EditarastericosAporteMinimoLps').addClass("text-danger");
        $("#Editar #validation_EditarAporteMinimoMayorACero").css('display', '');
        modalState = false;
    }

    //--

    // validar interes aporte

    // si es menor que cero
    if (parseInt($("#Editar #afp_InteresAporte").val()) > 0) {

        $('#EditarastericoInteresAporte').removeClass('text-danger');
        $("#Editar #validation_EditarInteresAporteMenorACero").css('display', 'none');
    }
    else {
        $('#EditarastericoInteresAporte').addClass("text-danger");
        $("#Editar #validation_EditarInteresAporteMenorACero").css('display', '');
        modalState = false;
    }

    // si es mayor que cién
    var interesAporteSinComas = $("#Editar #afp_InteresAporte").val();
    interesAporteSinComas = interesAporteSinComas.replace(/,/g, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Editar #afp_InteresAporte").val()) >= 0)
            $('#EditarastericoInteresAporte').removeClass('text-danger');

        $("#Editar #validation_EditarInteresAporteMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAporteSinComas).toFixed(2) > 100.00) {

        $('#EditarastericoInteresAporte').addClass("text-danger");
        $("#Editar #validation_EditarInteresAporteMenorACien").css('display', '');
        modalState = false;
    }

    //--

    // validar interes anual

    // si es menor que cero
    if (parseInt($("#Editar #afp_InteresAnual").val()) > 0) {

        $('#EditarastericosInteresAnual').removeClass('text-danger');
        $("#Editar #validation_EditarInteresAnualMenorACero").css('display', 'none');
    }
    else {
        $('#EditarastericosInteresAnual').addClass("text-danger");
        $("#Editar #validation_EditarInteresAnualMenorACero").css('display', '');
        modalState = false;
    }

    // si es mayor que cién
    var interesAnualSinComas = $("#Editar #afp_InteresAnual").val();
    interesAnualSinComas = interesAnualSinComas.replace(/,/g, '');

    if (parseFloat(interesAnualSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Editar #afp_InteresAnual").val()) >= 0)
            $('#EditarastericosInteresAnual').removeClass('text-danger');

        $("#Editar #validation_EditarInteresAnualMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAnualSinComas).toFixed(2) > 100.00) {

        $('#EditarastericosInteresAnual').addClass("text-danger");
        $("#Editar #validation_EditarInteresAnualMenorACien").css('display', '');
        modalState = false;
    }

    //--

    // validar tipo de deduccion create 
    if ($("#Editar #tde_IdTipoDedu").val() != 0) {
        $("#Editar #validation_EditarTipoDeduccionRequerida").css('display', 'none');
        $("#Editar #EditarastericosTipoDeduccion").removeClass('text-danger');

    }
    else {
        $("#Editar #validation_EditarTipoDeduccionRequerida").css('display', '');
        $("#Editar #EditarastericosTipoDeduccion").addClass("text-danger");
        modalState = false;
    }

    // terminan validaciones
    //--
    if (modalState == true) {
        $("#EditarAFP").modal('hide');
        $("#EditarAFPConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("#btnEditAFPConfirmar").attr('disabled', false);
    }
    else {
        $("#btnEditAFP").attr('disabled', false);
       
    }


});

// --- validaciones key up editar ---

$('#Editar #afp_Descripcion').keyup(function () {

    var descripcion = $("#Editar #afp_Descripcion").val();

    //si no está vacio
    if (descripcion.trim() != '') {

        $('#EditarasiteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionRequerida").css('display', 'none');
    }
    else {
        $('#EditarasiteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionRequerida").css('display', '');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

    // si es un número y no está vacio
    if (isNaN(descripcion) == false && descripcion.trim() != '') {

        $('#EditarasiteriscoDescripcion').addClass("text-danger");
        $("#Editar #validation_EditarDescripcionNumerico").css('display', '');
    }
    // si es un número
    else if (isNaN(descripcion) == true) {

        $('#EditarasiteriscoDescripcion').removeClass('text-danger');
        $("#Editar #validation_EditarDescripcionNumerico").css('display', 'none');
    }

});

$('#Editar #afp_AporteMinimoLps').keyup(function () {

    // si es menor o igual que cero
    if (parseInt($("#Editar #afp_AporteMinimoLps").val()) > 0) {

        $('#EditarastericosAporteMinimoLps').removeClass('text-danger');
        $("#Editar #validation_EditarAporteMinimoMayorACero").css('display', 'none');
    }
    else {
        $('#EditarastericosAporteMinimoLps').addClass("text-danger");
        $("#Editar #validation_EditarAporteMinimoMayorACero").css('display', '');
    }
});

$('#Editar #afp_InteresAporte').keyup(function () {

    // si es menor que cero
    if (parseInt($("#Editar #afp_InteresAporte").val()) >= 0) {

        $('#EditarastericoInteresAporte').removeClass('text-danger');
        $("#Editar #validation_EditarInteresAporteMenorACero").css('display', 'none');
    }
    else {
        $('#EditarastericoInteresAporte').addClass("text-danger");
        $("#Editar #validation_EditarInteresAporteMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAporteSinComas = $("#Editar #afp_InteresAporte").val();
    interesAporteSinComas = interesAporteSinComas.replace(/,/g, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Editar #afp_InteresAporte").val()) >= 0)
            $('#EditarastericoInteresAporte').removeClass('text-danger');

        $("#Editar #validation_EditarInteresAporteMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAporteSinComas).toFixed(2) > 100.00) {

        $('#EditarastericoInteresAporte').addClass("text-danger");
        $("#Editar #validation_EditarInteresAporteMenorACien").css('display', '');
    }
});

$('#Editar #afp_InteresAnual').keyup(function () {

    // si es menor que cero
    if (parseInt($("#Editar #afp_InteresAnual").val()) >= 0) {

        $('#EditarastericosInteresAnual').removeClass('text-danger');
        $("#Editar #validation_EditarInteresAnualMenorACero").css('display', 'none');
    }
    else {
        $('#EditarastericosInteresAnual').addClass("text-danger");
        $("#Editar #validation_EditarInteresAnualMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAnualSinComas = $("#Editar #afp_InteresAnual").val();
    interesAnualSinComas = interesAnualSinComas.replace(/,/g, '');

    if (parseFloat(interesAnualSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Editar #afp_InteresAnual").val()) >= 0)
            $('#EditarastericosInteresAnual').removeClass('text-danger');

        $("#Editar #validation_EditarInteresAnualMenorACien").css('display', 'none');
    }
    else if (parseFloat(interesAnualSinComas).toFixed(2) > 100.00) {

        $('#EditarastericosInteresAnual').addClass("text-danger");
        $("#Editar #validation_EditarInteresAnualMenorACien").css('display', '');
    }

});

$('#Editar #tde_IdTipoDedu').on('change', function () {

    if (this.value != 0) {
        $("#Editar #validation_EditarTipoDeduccionRequerida").css('display', 'none');
        $("#Editar #EditarastericosTipoDeduccion").removeClass('text-danger');

    }
    else {
        $("#Editar #validation_EditarTipoDeduccionRequerida").css('display', '');
        $("#Editar #EditarastericosTipoDeduccion").addClass("text-danger");
    }

});


// boton no confirmar edicion
$(document).on("click", "#btnRegresar", function () {
    $("#EditarAFPConfirmacion").modal('hide');
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
    $("#btnEditAFP").attr('disabled', false);
});


// editar 3 ejecutar
$("#btnEditAFPConfirmar").click(function () {

    $("#btnEditAFPConfirmar").attr('disabled', true);

    // serializar formulario
    var data = $("#frmEditarAFP").serializeArray();

    // quitar comas de los montos
    var aporteMinimoComas = data[3].value;
    data[3].value = aporteMinimoComas.replace(/,/g, '');


    $.ajax({
        url: "/AFP/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {

            //ocultar modal
            $("#EditarAFPConfirmacion").modal('hide');
            $("#EditarAFP").modal('hide');

            //actualizar datatable
            cargarGridDeducciones();

            // mensaje de exito
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
        else {
            // mensaje de error
            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
            $("#btnEditAFPConfirmar").attr('disabled', false);
        }
    });
});


// --- Detalles --- 
$(document).on("click", "#tblAFP tbody tr td #btnDetalleAFP", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AFP/Details");

    if (validacionPermiso.status == true) {
        // obtener id registro
        var ID = $(this).data('id');

        $.ajax({
            url: "/AFP/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                // si se obtiene data setear items
                if (data) {
                    var FechaCrea = FechaFormato(data[0].afp_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].afp_FechaModifica);
                    $(".field-validation-error").css('display', 'none');
                    $("#Detalles #afp_Id").html(data[0].afp_Id);
                    $("#Detalles #afp_Descripcion").html(data[0].afp_Descripcion);
                    $("#Detalles #afp_AporteMinimoLps").html((data[0].afp_AporteMinimoLps % 1 == 0) ? data[0].afp_AporteMinimoLps + ".00" : data[0].afp_AporteMinimoLps);
                    $("#Detalles #afp_InteresAporte").html((data[0].afp_InteresAporte % 1 == 0) ? data[0].afp_InteresAporte + ".00" : data[0].afp_InteresAporte);
                    $("#Detalles #afp_InteresAnual").html((data[0].afp_InteresAnual % 1 == 0) ? data[0].afp_InteresAnual + ".00" : data[0].afp_InteresAnual);
                    $("#Detalles #tde_IdTipoDedu").html(data[0].tde_IdTipoDedu);
                    $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #afp_UsuarioCrea").html(data[0].afp_UsuarioCrea);
                    $("#Detalles #afp_FechaCrea").html(FechaCrea);
                    data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #afp_UsuarioModifica").html(data[0].afp_UsuarioModifica);
                    $("#Detalles #afp_FechaModifica").html(FechaModifica);

                    // id selecciona
                    var SelectedId = data[0].tde_IdTipoDedu;

                    // obtener tipo de deduccion descripcion
                    $.ajax({
                        url: "/AFP/EditGetTipoDeduccionDDL",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                        })
                        .done(function (data) {

                            // setear caja de texto
                            $.each(data, function (i, iter) {
                                if (iter.Id == SelectedId) {
                                    $("#Detalles #tde_IdTipoDedu").html(iter.Descripcion);
                                }
                            });
                        });

                    // mostrar modal de detalles
                    $("#DetallesAFP").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    // mensaje de error
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se cargó la información, contacte al administrador!',
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


// --- Inactivar --- 

// inactivar no confirmar
$(document).on("click", "#btnBack", function () {

    // habilitar boton
    $("#btnInactivarRegistroAFP").attr('disabled', false);

    // ocultar modal inactivar
    $("#InactivarAFP").modal('hide');

    // mostrar modal editar
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });

});

// inactivar confirmar
$(document).on("click", "#btnInactivarAFP", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AFP/Inactivar");

    if (validacionPermiso.status == true) {
        // habilitar boton ejecutar inactivar
        $("#btnInactivarRegistroAFP").attr('disabled', false);

        // modales
        $("#EditarAFP").modal('hide');
        $("#InactivarAFP").modal({ backdrop: 'static', keyboard: false });
    }
    

});

// inactivar ejecutar
$("#btnInactivarRegistroAFP").click(function () {

    // deshabilitar el botón
    $("#btnInactivarRegistroAFP").attr('disabled', true);

    $.ajax({
        url: "/AFP/Inactivar",
        method: "POST",
        data: { afp_Id: inactivarID }
    }).done(function (data) {
        if (data == "error") {

            // mensaje de error
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {

            // actualizar datatable
            cargarGridDeducciones();

            // ocultar modales
            $("#InactivarAFP").modal('hide');
            $("#EditarAFP").modal('hide');

            // mensaje de exito
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });

});


// obtener script serialize date
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

// evitar postbacks de formularios
$("#frmCreateAFP").submit(function (e) {
    return false;
});

$("#frmInactivarAFP").submit(function (e) {
    return false;
});

$("#frmEditarAFP").submit(function (e) {
    return false;
});

$("#EditarAFP").submit(function (e) {
    return false;
});