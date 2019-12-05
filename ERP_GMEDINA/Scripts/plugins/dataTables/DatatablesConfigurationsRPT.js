$(document).ready(function () {
	$('.rpt').DataTable({
		"language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
		pageLength: 10,
		dom: '<"html5buttons"B>lTfgitp',
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
            },
            {
            	extend: 'pdf',
            	text: '<i class="fa fa-file-pdf-o btn-xs"></i>',
            	titleAttr: '',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4, 5, 6, 7],
            	},
            	className: 'btn btn-primary',
            	title: 'Reporte | Decimo Tercer Mes'
            },
            {
            	extend: 'print',
            	title: 'Reporte | Decimo Tercer Mes',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4, 5, 6, 7],
            	},
            	className: 'btn btn-primary',
            	text: '<i class="fa fa-print btn-xs"></i>',
            	titleAttr: 'Imprimir'
            }

		]
	});
});