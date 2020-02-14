var IDInactivar = 0;

// cargar grid
function cargarGridPreaviso() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/Preaviso/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {

                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            var ListaPreaviso = data;

            // limpiar datatable
            $('#tblPreaviso').DataTable().clear();

            for (var i = 0; i < ListaPreaviso.length; i++) {

                var FechaCrea = FechaFormato(ListaPreaviso[i].prea_FechaCrea);
                var FechaModifica = FechaFormato(ListaPreaviso[i].prea_FechaModifica);

                UsuarioModifica = ListaPreaviso[i].NombreUsuarioModifica == null ? 'Sin modificaciones' : ListaPreaviso[i].NombreUsuarioModifica;

                //variable para verificar el estado del registro
                var estadoRegistro = ListaPreaviso[i].prea_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetallePreaviso">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaPreaviso[i].prea_Activo == true ? '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-default btn-xs"  id="btnEditarPreaviso">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaPreaviso[i].prea_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaPreaviso[i].prea_IdPreaviso + '" type="button" class="btn btn-default btn-xs"  id="btnActivarPreaviso">Activar</button>' : '' : '';

                // agregar row
                $('#tblPreaviso').dataTable().fnAddData([
                     ListaPreaviso[i].prea_IdPreaviso,
                     ListaPreaviso[i].prea_RangoInicioMeses,
                     ListaPreaviso[i].prea_RangoFinMeses,
                     ListaPreaviso[i].prea_DiasPreaviso,
                     estadoRegistro,
                     botonDetalles + botonEditar + botonActivar]
                 );
            }
        });
    FullBody();
}

// create 1 modal 
$(document).on("click", "#btnAgregarPreaviso", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("Preaviso/Create");

    if (validacionPermiso.status == true) {
        // * rango inicio 
        $('#AsteriscoRangoInicio').removeClass('text-danger');

        // mesanje rango inicio no puede ser menor a cero
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');

        // mesanje rango inicio requerido
        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');

        // * rango final 
        $('#AsteriscoRangoFinal').removeClass('text-danger');

        // mesanje rango final no puede ser menor a cero
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');

        // mesanje rango final requerido
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');

        // mesanje rango final no puede ser menor a rango inicial
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');

        // vaciar cajas de texto
        $('#Crear input[type=text], input[type=number]').val('');


        // habilitar boton
        $("#btnCrearPreavisoConfirmar").attr("disabled", false);

        // mostrar modal
        $("#CrearPreaviso").modal({ backdrop: 'static', keyboard: false });
    }

});

// validaciones key up create

// validar rango inicio create
$('#Crear #prea_RangoInicioMeses').keyup(function () {

    var rangoFin = $("#Crear #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #prea_RangoInicioMeses").val().replace(/,/g, '');

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #prea_RangoFinMeses").val().trim() == '' || $("#Crear #prea_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoFinal').removeClass('text-danger');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', '');
    }

    // si es menor o igual que cero
    if (parseInt(rangoInicio) >= 0 || $("#Crear #prea_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoInicio').removeClass('text-danger');
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoInicio').addClass("text-danger");
        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');
        $("#Crear #validation_RangoInicioMenorACero").css('display', '');
    }

    // requerido
    if ($("#Crear #prea_RangoInicioMeses").val().trim() != '') {

        if (parseInt(rangoInicio) >= 0) {
            $('#AsteriscoRangoInicio').removeClass('text-danger');
        }

        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoInicio').addClass("text-danger");
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');
        $("#Crear #validation_RangoInicioRequerido").css('display', '');
    }

});

// validar rango final create
$('#Crear #prea_RangoFinMeses').keyup(function () {

    var rangoFin = $("#Crear #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #prea_RangoInicioMeses").val().replace(/,/g, '');


    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || rangoFin.trim() == '' || rangoInicio.trim() == '') {

        $('#AsteriscoRangoFinal').removeClass('text-danger');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', '');
    }

    // si es menor o igual que cero
    if (parseInt(rangoFin) >= 0 || $("#Crear #prea_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Crear #prea_RangoFinMeses").val().trim() != '') {
            $('#AsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', '');
    }

    // requerido
    if ($("#Crear #prea_RangoFinMeses").val().trim() != '') {

        if (parseInt(rangoFin) >= 0 && parseInt(rangoFin) > parseInt(rangoInicio)) {
            $('#AsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
        $("#Crear #validation_RangoFinalRequerido").css('display', '');
    }

});

// validar cantidad de dias create
$('#Crear #prea_DiasPreaviso').keyup(function () {

    var cantidadDias = $("#Crear #prea_DiasPreaviso").val().replace(/,/g, '');

    // si es menor o igual que cero
    if (parseInt(cantidadDias) >= 0 || $("#Crear #prea_DiasPreaviso").val().trim() == '') {

        $('#AsteriscoCantidadDia').removeClass('text-danger');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoCantidadDia').addClass("text-danger");
        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', '');
    }

    // requerido
    if ($("#Crear #prea_DiasPreaviso").val().trim() != '') {

        if (parseInt($("#Crear #prea_DiasPreaviso").val()) >= 0) {
            $('#AsteriscoCantidadDia').removeClass('text-danger');
        }

        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoCantidadDia').addClass("text-danger");
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');
        $("#Crear #validation_CantidadDiasRequerido").css('display', '');
    }

});

// create 2 ejecutar 
$('#btnCrearPreavisoConfirmar').click(function () {

    // deshabilitar boton
    $('#btnCrearPreavisoConfirmar').attr('disabled', true);
    var modalState = true;
    var rangoFin = $("#Crear #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #prea_RangoInicioMeses").val().replace(/,/g, '');
    var cantidaDiasPreaviso = $("#Crear #prea_DiasPreaviso").val().replace(/,/g, '');

    // --- validaciones rango inicio ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #prea_RangoFinMeses").val().trim() == '' || $("#Crear #prea_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoFinal').removeClass('text-danger');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', '');
        modalState = false;
    }

    // si es menor o igual que cero
    if (parseInt(rangoInicio) >= 0 || $("#Crear #prea_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoInicio').removeClass('text-danger');
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoInicio').addClass("text-danger");
        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');
        $("#Crear #validation_RangoInicioMenorACero").css('display', '');
        modalState = false;
    }

    // rango inicial requerido
    if ($("#Crear #prea_RangoInicioMeses").val().trim() != '') {

        if (parseInt(rangoInicio) >= 0) {
            $('#AsteriscoRangoInicio').removeClass('text-danger');
        }

        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoInicio').addClass("text-danger");
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');
        $("#Crear #validation_RangoInicioRequerido").css('display', '');
        modalState = false;
    }

    // --- validaciones rango fin ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #prea_RangoFinMeses").val().trim() == '' || $("#Crear #prea_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoFinal').removeClass('text-danger');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', '');
        modalState = false;
    }

    // si es menor o igual que cero
    if (parseInt(rangoFin) >= 0 || $("#Crear #prea_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Crear #prea_RangoFinMeses").val().trim() != '') {
            $('#AsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
        $("#Crear #validation_RangoFinalMenorACero").css('display', '');
        modalState = false;
    }

    // requerido
    if ($("#Crear #prea_RangoFinMeses").val().trim() != '') {

        if (parseInt(rangoFin) >= 0 && parseInt(rangoFin) > parseInt(rangoInicio)) {
            $('#AsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Crear #validation_RangoFinalRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoFinal').addClass("text-danger");
        $("#Crear #validation_RangoFinalMenorACero").css('display', 'none');
        $("#Crear #validation_RangoFinalMayoRangoInicio").css('display', 'none');
        $("#Crear #validation_RangoFinalRequerido").css('display', '');
        modalState = false;
    }

    // --- cantidad dias preaviso ---

    // si es menor o igual que cero
    if (parseInt($("#Crear #prea_DiasPreaviso").val()) >= 0 || $("#Crear #prea_DiasPreaviso").val().trim() == '') {

        $('#AsteriscoCantidadDia').removeClass('text-danger');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoCantidadDia').addClass("text-danger");
        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', '');
        modalState = false;
    }

    // requerido
    if ($("#Crear #prea_DiasPreaviso").val().trim() != '') {

        if (parseInt(cantidaDiasPreaviso) >= 0) {
            $('#AsteriscoCantidadDia').removeClass('text-danger');
        }

        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');
    }
    else {
        $('#AsteriscoCantidadDia').addClass("text-danger");
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');
        $("#Crear #validation_CantidadDiasRequerido").css('display', '');
        modalState = false;
    }

    //----

    if (modalState == true) {
        var data = $("#frmCreatePreaviso").serializeArray();

        

        var indiceRangoInicio = data[3].value;
        data[3].value = indiceRangoInicio.replace(/,/g, '');

        var indiceRangoFin = data[4].value;
        data[4].value = indiceRangoFin.replace(/,/g, '');

        var indiceCantidadDias = data[5].value;
        data[5].value = indiceCantidadDias.replace(/,/g, '');

        $.ajax({
            url: "/Preaviso/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data != "error") {

                $("#btnCrearPreavisoConfirmar").attr("disabled", false);
                $("#CrearPreaviso").modal('hide');

                cargarGridPreaviso();

                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else {

                $("#btnCrearPreavisoConfirmar").attr("disabled", false);

                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
        });
    }
    else {
        $('#btnCrearPreavisoConfirmar').attr('disabled', false);
    }
});


// editar 1 modal
$(document).on("click", "#tblPreaviso tbody tr td #btnEditarPreaviso", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("Preaviso/Edit");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        IDInactivar = ID;

        // * rango inicio 
        $('#EditAsteriscoRangoInicio').removeClass('text-danger');

        // mesanje rango inicio no puede ser menor a cero
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', 'none');

        // mesanje rango inicio requerido
        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');

        // * rango final 
        $('#EditAsteriscoRangoFinal').removeClass('text-danger');

        // mesanje rango final no puede ser menor a cero
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');

        // mesanje rango final requerido
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');

        // mesanje rango final no puede ser menor a rango inicial
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');


        // habilitar boton 
        $('#btnUpdatePreaviso').attr('disabled', false);

        $.ajax({
            url: "/Preaviso/Edit/" + ID,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {

                if (data) {

                    $.each(data, function (i, iter) {
                        $("#Editar #prea_IdPreaviso").val(iter.prea_IdPreaviso);
                        $("#Editar #prea_RangoInicioMeses").val(iter.prea_RangoInicioMeses);
                        $("#Editar #prea_RangoFinMeses").val(iter.prea_RangoFinMeses);
                        $("#Editar #prea_DiasPreaviso").val(iter.prea_DiasPreaviso);
                    });

                    $("#EditarPreaviso").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    iziToast.error({
                        title: 'Error',
                        message: 'No se pudo cargar la información, contacte al administrador',
                    });
                }
            });
    }
});

// validaciones key up update

// validar rango inicio create
$('#Editar #prea_RangoInicioMeses').keyup(function () {

    var rangoFin = $("#Editar #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #prea_RangoInicioMeses").val().replace(/,/g, '');

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #prea_RangoFinMeses").val().trim() == '' || $("#Editar #prea_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', '');
    }

    // si es menor o igual que cero
    if (parseInt(rangoInicio) >= 0 || $("#Editar #prea_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoInicio').removeClass('text-danger');
        $("#Editar #validation_RangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoInicio').addClass("text-danger");
        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', '');
    }

    // requerido
    if ($("#Editar #prea_RangoInicioMeses").val().trim() != '') {

        if (parseInt(rangoInicio) >= 0) {
            $('#EditAsteriscoRangoInicio').removeClass('text-danger');
        }

        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoInicio').addClass("text-danger");
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoInicioRequerido").css('display', '');
    }

});

// validar rango final create
$('#Editar #prea_RangoFinMeses').keyup(function () {

    var rangoFin = $("#Editar #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #prea_RangoInicioMeses").val().replace(/,/g, '');


    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || rangoFin.trim() == '' || rangoInicio.trim() == '') {

        $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', '');
    }

    // si es menor o igual que cero
    if (parseInt(rangoFin) >= 0 || $("#Editar #prea_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Editar #prea_RangoFinMeses").val().trim() != '') {
            $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Editar #validation_RangoFinalMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', '');
    }

    // requerido
    if ($("#Editar #prea_RangoFinMeses").val().trim() != '') {

        if (parseInt(rangoFin) >= 0 && parseInt(rangoFin) > parseInt(rangoInicio)) {
            $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
        $("#Editar #validation_EditRangoFinalRequerido").css('display', '');
    }

});

// validar cantidad de dias create
$('#Editar #prea_DiasPreaviso').keyup(function () {

    var cantidadDias = $("#Editar #prea_DiasPreaviso").val().replace(/,/g, '');

    // si es menor o igual que cero
    if (parseInt(cantidadDias) >= 0 || $("#Editar #prea_DiasPreaviso").val().trim() == '') {

        $('#EditAsteriscoCantidadDia').removeClass('text-danger');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoCantidadDia').addClass("text-danger");
        $("#Editar #validation_EditCantidadDiasRequerido").css('display', 'none');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', '');
    }

    // requerido
    if ($("#Editar #prea_DiasPreaviso").val().trim() != '') {

        if (parseInt($("#Editar #prea_DiasPreaviso").val()) >= 0) {
            $('#EditAsteriscoCantidadDia').removeClass('text-danger');
        }

        $("#Editar #validation_EditCantidadDiasRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoCantidadDia').addClass("text-danger");
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', 'none');
        $("#Editar #validation_EditCantidadDiasRequerido").css('display', '');
    }

});

// editar 2 ejecutar
$("#btnUpdatePreaviso").click(function () {

    $("#btnUpdatePreaviso").attr('disabled', true);

    var modalState = true;
    var rangoFin = $("#Editar #prea_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #prea_RangoInicioMeses").val().replace(/,/g, '');
    var cantidaDiasPreaviso = $("#Editar #prea_DiasPreaviso").val().replace(/,/g, '');

    // --- validaciones rango inicio ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #prea_RangoFinMeses").val().trim() == '' || $("#Editar #prea_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', '');
        modalState = false;
    }

    // si es menor o igual que cero
    if (parseInt(rangoInicio) >= 0 || $("#Editar #prea_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoInicio').removeClass('text-danger');
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoInicio').addClass("text-danger");
        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', '');
        modalState = false;
    }

    // rango inicial requerido
    if ($("#Editar #prea_RangoInicioMeses").val().trim() != '') {

        if (parseInt(rangoInicio) >= 0) {
            $('#EditAsteriscoRangoInicio').removeClass('text-danger');
        }

        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoInicio').addClass("text-danger");
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoInicioRequerido").css('display', '');
        modalState = false;
    }

    // --- validaciones rango fin ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #prea_RangoFinMeses").val().trim() == '' || $("#Editar #prea_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', '');
        modalState = false;
    }

    // si es menor o igual que cero
    if (parseInt(rangoFin) >= 0 || $("#Editar #prea_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Editar #prea_RangoFinMeses").val().trim() != '') {
            $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Editar #validation_RangoFinalMenorACero").css('display', 'none');
    }
    else {
        $('Edit#AsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', '');
        modalState = false;
    }

    // requerido
    if ($("#Editar #prea_RangoFinMeses").val().trim() != '') {

        if (parseInt(rangoFin) >= 0 && parseInt(rangoFin) > parseInt(rangoInicio)) {
            $('#EditAsteriscoRangoFinal').removeClass('text-danger');
        }

        $("#Editar #validation_EditRangoFinalRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoFinal').addClass("text-danger");
        $("#Editar #validation_EditRangoFinalMenorACero").css('display', 'none');
        $("#Editar #validation_EditRangoFinalMayoRangoInicio").css('display', 'none');
        $("#Editar #validation_EditRangoFinalRequerido").css('display', '');
        modalState = false;
    }

    // --- cantidad dias preaviso ---

    // si es menor o igual que cero
    if (parseInt(cantidaDiasPreaviso) >= 0 || $("#Editar #prea_DiasPreaviso").val().trim() == '') {

        $('#EditAsteriscoCantidadDia').removeClass('text-danger');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoCantidadDia').addClass("text-danger");
        $("#Editar #validation_EditCantidadDiasRequerido").css('display', 'none');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', '');
        modalState = false;
    }

    // requerido
    if ($("#Editar #prea_DiasPreaviso").val().trim() != '') {

        if (parseInt(cantidaDiasPreaviso) >= 0) {
            $('#EditAsteriscoCantidadDia').removeClass('text-danger');
        }

        $("#Editar #validation_EditCantidadDiasRequerido").css('display', 'none');
    }
    else {
        $('#EditAsteriscoCantidadDia').addClass("text-danger");
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', 'none');
        $("#Editar #validation_EditCantidadDiasRequerido").css('display', '');
        modalState = false;
    }


    if (modalState == true) {

        // ocultar modal de edicion
        $("#EditarPreaviso").modal('hide');

        // mostrar modal de confirmar edicion
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });

        // habilitar boton de confirmación
        $("#btnConfirmarEditar").attr("disabled", false);

    }
    else {
        $("#btnUpdatePreaviso").attr('disabled', false);
    }
});


// editar 3 ejecutar
$(document).on("click", "#btnConfirmarEditar", function () {

    $("#btnConfirmarEditar").attr("disabled", true);

    var data = $("#frmEditPreaviso").serializeArray();

    var indiceRangoInicio = data[3].value;
    data[3].value = indiceRangoInicio.replace(/,/g, '');

    var indiceRangoFin = data[4].value;
    data[4].value = indiceRangoFin.replace(/,/g, '');

    var indiceCantidadDias = data[5].value;
    data[5].value = indiceCantidadDias.replace(/,/g, '');



    $.ajax({
        url: "/Preaviso/Editar",
        method: "POST",
        data: data
    })
    .done(function (data) {

        // validar respuesta del servidor 
        if (data != "error") {

            $("#ConfirmarEdicion").modal('hide');
            cargarGridPreaviso();
            $("#btnUpdatePreaviso").attr("disabled", false);
            $("#btnConfirmarEditar").attr("disabled", false);

            // mensaje de exito
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
        else {

            $("#btnUpdatePreaviso").attr("disabled", false);
            $("#btnConfirmarEditar").attr("disabled", false);

            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
        }
    });
});

// inactivar cerrar modal confirmacion
$(document).on("click", "#btnCerrarConfirmarEditar", function () {

    // habilitar boton
    $("#btnUpdatePreaviso").attr("disabled", false);
    $("#btnConfirmarEditar").attr("disabled", false);

    // ocultar modal de confirmacion de edicion
    $("#ConfirmarEdicion").modal('hide');

    // mostrar modal de edicion
    $("#EditarPreaviso").modal();
});

//DESPLEGAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnInactivarPreaviso", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("Preaviso/Inactivar");

    if (validacionPermiso.status == true) {
        //OCULTAR EL MODAL DE EDICION
        $("#EditarPreaviso").modal('hide');
        //MOSTRAR MODAL DE INACTIVACION
        $("#InactivarPreaviso").modal({ backdrop: 'static', keyboard: false });
    }
});

//CERRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnCerrarInactivar", function () {
    //OCULTAR EL MODAL DE INACTIVACION
    $("#InactivarPreaviso").modal('hide');
    //MOSTRAR MODAL DE EDICION
    $("#EditarPreaviso").modal();
});

//CONFIRMAR INACTIVACION DEL REGISTRO
$("#btnInactivarPreavisoConfirmar").click(function () {
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/Preaviso/Inactivar/" + IDInactivar,
        method: "POST", dataType: "json",
        contentType: "application/json; charset=utf-8"
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
            cargarGridPreaviso();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarPreaviso").modal('hide');
            //MENSAJE DE EXITO DE LA EDICIÓN
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    IDInactivar = 0;
});



// Activar
var activarID = 0;
$(document).on("click", "#btnActivarPreaviso", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("Preaviso/Activar");

    if (validacionPermiso.status == true) {
        activarID = $(this).data('id');
        $("#frmActivarPreavis").modal({ backdrop: 'static', keyboard: false });
    }
});

//activar ejecutar
$("#btnActivarPreavis").click(function () {

    $.ajax({
        url: "/Preaviso/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador!',
            });
        }
        else {
            cargarGridPreaviso();
            $("#frmActivarPreavis").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;


});
//FUNCION: DETALLES DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

$(document).on("click", "#tblPreaviso tbody tr td #btnDetallePreaviso", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("Preaviso/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        IDInactivar = ID;
        $.ajax({
            url: "/Preaviso/Details/" + ID,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })

            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    $.each(data, function (i, iter) {

                        var FechaCrea = FechaFormato(data[0].prea_FechaCrea);
                        var FechaModifica = FechaFormato(data[0].prea_FechaModifica);
                        $("#Detalles #prea_IdPreaviso").html(iter.prea_IdPreaviso);
                        $("#Detalles #prea_RangoInicioMeses").html(iter.prea_RangoInicioMeses);
                        $("#Detalles #prea_RangoFinMeses").html(iter.prea_RangoFinMeses);
                        $("#Detalles #prea_DiasPreaviso").html(iter.prea_DiasPreaviso);
                        data[0].prea_UsuarioCrea == null ? $("#Detalles #tbUsuario_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                        $("#Detalles #prea_UsuarioCrea").html(iter.prea_UsuarioCrea);
                        $("#Detalles #prea_FechaCrea").html(FechaCrea);
                        data[0].prea_UsuarioModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                        $("#Detalles #prea_UsuarioModifica").html(data[0].prea_UsuarioModifica);
                        $("#Detalles #prea_FechaModifica").html(FechaModifica);
                    });
                    $("#DetallarPreaviso").modal({ backdrop: 'static', keyboard: false });


                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se pudo cargar la información, contacte al administrador',
                    });
                }
            });
    }
});


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

//*****************CREAR******************//

$("#IconCerrarCrear").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#CrearPreaviso").modal("hide");
});

//OCULTAR MODAL DE CREACION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#btnCerrarCrear").click(function () {
    $("#Crear #RangoInicio_Validation_descripcion").css("display", "none");
    $("#Crear #AsteriscoInicio").removeClass("text-danger");
    $("#Crear #Validation_descripcion1").css("display", "none");
    $("#Crear #AsteriscoFin").removeClass("text-danger");
    $("#Crear #Validation_descripcion2").css("display", "none");
    $("#Crear #AsteriscoDias").removeClass("text-danger");
    $("#CrearPreaviso #Validation_descripcion").css("display", "none");
    $("#Crear #validation_CantidadDiasMenorACero").css("display", "none");
    $("#Crear #validation_CantidadDiasRequerido").css("display", "none");
    $("#Crear #AsteriscoCantidadDia").removeClass("text-danger");
    $("#CrearPreaviso").modal("hide");
});

//INHABILITAR EL POSTBACK DEL FORMULARIO DE CREAR
$("#frmCreatePreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************EDITAR******************//

//FUNCION: OCULTAR MODAL DE EDICION CON EL ICONO DE CERRAR OCULTANDO EL DATAANNOTATION
$("#IconCerrarEditar").click(function () {
    $("#EditarPreaviso #Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});

//FUNCION: HABILITAR EL DATAANNOTATION Y DESPLEGAR EL MODAL
$("#btnCerrarEditar").click(function () {
    $("#Editar #RangoInicio_Validation_descripcion").css("display", "none");
    $("#Editar #RangoFin_Validation_descripcion").css("display", "none");
    $("#Editar #Dias_Validation_descripcion").css("display", "none");
    $("#EditarPreaviso").modal('hide');
});


//INHABILITAR EL POSTBACK DEL FORMULARIO DE EDITAR
$("#frmEditPreaviso").submit(function (event) {
    event.preventDefault();
});

//*****************INACTIVAR******************//

//MOSTRAR EL MODAL DE INACTIVAR
$(document).on("click", "#btnmodalInactivarPreaviso", function () {
    $("#DetallarPreaviso").modal('hide');
    $("#InactivarPreaviso").modal();
});

//Boton para cerrar el modal de Inactivar
$("#btnCerrarInactivacion").click(function () {
    $("#InactivarPreaviso").modal('hide');
});


// script serialize date
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