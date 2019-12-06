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

//OBTENER SCRIPT DE FORMATEO DE FECHA
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
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
    _ajax(null,
        '/TechosDeducciones/GetData',
        'GET',
        (data) => {            
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: 'No se pudo cargar la información, contacte al administrador',
                });
            }
            var ListaTechosDeducciones = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaTechosDeducciones.length; i++) {
                template += '<tr data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '">' +
                    '<td>' + ListaTechosDeducciones[i].tddu_PorcentajeColaboradores + '</td>' +
                    '<td>' + ListaTechosDeducciones[i].tddu_PorcentajeEmpresa + '</td>' +
                    '<td>' + ListaTechosDeducciones[i].tddu_Techo + '</td>' +
                    '<td>' + ListaTechosDeducciones[i].cde_DescripcionDeduccion + '</td>' +
                    '<td>' +
                    '<button data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '" type="button" class="btn btn-primary btn-xs"  id="btnEditarTechosDeducciones">Editar</button>' +
                    '<button data-id = "' + ListaTechosDeducciones[i].tddu_IdTechosDeducciones + '" type="button" class="btn btn-default btn-xs"  id="btnDetalleTechosDeducciones">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyTechosDeducciones').html(template);
        });
    FullBody();
}

//Modal Create Techos Deducciones
$(document).on("click", "#btnAgregarTechosDeducciones", function () {
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
    $("#AgregarTechosDeducciones").modal();
});

//FUNCION: CREAR EL NUEVO REGISTRO TECHOS DEDUCCIONES
$('#btnCreateTechoDeducciones').click(function () {
    var ModelState = true;
    
    //$("#Editar #tede_Id").val() == "" ? ModelState = false : '';
    $("#Crear #tddu_Techo").val() == "" ? ModelState = false : $("#Crear #tddu_Techo").val() == "0.00" ? ModelState = false : $("#Crear #tddu_Techo").val() == null ? ModelState = false : isNaN($("#Crear #tddu_Techo").val()) == true ? ModelState = false : '';
    $("#Crear #tddu_PorcentajeColaboradores").val() == "" ? ModelState = false : $("#Crear #tddu_PorcentajeColaboradores").val() == "0.00" ? ModelState = false : $("#Crear #tddu_PorcentajeColaboradores").val() == null ? ModelState = false : isNaN($("#Crear #tddu_PorcentajeColaboradores").val()) == true ? ModelState = false : '';
    $("#Crear #tddu_PorcentajeEmpresa").val() == "" ? ModelState = false : $("#Crear #tddu_PorcentajeEmpresa").val() == "0" ? ModelState = false : $("#Crear #tddu_PorcentajeEmpresa").val() == null ? ModelState = false : isNaN($("#Crear #tddu_PorcentajeEmpresa").val()) == true ? ModelState = false : '';
    $("#Crear #cde_IdDeducciones").val() == "" ? ModelState = false : $("#Crear #cde_IdDeducciones").val() == "0" ? ModelState = false : $("#Crear #cde_IdDeducciones").val() == null ? ModelState = false : isNaN($("#Crear #cde_IdDeducciones").val()) == true ? ModelState = false : '';

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    if (ModelState) {
        var data = $("#frmTechosDeduccionesCreate").serializeArray();
        console.log(data);
        debugger;
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
                    message: 'No se pudo guardar el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                cargarGridTechosDeducciones();
                console.log(data);
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue registrado de forma exitosa!',
                });
            }
        });
    }
    else {
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
    
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblTechosDeducciones tbody tr td #btnEditarTechosDeducciones", function () {
    var ID = $(this).data('id');
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
                $("#EditarTechosDeducciones").modal();
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
$("#btnEditarTecho").click(function () {
    
    var ModelState = true;
    $("#Editar #tddu_IdTechosDeducciones").val() == "" ? ModelState = false : $("#Editar #tddu_IdTechosDeducciones").val() == "0" ? ModelState = false : $("#Editar #tddu_IdTechosDeducciones").val() == null ? ModelState = false : '';
    $("#Editar #tddu_Techo").val() == "" ? ModelState = false : $("#Editar #tddu_Techo").val() == "0.00" ? ModelState = false : $("#Editar #tddu_Techo").val() == null ? ModelState = false : '';
    $("#Editar #tddu_PorcentajeColaboradores").val() == "" ? ModelState = false : $("#Editar #tddu_PorcentajeColaboradores").val() == "0.00" ? ModelState = false : $("#Editar #tddu_PorcentajeColaboradores").val() == null ? ModelState = false : '';
    $("#Editar #tddu_PorcentajeEmpresa").val() == "" ? ModelState = false : $("#Editar #tddu_PorcentajeEmpresa").val() == "0" ? ModelState = false : $("#Editar #tddu_PorcentajeEmpresa").val() == null ? ModelState = false : '';
    $("#Editar #cde_IdDeducciones").val() == "" ? ModelState = false : $("#Editar #cde_IdDeducciones").val() == "0" ? ModelState = false : $("#Editar #cde_IdDeducciones").val() == null ? ModelState = false : '';

    if (ModelState) {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditTechosDeducciones").serializeArray();
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
                    message: 'No se pudo editar el registro, contacte al administrador',
                });
            }
            else {
                cargarGridTechosDeducciones();
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarTechosDeducciones").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro fue editado de forma exitosa!',
                });
            }
        });
    }
    else {
        iziToast.error({
            title: 'Error',
            message: 'Ingrese datos válidos.',
        });
    }
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarTechosDeducciones").modal('hide');
});




$(document).on("click", "#btnInactivarTechoDeducciones", function () {
    $("#EditarTechosDeducciones").modal('hide');
    $("#InactivarTechosDeducciones").modal();
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
                message: 'No se pudo inactivar el registro, contacte al administrador',
            });
        }
        else {
            cargarGridTechosDeducciones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarTechosDeducciones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro fue Inactivado de forma exitosa!',
            });
        }
    });
    InactivarID = 0;
});


//DETALLES
$(document).on("click", "#tblTechosDeducciones tbody tr td #btnDetalleTechosDeducciones", function () {
    var ID = $(this).data('id');
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
                $("#Detalles #cde_IdDeducciones").val(data[0].cde_IdDeducciones);

                $("#Detalles #tddu_PorcentajeColaboradores").val(data[0].tddu_PorcentajeColaboradores);
                $("#Detalles #tddu_PorcentajeEmpresa").val(data[0].tddu_PorcentajeEmpresa);
                $("#Detalles #tddu_Techo").val(data[0].tddu_Techo);                
                $("#Detalles #tede_UsuarioCrea").val(data[0].tede_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detalles #tddu_FechaCrea").val(FechaCrea);
                $("#Detalles #tddu_UsuarioModifica").val(data.tddu_UsuarioModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detalles #tddu_FechaModifica").val(FechaModifica);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedId = data[0].cde_IdDeducciones;
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
                        $("#Detalles #cde_IdDeducciones").empty();
                        //LLENAR EL DROPDOWNLIST
                        $("#Detalles #cde_IdDeducciones").append("<option value=0>Selecione una opción...</option>");
                        $.each(data, function (i, iter) {
                            $("#Detalles #cde_IdDeducciones").append("<option" + (iter.Id == SelectedId ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#DetailsTechosDeducciones").modal();
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