//var id = 0;


///FUNCION SERIALIZAR 
function serializar(data) {
    var Data = new Object();
    $.each(data, function (index, valor) {
        Data[valor.name] = valor.value;
    });
    return Data;
}
///FUNCION SERIALZAR



//AGREGAR HORARIOS///
$('#btnAgregar').click(function () {

    var data = $("#frmAgregarTipoHoras").serializeArray();
    //data = serializar(data);
    //data = JSON.stringify({ tbTipoHoras: data });
    console.log(data);

    $.ajax({
        url: "/TipoHoras/Create",
        method: "POST",
        data: data
    }).done(function (data) {
       
        if (data == "error") {
            iziToast.error({
                title: 'Error',
                message: 'No se pudo guardar el registro, contacte al administrador',
            });
        }
        else {
            llenarTabla();
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue ingresado con Exito',
            });
        }
    });
});



//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../Scripts/app/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
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

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX

function llenarTabla() {
    _ajax(null,
        '/TipoHoras/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.tiho_Descripcion);
                tabla.row.add([value.tiho_Descripcion,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs tablaDetalles' data-id='" + value.tiho_Id + "' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs tablaEditar' data-id=" + value.tiho_Id + ">Editar</a>" +
                    "</div>"]).draw();
            });
          
        });
}

//FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
$(document).on("click", "#IndexTable tbody tr td #btnEditarR", function () {
    var id = $(this).closest('tr').data('id');
    console.log(id);
    $.ajax({
        url: "/TipoHoras/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
           
                if (data.length > 0) {
                    console.log("funciona");
                    console.log(data);
                    $.each(data, function (i, item) {
                        $("#ModalEdit #tiho_Id").val(item.tiho_Id)
                        $("#ModalEdit #tiho_Descripcion").val(item.tiho_Descripcion);
                        $("#ModalEdit #tiho_Recargo").val(item.tiho_Recargo)
                        //$("#ModalEdit #tiho_UsuarioCrea").val(item.tiho_UsuarioCrea)
                        //$("#ModalEdit #tiho_FechaCrea").val(item.tiho_FechaCrea);
                    })
                }
            
                })
                   
           
        
});

//EDICION DEL REGISTRO
$("#btnEditarModal").click(function () {

    var data = $("#frmEditarTipoHoras").serializeArray();
    console.log(data);
    $.ajax({
        url: "/TipoHoras/Edit",
        method: "POST",
        data: data
    }).done(function (data) {
        if (data == "error") {
           
            iziToast.error({
                title: 'Error',
                message: 'No se pudo editar el registro, contacte al administrador',
            });
        }
        else {
            llenarTabla();
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue editado con exito!',
            });
        }
    });
});


//MODAL DETALLES
$(document).on("click", "#IndexTable tbody tr td #btnDetalle", function () {
    var id = $(this).closest('tr').data('id');
    $.ajax({
        url: "/TipoHoras/Details/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            //SI SE OBTIENE DATA, LLENAR LOS CAMPOS DEL MODAL CON ELLA
            if (data) {
                $("#ModalDetallesR").find("#tiho_Descripcion")["0"].innerText = data.tiho_Descripcion;
                $("#ModalDetallesR").find("#tiho_Recargo")["0"].innerText = data.tiho_Recargo;
                $("#ModalDetallesR").find("#tiho_Estado")["0"].innerText = data.tiho_Estado;
                $("#ModalDetallesR").find("#tiho_FechaCrea")["0"].innerText = FechaFormato(data.tiho_FechaCrea);
                $("#ModalDetallesR").find("#tiho_FechaModifica")["0"].innerText = FechaFormato(data.tiho_FechaModifica);
                $("#ModalDetallesR").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = data.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesR").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = data.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetallesR").find("#btnEditarM")["0"].dataset.id = id;
                $('#ModalDetallesR').modal('show');
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






//INHABILITAR
$("#btnInhabilitar").click(function () {

    var data = $("#frmInhabilitarTipoHoras").serializeArray();
    //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
    console.log(data);
    $.ajax({
        url: "/TipoHoras/Inactivar",
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
            cargarGridDeducciones();
            //Mensaje de exito de la edicion
            iziToast.success({
                title: 'Exito',
                message: 'El registro fue Inactivado de forma exitosa!',
            });
        }
    });
});


// PROBANDO LOS IZITOAST
//$(document).ready(function () {
//    console.log('cargado JS');
//    iziToast.show({
//        title: 'Hola',
//        message: 'Estoy probando los iziToast'
//    });
//});


