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
                    '<td>' + ListaComisiones[i].cc_PorcentajeComision + '</td>' +
                      '<td>' + ListaComisiones[i].cc_TotalVenta + '</td>' +
                    '<td>' + FechaRegistro + '</td>' +
                     '<td>' + Check + '</td>' + //-----------------------------------AQUI ENVIA LA VARIABLE
                    '<td>' +
                    
                    '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" class="btn btn-primary btn-xs" id="btnDetalleEmpleadoComisiones">Detalle</button>' +
                    '<button data-id = "' + ListaComisiones[i].cc_Id + '" type="button" class="btn btn-default btn-xs" id="btnEditarEmpleadoComisiones">Editar</button>' +
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
                $("#Editar #cc_Id").val(data.cc_Id);
                $("#Editar #cc_PorcentajeComision").val(data.cc_PorcentajeComision);
                $("#Editar #cc_TotalVenta").val(data.cc_TotalVenta);
                //GUARDAR EL ID DEL DROPDOWNLIST (QUE ESTA EN EL REGISTRO SELECCIONADO) QUE NECESITAREMOS PONER SELECTED EN EL DDL DEL MODAL DE EDICION
                var SelectedIdEmp = data.emp_Id;
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
    var data = $("#frmEmpleadoComisionesEditar").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    mostrarCargandoEditar()
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
            ocultarCargandoEditar();
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
    var Empleado = $("#Crear #emp_IdEmpleado").val();

    if (Empleado == "0") {
        $("#Validation_descipcion").css("display", "");
    }
    else {
        $("#Validation_descipcion").css("display", "none");
        //SERIALIZAR EL FORMULARIO DEL MODAL (ESTÁ EN LA VISTA PARCIAL)

        var data = $("#frmEmpleadoComisionesCreate").serializeArray();
        
        mostrarCargandoCrear()
        $.ajax({
            url: "/EmpleadoComisiones/Create",
            method: "POST",
            data: data
        })
            .done(function (data) {
                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarEmpleadoComisiones");
                //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
                if (data == "error") {
                    $("#AgregarEmpleadoComisiones").modal('show');
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
                    ocultarCargandoCrear();
            });
    }
    

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
                $("#Detallar #cc_FechaRegistro").val(FechaRegistro);
                $("#Detallar #cc_UsuarioCrea").val(data[0].cc_UsuarioCrea);
                $("#Detallar #tbUsuario_usu_NombreUsuario").val(data[0].UsuCrea);
                $("#Detallar #tbCatalogoDeIngresos_cin_DescripcionIngreso").val(data[0].Ingreso);
                $("#Detallar #cin_IdIngreso").val(data[0].cin_IdIngreso);
                $("#Detallar #emp_Id").val(data[0].emp_Id);
                $("#Detallar #tbEmpleados_tbPersonas_per_Nombres").val(data[0].NombreEmpleado + ' ' + data[0].ApellidosEmpleado);
                $("#Detallar #cc_FechaCrea").val(FechaCrea);
                $("#Detallar #cc_UsuarioModifica").val(data[0].cc_UsuarioModifica);
                $("#Detallar #cc_PorcentajeComision").val(data[0].cc_PorcentajeComision);
                $("#Detallar #cc_TotalVenta").val(data[0].cc_TotalVenta);
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
                message: 'No se pudo Inhabilitar el registro, contacte al administrador',
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
                message: 'El registro fue Inhabilitado de forma exitosa!',
            });
        }
    });
});

//VALIDAR CREAR//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModal").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion1").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#PorcentajeComision").val('');
    $("#TotalVenta").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerrar").click(function () {
    $("#Validation_descipcion").css("display", "none");
    $("#Validation_descipcion1").css("display", "none");
    $("#Validation_descipcion2").css("display", "none");
    $("#PorcentajeComision").val('');
    $("#TotalVenta").val('');
});


//FUNCION: MOSTRAR DATA ANNOTATION SI LOS CAMPOS SIGUEN VACIOS (EN CASO DE USO CONTINUO PREVIO AL CIERRE DEL MODAL).
$("#btnCreateRegistroComisiones").click(function () {
    var PorcentajeComision = $("#PorcentajeComision").val();
    var TotalVenta = $("#TotalVenta").val();
    var Empleado = $("#emp_IdEmpleado").val();

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

});

//VALIDAR EDIT//

//FUNCION: OCULTAR DATA ANNOTATION CON BOTON INFERIOR CERRAR DEL MODAL.
$("#btnCerrarModaledit").click(function () {
    $("#Validation_descipcion1e").css("display", "none");
    $("#Validation_descipcion2e").css("display", "none");
    $("#cc_PorcentajeComision").val('');
    $("#cc_TotalVenta").val('');
});


//FUNCION: OCULTAR DATA ANNOTATION CON BOTON SUPERIOR DE CERRAR (BOTON CON X).
$("#IconoCerraredit").click(function () {
    $("#Validation_descipcion1e").css("display", "none");
    $("#Validation_descipcion2e").css("display", "none");
    $("#cc_PorcentajeComision").val('');
    $("#cc_TotalVenta").val('');
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


  
function mostrarCargandoCrear(){
    btnGuardar.hide();
    cargandoCrear.html(spinner());
    cargandoCrear.show();
}

function ocultarCargandoCrear(){
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


    

const btnGuardar = $('#btnCreateRegistroComisiones'),

cargandoCrearcargandoCrear=$('#cargandoCrear')

cargandoCrear=$('#cargandoCrear')//Div que aparecera cuando se le de click en crear






function mostrarCargandoEditar(){
    btnEditar.hide();
    cargandoEditar.html(spinner());
    cargandoEditar.show();
}

function ocultarCargandoEditar(){
    btnEditar.show();
    cargandoEditar.html('');
    cargandoEditar.hide();
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


    

const btnEditar = $('#btnUpdateComisiones'),

cargandoEditarcargandoEditar = $('#cargandoEditar')

cargandoEditar = $('#cargandoEditar')//Div que aparecera cuando se le de click en crear