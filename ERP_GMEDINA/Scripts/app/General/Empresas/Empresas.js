
//----------------------------------------------------------  Mostrar modal "Crear" e "UDP Insert" -----------------------------------------------------------------------------

// Mostrar modal
$(document).on("click", "#btnAgregarEmpresas", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#ModalCrear").modal();
});

// Insert 
$('#btnAgregar').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#fmrEmpresasCreate").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/Empresas/Create",
        method: "POST",
        data: data
    }).done(function (data) {
        //CERRAR EL MODAL DE AGREGAR
        $("#ModalCrear").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo guardar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });
});

//----------------------------------------------------------  Mostrar modal "Editar" y "UDP Update" -----------------------------------------------------------------------------

//-------------------------------------------------------------------------------------------------------------------------------------------



//----------------------------------------------------------  Mostrar modal "Deshabilitar" y "UDP Delete" -----------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------