var ChildTable = null;
var list = [];
var inactivar = [];
var dRow = null;
var Entidad = '';

$("#btnInhabilitar").on("click", function () {
    $("#depto_RazonInactivo").val("");
    $('#ModalEditar').modal('hide');
    $('#ModalInhabilitar').modal('toggle');
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#depto_RazonInactivo").focus();
    Entidad = "Depto";
});

$("#btnInactivarArea").on("click", function () {
    $('#ModalInhabilitar').modal('toggle');
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#depto_RazonInactivo").focus();
    Entidad = "Area";
});

$("#ModalInhabilitar").find("#InActivar").on("click", function () {
    if (Entidad == 'Depto') {
        var depto =
        {
            depto_Id: dRow.data().Id,
            depto_RazonInactivo: $("#ModalInhabilitar").find("#depto_RazonInactivo").val(),
        };
        if (depto.depto_RazonInactivo.trim() == '') {
            return null;
        }
        inactivar.push(depto);
        ChildTable
            .row(dRow)
            .remove()
            .draw();
        dRow = null;
        $('#ModalInhabilitar').modal('hide');
    } else {
        var area_Razoninactivo = $("#ModalInhabilitar").find("#depto_RazonInactivo").val()
        _ajax(JSON.stringify({ area_Razoninactivo: area_Razoninactivo }),
            '/Areas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
                    //MsgSuccess("¡Exito!", "Se ah Eliminado el Area");
                    $(location).attr('href', '/Areas');
                } else {
                    MsgError("Error", "No se pudo editar el registro, contacte al administrador");
                }
            });
    }
});
