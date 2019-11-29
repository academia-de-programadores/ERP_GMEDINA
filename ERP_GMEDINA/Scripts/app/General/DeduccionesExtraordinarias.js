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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    _ajax(null,
        '/DeduccionesExtraordinarias/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaDeduccionesExtraordinarias = data, template = '';
            //RECORRER DATA OBTENIDA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeduccionesExtraordinarias.length; i++) {
                template += '<tr data-id = "' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + '">' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].eqem_Id + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoInicial + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_MontoRestante + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_ObservacionesComentarios + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].dex_Cuota + '</td>' +
                    '<td>' + ListaDeduccionesExtraordinarias[i].cde_DescripcionDeduccion + '</td>' +
                    '<td>' +
                    '<a class="btn btn-primary btn-xs" href="/DeduccionesExtraordinarias/Edit?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra +'">Editar</a>' +
                    '<a class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra +'">Detalles</a>' +
                    '<button iddeduccionesextra=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + ' type="button" class="btn btn-danger btn-xs" id="btnInactivarDeduccionesExtraordinarias">Inactivar</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDeduccionesExtraordinarias').html(template);
        });
    FullBody();
}


//Modal de Inactivar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnInactivarDeduccionesExtraordinarias", function () {
    var ID = $(this).attr('iddeduccionesextra');
    console.log(ID)
    $.ajax({
        url: "/DeduccionesExtraordinarias/Inactivar/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    }).done(function (data) {
        $('#dex_IdDeduccionesExtra').val(data.dex_IdDeduccionesExtra);
        //Mostrar el Modal
        $("#InactivarDeduccionesExtraordinarias").modal();
    });
});

//Funcionamiento del Modal
$("#btnInactivar").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmDeduccionesExtraordinariasInactivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesExtraordinarias/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "Error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo Inactivar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarDeduccionesExtraordinarias").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });

        }
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