//#region Declaracion de variables
//Validaciones de Botones de las Pantallas
const btnAgregar = $('#btnAgregar'),
    //Div que aparecera cuando se le de click en crear
    cargandoCrear = $('#cargandoCrear'),
    equipoEmpId = $('#eqemp_Id'),
    montoInicial = $('#dex_MontoInicial'),
    montoRestante = $('#dex_MontoRestante'),
    observaciones = $('#dex_ObservacionesComentarios'),
    idDeduccion = $('#cde_Id'),
    cuota = $('#dex_Cuota'),
    asteriscoEquipoEmpleado = $('#asteriscoEquipoEmpleado'),
    asteriscoMontoInicial = $('#asteriscoMontoInicial'),
    asteriscoMontoRestante = $('#asteriscoMontoRestante'),
    asteriscoObservaciones = $('#asteriscoObservaciones'),
    asteriscoIdDeducciones = $('#asteriscoIdDeducciones'),
    asteriscoCuota = $('#asteriscoCuota'),
    validacionEquipoEmpleado = $('#validacionEquipoEmpleado'),
    validacionMontoInicial = $('#validacionMontoInicial'),
    validacionMontoRestante = $('#validacionMontoRestante'),
    validacionObservaciones = $('#validacionObservaciones'),
    validacionIdDeducciones = $('#validacionIdDeducciones'),
    validacionCuota = $('#validacionCuota');

const btnEditar = $("#btnEditar"),
    MontoInicial = $('#dex_MontoInicial'),
    MontoRestante = $('#dex_MontoRestante'),
    Observaciones = $('#dex_ObservacionesComentarios'),
    Cuota = $('#dex_Cuota'),
    asteriscMontoInicial = $('#asteriscMontoInicial'),
    asteriscMontoRestante = $('#asteriscMontoRestante'),
    asteriscObservaciones = $('#asteriscObservaciones'),
    asteriscCuota = $('#asteriscCuota'),
    validnEquipoEmpleado = $('#validEquipoEmpleado'),
    validMontoInicial = $('#validMontoInicial'),
    validMontoRestante = $('#validMontoRestante'),
    validObservaciones = $('#validObservaciones'),
    validCuota = $('#validCuota'),
    MontoRestanteEditar = $('#MontoRestanteEditar');



//#endregion

//#region Obtención de Script para Formateo de Fechas
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
    });
//#endregion

//#region Blur

$('#eqemp_Id').blur(function () {
    let equipoEmpleadoId = $(this).val();
    if (equipoEmpleadoId == null || equipoEmpleadoId == "" || equipoEmpleadoId == 0 || equipoEmpleadoId == "0") {
        $("#validacionEquipoEmpleado").html('Campo Equipo Empleado Requerido');
        $("#validacionEquipoEmpleado").show();
        $("#asteriscoEquipoEmpleado").addClass('text-danger');

    } else {
        $("#validacionEquipoEmpleado").html('');
        $("#validacionEquipoEmpleado").hide();
        $("#asteriscoEquipoEmpleado").removeClass('text-danger');
    }
});

$('#dex_MontoInicial').blur(function () {
    let montoInicial = $(this).val();
    let hayAlgo = false;

    if (montoInicial == "" || montoInicial == null || montoInicial == undefined) {
        $("#valMontoInicial").html('Campo Monto Inicial Requerido');
        $("#valMontoInicial").show();
        $("#asteriscoMontoInicial").addClass('text-danger');
    } else {
        hayAlgo = true;
        $("#valMontoInicial").html('');
        $("#valMontoInicial").hide();
        $("#asteriscoMontoInicial").removeClass('text-danger');
    }

    if (hayAlgo)
        if (montoInicial == 0.00 || montoInicial < 0) {
            $("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
            $("#valMontoInicial").show();
            $("#asteriscoMontoInicial").addClass('text-danger');
        }
        else {
            $("#valMontoInicial").html('');
            $("#valMontoInicial").hide();
            $("#asteriscoMontoInicial").removeClass('text-danger');
        }
});

$('#dex_MontoRestante').blur(function () {
    let montoRestante = $(this).val().replace(/,/, '');
    let hayAlgo = false;
    let montoInicial = $('#dex_MontoInicial').val().replace(/,/, '');

    if (montoRestante == "" || montoRestante == null || montoRestante == undefined) {
        $("#valMontoRestante").html('Campo Monto Restante Requerido');
        $("#valMontoRestante").show();
        $("#asteriscoMontoRestante").addClass('text-danger');
    } else {
        hayAlgo = true
        $("#valMontoRestante").html('');
        $("#valMontoRestante").hide();
        $("#asteriscoMontoRestante").removeClass('text-danger');
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (montoRestante == 0.00 || montoRestante < 0) {
            $("#valMontoRestante").html('El campo Monto Restante no puede ser menor o igual que cero.');
            $("#valMontoRestante").show();
            $("#asteriscoMontoRestante").addClass('text-danger');
        }
        else {
            esMayorCero = true;
            $("#valMontoRestante").html('');
            $("#valMontoRestante").hide()
            $("#asteriscoMontoRestante").removeClass('text-danger');
        }

        let floatMontoRestante = parseFloat(montoRestante);
        let floatMontoInicial = parseFloat(montoInicial)

        if (esMayorCero)
            if (floatMontoRestante > floatMontoInicial) {
                $("#valMontoRestante").html('El campo Monto Restante no puede ser mayor que Monto Inicial.');
                $("#valMontoRestante").show();
                $("#asteriscoMontoRestante").addClass('text-danger');
                hayAlgo = false;
            }
            else {
                hayAlgo = false;
                $("#valMontoRestante").html('');
                $("#valMontoRestante").hide()
                $("#asteriscoMontoRestante").removeClass('text-danger');
            }

    }
});

$('#dex_ObservacionesComentarios').blur(function () {
    let observaciones = $(this)
        .val()
        .trim();
    if (
        observaciones == ""
    ) {
        $('#validacionObservaciones').html('Campo Observaciones Requerido');
        $('#validacionObservaciones').show();
        $('#asteriscoObservaciones').addClass('text-danger');
    } else {
        $('#validacionObservaciones').hide();
        $('#asteriscoObservaciones').removeClass('text-danger');
    }
});

$('#cde_Id').blur(function () {
    let deduccion = $(this).val();
    if (deduccion == null || deduccion == "" || deduccion == 0 || deduccion == "0") {
        $("#validacionIdDeducciones").html('Campo Deducción Requerido');
        $("#validacionIdDeducciones").show();
        $("#asteriscoIdDeducciones").addClass('text-danger');

    } else {
        $("#validacionIdDeducciones").html('');
        $("#validacionIdDeducciones").hide();
        $("#asteriscoIdDeducciones").removeClass('text-danger');
    }
});

$('#dex_Cuota').blur(function () {
    let cuota = $(this).val().replace(/,/, '');
    let hayAlgo = false;
    let montoInicial = $('#dex_MontoInicial').val().replace(/,/, '');

    if (cuota == null || cuota == "") {
        $("#valCuota").html('Campo Cuota Requerido');
        $("#valCuota").show();
        $("#asteriscoCuota").addClass('text-danger');

    } else {
        hayAlgo = true;
        $("#valCuota").html('');
        $("#valCuota").hide();
        $("#asteriscoCuota").removeClass('text-danger');
    }

    if (hayAlgo) {
        let esMayorCero = false;
        if (cuota == 0.00 || cuota < 0) {
            $("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
            $("#valCuota").show();
            $("#asteriscoCuota").addClass('text-danger');
        }
        else {
            esMayorCero = true;
            $("#valCuota").html('');
            $("#valCuota").hide()
            $("#asteriscoCuota").removeClass('text-danger');
        }

        let cuotaFloat = parseFloat(cuota);
        let floatMontoInicial = parseFloat(montoInicial)

        if (esMayorCero)
            if (cuotaFloat > floatMontoInicial) {
                $("#valCuota").html('El campo Cuota no puede ser mayor que Monto Inicial.');
                $("#valCuota").show();
                $("#asteriscoCuota").addClass('text-danger');
                hayAlgo = false;
            }
            else {
                hayAlgo = false;
                $("#valCuota").html('');
                $("#valCuota").hide()
                $("#asteriscoCuota").removeClass('text-danger');
            }

    }
});

//#endregion

//#region Funciones
//Funció Genérica para utilizar Ajax
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

//Función: Cargar y Actualizar la Data del Index
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/DeduccionesExtraordinarias/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {

                //Validar si se genera un error al cargar de nuevo el Index
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }

            //Variable para guardar la data obtenida
            var ListaDeduccionesExtraordinarias = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblDeduccionesExtraordinarias').DataTable().clear();

            //Recorrer la data obtenida a traves de la función anterior y se crea un Template de la Tabla para Actualizarse
            for (var i = 0; i < ListaDeduccionesExtraordinarias.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? '<a type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Detalles</a>' : '';

                //variable boton editar
                var botonEditar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? '<a type="button" style="margin-right:3px;" class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Edit?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Editar</a>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarDeduccionesExtraordinarias" iddeduccionesextra="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Activar</button>' : '' : '';

                //variable boton inactivar
                var botonInactivar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? esAdministrador == "1" ? '<button type="button" name="iddeduccionesextraordinarias" class="btn btn-danger btn-xs" id="btnInactivarDeduccionesExtraordinarias" iddeduccionextra="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Inactivar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblDeduccionesExtraordinarias').dataTable().fnAddData([
                    ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra,
                    ListaDeduccionesExtraordinarias[i].per_Nombres + ' ' + ListaDeduccionesExtraordinarias[i].per_Apellidos,
                    ListaDeduccionesExtraordinarias[i].dex_MontoInicial,
                    ListaDeduccionesExtraordinarias[i].dex_MontoRestante,
                    ListaDeduccionesExtraordinarias[i].dex_ObservacionesComentarios,
                    ListaDeduccionesExtraordinarias[i].dex_Cuota,
                    ListaDeduccionesExtraordinarias[i].cde_DescripcionDeduccion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonInactivar + botonActivar
                ]);
            }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
}

function validaciones(equipoEmpId,
    montoInicial,
    montoRestante,
    observaciones,
    idDeduccion,
    cuota) {
    var todoBien = true;
    let equipoEmpleadoId = equipoEmpId.val();

    //Equipo Empleado
    if (equipoEmpleadoId == null || equipoEmpleadoId == "" || equipoEmpleadoId == 0 || equipoEmpleadoId == "0") {
        asteriscoEquipoEmpleado.removeClass('text-danger');
        validacionEquipoEmpleado.hide();
        asteriscoEquipoEmpleado.addClass('text-danger');
        validacionEquipoEmpleado.show();
        todoBien = false;
    } else {
        asteriscoEquipoEmpleado.removeClass('text-danger');
        validacionEquipoEmpleado.hide();
    }

    montoInicial = montoInicial.val();
    let hayAlgoMontoInicial = false;
    // Monto inicial
    if (montoInicial == "" || montoInicial == null || montoInicial == undefined) {
        $("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
        $("#valMontoInicial").show();
        $("#asteriscoMontoInicial").addClass('text-danger');
        todoBien = false;
    } else {
        asteriscoMontoInicial.removeClass('text-danger');
        validacionMontoInicial.hide();
        hayAlgoMontoInicial = true;
    }

    if (hayAlgoMontoInicial) {
        if (montoInicial == 0.00 || montoInicial < 0) {
            $("#valMontoInicial").html('Campo Monto Inicial no puede ser menor o igual que cero.');
            $("#valMontoInicial").show();
            $("#asteriscoMontoInicial").addClass('text-danger');
            todoBien = false;
        }
        else {
            $("#valMontoInicial").html('');
            $("#valMontoInicial").hide();
            $("#asteriscoMontoInicial").removeClass('text-danger');
        }
    }
    let hayAlgoMontoRestante = false;
    // Monto Restante
    if (montoRestante.val() != '') {
        $("#valMontoRestante").html('');
        $("#valMontoRestante").hide();
        hayAlgoMontoRestante = true
        $("#asteriscoMontoRestante").removeClass('text-danger');
    } else {
        $("#valMontoRestante").html('Campo Monto Restante Requerido');
        $("#valMontoRestante").show();
        $("#asteriscoMontoRestante").addClass('text-danger');
        todoBien = false;

        if (hayAlgoMontoRestante)
            if (montoRestante.val() > montoInicial.val()) {
                $("#valMontoRestante").html('El campo Monto Restante no puede ser mayor que Monto Inicial.');
                $("#valMontoRestante").show();
                $("#asteriscoMontoRestante").addClass('text-danger')
                todoBien = false;
            }
            else {
                $("#valMontoRestante").html('');
                $("#valMontoRestante").hide();
                $("#asteriscoMontoRestante").removeClass('text-danger');
            }
    }

    // Observaciones
    if (observaciones.val().trim() != '') {
        $('#validacionObservaciones').hide();
        $('#asteriscoObservaciones').removeClass('text-danger');
    } else {
        $('#validacionObservaciones').html('Campo Observaciones Requerido');
        $('#validacionObservaciones').show();
        $('#asteriscoObservaciones').addClass('text-danger');
        todoBien = false;
    }

    // Id deduccion
    if (idDeduccion.val() != '') {
        $("#validacionIdDeducciones").html('');
        $("#validacionIdDeducciones").hide();
        $("#asteriscoIdDeducciones").removeClass('text-danger');
    } else {
        $("#validacionIdDeducciones").html('Campo Deducción Requerido');
        $("#validacionIdDeducciones").show();
        $("#asteriscoIdDeducciones").addClass('text-danger');
        todoBien = false;
    }

    let hayAlgoCuota = false;
    // Cuota
    if (cuota.val() != '') {
        hayAlgoCuota = true;
        $("#valCuota").html('');
        $("#valCuota").hide()
        $("#asteriscoCuota").removeClass('text-danger');
    } else {
        $("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
        $("#valCuota").show();
        $("#asteriscoCuota").addClass('text-danger');
        todoBien = false;
    }

    if (hayAlgoCuota) {
        let esMayorCero = false;
        if (cuota.val() == 0.00 || cuota.val() < 0) {
            $("#valCuota").html('El campo Cuota no puede ser menor o igual que cero.');
            $("#valCuota").show();
            $("#asteriscoCuota").addClass('text-danger');
            todoBien = false;
        }
        else {
            esMayorCero = true;
            $("#valCuota").html('');
            $("#valCuota").hide()
            $("#asteriscoCuota").removeClass('text-danger');
        }

        let cuotaFloat = parseFloat(cuota.val());
        let floatMontoInicial = parseFloat(montoInicial);

        if (esMayorCero)
            if (cuotaFloat > floatMontoInicial) {
                $("#valCuota").html('El campo Cuota no puede ser mayor que Monto Inicial.');
                $("#valCuota").show();
                $("#asteriscoCuota").addClass('text-danger');
                todoBien = false;
                hayAlgo = false;
            }
            else {
                hayAlgo = false;
                $("#valCuota").html('');
                $("#valCuota").hide()
                $("#asteriscoCuota").removeClass('text-danger');
            }
    }
    return todoBien;
}

function validacionEditar(
    MontoInicial,
    MontoRestante,
    Observaciones,
    Cuota) {
    var todoCorrecto = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);


    // Monto inicial
    if (montoInicial.val() != '' && expreg.test(montoInicial.val())) {
        asteriscMontoInicial.removeClass('text-danger');
        validMontoInicial.hide();

        if (montoInicial.val() != 0 || montoInicial.val() != 0.00) {
            asteriscMontoInicial.removeClass('text-danger');
            validMontoInicial.hide();
        }
        else {
            asteriscMontoInicial.addClass('text-danger');
            validMontoInicial.show();
            todoBien = false;
        }
    } else {
        asteriscMontoInicial.addClass('text-danger');
        validMontoInicial.show();
        todoBien = false;
    }

    // Monto Restante

    if (MontoRestante.val() != '' && expreg.test(MontoRestante.val())) {
        asteriscMontoRestante.removeClass('text-danger');
        validMontoRestante.hide();
        if (MontoRestante.val() < MontoInicial.val()) {
            asteriscMontoRestante.removeClass('text-danger');
            MontoRestanteEditar.hide();
        }
        else {
            asteriscMontoRestante.addClass('text-danger');
            MontoRestanteEditar.show();
            todoCorrecto = false;
        }

    } else {
        asteriscMontoRestante.addClass('text-danger');
        validMontoRestante.show();
        todoCorrecto = false;

    }



    // Observaciones
    if (Observaciones.val() != '') {
        validObservaciones.hide();
        asteriscObservaciones.removeClass('text-danger');
    } else {
        asteriscObservaciones.addClass('text-danger');
        validObservaciones.show();
        todoCorrecto = false;
    }

    // Cuota
    if (Cuota.val() != '') {
        asteriscCuota.removeClass('text-danger');
        validCuota.hide();
    } else {
        asteriscCuota.addClass('text-danger');
        validCuota.show();
        todoCorrecto = false;
    }
    return todoCorrecto;
}

//#endregion

//#region Activar
//Activar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnActivarDeduccionesExtraordinarias", function () {
    document.getElementById("btnActivarDeduccionesExtraordinarias").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('iddeduccionesextra');
    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#ActivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });
});

//Activar
$("#btnActivarRegistroDeduccionesExtraordinarias").click(function () {
    document.getElementById("btnActivarRegistroDeduccionesExtraordinarias").disabled = true;
    let ID = localStorage.getItem('id')
    $.ajax({
        url: "/DeduccionesExtraordinarias/Activar",
        method: "POST",
        data: { id: ID }
    }).done(function (data) {
        $("#ActivarDeduccionesExtraordinarias").modal('hide');
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

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#ActivarDeduccionesExtraordinarias").submit(function (e) {
        return false;
    });

});
//#endregion

//#region Create

//Ocultar Modal de Create
$("#btnCerrarCreate").click(function () {
    $("#AgregarDeduccionesExtraordinarias").modal('hide');
});


$(btnAgregar).click(function () {
    if (validaciones(equipoEmpId,
        montoInicial,
        montoRestante,
        observaciones,
        idDeduccion,
        cuota
    )) {
        var data = $("#frmCreate").serializeArray();
        
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionesExtraordinarias/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                document.getElementById("btnAgregar").disabled = true;
                window.location.href = '/DeduccionesExtraordinarias/Index';
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
    $("#frmCreate").submit(function (e) {
        return false;
    });
    document.getElementById("btnAgregar").disabled = false;
});
//#endregion

//#region Editar
//Editar
$(btnEditar).click(function () {

    if (validacionEditar(
        MontoInicial,
        MontoRestante,
        Observaciones,
        Cuota
    )) {
        var data = $("#frmEditar").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionesExtraordinarias/Edit",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "Exito") {
                document.getElementById("btnEditar").disabled = true;
                window.location.href = '/DeduccionesExtraordinarias/Index';
                // Mensaje de exito cuando un registro se ha guardado bien
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

    }
    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEdit").submit(function (e) {
        return false;
    });
    document.getElementById("btnEditar").disabled = false;
});
//#endregion

//#region Inactivar
//Modal de Inactivar
$(document).on("click", "#btnInactivarDeduccionesExtraordinarias", function () {
    document.getElementById("btnInactivar").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('iddeduccionextra');

    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#InactivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });

});

//Funcionamiento del Modal Inactivar
$("#btnInactivar").click(function () {
    document.getElementById("btnInactivar").disabled = true;
    let ID = localStorage.getItem('id')
    //Se envia el Formato Json al Controlador para realizar la Inactivación
    $.ajax({
        url: "/DeduccionesExtraordinarias/Inactivar",
        method: "POST",
        data: { id: ID }
    }).done(function (data) {
        if (data == "Error") {
            //Cuando trae un error en el BackEnd al realizar la Inactivación
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            // Actualizar el Index para ver los cambios
            $("#InactivarDeduccionesExtraordinarias").modal('hide');
            cargarGridDeducciones();
            //Mensaje de Éxito de la Inactivación
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#InactivarDeduccionesExtraordinarias").submit(function (e) {
        return false;
    });


});
//#endregion

//#region Otras
//Ocultar Modal de Details
$("#btnCerrarDetails").click(function () {
    $("#DetailsDeduccionesExtraordinarias").modal('hide');
});

//Ocultar Modal de Edit
$("#btnCerrarEdit").click(function () {
    $("#EditarDeduccionesExtraordinarias").modal('hide');
});

//Ocultar Modal de Inactivar
$("#btnCerrarInactivar").click(function () {
    $("#InactivarDeduccionesExtraordinarias").modal('hide');
});
//#endregion