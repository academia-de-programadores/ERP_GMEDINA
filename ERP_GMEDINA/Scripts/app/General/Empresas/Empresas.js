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
  var formData = new FormData();
  formData.append('file', $('#empr_Logo')[0].files[0]);
  $.ajax({
   url: "/Empresas/Upload",
   type: "post",
   dataType: "html",
   data: formData,
   cache: false,
   contentType: false,
   processData: false
  })
 .done(function (res) {
  if (res == "true") {
      MsgSuccess("Exito","Archivo subido exitosamente");
     } else {
            MsgError("Error", "Cambiar el nombre del archivo");
            var img = document.getElementById('img1');
            img.src = "";
            $("#ModalNuevo").data("res", false);
     }
  $("#ModalNuevo").data("res", res);
 });
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
  var formData = new FormData();
  formData.append('file', $('#UPempr_Logo')[0].files[0]);
  $.ajax({
   url: "/Empresas/Upload",
   type: "post",
   dataType: "html",
   data: formData,
   cache: false,
   contentType: false,
   processData: false
  })
 .done(function (res) {
  if (res) {
   MsgSuccess("Exito", "Archivo subido exitosamente");
  } else {
   MsgError("Error", "El archivo no es valido");
   var img = document.getElementById('img2');
   img.src = "";
   $("#ModalEditar").data("res", false);
  }
  $("#ModalEditar").data("res", res);
 });
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
       console.log(value.empr_Nombre);
       tabla.row.add({
        ID: value.empr_Id,
        Empresa: value.empr_Nombre,
       }).draw();
      });
     });
}
function tablaEditar(ID) {
 id = ID;
 _ajax(null,
     '/Empresas/Edit/' + ID,
     'GET',
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
 id = ID;
 _ajax(null,
     '/Empresas/Edit/' + ID,
     'GET',
     function (obj) {
      if (obj != "-1" && obj != "-2" && obj != "-3") {
       $("#ModalDetalles").find("#empr_Nombre")["0"].innerText = obj.empr_Nombre;
       var lol = $("#ModalDetalles").find("#empr_Logo")["0"].src = obj.empr_Logo;
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
})

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
 var modalNuevo=$("#ModalNuevo").data("res");
    if (modalNuevo=="true") {
        event.preventDefault();
        var data = $("#FormNuevo").serializeArray();
        data = serializar(data);
        _ajax(JSON.stringify({ tbEmpresas: data }),
            '/Empresas/Create/',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    llenarTabla();
                    MsgSuccess("Exito","Archivo subido exitosamente");
                    $("#ModalNuevo").modal('hide');//ocultamos el modal
                    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
                    $('.modal-backdrop').remove();//eliminamos el
                }else {
                 MsgError("Error", "No se pudo editar el registro, contacte al administrador");
                }
            });
    } else {
     MsgError("Error", "Intente con otra imagen ó cambiar el nombre");
    }
});

$("#btnActualizar").click(function () {
 var data = $("#FormEditar").serializeArray();
 var img2 = $("#img2")[0].src;
 //var formEditar = $("#ModalEditar").data("res");
 data = serializar(data);
 if (data != null && img2 != "http://localhost:51144/Empresas") {
  data.empr_Id = id;
  data = JSON.stringify({ tbEmpresas: data });
  _ajax(data,
      '/Empresas/Edit',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        CierraPopups();
        llenarTabla();
        MsgSuccess("¡Exito!", "El registro se editó de forma exitosa");
       } else {
        MsgError("Error", "No se pudo editar el registro, contacte al administrador");
       }
      });
 } else {
  MsgError("Error", "Intente con otra imagen ó cambiar el nombre");
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
        llenarTabla();
        LimpiarControles(["empr_Nombre", "empr_RazonInactivo"]);
        MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
       } else {
        MsgError("Error", "No se logró Inactivar el registro, contacte al administrador");
       }
      });
 } else {
  MsgError("Error", "por favor llene todas las cajas de texto");
 }
});