function pad2(number) {
    return (number < 10 ? '0' : '') + number
}

function FechaFormato(pFecha) {
    if (!pFecha)
        return "Sin modificaciones";
    var fechaString = pFecha.substr(6, 19);
    var fechaActual = new Date(parseInt(fechaString));
    var mes = fechaActual.getMonth() + 1;
    var dia = pad2(fechaActual.getDate());
    var anio = fechaActual.getFullYear();
    var hora = pad2(fechaActual.getHours());
    var minutos = pad2(fechaActual.getMinutes());
    var segundos = pad2(fechaActual.getSeconds().toString());
    var FechaFinal = dia + "/" + mes + "/" + anio + " " + hora + ":" + minutos + ":" + segundos;
    return FechaFinal;
}

function FechaFormatoNac(pFecha) {
    var fechaString = pFecha.substr(6, 19);
    var fechaActual = new Date(parseInt(fechaString));
    var mes = fechaActual.getMonth() + 1;
    var dia = pad2(fechaActual.getDate());
    var anio = fechaActual.getFullYear();
    var FechaFinal = dia + "/" + mes + "/" + anio;
    return FechaFinal;
}

function FechaFormatoInvertido(pFecha) {
    var fechaString = pFecha.substr(6, 19);
    var fechaActual = new Date(parseInt(fechaString));
    var mes = fechaActual.getMonth() + 1;
    var dia = pad2(fechaActual.getDate());
    var anio = fechaActual.getFullYear();
    var hora = pad2(fechaActual.getHours());
    var minutos = pad2(fechaActual.getMinutes());
    var segundos = pad2(fechaActual.getSeconds().toString());
    var FechaFinal = anio + "/" + mes + "/" + dia;
    return FechaFinal;
}

function FullBody() {
    $("#Body").css("padding-right", "0px");
}

function userModelState(sPantalla) {
    var response = {
        status: true,
        mensajeError : ''
    }

    // recuperar view model con la información del usuario
    var VM_ModelState = JSON.parse(localStorage.getItem("VM_ModelState"));

    // validar si la información del usuario ya se cargó en el AJAX del menú
    if (VM_ModelState == '' || VM_ModelState == null) {
        // mensaje de error
        iziToast.warning({
            title: 'Advertencia',
            message: 'Se está cargando la información de usuario',
        });
        return false;
    }

    // validar si el usuario es administrador
    if (VM_ModelState.EsAdmin == true) {
        response = {
            status: true,
            mensajeError: ''
        }
        return response;
    }
    
    // validar si el usuario tiene acceso a la accion o pantalla
    if (validarPermisoUsuario(sPantalla, VM_ModelState.ListaPantallas.List) == false)
    {
        response = {
            status: false,
            mensajeError: 'No tiene permiso para realizar esta acción'
        }
        // mensaje de error
        iziToast.error({
            title: 'Error',
            message: 'No tiene permiso para realizar esta acción',
        });
    }

    // validar si la sesion es válida 
    if (VM_ModelState.SesionIniciada == false)
    {
        response = {
            status: false,
            mensajeError: 'La sesión es inválida'
        }
        // mensaje de error
        iziToast.error({
            title: 'Error',
            message: 'La sesión es inválida',
        });
    }

    // validar los roles del usuario
    if (VM_ModelState.CantidadRoles == 0)
    {
        response = {
            status: false,
            mensajeError: 'Roles de usuario inválidos'
        }

        // mensaje de error
        iziToast.error({
            title: 'Error',
            message: 'No tiene roles asignados',
        });
    }

    // validar si la contraseña del usuario expiró y debe cambiar
    if (VM_ModelState.ContraseniaExpirada == false)
    {
        response = {
            status: false,
            mensajeError: 'Contraseña expirada'
        }
        // mensaje de error
        iziToast.error({
            title: 'Error',
            message: 'Contraseña expirada',
        });
    }

    return response;
}

// funcion para validar si el usuario tiene permiso o acceso a una accion
function validarPermisoUsuario(sPantalla, arreglo) {
    var status = false;

    // iterar arreglo de la lista de pantallas y acciones a las que tiene acceso el usuario
    for (var i = 0; i < arreglo.length; i++) {

        // obtener pantalla o accion del indice actual del arreglo
        var pantallaIndexArreglo = arreglo[i].obj_Referencia;

        // comprar la pantalla del indice actual del arreglo con la pantalla o accion recibida
        if (pantallaIndexArreglo == sPantalla) {
            status = true;
            break;
        }
    }

    // retornar el resultado
    return status;
}

var timeOut = 0;

var timer = setInterval(()=>{
    console.log(++timeOut);
    if(timeOut == 10){
        cerrarSesion();
    }
}, 100000);

function cerrarSesion(){
        sessionStorage.clear();
        window.location = '/Login/CerrarSesion';
}

function resetTimeOut(){
    timeOut = 0;
}
