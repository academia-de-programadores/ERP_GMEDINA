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
$("#frmEditAcumuladosISR").submit(function (e) {
    e.preventDefault();
});
$("#frmAcumuladosISRCreate").submit(function (e) {
    e.preventDefault();
});

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridAcumuladosISR() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/AcumuladosISR/GetData',
        'GET',
        (data) => {
            if (data.length == 0) {
                iziToast.error({
                    title: 'Error',
                    message: 'No se cargó la información, contacte al administrador',
                });
            }
            var ListaAcumuladosISR = data, template = '';
            //Recorrer la data y crear el template que se pondrá en el tbody

            //LIMPIAR LA DATA DEL DATATABLE
            $('#tblAcumuladosISR').DataTable().clear();
            for (var i = 0; i < ListaAcumuladosISR.length; i++) {

                //variable para verificar el estado del registro
                var estadoRegistro = ListaAcumuladosISR[i].aisr_Activo == false ? 'Inactivo' : 'Activo'

                //variable boton detalles
                var botonDetalles = ListaAcumuladosISR[i].aisr_Activo == true ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnDetalleAcumuladosISR">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAcumuladosISR[i].aisr_Activo == true ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarAcumuladosISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaAcumuladosISR[i].aisr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarAcumuladosISR">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblAcumuladosISR').dataTable().fnAddData([
                    ListaAcumuladosISR[i].aisr_Id,
                    ListaAcumuladosISR[i].aisr_Descripcion,
                    ListaAcumuladosISR[i].aisr_Monto,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
    FullBody();
}

//Modal Create Techos Deducciones
$(document).on("click", "#btnAgregarAcumuladosISR", function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $(".field-validation-error").css('display', 'none');
    $('#Crear input[type=text], input[type=number]').val('');
    $("#AgregarAcumuladosISR").modal();
});

//FUNCION: CREAR EL NUEVO REGISTRO TECHOS DEDUCCIONES
$('#btnCreateAcumuladosISR').click(function () {
    var ModelState = true;

    $("#Crear #aisr_Descripcion").val() == "" ? ModelState = false : $("#Crear #aisr_Descripcion").val() == " " ? ModelState = false : $("#Crear #aisr_Descripcion").val() == null ? ModelState = false : isNaN($("#Crear #aisr_Descripcion").val()) == false ? ModelState = false : '';
    $("#Crear #aisr_Monto").val() == "" ? ModelState = false : $("#Crear #aisr_Monto").val() == "0.00" ? ModelState = false : $("#Crear #aisr_Monto").val() == null ? ModelState = false : isNaN($("#Crear #aisr_Monto").val()) == true ? ModelState = false : '';

    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
    if (ModelState) {
        var data = $("#frmAcumuladosISRCreate").serializeArray();
        console.log(data);
        debugger;
        $.ajax({
            url: "/AcumuladosISR/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            $("#AgregarAcumuladosISR").modal('hide');
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                iziToast.error({
                    title: 'Error',
                    message: 'No guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                cargarGridAcumuladosISR();
                console.log(data);
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
});

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnEditarAcumuladosISR", function () {
    var ID = $(this).data('id');
    InactivarID = ID;
    $.ajax({
        url: "/AcumuladosISR/Edit/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#Editar #aisr_Id").val(data.aisr_Id);
                $("#Editar #aisr_FechaCrea").val(data.aisr_FechaCrea);
                $("#Editar #aisr_UsuarioCrea").val(data.aisr_UsuarioCrea);

                $("#Editar #aisr_Descripcion").val(data.aisr_Descripcion);
                $("#Editar #aisr_Monto").val(data.aisr_Monto);
                $(".field-validation-error").css('display', 'none');

                $("#EditarAcumuladosISR").modal();
            }
            else {
                //Mensaje de error si no hay data
                iziToast.error({
                    title: 'Error',
                    message: 'No cargó la información, contacte al administrador',
                });
            }
        });
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditarAcumulado").click(function () {

    var ModelState = true;
    $("#Editar #aisr_Id").val() == "" ? ModelState = false : $("#Editar #aisr_Id").val() == "0" ? ModelState = false : $("#Editar #aisr_Id").val() == null ? ModelState = false : '';
    $("#Editar #aisr_Descripcion").val() == "" ? ModelState = false : $("#Editar #aisr_Descripcion").val() == " " ? ModelState = false : $("#Editar #aisr_Descripcion").val() == null ? ModelState = false : '';
    $("#Editar #aisr_Monto").val() == "" ? ModelState = false : $("#Editar #aisr_Monto").val() == "0.00" ? ModelState = false : $("#Editar #aisr_Monto").val() == null ? ModelState = false : '';

    if (ModelState) {
        //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
        var data = $("#frmEditAcumuladosISR").serializeArray();
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/AcumuladosISR/Edit",
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
                cargarGridAcumuladosISR();
                //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
                $("#EditarAcumuladosISR").modal('hide');
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Éxito',
                    message: '¡El registro se editó de forma exitosa!',
                });
            }
        });
    }
});

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    $("#EditarAcumuladosISR").modal('hide');
});

$(document).on("click", "#btnInactivarAcumuladosISR", function () {
    $("#EditarAcumuladosISR").modal('hide');
    $("#InactivarAcumuladosISR").modal();
});

//Inactivar registro Techos Deducciones    
$("#btnInactivarAcumuladosISREjecutar").click(function () {
    var data = $("#frmInactivarAcumuladosISR").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AcumuladosISR/Inactivar/" + InactivarID,
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: 'No inactivó el registro, contacte al administrador',
            });
        }
        else {
            cargarGridAcumuladosISR();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarAcumuladosISR").modal('hide');
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
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnDetalleAcumuladosISR", function () {
    var ID = $(this).data('id');
    $.ajax({
        url: "/AcumuladosISR/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: ID })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                var FechaCrea = FechaFormato(data[0].aisr_FechaCrea);
                var FechaModifica = FechaFormato(data[0].aisr_FechaModifica);
                $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);

                $("#Detalles #aisr_Descripcion").html(data[0].aisr_Descripcion);
                $("#Detalles #aisr_Monto").html(data[0].aisr_Monto);

                $("#Detalles #aisr_UsuarioCrea").html(data[0].aisr_UsuarioCrea);
                $("#Detalles #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                $("#Detalles #aisr_FechaCrea").html(FechaCrea);

                $("#Detalles #aisr_UsuarioModifica").html(data.aisr_UsuarioModifica);
                data[0].UsuModifica == null ? $("#Detalles #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detalles #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                $("#Detalles #aisr_FechaModifica").html(FechaModifica);
                $("#DetailsAcumuladosISR").modal();
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

$(document).ready(function () {
    $(document).ready(function () {
        $('.dataTables-AcumuladosISR').DataTable({
            "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
            responsive: true,
            pageLength: 10,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'copy',
                    text: '<i class="fa fa-copy btn-xs"></i>',
                    titleAttr: 'Copiar',
                    exportOptions: {
                        columns: [0, 1],
                    },
                    className: 'btn btn-primary'

                },

                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o btn-xs"></i>',
                    titleAttr: 'Excel',
                    exportOptions: {
                        columns: [0, 1],
                    },
                    className: 'btn btn-primary',
                    title: 'Acumulados ISR'
                }

            ]
        });
    });
});

// activar
var activarID = 0;
$(document).on("click", "#btnActivarAcumuladosISR", function () {
    activarID = $(this).data('id');
    $("#ActivarAcumuladosISR").modal();
});

//activar ejecutar
$("#btnActivarAcumuladosISREjecutar").click(function () {

    $.ajax({
        url: "/AcumuladosISR/Activar/" + activarID,
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
            cargarGridAcumuladosISR();
            $("#ActivarAcumuladosISR").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    activarID = 0;
});

//Modal editar despues de No Inactivar
$("#btnNoInactivar").click(function () {
    $("#EditarAcumuladosISR").modal();
    $("#InactivarAcumuladosISR").modal('hide');
});