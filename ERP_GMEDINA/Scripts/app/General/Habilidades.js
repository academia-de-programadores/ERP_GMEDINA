var id = 0;
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Habilidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" || obj != "-2" || obj != "-3") {
                $("#FormEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                $('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(ID) {
    id = ID;
    _ajax(null,
        '/Habilidades/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" || obj != "-2" || obj != "-3") {
                $("#ModalDetalles").find("#habi_Descripcion")["0"].innerText = obj.habi_Descripcion;
                $("#ModalDetalles").find("#habi_Estado")["0"].innerText = obj.habi_Estado;
                $("#ModalDetalles").find("#habi_RazonInactivo")["0"].innerText = obj.habi_RazonInactivo;
                $("#ModalDetalles").find("#habi_FechaCrea")["0"].innerText = FechaFormato(obj.habi_FechaCrea);
                $("#ModalDetalles").find("#habi_FechaModifica")["0"].innerText = FechaFormato(obj.habi_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}
function CierraPopup() {
    var modal = ["ModalNuevo", "ModalEditar", "ModalDelete", "ModalDetalles"];
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
                    "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.habi_Id + ")' >Detalles</a>" +
                        "<a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.habi_Id + ")'>Editar</a>" +
                    "</div>"]).draw();
            });
        });
}
function FechaFormato(pFecha) {
    if (pFecha != null && pFecha!=undefined) {
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
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    $("#FormNuevo").find("#habi_Descripcion").val("");
    modalnuevo.modal('show');
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/Habilidades/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" || obj != "-2" || obj != "-3") {
                CierraPopup();
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