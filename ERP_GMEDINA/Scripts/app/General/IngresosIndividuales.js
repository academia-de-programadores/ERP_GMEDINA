//
//Obtención de Script para Formateo de Fechas
//
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

$(document).ready(function () {
    //PLUGIN DE LOS CHECKS-BOX
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });

    //PLUGIN SELECT2 DATATABLE 
    $.ajax({
        url: "/IngresosIndividuales/EmpleadoGetDDL",
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        $('#Crear #emp_IdCrear').select2({
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
            $('#IndexTabla').DataTable().clear();

            //RECORRER DATA OBETINA Y CREAR UN "TEMPLATE" PARA REFRESCAR EL TBODY DE LA TABLA DEL INDEX
            for (var i = 0; i < ListaIngresoIndividual.length; i++) {
                //variable para verificar el estado del registro
                var estadoRegistro = ListaIngresoIndividual[i].ini_Activo == false ? 'Inactivo' : 'Activo';

                //variable boton detalles
                var botonDetalles = '<button type="button" style="margin-right:3px;" class="btn btn-primary btn-xs" id="btnDetalleIngresosIndividuales" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Detalles</button>';

                //variable boton editar
                var botonEditar = ListaIngresoIndividual[i].ini_Activo == true ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnEditarIngresosIndividuales" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Editar</button>' : '';

                //variable donde está el boton activar
                //var botonActivar = (ListaIngresoIndividual[i].ini_Activo) == false ? (esAdministrador) == "1" ? '<button type="button" style="margin-right:3px;" class="btn btn-default btn-xs" id="btnActivarIngresosIndividuales" data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '">Activar</button>' : '' : '';
                var botonActivar = ListaIngresoIndividual[i].ini_Activo == false ? esAdministrador == "1" ? '<button data-id = "' + ListaIngresoIndividual[i].ini_IdIngresosIndividuales + '" type="button" class="btn btn-default btn-xs"  id="btnActivarIngresosIndividuales">Activar</button>' : '' : '';

                //AGREGAR EL ROW AL DATATABLE
                $('#IndexTabla').dataTable().fnAddData([
                    ListaIngresoIndividual[i].ini_IdIngresosIndividuales,
                    ListaIngresoIndividual[i].ini_Motivo,
                    ListaIngresoIndividual[i].per_Nombres + ' ' + ListaIngresoIndividual[i].per_Apellidos,
                    (ListaIngresoIndividual[i].ini_Monto % 1 == 0) ? ListaIngresoIndividual[i].ini_Monto + ".00" : ListaIngresoIndividual[i].ini_Monto,
                    estadoRegistro,
                    ListaIngresoIndividual[i].ini_comentario,
                    botonDetalles + botonEditar + botonActivar
                ]);
                }
            //APLICAR EL MAX WIDTH
            FullBody();
        });
}


//VARIABLE GLOBAL DE INACTIVACION
var GB_Activar = 0;
//Activar
$(document).on("click", "#IndexTabla tbody tr td #btnActivarIngresosIndividuales", function () {
    var validacionPermiso = userModelState("IngresosIndividuales/Activar");

    if (validacionPermiso.status == true) {
        //DESBLOQUEAR EL BOTON DE ACTIVAR
        $("#btnActivarRegistroIngresoIndividual").attr("disabled", false);
        //OBTENER EL ID DEL BOTON
        var id = $(this).data('id');
        //SETEO DE LA VARIABLE GLOABAL DE ACTIVACION
        GB_Activar = id;
        //Mostrar el Modal
        $("#ActivarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    }
});


$("#btnActivarRegistroIngresoIndividual").click(function () {
    //BLOQUEAR EL BOTON DE ACTIVAR
    $("#btnActivarRegistroIngresoIndividual").attr("disabled", true);

    $.ajax({
        url: "/IngresosIndividuales/Activar/" + GB_Activar,
        method: "GET"
    }).done(function (data) {
        $("#ActivarIngresosIndividuales").modal('hide');
        //VALIDAR RESPUESTA OBETNIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
        if (data == "error") {
            //DESBLOQUEAR EL BOTON DE ACTIVAR
            $("#btnActivarRegistroIngresoIndividual").attr("disabled", false);
            //MOSTRAR EL MENSAJE DE ERROR
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
    //SETEAR LA VARIABLE DE ACTIVACION
    GB_Activar = 0;

});


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$("#btnCerrarCrear").click(function () {
    OcultarValidaciones()
});

//Agregar//
//FUNCION: PRIMERA FASE DE AGREGAR UN NUEVO REGISTRO, MOSTRAR MODAL DE CREATE
const btnGuardar = $('#btnCreateRegistroIngresoIndividual')

$(document).on("click", "#btnAgregarIngresoIndividual", function () {
    var validacionPermiso = userModelState("IngresosIndividuales/Create");

    if (validacionPermiso.status == true) {
        console.log($("#Crear #emp_IdCrear").val());

        //DESBLOQUEAR EL BOTON DE CREAR
        $("#btnCreateRegistroIngresoIndividual").attr("disabled", false);
        //INICIALIZAR LOS VALORES DEL MODAL
        $("#Crear #emp_IdCrear").val("0");
        $("#ini_Motivo").val('');
        $("#ini_Monto").attr("disabled", false);
        $("#ini_Monto").val('');
        $('#Crear #ini_PagaSiempre').prop('checked', false);
        $("#Crear #ini_comentario").val('');
        //VALIDAR EL DDL
        let valCreate = $("#Crear #emp_IdCrear").val();
        if (valCreate != null && valCreate != "")
            $("#Crear #emp_IdCrear").val('').trigger('change');
        //MOSTRAR EL MODAL DE AGREGAR
        $("#AgregarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
        console.log($("#Crear #emp_IdCrear").val());
    }
});



//FUNCION: CREAR EL NUEVO REGISTRO
$('#btnCreateRegistroIngresoIndividual').click(function () {
    var Motivo = $("#Crear #ini_Motivo").val();
    var Monto = $("#Crear #ini_Monto").val();
    var IdEmp = $("#Crear #emp_IdCrear").val();
    var ini_PagaSiempre = false;
    var comentario = document.getElementById("ini_comentario").value;//$("#Crear #ini_comentario").val();
    
    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Crear #ini_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE


    if ($('#Crear #ini_PagaSiempre').is(':checked')) {
        ini_PagaSiempre = true;
    }
    else{
        ini_PagaSiempre = false;
    }
   
    if (ValidarCamposCrear(Motivo, IdEmp, Monto)) {
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = true;
        var data = { ini_Motivo: Motivo, emp_Id: IdEmp, ini_Monto: MontoFormateado, ini_PagaSiempre: ini_PagaSiempre, ini_comentario: comentario };

        //ENVIAR DATA AL SERVIDOR PARA EJECUTAR LA INSERCIÓN
        $.ajax({
            url: "/IngresosIndividuales/Create",
            method: "POST",
            data: data
        }).done(function (data) {
            //VALIDAR RESPUESTA OBTENIDA DEL SERVIDOR, SI LA INSERCIÓN FUE EXITOSA O HUBO ALGÚN ERROR
            if (data != "error") {
                $("#Crear #ini_Motivo").val('');
                $("#Crear #ini_Monto").val('');
                $('#Crear #ini_PagaSiempre').prop('checked', false);
                $("#Crear #ini_comentario").val('');
                //CERRAR EL MODAL DE AGREGAR
                $("#AgregarIngresosIndividuales").modal('hide');
                OcultarValidaciones();
                cargarGridDeducciones();

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

    else {
        document.getElementById("btnCreateRegistroIngresoIndividual").disabled = false;
        //VALIDAR LOS TIPOS DE ERRORES EN LOS CAMPOS
        ValidarCamposCrear(Motivo, IdEmp, Monto);
    }

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmCreateIngresosIndividuales").submit(function (e) {
        return false;
    });

});


//////////crear

function ValidarCamposCrear(Motivo, IdEmp, Monto) {
    pasoValidacionCrear = true;
    //VALIDAR MONTO POR COLABORADOR
    if (IdEmp == 0)
        $("#Crear #ini_Monto").attr("disabled", true);   //DESBLOQUEAR EL CAMPO MONTO
    else
        $("#Crear #ini_Monto").attr("disabled", false);  //DESBLOQUEAR EL CAMPO MONTO

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Crear #ini_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);

    //VALIDACIONES DEL CAMPO EMP_ID
    if (IdEmp != "-1") {
        if (IdEmp == '' || IdEmp == null) {
            pasoValidacionCrear = false;
            $("#Crear #ast2").addClass("text-danger");
            $("#Crear #validatione_empleadoID").css("display", "");
        }
        else {
            $("#Crear #ast2").removeClass("text-danger");
            $("#Crear #validatione_empleadoID").css("display", "none");
        }
    }

    //VALIDACIONES DEL CAMPO RAZON
    if (Motivo != "-1") {
        var LengthString = Motivo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Motivo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Crear #ini_Motivo").val(Motivo.substring(0, FirstChar + 1));
        }
        if (Motivo == "" || Motivo == " " || Motivo == "  " || Motivo == null || Motivo == undefined) {
            if (Motivo == ' ')
                $("#Crear #ini_Motivo").val("");
            pasoValidacionCrear = false;
            $("#Crear #ast1").addClass("text-danger");
            $("#Crear #Validation_Motivo").css("display", "");

        } else {
            $("#Crear #ast1").removeClass("text-danger");
            $("#Crear #Validation_Motivo").css("display", "none");
        }
    }

    //VALIDACIONES DEL CAMPO MONTO
    if (Monto != "-1") {
        if (Monto == "" || Monto == null || Monto == undefined) {
            pasoValidacionCrear = false;
            $("#Crear #ast3").removeClass("text-danger");
            $("#Crear #Validation_Monto3").css("display", "none");

            $("#Crear #ast3").addClass("text-danger");
            //$('#AsteriscoMonto').show();
            $("#Crear #Validation_Monto2").css("display", "");
        } else {
            $("#Crear #ast3").removeClass("text-danger");
            $("#Crear #Validation_Monto2").css("display", "none");
            if (Monto <= 0) {
                pasoValidacionCrear = false;
                $("#Crear #ast3").addClass("text-danger");
                $("#Crear #Validation_Monto3").css("display", "");
            } else {
                $("#Crear #ast3").removeClass("text-danger");
                $("#Crear #Validation_Monto3").css("display", "none");
            }
        }
    }
    return pasoValidacionCrear;
}
////////// fin crear

function OcultarValidaciones() {
    $("#Crear #emp_IdCrear").val("0");
    $("#ini_Motivo").val('');
    $("#ini_Monto").val('');
    $("#ini_PagaSiempre").prop('checked', false);
    $("#ast1").css("color", "black");
    $("#ast2").css("color", "black");
    $("#ast3").css("color", "black");
    $("#Validation_Motivo").css("display", "none");
    $("#validatione_empleadoID").css("display", "none");
    $("#Validation_Monto2").css("display", "none");
    $("#Validation_Monto3").css("display", "none");
    $("#AgregarIngresosIndividuales").modal('hide');
}

//FUNCION: OCULTAR MODAL DE EDICIÓN
$("#btnCerrarEditar").click(function () {
    OcultarValidacionesEditar();
});


//Editar//
//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#IndexTabla tbody tr td #btnEditarIngresosIndividuales", function () {
    var validacionPermiso = userModelState("IngresosIndividuales/Edit");

    if (validacionPermiso.status == true) {
        //OBTENER TODA LA DATA DEL ROW SELECCIONADO
        let dataEmp = table.row($(this).parents('tr')).data();
        //INICIALIZAR UNA VARIABLE CON EL VALOR DEL ALMACENAMIENTO LOCAL
        let itemEmpleado = localStorage.getItem('idEmpleado');

        //VALIDAR QUE NO  ESTE NULL
        if (itemEmpleado != null) {
            $("#Editar #emp_Id option[value='" + itemEmpleado + "']").remove();
            localStorage.removeItem('idEmpleado');
        }

        //VARIABLE DE CONTEXTO PARA CAPTURAR EL ID DEL EMPLEADO SELCCIONADO
        var idEmpSelect = "";
        var NombreSelect = "";
        //CAPTURAR EL DATAID
        var id = $(this).data('id');
        //SETEO DE LA VARIABLE DE INACTIVACION
        GB_Inactivar = id;
        //PETICION AL SERVIDOR
        $.ajax({
            url: "/IngresosIndividuales/Edit/" + id,
            method: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: id })
        })
            .done(function (data) {
                //SETEO DE LA VARIABLE DE idEmpSelect CON EL ID DEL EMPLEADO 
                idEmpSelect = data.emp_Id;
                NombreSelect = dataEmp[2];
                //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
                if (data) {
                    //HABILITAR O INHABILITAR EL BOTON DE EDITAR SI ESTA DEDUCIDO O NO 
                    if (data.ini_Pagado) {
                        $("#btnEditIngresoIndividual").attr('disabled', true);
                    } else {
                        $("#btnEditIngresoIndividual").attr('disabled', false);
                    }

                    if (data.ini_PagaSiempre) {
                        $('#Editar #ini_PagaSiempre').prop('checked', true);
                    }
                    else {
                        $('#Editar #ini_PagaSiempre').prop('checked', false);
                    }

                    $("#Editar #emp_Id").select2("val", "");

                    $('#Editar #emp_Id').val(idEmpSelect).trigger('change');

                    let valor = $('#Editar #emp_Id').val();

                    if (valor == null) {
                        $("#Editar #emp_Id").prepend("<option value='" + idEmpSelect + "' selected>" + NombreSelect + "</option>").trigger('change');
                        localStorage.setItem('idEmpleado', idEmpSelect);
                    }

                    $("#Editar #ini_IdIngresosIndividuales").val(data.ini_IdIngresosIndividuales);
                    $("#Editar #ini_Motivo").val(data.ini_Motivo);
                    $("#Editar #ini_Monto").val(data.ini_Monto);
                    $("#Editar #ini_PagaSiempre").val(data.ini_PagaSiempre);
                    $("#Editar #ini_comentario").val(data.ini_comentario);
                    

                    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });

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

$(document).on("click", "#btnRegresar", function () {
    $("#EditarIngresosIndividualesConfirmacion").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });


});

$(document).on("click", "#btnReg", function () {
    $("#EditarIngresosIndividualesConfirmacion").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });


});

$("#btnEditIngresoIndividual").click(function () {
    var Motivo = $("#Editar #ini_Motivo").val();
    var IdColaborador = $("#Editar #emp_Id").val();
    var Monto = $("#Editar #ini_Monto").val();
    //var comentario = document.getElementById("ini_comentario").value;


    if (ValidarCamposEditar(Motivo, IdColaborador, Monto)) {
        $("#EditarIngresosIndividuales").modal('hide');
        $("#EditarIngresosIndividualesConfirmacion").modal({ backdrop: 'static', keyboard: false });
        $('#btnEditIngresoIndividual2').attr('disabled', false);
    }  
    else {
        ValidarCamposEditar(Motivo, IdColaborador, Monto);
        $('#btnEditIngresoIndividual').attr('disabled', true);
    }
});

//EJECUTAR EDICIÓN DEL REGISTRO EN EL MODAL
$("#btnEditIngresoIndividual2").click(function () {
    $('#btnEditIngresoIndividual2').attr('disabled', true);
    var ini_PagaSiempre = false;
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE
    var ini_IdIngresosIndividuales = $("#Editar #ini_IdIngresosIndividuales").val();
    var emp_Id = $("#Editar #emp_Id").val();
    var ini_Motivo = $("#Editar #ini_Motivo").val();
    var ini_Monto = $("#Editar #ini_Monto").val();
    //var comentario1 = $("#ini_comentario").val();
    //
    var comentario = $("#Editar #ini_comentario").val(); //document.getElementById("ini_comentario").value;
    

    //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
    var indices = $("#Editar #ini_Monto").val().split(",");
    //VARIABLE CONTENEDORA DEL MONTO
    var MontoFormateado = "";
    //ITERAR LOS INDICES DEL ARRAY MONTO
    for (var i = 0; i < indices.length; i++) {
        //SETEAR LA VARIABLE DE MONTO
        MontoFormateado += indices[i];
    }
    //FORMATEAR A DECIMAL
    MontoFormateado = parseFloat(MontoFormateado);
    // SIEMPRE HACER LAS RESPECTIVAS VALIDACIONES DEL LADO DEL CLIENTE

    if ($('#Editar #ini_PagaSiempre').is(':checked')) {
        ini_PagaSiempre = true;
    }
    else{
        ini_PagaSiempre = false;
    }

    var data = { ini_IdIngresosIndividuales: ini_IdIngresosIndividuales, ini_Motivo: ini_Motivo, emp_Id: emp_Id, ini_Monto: MontoFormateado, ini_PagaSiempre: ini_PagaSiempre, ini_comentario: comentario };
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        $.ajax({
            url: "/IngresosIndividuales/Edit",
            method: "POST",
            data: data
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
});

// Evitar PostBack en los Formularios de las Vistas Parciales de Modal
$("#frmEditIngresoIndividual").submit(function (e) {
    return false;
});


//FUNCION: VALIDAR LOS CAMPOS DEL MODAL DE EDITAR
function ValidarCamposEditar(Motivo, IDColaborador, Monto) {
    var pasoValidacion = true;

    if (IDColaborador != "-1") {
        if (IDColaborador <= 0 || isNaN(IDColaborador)) {
            pasoValidacion = false;
            $('#Editar #validation_emp_Id').css("display", "block");
            $("#Editar #ast2").addClass("text-danger");
            //razon.focus();
        } else {
            //OCULTAR VALIDACIONES
            $('#Editar #validation_emp_Id').css("display", "none");
            $("#Editar #ast2").removeClass("text-danger");
        }
    }

    if (Motivo != "-1") {
        var LengthString = Motivo.length;
        if (LengthString > 1) {
            var FirstChar = LengthString - 2;
            var LastChar = Motivo.substring(FirstChar, LengthString);
        }
        if (LastChar == "  ") {
            $("#Editar #ini_Motivo").val(Motivo.substring(0, FirstChar + 1));
        }
        if (Motivo == null || Motivo == '' || Motivo == ' ' || Motivo == '  ') {
            pasoValidacion = false;
            if (Motivo == ' ')
                $("#Editar #ini_Motivo").val("");
            $('#validation_ini_Motivo').show();
            $("#Editar #ast1").addClass("text-danger");
        } else {
            //OCULTAR VALIDACIONES
            $('#validation_ini_Motivo').hide();
            $("#Editar #ast1").removeClass("text-danger");
        }
    }


    //VALIDACION DEL MONTO
    if (Monto != "-1") {
        //CONVERTIR EN ARRAY EL MONTO A PARTIR DEL SEPARADOR DE MILLARES
        var indices = Monto.split(",");
        //VARIABLE CONTENEDORA DEL MONTO
        var MontoFormateado = "";
        //ITERAR LOS INDICES DEL ARRAY MONTO
        for (var i = 0; i < indices.length; i++) {
            //SETEAR LA VARIABLE DE MONTO
            MontoFormateado += indices[i];
        }
        //FORMATEAR A DECIMAL
        MontoFormateado = parseFloat(MontoFormateado);

        //VALIDACIONES
        if (MontoFormateado == null || MontoFormateado == '' || MontoFormateado == undefined) {
            pasoValidacion = false;
            $('#validation_ini_Monto2').hide();
            $('#validation_ini_Monto').show();
            $("#Editar #ast3").addClass("text-danger");
        } else {
            $('#validation_ini_Monto').hide();
            $("#Editar #ast3").removeClass("text-danger");
            if (MontoFormateado <= 0) {
                pasoValidacion = false;
                $('#validation_ini_Monto').hide();
                $("#Editar #ast3").addClass("text-danger");
                $('#validation_ini_Monto2').show();
            } else {
                $("#Editar #ast3").removeClass("text-danger");
                $('#validation_ini_Monto2').hide();
            }
        }
    }

    return pasoValidacion;
}

//FUNCION: OCULTAR LOS MENSAJES DE ERROR DE VALIDACIONES, AL CERRAR EL MODAL
function OcultarValidacionesEditar() {
    $("#validation_ini_Motivo").css("display", "none");
    $("#validation_emp_Id").css("display", "none");
    $("#validation_ini_Monto").css("display", "none");
    $("#validation_ini_Monto2").css("display", "none");
    $("#Editar #ast1").removeClass("text-danger");
    $("#Editar #ast2").removeClass("text-danger");
    $("#Editar #ast3").removeClass("text-danger");
    $("#EditarIngresosIndividuales").modal('hide');
    $("#Editar #ini_IdIngresosIndividuales").val('');
    $("#Editar #ini_Motivo").val('');
    $("#Editar #ini_Monto").val('');
    $("#Editar #ini_PagaSiempre").prop('checked', false);
}




//Detalles//
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).on("click", "#IndexTabla tbody tr td #btnDetalleIngresosIndividuales", function () {
    var validacionPermiso = userModelState("IngresosIndividuales/Details");

    if (validacionPermiso.status == true) {
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
                    $("#Detalles #ini_comentario").html(data[0].ini_comentario);

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
///////////////////////////////////////////////////////////////////////////////////////////////////



//Inactivar//
$(document).on("click", "#btnBack", function () {
    $("#InactivarIngresosIndividuales").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    
    
});

$(document).on("click", "#btnBa", function () {
    $("#InactivarIngresosIndividuales").modal('hide');
    $("#EditarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });
    
    
});

$(document).on("click", "#btnInactivarIngresoIndividual", function () {
    var validacionPermiso = userModelState("IngresosIndividuales/Inactivar");

    if (validacionPermiso.status == true) {
        $("#EditarIngresosIndividuales").modal('hide');
        $("#InactivarIngresosIndividuales").modal({ backdrop: 'static', keyboard: false });

        document.getElementById("btnInactivarRegistroIngresoIndividual").disabled = false;
    }
});

//VARIABLE GLOBAL DE INACTIVACION
var GB_Inactivar = 0;
//EJECUTAR INACTIVACION DEL REGISTRO EN EL MODAL
$("#btnInactivarRegistroIngresoIndividual").click(function () {
    //BLOQUEAR EL BOTON
    $("#btnInactivarRegistroIngresoIndividual").attr("disabled", true);

    //var data = $("#frmInactivarIngresoIndividual").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    $.ajax({
        url: "/IngresosIndividuales/Inactivar/" + GB_Inactivar,
        method: "GET"
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
        //SETEAR LA VARIABLE GLOBAL DE INACTIVAR
        GB_Inactivar = 0;
    });

    // Evitar PostBack en los Formularios de las Vistas Parciales de Modal
    $("#frmInactivarIngresoIndividual").submit(function (e) {
        return false;
    });

});