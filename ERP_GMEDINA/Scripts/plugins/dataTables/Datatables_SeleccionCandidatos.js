var tabla = null;
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
        else {
            columnas.push({ data: campo });
            botones.push(contador);
        }
    });
    tabla = $('#IndexTable').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            {
                extend: 'copy',
                exportOptions: {
                    columns: botones
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: botones
                }
            },
            {
                extend: 'excel',
                exportOptions: {
                    columns: botones
                },
                title: 'ExampleFile'
            },
            {
                extend: 'pdf',
                exportOptions: {
                    columns: botones
                },
                title: 'nadaaa'
            },

            {
                extend: 'print',
                exportOptions: {
                    columns: botones
                },
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');



                    $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                }
            }
        ],
        //Aqui se le pasa al DataTables la estructura de la tabla con sus parametros correspondientes
        columns: columnas,
        order: [[col, 'asc']],
    });
});


function CallDetalles(btn) {
    var tr = $(btn).closest('tr');
    var row = tabla.row(tr);
    var id = row.data().ID;
    tablaDetalles(id);
}

//function CallEditar(btn) {
//    var tr = $(btn).closest('tr');
//    var row = tabla.row(tr);
//    var id = row.data().ID;
//    debugger
//    tablaEditar(id);
//}

function validarDT(obj) {
    if (obj == "-2") {
        //$("#ibox1").find(".ibox-content").hide();
        //$("#ibox1").append('verifique su conexion a internet. (Sí el problema persiste llame al administrador)');
        var ventana = $('#IndexTable tbody td.dataTables_empty');
        ventana[0].innerHTML = "verifique su conexion a internet. (Sí el problema persiste llame al administrador)";
        return true;
    } else {
        if (obj.Length == 0) {
            $("#ibox1").find(".ibox-content").hide();
            $("#ibox1").append('No hay registros para mostrar.');
        } else {
            return false;
        }
        return true;
    }
}
