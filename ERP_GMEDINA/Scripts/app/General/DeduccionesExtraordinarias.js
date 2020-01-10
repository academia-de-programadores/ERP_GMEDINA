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
    var esAdministrador = $("#rol_Usuario").val();
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
                //variable para verificar el estado del registro
                var estadoRegistro = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? '<button type="button" class="btn btn-primary btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? '<button type="button" class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Edit?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? esAdministrador == "1" ? '<button type="button" class="btn btn-primary btn-xs" id="btnActivarDeduccionesExtraordinarias" iddeduccionesextra="@item.dex_IdDeduccionesExtra" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Activar</button>' : '' : '';

                template += '<tr data-id = "' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].eqem_Id + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoInicial + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoRestante + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_ObservacionesComentarios + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_Cuota + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].cde_DescripcionDeduccion + '</td>' +
                    //variable del estado del registro creada en el operador ternario de arriba
                    '<td>' + estadoRegistro + '</td>' +

                    //variable donde está el boton de detalles
                    '<td>' + botonDetalles +

                    //variable donde está el boton de detalles
                     botonEditar +

                    //boton activar 
                    botonActivar
                    '</td>' +
                    '</tr>';
            }

            //Refrescar el tbody de la Tabla del Index
            $("#tbodyDeduccionesExtraordinarias").html(template);
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


//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')


//Activar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnActivarDeduccionesExtraordinarias", function () {

    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('iddeduccionesextra');

    console.log(ID)

    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#ActivarDeduccionesExtraordinarias").modal();
});

$("#btnActivarRegistroDeduccionesExtraordinarias").click(function () {

    let ID = localStorage.getItem('id')
    console.log(ID)
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
                message: 'No se pudo activar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridDeducciones();
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue activado de forma exitosa!',
            });
        }
    });

});

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





//Validaciones de Botones de las Pantallas
const btnAgregar = $('#btnAgregar')

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

//Agregar
$(document).on("click", "#btnAgregar", function () {
    $.ajax({
        url: "/DeduccionesExtraordinarias/Create",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "Error")
        {
            ocultarCargandoCrear();
        }
        else if (data == "Exito")
        {
            iziToast.success({
                title: 'Exito',
                message: 'El registro se agregó de forma exitosa!',
            });
            mostrarCargandoCrear();
        }
    })
});


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
                
            function mostrarCargandoInhabilitar() {
                btnInhabilitar.hide();
                cargandoInhabilitar.show();
                cargandoInhabilitar.html(spinner());
                btnEditar.hide();
                cargandoEditar.html(spinner());
                cargandoEditar.show();
            }

            function ocultarCargandoInhabilitar() {
                btnInhabilitar.show();
                cargandoInhabilitar.html('');
                cargandoInhabilitar.hide();
            }

                // Actualizar el Index para ver los cambios
                location.href = "/DeduccionesExtraordinarias/Index";

                cargarGridDeducciones();

                //Ya actualizado, se oculta el Modal
                $("#InactivarDeduccionesExtraordinarias").modal('hide');

                //Mensaje de Éxito de la Inactivación
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro se inhabilitó de forma exitosa!',
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