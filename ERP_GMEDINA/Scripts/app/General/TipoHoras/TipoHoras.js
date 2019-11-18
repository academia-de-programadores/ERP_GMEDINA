var id = 0;

$(document).ready(function () {
    AllFunctions();
});
///FUNCION SERIALIZAR 
function serializar(data) {
    var Data = new Object();
    $.each(data, function (index, valor) {
        Data[valor.name] = valor.value;
    });
    return Data;
}
///FUNCION SERIALZAR


function AllFunctions() {

    //AGREGAR HORARIOS///
    $('#btnAgregar').click(function () {

        var data = $("#frmAgregarTipoHoras").serializeArray();
        //data = serializar(data);
        //data = JSON.stringify({ tbTipoHoras: data });
        //console.log(data);

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
                //llenarTabla();
        
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue ingresado con Exito',
                });
                //table.ajax.reload(null, false);
                llenarTabla();
            }
        });
    });
    //AGREGAR HORARIOS///



    ///////FUNCIONES PARA EDITAR///////////

    //FUNCION: PRIMERA FASE DE EDICION DE REGISTROS, MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
    $(".tablaEditar").click(function () {
       // $(document).on("click", "#IndexTable tbody tr td #btnEditarR", function () {
       // var id = $(this).closest('tr').data('id');
        var tr = this.closest("tr");
         id = $(this).data("id");
            //console.log(id);
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
                        //console.log("funciona");
                        //console.log(data);
                        $.each(data, function (i, item) {
                            $("#ModalEdit #tiho_Id").val(item.tiho_Id)
                            $("#ModalEdit #tiho_Descripcion").val(item.tiho_Descripcion);
                            $("#ModalEdit #tiho_Recargo").val(item.tiho_Recargo)
                            //$("#ModalEdit #tiho_UsuarioCrea").val(item.tiho_UsuarioCrea)
                            //$("#ModalEdit #tiho_FechaCrea").val(item.tiho_FechaCrea);
                        })
                    }

                //})



        });
    });
    //EDICION DEL REGISTRO
    $("#btnEditarModal").click(function () {

        var data = $("#frmEditarTipoHoras").serializeArray();
        //console.log(data);
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
                
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue editado con exito!',
                });
                //llenarTabla();

                llenarTabla();
            }
        });
    });

    //////////////


    ////////CARGAR EL MODAL DE DETALLES/////////

    //MODAL DETALLES
    $(".tablaDetalle").click(function () {
        // $(document).on("click", "#IndexTable tbody tr td #btnDetalle", function () {
        var tr = this.closest("tr");
         id = $(this).data("id");
        //var id = $(this).closest('tr').data('id');
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
        // });
    });
    ////////////////



    //////FUNCION PARA INHABILITAR////////////


    //INHABILITAR
    $("#btnInhabilitar").click(function () {

       // var data = $("#frmInhabilitarTipoHoras").serializeArray();
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        var data = $("#frmEditarTipoHoras").serializeArray();
     
        data.tiho_Id = id;
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
                llenarTabla();
                //Mensaje de exito de la edicion
                iziToast.success({
                    title: 'Exito',
                    message: 'El registro fue Inactivado de forma exitosa!',
                });
            }
        });
    });
    ///////////////

}



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


//var table = $('#IndexTable').DataTable({
//    ajax: {
//        url: '/TipoHoras/llenarTabla',
//        method: "POST",
//        data: data
//    }
//});
//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX

function llenarTabla() {
    _ajax(null,
        '/TipoHoras/llenarTabla',
        'POST',
        function (data) {
            if (data.length > 0) {
                console.log(data);
            }
            var IndexTable = $('#IndexTable').DataTable();
            IndexTable.clear();
            IndexTable.draw();
            $.each(data, function (i, item) {
                //console.log(item.tiho_Descripcion);
                IndexTable.row.add(['<tr data-id = "' + item.tiho_Id + '">' +
                    item.tiho_Descripcion, item.tiho_Recargo,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<button type='button' class='btn btn-primary btn-xs tablaDetalle' id='btnDetalle' data-toggle='modal' data-id=" + item.tiho_Id + " data-target='#ModalDetalles'>Detalle</button>" +
                        "<button type='button' class='btn btn-default btn-xs tablaEditar' id='btnEditarR' data-toggle='modal' data-id=" + item.tiho_Id + " data-target='#ModalEditar'>Editar</button>" +
                    "</div>"]).draw();
            
            });
            AllFunctions();
        });
}



