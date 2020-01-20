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
    validacionCorreo = $('#validacionCorreo')
;


const btnEditar = $('#btnAgregarInstFin'),
    DescripInstFin = $('#insf_DescInstitucionFinanc'),
    Contact = $('#insf_Contacto'),
    Tel = $('#phone'),
    Cor = $('#insf_Correo'),
    AsteriscDescrip = $('#AsteriscoDescripcion'),
    AsteriscContact = $('#AsteriscoContacto'),
    AsteriscTel = $('#AsteriscoTelefono'),
    AsteriscCorre = $('#AsteriscoCorreo'),
    validaDescripcion = $('#validaDescripcion'),
    validaContacto = $('#validaContacto'),
    validaTelefono = $('#validaTelefono'),
    validaCorreo = $('#validaCorreo')
;


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
                $("#frmOpcionesINFS").modal();
            }
        });
});







// INACTIVAR 
$("#frmInactivarINFS").click(function () {
    //$("#frmEditarAuxCes").modal('hide');
    $("#frmEliminarAuxCes").modal();
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
    $("#frmActivarAuxCes").modal();
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
