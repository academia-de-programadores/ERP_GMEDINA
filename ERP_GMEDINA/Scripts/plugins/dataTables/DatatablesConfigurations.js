$(document).ready(function () {
	$('.dataTables-example').DataTable({
		"language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
		responsive: true,
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

		]
	});
});