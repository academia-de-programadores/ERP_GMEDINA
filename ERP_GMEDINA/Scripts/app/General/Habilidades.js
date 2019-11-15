$(document).ready(function () {
    AsignarFunciones();
});
function AsignarFunciones() {
    $("#btnAgregar").click(function () {
        var modalnuevo = $('#ModalNuevo');
        $("#FormNuevo").find("#habi_Descripcion").val("");
        modalnuevo.modal('show');
    });
    $(".tablaEditar").click(function () {
        var tr = this.closest("tr");
        id = $(this).data("id");
        _ajax(null,
            '/Habilidades/Edit/' + id,
            'GET',
            function (obj) {
                if (obj != "-1" || obj != "-2" || obj != "-3") {
                    $("#FormEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                    $('#ModalEditar').modal('show');
                }
            });
    });
    $("#btnInhabilitar").click(function () {
        CierraPopup();
        $('#ModalDelete').modal('show');
    });
    $("#btnGuardar").click(function () {
        var data = $("#FormNuevo").serializeArray();
        data = serializar(data);
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Create',
            'POST',
            function (obj) {
                if (obj != "-1" || obj != "-2" || obj != "-3") {
                    CierraPopup();
                    llenarTabla();
                }
            });
    });
    $("#InActivar").click(function () {
        var data = $("#FormEditar").serializeArray();
        data = serializar(data);
        data.habi_Id = id;
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" || obj != "-2" || obj != "-3") {
                    CierraPopup();
                    llenarTabla();
                }
            });
    });
    $("#btnActualizar").click(function () {
        var data = $("#FormEditar").serializeArray();
        data = serializar(data);
        data.habi_Id = id;
        data = JSON.stringify({ tbHabilidades: data });
        _ajax(data,
            '/Habilidades/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" || obj != "-2" || obj != "-3") {
                    CierraPopup();
                    llenarTabla();
                }
            });
    });
}

function CierraPopup() {
    var modal = ["ModalNuevo", "ModalEditar", "ModalDelete"];
    $.each(modal, function(index,valor) {
        $("#" + valor).modal('hide');//ocultamos el modal
        $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    })        
}
function _ajax(params, uri, type, callback) {
    $.ajax({
        url: uri,
        method: type,
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        data: params,
        success: callback
    });
}
function serializar(data) {
    var Data = new Object();
    $.each(data, function (index,valor) {
        Data[valor.name] = valor.value;
    });
    return Data;
}
function llenarTabla() {
    _ajax(null,
        '/Habilidades/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                console.log(value.habi_Descripcion);
                tabla.row.add([value.habi_Descripcion,
                    "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                    "<a class='btn btn-primary btn-xs' href='/Habilidades/Details/" + value.habi_Id + "' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs tablaEditar' data-id=" + value.habi_Id + ">Editar</a>" +
                    "</div>"]).draw();
            });
            AsignarFunciones();
        });
}