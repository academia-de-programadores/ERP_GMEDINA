var dataTable;
var listadoCesantia = new Array();
(function () {

    $(document).ready(function () {
        dataTable = $('#Tabla').DataTable({
            "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
            responsive: true,
            destroy: true,
            pageLength: 10,
            dom: '<"html5buttons"B>lTfgt<"pull-left"i><"col-sm-pull-11"p>',
            buttons: [
                {
                    extend: 'copy',
                    text: '<i class="fa fa-copy btn-xs"></i>',
                    titleAttr: 'Copiar',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4],
                    },
                    className: 'btn btn-primary'
                }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        $.map(dataTable.rows().data(), function (x) {
            listadoCesantia.push({ idEmpleado: x[0], totalCesantia: x[6], diasPagados: x[4], sueldoBruto: x[5] });
        });
    });

    $('#btnImprimirExcel').click(function () {
        $.post('ProcesarCesantia',
            { listadoCesantia },
            function (response) {
                if (response == "bien") {
                    iziToast.success({
                        title: 'Éxito',
                        message: 'Se ha deducido la cesantía de forma exitosa!',
                    });
                } else {
                    iziToast.error({
                        title: 'Error',
                        message: '¡Ha ocurrido un error al deducir la cesantía, contacte al administrador!',
                    });
                }
            }).done((x) => {

            });
    });
})();