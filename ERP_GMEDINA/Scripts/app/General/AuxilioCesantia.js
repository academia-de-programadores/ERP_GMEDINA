var EliminarID = 0;

//cargar grid
function cargarGridAuxilioCesantia() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AuxilioDeCesantias/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                // no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //almacenar data
            var ListaAuxCes = data;

            //limpiar datatable
            $('#tblAuxCesantia').DataTable().clear();

            for (var i = 0; i < ListaAuxCes.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaAuxCes[i].aces_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnModalDetalles">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaAuxCes[i].aces_Activo == true ? '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-default btn-xs"  id="btnModalEdit">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAuxCes[i].aces_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAuxCes[i].aces_IdAuxilioCesantia + '" type="button" class="btn btn-default btn-xs"  id="btnModalActivarAuxCes">Activar</button>' : '' : '';

                //agregar fila al datatable
                $('#tblAuxCesantia').dataTable().fnAddData([
                    ListaAuxCes[i].aces_IdAuxilioCesantia,
                    ListaAuxCes[i].aces_RangoInicioMeses,
                    ListaAuxCes[i].aces_RangoFinMeses,
                    ListaAuxCes[i].aces_DiasAuxilioCesantia,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

// create 1
$(document).on("click", "#btnModalCrear", function () {
    //validar informacion del usuario

    var validacionPermiso = userModelState("AuxilioDeCesantias/Create");

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

        // mesanje rango final no puede ser menor a rango inicial
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');

        // mesanje rango final no puede ser menor a rango inicial
        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');

        // * rango final 
        $('#AsteriscoCantidadDia').removeClass('text-danger');

        // vaciar cajas de texto
        $('#Crear input[type=text], input[type=number]').val('');

        // habilitar boton 
        $('#btnCrearAuxCes').attr('disabled', false);

        // mostrar modal
        $("#frmCrearAuxCes").modal({ backdrop: 'static', keyboard: false });
    }

});

// validaciones key up create

// validar rango inicio create
$('#Crear #aces_RangoInicioMeses').keyup(function () {

    var rangoFin = $("#Crear #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #aces_RangoInicioMeses").val().replace(/,/g, '');

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #aces_RangoFinMeses").val().trim() == '' || $("#Crear #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoInicio) >= 0 || $("#Crear #aces_RangoInicioMeses").val().trim() == '') {

        $('#AsteriscoRangoInicio').removeClass('text-danger');
        $("#Crear #validation_RangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoRangoInicio').addClass("text-danger");
        $("#Crear #validation_RangoInicioRequerido").css('display', 'none');
        $("#Crear #validation_RangoInicioMenorACero").css('display', '');
    }

    // requerido
    if ($("#Crear #aces_RangoInicioMeses").val().trim() != '') {

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
$('#Crear #aces_RangoFinMeses').keyup(function () {

    var rangoFin = $("#Crear #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #aces_RangoInicioMeses").val().replace(/,/g, '');


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
    if (parseInt(rangoFin) >= 0 || $("#Crear #aces_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Crear #aces_RangoFinMeses").val().trim() != '') {
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
    if ($("#Crear #aces_RangoFinMeses").val().trim() != '') {

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
$('#Crear #aces_DiasAuxilioCesantia').keyup(function () {

    var cantidadDias = $("#Crear #aces_DiasAuxilioCesantia").val().replace(/,/g, '');

    // si es menor o igual que cero
    if (parseInt(cantidadDias) >= 0 || $("#Crear #aces_DiasAuxilioCesantia").val().trim() == '') {

        $('#AsteriscoCantidadDia').removeClass('text-danger');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#AsteriscoCantidadDia').addClass("text-danger");
        $("#Crear #validation_CantidadDiasRequerido").css('display', 'none');
        $("#Crear #validation_CantidadDiasMenorACero").css('display', '');
    }

    // requerido
    if ($("#Crear #aces_DiasAuxilioCesantia").val().trim() != '') {

        if (parseInt($("#Crear #aces_DiasAuxilioCesantia").val()) >= 0) {
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
$('#btnCrearAuxCes').click(function () {

    // deshabilitar boton
    $('#btnCrearAuxCes').attr('disabled', true);
    var modalState = true;
    var rangoFin = $("#Crear #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Crear #aces_RangoInicioMeses").val().replace(/,/g, '');
    var cantidaDiasPreaviso = $("#Crear #aces_DiasAuxilioCesantia").val().replace(/,/g, '');

    // --- validaciones rango inicio ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #aces_RangoFinMeses").val().trim() == '' || $("#Crear #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoInicio) >= 0 || $("#Crear #aces_RangoInicioMeses").val().trim() == '') {

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
    if ($("#Crear #aces_RangoInicioMeses").val().trim() != '') {

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
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Crear #aces_RangoFinMeses").val().trim() == '' || $("#Crear #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoFin) >= 0 || $("#Crear #aces_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Crear #aces_RangoFinMeses").val().trim() != '') {
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
    if ($("#Crear #aces_RangoFinMeses").val().trim() != '') {

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
    if (parseInt($("#Crear #aces_DiasAuxilioCesantia").val()) >= 0 || $("#Crear #aces_DiasAuxilioCesantia").val().trim() == '') {

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
    if ($("#Crear #aces_DiasAuxilioCesantia").val().trim() != '') {

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

    if (modalState == true) {

        var data = $("#frmCrearAuxCess").serializeArray();

        var indiceRangoInicio = data[0].value;
        data[0].value = indiceRangoInicio.replace(/,/g, '');

        var indiceRangoFin = data[1].value;
        data[1].value = indiceRangoFin.replace(/,/g, '');

        var indiceCantidadDias = data[2].value;
        data[2].value = indiceCantidadDias.replace(/,/g, '');


        $.ajax({
            url: "/AuxilioDeCesantias/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data == "error") {

                $('#btnCrearAuxCes').attr('disabled', false);

                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
            else {
                $("#frmCrearAuxCes").modal('hide');
                cargarGridAuxilioCesantia();

                iziToast.success({
                    title: 'Exito',
                    message: 'El registro se agregó de forma exitosa!',
                });
            }

        });
    }
    else {

        $('#btnCrearAuxCes').attr('disabled', false);
    }
});


// cerrar modal create
$("#btnCerrarCrearAuxCes").click(function () {
    $("#frmCrearAuxCes").modal('hide');
});

// cerrar modal editar
$("#closebutton").click(function () {
    $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });
});

// mostrar modal editar
$("#closebutton2").click(function () {
    $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });
});

// detalles auxilio cesantias
$(document).on("click", "#tblAuxCesantia tbody tr td #btnModalDetalles", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AuxilioDeCesantias/Details");
    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');

        $.ajax({
            url: "/AuxilioDeCesantias/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                if (data) {
                    var FechaCrea = FechaFormato(data[0].aces_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].aces_FechaModifica);
                    $("#aces_IdAuxilioCesantia").html(data[0].aces_IdAuxilioCesantia);
                    $("#frmDetallesAuxCess #aces_RangoInicioMeses").html(data[0].aces_RangoInicioMeses);
                    $("#frmDetallesAuxCess #aces_RangoFinMeses").html(data[0].aces_RangoFinMeses);
                    $("#frmDetallesAuxCess #aces_DiasAuxilioCesantia").html(data[0].aces_DiasAuxilioCesantia);
                    $("#tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#aces_FechaCrea").html(FechaCrea);
                    data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#aces_UsuarioModifica").val(data[0].aces_UsuarioModifica);
                    $("#aces_FechaModifica").html(FechaModifica);
                    $("#frmDetailAuxCes").modal({ backdrop: 'static', keyboard: false });
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



//editar 1 modal
$(document).on("click", "#tblAuxCesantia tbody tr td #btnModalEdit", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AuxilioDeCesantias/Edit");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');

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
        $('#btnUpdateAuxCes').attr('disabled', false);

        EliminarID = ID;
        $.ajax({
            url: "/AuxilioDeCesantias/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {

                if (data != 'error') {
                    var FechaModifica = FechaFormato(data.aces_FechaModifica);
                    $("#frmEditarAuxCes #aces_IdAuxilioCesantia").val(data.aces_IdAuxilioCesantia);
                    $("#frmEditarAuxCes #aces_RangoInicioMeses").val(data.aces_RangoInicioMeses);
                    $("#frmEditarAuxCes #aces_RangoFinMeses").val(data.aces_RangoFinMeses);
                    $("#frmEditarAuxCes #aces_DiasAuxilioCesantia").val(data.aces_DiasAuxilioCesantia);
                    $("#aces_UsuarioModifica").val(data.aces_UsuarioModifica);
                    $("#aces_FechaModifica").val(FechaModifica);
                    $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });
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
$('#Editar #aces_RangoInicioMeses').keyup(function () {

    var rangoFin = $("#Editar #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #aces_RangoInicioMeses").val().replace(/,/g, '');

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #aces_RangoFinMeses").val().trim() == '' || $("#Editar #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoInicio) >= 0 || $("#Editar #aces_RangoInicioMeses").val().trim() == '') {

        $('#EditAsteriscoRangoInicio').removeClass('text-danger');
        $("#Editar #validation_RangoInicioMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoRangoInicio').addClass("text-danger");
        $("#Editar #validation_EditRangoInicioRequerido").css('display', 'none');
        $("#Editar #validation_EditRangoInicioMenorACero").css('display', '');
    }

    // requerido
    if ($("#Editar #aces_RangoInicioMeses").val().trim() != '') {

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
$('#Editar #aces_RangoFinMeses').keyup(function () {

    var rangoFin = $("#Editar #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #aces_RangoInicioMeses").val().replace(/,/g, '');


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
    if (parseInt(rangoFin) >= 0 || $("#Editar #aces_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Editar #aces_RangoFinMeses").val().trim() != '') {
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
    if ($("#Editar #aces_RangoFinMeses").val().trim() != '') {

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
$('#Editar #aces_DiasAuxilioCesantia').keyup(function () {

    var cantidadDias = $("#Editar #aces_DiasAuxilioCesantia").val().replace(/,/g, '');

    // si es menor o igual que cero
    if (parseInt(cantidadDias) >= 0 || $("#Editar #aces_DiasAuxilioCesantia").val().trim() == '') {

        $('#EditAsteriscoCantidadDia').removeClass('text-danger');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', 'none');
    }
    else {
        $('#EditAsteriscoCantidadDia').addClass("text-danger");
        $("#Editar #validation_EditCantidadDiasRequerido").css('display', 'none');
        $("#Editar #validation_EditCantidadDiasMenorACero").css('display', '');
    }

    // requerido
    if ($("#Editar #aces_DiasAuxilioCesantia").val().trim() != '') {

        if (parseInt($("#Editar #aces_DiasAuxilioCesantia").val()) >= 0) {
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



$("#btnUpdateAuxCes").click(function () {
    $("#btnConfirmarEditar").attr('disabled', false);
    $("#btnUpdateAuxCes").attr('disabled', true);

    var modalState = true;
    var rangoFin = $("#Editar #aces_RangoFinMeses").val().replace(/,/g, '');
    var rangoInicio = $("#Editar #aces_RangoInicioMeses").val().replace(/,/g, '');
    var cantidaDiasPreaviso = $("#Editar #aces_DiasAuxilioCesantia").val().replace(/,/g, '');

    // --- validaciones rango inicio ---

    // si rango fin es mayor que rango inicio
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #aces_RangoFinMeses").val().trim() == '' || $("#Editar #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoInicio) >= 0 || $("#Editar #aces_RangoInicioMeses").val().trim() == '') {

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
    if ($("#Editar #aces_RangoInicioMeses").val().trim() != '') {

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
    if (parseInt(rangoFin) > parseInt(rangoInicio) || $("#Editar #aces_RangoFinMeses").val().trim() == '' || $("#Editar #aces_RangoInicioMeses").val().trim() == '') {

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
    if (parseInt(rangoFin) >= 0 || $("#Editar #aces_RangoFinMeses").val().trim() == '') {

        if (parseInt(rangoFin) > parseInt(rangoInicio) && $("#Editar #aces_RangoFinMeses").val().trim() != '') {
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
    if ($("#Editar #aces_RangoFinMeses").val().trim() != '') {

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
    if (parseInt(cantidaDiasPreaviso) >= 0 || $("#Editar #aces_DiasAuxilioCesantia").val().trim() == '') {

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
    if ($("#Editar #aces_DiasAuxilioCesantia").val().trim() != '') {

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

        $("#frmEditarAuxCes").modal('hide');
        $("#btnUpdateAuxCes").attr('disabled', true);
        $("#ConfirmarEdicion").modal({ backdrop: 'static', keyboard: false });
    }
    else {
        $("#btnUpdateAuxCes").attr('disabled', false);
    }

});

// editar 3 ejecutar
$("#btnConfirmarEditar").click(function () {

    $("#btnConfirmarEditar").attr('disabled', true);

    var data = $("#frmEditarAuxCesan").serializeArray();


    var indiceRangoInicio = data[3].value;
    data[3].value = indiceRangoInicio.replace(/,/g, '');

    var indiceRangoFin = data[4].value;
    data[4].value = indiceRangoFin.replace(/,/g, '');

    var indiceCantidadDias = data[5].value;
    data[5].value = indiceCantidadDias.replace(/,/g, '');


    $.ajax({
        url: "/AuxilioDeCesantias/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
            $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });
            $("#ConfirmarEdicion").modal('hide');
        }
        else {
            $("#ConfirmarEdicion").modal('hide');
            $("#frmEditarAuxCes").modal('hide');
            cargarGridAuxilioCesantia();

            iziToast.success({
                title: 'Exito',
                message: 'El registro se editó de forma exitosa!',
            });
        }
    });
});

// no confirmar edicion
$("#btnCerrarConfirmarEditar").click(function () {
    //Deshabilitar boton Editar
    $("#btnUpdateAuxCes").attr('disabled', false);

    // cerrar modal confirmacion
    $("#ConfirmarEdicion").modal('hide');

    // mostrar modal edición
    $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });

});

// no inactivar
$("#btnCerrarInactivar").click(function () {

    $("#frmEditarAuxCes").modal({ backdrop: 'static', keyboard: false });


});

// modal confirmar inactivar 
$("#btnModalEliminar").click(function () {

    //validar informacion del usuario
    var validacionPermiso = userModelState("AuxilioDeCesantias/Inactivar");

    if (validacionPermiso.status == true) {
        $('#btnEliminarAuxCes').attr('disabled', false);
        $("#frmEditarAuxCes").modal('hide');
        $("#frmEliminarAuxCes").modal({ backdrop: 'static', keyboard: false });
    }

});

// ejecutar inactivar
$("#btnEliminarAuxCes").click(function () {
    $("#btnEliminarAuxCes").attr('disabled', true);
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEliminarAuxCes").serializeArray();
    var ID = EliminarID;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AuxilioDeCesantias/Inactivar/" + ID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            $("#frmEliminarAuxCes").modal('hide');
            $("#frmEditarAuxCes").modal('hide');
            cargarGridAuxilioCesantia();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro se inactivó de forma exitosa!',
            });
        }
    });
});


// activar
var activarID = 0;

// modal confirmar activar
$(document).on("click", "#btnModalActivarAuxCes", function () {
    //validar informacion del usuario
    var validacionPermiso = userModelState("AuxilioDeCesantias/Activar");

    if (validacionPermiso.status == true) {
        activarID = $(this).data('id');
        $("#frmActivarAuxCes").modal({ backdrop: 'static', keyboard: false });
        $("#btnActivarAuxCes").attr('disabled', false);
    }


});

//activar ejecutar
$("#btnActivarAuxCes").click(function () {
    $("#btnActivarAuxCes").attr('disabled', true);
    $.ajax({
        url: "/AuxilioDeCesantias/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se logró activar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridAuxilioCesantia();

            $("#frmActivarAuxCes").modal('hide');

            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});

//funcion generica ajax
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

// obtener script serialize date
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {

    })
    .fail(function (jqxhr, settings, exception) {

    });

//evitar postback
$("#frmCrearAuxCes").submit(function (e) {
    return false;
});