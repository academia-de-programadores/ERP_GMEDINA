$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    
});
$("#btnUpload").change(function () {
    var fileExtension = ['xls','xlsx'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        MsgError("¡Error!", "Debe Agregar el archivo de excel correspondiente");
    }
});
//$("#btnGuardar").click(function () {
//    var data = new FormData();
//    //var Archivo="ArchivoEmpleados";
//    var files = $("#btnUpload").get(0).files;
//    if (files.length == 0) {
//        MsgError("¡Error!", "Debe Agregar el archivo de excel");

//    } else {
//        data.append("UploadedFile", files[0]);

//    }
//    //var ajaxRequest = $.ajax({
//    //    type: "POST",
//    //    url: '/Empleados/UploadEmpleados',
//    //    contentType: false,
//    //    processData: false,
//    //    data: data
//    //});

          
//});