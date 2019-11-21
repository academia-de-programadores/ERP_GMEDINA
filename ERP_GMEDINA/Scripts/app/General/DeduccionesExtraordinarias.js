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
                    '<a class="btn btn-default btn-xs" href="/DeduccionesExtraordinarias/Details?id=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra +'">Detalle</a>' +
                    '<button iddeduccionesextra=' + ListaDeduccionesExtraordinarias[i].dex_IdDeduccionesExtra + ' type="button" class="btn btn-danger btn-xs" id="btnInactivarDeduccionesExtraordinarias">Inactivar</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDeduccionesExtraordinarias').html(template);
        });
}

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoDeducciones tbody tr td #btnEditarCatalogoDeducciones", function () {
    var ID = $(this)[0].attr('ID');
    $.ajax({
        url: "/CatalogoDeDeducciones/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #cde_IdDeducciones").val(data.cde_IdDeducciones);
                $("#Editar #cde_DescripcionDeduccion").val(data.cde_DescripcionDeduccion);
                $("#Editar #cde_PorcentajeColaborador").val(data.cde_PorcentajeColaborador);
                $("#Editar #cde_PorcentajeEmpresa").val(data.cde_PorcentajeEmpresa);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/CatalogoDeDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Editar #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Editar #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarDeduccionesExtraordinarias").modal();
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

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditDeduccionesExtraordinarias").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmDeduccionesExtraordinariasEdit").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionesExtraordinarias/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "Error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarDeduccionesExtraordinarias").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });
        }
    });
});


//$(document).ready(function () {
//    //codigo cargar grid
//    /*cargarGridDeducciones();*/
//});


////FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarDeduccionExtraordinaria", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL 
    console.log("Adios")
    debugger
    console.log("Prueba")
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#eqem_Id").val(data.eqem_Id);
                $("#per_Nombres").val(data.per_Nombres);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.eqem_Id;
                
                $.ajax({
                    url: "/DeduccionesExtraordinarias/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                })
            }})

        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
            $("#eqem_Id").empty();
            //LLENAR EL DROPDOWNLIST
            $("#eqem_Id").append("<option value=0>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#eqem_Id").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });

    //MOSTRAR EL MODAL DE AGREGAR
                $("#AgregarDeduccionesExtraordinarias").modal();

});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateDeduccionesExtraordinarias').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmDeduccionesExtraordinariasCreate").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/DeduccionesExtraordinarias/Create",
        method: "POST",
        data: data
    })
        .done(function (data) {
        //CERRAR EL MODAL DE AGREGAR
        $("#AgregarDeduccionesExtraordinarias").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "Error") {
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



//Modal de Inactivar
$(document).on("click", "#tblDeduccionesExtraordinarias tbody tr td #btnInactivarDeduccionesExtraordinarias", function () {
    var ID = $(this).attr('iddeduccionesextra');
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