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
                var botonDetalles = ListaAFP[i].afp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleAFP" data-id = "' + ListaAFP[i].afp_Id + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAFP[i].afp_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarAFP" data-id = "' + ListaAFP[i].afp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAFP[i].afp_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarAFP" afpid="' + ListaAFP[i].afp_Id + '" data-id = "' + ListaAFP[i].afp_Id + '">Activar</button>' : '' : '';

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

    // habilitar boton inactivar
    $("#btnActivarRegistroAFP").attr('disabled', false);

    // obtener ID
    var ID = $(this).data('id');
    activarID = ID;

    // guardar ID en local storage
    localStorage.setItem('id', ID);

    //mostrar el Modal activar
    $("#ActivarAFP").modal();
});

// activar 2
$("#btnActivarRegistroAFP").click(function () {

    // inhabilitar el boton
    $("#btnActivarRegistroAFP").attr('disabled',false);
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
    if (parseInt($("#Crear #afp_InteresAporte").val()) >= 0) {

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
    interesAporteSinComas = interesAporteSinComas.replace(/,/, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAporte").val()) >= 0)
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
    if (parseInt($("#Crear #afp_InteresAnual").val()) >= 0) {

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
    interesAnualSinComas = interesAnualSinComas.replace(/,/, '');

    if (parseFloat(interesAnualSinComas).toFixed(2) <= 100.00) {

        if (parseInt($("#Crear #afp_InteresAnual").val()) >= 0)
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

        debugger;
        // serializar formulario
        var data = $("#frmCreateAFP").serializeArray();

        // quitar comas de los montos
        var aporteMinimoComas = data[3].value;
        data[3].value = aporteMinimoComas.replace(/,/, '');

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
    if (parseInt($("#Crear #afp_InteresAporte").val()) >= 0) {

        $('#astericoInteresAporte').removeClass('text-danger');
        $("#Crear #validation_InteresAporteMenorACero").css('display', 'none');
    }
    else {
        $('#astericoInteresAporte').addClass("text-danger");
        $("#Crear #validation_InteresAporteMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAporteSinComas = $("#Crear #afp_InteresAporte").val();
    interesAporteSinComas = interesAporteSinComas.replace(/,/, '');

    if (parseFloat(interesAporteSinComas).toFixed(2) <= 100.00) {

        if(parseInt($("#Crear #afp_InteresAporte").val()) >= 0)
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
    if (parseInt($("#Crear #afp_InteresAnual").val()) >= 0) {

        $('#astericosInteresAnual').removeClass('text-danger');
        $("#Crear #validation_InteresAnualMenorACero").css('display', 'none');
    }
    else {
        $('#astericosInteresAnual').addClass("text-danger");
        $("#Crear #validation_InteresAnualMenorACero").css('display', '');
    }

    // si es mayor que cién
    var interesAnualSinComas = $("#Crear #afp_InteresAnual").val();
    interesAnualSinComas = interesAnualSinComas.replace(/,/, '');

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
$('#Crear #tde_IdTipoDedu').on('change', function () {
    
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

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {

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
var inactivarID = 0;
var activarID = 0;

$(document).on("click", "#tblAFP tbody tr td #btnEditarAFP", function () {
    document.getElementById("btnEditAFPConfirmar").disabled = false;
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
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #afp_Id").val(data.afp_Id);
                $("#Editar #afp_Descripcion").val(data.afp_Descripcion);
                $("#Editar #afp_AporteMinimoLps").val((data.afp_AporteMinimoLps % 1 == 0) ? data.afp_AporteMinimoLps + ".00" : data.afp_AporteMinimoLps);
                $("#Editar #afp_InteresAporte").val((data.afp_InteresAporte % 1 == 0) ? data.afp_InteresAporte + ".00" : data.afp_InteresAporte);
                $("#Editar #afp_InteresAnual").val((data.afp_InteresAnual % 1 == 0) ? data.afp_InteresAnual + ".00" : data.afp_InteresAnual);

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
    if (vale1 == "" || vale1 == null) {
        $("#validationes1").css("display", "");
        $("#Editar #aste1").css("color", "red");
        TOF = false;
    }
    else {
        $("#validationes1").css("display", "none");
        $("#Editar #aste1").css("color", "black");
    }
    //--
    if (vale2 != "" || vale2 != null || vale2 != undefined) {
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2").css("display", "");
        $("#validationes2d").css("display", "none");
        $("#Editar #aste2").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale2)) {
        $("#validationes2d").css("display", "none");
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2d").css("display", "");
        $("#Editar #aste2").css("color", "red");
        TOF = false;
    }
    //--
    if (vale3 != "" || vale3 != null || vale3 != undefined) {
        $("#Editar #aste3").css("color", "black");
        $("#validationes3d").css("display", "none");
    }
    else {
        $("#validationes3").css("display", "");
        $("#validationes3d").css("display", "none");
        $("#Editar #aste3").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale3)) {
        $("#validationes3d").css("display", "none");
        $("#Editar #aste3").css("color", "black");
    }
    else {
        $("#validationes3d").css("display", "");
        $("#Editar #aste3").css("color", "red");
        TOF = false;
    }
    //--
    if (vale4 != "" || vale4 != null || vale4 != undefined) {
        $("#Editar #aste4").css("color", "black");
        $("#validationes4").css("display", "none");
    }
    else {
        $("#validationes4").css("display", "");
        $("#validationes4d").css("display", "none");
        $("#Editar #aste4").css("color", "red");
        TOF = false;
    }
    if (expreg.test(vale4)) {
        $("#validationes4d").css("display", "none");
        $("#Editar #aste4").css("color", "black");
    }
    else {
        $("#validationes4d").css("display", "");
        $("#Editar #aste4").css("color", "red");
        TOF = false;
    }
    //--
    if (TOF == true) {
        $("#EditarAFP").modal('hide');
        $("#EditarAFPConfirmacion").modal({ backdrop: 'static', keyboard: false });
    }

    $("#EditarAFP").submit(function (e) {
        return false;
    });
});

// data annotations editar

$('#Editar #afp_Descripcion').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $("#validationes1").css("display", "none");
        $("#Editar #aste1").css("color", "black");
    }
    else {
        $("#validationes1").css("display", "");
        $("#Editar #aste1").css("color", "red");
    }
});

$('#Editar #afp_AporteMinimoLps').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2").css("display", "");
        $("#validationes2d").css("display", "none");
        $("#Editar #aste2").css("color", "red");
    }

    if (expreg.test($(this).val())) {
        $("#validationes2d").css("display", "none");
        $("#Editar #aste2").css("color", "black");
    }
    else {
        $("#validationes2d").css("display", "");
        $("#Editar #aste2").css("color", "red");
    }
});

$('#Editar #afp_InteresAporte').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $("#Editar #aste3").css("color", "black");
    }
    else {
        $("#validationes3").css("display", "");
        $("#validationes3d").css("display", "none");
        $("#Editar #aste3").css("color", "red");
    }

    if (expreg.test($(this).val())) {
        $("#validationes3d").css("display", "none");
        $("#Editar #aste3").css("color", "black");
    }
    else {
        $("#validationes3d").css("display", "");
        $("#Editar #aste3").css("color", "red");
    }
});

$('#Editar #afp_InteresAnual').keyup(function () {
    if ($(this)
        .val()
        .trim() != '') {
        $("#Editar #aste4").css("color", "black");
    }
    else {
        $("#validationes4").css("display", "");
        $("#validationes4d").css("display", "none");
        $("#Editar #aste4").css("color", "red");
    }

    if (expreg.test($(this).val())) {
        $("#validationes4d").css("display", "none");
        $("#Editar #aste4").css("color", "black");
    }
    else {
        $("#validationes4d").css("display", "");
        $("#Editar #aste4").css("color", "red");
    }
});

$('#Crear #tde_IdTipoDedu').on('change', function () {
    if (this.value != 0) {
        $("#Crear #validation5").css("display", "none");
        $("#Crear #ast5").css("color", "black");

    }
    else {
        $("#Crear #validation5").css("display", "");
        $("#Crear #ast5").css("color", "red");
    }
});






$(document).on("click", "#btnRegresar", function () {
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });
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

    $("#InactivarAFP").modal('hide');
});

$(document).on("click", "#btnBa", function () {
    $("#EditarAFP").modal({ backdrop: 'static', keyboard: false });


    $("#InactivarAFP").modal('hide');
});

$(document).on("click", "#btnInactivarAFP", function () {
    document.getElementById("btnInactivarRegistroAFP").disabled = false;
    $("#EditarAFP").modal('hide');
    $("#InactivarAFP").modal({ backdrop: 'static', keyboard: false });


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
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AFP/Inactivar",
        method: "POST",
        data: { afp_Id: inactivarID }
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


// obtener script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
        console.log(textStatus);
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
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

// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#frmCreateAFP").submit(function (e) {
    return false;
});