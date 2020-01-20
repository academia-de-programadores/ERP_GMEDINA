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
    ;

const btnEditar = $("#btnEditar"),
MontoInicial = $('#dex_MontoInicial'),
MontoRestante = $('#dex_MontoRestante'),
Observaciones = $('#dex_ObservacionesComentarios'),
Cuota = $('#dex_Cuota'),
asteriscMontoInicial = $('#asteriscoMontoInicial'),
asteriscMontoRestante = $('#asteriscoMontoRestante'),
asteriscObservaciones = $('#asteriscoObservaciones'),
asteriscCuota = $('#asteriscoCuota'),
validnEquipoEmpleado = $('#validEquipoEmpleado'),
validMontoInicial = $('#validMontoInicial'),
validMontoRestante = $('#validMontoRestante'),
validObservaciones = $('#validObservaciones'),
validCuota = $('#validCuota');

//#endregion

//
//Obtención de Script para Formateo de Fechas
//
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
    });

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

function mostrarCargandoCrear() {
    btnAgregar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear() {
    btnAgregar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

//#endregion

//Activar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnActivarDeduccionesExtraordinarias", function () {
    document.getElementById("btnActivarDeduccionesExtraordinarias").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('iddeduccionesextra');
    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#ActivarDeduccionesExtraordinarias").modal();
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


$(btnAgregar).click(function () {
    console.clear();
    if (validaciones(equipoEmpId,
        montoInicial,  
        montoRestante,
        observaciones,
        idDeduccion,
        cuota
    )) {
        console.log('Paso las validaciones');

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

function validaciones(equipoEmpId,
    montoInicial,
    montoRestante,
    observaciones,
    idDeduccion,
    cuota) {
    var todoBien = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);

    //Equipo Empleado
    if (equipoEmpId.val() != '') {
        asteriscoEquipoEmpleado.removeClass('text-danger');
        validacionEquipoEmpleado.hide();
    } else {
        asteriscoEquipoEmpleado.addClass('text-danger');
        validacionEquipoEmpleado.show();
        todoBien = false;
    }

    // Monto inicial
    if (montoInicial.val() != '' && expreg.test(montoInicial.val())) {
        asteriscoMontoInicial.removeClass('text-danger');
        validacionMontoInicial.hide();
    } else {
        asteriscoMontoInicial.addClass('text-danger');
        validacionMontoInicial.show();
        todoBien = false;
    }

    // Monto Restante
    if (montoRestante.val() != '' && expreg.test(montoRestante.val())) {
        asteriscoMontoRestante.removeClass('text-danger');
        validacionMontoRestante.hide();
    } else {
        asteriscoMontoRestante.addClass('text-danger');
        validacionMontoRestante.show();
        todoBien = false;
    }

    // Observaciones
    if (observaciones.val() != '') {
        validacionObservaciones.hide();
        asteriscoObservaciones.removeClass('text-danger');
    } else {
        asteriscoObservaciones.addClass('text-danger');
        validacionObservaciones.show();
        todoBien = false;
    }

    // Id deduccion
    if (idDeduccion.val() != '') {
        asteriscoIdDeducciones.removeClass('text-danger');
        validacionIdDeducciones.hide();
    } else {
        asteriscoIdDeducciones.addClass('text-danger');
        validacionIdDeducciones.show();
        todoBien = false;
    }

    // Cuota
    if (cuota.val() != '') {
        asteriscoCuota.removeClass('text-danger');
        validacionCuota.hide();
    } else {
        asteriscoCuota.addClass('text-danger');
        validacionCuota.show();
        todoBien = false;
    }
    return todoBien;
}




////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function mostrarCargandoEditar() {
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarCargandoEditar() {
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

//Editar
$(btnEditar).click(function () {
    console.clear();
    if (validacion(
        MontoInicial,
        MontoRestante,
        Observaciones,
        Cuota
    )) {
        console.log('Paso las validaciones');

        var data = $("#frmEditar").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/DeduccionesExtraordinarias/Edit",
            method: "POST",
            data: data
        }).done(function (data) {
            debugger;
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

function validacion(
    MontoInicial,
    MontoRestante,
    Observaciones,
    Cuota) {
    var todoCorrecto = true;
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);


    // Monto inicial
    if (MontoInicial.val() != '' && expreg.test(MontoInicial.val())) {
        asteriscMontoInicial.removeClass('text-danger');
        validMontoInicial.hide();
    } else {
        asteriscMontoInicial.addClass('text-danger');
        validMontoInicial.show();
        todoCorrecto = false;
    }

    // Monto Restante
    if (MontoRestante.val() != '' && expreg.test(MontoRestante.val())) {
        asteriscMontoRestante.removeClass('text-danger');
        validMontoRestante.hide();
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


//Modal de Inactivar
$(document).on("click", "#btnInactivarDeduccionesExtraordinarias", function () {
    document.getElementById("btnInactivar").disabled = false;
    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('iddeduccionextra');

    console.log(ID)

    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#InactivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

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

//Ocultar Modal de Create
$("#btnCerrarCreate").click(function () {
    $("#AgregarDeduccionesExtraordinarias").modal('hide');
});

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