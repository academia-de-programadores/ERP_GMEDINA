var tabla = null;
var Admin = false;
var textoBoton = 'Mostrar activos';
var botones = [];
$(document).ready(function () {
    var columnas = [];
    var col = 0;
    var contador = -1;
    //Al cargar el documento, esta funcion identificara y nombrara los paramtros de la tabla
    var head = $("#IndexTable thead tr").find("th").each(function (indice, valor) {
        contador = contador + 1;
        campo = valor.innerText;
        //Quita los espacios del enacabezado.
        //El nombre del campo en el Json sera el DisplayName de la clase parcial SIN espacios, respetando mayusculas.
        campo = campo.replace(/\s/g, '');
        //Si la primera columna no tiene encabezado, sera la columna de botones de expandir
        if (campo == "") {
            columnas.push({
                className: 'details-control',
                orderable: false,
                data: null,
                defaultContent: ''
            });
            col = col + 1;
        }
            //Si la columna tiene el nombre de ID se ocultara al usuario, la informacion seguira en el Json de DataTables
        else if (campo.toUpperCase() == "ID") {
            columnas.push({
                data: campo,
                visible: false
            });
            col = col + 1;
        }



            //Si la columa tiene el nombre de "Acciones", automaticamente insertara los botones de Detalles y Editar
        else if (campo == "Acciones") {
            columnas.push({
                data: campo,
                orderable: false,
                defaultContent: "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                                    "<a class='btn btn-primary btn-xs' style= 'min-width: 70px;' onclick='CallDetalles(this)' >Detalles</a>" +
                                    "<a class='btn btn-default btn-xs' style= 'min-width: 70px;' onclick='CallEditar(this)'>Editar</a>" +
                                    "<a class='btn btn-success btn-xs ' style= 'min-width: 70px;' onclick='CallContratar(this)'>Contratar</a>" +
                                    "<a class='btn btn-danger btn-xs ' style= 'min-width: 70px;'  onclick='CallEliminar(this)'>Inactivar</a>" +
                                "</div>"
            });
        }
        else if (campo == "Info") {
            columnas.push({
                data: null,
                orderable: false,
                defaultContent: "<div class='visible-md visible-lg hidden-sm hidden-xs action-buttons'>" +
                                    "<a class='btn btn-primary btn-xs ' onclick='CallDetalles(this)' >Detalles</a>" +
                                    " <a class='btn btn-default btn-xs ' onclick='CallEditar(this)' >Archivos</a>" +
                                "</div>"
            });
        }
        else {
            columnas.push({ data: campo });
            botones.push(contador);
        }
    });
    tabla = $('#IndexTable').DataTable({
        "language": language,
        responsive: true,
        "scrollX": true,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "autoWidth": false,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
                  {
                      extend: 'copy',
                      text: '<i class="fa fa-copy btn-xs"></i>',
                      exportOptions: {
                          columns: botones
                      }
                  }
        ],
        //Aqui se le pasa al DataTables la estructura de la tabla con sus parametros correspondientes
        columns: columnas,
        order: [[col, 'asc']]
    });
});
function CallDetalles(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    tablaDetalles(id);
}
function CallEditar(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    tablaEditar(id);
}
