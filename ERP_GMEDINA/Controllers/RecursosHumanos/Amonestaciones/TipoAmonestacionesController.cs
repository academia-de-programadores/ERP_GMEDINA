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
    public class TipoAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: TipoAmonestaciones
        [SessionManager("TipoAmonestaciones/Index")]
        public ActionResult Index()
        {
            tbTipoAmonestaciones tbTipoAmonestaciones = new tbTipoAmonestaciones { tamo_Estado = true };
            bool Admin = (bool)Session["Admin"];
            return View(tbTipoAmonestaciones);
        }

        [HttpPost]
        [SessionManager("TipoAmonestaciones/Index")]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbTipoAmonestacion = db.tbTipoAmonestaciones
                       .Select(
                       t => new
                       {
                           tamo_Id = t.tamo_Id,
                           tamo_Descripcion = t.tamo_Descripcion,
                           tamo_Estado = t.tamo_Estado
                       }
                       )
                       .ToList();
                return Json(tbTipoAmonestacion, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: TipoAmonestaciones/Create
        [HttpPost]
        [SessionManager("TipoAmonestaciones/Create")]
        public JsonResult Create(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";
            tbTipoAmonestaciones tbTipoAmonestacion = new tbTipoAmonestaciones();
            if (tbTipoAmonestaciones.tamo_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Insert(tbTipoAmonestaciones.tamo_Descripcion, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Insert_Result item in list)
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

        // GET: TipoAmonestaciones/Edit/5
        [SessionManager("TipoAmonestaciones/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoAmonestaciones tbTipoAmonestaciones = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
                if (tbTipoAmonestaciones == null)
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
            var tipoAmonestaciones = new tbTipoAmonestaciones
            {
                tamo_Id = tbTipoAmonestaciones.tamo_Id,
                tamo_Descripcion = tbTipoAmonestaciones.tamo_Descripcion,
                tamo_Estado = tbTipoAmonestaciones.tamo_Estado,
                tamo_RazonInactivo = tbTipoAmonestaciones.tamo_RazonInactivo,
                tamo_UsuarioCrea = tbTipoAmonestaciones.tamo_UsuarioCrea,
                tamo_FechaCrea = tbTipoAmonestaciones.tamo_FechaCrea,
                tamo_UsuarioModifica = tbTipoAmonestaciones.tamo_UsuarioModifica,
                tamo_FechaModifica = tbTipoAmonestaciones.tamo_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoAmonestaciones.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoAmonestaciones.tbUsuario1).usu_NombreUsuario }
            };
            return Json(tipoAmonestaciones, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoAmonestaciones/Edit/5
        [HttpPost]
        [SessionManager("TipoAmonestaciones/Edit")]
        public JsonResult Edit(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";
            tbTipoAmonestaciones tbTipoAmonestacion = new tbTipoAmonestaciones();
            if (tbTipoAmonestacion.tamo_Id != 0 && tbTipoAmonestacion.tamo_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Update(id, tbTipoAmonestacion.tamo_Descripcion, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Update_Result item in list)
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
        [SessionManager("TipoAmonestacion/habilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Restore(id, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Restore_Result item in list)
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

        // POST: TipoAmonestaciones/Delete/5
        [HttpPost]
        [SessionManager("TipoAmonestaciones/Delete")]
        public ActionResult Delete(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";


            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbTipoAmonestaciones.tamo_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Delete(id, RazonInactivo, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Delete_Result item in list)
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