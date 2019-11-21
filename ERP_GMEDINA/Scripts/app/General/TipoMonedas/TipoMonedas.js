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
$("#btnEditarM").click(function () {
    // id = ID;
    //console.log(id);
    $.ajax({
        url: "/TipoMonedas/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA

            if (data.length > 0) {
                //console.log("funciona");
                //console.log(data);
                $.each(data, function (i, item) {
                    $("#ModalEdit #tmo_Id").val(item.tiho_Id)
                    $("#ModalEdit #tmo_Descripcion").val(item.tiho_Descripcion);
                    //$("#ModalEdit").find("#btnInhabilitarModal").dataset.id = id;
                    //$("#ModalEdit #tmo_UsuarioCrea").val(item.tmo_UsuarioCrea)
                    //$("#ModalEdit #tmo_FechaCrea").val(item.tmo_FechaCrea);
                    $('#ModalEditar').modal('show');
                })
            }
            //})
        });
});

//EDICION DEL REGISTRO
$("#btnEditarModal").click(function () {

    var data = $("#frmEditarTipoHoras").serializeArray();
    //console.log(data);
    $.ajax({
        url: "/TipoHoras/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "-1" || data == "2") {

            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
        }
        else {

            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado con exito!',
            });
            $('#ModalEditar').modal('hide');
            //llenarTabla();

            llenarTabla();
        }
    });
});
