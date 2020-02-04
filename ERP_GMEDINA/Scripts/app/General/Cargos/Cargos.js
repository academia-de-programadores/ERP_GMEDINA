$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

var fill = 0;
var id = 0;

//Funciones GET
function tablaEditar(ID) {
    id = ID;
    _ajax(null,
        '/Cargos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#FormEditar").find("#car_Descripcion").val(obj.car_Descripcion);
                $("#ModalEditar").find("#car_SalarioMinimo").val(obj.car_SalarioMinimo);
                $("#ModalEditar").find("#car_SalarioMaximo").val(obj.car_SalarioMaximo);
                $('#ModalEditar').modal('show');
            }
        });
}

function tablaDetalles(ID) {
    debugger
    id = ID;
    _ajax(null,
        '/Cargos/Edit/' + ID,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#ModalDetalles").find("#car_Descripcion")["0"].innerText = obj.car_Descripcion;
                $("#ModalDetalles").find("#car_SalarioMinimo")["0"].innerText = obj.car_SalarioMinimo;
                $("#ModalDetalles").find("#car_SalarioMaximo")["0"].innerText = obj.car_SalarioMaximo;
                $("#ModalDetalles").find("#car_FechaCrea")["0"].innerText = FechaFormato(obj.car_FechaCrea);
                $("#ModalDetalles").find("#car_FechaModifica")["0"].innerText = FechaFormato(obj.car_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = ID;
                $('#ModalDetalles').modal('show');
            }
        });
}

function llenarTabla() {
    _ajax(null,
        '/Cargos/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear().draw();
            if (validarDT(Lista)) {
                return null;
            }
            $.each(Lista, function (index, value) {
               // console.log(value.car_Descripcion);
                var Acciones = value.car_Estado == 1
                   ? null :
"<div>" +
                       "<a class='btn btn-primary btn-xs' onclick='CallDetalles(this)' >Detalles</a>" +
                       "<a class='btn btn-default btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                   "</div>";
                if (value.car_Estado > fill) {
                    tabla.row.add({
                        ID: value.car_Id,
                        Número: value.car_Id,
                        Cargo: value.car_Descripcion,
                        Estado:value.car_Estado ?"Activo":"Inactivo",
                        Acciones:Acciones
                    }).draw();
                }
            });
        });
}

$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
    $(modalnuevo).find("#car_Descripcion").val("");
    $(modalnuevo).find("#car_Descripcion").focus();
});

$("#btnEditar").click(function () {
    _ajax(null,
        '/Cargos/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
                $("#ModalEditar").find("#car_Descripcion").val(obj.car_Descripcion);
           
               
                $("#ModalEditar").find("#car_Descripcion").focus();
               
            }
        });
});

$("#btnInactivar").click(function () {
    CierraPopups();
    $('#ModalInactivar').modal('show');
    $("#ModalInactivar").find("#car_RazonInactivo").val("");
    $("#ModalInactivar").find("#car_RazonInactivo").focus();
});

//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");
                    LimpiarControles(["car_Descripcion", "car_RazonInactivo"]);
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
        data.car_Id = id;
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    MsgSuccess("¡Éxito!", "El registro se inactivó de forma exitosa.");
                    LimpiarControles(["car_Descripcion", "car_RazonInactivo"]);
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
    debugger
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.car_Id = id;
        data = JSON.stringify({ tbCargos: data });
        _ajax(data,
            '/Cargos/Edit',
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