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
                var botonDetalles = ListaAcumuladosISR[i].aisr_Activo == true ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" style="margin-right:3px;" class="btn btn-primary btn-xs"  id="btnDetalleAcumuladosISR">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaAcumuladosISR[i].aisr_Activo == true ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-default btn-xs"  id="btnEditarAcumuladosISR">Editar</button>' : '';

                //variable boton activar
                var botonActivar = ListaAcumuladosISR[i].aisr_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaAcumuladosISR[i].aisr_Id + '" type="button" class="btn btn-primary btn-xs"  id="btnActivarAcumuladosISR">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#tblAcumuladosISR').dataTable().fnAddData([
                    ListaAcumuladosISR[i].aisr_Id,
                    ListaAcumuladosISR[i].aisr_Descripcion,
                    (ListaAcumuladosISR[i].aisr_Monto % 1 == 0) ? ListaAcumuladosISR[i].aisr_Monto + ".00" : ListaAcumuladosISR[i].aisr_Monto,
                    estadoRegistro,
                    botonDetalles + botonEditar + botonActivar
                ]);
            }
        });
    FullBody();
}

//Modal Create Techos Deducciones
$(document).on("click", "#btnAgregarAcumuladosISR", function () {
    //OCULTAR SPAN - VALIDACION DECIMALES
    $("#Crear #Validation_decimal").css("display", "none");
    //DESBLOQUEAR EL BOTON
    $('#btnCreateAcumuladosISR').attr('disabled', false);
    //MOSTRAR EL MODAL DE AGREGAR
    $('#Crear input[type=text], input[type=number]').val('');
    $("#AgregarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
});

//BOTON CERRAR AGREGAR
$("#btnCerrarCrear").click(function () {
    $("#Crear #Validation_descripcion").css("display", "none");
    $("#Crear #Validation_descripcion2").css("display", "none");
    $("#Crear #AsteriscoDescripcionAISR").removeClass("text-danger");
    $("#Crear #AsteriscoMontoAISR").removeClass("text-danger");
});

//FUNCION: CREAR EL NUEVO REGISTRO TECHOS DEDUCCIONES
$('#btnCreateAcumuladosISR').click(function () {
    var aisr_Descripcion = $("#Crear #aisr_Descripcion").val();
    var aisr_Monto = $("#Crear #aisr_Monto").val();
    var ModelState = true;
    var ModelState2 = true;
    //VALIDAR DECIMALES
    var Montoe = aisr_Monto.split(".");

    if (aisr_Descripcion == "" || aisr_Descripcion == " " || aisr_Descripcion == null || isNaN(aisr_Descripcion) == false) {
        $("#Crear #Validation_descripcion").css("display", "block");
        $("#Crear #AsteriscoDescripcionAISR").addClass("text-danger");
        ModelState = false;
    }
    else {
        $("#Crear #Validation_descripcion").css("display", "none");
        $("#Crear #AsteriscoDescripcionAISR").removeClass("text-danger");
    }

    if (aisr_Monto == "" || parseInt(aisr_Monto) < 0 || aisr_Monto == null) {
        $("#Crear #Validation_descripcion2").css("display", "block");
        $("#Crear #AsteriscoMontoAISR").addClass("text-danger");
        ModelState2 = false;
    }
    else {
        //OCULTAR DATAANNOTATIONS
        $("#Crear #Validation_descripcion2").css("display", "none");
        $("#Crear #AsteriscoMontoAISR").removeClass("text-danger");
        if (Montoe[1]) {
            $("#Crear #Validation_decimal").css("display", "none");
            $("#Crear #AsteriscoMontoAISR").removeClass("text-danger");
        }
        else {
            $("#Crear #Validation_decimal").css("display", "block");
            $("#Crear #AsteriscoMontoAISR").addClass("text-danger");
            ModelState2e = false;
        }
        
    }


    if (ModelState == false || ModelState2 == false) {
        $('#btnCreateAcumuladosISR').attr('disabled', false);
    }
    else {
        $('#btnCreateAcumuladosISR').attr('disabled', true);

        $("#Crear #Validation_descripcion").css("display", "none");
        $("#Crear #Validation_descripcion2").css("display", "none");
        $("#Crear #AsteriscoDescripcionAISR").removeClass("text-danger");
        $("#Crear #AsteriscoMontoAISR").removeClass("text-danger");
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)
        var data = $("#frmAcumuladosISRCreate").serializeArray();

        $.ajax({
            url: "/AcumuladosISR/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            $("#AgregarAcumuladosISR").modal('hide');
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                //DESBLOQUEAR EL BOTON
                $('#btnCreateAcumuladosISR').attr('disabled', false);
                //MENSAJE DE ERROR
                iziToast.error({
                    title: 'Error',
                    message: 'No guardó el registro, contacte al administrador',
                });
            }
            else if (data == "bien") {
                cargarGridAcumuladosISR();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: '¡El registro se agregó de forma exitosa!',
                });
            }
        });
    }
});

//VariableGlobal de edicion
var Data_Edit = "";
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblAcumuladosISR tbody tr td #btnEditarAcumuladosISR", function () {
    //CAPTURA DEL ID
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
                //LLENADO DEL FORMULARIO DEL MODAL
                var montoFormato = (data.aisr_Monto % 1 == 0) ? data.aisr_Monto + ".00" : data.aisr_Monto;
                $("#Editar #aisr_Id").val(data.aisr_Id);
                $("#Editar #aisr_FechaCrea").val(data.aisr_FechaCrea);
                $("#Editar #aisr_UsuarioCrea").val(data.aisr_UsuarioCrea);
                $("#Editar #aisr_Descripcion").val(data.aisr_Descripcion);
                $("#Editar #aisr_Monto").val(montoFormato);

                $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
                $('#btnUpdateAISR2').attr('disabled', false);
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
    var aisr_Descripcione = $("#Editar #aisr_Descripcion").val();
    var aisr_Montoe = $("#Editar #aisr_Monto").val();
    var ModelStatee = true;
    var ModelState2e = true;
    var Montoe = aisr_Montoe.split(".");

    if (aisr_Descripcione == "" || aisr_Descripcione == " " || aisr_Descripcione == null) {
        $("#Editar #validatione1").css("display", "");
        $("#Editar #AsteriscoDescripcionEditAISR").addClass("text-danger");
        ModelStatee = false;
    }
    else {
        $("#Editar #validatione1").css("display", "none");
        $("#Editar #AsteriscoDescripcionEditAISR").removeClass("text-danger");
        //ModelStatee = true;
    }

    if (aisr_Montoe == "" || aisr_Montoe < 0 || aisr_Montoe == null) {
        $("#Editar #validatione2").css("display", "");
        $("#Editar #Validation_decimal").css("display", "none");
        $("#Editar #AsteriscoMontoEditAISR").addClass("text-danger");
        ModelState2e = false;
    }
    else {
        if (Montoe[1]) {
            $("#Editar #validatione2").css("display", "none");
            $("#Editar #Validation_decimal").css("display", "none");
            $("#Editar #AsteriscoMontoEditAISR").removeClass("text-danger");
        }
        else {
            $("#Editar #Validation_decimal").css("display", "block");
            $("#Editar #validatione2").css("display", "block");
            $("#Editar #AsteriscoMontoEditAISR").addClass("text-danger");
            ModelState2e = false;
        }
    }
    if (ModelStatee == false || ModelState2e == false) {
        $('#btnUpdateAISR2').attr('disabled', false);
    }
    else {
        //OCULTAR MODAL EDICION
        $("#EditarAcumuladosISR").modal('hide');
        //MOSTRAR MODAL DE CONFIRMACION
        $("#EditarAISRConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $('#btnUpdateAISR2').attr('disabled', false);
    }
});


///
$("#btnUpdateAISR2").click(function () {
    $('#btnUpdateAISR2').attr('disabled', true);
    var data = $("#frmEditAcumuladosISR").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/AcumuladosISR/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data != "error") {
            cargarGridAcumuladosISR();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarAcumuladosISR").modal('hide');
            $("#EditarAISRConfirmacion").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se editó de forma exitosa!',
            });
        }
        else {
            iziToast.error({
                title: 'Error',
                message: 'No se editó el registro, contacte al administrador',
            });
            $('#btnUpdateAISR2').attr('disabled', false);
            $("#EditarAISRConfirmacion").modal('hide');
        }
    });
});


///

//BOTON ICON CERRAR EDITAR
$("#IconCerrarEdit").click(function () {
    $("#Editar #validatione1").css("display", "none");
    $("#Editar #validatione2").css("display", "none");
    $("#Editar #AsteriscoDescripcionEditAISR").removeClass("text-danger");
    $("#Editar #AsteriscoMontoEditAISR").removeClass("text-danger");
});

//BOTON NO CERRAR EDITAR
$("#btnConfirmacionNOAISR").click(function () {

    $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
});
    //BOTON CERRAR EDITAR
    $("#btnCerrarEditar").click(function () {
        $("#Editar #validatione1").css("display", "none");
        $("#Editar #validatione2").css("display", "none");
        $("#Editar #AsteriscoDescripcionEditAISR").removeClass("text-danger");
        $("#Editar #AsteriscoMontoEditAISR").removeClass("text-danger");

    });

    //FUNCION: OCULTAR MODAL DE EDICIÓN
    $(document).on("click", "#btnInactivarAcumuladosISR", function () {
        $("#EditarAcumuladosISR").modal('hide');
        $("#InactivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
        $('#btnInactivarAcumuladosISREjecutar').attr('disabled', false);
    });

    //Inactivar registro Techos Deducciones
    $("#btnInactivarAcumuladosISREjecutar").click(function () {
        $('#btnInactivarAcumuladosISREjecutar').attr('disabled', true);
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
                $('#btnInactivarAcumuladosISREjecutar').attr('disabled', false);
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

    //Modal editar despues de No Inactivar
    $("#btnNoInactivar").click(function () {
        $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
        $("#InactivarAcumuladosISR").modal('hide');
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
                    $("#DetailsAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
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
        $("#ActivarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
        $('#btnActivarAcumuladosISREjecutar').attr('disabled', false);
    });

    //activar ejecutar
    $("#btnActivarAcumuladosISREjecutar").click(function () {
        $('#btnActivarAcumuladosISREjecutar').attr('disabled', true);

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
                $('#btnActivarAcumuladosISREjecutar').attr('disabled', false);
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
        $("#EditarAcumuladosISR").modal({ backdrop: 'static', keyboard: false });
        $("#InactivarAcumuladosISR").modal('hide');
    });
