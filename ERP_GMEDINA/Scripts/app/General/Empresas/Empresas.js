var Admin = false;
$(document).ready(function () {
 llenarTabla();
});
function init() {
 var inputFile = document.getElementById('empr_Logo');
 inputFile.addEventListener('change', mostrarImagen, false);

 var inputFile = document.getElementById('UPempr_Logo');
 inputFile.addEventListener('change', mostrarImagen, false);
}
function mostrarImagen(event) {
 var file = event.target.files[0];
 var reader = new FileReader();
 reader.onload = function (event) {
  var img = document.getElementById('img1');
  img.src = event.target.result;
  var img = document.getElementById('img2');
  img.src = event.target.result;
 }
 reader.readAsDataURL(file);
}
window.addEventListener('load', init, false);
$("#empr_Logo").change(function () {
 var fileExtension = ['png', 'jpeg', 'jpg'];
 if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
  var img = document.getElementById('img1');
  img.src = "";
  MsgError("¡Error!", "Debe Agregar el logo en el formato correspondiente");
  $("#ModalNuevo").data("res", false);
 } else {
  $("#ModalNuevo").data("res", true);
 }
});
$("#UPempr_Logo").change(function () {
 var fileExtension = ['png', 'jpeg', 'jpg'];
 if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
  var img = document.getElementById('img2');
  img.src = "";
  MsgError("¡Error!", "Debe Agregar el logo en el formato correspondiente");
  $("#ModalEditar").data("res", false);
 } else {
  $("#ModalEditar").data("res", true);
 }
});
function llenarTabla() {
 _ajax(null,
     '/Empresas/llenarTabla',
     'POST',
     function (Lista) {
      var tabla = $("#IndexTable").DataTable();
      tabla.clear();
      tabla.draw();
      $.each(Lista, function (index, value) {
       var Acciones = value.empr_Estado == 1
                   ? null : Admin ?
                    "<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>" : '';
       tabla.row.add({
        ID: value.empr_Id,
        "Número": value.empr_Id,
        Empresa: value.empr_Nombre,
        Estado: value.empr_Estado ? 'Activo' : 'Inactivo',
        Acciones: Acciones
       }).draw();
      });
     });
}
function tablaEditar(ID) {
    id = ID;
 _ajax(JSON.stringify({ id: ID }),
     '/Empresas/Datos/',
     'POST',
     function (obj) {
      if (obj != "-1" && obj != "-2" && obj != "-3") {
       $("#FormEditar").find("#empr_Nombre").val(obj.empr_Nombre);
       //$('#UPempr_Logo').val(obj.empr_Logo);
       $("#ModalEditar").find("#img2")[0].src = obj.empr_Logo;
       $('#ModalEditar').modal('show');
      }
     });
}
function tablaDetalles(ID) {
 _ajax(JSON.stringify({ id: ID }),
     '/Empresas/Datos/',
     'POST',
     function (obj) {
      if (obj != "-1" && obj != "-2" && obj != "-3") {
       $("#ModalDetalles").find("#empr_Nombre")["0"].innerText = obj.empr_Nombre;
       var lol = $("#ModalDetalles").find("#empr_Logo")["0"].src = obj.empr_Logo;
       $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
       var lol = $("#ModalDetalles").find("#empr_FechaCrea")["0"].innerText = FechaFormato(obj.empr_FechaCrea);
       $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
       var lol = $("#ModalDetalles").find("#empr_FechaModifica")["0"].innerText = FechaFormato(obj.empr_FechaModifica);
       //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
       $('#ModalDetalles').modal('show');
      }
     });
}
//Botones GET
$("#btnAgregar").click(function () {
 var modalnuevo = $('#ModalNuevo');
 modalnuevo.modal('show');
 $(modalnuevo).find("#empr_Nombre").val("");
 $(modalnuevo).find("#empr_Nombre").focus();
 $(modalnuevo).find("#empr_Logo").val(null);
 $("#ModalNuevo").find("#img1")[0].src = '';
});
$("#btnEditar").click(function () {
 _ajax(null,
     '/Empresas/Edit/' + id,
     'GET',
     function (obj) {
      if (obj != "-1" && obj != "-2" && obj != "-3") {
       CierraPopups();
       $('#ModalEditar').modal('show');
       var img = $("#ModalEditar").find("#img2");
       $("#ModalEditar").find("#empr_Nombre").focus();
      }
     });
});
$("#btnInactivar").click(function () {
 CierraPopups();
 $('#ModalInactivar').modal('show');
 $("#ModalInactivar").find("#empr_RazonInactivo").val("");
 $("#ModalInactivar").find("#empr_RazonInactivo").focus();
});
$("#FormNuevo").on("submit", function (event) {
 var data = $("#FormNuevo").serializeArray();
 data = serializar(data);

 var modalNuevo = $("#img1")[0].src;
 if (modalNuevo != "http://localhost:51144/Empresas") {
  event.preventDefault();
  if (data != null) {
   var data = new FormData($("#FormNuevo")[0]);
   data.append('file', $('#empr_Logo')[0].files[0]);
   $.ajax({
    url: '/Empresas/Create/',
    type: "post",
    dataType: "html",
    data: data,
    cache: false,
    contentType: false,
    processData: false
   })
    .done(function (obj) {
     if (obj == "-4") {
      MsgError("Error", "formato incorrecto, use archivos con extension .jpg, .png y .jpeg");
     } else if (obj != "-1" && obj != "-2" && obj != "-3") {
      llenarTabla();
      MsgSuccess("¡Exito!", "El registro se ha agregado de forma exitosa");
      $("#ModalNuevo").modal('hide');//ocultamos el modal
      $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
      $('.modal-backdrop').remove();//eliminamos el
     } else {
      MsgError("Error", "No se pudo agregar el registro, contacte al administrador");
     }
    });
  }
 } else {
  MsgError("Error", "por favor, seleccione una imagen");
 }
});
$("#btnActualizar").click(function () {
 var img = $("#img2")[0].innerText;
 if (ModalEditar != '') {
  event.preventDefault();
  var data = new FormData($("#FormEditar")[0]);
  data.append('file', $('#UPempr_Logo')[0].files[0]);
  $.ajax({
   url: '/Empresas/Edit/',
   type: "post",
   dataType: "html",
   data: data,
   cache: false,
   contentType: false,
   processData: false
  })
   .done(function (obj) {
    if (obj == "-4") {
     MsgError("Error", "formato incorrecto, use archivos con extension .jpg, .png y .jpeg");
    } else if (obj != "-1" && obj != "-2" && obj != "-3") {
     MsgSuccess("¡Exito!", "El registro se ha editado de forma exitosa");
     llenarTabla();
     $("#ModalEditar").modal('hide');//ocultamos el modal
     $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
     $('.modal-backdrop').remove();//eliminamos el
    } else {
     MsgError("Error", "No se pudo editar el registro, contacte al administrador");
    }
   });
 }
});
$("#InActivar").click(function () {
 var data = $("#FormInactivar").serializeArray();
 data = serializar(data);
 if (data != null) {
  data.empr_Id = id;
  data = JSON.stringify({ tbEmpresas: data });
  _ajax(data,
      '/Empresas/Delete',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        CierraPopups();
        MsgSuccess("¡Exito!", "El registro se ha inactivado de forma exitosa");
        LimpiarControles(["empr_Nombre", "empr_RazonInactivo"]);
        llenarTabla();
       } else {
        MsgError("Error", "No se logró inactivar el registro, contacte al administrador");
       }
      });
 } else {
  MsgError("Error", "por favor llene todas las cajas de texto");
 }
});
