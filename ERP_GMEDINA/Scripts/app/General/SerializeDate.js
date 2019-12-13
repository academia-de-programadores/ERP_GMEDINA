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