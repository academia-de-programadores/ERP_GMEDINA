var ID = 0;
var fill = 0;
var Admin = false;
//Funciones GET
function tablaEditar(id) {
    var validacionPermiso = userModelState("Habilidades/Edit");
    if (validacionPermiso.status == true) {
        var data = { id: id };
        _POST(data,
            '/Habilidades/Datos/',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ID = obj.habi_Id;
                    $("#FormEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
                    $('#ModalEditar').modal('show');
                }
            });
    }
}
function tablaDetalles(id) {
    var validacionPermiso = userModelState("Habilidades/Details");
    if (validacionPermiso.status == true) {
        var data = { id: id };
        _POST(data,
            '/Habilidades/Datos/',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ID = obj.habi_Id;
                    $("#ModalDetalles").find("#habi_Descripcion")["0"].innerText = obj.habi_Descripcion;
                    $("#ModalDetalles").find("#habi_FechaCrea")["0"].innerText = FechaFormato(obj.habi_FechaCrea);
                    $("#ModalDetalles").find("#habi_FechaModifica")["0"].innerText = FechaFormato(obj.habi_FechaModifica);
                    $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                    $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                    //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                    $('#ModalDetalles').modal('show');
                }
            });
    }
}
//fill = -1 para cargar toda la data
//fill = 0 para cargar solo los activos
function llenarTabla() {
 _ajax(null,
     '/Habilidades/llenarTabla',
     'POST',
     function (Lista) {
      if (validarDT(Lista)) {
       return null;
      }
      tabla.clear();
      tabla.draw();
      $.each(Lista, function (index, value) {
       var Acciones = value.habi_Estado == 1
                  ? null : Admin ?
                  "<div>" +
                     "<a class='btn  btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                     "<a class='btn  btn-default btn-xs' onclick='hablilitar(this)' >Activar</a>" +
                 "</div>" : '';
       tabla.row.add({
        Estado: value.habi_Estado ? 'Activo' : 'Inactivo',
        "Número": value.habi_Id,
        ID: value.habi_Id,
        Descripción: value.habi_Descripcion,
        Acciones: Acciones
       });
      });
      tabla.draw();
     });
}
$(document).ready(function () {
 fill = Admin == undefined ? 0 : -1;
 llenarTabla();
});
//Botones GET
$("#btnAgregar").click(function () {
    var validacionPermiso = userModelState("Habilidades/Create");
    if (validacionPermiso.status == true) {
        var modalnuevo = $('#ModalNuevo');
        modalnuevo.modal('show');
        $(modalnuevo).find("#habi_Descripcion").val("");
        $(modalnuevo).find("#habi_Descripcion").focus();
    }
});
$("#btnEditar").click(function () {
 _ajax(null,
     '/Habilidades/Edit/' + ID,
     'GET',
     function (obj) {
      if (obj != "-1" && obj != "-2" && obj != "-3") {
       ID = obj.habi_Id;
       CierraPopups();
       $('#ModalEditar').modal('show');
       $("#ModalEditar").find("#habi_Descripcion").val(obj.habi_Descripcion);
       $("#ModalEditar").find("#habi_Descripcion").focus();
      }
     });
});
$("#btnInactivar").click(function () {
 CierraPopups();
 $('#ModalInactivar').modal('show');
 $("#ModalInactivar").find("#habi_RazonInactivo").val("");
 $("#ModalInactivar").find("#habi_RazonInactivo").focus();
});
//botones POST

$("#btnGuardar").click(function () {
 var data = $("#FormNuevo").serializeArray();
 data = serializar(data);
 if (data != null) {
  data = JSON.stringify({ tbHabilidades: data });
  _ajax(data,
      '/Habilidades/Create',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        CierraPopups();
        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
        LimpiarControles(["habi_Descripcion"]);
        llenarTabla();
       } else {
        MsgError("Error", "No se agregó el registro, contacte al administrador.");
       }
      });
 } else {
  MsgError("Error", "Por favor llene todas las cajas de texto.");
 }
});

$("#InActivar").click(function () {
 var data = $("#FormInactivar").serializeArray();
 data = serializar(data);
 if (data != null) {
  data.habi_Id = ID;
  data = JSON.stringify({ tbHabilidades: data });
  _ajax(data,
      '/Habilidades/Delete',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        CierraPopups();
        MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
        LimpiarControles(["habi_Descripcion", "habi_RazonInactivo"]);
        llenarTabla();
       } else {
           MsgError("Error", "No se inactivó el registro, contacte al administrador.");
       }
      });
 } else {
  MsgError("Error", "Por favor llene todas las cajas de texto.");
 }
});

$("#btnActualizar").click(function () {
 var data = $("#FormEditar").serializeArray();
 data = serializar(data);
 if (data != null) {
  data.habi_Id = ID;
  data = JSON.stringify({ tbHabilidades: data });
  _ajax(data,
      '/Habilidades/Edit',
      'POST',
      function (obj) {
       if (obj != "-1" && obj != "-2" && obj != "-3") {
        CierraPopups();
        MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
        llenarTabla();
       } else {
           MsgError("Error", "No se editó el registro, contacte al administrador.");
       }
      });
 } else {
  MsgError("Error", "Por favor llene todas las cajas de texto.");
 }
});


$.each($(".modal"), function (index, value) {
    $(value).on('hidden.bs.modal', function () {
        limpiarClases(value);
        var INPUT = $(value).find('input');
        $.each(INPUT, function (index, value) {
            $(value).val("");
            $(value).prop("checked", false);
        });
        var SELECT = $(value).find('select');
        $.each(SELECT, function (index, value) {
            $(value).val("");
            $(value).val("0");
        });
    });
});