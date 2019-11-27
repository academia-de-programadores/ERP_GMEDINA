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
    _ajax(null,
        '/EmpleadoComisiones/GetData',
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
            var ListaComisiones = data, template = '';
            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaComisiones.length; i++) {
                var FechaRegistro = FechaFormato(ListaComisiones[i].cc_FechaRegistro);

                var Check = "";
                if (ListaComisiones[i].cc_Pagado == true) {
                    Check = '<div class="icheckbox_square-green checked disabled" style="position: relative;"><input type="checkbox" class="i-checks" id="check-id" checked="" disabled="" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div>'; //SE LLENA LA VARIABLE CON UN INPUT CHEQUEADO
                } else {
                    Check = '<div class="icheckbox_square-green disabled" style="position: relative;"><input type="checkbox" class="i-checks" id="check-" disabled="" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div>'; //SE LLENA LA VARIABLE CON UN INPUT QUE NO ESTA CHEQUEADO
                }

                template += '<tr data-id = "' + ListaComisiones[i].cc_Id + '">' +
                    '<td>' + ListaComisiones[i].per_Nombres + '</td>' +
                    '<td>' + ListaComisiones[i].per_Apellidos + '</td>' +
                    '<td>' + ListaComisiones[i].cin_DescripcionIngreso + '</td>' +
                    '<td>' + ListaComisiones[i].cc_Monto + '</td>' +
                    '<td>' + FechaRegistro + '</td>' +
                     '<td>' + Check + '</td>' + //-----------------------------------AQUI ENVIA LA VARIABLE
                    '<td>' +
                    '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" class="btn btn-primary btn-xs" id="btnEditarEmpleadoComisiones">Editar</button>' +
                    '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" class="btn btn-default btn-xs" id="btnDetalleEmpleadoComisiones">Detalle</button>' +
                    '</td>' +
                    '</tr>';
            }
            //REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            $('#tbodyComisiones').html(template);
        });
    FullBody();
}


//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnEditarEmpleadoComisiones", function () {
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
                var FechaRegistro = FechaFormato(data.cc_FechaRegistro);
                $("#Editar #cc_Id").val(data.cc_Id);
                $("#Editar #cc_Monto").val(data.cc_Monto);
                $("#Editar #cc_FechaRegistro").val(FechaRegistro);
                $("#Editar #cc_Pagado").val(data.cc_Pagado);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedIdEmp = data.emp_Id;
                var SelectedIdCatIngreso = data.cin_IdIngreso;
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
                            $("#Editar #cin_IdIngreso").append("<option" + (iter.Id == SelectedIdCatIngreso ? " selected" : " ") + " value='" + iter.Id + "'>" + iter.Descripcion + "</option>");
                        });
                    });
                $("#EditarEmpleadoComisiones").modal();
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

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnUpdateComisiones").click(function () {
    //SERIALIZAR EL FORMULARIO (QUE ESTÁ EN LA VISTA PARCIAL) DEL MODAL, SE PARSEA A FORMATO JSON
    var data = $("#frmEmpleadoComisiones").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/EmpleadoComisiones/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
        }
        else {            
            // REFRESCAR UNICAMENTE LA TABLA
            cargarGridComisiones();
            //UNA VEZ REFRESCADA LA TABLA, SE OCULTA EL MODAL
            $("#EditarEmpleadoComisiones").modal('hide');
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado de forma exitosa!',
            });

        }
    });
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoComisiones", function () {
    var cc_Monto = $('#AgregarEmpleadoComisiones #Monto').val('');
    var cc_Pagado = $('#AgregarEmpleadoComisiones #cc_Pagado').prop('checked', false);

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
    $("#AgregarEmpleadoComisiones").modal();
});

//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
$(document).on("click", "#btnAgregarEmpleadoComisiones", function () {
    //PEDIR DATA PARA LLENAR EL DROPDOWNLIST DEL MODAL
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
    $("#AgregarEmpleadoComisiones").modal();
});
//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroComisiones').click(function () {
    //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)

    var data = $("#frmEmpleadoComisionesCreate").serializeArray();

    $.ajax({
        url: "/EmpleadoComisiones/Create",
        method: "POST",
        data: data
    }).done(function (data) {
        //CERRAR EL MODAL DE AGREGAR
        $("#AgregarEmpleadoComisiones");
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {

        }
        else {            
            cargarGridComisiones();
            $("#AgregarEmpleadoComisiones").modal('hide');
            // Mensaje de exito cuando un registro se ha guardado bien
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue registrado de forma exitosa!',
            });
        }
    });

});


//FUNCION: Detail 
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#tblEmpleadoComisiones tbody tr td #btnDetalleEmpleadoComisiones", function () {

    var ID = $(this).data('id');
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

                //-------------------------CHECKBOX-------------------------
                var padre = document.getElementById("padre");
                //OBTIENE EL DIV PADRE QUE CONTENDRA EL CHECKBOX
                if (data[0].cc_Pagado) {
                    //VACIA EN CONTENIDO DEL DIV PADRE ANTES DE LLENARLO
                    padre.innerHTML = '';
                    //CREA EL DIV HIJO
                    var input = document.createElement("INPUT");
                    //ASIGNA LOS ATRIBUTOS Y CLASES AL DIV HIJO
                    input.type = 'checkbox';
                    input.classList.add('i-checks');
                    input.disabled = true;
                    input.checked = true;

                    //AGREGA EL DIV HIJO AL DIV PADRE
                    padre.appendChild(input);

                    //LLAMA LA FUNCION DE I-CHECKS PARA DARLE ESTILO AL CHECKBOX
                    $('.i-checks').iCheck({
                        checkboxClass: 'icheckbox_square-green',
                        radioClass: 'iradio_square-green',
                    });
                } else {
                    //VACIA EN CONTENIDO DEL DIV PADRE ANTES DE LLENARLO
                    padre.innerHTML = '';
                    //CREA EL DIV HIJO
                    var input = document.createElement("INPUT");
                    //ASIGNA LOS ATRIBUTOS Y CLASES AL DIV HIJO
                    input.type = 'checkbox';
                    input.classList.add('i-checks');
                    input.disabled = true;

                    //AGREGA EL DIV HIJO AL DIV PADRE
                    padre.appendChild(input);

                    //LLAMA LA FUNCION DE I-CHECKS PARA DARLE ESTILO AL CHECKBOX
                    $('.i-checks').iCheck({
                        checkboxClass: 'icheckbox_square-green',
                        radioClass: 'iradio_square-green',
                    });
                }



                $("#Detallar #cc_Id").val(data[0].cc_Id);
                $("#Detallar #cc_Monto").val(data[0].cc_Monto);
                $("#Detallar #cc_FechaRegistro").val(FechaRegistro);
                $("#Detallar #cc_UsuarioCrea").val(data[0].cc_UsuarioCrea);
                $("#Detallar #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detallar #tbCatalogoDeIngresos_cin_DescripcionIngreso").val(data[0].Ingreso);
                $("#Detallar #cin_IdIngreso").val(data[0].cin_IdIngreso);
                $("#Detallar #emp_Id").val(data[0].emp_Id);
                $("#Detallar #tbEmpleados_tbPersonas_per_Nombres").val(data[0].NombreEmpleado + ' ' + data[0].ApellidosEmpleado);
                $("#Detallar #cc_FechaCrea").val(FechaCrea);
                $("#Detallar #cc_UsuarioModifica").val(data[0].cc_UsuarioModifica);
                data[0].UsuModifica == null ? $("#Detallar #tbUsuario1_usu_NombreUsuario").val('Sin modificaciones') : $("#Detallar #tbUsuario1_usu_NombreUsuario").val(data[0].UsuModifica);
                $("#Detallar #cc_FechaModifica").val(FechaModifica);
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

                $("#DetalleEmpleadoComisiones").modal();
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



$(document).on("click", "#btnInactivarEmpleadoComisiones", function () {
    //MOSTRAR EL MODAL DE INACTIVAR
    $("#EditarEmpleadoComisiones").modal('hide');
    $("#InactivarEmpleadoComisiones").modal();
});

//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroComisiones").click(function () {

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
                message: 'No se pudo inactivar el registro, contacte al administrador',
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
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
});

//VALIDAR CREAR//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModal").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Monto").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#Monto").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnCreateRegistroComisiones").click(function () {
    var Monto = $("#Monto").val();
    var Empleado = $("#emp_IdEmpleado").val();

    if (Monto == "") {
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

});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModaledit").click(function () {

    $("#Validation_descipcion2e").css("display", "none");
    $("#Monto").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerraredit").click(function () {

    $("#Validation_descipcion2e").css("display", "none");
    $("#Monto").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnUpdateComisiones").click(function () {
    var MontoE = $("#cc_Monto").val();


    if (MontoE == "" || MontoE == null || MontoE == undefined) {
        $("#Validation_descipcion2e").css("display", "");
    }
    else {
        $("#Validation_descipcion2e").css("display", "none");
    }

});

$("#frmEmpleadoComisionesCreate").submit(function (e) {
    e.preventDefault();
});