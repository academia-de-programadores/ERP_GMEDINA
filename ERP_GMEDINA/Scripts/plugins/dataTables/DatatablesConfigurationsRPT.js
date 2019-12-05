$(document).ready(function () {
	$('.rpt').DataTable({
		"language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
		responsive: true,
		pageLength: 10,
		dom: '<"html5buttons"B>lTfgitp',
		buttons: [
            {
            	extend: 'copy',
            	text: '<i class="fa fa-copy btn-xs"></i>',
            	titleAttr: 'Copiar',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4],
            	},
            	className: 'btn btn-primary'

            },

            {
            	extend: 'excel',
            	text: '<i class="fa fa-file-excel-o btn-xs"></i>',
            	titleAttr: 'Excel',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4],
            	},
            	className: 'btn btn-primary',
            	title: 'Excel'
            },
            {
            	extend: 'pdf',
            	text: '<i class="fa fa-file-pdf-o btn-xs"></i>',
            	titleAttr: 'PDF',
            	exportOptions: {
            		columns: [0, 1, 2, 3, 4],
            	},
            	className: 'btn btn-primary',
            	title: 'PDF'
            },
            {
            	extend: 'print',
            	customize: function (win) {
            		$(win.document.body)
                        .css('font-size', '10pt')
                        .prepend($('<img />')
                            .attr('src', 'Hospital_BI/assets/img/bi-stroke.png')
                            .addClass('asset-print-img')
                        );

            		$(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
            	},
            	messageTop: 'Reporte',

            	exportOptions: {
            		columns: [0, 1, 2, 3, 4],
            	},
            	className: 'btn btn-primary',
            	text: '<i class="fa fa-print btn-xs"></i>',
            	titleAttr: 'Imprimir'
            }

		]
	});
});