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
        '/AFP/GetData',
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
            var ListaAFP = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaAFP.length; i++) {
                template += '<tr data-id = "' + ListaAFP[i].afp_Id + '">' +
                    '<td>' + ListaAFP[i].afp_Descripcion + '</td>' +
                    '<td>' + ListaAFP[i].afp_AporteMinimoLps + '</td>' +
                    '<td>' + ListaAFP[i].afp_InteresAporte + '</td>' +
                    '<td>' + ListaAFP[i].afp_InteresAnual + '</td>' +
                    '<td>' + ListaAFP[i].tde_Descripcion + '</td>' +
                    '<td>' +
                    '<button type="button" data-id = "' + ListaAFP[i].afp_Id + '" class="btn btn-primary btn-xs" id="btnEditarAFP">Editar</button>' +
                    '<button type="button" data-id = "' + ListaAFP[i].afp_Id + '" class="btn btn-default btn-xs" id="btnDetalleAFP">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyAFP').html(template);
        });
}

$("#btnCerrarCrear").click(function () {
    $("#validation1").css("display", "none");
    $("#validation2").css("display", "none");
    $("#validation3").css("display", "none");
    $("#validation4").css("display", "none");
    $("#validation5").css("display", "none");
    $("#AgregarAFP").modal('hide');
});

$("#btnIconCerrar").click(function () {
    $("#validation1").css("display", "none");
    $("#validation2").css("display", "none");
    $("#validation3").css("display", "none");
    $("#validation4").css("display", "none");
    $("#validation5").css("display", "none");
    $("#AgregarAFP").modal('hide');
});

//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarAFP", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/AFP/EditGetTipoDeduccionDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #tde_IdTipoDedu").empty();
            $.each(data, function (i, iter) {
                $("#Crear #tde_IdTipoDedu").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarAFP").modal();
    $("#Crear #afp_Descripcion").val('');
    $("#Crear #afp_AporteMinimoLps").val('');
    $("#Crear #afp_InteresAporte").val('');
    $("#Crear #afp_InteresAnual").val('');
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroAFP').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var val1 = $("#Crear #tde_IdTipoDedu").val();
    console.log(val1)
    var val2 = $("#Crear #afp_Descripcion").val();
    var val3 = $("#Crear #afp_AporteMinimoLps").val();
    var val4 = $("#Crear #afp_InteresAporte").val();
    var val5 = $("#Crear #afp_InteresAnual").val();

    if (val2 == "") {
        $("#Crear #validation1").css("display", "");
    }
    else {
        $("#Crear #validation1").css("display", "none");
    }

    if (val3 == "" || val3 == null || val3 == undefined) {
        $("#Crear #validation2").css("display", "");
    }
    else {
        $("#Crear #validation2").css("display", "none");
    }

    if (val4 == "" || val4 == null || val4 == undefined) {
        $("#Crear #validation3").css("display", "");
    }
    else {
        $("#Crear #validation3").css("display", "none");
    }
    if (val5 == "" || val5 == null || val5 == undefined) {
        $("#Crear #validation4").css("display", "");
    }
    else {
        $("#Crear #validation4").css("display", "none");
    }

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateAFP").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/AFP/Create",
        method: "POST",
        data: data
    }).done(function (data) {

        //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data != "error") {

            cargarGridDeducciones();

            $("#Crear #afp_Descripcion").val('');
            $("#Crear #afp_AporteMinimoLps").val('');
            $("#Crear #afp_InteresAporte").val('');
            $("#Crear #afp_InteresAnual").val('');

            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarAFP").modal('hide');

            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateAFP").submit(function (e) {
        return false;
    });

});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#validatione1").css("display", "none");
    $("#validatione2").css("display", "none");
    $("#validatione3").css("display", "none");
    $("#validatione4").css("display", "none");
    $("#EditarAFP").modal('hide');
});

$("#btnIconCerrare").click(function () {
    $("#validatione1").css("display", "none");
    $("#validatione2").css("display", "none");
    $("#validatione3").css("display", "none");
    $("#validatione4").css("display", "none");
    $("#EditarAFP").modal('hide');
});



//Editar//
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAFP tbody tr td #btnEditarAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/AFP/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #afp_Id").val(data.afp_Id);
                $("#Editar #afp_Descripcion").val(data.afp_Descripcion);
                $("#Editar #afp_AporteMinimoLps").val(data.afp_AporteMinimoLps);
                $("#Editar #afp_InteresAporte").val(data.afp_InteresAporte);
                $("#Editar #afp_InteresAnual").val(data.afp_InteresAnual);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.tde_IdTipoDedu;

                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/AFP/EditGetTipoDeduccionDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST                    
                        $.each(data, function (i, iter) {
                            $("#Editar #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#DetallesAFP").modal('hide');
                $("#EditarAFP").modal();
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
$("#btnEditAFP").click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var vale2 = $("#Editar #afp_Descripcion").val();
    var vale3 = $("#Editar #afp_AporteMinimoLps").val();
    var vale4 = $("#Editar #afp_InteresAporte").val();
    var vale5 = $("#Editar #afp_InteresAnual").val();


    if (vale2 == "") {
        $("#Editar #validatione1").css("display", "");
    }
    else {
        $("#Editar #validatione1").css("display", "none");
    }

    if (vale3 == "" || vale3 == null || vale3 == undefined) {
        $("#Editar #validatione2").css("display", "");
    }
    else {
        $("#Editar #validatione2").css("display", "none");
    }

    if (vale4 == "" || vale4 == null || vale4 == undefined) {
        $("#Editar #validatione3").css("display", "");
    }
    else {
        $("#Editar #validatione3").css("display", "none");
    }
    if (vale5 == "" || vale5 == null || vale5 == undefined) {
        $("#Editar #validatione4").css("display", "");
    }
    else {
        $("#Editar #validatione4").css("display", "none");
    }

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEditarAFP").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AFP/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {

            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();

            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarAFP").modal('hide');


            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });

        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditarAFP").submit(function (e) {
        return false;
    });

});






//Detalles//
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblAFP tbody tr td #btnDetalleAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/AFP/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].afp_FechaCrea);
                var FechaModifica = FechaFormato(data[0].afp_FechaModifica);
                $(".field-validation-error").css('display', 'none');
                $("#Detalles #afp_Id").val(data[0].afp_Id);
                $("#Detalles #afp_Descripcion").val(data[0].afp_Descripcion);
                $("#Detalles #afp_AporteMinimoLps").val(data[0].afp_AporteMinimoLps);
                $("#Detalles #afp_InteresAporte").val(data[0].afp_InteresAporte);
                $("#Detalles #afp_InteresAnual").val(data[0].afp_InteresAnual);
                $("#Detalles #tde_IdTipoDedu").val(data[0].tde_IdTipoDedu);
                $("#Detalles #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detalles #afp_UsuarioCrea").val(data[0].afp_UsuarioCrea);
                $("#Detalles #afp_FechaCrea").val(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detalles #afp_UsuarioModifica").val(data[0].afp_UsuarioModifica);
                $("#Detalles #afp_FechaModifica").val(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].tde_IdTipoDedu;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/AFP/EditGetTipoDeduccionDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#DetallesAFP").modal();
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
///////////////////////////////////////////////////////////////////////////////////////////////////



//Inactivar//
$(document).on("click", "#btnInactivarAFP", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarAFP").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroAFP").click(function () {

    var data = $("#frmInactivarAFP").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AFP/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se pudo inactivar el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();

            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarAFP").modal('hide');
            $("#EditarAFP").modal('hide');

            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmInactivarAFP").submit(function (e) {
        return false;
    });

});