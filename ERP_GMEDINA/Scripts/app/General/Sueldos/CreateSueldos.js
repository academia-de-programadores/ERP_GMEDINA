var ChildTable = null;
var list = [];
function Remove(Id, lista) {
    var list = [];
    lista.forEach(function (value, index) {
        if (value.Id != Id) {
            list.push(value);
        }

    });
    return list;
}

function Add(sue_Cantidad) {
    if (sue_Cantidad.trim().length != 0) {
        for (var i = 0; i < ChildTable.data().lemgth; i++) {
            var Fila = ChildTable.rows().data()[i];
            if (Fila.Cantidad == sue_Cantidad) {
                var span = $("#FormSueldos").find("errorcar_cantidad");
                $(span).addClass("text-warning");
                $(span).closest("div").addClass("has-warning");
                span.text('La persona"' + sue_Cantidad + '" ya existe');
                $("#FormSueldos").find("#sue_Cantidad").focus();

            }
            if (Fila.Cantidad == sue_Cantidad) {
                var span =$("#FormSueldos").find("")
            }


        }


    }




}
