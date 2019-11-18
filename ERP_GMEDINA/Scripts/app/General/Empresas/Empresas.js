
//----------------------------------------------------------  Mostrar modal "Crear" e "UDP Insert" -----------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Mostrar modal
$(document).on("click", "#btnAgregarEmpresas", function () {
    //MOSTRAR EL MODAL DE AGREGAR
        
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
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Mostrar modal con info
//$(document).on("click", "#IndexTable tbody tr td #btnEditarEmpresas", function () {
//    var ID = $(this).closest('tr').data('id');
//    console.log(ID);

//    $.ajax({
//        url: "/Empresas/Edit/" + ID,
//        method: "GET",
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ ID: ID })
//    })
//        .done(function (data) {
//            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
//            if (data.length > 0) {
//                console.log('Hla')
//                console.log(data)
//                $.each(data, function (i, item) {
//                    $("#ModalEditar #empr_Id").val(item.empr_Id);
//                    $("#ModalEditar #empr_Nombre").val(item.empr_Nombre);
//                    $("#ModalEditar #empr_RazonInactivo").val(item.empr_RazonInactivo);
//                })

                
//            }
//            else {
//                //Mensaje de error si no hay data
//                iziToast.error({
//                    title: 'Error',
//                    message: 'No se pudo cargar la información, contacte al administrador',
//                });
//            }

//            $("#ModalEditar").modal();
//        });
//});

$(document).on("click", "#IndexTable tbody tr td #btnEditarEmpresas", function () {
    var id = $(this).closest('tr').data('id');
    console.log(id);
    $.ajax({
        url: "/Empresas/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA

            if (data.length > 0) {
                console.log("funciona");
                console.log(data);
                $.each(data, function (i, item) {
                    $("#ModalEdit #empr_Id").val(item.empr_Id);
                    $("#ModalEdit #empr_Nombre").val(item.empr_Nombre);
                    $("#ModalEdit #empr_RazonInactivo").val(item.empr_RazonInactivo);
                    //$("#ModalEdit #tiho_UsuarioCrea").val(item.tiho_UsuarioCrea)
                    //$("#ModalEdit #tiho_FechaCrea").val(item.tiho_FechaCrea);
                })
                $("#ModalEditar").modal();
            }
        })
});



//----------------------------------------------------------  Mostrar modal "Deshabilitar" y "UDP Delete" -----------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//----------------------------------------------------------  Otras funciones ---------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
