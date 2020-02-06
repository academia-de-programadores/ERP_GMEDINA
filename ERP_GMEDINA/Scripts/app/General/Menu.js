$(document).ready(function () {

    debugger;

    // recuperar información del usuario para las validaciones de permisos
    $.ajax({
        url: "/Login/LoadUserModelState",
        method: "GET"        
    }).done(function (data) {

        console.log(data);

        // validar respuesta del servidor
        if (data == '') {
            // mensaje de error
            iziToast.error({
                title: 'Error',
                message: 'No guardó el registro, contacte al administrador',
            });
        }
        else {
            sessionStorage.setItem("VM_ModelState", data);

            iziToast.success({
                title: 'Exito',
                message: '¡El parece que funcionó!',
            });
        }
    });
});