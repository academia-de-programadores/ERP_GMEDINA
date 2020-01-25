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
    public class RequerimientosEspecialesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        public ActionResult Index()
        {
            tbRequerimientosEspeciales tbRequerimientosEspeciales = new tbRequerimientosEspeciales { resp_Estado=true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                //tbRequerimientosEspeciales = db.tbRequerimientosEspeciales.Where(x => x.resp_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                //tbFasesReclutamiento.Add(new tbFasesReclutamiento { fare_Id = 0, habi_Descripcion = "fallo la conexion" });
                return View(tbRequerimientosEspeciales);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //tbRequerimientosEspeciales.Add(new tbRequerimientosEspeciales { resp_Id = 0, resp_Descripcion = "fallo la conexion" });
            }
            return View(tbRequerimientosEspeciales);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                var tbRequerimientosEspeciales = db.tbRequerimientosEspeciales
                    .Select(
                       x => new {
                           resp_Id = x.resp_Id,
                           resp_Descripcion = x.resp_Descripcion,
                           resp_Estado = x.resp_Estado
                       }
                    )
                    .ToList();
                return Json(tbRequerimientosEspeciales, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        
    }


        // POST: FasesReclutamiento/Create
        [HttpPost]
        public JsonResult Create(tbRequerimientosEspeciales tbRequerimientosEspeciales)
        {
            string msj = "";
            if (tbRequerimientosEspeciales.resp_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequerimientosEspeciales_Insert(tbRequerimientosEspeciales.resp_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequerimientosEspeciales_Insert_Result item in list)
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

        // GET: Habilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbRequerimientosEspeciales tbRequerimientosEspeciales = null;
            try
            {
                tbRequerimientosEspeciales = db.tbRequerimientosEspeciales.Find(id);
                if (tbRequerimientosEspeciales == null )
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
            var RequerimientosEspeciales = new tbRequerimientosEspeciales
            {
                resp_Id = tbRequerimientosEspeciales.resp_Id,
                resp_Descripcion = tbRequerimientosEspeciales.resp_Descripcion,
                resp_Estado = tbRequerimientosEspeciales.resp_Estado,
                resp_RazonInactivo = tbRequerimientosEspeciales.resp_RazonInactivo,
                resp_UsuarioCrea = tbRequerimientosEspeciales.resp_UsuarioCrea,
                resp_FechaCrea = tbRequerimientosEspeciales.resp_FechaCrea,
                resp_UsuarioModifica = tbRequerimientosEspeciales.resp_UsuarioModifica,
                resp_FechaModifica = tbRequerimientosEspeciales.resp_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbRequerimientosEspeciales.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbRequerimientosEspeciales.tbUsuario1).usu_NombreUsuario }
            };
            return Json(RequerimientosEspeciales, JsonRequestBehavior.AllowGet);
        }

        // POST: Habilidades/Edit/5
        [HttpPost]
        public JsonResult Edit(tbRequerimientosEspeciales tbRequerimientosEspeciales)
        {
            string msj = "";
            if (tbRequerimientosEspeciales.resp_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequerimientosEspeciales_Update(id, tbRequerimientosEspeciales.resp_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequerimientosEspeciales_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: Habilidades/Delete/5
        [HttpPost]
        public ActionResult Delete(tbRequerimientosEspeciales tbRequerimientosEspeciales)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequerimientosEspeciales_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequerimientosEspeciales_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }

        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbRequerimientosEspeciales_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequerimientosEspeciales_Restore_Result item in list)
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

