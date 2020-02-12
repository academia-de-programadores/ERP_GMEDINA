using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class TipoPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();


        [SessionManager("TipoPermisos/Index")]
        public ActionResult Index()
        {
            tbTipoPermisos tbTipoPermisos = new tbTipoPermisos { tper_Estado = true };
            return View(tbTipoPermisos);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbTipoPermisos = db.tbTipoPermisos
                    .Select(
                t =>
                new
                {
                    tper_Id = t.tper_Id,
                    tper_Descripcion = t.tper_Descripcion,
                    tper_Estado = t.tper_Estado
                })
                .ToList();
                return Json(tbTipoPermisos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [SessionManager("TipoPermisos/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Restore(id, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoPermisos_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("TipoPermisos/Create")]
        public JsonResult Create(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "";
            if (tbTipoPermisos.tper_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();
                try
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Insert(
                        tbTipoPermisos.tper_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoPermisos_Insert_Result item in list)
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
        
        [SessionManager("TipoPermisos/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoPermisos tbTipoPermisos = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbTipoPermisos = db.tbTipoPermisos.Find(id);
                if (tbTipoPermisos == null )
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
            var competencias = new tbTipoPermisos
            {
                tper_Id = tbTipoPermisos.tper_Id,
                tper_Descripcion = tbTipoPermisos.tper_Descripcion,
                tper_Estado = tbTipoPermisos.tper_Estado,
                tper_RazonInactivo = tbTipoPermisos.tper_RazonInactivo,
                tper_UsuarioCrea = tbTipoPermisos.tper_UsuarioCrea,
                tper_FechaCrea = tbTipoPermisos.tper_FechaCrea,
                tper_UsuarioModifica = tbTipoPermisos.tper_UsuarioModifica,
                tper_FechaModifica = tbTipoPermisos.tper_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoPermisos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoPermisos.tbUsuario1).usu_NombreUsuario }
            };
            return Json(competencias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("TipoPermisos/Edit")]
        public JsonResult Edit(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "";
            if (tbTipoPermisos.tper_Id != 0 && tbTipoPermisos.tper_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();
                var id = (int)Session["id"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Update(id,
                        tbTipoPermisos.tper_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoPermisos_Update_Result item in list)
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
        [HttpPost]
        [SessionManager("TipoPermisos/Delete")]
        public ActionResult Delete(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "...";

            if (tbTipoPermisos.tper_Id != 0 )
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoPermisos_Delete(id, null, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoPermisos_Delete_Result item in list)
                    {
                        msj = item.MensajeError = " ";
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
            return Json(msj, JsonRequestBehavior.AllowGet);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
