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

//$("#FormEmpleados").submit(function (e) {
//    e.preventDefault();
//    var file = $("#FormEmpleados").
//});

$( "#FormEmpleados" ).on( "submit", function( event ) {
    event.preventDefault();
    var f = $(this);
    var parametros = new FormData($(this)[0]);

    $.ajax({
        url: "/Empleados/UploadEmpleados",
        type: "post",
        dataType: "html",
        data: parametros,
        cache: false,
        contentType: false,
        processData: false
    })
    .done(function (res) {
        if (res == "-1" ) {
            MsgError("Error", "Codigo:"+res+"Contacte al administrador");
        }
        else if (res == "-2")
        {
            MsgError("Error", "El archivo ya existe");
        }
        else if (res == "-3") {
            MsgError("Error", "El archivo ya existe");
        }
        else if (res == "1") {
            MsgSuccess("Exito", "Se han agregado los registros con exito");
            llenartabla();
            $('#ModalNuevo').modal('hide');
        }
    });
});