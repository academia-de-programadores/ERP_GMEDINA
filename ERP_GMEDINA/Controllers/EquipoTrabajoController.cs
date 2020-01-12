using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{				
	public class EquipoTrabajoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /EquipoTrabajo/
        public ActionResult Index()        
		{           
		    List<tbEquipoTrabajo> tbEquipoTrabajo = new List<Models.tbEquipoTrabajo> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbEquipoTrabajo);
        }
		[HttpPost]
        public JsonResult llenarTabla()
        {
			List<tbEquipoTrabajo> tbEquipoTrabajo = new List<Models.tbEquipoTrabajo> { };
            var lista = db.tbEquipoTrabajo.Where(x => x.eqtr_Estado).ToList();
            foreach (tbEquipoTrabajo x in db.tbEquipoTrabajo.ToList().Where(x=>x.eqtra_Estado))
            {
                tbEquipoTrabajo.Add( new tbEquipoTrabajo
                {
					eqtra_Id = x.eqtra_Id,
					eqtra_Codigo = x.eqtra_Codigo,
					eqtra_Descripcion = x.eqtra_Descripcion,
					eqtra_Observacion = x.eqtra_Observacion
				});
            }
            return Json(tbEquipoTrabajo, JsonRequestBehavior.AllowGet);
        }
        // POST: /EquipoTrabajo/Create
        [HttpPost]
        public JsonResult Create(tbEquipoTrabajo tbEquipoTrabajo)
        {
            string msj = "";
            if (tbEquipoTrabajo.eqtra_Codigo != "" && tbEquipoTrabajo.eqtra_Descripcion != "" && tbEquipoTrabajo.eqtra_Observacion != "")
            { 
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEquipoTrabajo_Insert(tbEquipoTrabajo.eqtra_Codigo, tbEquipoTrabajo.eqtra_Descripcion, tbEquipoTrabajo.eqtra_Observacion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoTrabajo_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
		// GET: /EquipoTrabajo//Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbEquipoTrabajo tbEquipoTrabajo = null;
            try
            {
                tbEquipoTrabajo = db.tbEquipoTrabajo.Find(id);
                if (tbEquipoTrabajo == null || !tbEquipoTrabajo.eqtra_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }            
            Session["id"] = id;
            var tabla = new tbEquipoTrabajo
            {
				eqtra_Id = tbEquipoTrabajo.eqtra_Id,
				eqtra_Codigo = tbEquipoTrabajo.eqtra_Codigo,
				eqtra_Descripcion = tbEquipoTrabajo.eqtra_Descripcion,
				eqtra_Observacion = tbEquipoTrabajo.eqtra_Observacion,
				eqtra_Estado = tbEquipoTrabajo.eqtra_Estado,
				eqtra_RazonInactivo = tbEquipoTrabajo.eqtra_RazonInactivo,
				eqtra_UsuarioCrea = tbEquipoTrabajo.eqtra_UsuarioCrea,
				eqtra_FechaCrea = tbEquipoTrabajo.eqtra_FechaCrea,
				eqtra_UsuarioModifica = tbEquipoTrabajo.eqtra_UsuarioModifica,
				eqtra_FechaModifica = tbEquipoTrabajo.eqtra_FechaModifica,
				tbUsuario = new tbUsuario { usu_NombreUsuario= IsNull(tbEquipoTrabajo.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbEquipoTrabajo.tbUsuario1).usu_NombreUsuario }
            };
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
        // POST: /EquipoTrabajo/Edit/5
        [HttpPost]
        public JsonResult Edit(tbEquipoTrabajo tbEquipoTrabajo)
        {
            string msj = "";
            if (tbEquipoTrabajo.eqtra_Id != 0 && tbEquipoTrabajo.eqtra_Codigo != "" && tbEquipoTrabajo.eqtra_Descripcion != "" && tbEquipoTrabajo.eqtra_Observacion != "")            
			{
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEquipoTrabajo_Update(id, tbEquipoTrabajo.eqtra_Codigo, tbEquipoTrabajo.eqtra_Descripcion, tbEquipoTrabajo.eqtra_Observacion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoTrabajo_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }            
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
		// GET: /EquipoTrabajo//Delete/5
        [HttpPost]
        public ActionResult Delete(tbEquipoTrabajo tbEquipoTrabajo)
        {
            string msj = "";
            if (tbEquipoTrabajo.eqtra_Id != 0 && tbEquipoTrabajo.eqtra_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEquipoTrabajo_Delete(id, tbEquipoTrabajo.eqtra_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoTrabajo_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }            
            return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        }
        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor!=null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario {usu_NombreUsuario="" };
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var id = 0;
//Funciones GET
function tablaEditar(btn) {
    var tr=$(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    _ajax(null,
        '/EquipoTrabajo/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
				$("#FormEditar").find("#eqtra_Codigo").val(obj.eqtra_Codigo);
								$("#FormEditar").find("#eqtra_Descripcion").val(obj.eqtra_Descripcion);
								$("#FormEditar").find("#eqtra_Observacion").val(obj.eqtra_Observacion);
				$('#ModalEditar').modal('show');
            }
        });
}
function tablaDetalles(btn) {
    var tr = $(btn).closest("tr");
    var row = tabla.row(tr);
    id = row.data().id;
    _ajax(null,
        '/EquipoTrabajo/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
				$("#ModalDetalles").find("#eqtra_Codigo")["0"].innerText = obj.eqtra_Codigo;
				$("#ModalDetalles").find("#eqtra_Descripcion")["0"].innerText = obj.eqtra_Descripcion;
				$("#ModalDetalles").find("#eqtra_Observacion")["0"].innerText = obj.eqtra_Observacion;
				$("#ModalDetalles").find("#eqtra_Estado")["0"].innerText = obj.eqtra_Estado;
                $("#ModalDetalles").find("#eqtra_RazonInactivo")["0"].innerText = obj.eqtra_RazonInactivo;
                $("#ModalDetalles").find("#eqtra_FechaCrea")["0"].innerText = FechaFormato(obj.eqtra_FechaCrea);
                $("#ModalDetalles").find("#eqtra_FechaModifica")["0"].innerText = FechaFormato(obj.eqtra_FechaModifica);
                $("#ModalDetalles").find("#tbUsuario_usu_NombreUsuario")["0"].innerText = obj.tbUsuario.usu_NombreUsuario;
                $("#ModalDetalles").find("#tbUsuario1_usu_NombreUsuario")["0"].innerText = obj.tbUsuario1.usu_NombreUsuario;
                $("#ModalDetalles").find("#btnEditar")["0"].dataset.id = id;
                $('#ModalDetalles').modal('show');
            }
        });
}
function llenarTabla() {
    _ajax(null,
        '/EquipoTrabajo/llenarTabla',
        'POST',
        function (Lista) {
            tabla.clear();
            tabla.draw();
            $.each(Lista, function (index, value) {
                tabla.row.add({
					id: value.eqtra_Id,
					_Codigo: value.eqtra_Codigo,				
					_Descripcion: value.eqtra_Descripcion,				
					_Observacion: value.eqtra_Observacion				
                });
            });
            tabla.draw();
        });
}
$(document).ready(function () {
    llenarTabla();
});
//Botones GET
$("#btnAgregar").click(function () {
    var modalnuevo = $('#ModalNuevo');
    modalnuevo.modal('show');
	$(modalnuevo).find("#eqtra_Codigo").val("");
	$(modalnuevo).find("#eqtra_Codigo").focus();
	$(modalnuevo).find("#eqtra_Descripcion").val("");
	$(modalnuevo).find("#eqtra_Observacion").val("");
});
$("#btnEditar").click(function () {
    _ajax(null,
        '/EquipoTrabajo/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                CierraPopups();
                $('#ModalEditar').modal('show');
				$("#ModalEditar").find("#eqtra_Codigo").val(obj.eqtra_Codigo);
				$("#ModalEditar").find("#eqtra_Codigo").focus();
				$("#ModalEditar").find("#eqtra_Descripcion").val(obj.eqtra_Descripcion);
				$("#ModalEditar").find("#eqtra_Observacion").val(obj.eqtra_Observacion);
            }
        });
});
$("#btnInhabilitar").click(function () {
    CierraPopups();
    $('#ModalInhabilitar').modal('show');
    $("#ModalInhabilitar").find("#eqtr_RazonInactivo").val("");
    $("#ModalInhabilitar").find("#eqtr_RazonInactivo").focus();
});
//botones POST
$("#btnGuardar").click(function () {
    var data = $("#FormNuevo").serializeArray();
    data = serializar(data);
    if (data!=null) {
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Create',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["eqtra_Codigo", "eqtra_Descripcion", "eqtra_Observacion", "eqtr_RazonInactivo"]);
                    MsgSuccess("¡Exito!", "Se ah agregado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error","por favor llene todas las cajas de texto");
    }    
});
$("#InActivar").click(function () {
    var data = $("#FormInactivar").serializeArray();
    data = serializar(data);
    if (data != null) {
        data.eqtr_Id = id;
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Delete',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    LimpiarControles(["eqtra_Codigo", "eqtra_Descripcion", "eqtra_Observacion", "eqtr_RazonInactivo"]);
                    MsgWarning("¡Exito!", "Se ah Inactivado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }
});
$("#btnActualizar").click(function () {
    var data = $("#FormEditar").serializeArray();
    data = serializar(data);
    if (data!=null) {
        data.eqtr_Id = id;
        data = JSON.stringify({ tbEquipoTrabajo: data });
        _ajax(data,
            '/EquipoTrabajo/Edit',
            'POST',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    CierraPopups();
                    llenarTabla();
                    MsgSuccess("¡Exito!", "Se ah actualizado el registro");
                } else {
                    MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                }
            });
    } else {
        MsgError("Error", "por favor llene todas las cajas de texto");
    }    
});
