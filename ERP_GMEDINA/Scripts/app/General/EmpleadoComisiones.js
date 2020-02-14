//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
    })
    .fail(function (jqxhr, settings, exception) {

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

var Idinactivar = 0;

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function cargarGridComisiones() {
    var esAdministrador = $("#rol_Usuario").val();
    _ajax(null,
        '/EmpleadoComisiones/GetData',
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
            var ListaComisiones = data;
            //Limpiar Data del DataTable
            $('#tblEmpleadoComisiones').DataTable().clear();
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaComisiones.length; i++) {
                var estadoRegistro = ListaComisiones[i].cc_Activo == false ? 'Inactivo' : 'Activo';
                //var PagadoRegistro = ListaComisiones[i].cc_Pagado == false ? 'Inactivo' : 'Activo'
                //variable boton detalles
                var botonDetalles = '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-primary btn-xs"  id="btnDetalleEmpleadoComisiones">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaComisiones[i].cc_Activo == true ? '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-default btn-xs"  id="btnEditarEmpleadoComisiones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaComisiones[i].cc_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-default btn-xs"  id="btnActivarRegistroComisiones">Activar</button>' : '' : '';

                var FechaRegistro = FechaFormato(ListaComisiones[i].cc_FechaRegistro);

                var Check = "";
                //ESTA VARIABLE GUARDA CODIGO HTML DE UN CHECKBOX, PARA ENVIARLO A LA TABLA
                if (ListaComisiones[i].cc_Pagado == true) {
                    Check = '<input type="checkbox" id="cc_Pagado" name="cc_Pagado" checked disabled>'; //SE LLENA LA VARIABLE CON UN INPUT CHEQUEADO
                } else {
                    Check = '<input type="checkbox" id="cc_Pagado" name="cc_Pagado" disabled>'; //SE LLENA LA VARIABLE CON UN INPUT QUE NO ESTA CHEQUEADO
                }

                $('#tblEmpleadoComisiones').dataTable().fnAddData([
                    ListaComisiones[i].cc_Id,
                    ListaComisiones[i].per_Nombres + ' ' + ListaComisiones[i].per_Apellidos,
                    ListaComisiones[i].cin_DescripcionIngreso,
                    (ListaComisiones[i].cc_TotalVenta % 1 == 0) ? ListaComisiones[i].cc_TotalVenta + ".00" : ListaComisiones[i].cc_TotalVenta,
                    (ListaComisiones[i].cc_TotalComision % 1 == 0) ? ListaComisiones[i].cc_TotalComision + ".00" : ListaComisiones[i].cc_TotalComision,
                    FechaRegistro,
                    estadoRegistro,
                    Check,
                    botonDetalles + botonEditar + botonActivar]
                );
            }
        });
    (FullBody);
}
$(document).ready(function () {
    $.ajax({
        url: "/AdelantoSueldo/EmpleadoGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $('#Crear #emp_IdEmpleado').select2({
            dropdownParent: $('#Crear'),
            placeholder: 'Seleccione un empleado',
            allowClear: true,
            language: {
                noResults: function () {
                    return 'Resultados no encontrados.';
                },
                searching: function () {
                    return 'Buscando...';
                }
            },
            data: data.results
        });

        $('#Editar #emp_Id').select2({
            dropdownParent: $('#Editar'),
            placeholder: 'Seleccione un empleado',
            allowClear: true,
            language: {
                noResults: function () {
                    return 'Resultados no encontrados.';
                },
                searching: function () {
                    return 'Buscando...';
                }
            },
            data: data.results
        });
    });
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoComisiones", function () {
    var validacionPermiso = userModelState("EmpleadoComisiones/Create");

    if (validacionPermiso.status == true) {
        $("#Crear #emp_IdEmpleado").val('').trigger('change');
        //OCULTAR VALIDACIONES
        OcultarValidacionesCrear();
        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCreateRegistroComisiones").attr("disabled", false);
        //RECUPERAR LA DATA PARA LLENAR EL DROPDOWNLIST DE INGRESO
        $.ajax({
            url: "/EmpleadoComisiones/EditGetDDLIngreso",
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
            //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
            .done(function (data) {
                //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                $("#Crear #cin_IdIngreso").empty();
                //LLENAR EL DROPDOWNLIST
                $.each(data, function (i, iter) {
                    $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                });
            });

        //MOSTRAR EL MODAL DE AGREGAR
        $("#AgregarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
    }
});

//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroComisiones').click(function () {
    var Empleado = $("#Crear #emp_IdEmpleado").val();
    var Ingreso = $("#Crear #cin_IdIngreso").val();
    //var Porcentaje = $("#Crear #PorcentajeComision").val();
    var Total = $("#Crear #TotalVenta").val();

    if (ValidarCamposCrear(Empleado, Ingreso, Total)) {

        //BLOQUEAR EL BOTON DE CREAR       
        $("#btnCreateRegistroComisiones").attr("disabled", true);
        //CONVERTIR EN ARRAY EL TOTAL A PARTIR DEL SEPARADOR DE MILLARES
        var indicest = $("#Crear #TotalVenta").val().split(",");
        //VARIABLE CONTENEDORA DEL TOTAL
        var TotalFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY TOTAL
        for (var i = 0; i < indicest.length; i++) {
            //SETEAR LA VARIABLE DE TOTAL
            TotalFormateado += indicest[i];
        }
        TotalFormateado = parseFloat(TotalFormateado);

        var data = {
            emp_Id: $("#Crear #emp_IdEmpleado").val(),
            cin_IdIngreso: $("#Crear #cin_IdIngreso").val(),
            cc_TotalVenta: TotalFormateado,
        };

        //REALIZAR LA PETICION AL SERVIDOR
        $.ajax({
            url: "/EmpleadoComisiones/Create",
            method: "POST",
            data: data
        })
            .done(function (data) {
                //VALIDAR ERROR
                if (data == "error") {
                    $("#AgregarEmpleadoComisiones").modal('hide');
                    //DESBLOQUEAR EL BOTON DE CREAR
                    $("#btnCreateRegistroComisiones").attr("disabled", false);
                    //MOSTRAR MENSAJE DE ERROR
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se guardó el registro, contacte al administrador!',
                    });
                }
                else {
                    //REFRESCAR LA DATA DEL DATATABLE                    
                    cargarGridComisiones();
                    //OCULTAR MODAL DE CREACION
                    $("#AgregarEmpleadoComisiones").modal('hide');
                    OcultarValidacionesCrear();
                    //MOSTRAR MENSAJE DE EXITO
                    iziToast.success({
                        title: 'Éxito',
                        message: '¡El registro se agregó de forma exitosa!',
                    });
                }
            });
    }

});

//EVITAR EL POSTBACK DEL FORMULARIO DE CREACION
$("#frmEmpleadoComisionesCreate").submit(function (e) {
    e.preventDefault();
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModalCrear").click(function () {
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarEmpleadoComisiones").modal();
    //OCULTAR VALIDACIONES
    OcultarValidacionesCrear();
});




//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnEditarEmpleadoComisiones", function () {
    var validacionPermiso = userModelState("EmpleadoComisiones/Edit");

    if (validacionPermiso.status == true) {
        let itemEmpleado = localStorage.getItem('idEmpleado');
        let dataEmp = table.row($(this).parents('tr')).data(); //obtener la data de la fila seleccionada
        console.table(dataEmp);
        if (itemEmpleado != null) {
            $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove();
            localStorage.removeItem('idEmpleado');
        }
        //OCULTAR DATAANNOTATIONS
        $("#Editar #Validation_descipcion1e").css("display", "hidden");
        $("#Editar #Validation_descipcion2e").css("display", "hidden");
        $("#Editar #Validation_descipcion1").css("display", "none");
        $("#Editar #Validation_descipcion2").css("display", "hidden");
        $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
        $("#Editar #AsteriscoTotal").removeClass("text-danger");
        var ID = $(this).data('id');
        Idinactivar = ID;
        var idEmpSelect = "";
        var NombreSelect = "";
        $.ajax({
            url: "/EmpleadoComisiones/Edit/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    if (data.cc_Pagado) {
                        $("#btnUpdateComisionesConfirmar").attr('disabled', true);
                    } else {
                        $("#btnUpdateComisionesConfirmar").attr('disabled', false);
                    }
                    idEmpSelect = data.emp_Id;
                    NombreSelect = data.Descripcion;
                    $('#Editar #emp_Id').val(idEmpSelect).trigger('change');

                    let valor = $('#Editar #emp_Id').val();

                    if (valor == null) {
                        $("#Editar #emp_Id").prepend("<option value='" + idEmpSelect + "' selected>" + dataEmp[1] + "</option>").trigger('change');
                        localStorage.setItem('idEmpleado', idEmpSelect);
                    }

                    $("#Editar #cc_Id").val(data.cc_Id);
                    $("#Editar #cc_TotalVenta").val(data.cc_TotalVenta);

                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedIdEmp = data.emp_Id;
                    var SelectedIdIng = data.cin_IdIngreso;

                    $.ajax({
                        url: "/EmpleadoComisiones/EditGetDDLIngreso",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            $("#Editar #cin_IdIngreso").empty();
                            //LLENAR EL DROPDOWNLIST
                            $.each(data, function (i, iter) {
                                $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdIng ? " selected" : "") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });

                        });
                    $("#EditarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });


                    //$("#DetalleEmpleadoComisiones").modal(hide);
                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: 'No se pudo cargar la información, contacte al administrador',
                    });
                }
                Check = "";
            });
    }
});

//BOTON DE CONFIRMAR EDICION
$('#btnUpdateComisionesConfirmar').click(function () {
    var Colaborador = $("#Editar #emp_Id").val();
    var idIngreso = $("#Editar #cin_IdIngreso").val();
    var TotalVenta = $("#Editar #cc_TotalVenta").val();
    if (ValidarCamposEditar(Colaborador, idIngreso, TotalVenta)) {
        $("#EditarEmpleadoComisiones").modal('hide');
        //DESBLOQUEAR EL BOTON
        $("#btnUpdateComisionesConfirmar2").attr("disabled", false);
        $("#EditarEmpleadoComisionesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        OcultarValidacionesEditar();
    }
});

//BOTON DE CERRAR EL MODAL DE CONFIRMAR EDITAR
$("#btCerrarEditar").click(function () {
    //OCULTAR MODAL DE CONFIRMAR EDITAR
    $("#EditarEmpleadoComisionesConfirmacion").modal('hide');
    //MOSTRAR MODAL DE EDITAR
    $("#EditarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
});

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModaledit").click(function () {
    //OCULTAR VALIDACIONES
    OcultarValidacionesEditar();
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateComisionesConfirmar2").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnUpdateComisionesConfirmar2").attr("disabled", true);



    //CONVERTIR EN ARRAY EL TOTAL A PARTIR DEL SEPARADOR DE MILLARES
    var indicest = $("#Editar #cc_TotalVenta").val().replace(/,/g, '');;

    //ITERAR LOS INDICES DEL ARRAY TOTAL
    var data = {
        cc_Id: $("#Editar #cc_Id").val(),
        emp_Id: $("#Editar #emp_Id").val(),
        cin_IdIngreso: $("#Editar #cin_IdIngreso").val(),
        //cc_PorcentajeComision: PorcentajeFormateado,
        cc_TotalVenta: indicest
    };
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN   
    $.ajax({
        url: "/EmpleadoComisiones/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
            //DESBLOQUEAR EL BOTON
            $("#btnUpdateComisionesConfirmar2").attr("disabled", false);
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se editó el registro, contacte al administrador!',
            });
            $("#EditarEmpleadoComisionesConfirmacion").modal('hide');
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarEmpleadoComisiones").modal('hide');
            $("#EditarEmpleadoComisionesConfirmacion").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se editó de forma exitosa!',
            });

        }
    });
});

// EVITAR POSTBACK DE FORMULARIOS
$("#frmEmpleadoComisionesEditar").submit(function (e) {
    return false;
});




//EVENTOS DE DETALLES
$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnDetalleEmpleadoComisiones", function () {
    var validacionPermiso = userModelState("EmpleadoComisiones/Details");

    if (validacionPermiso.status == true) {
        var ID = $(this).data('id');
        Editar = ID;

        $.ajax({
            url: "/EmpleadoComisiones/Details/" + ID,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID: ID })
        })
            .done(function (data) {
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    var FechaRegistro = FechaFormato(data[0].cc_FechaRegistro);
                    var FechaCrea = FechaFormato(data[0].cc_FechaCrea);
                    var FechaModifica = FechaFormato(data[0].cc_FechaModifica);
                    if (data[0].cc_Pagado) {
                        //$('#Detalles #cb_Pagado').prop('checked', true);
                        $("#Detallar #cc_Pagado").html("Si");
                    } else {
                        $("#Detallar #cc_Pagado").html("No");
                    }
                    $("#Detallar #cc_Id").html(data[0].cc_Id);
                    $("#Detallar #cc_FechaRegistro").html(FechaRegistro);
                    $("#Detallar #cc_UsuarioCrea").html(data[0].cc_UsuarioCrea);
                    $("#Detallar #tbUsuario_usu_NombreUsuario").html(data[0].UsuCrea);
                    $("#Detallar #tbCatalogoDeIngresos_cin_DescripcionIngreso").html(data[0].Ingreso);
                    $("#Detallar #cin_IdIngreso").html(data[0].cin_IdIngreso);
                    $("#Detallar #emp_Id").html(data[0].emp_Id);
                    $("#Detallar #tbEmpleados_tbPersonas_per_Nombres").html(data[0].NombreEmpleado + ' ' + data[0].ApellidosEmpleado);
                    $("#Detallar #cc_FechaCrea").html(FechaCrea);
                    $("#Detallar #cc_UsuarioModifica").html(data[0].cc_UsuarioModifica);
                    $("#Detallar #cc_PorcentajeComision").html((data[0].cc_PorcentajeComision % 1 == 0) ? data[0].cc_PorcentajeComision + ".00" : data[0].cc_PorcentajeComision);
                    $("#Detallar #cc_TotalVenta").html((data[0].cc_TotalVenta % 1 == 0) ? data[0].cc_TotalVenta + ".00" : data[0].cc_TotalVenta);
                    data[0].UsuModifica == null ? $("#Detallar #tbUsuario1_usu_NombreUsuario").html('Sin modificaciones') : $("#Detallar #tbUsuario1_usu_NombreUsuario").html(data[0].UsuModifica);
                    $("#Detallar #cc_FechaModifica").html(FechaModifica);
                    //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                    var SelectedIdEmp = data[0].emp_Id;
                    var SelectedIdCatIngreso = data[0].cin_IdIngreso;
                    //CARGAR INFORMACIÓN DEL DROPDOWNLIST PARA EL MODAL
                    $.ajax({
                        url: "/EmpleadoComisiones/EditGetDDLEmpleado",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            $("#Detallar #emp_IdEmpleado").empty();
                            //LLENAR EL DROPDOWNLIST
                            $("#Detallar #emp_IdEmpleado").append("<option value=0>Selecione una opción...</option>");
                            $.each(data, function (i, iter) {
                                $("#Detallar #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });
                        });

                    $.ajax({
                        url: "/EmpleadoComisiones/EditGetDDLIngreso",
                        method: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ ID })
                    })
                        .done(function (data) {
                            //LIMPIAR EL DROPDOWNLIST ANTES DE VOLVER A LLENARLO
                            $("#Detallar #cin_IdIngreso").empty();
                            //LLENAR EL DROPDOWNLIST

                            $.each(data, function (i, iter) {
                                $("#Detallar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                            });
                        });
                    $("#DetalleEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });


                }
                else {
                    //Mensaje de error si no hay data
                    iziToast.error({
                        title: 'Error',
                        message: '¡No se cargó la información, contacte al administrador!',
                    });
                }
            });
    }
});

//EVENTOS DE INACTIVACION
$(document).on("click", "#btnInactivarEmpleadoComisiones", function () {
    var validacionPermiso = userModelState("EmpleadoComisiones/Inactivar");

    if (validacionPermiso.status == true) {
        //MOSTRAR EL MODAL DE INACTIVAR
        $("#EditarEmpleadoComisiones").modal('hide');
        $("#InactivarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });

        document.getElementById("btnInactivarRegistroComisiones").disabled = false;
        document.getElementById("btnInactivarRegistroComisionesNo").disabled = false;
    }
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroComisiones").click(function () {
    OcultarValidacionesEditar();
    document.getElementById("btnInactivarRegistroComisiones").disabled = true;
    var data = $("#frmEmpleadoComisionesInactivar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoComisiones/Inactivar/" + Idinactivar,
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
            // REFRESCAR UNICAMENTE LA TABLA

            cargarGridComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#InactivarEmpleadoComisiones").modal('hide');
            $("#EditarEmpleadoComisiones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: '¡El registro se inactivó de forma exitosa!',
            });
        }
    });
});

//CERRAR MODAL DE INACTIVAR
$("#btnInactivarRegistroComisionesNo").click(function () {
    document.getElementById("btnInactivarRegistroComisionesNo").disabled = true;
    $("#InactivarEmpleadoComisiones").modal('hide');
    $("#EditarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });



});




//EVENTOS DE ACTIVACION 
var IDActivar = 0;
$(document).on("click", "#btnActivarRegistroComisiones", function () {
    var validacionPermiso = userModelState("EmpleadoComisiones/Activar");

    if (validacionPermiso.status == true) {
        IDActivar = $(this).data('id');
        $("#ActivarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });

        document.getElementById("btnActivarRegistroComisionesEjecutar").disabled = false;
    }
});

//CONFIRMAR ACTIVACION
$("#btnActivarRegistroComisionesEjecutar").click(function () {
    document.getElementById("btnActivarRegistroComisionesEjecutar").disabled = true;
    $.ajax({
        url: "/EmpleadoComisiones/Activar/" + IDActivar,
        method: "POST",
        data: { id: IDActivar }
    }).done(function (data) {
        if (data == "error") {
            //Cuando traiga un error del backend al guardar la edicion
            iziToast.error({
                title: 'Error',
                message: '¡No se activó el registro, contacte al administrador',
            });
        }
        else {
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#ActivarEmpleadoComisiones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Éxito',
                message: '¡El registro se activó de forma exitosa!',
            });
        }
    });
    IDActivar = 0;
});




//FUNCION: VALIDAR LOS CAMPOS DE CREAR
function ValidarCamposCrear(Empleado, Ingreso, Porcentaje, Total) {
    var pasoValidacion = true;

    //CONVERTIR EN ARRAY EL PORCENTAJE A PARTIR DEL SEPARADOR DE MILLARES
    //var indicesp = $("#Crear #PorcentajeComision").val().split(",");
    ////VARIABLE CONTENEDORA DEL PORCENTAJE
    //var PorcentajeFormateado = "";
    ////ITERAR LOS INDICES DEL ARRAY PORCENTAJE
    //for (var i = 0; i < indicesp.length; i++) {
    //    //SETEAR LA VARIABLE DE PORCENTAJE
    //    PorcentajeFormateado += indicesp[i];
    //}

    ////FORMATEAR A DECIMAL
    //PorcentajeFormateado = parseFloat(PorcentajeFormateado);

    //CONVERTIR EN ARRAY EL TOTAL A PARTIR DEL SEPARADOR DE MILLARES
    var indicest = $("#Crear #TotalVenta").val().split(",");
    //VARIABLE CONTENEDORA DEL TOTAL
    var TotalFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY TOTAL
    for (var i = 0; i < indicest.length; i++) {
        //SETEAR LA VARIABLE DE TOTAL
        TotalFormateado += indicest[i];
    }
    TotalFormateado = parseFloat(TotalFormateado);

    if (Empleado != "-1") {
        //VALIDAR EL ID DEL EMPLEADO
        if (Empleado == null || Empleado == "") {
            $("#Crear #AsteriscoEmpleado").addClass("text-danger");
            $("#Crear #Validation_empleado").show();
            pasoValidacion = false;
        } else {
            $("#Crear #AsteriscoEmpleado").removeClass("text-danger");
            $("#Crear #Validation_empleado").hide();
        }
    }
    if (Ingreso != "-1") {
        //VALIDAR EL ID DEL INGRESO
        if (Ingreso == "0") {
            $("#Crear #AsteriscoComision").addClass("text-danger");
            $("#Crear #Validation_idIngreso").show();
            pasoValidacion = false;
        } else {
            $("#Crear #AsteriscoComision").removeClass("text-danger");
            $("#Crear #Validation_idIngreso").hide();
        }
    }

    //if (Porcentaje != "-1") {
    //    //VALIDAR EL PORCENTAJE COMISION
    //    if (isNaN(PorcentajeFormateado)) {
    //        $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
    //        $("#Crear #Validation_porcentaje").show();
    //        $("#Crear #Validation_porcentaje2").hide();
    //        $("#Crear #Validation_porcentaje3").hide();
    //        pasoValidacion = false;
    //    } else {
    //        $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
    //        $("#Crear #Validation_porcentaje").hide();
    //        if (PorcentajeFormateado <= 0) {
    //            $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
    //            $("#Crear #Validation_porcentaje").hide();
    //            $("#Crear #Validation_porcentaje2").show();
    //            $("#Crear #Validation_porcentaje3").hide();
    //            pasoValidacion = false;
    //        } else {
    //            $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
    //            $("#Crear #Validation_porcentaje2").hide();
    //            if (Porcentaje > 100) {
    //                $("#Crear #AsteriscoPorcentaje").addClass("text-danger");
    //                $("#Crear #Validation_porcentaje").hide();
    //                $("#Crear #Validation_porcentaje2").hide();
    //                $("#Crear #Validation_porcentaje3").show();
    //                pasoValidacion = false;
    //            } else {
    //                $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
    //                $("#Crear #Validation_porcentaje3").hide();
    //            }
    //        }
    //    }
    //}

    if (Total != "-1") {
        //VALIDAR EL TOTAL DE VENTA
        if (isNaN(TotalFormateado)) {
            $("#Crear #AsteriscoTotal").addClass("text-danger");
            $("#Crear #Validation_total").show();
            $("#Crear #Validation_total2").hide();
            pasoValidacion = false;
        } else {
            $("#Crear #AsteriscoTotal").removeClass("text-danger");
            $("#Crear #Validation_total").hide();
            if (TotalFormateado <= 0) {
                $("#Crear #AsteriscoTotal").addClass("text-danger");
                $("#Crear #Validation_total").hide();
                $("#Crear #Validation_total2").show();
                pasoValidacion = false;
            } else {
                $("#Crear #AsteriscoTotal").removeClass("text-danger");
                $("#Crear #Validation_total").hide();
                $("#Crear #Validation_total2").hide();
            }
        }
    }

    return pasoValidacion;
}

//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE CREAR
function OcultarValidacionesCrear() {
    //VACIAR LOS CAMPOS
    $("#Crear #emp_IdEmpleado").val(0);
    $("#Crear #cin_IdIngreso").val(0);
    $("#Crear #PorcentajeComision").val("");
    $("#Crear #TotalVenta").val("");

    $("#Crear #AsteriscoEmpleado").removeClass("text-danger");
    $("#Crear #Validation_empleado").hide();

    $("#Crear #AsteriscoComision").removeClass("text-danger");
    $("#Crear #Validation_idIngreso").hide();

    $("#Crear #AsteriscoPorcentaje").removeClass("text-danger");
    $("#Crear #Validation_porcentaje").hide();
    $("#Crear #Validation_porcentaje2").hide();
    $("#Crear #Validation_porcentaje3").hide();

    $("#Crear #AsteriscoTotal").removeClass("text-danger");
    $("#Crear #Validation_total").hide();
    $("#Crear #Validation_total2").hide();
}

//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(Empleado, Ingreso, Porcentaje, Total) {
    var pasoValidacion = true;

    //CONVERTIR EN ARRAY EL PORCENTAJE A PARTIR DEL SEPARADOR DE MILLARES
    //var indicesp = $("#Editar #cc_PorcentajeComision").val().split(",");
    ////VARIABLE CONTENEDORA DEL PORCENTAJE
    //var PorcentajeFormateado = "";
    ////ITERAR LOS INDICES DEL ARRAY PORCENTAJE
    //for (var i = 0; i < indicesp.length; i++) {
    //    //SETEAR LA VARIABLE DE PORCENTAJE
    //    PorcentajeFormateado += indicesp[i];
    //}

    ////FORMATEAR A DECIMAL
    //PorcentajeFormateado = parseFloat(PorcentajeFormateado);

    //CONVERTIR EN ARRAY EL TOTAL A PARTIR DEL SEPARADOR DE MILLARES
    var indicest = $("#Editar #cc_TotalVenta").val().split(",");
    //VARIABLE CONTENEDORA DEL TOTAL
    var TotalFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY TOTAL
    for (var i = 0; i < indicest.length; i++) {
        //SETEAR LA VARIABLE DE TOTAL
        TotalFormateado += indicest[i];
    }
    TotalFormateado = parseFloat(TotalFormateado);

    if (Empleado != "-1") {
        //VALIDAR EL ID DEL EMPLEADO
        if (Empleado == "0" || Empleado == 0) {
            $("#Editar #AsteriscoEmpleado").addClass("text-danger");
            $("#Editar #Validation_empleado").show();
            pasoValidacion = false;
        } else {
            $("#Editar #AsteriscoEmpleado").removeClass("text-danger");
            $("#Editar #Validation_empleado").hide();
        }
    }

    if (Ingreso != "-1") {
        //VALIDAR EL ID DEL INGRESO
        if (Ingreso == "0") {
            $("#Editar #AsteriscoComision").addClass("text-danger");
            $("#Editar #Validation_idIngreso").show();
            pasoValidacion = false;
        } else {
            $("#Editar #AsteriscoComision").removeClass("text-danger");
            $("#Editar #Validation_idIngreso").hide();
        }
    }

    //if (Porcentaje != "-1") {
    //    //VALIDAR EL PORCENTAJE COMISION
    //    if (isNaN(PorcentajeFormateado)) {
    //        $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
    //        $("#Editar #Validation_porcentaje").show();
    //        $("#Editar #Validation_porcentaje2").hide();
    //        $("#Editar #Validation_porcentaje3").hide();
    //        pasoValidacion = false;
    //    } else {
    //        $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
    //        $("#Editar #Validation_porcentaje").hide();
    //        if (PorcentajeFormateado <= 0) {
    //            $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
    //            $("#Editar #Validation_porcentaje").hide();
    //            $("#Editar #Validation_porcentaje2").show();
    //            $("#Editar #Validation_porcentaje3").hide();
    //            pasoValidacion = false;
    //        } else {
    //            $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
    //            $("#Editar #Validation_porcentaje2").hide();
    //            if (Porcentaje > 100) {
    //                $("#Editar #AsteriscoPorcentaje").addClass("text-danger");
    //                $("#Editar #Validation_porcentaje").hide();
    //                $("#Editar #Validation_porcentaje2").hide();
    //                $("#Editar #Validation_porcentaje3").show();
    //                pasoValidacion = false;
    //            } else {
    //                $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
    //                $("#Editar #Validation_porcentaje3").hide();
    //            }
    //        }
    //    }
    //}

    if (Total != "-1") {
        //VALIDAR EL TOTAL DE VENTA
        if (isNaN(TotalFormateado)) {
            $("#Editar #AsteriscoTotal").addClass("text-danger");
            $("#Editar #Validation_total").show();
            $("#Editar #Validation_total2").hide();
            pasoValidacion = false;
        } else {
            $("#Editar #AsteriscoTotal").removeClass("text-danger");
            $("#Editar #Validation_total").hide();
            if (TotalFormateado <= 0) {
                $("#Editar #AsteriscoTotal").addClass("text-danger");
                $("#Editar #Validation_total").hide();
                $("#Editar #Validation_total2").show();
                pasoValidacion = false;
            } else {
                $("#Editar #AsteriscoTotal").removeClass("text-danger");
                $("#Editar #Validation_total").hide();
                $("#Editar #Validation_total2").hide();
            }
        }
    }

    return pasoValidacion;
}

//FUNCION: OCULTAR LOS MENSAJES DE VALIDACION DEL MODAL DE EDITAR
function OcultarValidacionesEditar() {

    $("#Editar #AsteriscoEmpleado").removeClass("text-danger");
    $("#Editar #Validation_empleado").hide();

    $("#Editar #AsteriscoComision").removeClass("text-danger");
    $("#Editar #Validation_idIngreso").hide();

    $("#Editar #AsteriscoPorcentaje").removeClass("text-danger");
    $("#Editar #Validation_porcentaje").hide();
    $("#Editar #Validation_porcentaje2").hide();
    $("#Editar #Validation_porcentaje3").hide();

    $("#Editar #AsteriscoTotal").removeClass("text-danger");
    $("#Editar #Validation_total").hide();
    $("#Editar #Validation_total2").hide();
}