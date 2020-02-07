$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("Empleados/ArchivoEmpleados");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
    }
    
});
$("#btnUpload").change(function () {
    var fileExtension = ['xls','xlsx'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        MsgError("Error", "Debe agregar el archivo de excel correspondiente.");
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
            MsgError("Error", "Codigo:"+res+" Contacte al administrador.");
        }
        else if (res == "-2")
        {
            MsgError("Error", "El archivo ya existe.");
        }
        else if (res == "-3") {
            MsgError("Error", "El archivo ya existe.");
        }
        else if (res == "1") {
            MsgSuccess("¡Éxito!", "Los registros se agregaron de forma exitosa.");
            llenarTabla();
            $('#ModalNuevo').modal('hide');
        }
        else if (res == "-4") {
            MsgError("Error", "Debe agregar el archivo de excel correspondiente.");
        }
    });
});


function tablaDetalles(ID) {
    var validacionPermiso = userModelState("Empleados/Details");
    if (validacionPermiso.status == true) {
        id = ID;
        sessionStorage.setItem("emp_Id", ID);
        $(location).attr('href', "/Empleados/details/" + id);
    }
}