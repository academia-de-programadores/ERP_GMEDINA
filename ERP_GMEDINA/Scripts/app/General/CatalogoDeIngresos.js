//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
        console.log("No se pudo recuperar Script SerializeDate");
    });
// REGION DE VARIABLES
var InactivarID = 0;


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
// REFRESCAR INFORMACIÓN DE LA TABLA
function cargarGridIngresos() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/CatalogoDeIngresos/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                //Validar si se genera un error al cargar de nuevo el grid
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            //GUARDAR EN UNA VARIABLE LA DATA OBTENIDA
            var ListaIngresos = data;
            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblCatalogoIngresos').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaIngresos.length; i++) {
                var estadoRegistro = ListaIngresos[i].cin_Activo == false ? "Inactivo" : "Activo";

                var botonDetalles = ListaIngresos[i].cin_Activo == true ?
                    '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalle" data-id="'
                    + ListaIngresos[i].cin_IdIngresos + '">Detalles</button>' : '';

                var botonEditar = ListaIngresos[i].cin_Activo == true ?
                    '<button type="button" class="btn btn-default btn-xs" id="btnEditarIngreso" data-id="'
                    + ListaIngresos[i].cin_IdIngresos + '">Editar</button>' : '';

                var botonActivar = ListaIngresos[i].cin_Activo == false ? esAdministrador == "1" ?
                    '<button type="button" class="btn btn-primary btn-xs" id="btnActivar" data-id="'
                    + ListaIngresos[i].cin_IdIngresos + '">Activar</button>' : '' : '';
                //AGREGAR EL ROW AL DATATABLE
                $('#tblCatalogoIngresos').dataTable().fnAddData([
                    ListaIngresos[i].cin_IdIngresos,
                    ListaIngresos[i].cin_DescripcionIngreso,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}


// DETALLES
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnDetalle", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/CatalogoDeIngresos/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].cin_FechaCrea);
                var FechaModifica = FechaFormato(data[0].cin_FechaModifica);
                $("#Detallar #cin_IdIngreso").html(data[0].cin_IdIngreso);
                $("#Detallar #cin_DescripcionIngreso").html(data[0].cin_DescripcionIngreso);
                $("#Detallar #cin_UsuarioCrea").val(data[0].cin_UsuarioCrea);
                $("#Detallar #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detallar #cin_FechaCrea").val(FechaCrea);
                data[0].UsuModifica == null ? $("#Detallar #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detallar #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detallar #cin_UsuarioModifica").val(data[0].cin_UsuarioModifica);
                $("#Detallar #cin_FechaModifica").val(FechaModifica);
                $("#DetailCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");

            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
});



//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnEditarIngreso", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
    $.ajax({
        url: "/CatalogoDeIngresos/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #cin_IdIngreso").val(data.cin_IdIngreso);
                $("#Editar #cin_DescripcionIngreso").val(data.cin_DescripcionIngreso);
                $('#asteriscoEdit').removeClass('text-danger');
                $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");

            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
});

$('#frmCatalogoIngresosCreate #cin_DescripcionIngreso').keyup(function(){
    if($(this)
    .val()
    .trim() != ''){
        $('#asteriscoCreate').removeClass('text-danger');
    }
});

$('#Editar #cin_DescripcionIngreso').keyup(function(){
    if($(this)
    .val()
    .trim() != ''){
        $('#validareditar').hide();
        $('#asteriscoEdit').removeClass('text-danger');
    }
});

$("#btnUpdateIngresos").click(function () {
    //descedit es la variable que uso para validar si esta vacio o no
    var descedit = $("#Editar #cin_DescripcionIngreso").val();
    document.getElementById("btnEditarIngresos").disabled = false;
    if (descedit != '' && descedit != null && descedit != undefined && isNaN(descedit) == true) {
        //al validar que no este vacio muestro mi modal de confirmación
        $("#Editar #validareditar").css("display", "none");
        $("#EditarCatalogoIngresos").modal('hide');
        $("#EditarCatalogoIngresosConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("html, body").css("overflow", "hidden");
        $("html, body").css("overflow", "scroll");
    }
    else {
        $("#descripcioncrear").css("display", "");
        $("#Crear #cin_DescripcionIngreso").focus();

        $('#validareditar').html('Campo Descripción Ingresos requerido');
        $('#validareditar').show();
        $('#asteriscoEdit').addClass('text-danger');
    }
});



//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarIngresos").click(function () {

    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $('input[name$="cin_DescripcionIngreso"').val();
    var id = $('input[name$="cin_IdIngreso"').val();

    console.log(data);
    var descedit = $("#Editar #cin_DescripcionIngreso").val();

    //VALIDAMOS LOS CAMPOS
    if (descedit != '' && descedit != null && descedit != undefined && isNaN(descedit) == true) {
        document.getElementById("btnEditarIngresos").disabled = true;
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Edit",
            method: "POST",
            data: { cin_DescripcionIngreso: data, id: id }
        }).done(function (data) {

            if (data != "error") {

                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarCatalogoIngresos").modal('hide');
                $("#EditarCatalogoIngresosConfirmacion").modal('hide');
                
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
               
            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }

        });

        // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
        $("#frmCatalogoIngresos").submit(function (e) {
            return false;
        });
    }
});
btnEditarNo

$("#btnEditarNo").click(function () {  
    $("#EditarCatalogoIngresosConfirmacion").modal('hide');
    $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});
const btneditar = $('#btnEditarIngresos'),
    cargandoEditar = $('#cargandoEditar')//Div que aparecera cuando se le de click en crear

function mostrarcargandoEditar() {
    btneditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarcargandoEditar() {
    btneditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
}

// INACTIVAR 
$("#btnModalInactivar").click(function () {

    document.getElementById("btnInactivarIngresos").disabled = false;
    $("#EditarCatalogoIngresos").modal('hide');
    $("#InactivarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

//Modal editar despues de No Inactivar
$("#btnNoInactivar").click(function () {
    $("#validareditar").css("display", "none");
    $("#EditarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    $("#InactivarCatalogoIngresos").modal('hide');
});


$("#btnInactivarIngresos").click(function () {
    document.getElementById("btnInactivarIngresos").disabled = true;
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmInactivarCatalogoIngresos").serializeArray();
    var ID = InactivarID;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Inactivar/" + ID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se inactivó el registro, contacte al administrador',
            });
        }
        else {
            $("#InactivarCatalogoIngresos").modal('hide');
            $("#EditarCatalogoIngresos").modal('hide');
            cargarGridIngresos();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    $("#frmCatalogoIngresos").submit(function (e) {
        return false;
    });
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarCatalogoIngresos", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#Crear #cin_DescripcionIngreso").val('');
    $('#asteriscoCreate').removeClass('text-danger');
    document.getElementById("btnCreateRegistroIngresos").disabled = false;
    $("#AgregarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

$("#frmCatalogoIngresosCreate").submit(function (e) {
    return false;
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroIngresos').click(function () {

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    var data = $("#frmCatalogoIngresosCreate").serializeArray();

    var descripcion = $("#Crear #cin_DescripcionIngreso").val();

    //VALIDAMOS LOS CAMPOS
    if (descripcion != '' && descripcion != null && descripcion != undefined && isNaN(descripcion) == true) {
        document.getElementById("btnCreateRegistroIngresos").disabled = true;
        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/CatalogoDeIngresos/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR

            if (data != "error") {
                $("#AgregarCatalogoIngresos").modal('hide');
               

                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
                
                $("#Crear #cin_DescripcionIngreso").val('');
            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
        });
    }
    else {
        document.getElementById("btnCreateRegistroIngresos").disabled = false;
        $("#descripcioncrear").css("display", "");
        $("#Crear #cin_DescripcionIngreso").focus();
        $('#asteriscoCreate').addClass('text-danger');
    }
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarCatalogoIngresos").modal('hide');
    $("#frmCatalogoIngresosCreate").modal('hide');
});


//#region CREAR

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarCrear").click(function () {
    $("#descripcioncrear").css("display", "none");
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarCreate").click(function () {
    $("#descripcioncrear").css("display", "none");
});
//#endregion

//#region EDITAR
//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarEditar").click(function () {
    $("#validareditar").css("display", "none");
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconCerrarEditar").click(function () {
    $("#validareditar").css("display", "none");
});
//#endregion


const btnGuardar = $('#btnCreateRegistroIngresos'),

    cargandoCrearcargandoCrear = $('#cargandoCrear'),

    cargandoCrear = $('#cargandoCrear')//Div que aparecera cuando se le de click en crear

function mostrarCargandoCrear() {
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear() {
    btnGuardar.show();
    cargandoCrear.html('');
    cargandoCrear.hide();
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


//FUNCION: PRIMERA FASE DE ACTIVAR

$(document).on("click", "#tblCatalogoIngresos tbody tr td #btnActivar", function () {
    document.getElementById("btnActivarIngreso").disabled = false;
    //FUNCION: MOSTRAR EL MODAL DE ACTIVAR
    IDActivar = $(this).data('id');
    $("#ActivarCatalogoIngresos").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});


//EJECUTAR LA ACTIVACION DEL REGISTRO
$("#btnActivarIngreso").click(function () {
    document.getElementById("btnActivarIngreso").disabled = true;
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/CatalogoDeIngresos/Activar/" + IDActivar,
        method: "POST"
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarCatalogoIngresos").modal('hide');
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridIngresos();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});

