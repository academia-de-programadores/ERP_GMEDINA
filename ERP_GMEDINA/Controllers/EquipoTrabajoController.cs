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
        private ERP_GMEDINAEntities db = null;

        // GET: /EquipoTrabajo/
        public ActionResult Index()        
		{
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbEquipoTrabajo = new tbEquipoTrabajo { eqtra_Estado = true };
            return View(tbEquipoTrabajo);
        }
		[HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                //Aqui codigo llenarTabla
                var tbEquipoTrabajo = db.tbEquipoTrabajo
                    .Select(
                        x =>
                        new
                        {
                            Número = x.eqtra_Id,
                    eqtra_Id = x.eqtra_Id,
					eqtra_Codigo = x.eqtra_Codigo,
					eqtra_Descripcion = x.eqtra_Descripcion,
					eqtra_Observacion = x.eqtra_Observacion,
                            eqtra_Estado = x.eqtra_Estado
                        }
                        )
                        .ToList();
                return Json(tbEquipoTrabajo, JsonRequestBehavior.AllowGet);

                //aqui termina llenarTabla
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
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
                    db = new ERP_GMEDINAEntities();
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
                db = new ERP_GMEDINAEntities();
                tbEquipoTrabajo = db.tbEquipoTrabajo.Find(id);
                if (tbEquipoTrabajo == null )
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
                    db = new ERP_GMEDINAEntities();
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
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbEquipoTrabajo_Inactivar(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoTrabajo_Inactivar_Result item in list)
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

        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbEquipoTrabajo_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoTrabajo_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
            if (disposing && db!=null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
