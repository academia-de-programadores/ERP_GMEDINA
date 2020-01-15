//OBTENER SCRIPT DE FORMATEO DE FECHA // 
$.getScript("../Scripts/app/General/SerializeDate.js")
    .done(function (script, textStatus) {
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
                var estadoRegistro = ListaComisiones[i].cc_Activo == false ? 'Inactivo' : 'Activo'
                //var PagadoRegistro = ListaComisiones[i].cc_Pagado == false ? 'Inactivo' : 'Activo'
                //variable boton detalles
                var botonDetalles = ListaComisiones[i].cc_Activo == true ? '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-primary btn-xs"  id="btnDetalleEmpleadoComisiones">Detalles</button>' : '';

                //variable boton editar
                var botonEditar = ListaComisiones[i].cc_Activo == true ? '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-default btn-xs"  id="btnEditarEmpleadoComisiones">Editar</button>' : '';

                //variable donde está el boton activar
                var botonActivar = ListaComisiones[i].cc_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" style="margin rigth:3px;" class="btn btn-primary btn-xs"  id="btnActivarRegistroComisiones">Activar</button>' : '' : '';

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
                    ListaComisiones[i].cc_PorcentajeComision,
                    ListaComisiones[i].cc_TotalVenta,
                    FechaRegistro,
                    estadoRegistro,
                    Check,
                    botonDetalles + botonEditar + botonActivar]
                    );
            }
        });
    (FullBody);
}


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO


$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnEditarEmpleadoComisiones", function () {
    //OCULTAR DATAANNOTATIONS
    $("#Editar #Validation_descipcion1e").css("display", "hidden");
    $("#Editar #Validation_descipcion2e").css("display", "hidden");
    $("#Editar #Validation_descipcion1").css("display", "hidden");
    $("#Editar #Validation_descipcion2").css("display", "hidden");
    $("#Editar #Validation_descipcion11").css("display", "");
    $("#Editar #Validation_descipcion12").css("display", "");
    $("#Editar #Validation_descipcion13").css("display", "");
    $("#Editar #Validation_descipcion14").css("display", "");
    $("#Editar #Validation_descipcion15").css("display", "");
    var ID = $(this).data('id');
    Idinactivar = ID;
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
                    document.getElementById("btnUpdateComisionesConfirmar").disabled = true;
                } else {
                    document.getElementById("btnUpdateComisionesConfirmar").disabled = false;
                }
                $("#Editar #cc_Id").val(data.cc_Id);
                $("#Editar #cc_PorcentajeComision").val(data.cc_PorcentajeComision);
                $("#Editar #cc_TotalVenta").val(data.cc_TotalVenta);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedIdEmp = data.emp_Id;
                var SelectedIdIng = data.cin_IdIngreso;
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
                        $("#Editar #emp_IdEmpleado").empty();
                        //LLENAR EL DROPDOWNLIST
                        $.each(data, function (i, iter) {
                            $("#Editar #emp_IdEmpleado").append("<option" + (iter.Id == SelectedIdEmp ? " selected" : "") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
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
                       $("#Editar #cin_IdIngreso").empty();
                       //LLENAR EL DROPDOWNLIST
                       $.each(data, function (i, iter) {
                           $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdIng ? " selected" : "") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                       });

                   });
                $("#EditarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
                $("html, body").css("overflow", "hidden");
                $("html, body").css("overflow", "scroll");
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
});

$('#btnUpdateComisionesConfirmar').click(function () {
    var PorcentajeComision = $("#Editar #cc_PorcentajeComision").val();
    var TotalVenta = $("#Editar #cc_TotalVenta").val();
    if (PorcentajeComision == "" && TotalVenta == "" || PorcentajeComision == "0.00" && TotalVenta == "0.00" || PorcentajeComision == null && TotalVenta == null || PorcentajeComision == undefined && TotalVenta == undefined) {
        $("#Editar #Validation_descipcion1e").css("display", "block");
        $("#Editar #Validation_descipcion1").css("display", "");
        $("#Editar #Validation_descipcion11").css("display", "none");
        $("#Editar #Validation_descipcion12").css("display", "none");
        $("#Editar #Validation_descipcion2e").css("display", "block");
        $("#Editar #Validation_descipcion2").css("display", "");
    }
    else if (TotalVenta == "" || TotalVenta == "0.00" || TotalVenta == null || TotalVenta == undefined) {
        $("#Editar #Validation_descipcion2e").css("display", "block");
        $("#Editar #Validation_descipcion1").css("display", "none");
        $("#Editar #Validation_descipcion12").css("display", "none");
        $("#Editar #Validation_descipcion2").css("display", "");

    }
    else if (PorcentajeComision == "" || PorcentajeComision == "0.00" || PorcentajeComision == null || PorcentajeComision == undefined) {

        //MOSTRAR DATAANNOTATIONS
        $("#Editar #Validation_descipcion1e").css("display", "block");
        $("#Editar #Validation_descipcion1").css("display", "");
        $("#Editar #Validation_descipcion11").css("display", "none");
        $("#Editar #Validation_descipcion2").css("display", "none");
        //MOSTRAR MODAL
        $("#EditarEmpleadoComisionesConfirmacion").modal('hide');
    }

    else {
        $("#Editar #Validation_descipcion1").css("display", "none");
        $("#Editar #Validation_descipcion2").css("display", "none");
        $("#EditarEmpleadoComisionesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $("html, body").css("overflow", "hidden");
        $("html, body").css("overflow", "scroll");
        $("#EditarEmpleadoComisiones").modal('hide');
    }

});

$("#btCerrarEditar").click(function () {
    $("#EditarEmpleadoComisiones").modal();
    $("#EditarEmpleadoComisionesConfirmacion").modal('hide');
})

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateComisionesConfirmar2").click(function () {
    document.getElementById("btnUpdateComisionesConfirmar2").disabled = true;
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEmpleadoComisionesEditar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN   
    $.ajax({
        url: "/EmpleadoComisiones/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
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
$("#frmEmpleadoComisiones").submit(function (e) {
    return false;
});
// EVITAR POSTBACK DE FORMULARIOS
$("#frmEmpleadoComisionesEditar").submit(function (e) {
    return false;
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoComisiones", function () {
    var cc_Pagado = $('#AgregarEmpleadoComisiones #cc_Pagado').prop('checked', false);
    document.getElementById("btnCreateRegistroComisiones").disabled = false;
    $("#PorcentajeComision").val('');
    $("#TotalVenta").val('');
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $.ajax({
        url: "/EmpleadoComisiones/EditGetDDLEmpleado",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"

    })
        //LLENAR EL DROPDONWLIST DEL MODAL CON LA DATA OBTENIDA
        .done(function (data) {

            $("#Crear #emp_IdEmpleado").empty();
            $("#Crear #emp_IdEmpleado").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #emp_IdEmpleado").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });
    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoComisiones", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
    $("#Validation_descipcion4").css("display", "none");
    $("#Validation_descipcion5").css("display", "none");
    $("#Validation_descipcion6").css("display", "none");
    $("#Validation_descipcion7").css("display", "none");
 
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
            $("#Crear #cin_IdIngreso").append("<option value='0'>Selecione una opción...</option>");
            $.each(data, function (i, iter) {
                $("#Crear #cin_IdIngreso").append("<option value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
            });
        });

    //MOSTRAR EL MODAL DE AGREGAR
    $("#AgregarEmpleadoComisiones").modal();
});
//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroComisiones').click(function () {
    var Empleado = $("#Crear #emp_IdEmpleado").val();
    var Ingreso = $("#Crear #cin_IdIngreso").val();
    var Porcentaje = $("#Crear #PorcentajeComision").val();
    var Total = $("#Crear #TotalVenta").val();
    document.getElementById("btnCreateRegistroComisiones").disabled = true;
    if (Empleado != "0" && Ingreso != "0" && Porcentaje != "" && Porcentaje != "0" && Total != "" && Total != "0") {
        $("#Validation_descipcion4").css("display", "none");
        $("#Validation_descipcion5").css("display", "none");
        $("#Validation_descipcion6").css("display", "none");
        $("#Validation_descipcion7").css("display", "none");
        
        var data = $("#frmEmpleadoComisionesCreate").serializeArray();

        $.ajax({
            url: "/EmpleadoComisiones/Create",
            method: "POST",
            data: data
        })
        .done(function (data) {
            //CERRAR EL MODAL DE AGREGAR
            $("#AgregarEmpleadoComisiones").serializeArray();;
            //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data == "error") {
                $("#AgregarEmpleadoComisiones").modal('show');
                iziToast.error({
                    title: 'Error',
                    message: '¡No se guardó el registro, contacte al administrador!',
                });
            }
            else {
                cargarGridComisiones();
                $("#AgregarEmpleadoComisiones").modal('hide');
                $("#Crear #emp_IdEmpleado").val();
                $("#Crear #cin_IdIngreso").val();
                // Mensaje de exito cuando un registro se ha guardado bien
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue Creado de forma exitosa!',
                });
            }

        });
    }
    else {
        if (Empleado == "0") {
            $("#Validation_descipcion").css("display", "");
            $("#Validation_descipcion4").css("display", "");
            document.getElementById("btnCreateRegistroComisiones").disabled = false;
        }
        else {
            $("#Validation_descipcion4").css("display", "none");
        }
        if (Ingreso == "0") {
            $("#Validation_descipcion3").css("display", "");
            $("#Validation_descipcion5").css("display", "");
            document.getElementById("btnCreateRegistroComisiones").disabled = false;
        }
        else {
            $("#Validation_descipcion5").css("display", "none");
        }
        if (Porcentaje == "" || Porcentaje == "0") {
            $("#Validation_descipcion6").css("display", "");
            document.getElementById("btnCreateRegistroComisiones").disabled = false;
        }
        else {
            $("#Validation_descipcion6").css("display", "none");
        }
        if (Total == "" || Total == "0") {
            $("#Validation_descipcion7").css("display", "");
            document.getElementById("btnCreateRegistroComisiones").disabled = false;
        }
        else {
            $("#Validation_descipcion7").css("display", "none");
        }
    }


    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)

});

//FUNCION: Detail 
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnDetalleEmpleadoComisiones", function () {

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
                console.log(data);
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
                $("#Detallar #cc_PorcentajeComision").html(data[0].cc_PorcentajeComision);
                $("#Detallar #cc_TotalVenta").html(data[0].cc_TotalVenta);
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

$(document).on("click", "#btnInactivarEmpleadoComisiones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarEmpleadoComisiones").modal('hide');
    $("#InactivarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

    document.getElementById("btnInactivarRegistroComisiones").disabled = false;
    document.getElementById("btnInactivarRegistroComisionesNo").disabled = false;
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroComisiones").click(function () {
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
$("#btnInactivarRegistroComisionesNo").click(function () {
    document.getElementById("btnInactivarRegistroComisionesNo").disabled = true;
    $("#InactivarEmpleadoComisiones").modal('hide');
    $("#EditarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");

});
//VALIDAR CREAR//
var IDActivar = 0;
$(document).on("click", "#btnActivarRegistroComisiones", function () {
    IDActivar = $(this).data('id');
    $("#ActivarEmpleadoComisiones").modal({ backdrop: 'static', keyboard: false });
    $("html, body").css("overflow", "hidden");
    $("html, body").css("overflow", "scroll");
    document.getElementById("btnActivarRegistroComisionesEjecutar").disabled = false;
});
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
//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModal").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion1").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    $("#PorcentajeComision").val('');
    $("#TotalVenta").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion1").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Validation_descipcion3").css("display", "none");
    $("#PorcentajeComision").val('');
    $("#TotalVenta").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnCreateRegistroComisiones").click(function () {
    var PorcentajeComision = $("#PorcentajeComision").val();
    var TotalVenta = $("#TotalVenta").val();
    var Empleado = $("#emp_IdEmpleado").val();
    var Ingresos = $("#cin_IdIngreso").val();

    if (PorcentajeComision == "") {
        $("#Validation_descipcion1").css("display", "");
    }
    else {
        $("#Validation_descipcion1").css("display", "none");
    }
    if (TotalVenta == "") {
        $("#Validation_descipcion2").css("display", "");
    }
    else {
        $("#Validation_descipcion2").css("display", "none");
    }

    if (Empleado == "0") {
        $("#Validation_descipcion").css("display", "");
    }
    else {
        $("#Validation_descipcion").css("display", "none");
    }
    if (Ingresos == "0") {
        $("#Validation_descipcion3").css("display", "");
    }
    else {
        $("#Validation_descipcion3").css("display", "none");
    }
});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModaledit").click(function () {
    $("#Editar #Validation_descipcion1e").css("display", "none");
    $("#Editar #Validation_descipcion2e").css("display", "none");
    $("#Editar #Validation_descipcion1").css("display", "none");
    $("#Editar #Validation_descipcion2").css("display", "none");
    $("#Editar #cc_PorcentajeComision").val('');
    $("#Editar #cc_TotalVenta").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerraredit").click(function () {
    $("#Editar #Validation_descipcion1e").css("display", "none");
    $("#Editar #Validation_descipcion2e").css("display", "none");
    $("#Editar #Validation_descipcion1").css("display", "none");
    $("#Editar #Validation_descipcion2").css("display", "none");
    $("#Editar #cc_PorcentajeComision").val('');
    $("#Editar #cc_TotalVenta").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnUpdateComisiones").click(function () {
    var PorcentajeComisionE = $("#cc_PorcentajeComision").val();
    var TotalVentaE = $("#cc_TotalVenta").val();

    if (PorcentajeComisionE == "" || PorcentajeComisionE == null || PorcentajeComisionE == undefined) {
        $("#Validation_descipcion1e").css("display", "");
    }
    else {
        $("#Validation_descipcion1e").css("display", "none");
    }

    if (TotalVentaE == "" || TotalVentaE == null || TotalVentaE == undefined) {
        $("#Validation_descipcion2e").css("display", "");
    }
    else {
        $("#Validation_descipcion2e").css("display", "none");
    }

});

$("#frmEmpleadoComisionesCreate").submit(function (e) {
    e.preventDefault();
});

