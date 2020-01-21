const btnAgregar = $('#btnAgregarInstFin'),
    DescInstFin = $('#insf_DescInstitucionFinanc'),
    Contac = $('#insf_Contacto'),
    Telef = $('#phone'),
    Corre = $('#insf_Correo'),
    AsteriscoDescripcion = $('#AsteriscoDescripcion'),
    AsteriscoContacto = $('#AsteriscoContacto'),
    AsteriscoTelefono = $('#AsteriscoTelefono'),
    AsteriscoCorreo = $('#AsteriscoCorreo'),
    validacionDescripcion = $('#validacionDescripcion'),
    validacionContacto = $('#validacionContacto'),
    validacionTelefono = $('#validacionTelefono'),
    validacionCorreo = $('#validacionCorreo'),
    validatel = $('#validatel')
;

function Validaciones(
    DescInstFin,
    Contac,
    Telef,
    Corre) {
    debugger;
    var ExpregPhone = new RegExp(/^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$/);
    var ExpregEmail = new RegExp(/^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/);
    var todoBien = true;

    // Descripción Institución Financiera
    if (DescInstFin.val() != '') {
        AsteriscoDescripcion.removeClass('text-danger');
        validacionDescripcion.hide();
    } else {
        AsteriscoDescripcion.addClass('text-danger');
        validacionDescripcion.show();
        todoBien = false;
    }

    // Contacto
    if (Contac.val() != '') {
        AsteriscoContacto.removeClass('text-danger');
        validacionContacto.hide();
        validatel.hide();
    } else {
        AsteriscoContacto.addClass('text-danger');
        validacionContacto.show();
        todoBien = false;
    }

    // Telefono
    if (Telef.val() != '' && ExpregPhone.test(Telef.val())) {
        AsteriscoTelefono.removeClass('text-danger');
        validacionTelefono.hide();
    } else {
        AsteriscoTelefono.addClass('text-danger');
        validacionTelefono.show();
        todoBien = false;
    }

    // Correo
    if (Corre.val() != '' && ExpregEmail.test(Corre.val())) {
        AsteriscoCorreo.removeClass('text-danger');
        validacionCorreo.hide();
    } else {
        AsteriscoCorreo.addClass('text-danger');
        validacionCorreo.show();
        todoBien = false;
    }

    return todoBien;
}

//////////////////////////////////////////////////////////////////
const btnEditar = $('#btnEditarInstFin');
const btnEditarConfirmar = $('#btnModalActualizarINFS'),
    DescripInstFin = $('#insf_DescInstitucionFinanc'),
    Contact = $('#insf_Contacto'),
    Tel = $('#phone'),
    Cor = $('#insf_Correo'),
    AsteriscDescrip = $('#AsteriscDescrip'),
    AsteriscContact = $('#AsteriscContact'),
    AsteriscTel = $('#AsteriscTel'),
    AsteriscCorre = $('#AsteriscCorre'),
    validaDescripcion = $('#validaDescripcion'),
    validaContacto = $('#validaContacto'),
    validaTelefono = $('#validaTelefono'),
    validaCorreo = $('#validaCorreo')
;

function Validacion(
    DescripInstFin,
    Contact,
    Tel,
    Cor) {

    var todoBien = true;

    // Descripción Institución Financiera
    if (DescripInstFin.val() != '') {
        AsteriscDescrip.removeClass('text-danger');
        validaDescripcion.hide();
    } else {
        AsteriscDescrip.addClass('text-danger');
        validaDescripcion.show();
        todoBien = false;
    }

    // Contacto
    if (Contact.val() != '') {
        AsteriscContact.removeClass('text-danger');
        validaContacto.hide();
    } else {
        AsteriscContact.addClass('text-danger');
        validaContacto.show();
        todoBien = false;
    }

    // Telefono
    if (Tel.val() != '') {
        validaTelefono.hide();
        AsteriscTel.removeClass('text-danger');
    } else {
        AsteriscTel.addClass('text-danger');
        validaTelefono.show();
        todoBien = false;
    }

    // Correo
    if (Cor.val() != '') {
        AsteriscCorre.removeClass('text-danger');
        validaCorreo.hide();
    } else {
        AsteriscCorre.addClass('text-danger');
        validaCorreo.show();
        todoBien = false;
    }
    return todoBien;
}


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

// REGION DE VARIABLES
var EliminarID = 0;

//FUNCION: Opciones de validacion
$('#btnCargarPlanilla').click(function ()
{
//------------------------------------------------------------------------------------------------------
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/InstitucionesFinancieras/_CargaDocumento",
            method: "POST",
            data: data
        }).done(function (data)
        {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR

            if (data.Data == "error")
            {
               // $("#frmCrearAuxCes").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: 'Error: Revise la configuracion, en caso de continuar el error, contacte al administrador',
                });
            }
            else
            {
                $("#frmOpcionesINFS").modal({ backdrop: 'static', keyboard: false });
            }
        });
});


//Crear Institución Financiera ya validado
$(btnAgregar).click(function () {
    if (Validaciones(
    DescInstFin,
    Contac,
    Telef,
    Corre
    )) {
        debugger;
        var data = $("#frmCreateInstFin").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/InstitucionesFinancieras/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                document.getElementById("btnAgregarInstFin").disabled = true;
                window.location.href = '/InstitucionesFinancieras/Index';
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
    $("#frmCreateInstFin").submit(function (e) {
        return false;
    });
    document.getElementById("btnAgregarInstFin").disabled = false;
});



//Editar Institución Financiera ya validado
$(btnEditarConfirmar).click(function () {

    if (Validacion(
    DescripInstFin,
    Contact,
    Tel,
    Cor
    )) {
        $("#frmActualizarINFS").modal({ backdrop: 'static', keyboard: false });
       }

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditInstFin").submit(function (e) {
        return false;
    });

});

$(btnEditar).click(function () {

    var data = $("#frmEditInstFin").serializeArray();
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/InstitucionesFinancieras/Edit",
            method: "POST",
            data: data
        }).done(function (data) {

            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                document.getElementById("btnEditarInstFin").disabled = true;
                window.location.href = '/InstitucionesFinancieras/Index';
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

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
        $("#frmEditInstFin").submit(function (e) {
            return false;
        });
        document.getElementById("btnEditarInstFin").disabled = false;
});




// INACTIVAR
$("#frmInactivarINFS").click(function () {
    //$("#frmEditarAuxCes").modal('hide');
    $("#frmEliminarAuxCes").modal({ backdrop: 'static', keyboard: false });
});

$("#btnInactivarINFS").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmInactivarINFS").serializeArray();
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


// Activar
var activarID = 0;
$(document).on("click", "#btnModalActivarAuxCes", function () {
    activarID = $(this).data('id');
    $("#frmActivarAuxCes").modal({ backdrop: 'static', keyboard: false });
});

//activar ejecutar
$("#btnActivarAuxCes").click(function () {

    $.ajax({
        url: "/AuxilioDeCesantias/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se logró inactivar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridAuxilioCesantia();
            $("#frmActivarAuxCes").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});
