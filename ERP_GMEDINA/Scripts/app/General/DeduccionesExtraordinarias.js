//
//Obtención de Script para Formateo de Fechas
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });

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
    _ajax(null,
        '/DeduccionesExtraordinarias/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {

                //Validar si se genera un error al cargar de nuevo el Index
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }

            //Variable para guardar la data obtenida
            var ListaDeduccionesExtraordinarias = data, template = '';

            //Recorrer la data obtenida a traves de la función anterior y se crea un Template de la Tabla para Actualizarse
            for (var i = 0; i < ListaDeduccionesExtraordinarias.length; i++) {
                template += '<tr data-id = "' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].eqem_Id + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoInicial + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoRestante + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_ObservacionesComentarios + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_Cuota + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].cde_DescripcionDeduccion + '</td>' +
                    '<td>' +
                    '<a class="btn btn-primary btn-xs" href="/DeduccionesExtraordinarias/Edit?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Editar</a>' +
                    '<a class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Detalle</a>' +
                    '<button iddeduccionesextra=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + ' type="button" class="btn btn-danger btn-xs" id="btnInactivarDeduccionesExtraordinarias">Inactivar</button>' +
                    '</td>' +
                    '</tr>';
            }

            //Refrescar el tbody de la Tabla del Index
            $('#tbodyDeduccionesExtraordinarias').html(template);
        });
}

//Modal de Inactivar
$(document).on("click", "#btnInactivarDeduccionesExtraordinarias", function () {

    //Mostrar el Modal de Inactivar
    $("#InactivarDeduccionesExtraordinarias").modal();

});


//Funcionamiento del Modal Inactivar
$("#btnInactivar").click(function () {

    //Serializar el Formulario del Modal que esta en su respectiva Vista Parcial, para Parsear al Formato Json 
    var data = $("#frmDeduccionesExtraordinariasInactivar").serializeArray();

    //Se envia el Formato Json al Controlador para realizar la Inactivación
    $.ajax({
        url: "/DeduccionesExtraordinarias/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "Error") {

            //Cuando trae un error en el BackEnd al realizar la Inactivación
            iziToast.error({
                title: 'Error',
                message: 'No se pudo Inactivar el registro, contacte al administrador',
            });
        }
        else {

            // Actualizar el Index para ver los cambios
            location.href = "/DeduccionesExtraordinarias/Index";

            cargarGridDeducciones();

            //Ya actualizado, se oculta el Modal
            $("#InactivarDeduccionesExtraordinarias").modal('hide');

            //Mensaje de Éxito de la Inactivación
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });

        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmDeduccionesExtraordinariasInactivar").submit(function (e) {
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