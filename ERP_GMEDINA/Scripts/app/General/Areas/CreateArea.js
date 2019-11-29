var ChildTable = null;
$(document).ready(function () {
    ChildTable = $(ChildDataTable).DataTable({
        data: [
            { Descripcion:'' },
            { Cargo: '' },
            {
                Acciones: '<div>' +
                                    '<input type="button" class="btn btn-white edit" value="Editar" />'+
                                    '<input type="button" class="btn btn-danger remove" value="Remover" />'+
                                '</div>'
            }
        ],
        order: [[1, 'asc']]
    });
});