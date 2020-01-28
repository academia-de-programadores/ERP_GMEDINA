var ChildTable = null;
var list = [];
var inactivar = [];
var dRow = null;
var Entidad = '';

$("#btnInactivar").on("click", function () {
    $("#depto_RazonInactivo").val("");
    $('#ModalEditar').modal('hide');
    $('#ModalInactivar').modal('toggle');
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#depto_RazonInactivo").focus();
    Entidad = "Depto";
});

$("#btnInactivarArea").on("click", function () {
    $('#ModalInactivar').modal('toggle');
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#depto_RazonInactivo").focus();
    Entidad = "Area";
});

$("#ModalInactivar").find("#InActivar").on("click", function () {
    if (Entidad == 'Depto') {
        var depto =
        {
            depto_Id: dRow.data().Id,
            depto_RazonInactivo: $("#ModalInactivar").find("#depto_RazonInactivo").val(),
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
        $('#ModalInactivar').modal('hide');
    } else {
        var area_Razoninactivo = $("#ModalInactivar").find("#depto_RazonInactivo").val()
        _ajax(JSON.stringify({ area_Razoninactivo: area_Razoninactivo }),
            '/Areas/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    //LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
                    //MsgSuccess("¡Exito!", "Se ah Eliminado el Area");
                    $(location).attr('href', '/Areas');
                } else {
                    MsgError("Error", "No se editó el registro, contacte al administrador.");
                }
            });
    }
});
