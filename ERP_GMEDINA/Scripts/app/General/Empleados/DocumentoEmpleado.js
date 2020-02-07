var id = 0;
function tablaEditar(ID) {
    var validacionPermiso = userModelState("Empleados/ArchivoEmpleados");
    if (validacionPermiso.status == true) {
        id = ID;
     
                    // $("#FormEditar").find("#car_Descripcion").val(obj.car_Descripcion);
                    $('#ModalEditar').modal('show');
                    $("#ModalNuevo").find("#FileUpload")[0] = '';
                
      
    }
}
$("#btnUploadDocument").change(function () {
    var fileExtension = ['xls', 'xlsx', 'jpeg', 'jpg', 'png', 'pdf', 'svg', 'doc', 'docx'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        MsgError("Error", "Debe agregar el tipo de archivo correspondiente.");
    }
});

//$("#FormEmpleados").submit(function (e) {
//    e.preventDefault();
//    var file = $("#FormEmpleados").
//});

$("#FormEmpleadosDocument").on("submit", function (event) {
    event.preventDefault();
    //var data = $("#FormEmpleadosDocument").serializeArray();
    //data = serializar(data);
    //var f = $(this);
    var parametros = new FormData($(this)[0]);
    
    var data = $("#FormEmpleadosDocument").serializeArray()[0].value;
    //var data=$('#tiposArchivo option:selected').val();
    //data = serializar(data);

    if (data != null) {
        //var data = new FormData($("#FormEmpleadosDocument")[0]);
        //data.append('file', $('#FileUpload')[0].files[0]);

        var FileData = ($('#FileUpload')[0].files[0]);
        var UploadFile = new FormData();

        UploadFile.append("File", FileData);
        UploadFile.append("tiposArchivo", data);
        UploadFile.append("id", id);

        //data = serializar(data);
        
        //data = JSON.stringify({ data: data });
        $.ajax({
            url: "/Empleados/UploadDocumento",
            type: "post",
            dataType: "html",
            data: UploadFile,
            cache: false,
            contentType: false,
            processData: false
        })
        .done(function (res) {
            if (res == "-1") {
                MsgError("Error", "No se agregó el archivo, contacte al administrador.");
            }
            else if (res == "-2") {
                MsgError("Error", "El archivo ya existe.");
            }
            else if (res == "-3") {
                MsgError("Error", "El archivo ya existe o su nombre es duplicado.");
            }
            else if (res == "1") {
                MsgSuccess("¡Éxito!", "El archivo se agregó de forma exitos.a");
                llenarTabla();
                $('#ModalEditar').modal('hide');
            }
            else if (res == "-4") {
                MsgError("Error", "Debe agregar el archivo correspondiente.");
            }

        });
    } else {
        MsgError("Error", "Debe agregar el archivo correspondiente.");
    }
});
