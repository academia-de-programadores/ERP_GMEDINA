////FUNCION GENERICA PARA REUTILIZAR AJAX
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
var InactivarID = 0;

//#region Blur
$('#Crear #cde_IdDeducciones, #Editar #cde_IdDeducciones').blur(function () {
    let idDeduccion = $(this).val();
    if (idDeduccion == null || idDeduccion == "" || idDeduccion == "0") {
        $('#Crear #Validation_deduccion, #Editar #Validation_deduccionE').show();
        $('#Crear #AsteriskDeduccion, #Editar #AsteriskDeduccionE').addClass('text-danger');
    } else {
        $('#Crear #AsteriskDeduccion, #Editar #AsteriskDeduccionE').removeClass('text-danger');
        $('#Crear #Validation_deduccion, #Editar #Validation_deduccionE').hide();
    }
});

$('#Crear #tddu_Techo, #Editar #tddu_Techo').blur(function () {
    let techo = $(this).val();

    let tieneValorTecho = false;
    //Validacion Requerido
    if (techo == null || techo == "") {

        $('#Crear #Validation_Techo, #Editar #Validation_TechoE').html('El campo es requerido.');
        $('#Crear #Validation_Techo, #Editar #Validation_TechoE').show();
        $('#Crear #AsteriskTecho, #Editar #AsteriskTechoE').addClass('text-danger');
    } else {
        tieneValorTecho = true;
        $('#Crear #AsteriskTecho, #Editar #AsteriskTechoE').removeClass('text-danger');
        $('#Crear #Validation_Techo, #Editar #Validation_TechoE').hide();
    }

    if (tieneValorTecho) {
        if (techo < 0 || techo == 0) {
            $('#Crear #Validation_Techo, #Editar #Validation_TechoE').html('EL campo no puede ser menor o igual que cero.');
            $('#Crear #Validation_Techo, #Editar #Validation_TechoE').show();
            $('#Crear #AsteriskTecho, #Editar #AsteriskTechoE').addClass('text-danger');
        } else {
            $('#Crear #Validation_Techo, #Editar #Validation_TechoE').html('El campo es requerido.');
            $('#Crear #Validation_Techo, #Editar #Validation_TechoE').hide();
            $('#Crear #AsteriskTecho, #Editar #AsteriskTechoE').removeClass('text-danger');
        }
    }
});

$('#Crear #tddu_PorcentajeColaboradores, #Editar #tddu_PorcentajeColaboradores').blur(function () {
    let porcentajeColaborador = $(this).val();

    let tieneValorColaborador = false;
    //Validacion Requerido
    if (porcentajeColaborador == null || porcentajeColaborador == "") {
        $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').empty();
        $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
        $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').show();
        $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
    } else {
        tieneValorColaborador = true;
        $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').hide();
    }

    if (tieneValorColaborador) {
        if (porcentajeColaborador < 0) {
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').html('El campo no puede ser menor que cero.');
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').show();
            $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').hide();
            $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        }
        //CONVERTIR A DECIMAL
        porcentajeColaborador = parseInt(porcentajeColaborador.replace(/,/g, ''));
        if (porcentajeColaborador > 100) {
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').html('EL campo no puede ser mayor que 100');
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').show();
            $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeColaboradores, #Editar #Validation_PorcentajeColaboradoresE').hide();
            $('#Crear #AsteriskPorcentajeColaborador, #Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        }
    }
});

$('#Crear #tddu_PorcentajeEmpresa, #Editar #tddu_PorcentajeEmpresa').blur(function () {
    let porcentajeEmpresa = $(this).val();

    let tieneValorPorcentajeEmpresa = false;
    //Validacion Requerido
    if (porcentajeEmpresa == null || porcentajeEmpresa == "") {
        $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').empty();
        $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
        $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').show();
        $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
    } else {
        tieneValorPorcentajeEmpresa = true;
        $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').hide();
    }

    if (tieneValorPorcentajeEmpresa) {
        if (porcentajeEmpresa < 0) {
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').html('El campo no puede ser menor que cero.');
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').show();
            $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').hide();
            $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');
        }
        //CONVERTIR A DECIMAL
        porcentajeEmpresa = parseInt(porcentajeEmpresa.replace(/,/g, ''));
        if (porcentajeEmpresa > 100) {
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').html('El campo no puede ser mayor que 100.');
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').show();
            $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeEmpresa, #Editar #Validation_PorcentajeEmpresaE').hide();
            $('#Crear #AsteriskPorcentajeEmpresa, #Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');
        }
    }
});
//#endregion


function limpiarMensajes() {
    //span crear
    $('#Crear #Validation_Techo').hide();
    $('#Crear #Validation_deduccion').hide();
    $('#Crear #Validation_PorcentajeColaboradores').hide();
    $('#Crear #Validation_PorcentajeEmpresa').hide();

    //asteriscos rojos crear
    $('#Crear #asterisco').removeClass('text-danger');
    $('#Crear #AsteriskTecho').removeClass('text-danger');
    $('#Crear #AsteriskDeduccion').removeClass('text-danger');
    $('#Crear #AsteriskPorcentajeColaborador').removeClass('text-danger');
    $('#Crear #AsteriskPorcentajeEmpresa').removeClass('text-danger');

    //span editar
    $('#Editar #Validation_deduccionE').hide();
    $('#Editar #Validation_TechoE').hide();
    $('#Editar #Validation_PorcentajeColaboradoresE').hide();
    $('#Editar #Validation_PorcentajeEmpresaE').hide();

    //asteriscos Editar
    $('#Editar #AsteriskDeduccionE').removeClass('text-danger');
    $('#Editar #AsteriskTechoE').removeClass('text-danger');
    $('#Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
    $('#Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');

}

//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {
    });

// EVITAR POSTBACK DE FORMULARIOS
$("#frmEditTechosDeducciones").submit(function (e) {
    e.preventDefault();
});
$("#frmTechosDeduccionesCreate").submit(function (e) {
    e.preventDefault();
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridTechosDeducciones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/TechosDeducciones/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            var ListaTechosDeducciones = data;
            $('#tblTechosDeducciones').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTechosDeducciones.length; i++) {
                var Activo;

                //variable para verificar el estado del registro
                var estadoRegistro = ListaTechosDeducciones[i].tddu_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '" type="button" class="btn btn-primary btn-xs" style="margin-right: 3px;"  id="btnDetalleTechosDeducciones">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaTechosDeducciones[i].tddu_Activo == true ? '<button data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '" type="button" class="btn btn-default btn-xs"  id="btnEditarTechosDeducciones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaTechosDeducciones[i].tddu_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '" type="button" class="btn btn-default btn-xs"  id="btnActivarTechosDeducciones">Activar</button>' : '' : '';

                $('#tblTechosDeducciones').dataTable().fnAddData([
                    ListaTechosDeducciones[i].tddu_IdTechosDeducciones,
                    ListaTechosDeducciones[i].tddu_PorcentajeColaboradores.toFixed(2),
                    ListaTechosDeducciones[i].tddu_PorcentajeEmpresa.toFixed(2),
                    ListaTechosDeducciones[i].tddu_Techo.toFixed(2),
                    ListaTechosDeducciones[i].cde_DescripcionDeduccion,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    FullBody();
}

//Reiniciar DataAnnotations cuando se cierra un modal 
$("#btnCerrarCreateTechosDeducciones").click(function () {
    $("#frmTechosDeduccionesCreate #Validation_deduccion").css("display", "none");
    $("#frmTechosDeduccionesCreate #Validation_Techo").css("display", "none");
    $("#frmTechosDeduccionesCreate #Validation_PorcentajeColaboradores").css("display", "none");
    $("#frmTechosDeduccionesCreate #Validation_PorcentajeEmpresa").css("display", "none");
    $("#Crear .asterisco").removeClass("text-danger");
});

$("#btnCerrarEditar").click(function () {
    $("#Editar #Validation_deduccionE").css("display", "none");
    $("#Editar #Validation_TechoE").css("display", "none");
    $("#Editar #Validation_PorcentajeColaboradoresE").css("display", "none");
    $("#Editar #Validation_PorcentajeEmpresaE").css("display", "none");
    $("#Editar .asterisco").removeClass("text-danger");
});

//Modal Create Techos Deducciones
$(document).on("click", "#btnAgregarTechosDeducciones", function () {
    limpiarMensajes();
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosDeducciones/Create");
            
    if (validacionPermiso.status == true) {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/TechosDeducciones/EditGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {
            $("#Crear #cde_IdDeducciones").empty();
            $("#Crear #cde_IdDeducciones").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #cde_IdDeducciones").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $(".field-validation-error").css('display', 'none');
    $('#Crear input[type=text], input[type=number]').val('');
    $("#AgregarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
    }
});

function validacionCrear() {
    let todoBien = true;
    let idDeduccion = $('#Crear #cde_IdDeducciones').val();
    if (idDeduccion == null || idDeduccion == "" || idDeduccion == "0") {
        $('#Crear #Validation_deduccion').show();
        $('#Crear #AsteriskDeduccion').addClass('text-danger');
        todoBien = false;
    } else {
        $('#Crear #AsteriskDeduccion').removeClass('text-danger');
        $('#Crear #Validation_deduccion').hide();
    }

    let techo = $('#Crear #tddu_Techo').val();
    let tieneValorTecho = false;
    //Validacion Requerido
    if (techo == null || techo == "") {
        $('#Crear #Validation_Techo').html('Campo Techo requerido');
        $('#Crear #Validation_Techo').show();
        $('#Crear #AsteriskTecho').addClass('text-danger');
        todoBien = false;
    } else {
        tieneValorTecho = true;
        $('#Crear #AsteriskTecho').removeClass('text-danger');
        $('#Crear #Validation_Techo').hide();
    }

    if (tieneValorTecho) {
        if (techo < 0 || techo == 0) {
            $('#Crear #Validation_Techo').html('El campo no puede ser menor o igual que cero.');
            $('#Crear #Validation_Techo').show();
            $('#Crear #AsteriskTecho').addClass('text-danger');
            todoBien = false;
        } else {
            $('#Crear #Validation_Techo').html('El campo es requerido.');
            $('#Crear #Validation_Techo').hide();
            $('#Crear #AsteriskTecho').removeClass('text-danger');
        }
    }

    let porcentajeColaborador = $('#Crear #tddu_PorcentajeColaboradores').val();

    let tieneValorColaborador = false;
    //Validacion Requerido
    if (porcentajeColaborador == null || porcentajeColaborador == "") {
        todoBien = false;
        $('#Crear #Validation_PorcentajeColaboradores').html('El campo es requerido.');
        $('#Crear #Validation_PorcentajeColaboradores').show();
        $('#Crear #AsteriskPorcentajeColaborador').addClass('text-danger');
    } else {
        tieneValorColaborador = true;
        $('#Crear #AsteriskPorcentajeColaborador').removeClass('text-danger');
        $('#Crear #Validation_PorcentajeColaboradores').hide();
    }

    if (tieneValorColaborador) {
        if (porcentajeColaborador < 0) {
            todoBien = false;
            $('#Crear #Validation_PorcentajeColaboradores').html('El campo no puede ser menor que cero.');
            $('#Crear #Validation_PorcentajeColaboradores').show();
            $('#Crear #AsteriskPorcentajeColaborador').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeColaboradores').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeColaboradores').hide();
            $('#Crear #AsteriskPorcentajeColaborador').removeClass('text-danger');
        }
        porcentajeColaborador = parseInt(porcentajeColaborador.replace(/,/g, ''));
        if (porcentajeColaborador > 100) {
            todoBien = false;
            $('#Crear #Validation_PorcentajeColaboradores').empty();
            $('#Crear #Validation_PorcentajeColaboradores').html('El campo no puede ser mayor que 100.');
            $('#Crear #Validation_PorcentajeColaboradores').show();
            $('#Crear #AsteriskPorcentajeColaborador').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeColaboradores').empty();
            $('#Crear #Validation_PorcentajeColaboradores').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeColaboradores').hide();
            $('#Crear #AsteriskPorcentajeColaborador').removeClass('text-danger');
        }
    }

    let porcentajeEmpresa = $('#Crear #tddu_PorcentajeEmpresa').val();

    let tieneValorPorcentajeEmpresa = false;
    //Validacion Requerido
    if (porcentajeEmpresa == null || porcentajeEmpresa == "") {
        todoBien = false;
        $('#Crear #Validation_PorcentajeEmpresa').html('El campo es requerido.');
        $('#Crear #Validation_PorcentajeEmpresa').show();
        $('#Crear #AsteriskPorcentajeEmpresa').addClass('text-danger');
    } else {
        tieneValorPorcentajeEmpresa = true;
        $('#Crear #AsteriskPorcentajeEmpresa').removeClass('text-danger');
        $('#Crear #Validation_PorcentajeEmpresa').hide();
    }

    if (tieneValorPorcentajeEmpresa) {
        if (porcentajeEmpresa < 0) {
            todoBien = false;
            $('#Crear #Validation_PorcentajeEmpresa').empty();
            $('#Crear #Validation_PorcentajeEmpresa').html('El campo no puede ser menor que cero.');
            $('#Crear #Validation_PorcentajeEmpresa').show();
            $('#Crear #AsteriskPorcentajeEmpresa').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeEmpresa').empty();
            $('#Crear #Validation_PorcentajeEmpresa').html('El campo es requerido');
            $('#Crear #Validation_PorcentajeEmpresa').hide();
            $('#Crear #AsteriskPorcentajeEmpresa').removeClass('text-danger');
        }

        porcentajeEmpresa = parseInt(porcentajeEmpresa.replace(/,/g, ''));
        if (porcentajeEmpresa > 100) {
            todoBien = false;
            $('#Crear #Validation_PorcentajeEmpresa').empty();
            $('#Crear #Validation_PorcentajeEmpresa').html('EL campo no puede ser mayor que 100.');
            $('#Crear #Validation_PorcentajeEmpresa').show();
            $('#Crear #AsteriskPorcentajeEmpresa').addClass('text-danger');
        } else {
            $('#Crear #Validation_PorcentajeEmpresa').empty();
            $('#Crear #Validation_PorcentajeEmpresa').html('El campo es requerido.');
            $('#Crear #Validation_PorcentajeEmpresa').hide();
            $('#Crear #AsteriskPorcentajeEmpresa').removeClass('text-danger');
        }
    }

    return todoBien;
}

function validacionEditar() {
    let todoBien = true;
    let idDeduccion = $('#Editar #cde_IdDeducciones').val();
    if (idDeduccion == null || idDeduccion == "" || idDeduccion == "0") {
        $('#Editar #Validation_deduccionE').show();
        $('#Editar #AsteriskDeduccionE').addClass('text-danger');
        todoBien = false;
    } else {
        $('#Editar #AsteriskDeduccionE').removeClass('text-danger');
        $('#Editar #Validation_deduccionE').hide();
    }

    let techo = $('#Editar #tddu_Techo').val();
    let tieneValorTecho = false;
    //Validacion Requerido
    if (techo == null || techo == "") {
        $('#Editar #Validation_TechoE').html('Campo Techo requerido');
        $('#Editar #Validation_TechoE').show();
        $('#Editar #AsteriskTechoE').addClass('text-danger');
        todoBien = false;
    } else {
        tieneValorTecho = true;
        $('#Editar #AsteriskTechoE').removeClass('text-danger');
        $('#Editar #Validation_TechoE').hide();
    }

    if (tieneValorTecho) {
        if (techo < 0 || techo == 0) {
            $('#Editar #Validation_TechoE').html('Campo Techo no puede ser menor o igual que cero.');
            $('#Editar #Validation_TechoE').show();
            $('#Editar #AsteriskTechoE').addClass('text-danger');
            todoBien = false;
        } else {
            $('#Editar #Validation_TechoE').html('Campo Techo requerido');
            $('#Editar #Validation_TechoE').hide();
            $('#Editar #AsteriskTechoE').removeClass('text-danger');
        }
    }

    let porcentajeColaborador = $('#Editar #tddu_PorcentajeColaboradores').val();

    let tieneValorColaborador = false;
    //Validacion Requerido
    if (porcentajeColaborador == null || porcentajeColaborador == "") {
        todoBien = false;
        $('#Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
        $('#Editar #Validation_PorcentajeColaboradoresE').show();
        $('#Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
    } else {
        tieneValorColaborador = true;
        $('#Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        $('#Editar #Validation_PorcentajeColaboradoresE').hide();
    }

    if (tieneValorColaborador) {
        if (porcentajeColaborador < 0) {
            todoBien = false;
            $('#Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Editar #Validation_PorcentajeColaboradoresE').html('El campo no puede ser menor que cero.');
            $('#Editar #Validation_PorcentajeColaboradoresE').show();
            $('#Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
        } else {
            $('#Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
            $('#Editar #Validation_PorcentajeColaboradoresE').hide();
            $('#Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        }
        //CONVERTIR A DECIMAL
        porcentajeColaborador = parseInt(porcentajeColaborador.replace(/,/g, ''));
        if (porcentajeColaborador > 100) {
            todoBien = false;
            $('#Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Editar #Validation_PorcentajeColaboradoresE').html('El campo no puede ser mayor que 100.');
            $('#Editar #Validation_PorcentajeColaboradoresE').show();
            $('#Editar #AsteriskPorcentajeColaboradorE').addClass('text-danger');
        } else {
            $('#Editar #Validation_PorcentajeColaboradoresE').empty();
            $('#Editar #Validation_PorcentajeColaboradoresE').html('El campo es requerido.');
            $('#Editar #Validation_PorcentajeColaboradoresE').hide();
            $('#Editar #AsteriskPorcentajeColaboradorE').removeClass('text-danger');
        }

    }

    let porcentajeEmpresa = $('#Editar #tddu_PorcentajeEmpresa').val();

    let tieneValorPorcentajeEmpresa = false;
    //Validacion Requerido
    if (porcentajeEmpresa == null || porcentajeEmpresa == "") {
        todoBien = false;
        $('#Editar #Validation_PorcentajeEmpresaE').empty();
        $('#Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
        $('#Editar #Validation_PorcentajeEmpresaE').show();
        $('#Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
    } else {
        tieneValorPorcentajeEmpresa = true;
        $('#Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');
        $('#Editar #Validation_PorcentajeEmpresaE').hide();
    }

    if (tieneValorPorcentajeEmpresa) {

        if (porcentajeEmpresa < 0) {
            todoBien = false;
            $('#Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Editar #Validation_PorcentajeEmpresaE').html('El campo no puede ser menor que cero.');
            $('#Editar #Validation_PorcentajeEmpresaE').show();
            $('#Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
        } else {
            $('#Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
            $('#Editar #Validation_PorcentajeEmpresaE').hide();
            $('#Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');
        }

        //CONVERTIR A DECIMAL
        porcentajeEmpresa = parseInt(porcentajeEmpresa.replace(/,/g, ''));
        if (porcentajeEmpresa > 100) {
            todoBien = false;
            $('#Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Editar #Validation_PorcentajeEmpresaE').html('El campo no puede ser mayor que 100.');
            $('#Editar #Validation_PorcentajeEmpresaE').show();
            $('#Editar #AsteriskPorcentajeEmpresaE').addClass('text-danger');
        } else {
            $('#Editar #Validation_PorcentajeEmpresaE').empty();
            $('#Editar #Validation_PorcentajeEmpresaE').html('El campo es requerido.');
            $('#Editar #Validation_PorcentajeEmpresaE').hide();
            $('#Editar #AsteriskPorcentajeEmpresaE').removeClass('text-danger');
        }
    }

    return todoBien;
}

//FUNCION: CREAR EL NUEVO REGISTRO TECHOS DEDUCCIONES
$('#btnCreateTechoDeducciones').click(function () {
    var deduccion = $("#Crear #cde_IdDeducciones").val();
    var techo = $("#Crear #tddu_Techo").val();
    var porcentajeColaborador = $("#Crear #tddu_PorcentajeColaboradores").val();
    var porcentajeEmpresa = $("#Crear #tddu_PorcentajeEmpresa").val();
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    if (validacionCrear()) {
        var data = $("#frmTechosDeduccionesCreate").serializeArray();
        data[5].value = data[5].value.replace(/,/g, '');
        data[6].value = data[6].value.replace(/,/g, '');
        data[7].value = data[7].value.replace(/,/g, '');
        $.ajax({
            url: "/TechosDeducciones/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            $("#AgregarTechosDeducciones").modal('hide');
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No se guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                cargarGridTechosDeducciones();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblTechosDeducciones tbody tr td #btnEditarTechosDeducciones", function () {
    limpiarMensajes();
    var ID = $(this).data('id');
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosDeducciones/Edit");

    if (validacionPermiso.status == true) {    
    InactivarID = ID;
    $.ajax({
        url: "/TechosDeducciones/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #tddu_IdTechosDeducciones").val(data.tddu_IdTechosDeducciones);
                $("#Editar #tddu_Techo").val(data.tddu_Techo);
                $("#Editar #tddu_PorcentajeColaboradores").val(data.tddu_PorcentajeColaboradores);
                $("#Editar #tddu_PorcentajeEmpresa").val(data.tddu_PorcentajeEmpresa);
                $("#Editar #cde_IdDeduccion").val(data.cde_IdDeducciones);
                $(".field-validation-error").css('display', 'none');

                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data.cde_IdDeducciones;
                //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                $.ajax({
                    url: "/TechosDeducciones/EditGetDDL",
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ ID })
                })
                    .done(function (data) {
                        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                        $("#Editar #cde_IdDeducciones").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Editar #cde_IdDeducciones").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Editar #cde_IdDeducciones").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
        });
    }
});

//FUNCION: OCULTAR EL MODAL DE EDITAR Y MOSTRAR EL MODAL DE CONFIRMACION
$("#btnEditarTecho").click(function () {



	var deduccionE = $("#Editar #cde_IdDeducciones").val();
	var techoE = $("#Editar #tddu_Techo").val();
	var porcentajeColaboradorE = $("#Editar #tddu_PorcentajeColaboradores").val();
	var porcentajeEmpresaE = $("#Editar #tddu_PorcentajeEmpresa").val();

	//DESBLOQUEAR EL BOTON DE EDICION
	$("#btnConfirmarEditar").attr("disabled", false);
	//VALIDAR EL FORMULARIO
	if (validacionEditar()) {
	document.getElementById("btnEditarrTechosDeducciones").disabled = false;
			//OCULTAR EL MODAL DE EDICION
			$("#EditarTechosDeducciones").modal('hide');
			//DESPLEGAR EL MODAL DE CONFIRMACION
			$("#EditTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
	} else {
		//OCULTAR EL MODAL DE CONFIRMACION
		$("#EditTechosDeducciones").modal('hide');
		//DESPLEGAR EL MODAL DE EDICION
		$("#EditarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
	}

});

//FUNCION: OCULTAR EL MODAL DE CONFIRMACION Y MOSTRAR EL MODAL DE EDITAR
$("#btnNoEditar").click(function () {

	debugger;
	//OCULTAR EL MODAL DE EDICION
	$("#EditTechosDeducciones").modal('hide');
	//DESPLEGAR EL MODAL DE CONFIRMACION
	$("#EditarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });

});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarrTechosDeducciones").click(function () {
    var deduccionE = $("#Editar #cde_IdDeducciones").val();
    var techoE = $("#Editar #tddu_Techo").val();
    var porcentajeColaboradorE = $("#Editar #tddu_PorcentajeColaboradores").val();
    var porcentajeEmpresaE = $("#Editar #tddu_PorcentajeEmpresa").val();
    if (validacionEditar()) {
    	document.getElementById("btnEditarrTechosDeducciones").disabled = true;
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditTechosDeducciones").serializeArray();
        data[5].value = data[5].value.replace(/,/g, '');
        data[6].value = data[6].value.replace(/,/g, '');
        data[7].value = data[7].value.replace(/,/g, '');
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/TechosDeducciones/Edit",
            method: "POST",
            data: data
        }).done(function (data) {
            if (data == "error") {
                //Cuando traiga un error del backend al guardar la edicion
                iziToast.error({
                    title: 'Error',
                    message: 'No se editó el registro, contacte al administrador',
                });
            }
            else {
            	cargarGridTechosDeducciones();
            	debugger;
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            	$("#EditTechosDeducciones").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro fue editado de forma exitosa!',
                });
            }
        });
    }
    //Validaciones Data Annotations + asteriscos
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarTechosDeducciones").modal('hide');
});

//quitar Confirmacion
$('#btnNoInactivar').click(function () {
    $("#InactivarTechosDeducciones").modal('hide');
    $("#EditarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
});

// validar informacion del usuario

$(document).on("click", "#btnInactivarTechoDeducciones", function () {
    var validacionPermiso = userModelState("TechosDeducciones/Inactivar");
    if (validacionPermiso.status == true) {
            $("#EditarTechosDeducciones").modal('hide');
            $("#InactivarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
    }
});

//Inactivar registro Techos Deducciones
$("#btnInactivarTechosDeducciones").click(function () {
    var data = $("#frmInactivarTechosDeducciones").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/TechosDeducciones/Inactivar/" + InactivarID,
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
            cargarGridTechosDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarTechosDeducciones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
    InactivarID = 0;
});

//DETALLES
$(document).on("click", "#tblTechosDeducciones tbody tr td #btnDetalleTechosDeducciones", function () {
    var ID = $(this).data('id');
    // validar informacion del usuario
    var validacionPermiso = userModelState("TechosDeducciones/Details");

    if (validacionPermiso.status == true) {
        $.ajax({
            url: "/TechosDeducciones/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    var FechaCrea = FechaFormato(data[0].tddu_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].tddu_FechaModifica);
                    $("#Detalles #tddu_UsuarioCrea").val(data[0].tddu_UsuarioCrea);
                    $("#Detalles #cde_IdDeducciones").html(data[0].cde_IdDeducciones);
                    $("#Detalles #cde_DescripcionDeduccion").html(data[0].cde_DescripcionDeduccion);
                    $("#Detalles #tddu_PorcentajeColaboradores").html(data[0].tddu_PorcentajeColaboradores.toFixed(2));
                    $("#Detalles #tddu_PorcentajeEmpresa").html(data[0].tddu_PorcentajeEmpresa.toFixed(2));
                    $("#Detalles #tddu_Techo").html(data[0].tddu_Techo.toFixed(2));
                    $("#Detalles #tede_UsuarioCrea").html(data[0].tede_UsuarioCrea);
                    $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detalles #tddu_FechaCrea").html(FechaCrea);
                    $("#Detalles #tddu_UsuarioModifica").html(data.tddu_UsuarioModifica);
                    data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detalles #tddu_FechaModifica").html(FechaModifica);
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedId = data[0].cde_IdDeducciones;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                    //$.ajax({
                    //    url: "/TechosDeducciones/EditGetDDL",
                    //    method: "GET",
                    //    dataType: "json",
                    //    contentType: "application/json; charset=utf-8",
                    //    data: JSON.stringify({ ID })
                    //    })
                    //    .done(function (data) {
                    //        //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                    //        $("#Detalles #cde_IdDeducciones").empty();
                    //        //LLENAR EL DROPDOWNLIST
                    //        $("#Detalles #cde_IdDeducciones").append("<option value=0>Selecione una opción...</option>");
                    //        $.each(data, function (i, iter) {
                    //            $("#Detalles #cde_IdDeducciones").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                    //        });
                    //    });
                    $("#DetailsTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se cargó la información, contacte al administrador',
                    });
                }
            });
    }
});

// activar
var activarID = 0;
// validar informacion del usuario
$(document).on("click", "#btnActivarTechosDeducciones", function () {
    var validacionPermiso = userModelState("TechosDeducciones/Activar");
    if (validacionPermiso.status == true) {
        activarID = $(this).data('id');
        $("#ActivarTechosDeducciones").modal({ backdrop: 'static', keyboard: false });
    }
});

//activar ejecutar
$("#btnActivarTechosDeduccionesEjecutar").click(function () {

    $.ajax({
        url: "/TechosDeducciones/Activar/" + activarID,
        method: "POST",
        data: { id: activarID }
    }).done(function (data) {
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se activó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridTechosDeducciones();
            $("#ActivarTechosDeducciones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});

//VALIDAR LAS ENTRADAS DE LOS CONCEPTOS AGREGADOS
$('.ValidarCaracteres').bind('keypress', function (event) {
    //var regex = new RegExp("^[a-zA-Z0-9]+$");
    var regex = new RegExp("^[0-9.]");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});