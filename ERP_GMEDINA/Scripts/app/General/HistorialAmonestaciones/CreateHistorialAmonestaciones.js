var ChildTable = null;
var llist = [];
function Remove(Id, lista) {
    var list = [];
    lista.forEach(function(value,index){
        if(value.Id!=Id){
            list.push(value);
        }
    });
    return list;
}
function Add(emp_Id, tamo_Id) {
    for (var i = 0; i < ChildTable.data().length; i++) {
        var Fila = ChildTable.rows().data()[i];
        if (Fila.Empleado == Empleado || Fila.TipoAmonestacion == TipoAmonestacion) {
            if (Fila.Empleado == Empleado) {
                MsgError("Error","Ya Existe el Empledo")
            }
        }
    }
}