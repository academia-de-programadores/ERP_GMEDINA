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
//    //var data = new FormData();
//    //var Archivo="ArchivoEmpleados";
//    var $file = $("#btnUpload").get(0).files;
//    $formData = new FormData();

//   // var files = $("#btnUpload").get(0).files;
//    if ($file.length == 0) {
//        MsgError("¡Error!", "Debe Agregar el archivo de excel");

//    } else {
//        $formData.append("UploadedFile", $file);

//    }
//INTENTAR CON POST
//    //if ($file.files.length > 0) {
//    //    for (var i = 0; i < $file.files.length; i++) {
//    //        $formData.append('file-' + i, $file.files[i]);
//    //    }
//    //}
//    $.ajax({
//        url: '/Empleados/UploadEmpleados',
//        type: 'POST',
//        data: $file,
//        //dataType: 'json',
//        contentType: false,
//        processData: false,
//        success: function ($data) {

//        }
//    });

          
//});