var ID = 0;
var fill = 0;
var Admin = false;
//Funciones GET
function tablaEditar(ID) {
    id = ID;
    sessionStorage.setItem("idSucursal", id);
    window.location.href = "/Sucursales/Edit";
}
$(document).ready(function () {
    fill = Admin == undefined ? 0 : -1;
    llenarTabla();
});

function ModalInactivar(id) {
    CierraPopups();
    $("#ModalInactivar").find("#suc_Id").val(id);
    $("#ModalInactivar").find("#suc_RazonInactivo").val("");
    $("#ModalInactivar").find("#scu_RazonInactivo").focus();
    $('#ModalInactivar').modal('show');
};

$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data = JSON.stringify({ tbSucursales: data });
        _ajax(data,
            '/Sucursales/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["suc_Id", "suc_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "El registro se inhabilitado  de forma exitosa");
                } else {
                    MsgError("Error", "No se logró Inactivar el registro, contacte al administrador");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
function tablaDetalles(id) {
    //var tr = $(btn).closest("tr");
    //var row = tabla.row(tr);
    //id = row.data().id;

    _ajax(null,
        '/Sucursales/Detalles/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                ID = obj.habi_Id;
                $("#ModalDetalles").find("#mun_Codigo")["0"].innerText = obj[0].mun_Codigo;
                $("#ModalDetalles").find("#bod_Id")["0"].innerText = obj[0].bod_Id;
                $("#ModalDetalles").find("#pemi_Id")["0"].innerText = obj[0].pemi_Id;
                $("#ModalDetalles").find("#tbEmpresas")["0"].innerText = obj[0].empr_Nombre;//empresa
                $("#ModalDetalles").find("#suc_Descripcion")["0"].innerText = obj[0].suc_Descripcion;
                $("#ModalDetalles").find("#suc_Correo")["0"].innerText = obj[0].suc_Correo;
                $("#ModalDetalles").find("#suc_Direccion")["0"].innerText = obj[0].suc_Direccion;
                $("#ModalDetalles").find("#suc_Telefono")["0"].innerText = obj[0].suc_Telefono;
                $("#ModalDetalles").find("#suc_FechaCrea")["0"].innerText = FechaFormato(obj[0].suc_FechaCrea);
                $("#ModalDetalles").find("#suc_FechaModifica")["0"].innerText = FechaFormato(obj[0].suc_FechaModifica);
                $("#ModalDetalles").find("#suc_UsuarioCrea")["0"].innerText = obj[0].suc_UsuarioCrea;
                $("#ModalDetalles").find("#suc_UsuarioModifica")["0"].innerText = obj[0].suc_UsuarioModifica;
                //$("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}
//fill = -1 para cargar toda la data
//fill = 0 para cargar solo los activos
function llenarTabla() {
    _ajax(null,
        '/Sucursales/llenarTabla',
        'POST',
        function (Lista) {
            if (validarDT(Lista)) {
                return null;
            }
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                var Acciones = value.suc_Estado == 1
                ? "<a class='btn btn-primary btn-xs ' onclick='tablaDetalles(" + value.suc_Id + ")'>Detalles</a> <a class='btn btn-default btn-xs ' onclick='tablaEditar(" + value.suc_Id + ")'>Editar</a> <a class='btn btn-danger btn-xs ' onclick='ModalInactivar(" + value.suc_Id + ")'>Inactivar</a>"
                : Admin ?
                    "<div>" +
                        "<a class='btn btn-primary btn-xs ' onclick='hablilitar(this)' >Activar</a>" +
                    "</div>" : '';
                tabla.row.add({
                    Estado: value.suc_Estado ? 'Activo' : 'Inactivo',
                    "Número": value.suc_Id,
                    ID: value.suc_Id,
                    Descripcion: value.suc_Descripcion,
                    Direccion: value.suc_Direccion,
                    Telefono: value.suc_Telefono,
                    Acciones: Acciones
                });
            });
            tabla.draw();
        });
}

