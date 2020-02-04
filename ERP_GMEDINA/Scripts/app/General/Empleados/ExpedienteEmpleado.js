$(document).ready(function () {
    CargarDatos();
    CargarDatosExpedienteViejo();
});


var id = sessionStorage.getItem("emp_Id");
function CargarDatos() {
    debugger
    var Amonestaciones = "";
    var Permisos = "";
    var Incapacidades = "";
    var Constancias = "";
    var Solicitudes = "";
    var Facturas = "";
    var Archivos_Personales = "";
    var Archivos_de_Finalizacion_Laboral = "";
    var Otros = "";
    //id = (id, 16);
    _ajax(null,
          '/Empleados/CargarArchivos/' + id,
          'GET',
          function (Lista) {
              $.each(Lista, function (index, value) {
                  var NombreArchivo = "";
                  if (value.direm_NombreArchivo.length > 30) {
                      NombreArchivo = value.direm_NombreArchivo.substring(0, 30) + "...";
                  }
                  else if (value.direm_NombreArchivo.length > 29) {
                      NombreArchivo = value.direm_NombreArchivo.substring(0, 31) + "...";
                  }
                  else if (value.direm_NombreArchivo.length > 28) {
                      NombreArchivo = value.direm_NombreArchivo.substring(0, 32) + "...";
                  }
                  else {
                      NombreArchivo = value.direm_NombreArchivo;
                  }//<a download href='/Expedientes/Expediente_1/Amonestaciones/ArchivoCasoPruebaCarlosUmanzor.docx'></a>
                  switch (value.direm_Carpeta) {
                      case "Amonestaciones":
                          Amonestaciones += "   <li class='dd-item' >" +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Amonestaciones/" + value.direm_NombreArchivo + "'></a></span>" +
                                             "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";
                          break;
                      case "Permisos":
                          Permisos += " <li class='dd-item'  >" +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Permisos/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Incapacidades":
                          Incapacidades += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Incapacidades/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Constancia":
                          Constancias += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Constancia/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Solicitud":
                          Solicitudes += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Solicitud/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Facturas":
                          Facturas += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Facturas/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Archivos_Personales":
                          Archivos_Personales += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Archivos_Personales/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Archivos_de_Finalizacion_Laboral":
                          Archivos_de_Finalizacion_Laboral += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Archivos_de_Finalizacion_Laboral/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;
                      case "Otros":
                          Otros += " <li class='dd-item'> " +
                                              "     <div class='dd-handle'>" +
                                              "         <span class='pull-right'> <a href='#' class='btn btn-danger btn-xs fa fa-trash' onclick='InactivarExpediente(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                              "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Otros/" + value.direm_NombreArchivo + "'></a></span>" +
                                              "         <span class='label label-info'></span>" + NombreArchivo +
                                              "     </div>" +
                                              " </li>";

                          break;

                      default:

                  }

              });


              $("#ListAmonestaciones").html(Amonestaciones).show();
              $("#ListPermisos").html(Permisos).show();
              $("#ListIncapacidades").html(Incapacidades).show();
              $("#ListConstancias").html(Constancias).show();
              $("#ListSolicitudes").html(Solicitudes).show();
              $("#ListFacturas").html(Facturas).show();
              $("#ListArchivos_Personales").html(Archivos_Personales).show();
              $("#ListArchivos_de_Finalizacion_Laboral").html(Archivos_de_Finalizacion_Laboral).show();
              $("#ListOtros").html(Otros).show();

              document.getElementById("ListAmonestaciones").style.display = 'none';
              document.getElementById("ListPermisos").style.display = 'none';
              document.getElementById("ListIncapacidades").style.display = 'none';
              document.getElementById("ListConstancias").style.display = 'none';
              document.getElementById("ListSolicitudes").style.display = 'none';
              document.getElementById("ListFacturas").style.display = 'none';
              document.getElementById("ListArchivos_Personales").style.display = 'none';
              document.getElementById("ListArchivos_de_Finalizacion_Laboral").style.display = 'none';
              document.getElementById("ListOtros").style.display = 'none';

              $("#btnMenosAmonestaciones").removeAttr("style").hide();
              $("#btnMasAmonestaciones").show();
              $("#btnMenosPermisos").removeAttr("style").hide();
              $("#btnMasPermisos").show();
              $("#btnMenosIncapacidades").removeAttr("style").hide();
              $("#btnMasIncapacidades").show();
              $("#btnMenosConstancias").removeAttr("style").hide();
              $("#btnMasConstancias").show();

              $("#btnMenosSolicitudes").removeAttr("style").hide();
              $("#btnMasSolicitudes").show();
              $("#btnMenosFacturas").removeAttr("style").hide();
              $("#btnMasFacturas").show();
              $("#btnMenosArchivos_Personales").removeAttr("style").hide();
              $("#btnMasArchivos_Personales").show();
              $("#btnMenosArchivos_de_Finalizacion_Laboral").removeAttr("style").hide();
              $("#btnMasArchivos_de_Finalizacion_Laboral").show();

              $("#btnMenosOtros").removeAttr("style").hide();
              $("#btnMasOtros").show();

          });


}

//////////////////
function CargarDatosExpedienteViejo() {
    var Amonestaciones = "";
    var Permisos = "";
    var Incapacidades = "";
    var Constancias = "";
    var Solicitudes = "";
    var Facturas = "";
    var Archivos_Personales = "";
    var Archivos_de_Finalizacion_Laboral = "";
    var Otros = "";
    //id = (id, 16);
    _ajax(null,
          '/Empleados/CargarArchivosExpedienteViejo/' + id,
          'GET',
          function (Lista) {
              if (Lista.length > 0) {
                  $("#ExpedienteViejoNestable").show();


                  $.each(Lista, function (index, value) {
                      var NombreArchivo = "";
                      if (value.direm_NombreArchivo.length > 30) {
                          NombreArchivo = value.direm_NombreArchivo.substring(0, 30) + "...";
                      }
                      else if (value.direm_NombreArchivo.length > 29) {
                          NombreArchivo = value.direm_NombreArchivo.substring(0, 31) + "...";
                      }
                      else if (value.direm_NombreArchivo.length > 28) {
                          NombreArchivo = value.direm_NombreArchivo.substring(0, 32) + "...";
                      }
                      else {
                          NombreArchivo = value.direm_NombreArchivo;
                      }//<a download href='/Expedientes/Expediente_1/Amonestaciones/ArchivoCasoPruebaCarlosUmanzor.docx'></a>
                      switch (value.direm_Carpeta) {
                          case "Amonestaciones":
                              Amonestaciones += "   <li class='dd-item' >" +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Amonestaciones/" + value.direm_NombreArchivo + "'></a></span>" +
                                                 "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";
                              break;
                          case "Permisos":
                              Permisos += " <li class='dd-item'  >" +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Permisos/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Incapacidades":
                              Incapacidades += " <li class='dd-item'> " +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Incapacidades/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Constancia":
                              Constancias += " <li class='dd-item'> " +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Constancia/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Solicitud":
                              Solicitudes += " <li class='dd-item'> " +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Solicitud/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Facturas":
                              Facturas += " <li class='dd-item' >" +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Facturas/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Archivos_Personales":
                              Archivos_Personales += " <li class='dd-item' >" +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Archivos_Personales/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;
                          case "Archivos_de_Finalizacion_Laboral":
                              Archivos_de_Finalizacion_Laboral += " <li class='dd-item'> " +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Archivos_de_Finalizacion_Laboral/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break; case "Archivos_de_Finalizacion_Laboral":
                              Otros += " <li class='dd-item'> " +
                                                  "     <div class='dd-handle'>" +
                                                  "         <span class='pull-right'><a class='btn btn-info btn-xs fa fa-info-circle' onclick='ModalDetalles(" + value.direm_Id + ")'></a></span>" +
                                                  "         <span class='pull-right'>" + FechaFormato(value.direm_FechaCrea).substring(0, 10) + "<a class='btn btn-success btn-xs fa fa-download' download href='/Expedientes/Expediente_" + value.emp_Id + "/Otros/" + value.direm_NombreArchivo + "'></a></span>" +
                                                  "         <span class='label label-info'></span>" + NombreArchivo +
                                                  "     </div>" +
                                                  " </li>";

                              break;

                          default:

                      }

                  });


                  $("#ListAmonestacionesExpedienteViejo").html(Amonestaciones).show();
                  $("#ListPermisosExpedienteViejo").html(Permisos).show();
                  $("#ListIncapacidadesExpedienteViejo").html(Incapacidades).show();
                  $("#ListConstanciasExpedienteViejo").html(Constancias).show();
                  $("#ListSolicitudesExpedienteViejo").html(Solicitudes).show();
                  $("#ListFacturasExpedienteViejo").html(Facturas).show();
                  $("#ListArchivos_PersonalesExpedienteViejo").html(Archivos_Personales).show();
                  $("#ListArchivos_de_Finalizacion_LaboralExpedienteViejo").html(Archivos_de_Finalizacion_Laboral).show();
                  $("#ListOtrosExpedienteViejo").html(Otros).show();

                  document.getElementById("ListAmonestacionesExpedienteViejo").style.display = 'none';
                  document.getElementById("ListPermisosExpedienteViejo").style.display = 'none';
                  document.getElementById("ListIncapacidadesExpedienteViejo").style.display = 'none';
                  document.getElementById("ListConstanciasExpedienteViejo").style.display = 'none';
                  document.getElementById("ListSolicitudesExpedienteViejo").style.display = 'none';
                  document.getElementById("ListFacturasExpedienteViejo").style.display = 'none';
                  document.getElementById("ListArchivos_PersonalesExpedienteViejo").style.display = 'none';
                  document.getElementById("ListArchivos_de_Finalizacion_LaboralExpedienteViejo").style.display = 'none';
                  document.getElementById("ListOtrosExpedienteViejo").style.display = 'none';

                  $("#btnMenosAmonestacionesExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasAmonestacionesExpedienteViejo").show();
                  $("#btnMenosPermisosExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasPermisosExpedienteViejo").show();
                  $("#btnMenosIncapacidadesExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasIncapacidadesExpedienteViejo").show();
                  $("#btnMenosConstanciasExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasConstanciasExpedienteViejo").show();

                  $("#btnMenosSolicitudesExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasSolicitudesExpedienteViejo").show();
                  $("#btnMenosFacturasExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasFacturasExpedienteViejo").show();
                  $("#btnMenosArchivos_PersonalesExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasArchivos_PersonalesExpedienteViejo").show();
                  $("#btnMenosArchivos_de_Finalizacion_LaboralExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasArchivos_de_Finalizacion_LaboralExpedienteViejo").show();
                  
                  $("#btnMenosOtrosExpedienteViejo").removeAttr("style").hide();
                  $("#btnMasOtrosExpedienteViejo").show();
              }
          });


}

function OpenTag(TipoArchivoId) {
    $("#btnMas" + TipoArchivoId).removeAttr("style").hide();
    $("#btnMenos" + TipoArchivoId).show();

    document.getElementById("List" + TipoArchivoId).style.display = 'block';
}

function CloseTag(TipoArchivoId) {
    $("#btnMas" + TipoArchivoId).show();
    $("#btnMenos" + TipoArchivoId).removeAttr("style").hide();
    document.getElementById("List" + TipoArchivoId).style.display = 'none';
}


var direm_Id = 0;
function InactivarExpediente(id) {
    direm_Id = id;
    CierraPopups();
    $('#ModalInactivar').modal('show');

}

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.direm_Id = direm_Id;
        data = JSON.stringify({ tbDirectoriosEmpleados: data });
        _ajax(data,
            '/Empleados/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El archivo se eliminó de forma exitosa.");
                    CargarDatos();
                } else {
                    MsgError("Error", "No se eliminó el archivo, contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "No se eliminó el archivo, contacte al administrador.");
    }
});



function ModalDetalles(id) {
    _ajax(null,
        '/Empleados/DetallesExpediente/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#direm_NombreArchivo")["0"].innerText = obj.direm_NombreArchivo;
                $("#ModalDetalles").find("#direm_Carpeta")["0"].innerText = obj.direm_Carpeta;
                $("#ModalDetalles").find("#direm_FechaCrea")["0"].innerText = FechaFormato(obj.direm_FechaCrea);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}