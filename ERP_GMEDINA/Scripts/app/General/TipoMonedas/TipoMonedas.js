//----------------------------------------------------------  Mostrar modal "Crear" e "UDP Insert" -----------------------------------------------------------------------------

// Mostrar modal
$(document).on("click", "#btnAgregarTipoMonedas", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#ModalCrear").modal();
});

// Insert 
$('#btnAgregar').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmTipoMonedasCreate").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/TipoMonedas/Create",
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

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblTipoMonedas tbody tr td #btnEditarTipoMonedas", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/TipoMonedas/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                console.log('Hla')
                $("#Editar #cde_IdDeducciones").val(data.cde_IdDeducciones);
                $("#Editar #cde_DescripcionDeduccion").val(data.cde_DescripcionDeduccion);
                $("#Editar #cde_PorcentajeColaborador").val(data.cde_PorcentajeColaborador);
                $("#Editar #cde_PorcentajeEmpresa").val(data.cde_PorcentajeEmpresa);
             
                $("#EditarCatalogoDeducciones").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
        });
});