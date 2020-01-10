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
        '/DeduccionAFP/GetData',
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
            var ListaDeduccionAFP = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaDeduccionAFP.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaAFP[i].dafp_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaAFP[i].dafp_Activo == true ? '<button type="button" class="btn btn-primary btn-xs" id="btnDetalleDeduccionAFP" data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAFP[i].dafp_Activo == true ? '<button type="button" class="btn btn-default btn-xs" id="btnEditarDeduccionAFP" data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaAFP[i].dafp_Activo == false ? esAdministrador == "1" ? '<button type="button" class="btn btn-primary btn-xs" id="btnActivarDeduccionAFP" dafpid="' + ListaDeduccionAFP[i].dafp_Id + '" data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">Activar</button>' : '' : '';

                template += '<tr data-id = "' + ListaDeduccionAFP[i].dafp_Id + '">' +
                    '<td>' + ListaDeduccionAFP[i].per_Nombres + ' ' + ListaDeduccionAFP[i].per_Apellidos + '</td>' +
                    '<td>' + ListaDeduccionAFP[i].dafp_AporteLps + '</td>' +
                    '<td>' + ListaDeduccionAFP[i].afp_Descripcion + '</td>' +
                     +
                     +
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
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyDeduccionAFP').html(template);
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

const btnActivar = $('#btnActivarRegistroDeduccionAFP')

//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')

function ocultarCargandoCrear() {
    btnActivar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

function mostrarCargandoCrear() {
    btnActivar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

//Activar
$(document).on("click", "#tblAFP tbody tr td #btnActivarDeduccionAFP", function () {

    var ID = $(this).closest('tr').data('id');

    var ID = $(this).attr('dafpid');

    $.ajax({
        url: "/DeduccionAFP/Activar/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

    $('#dafp_Id').val(data.dafp_Id);

    $("#ActivarDeduccionAFP").modal();
})

$("#btnActivarRegistroDeduccionAFP").click(function () {

    var data = $("#frmActivarDeduccionAFP").serializeArray();

    $.ajax({
        url: "/DeduccionAFP/Activar",
        method: "POST",
        data: data
    }).done(function (data) {
        $("#ActivarDeduccionAFP").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo activar el registro, contacte al administrador',
            });
        }
        else if (data == "bien") {
            cargarGridDeducciones();
            console.log(data);
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue activado de forma exitosa!',
            });
        }
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#ActivarDeduccionAFP").submit(function (e) {
        return false;
    });

})

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Agregar//

const btnGuardar = $('#btnCreateRegistroDeduccionAFP')

//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
}

function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

$("#validation1d").css("display", "none");

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarDeduccionAFP", function () {

    //CARGAR INFORMACIÓN DEL DROPDOWNLIST EMPLEADO PARA EL MODAL
    $.ajax({
        url: "/DeduccionAFP/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        .done(function (data) {
            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
            $("#Crear #emp_Id").empty();
            //LLENAR EL DROPDOWNLIST
            $.each(data, function (i, iter) {
                $("#Crear #emp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });

    //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
    $.ajax({
        url: "/DeduccionAFP/EditGetAFPDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        .done(function (data) {
            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
            $("#Crear #afp_Id").empty();
            //LLENAR EL DROPDOWNLIST
            $.each(data, function (i, iter) {
                $("#Crear #afp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });


    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarDeduccionAFP").modal();
    $("#Crear #dafp_AporteLps").val('');
    
});



//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroDeduccionAFP').click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var val1 = $("#Crear #dafp_AporteLps").val();

    if (val1 == "") {
        $("#Crear #validation1d").css("display", "");
    }
    else {
        $("#Crear #validation1d").css("display", "none");
    }
    mostrarCargandoCrear();

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCreateDeduccionAFP").serializeArray();
    //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
    $.ajax({
        url: "/DeduccionAFP/Create",
        method: "POST",
        data: data
    }).done(function (data) {

        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data != "error") {

            cargarGridDeducciones();

            $("#Crear #dafp_AporteLps").val('');

            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarDeduccionAFP").modal('hide');

            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro se agregó de forma exitosa!',
            });

            $("#Crear #dafp_AporteLps").val('');
        }
        else
        {
            iziToast.error({
                title: 'Error',
                message: 'Datos Invalidos!',
            });
        }
        
        ocultarCargandoCrear();
        
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateDeduccionAFP").submit(function (e) {
        return false;
    });

});

$("#btnCerrarAgregar").click(function () {
    $("#validation1d").css("display", "none");
    $("#AgregarDeduccionAFP").modal('hide');
});

$("#btnIconCerrar").click(function () {
    $("#validation1d").css("display", "none");
    $("#AgregarDeduccionAFP").modal('hide');
});


//Editar//

const btnEditar = $('#btnEditDeduccionAFP')

//Div que aparecera cuando se le de click en crear
cargandoEditar = $('#cargandoEditar')

function ocultarCargandoEditar() {
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

function mostrarCargandoEditar() {
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

$("#Editar #validatione1").css("display", "none");

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnEditarDeduccionAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/DeduccionAFP/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })

        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #dafp_Id").val(data.dafp_Id);
                $("#Editar #dafp_AporteLps").val(data.dafp_AporteLps);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION


                var SelectedIdEmpleado = data.emp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST EMPLEADO PARA EL MODAL
                $.ajax({
                    url: "/DeduccionAFP/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #emp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedIdEmpleado ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


                var SelectedIdAFP = data.afp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
                $.ajax({
                    url: "/DeduccionAFP/EditGetAFPDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #afp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #afp_Id").append("<option" + (iter.Id == SelectedIdAFP ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#DetallesDeduccionAFP").modal('hide');
                $("#EditarDeduccionAFP").modal();
                
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
$("#btnEditDeduccionAFP").click(function () {
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var vale1 = $("#Editar #dafp_AporteLps").val();
    debugger
    console.log(vale1)
    if (vale1 == "" || vale1 == null || vale1 == undefined) {
        $("#Editar #validatione1").css("display", "");
    }
    else {
        $("#Editar #validatione1").css("display", "none");
    }
    mostrarCargandoEditar();

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEditDeduccionAFP").serializeArray();

    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionAFP/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {

            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();

            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarDeduccionAFP").modal('hide');

            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro se editó de forma exitosa!',
            });
        }
        else
        {
            iziToast.error({
                title: 'Error',
                message: 'Datos Invalidos!',
            });
        }
        ocultarCargandoEditar();
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditDeduccionAFP").submit(function (e) {
        return false;
    });
});

//FUNCION: OCULTAR MODAL DE EDICIÓN

$("#btnCerrarEditar").click(function () {
    $("#validatione1").css("display", "none");
    $("#EditarDeduccionAFP").modal('hide');
});

$("#btnIconCerrare").click(function () {
    $("#validatione1").css("display", "none");
    $("#EditarDeduccionAFP").modal('hide');
});




//Detalles//
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblDeduccionAFP tbody tr td #btnDetalleDeduccionAFP", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/DeduccionAFP/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].dafp_FechaCrea);
                var FechaModifica = FechaFormato(data[0].dafp_FechaModifica);
                $("#Detalles #dafp_Id").html(data[0].dafp_Id);
                $("#Detalles #emp_Id").html(data[0].emp_Id);
                $("#Detalles #per_Nombres + #per_Apellidos").html(data[0].per_Nombres + data[0].per_Apellidos);
                $("#Detalles #emp_CuentaBancaria").html(data[0].emp_CuentaBancaria);
                $("#Detalles #dafp_AporteLps").html(data[0].dafp_AporteLps);
                $("#Detalles #afp_Id").html(data[0].afp_Id);
                $("#Detalles #afp_Descripcion").html(data[0].afp_Descripcion);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #dafp_UsuarioCrea").html(data[0].dafp_UsuarioCrea);
                $("#Detalles #dafp_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #dafp_UsuarioModifica").html(data[0].dafp_UsuarioModifica);
                $("#Detalles #dafp_FechaModifica").html(FechaModifica);

                var SelectedIdEmpleado = data[0].emp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST EMPLEADO PARA EL MODAL
                $.ajax({
                    url: "/DeduccionAFP/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Detalles #emp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Detalles #emp_Id").append("<option" + (iter.Id == SelectedIdEmpleado ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });


                var SelectedIdAFP = data[0].afp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST AFP PARA EL MODAL
                $.ajax({
                    url: "/DeduccionAFP/EditGetAFPDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Detalles #afp_Id").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #afp_Id").append("<option value = 0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #afp_Id").append("<option" + (iter.Id == SelectedIdAFP ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });

                $("#DetallesDeduccionAFP").modal();

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

$(document).on("click", "#btnEdicionDeduccionAFP", function () {
    //MOSTRAR EL MODAL DE Editar
    $("#EditarDeduccionAFP").modal();
    $("#DetallesDeduccionAFP").modal('hide');
});



///////////////////////////////////////////////////////////////////////////////////////////////////

//Inactivar//
$(document).on("click", "#btnInactivarDeduccionAFP", function () {
    $("#EditarDeduccionAFP").modal('hide');
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#InactivarDeduccionAFP").modal();
});

const btnInhabilitar = $('#btnInactivarRegistroDeduccionAFP');

//Div que aparecera cuando se le de click en crear
cargandoInhabilitar = $('#cargandoInhabilitar');

function ocultarCargandoInhabilitar() {
    btnInhabilitar.show();
    cargandoInhabilitar.html('');
    cargandoInhabilitar.hide();
}

function mostrarCargandoInhabilitar() {
    btnInhabilitar.hide();
    cargandoInhabilitar.html(spinner());
    cargandoInhabilitar.show();
}

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroDeduccionAFP").click(function () {

    var data = $("#frmDeduccionAFPInactivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/DeduccionAFP/Inactivar",
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
            mostrarCargandoInhabilitar();
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarDeduccionAFP").modal('hide');
            
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro se inhabilitó de forma exitosa!',
            });
        }
        ocultarCargandoInhabilitar();
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmDeduccionAFPInactivar").submit(function (e) {
        return false;
    });
});


// PROBANDO LOS IZITOAST
//$(document).ready(function () {
//    console.log('cargado JS');
//    iziToast.show({
//        title: 'Hola',
//        message: 'Estoy probando los iziToast'
//    });
//});


