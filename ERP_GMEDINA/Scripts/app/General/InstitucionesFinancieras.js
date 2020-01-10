//FUNCION GENERICA PARA REUTILIZAR AJAX
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




        //$(document).on("click", "#tblAuxCesantia tbody tr td #btnModalDetalles", function () {
        //    var ID = $(this).data('id');
        //    $.ajax({
        //        url: "/AuxilioDeCesantias/Details/" + ID,
        //        method: "GET",
        //        dataType: "json",
        //        contentType: "application/json; charset=utf-8",
        //        data: JSON.stringify({ ID: ID })
        //    })
        //        .done(function (data)
        //        {
        //            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
        //            if (data)
        //            {
        //                console.log(data);
        //                var FechaCrea = FechaFormato(data[0].aces_FechaCrea);
        //                var FechaModifica = FechaFormato(data[0].aces_FechaModifica);
        //                $("#aces_IdAuxilioCesantia").val(data[0].aces_IdAuxilioCesantia);
        //                $("#frmDetallesAuxCess #aces_RangoInicioMeses").val(data[0].aces_RangoInicioMeses);
        //                $("#frmDetallesAuxCess #aces_RangoFinMeses").val(data[0].aces_RangoFinMeses);
        //                $("#frmDetallesAuxCess #aces_DiasAuxilioCesantia").val(data[0].aces_DiasAuxilioCesantia);
        //                $("#tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
        //                $("#aces_FechaCrea").val(FechaCrea);
        //                data[0].UsuModifica == null ? $("#tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
        //                $("#aces_UsuarioModifica").val(data[0].aces_UsuarioModifica);
        //                $("#aces_FechaModifica").val(FechaModifica);
        //                $("#frmDetailAuxCes").modal();

        //            }
        //            else {
        //                //Mensaje de error si no hay data
        //                iziToast.error({
        //                    title: 'Error',
        //                    message: 'No se pudo cargar la información, contacte al administrador',
        //                });
        //            }
        //        });
        //});
