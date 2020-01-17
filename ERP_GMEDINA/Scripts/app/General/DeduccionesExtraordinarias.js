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

                //variable boton inactivar
                var botonInactivar = ListaDeduccionesExtraordinarias[i].dex_Activo == true ? esAdministrador == "1" ? '<button type="button" class="btn btn-danger btn-xs"  id="btnInactivarDeduccionesExtraordinarias" iddeduccionextra="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '"  data-id = "' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Inctivar</button>' : '' : '';

                //variable donde está el boton activar
                var botonActivar = ListaDeduccionesExtraordinarias[i].dex_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarDeduccionesExtraordinarias" iddeduccionesextra="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '" data-id="' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">Activar</button>' : '' : '';

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
                    botonDetalles + botonEditar + botonActivar
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

$(document).on("click", "btnAgregarDeduccionExtraordinaria", function () {
    //DDL Equipo Empleado
    $.ajax({
        url: "/DeduccionesExtraordinarias/DDLEquipoEmpleado",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $("#eqem_Id").empty();
        $("#eqem_Id").append("<option value='0'>Selecione una opción...</option>");
        $.each(data, function (i, iter) {
            $("#eqem_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
        });
    });

    //DDL Deducciones
    $.ajax({
        url: "/DeduccionesExtraordinarias/DDLDeducciones",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $("#cde_IdDeducciones").empty();
        $("#cde_IdDeducciones").append("<option value='0'>Selecione una opción...</option>");
        $.each(data, function (i, iter) {
            $("#cde_IdDeducciones").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
        });
    });

});



//Validaciones de Botones de las Pantallas
const btnAgregar = $('#btnAgregar')

//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')

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


$("#btnAgregar").click(function () {
    //Validación para Agregar

});

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//Activar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnActivarDeduccionesExtraordinarias", function () {
    var ID = $(this).closest('tr').data('id');
    var ID = $(this).attr('iddeduccionesextra');
    console.log(ID)
    localStorage.setItem('id', ID);
    //Mostrar el Modal
    $("#ActivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
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

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//Modal de Inactivar
$(document).on("click", "tblDeduccionesExtraordinarias tbody tr td #btnInactivarDeduccionesExtraordinarias", function () {
    var ID = $(this).closest('tr').data('id');
    var ID = $(this).attr('iddeduccionextra');
    console.log(ID)
    localStorage.setItem('id', ID);
    //Mostrar el Modal de Inactivar
    $("#InactivarDeduccionesExtraordinarias").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

});

//Funcionamiento del Modal Inactivar
$("#btnInactivar").click(function () {

    let ID = localStorage.getItem('id')
    //Se envia el Formato Json al Controlador para realizar la Inactivación
    $.ajax({
        url: "/DeduccionesExtraordinarias/Inactivar",
        method: "POST",
        data: {id: ID}
    }).done(function (data) {
        if (data == "Error") {
            //Cuando trae un error en el BackEnd al realizar la Inactivación
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
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