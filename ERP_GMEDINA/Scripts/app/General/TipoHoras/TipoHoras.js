var id = 0;

$(document).ready(function () {
    //AllFunctions();

});
//function AllFunctions() {

    //AGREGAR HORARIOS///
    $('#btnAgregar').click(function () {
        var data = $("#frmAgregarTipoHoras").serializeArray();
        if (data!=null) {
            _ajax(data,
                    '/TipoHoras/Create',
                   'POST',
                   function (obj) {
                       if (obj != "-1" && obj != "-2" && obj != "-3") {
                           CierraPopups();
                           llenarTabla();
                           LimpiarControles(["tiho_Descripcion", "tiho_Recargo"]);
                           MsgSuccess("¡Exito!", "Se ah agregado el registro");
                       } else {
                           MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                       }
                   });
        } else {
            MsgError("Error", "por favor llene todas las cajas de texto");
        }
        });
    
    //AGREGAR HORARIOS///

    $("#btnEditarM").click(function () {
       // id = ID;
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
                        $("#ModalEdit #tiho_Recargo").val(item.tiho_Recargo);
                        //$("#ModalEdit").find("#btnInhabilitarModal").dataset.id = id;
                        //$("#ModalEdit #tiho_UsuarioCrea").val(item.tiho_UsuarioCrea)
                        //$("#ModalEdit #tiho_FechaCrea").val(item.tiho_FechaCrea);
                        $('#ModalEditar').modal('show');
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
            if (data == "-1" || data == "2") {

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
                $('#ModalEditar').modal('hide');
                //llenarTabla();

                llenarTabla();
            }
        });
    });
    //////////////

//}

  

//////FUNCION PARA INHABILITAR////////////
    //INHABILITAR
    $("#btnInhabilitar").click(function () {

        // var data = $("#frmInhabilitarTipoHoras").serializeArray();
        //SE ENVIA EL JSON AL SERVIDOR PARA EJECUTAR LA EDICIÓN
        var tbTipoHoras = $("#frmInhabilitarTipoHoras").serializeArray();
        //tbTipoHoras = serializar(tbTipoHoras);
        //tbTipoHoras.tiho_Id = id;
        //tbTipoHoras = JSON.stringify({ tbTipoHoras: tbTipoHoras });
        console.log(tbTipoHoras);
        $.ajax({
            url: "/TipoHoras/Inactivar",
            method: "POST",
            data: tbTipoHoras
        }).done(function (tbTipoHoras) {
            if (tbTipoHoras == "-1" || tbTipoHoras == "2") {
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
                $('#ModalEditar').modal('hide');
                $('#ModalInhabilitar').modal('hide');
                LimpiarControles()
            }
        });
    });

//////FUNCION PARA INHABILITAR////////////


///////FUNCIONES PARA EDITAR///////////
//FUNCION:MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
function tablaEditar(ID) {
    id = ID;
    $.ajax({
        url: "/TipoHoras/Edit/" + id,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: id })
    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#ModalEdit #tiho_Id").val(item.tiho_Id)
                    $("#ModalEdit #tiho_Descripcion").val(item.tiho_Descripcion);
                    $("#ModalEdit #tiho_Recargo").val(item.tiho_Recargo);
                    $('#ModalEditar').modal('show');
                })
            }
        });
}
//MODAL DETALLES
function tablaDetalle(ID) {
    id = ID;
    $.ajax({
        url: "/TipoHoras/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
        .done(function (data) {
            if (data != "-1" && data != "-2" && data != "-3") {
                $("#ModalDetallesR").find("#tiho_Descripcion")["0"].innerText = data.tiho_Descripcion;
                $("#ModalDetallesR").find("#tiho_Recargo")["0"].innerText = data.tiho_Recargo;
                $("#ModalDetallesR").find("#tiho_Estado")["0"].innerText = data.tiho_Estado;
                $("#ModalDetallesR").find("#tiho_FechaCrea")["0"].innerText = FechaFormato(data.tiho_FechaCrea);
                $("#ModalDetallesR").find("#tiho_FechaModifica")["0"].innerText = FechaFormato(data.tiho_FechaModifica);
                $("#ModalDetallesR").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = data.tbUsuario.usu_NombreUsuario;
                $("#ModalDetallesR").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = data.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditarM")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
         
        });
    // });
}

//FUNCION: CARGAR DATA Y REFRESCAR LA TABLA DEL INDEX
function llenarTabla() {
    _ajax(null,
        '/TipoHoras/llenarTabla',
        'POST',
        function (data) {
            //if (data.length > 0) {
            //    console.log(data);
            //}
            var IndexTable = $('#IndexTable').DataTable();
            IndexTable.clear();
            IndexTable.draw();
            $.each(data, function (i, item) {
                //console.log(item.tiho_Descripcion);
                IndexTable.row.add(['<tr data-id = "' + item.tiho_Id + '">' +
                    item.tiho_Descripcion, item.tiho_Recargo,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<button type='button' class='btn btn-primary btn-xs tablaDetalle' id='btnDetalle' data-toggle='modal' onclick='tablaDetalle("+item.tiho_Id+")' data-target='#ModalDetalles'>Detalle</button>" +
                        "<button type='button' class='btn btn-default btn-xs tablaEditar' id='btnEditarR' data-toggle='modal' onclick='tablaEditar("+item.tiho_Id+")' data-target='#ModalEditar'>Editar</button>" +
                    "</div>"]).draw();
            
            });
            AllFunctions();
        });
}


//Modals
$("#ModalNuevo").on('hidden.bs.modal', function () {
    SetearClases("tiho_Descripcion","tiho_Recargo", "valid", "error");
});
$("#ModalEditar").on('hidden.bs.modal', function () {
    SetearClases("tiho_Descripcion", "tiho_Recargo", "valid", "error");
});
