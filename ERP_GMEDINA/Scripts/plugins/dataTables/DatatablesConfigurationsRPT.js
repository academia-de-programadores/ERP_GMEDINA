$(document).ready(function () {

	$('.aguinaldos').DataTable({
		"language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
		pageLength: 10,
		dom: '<"html5buttons"B>lTfgtpi',
		buttons: [
            {
            	extend: 'copy',
            	text: '<i class="fa fa-copy btn-xs"></i>',
            	titleAttr: 'Copiar',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4, 5, 6, 7],
            	},
            	className: 'btn btn-primary'

            },

            {
            	extend: 'excel',
            	text: '<i class="fa fa-file-excel-o btn-xs"></i>',
            	titleAttr: 'Excel',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4, 5, 6, 7],
            	},
            	className: 'btn btn-primary',
            	title: 'Excel'
            }

		]
    });

    $('.deducciones').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        pageLength: 10,
        dom: '<"html5buttons"B>lTfgtpi',
        buttons: [
            {
                extend: 'copy',
                text: '<i class="fa fa-copy btn-xs"></i>',
                titleAttr: 'Copiar',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6],
                },
                className: 'btn btn-primary'

            },

            {
                extend: 'excel',
                text: '<i class="fa fa-file-excel-o btn-xs"></i>',
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6],
                },
                className: 'btn btn-primary',
                title: 'Excel'
            }

        ]
    });

    $('.especificos').DataTable({
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        pageLength: 10,
        dom: '<"html5buttons"B>lTfgtpi',
        buttons: [
            {
                extend: 'copy',
                text: '<i class="fa fa-copy btn-xs"></i>',
                titleAttr: 'Copiar',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                },
                className: 'btn btn-primary'

            },

            {
                extend: 'excel',
                text: '<i class="fa fa-file-excel-o btn-xs"></i>',
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                },
                className: 'btn btn-primary',
                title: 'Excel'
            }

        ]
    });





});