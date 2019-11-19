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

//$.each(function Validar (data) {
//    var isValid = true;
//    if ($.trim($(this).val()) == '') {
//        isValid = false;
//        $(this).css({
//            "border": "1px solid red",
//            "background": "#FFCECE"
//        });
//    }
//    else {
//        $(this).css({
//            "border": "",
//            "background": ""
//        });
//    }
//});
//if (isValid == false)
//    e.preventDefault();



function AllFunctions() {

    //AGREGAR HORARIOS///
    $('#btnAgregar').click(function () {
        //var isValid = true;
        $('#tiho_Descripcion,#tiho_Recargo').each(function () {
            if ($.trim($(this).val()) == ''|| $.trim($(this).val())==0) {
               // isValid = false;
                $(this).css({
                    "border": "1px solid red",
                    "background": "#ff9696"
                });
            
            }
            else {
                $(this).css({
                    "border": "",
                    "background": ""
                });
                //$("#tiho_Descripcion").removeClass("border", "background");
                //$("#tiho_Recargo").removeClass("border", "background");
            }
           
        }
        );
       
    
        var data = $("#frmAgregarTipoHoras").serializeArray();
        
        $.ajax({
            url: "/TipoHoras/Create",
            method: "POST",
            data: data
        }).done(function (data) {

            if (data == "-1" ) {
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
                $('#ModalCrear').modal('hide');
                //table.ajax.reload(null, false);
                $(this).css({
                    "border": "",
                    "background": ""
                });
                llenarTabla();
                LimpiarControles()
            }
        });
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

}

  

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

///////FUNCIONES PARA EDITAR///////////
//FUNCION:MOSTRAR MODAL CON LA INFORMACIÓN DEL REGISTRO SELECCIONADO
function tablaEditar(ID) {
    // $(document).on("click", "#IndexTable tbody tr td #btnEditarR", function () {
    // var id = $(this).closest('tr').data('id');
    // var tr = this.closest("tr");
    id = ID;
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
                })
            }

            //})



        });
}

////////CARGAR EL MODAL DE DETALLES/////////
//MODAL DETALLES
function tablaDetalle(ID) {
    // $(document).on("click", "#IndexTable tbody tr td #btnDetalle", function () {
    //var tr = this.closest("tr");
    id = ID;
    //var ide = id;
    //var id = $(this).closest('tr').data('id');
    $.ajax({
        url: "/TipoHoras/Details/" + ID,
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
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
                $("#ModalDetalles").find("#btnEditarM")["0"].dataset.id = id;
                //$('#ModalEditar').modal('show');
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

function FechaFormato(pFecha) {
    if (pFecha != null && pFecha != undefined) {
        var fechaString = pFecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = fechaActual.getMonth() + 1;
        var dia = pad2(fechaActual.getDate());
        var anio = fechaActual.getFullYear();
        var hora = pad2(fechaActual.getHours());
        var minutos = pad2(fechaActual.getMinutes());
        var segundos = pad2(fechaActual.getSeconds().toString());
        var FechaFinal = dia + "/" + mes + "/" + anio + " " + hora + ":" + minutos + ":" + segundos;
        return FechaFinal;
    }
    return '';
}
function pad2(number) {
    return (number < 10 ? '0' : '') + number
}

function LimpiarControles() {
    //$("#tiho_Id").val("");
    $("#tiho_Descripcion").val("");
    $("#tiho_Recargo").val("");
    $("#tiho_RazonInactivo").val("");


}