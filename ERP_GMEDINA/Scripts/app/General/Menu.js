$(document).ready(function () {

    // script formatos fechas
    $.getScript("../Scripts/app/General/SerializeDate.js")
        .done(function (script, textStatus) {

        })
        .fail(function (jqxhr, settings, exception) {

        });

    // recuperar información del usuario para las validaciones de permisos
    $.ajax({
    	url: "/Login/LoadUserModelState",
        method: "GET"
    }).done(function (data) {
        // validar respuesta del servidor
        if (data == '') {
            // mensaje de error
            iziToast.error({
                title: 'Error',
                message: 'Error al recuperar información de la sesión, contacte al administrador',
            });
        }
        else {
            sessionStorage.setItem("VM_ModelState", JSON.stringify(data));

            // codigo de ejemplo  de como implementar

            // validar informacion del usuario
            var validacionPermiso = userModelState("Planilla/Index");

            if (validacionPermiso.status == true) {

                // activar
                console.log('codigo...');
                // termina activar
            }

            // codigo de ejemplo  de como implementar
        }
    });
});
