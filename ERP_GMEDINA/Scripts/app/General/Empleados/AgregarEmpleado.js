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
$("#btnGuardar").click(function () {

    var inputFileImage = document.getElementById("#btnUpload").value;
    //var texto = document.getElementById("texto").value;
    //alert(texto + inputFileImage);
    $.ajax({
        url: '/Empleados/UploadEmpleados',
        type: "POST",
        data: { FileUpload: inputFileImage }
    });
    //var data = new FormData();
    //var Archivo="ArchivoEmpleados";
//    var $file = $("#btnUpload").get(0).files;
//    var $formData = new FormData();

//   //// var files = $("#btnUpload").get(0).files;
//    if ($file.length == 0) {
//        MsgError("¡Error!", "Debe Agregar el archivo de excel");

//    } else {
//        $formData.append("UploadedFile", $file);

//    }
//    //var data = new FormData();
//    //jQuery.each(jQuery('#btnUpload')[0].files, function (i, file) {
//    //    data.append('btnUpload-' + i, file);
//    //});


//////INTENTAR CON POST
//    //if ($file.files.length > 0) {
//    //    for (var i = 0; i < $file.files.length; i++) {
//    //        $formData.append('file-' + i, $file.files[i]);
//    //    }
//    //}
//    $.ajax({
//        url: '/Empleados/UploadEmpleados',
//        type: "post",
//        //dataType: "html",
//        data: $formData,
//        cache: false,
//        contentType: false,
//        processData: false
//    })
//    //$.post('/Empleados/UploadEmpleados', formData);
          
});