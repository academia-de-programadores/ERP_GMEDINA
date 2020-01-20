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

$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
})

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/IngresosIndividuales/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaIngresoIndividual = data;

            //LIMPIAR LA DATA DEL DATATABLE
            $('#IndexTable').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaIngresoIndividual.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaIngresoIndividual[i].ini_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaIngresoIndividual[i].ini_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleIngresosIndividuales" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaIngresoIndividual[i].ini_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarIngresosIndividuales" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaIngresoIndividual[i].ini_Activo == false ? esAdministrador == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnActivarIngresosIndividuales" iniid="' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#IndexTable').dataTable().fnAddData([
                    ListaIngresoIndividual[i].ini_IdIngresosIndividuales,
                    ListaIngresoIndividual[i].ini_Motivo,
                    ListaIngresoIndividual[i].per_Nombres + ' ' + ListaIngresoIndividual[i].per_Apellidos,
                    ListaIngresoIndividual[i].ini_Monto,
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

//Activar
$(document).on("click", "#IndexTable tbody tr td #btnActivarIngresosIndividuales", function () {
    document.getElementById("btnActivarRegistroIngresoIndividual").disabled = false;
    var id = $(this).closest('tr').data('id');

    var id = $(this).attr('iniid');
    localStorage.setItem('id', id);
    //Mostrar el Modal
    $("#ActivarIngresosIndividuales").modal();
});

$("#btnActivarRegistroIngresoIndividual").click(function () {
    document.getElementById("btnActivarRegistroIngresoIndividual").disabled = true;
    let id = localStorage.getItem('id')

    $.ajax({
        url: "/IngresosIndividuales/Activar",
        method: "POST",
        data: { id: id }
    }).done(function (data) {
        $("#ActivarIngresosIndividuales").modal('hide');
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

});


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

$("#btnCerrarCrear").click(function () {
    $("#Crear #emp_Id").val("0");
    $("#ini_Motivo").val('');
    $("#ini_Monto").val('');
    $("#ini_PagaSiempre").prop('checked', false);
    $("#ast1").css("color", "black");
    $("#ast2").css("color", "black");
    $("#ast3").css("color", "black");
    $("#validatione1").css("display", "none");
    $("#validatione2").css("display", "none");
    $("#validatione3").css("display", "none");
    $("#AgregarIngresosIndividuales").modal('hide');
    ocultarCargandoCrear();
});

//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
const btnGuardar = $('#btnCreateRegistroIngresoIndividual')

//Div que aparecera cuando se le de click en crear
cargandoCrear = $('#cargandoCrear')

$(document).on("click", "#btnAgregarIngresoIndividual", function () {
    document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/IngresosIndividuales/EditGetEmpleadoDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #emp_Id").empty();
            $("#Crear #emp_Id").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #emp_Id").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#Crear #emp_Id").val("0");
    $("#ini_Motivo").val('');
    $("#ini_Monto").val('');
    $('#Crear #ini_PagaSiempre').prop('checked', false);
});



//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroIngresoIndividual').click(function () {
    var TOF = true;
    var ini_PagaSiempre = false;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var emp_Id = $("#Crear #emp_Id").val();
    var ini_Motivo = $("#Crear #ini_Motivo").val();
    var ini_Monto = $("#Crear #ini_Monto").val();
    var expr = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);

    if ($('#Crear #ini_PagaSiempre').is(':checked')) {
        ini_PagaSiempre = true;
    }
    else{
        ini_PagaSiempre = false;
    }
    //----
    if (ini_Motivo == "" || ini_Motivo == null) {
        $("#Crear #validatione1").css("display", "");
        $("#ast1").css("color", "red");
        TOF = false;
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
    }
    else {
        $("#Crear #validatione1").css("display", "none");
        $("#ast1").css("color", "black");
    }
    //--
    if (emp_Id == "" || emp_Id == 0 || emp_Id == "0") {
        $("#Crear #validatione2").css("display", "");
        $("#ast2").css("color", "red");
        TOF = false;
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
    }
    else {
        $("#Crear #validatione2").css("display", "none");
        $("#ast2").css("color", "black");
    }
    //--
    if (ini_Monto != "" || ini_Monto != null || ini_Monto != undefined) {
        $("#Crear #validatione3").css("display", "none");
        $("#ast3").css("color", "black");
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
    }
    else {
        $("#Crear #validatione3").css("display", "");
        $("#ast3").css("color", "red");
        TOF = false;
    }
    //--
    if (expr.test(ini_Monto)) {
        $("#Crear #validatione3").css("display", "none");
        $("#ast3").css("color", "black");
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
    }
    else {
        $("#Crear #validatione3").css("display", "");
        $("#ast3").css("color", "red");
        TOF = false;
    }

    if (TOF) {
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = true;
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/IngresosIndividuales/Create",
            method: "POST",
            data: { ini_Motivo: ini_Motivo, emp_Id: emp_Id, ini_Monto: ini_Monto, ini_PagaSiempre: ini_PagaSiempre }
        }).done(function (data) {
            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                $("#Crear #ini_Motivo").val('');
                $("#Crear #ini_Monto").val('');
                $('#Crear #ini_PagaSiempre').prop('checked', false);
                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarIngresosIndividuales").modal('hide');

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }

            
        });
    }

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateIngresosIndividuales").submit(function (e) {
        return false;
    });

});



//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#validation1").css("display", "none");
    $("#validation2").css("display", "none");
    $("#validation3").css("display", "none");
    $("#aste1").css("color", "black");
    $("#aste2").css("color", "black");
    $("#aste3").css("color", "black");
    $("#EditarIngresosIndividuales").modal('hide');
    $("#Editar #ini_IdIngresosIndividuales").val('');
    $("#Editar #ini_Motivo").val('');
    $("#Editar #ini_Monto").val('');
    $("#Editar #ini_PagaSiempre").prop('checked', false);
});


//Editar//
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO

const btnEditar = $('#btnEditIngresoIndividual')

const btnInactivar = $('#btnInactivarIngresoIndividual')

//Div que aparecera cuando se le de click en crear
cargandoEditar = $('#cargandoEditar')

function ocultarCargandoEditar() {
    btnEditar.show();
    btnInactivar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

function mostrarCargandoEditar() {
    btnEditar.hide();
    btnInactivar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

$(document).on("click", "#IndexTable tbody tr td #btnEditarIngresosIndividuales", function () {
    var id = $(this).data('id');
    $.ajax({
        url: "/IngresosIndividuales/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {

                if (data.ini_PagaSiempre) {
                    $('#Editar #ini_PagaSiempre').prop('checked', true);
                }
                else {
                    $('#Editar #ini_PagaSiempre').prop('checked', false);
                }

                $("#Editar #ini_IdIngresosIndividuales").val(data.ini_IdIngresosIndividuales);
                $("#Editar #ini_Motivo").val(data.ini_Motivo);
                $("#Editar #ini_Monto").val(data.ini_Monto);
                $("#Editar #ini_PagaSiempre").val(data.ini_PagaSiempre);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.emp_Id;

                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/IngresosIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #emp_Id").empty();
                        //LLENAR EL DROPDOWNLIST                    
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_Id").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});

$("#btnEditIngresoIndividual").click(function () {
    var TOF = true;
    var ini_Motivo = $("#Editar #ini_Motivo").val();
    var ini_Monto = $("#Editar #ini_Monto").val();
    var expreg = new RegExp(/^[0-9]+(\.[0-9]{1,2})$/);
  
    if (ini_Motivo == "" || ini_Motivo == null) {
        $("#Editar #validation1").css("display", "");
        $("#aste1").css("color", "red");
        TOF = false;

    }
    else {
        $("#Editar #validation1").css("display", "none");
        $("#aste1").css("color", "black");
    }
    //--
    if (ini_Monto != "" || ini_Monto != null || ini_Monto != undefined) {
        $("#Editar #validation3").css("display", "none");
        $("#aste3").css("color", "black");
    }
    else {
        $("#Editar #validation3").css("display", "");
        $("#aste3").css("color", "red");
        TOF = false;
    }
    //--
    if (expreg.test(ini_Monto)) {
        $("#Editar #validation3").css("display", "none");
        $("#aste3").css("color", "black");
    }
    else {
        $("#Editar #validation3").css("display", "");
        $("#aste3").css("color", "red");
        TOF = false;
    }

    if (TOF) {
        $("#EditarIngresosIndividuales").modal('hide');
        $("#EditarIngresosIndividualesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("html, body").css("overflow", "hidden");
        $("html, body").css("overflow", "scroll");
        document.getElementById("btnEditIngresoIndividual2").disabled = false;
    }

    $("#EditarIngresosIndividuales").submit(function (e) {
        return false;
    });
});

$(document).on("click", "#btnRegresar", function () {
    $("#EditarIngresosIndividualesConfirmacion").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

$(document).on("click", "#btnReg", function () {
    $("#EditarIngresosIndividualesConfirmacion").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});





//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditIngresoIndividual2").click(function () {
    document.getElementById("btnEditIngresoIndividual2").disabled = true;
    var ini_PagaSiempre = false;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var ini_IdIngresosIndividuales = $("#Editar #ini_IdIngresosIndividuales").val();
    var emp_Id = $("#Editar #emp_Id").val();
    var ini_Motivo = $("#Editar #ini_Motivo").val();
    var ini_Monto = $("#Editar #ini_Monto").val();

    if ($('#Editar #ini_PagaSiempre').is(':checked')) {
        ini_PagaSiempre = true;
    }
    else{
        ini_PagaSiempre = false;
    }
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/IngresosIndividuales/Edit",
            method: "POST",
            data: { ini_IdIngresosIndividuales: ini_IdIngresosIndividuales, ini_Motivo: ini_Motivo, emp_Id: emp_Id, ini_Monto: ini_Monto, ini_PagaSiempre: ini_PagaSiempre }
        }).done(function (data) {
            if (data != "error") {
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarIngresosIndividuales").modal('hide');
                $("#EditarIngresosIndividualesConfirmacion").modal('hide');
                // REFRESCAR UNICAMENTE LA TABLA
                cargarGridDeducciones();
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se editó de forma exitosa!',
                });

            }
            else {
                $("#EditarIngresosIndividualesConfirmacion").modal('hide');
                iziToast.error({
                    title: 'Error',
                    message: '¡No se editó el registro, contacte al administrador!',
                });
            }
        });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmEditIngresoIndividual").submit(function (e) {
        return false;
    });

});






//Detalles//
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#IndexTable tbody tr td #btnDetalleIngresosIndividuales", function () {
    var id = $(this).data('id');
    $.ajax({
        url: "/IngresosIndividuales/Details/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                if (data[0].ini_PagaSiempre) {
                    $("#Detalles #ini_PagaSiempre").html("Si");
                }
                else {
                    $("#Detalles #ini_PagaSiempre").html("No");
                }
                var FechaCrea = FechaFormato(data[0].ini_FechaCrea);
                var FechaModifica = FechaFormato(data[0].ini_FechaModifica);
                $(".field-validation-error").css('display', 'none');
                $("#Detalles #ini_IdIngresosIndividuales").html(data[0].ini_IdIngresosIndividuales);
                $("#Detalles #ini_Motivo").html(data[0].ini_Motivo);
                $("#Detalles #ini_Monto").html(data[0].ini_Monto);
                $("#Detalles #emp_Id").html(data[0].emp_Id);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #ini_UsuarioCrea").html(data[0].ini_UsuarioCrea);
                $("#Detalles #ini_FechaCrea").html(FechaCrea);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #ini_UsuarioModifica").html(data[0].dei_UsuarioModifica);
                $("#Detalles #ini_FechaModifica").html(FechaModifica);

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].emp_Id;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/IngresosIndividuales/EditGetEmpleadoDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        //$("#Detalles #tde_IdTipoDedu").empty();
                        //LLENAR EL DROPDOWNLIST
                        //$("#Detalles #tde_IdTipoDedu").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            //$("#Detalles #tde_IdTipoDedu").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            if (iter.Id == SelectedId) {
                                $("#Detalles #emp_Id").html(iter.Descripcion);
                            }
                        });
                    });
                $("#DetallesIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: '¡No se cargó la información, contacte al administrador!',
                });
            }
        });
});
///////////////////////////////////////////////////////////////////////////////////////////////////



//Inactivar//
$(document).on("click", "#btnBack", function () {
    $("#InactivarIngresosIndividuales").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

$(document).on("click", "#btnBa", function () {
    $("#InactivarIngresosIndividuales").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

$(document).on("click", "#btnInactivarIngresoIndividual", function () {
    $("#EditarIngresosIndividuales").modal('hide');
    $("#InactivarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    document.getElementById("btnInactivarRegistroIngresoIndividual").disabled = false;
});

const btnInhabilitar = $('#btnInactivarRegistroIngresoIndividual')

//Div que aparecera cuando se le de click en crear
cargandoInhabilitar = $('#cargandoInhabilitar')

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
$("#btnInactivarRegistroIngresoIndividual").click(function () {
    document.getElementById("btnInactivarRegistroIngresoIndividual").disabled = true;
    var data = $("#frmInactivarIngresoIndividual").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/IngresosIndividuales/Inactivar",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se inactivó el registro, contacte al administrador!',
            });
        }
        else {
            mostrarCargandoInhabilitar();
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridDeducciones();

            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarIngresosIndividuales").modal('hide');
            $("#EditarIngresosIndividuales").modal('hide');

            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
        ocultarCargandoInhabilitar();
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmInactivarIngresoIndividual").submit(function (e) {
        return false;
    });

});