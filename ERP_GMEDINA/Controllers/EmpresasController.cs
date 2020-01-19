using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.IO;

namespace ERP_GMEDINA.Controllers
{
    public class EmpresasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Empresas
        public ActionResult Index()
        {
            var Admin = (bool)Session["Admin"];
            var tbEmpresas = new tbEmpresas {empr_Estado= Admin };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbEmpresas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbEmpresas = db.tbEmpresas
                .Select(x => new
                {
                    empr_Id = x.empr_Id,
                    empr_Nombre = x.empr_Nombre,
                    empr_Estado = x.empr_Estado
                }
                ).ToList();
                return Json(tbEmpresas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. tbEmpresas tbEmpresas,
        [HttpPost]
        public JsonResult Create(tbEmpresas tbEmpresas, HttpPostedFileBase file)
        {
            int msj = 0;
            if (tbEmpresas.empr_Nombre != "" && file != null)
            {
                string ext = file.FileName.Split('.')[1].ToLower();
                if (ext != "png" && ext != "jpeg" && ext != "jpg")
                {
                    return Json(-4, JsonRequestBehavior.AllowGet);
                }
                string path = file.FileName;
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbEmpresas_Insert(tbEmpresas.empr_Nombre, ext, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Insert_Result item in list)
                    {
                        msj = int.Parse(item.MensajeError);
                    }
                    string ruta = Server.MapPath("~/") + "/Logos/" + msj.ToString() + "." + ext;
                    file.SaveAs(ruta);
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    msj = -2;
                    ex.Message.ToString();
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                msj = -3;
            }
            return Json(msj, JsonRequestBehavior.AllowGet);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbEmpresas tbEmpresas = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbEmpresas = db.tbEmpresas.Find(id);
                if (tbEmpresas == null || !tbEmpresas.empr_Estado)
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
            var empresa = new tbEmpresas
            {
                empr_Id = tbEmpresas.empr_Id,
                empr_Nombre = tbEmpresas.empr_Nombre,
                empr_Logo = tbEmpresas.empr_Logo,
                empr_Estado = tbEmpresas.empr_Estado,
                empr_RazonInactivo = tbEmpresas.empr_RazonInactivo,
                empr_UsuarioCrea = tbEmpresas.empr_UsuarioCrea,
                empr_FechaCrea = tbEmpresas.empr_FechaCrea,
                empr_UsuarioModifica = tbEmpresas.empr_UsuarioModifica,
                empr_FechaModifica = tbEmpresas.empr_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbEmpresas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbEmpresas.tbUsuario1).usu_NombreUsuario }
            };
            Session["Path"] = tbEmpresas.empr_Logo;
            return Json(empresa, JsonRequestBehavior.AllowGet);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(tbEmpresas tbEmpresas, HttpPostedFileBase file)
        {
            string msj = "";
            if (tbEmpresas.empr_Nombre != "")
            {
                string path = file == null ? (string)Session["Path"] : file.FileName;
                string ext = path.Split('.')[1].ToLower();
                if (ext != "png" && ext != "jpeg" && ext != "jpg")
                {
                    return Json("-4", JsonRequestBehavior.AllowGet);
                }
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    string ruta = "/Logos/" + id + "." + ext;
                    var list = db.UDP_RRHH_tbEmpresas_Update(id, tbEmpresas.empr_Nombre, ruta, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    ruta = Server.MapPath("~/") + "/Logos/" + id + "." + ext;
                    file.SaveAs(ruta);
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
            return Json(msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbEmpresas_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Restore_Result item in list)
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
        // GET: Empresas/Delete/5
        public ActionResult Delete(tbEmpresas tbEmpresas)
        {
            db = new ERP_GMEDINAEntities();
            string msj = "...";
            if (tbEmpresas.empr_Id != 0 && tbEmpresas.empr_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEmpresas_Delete(id, tbEmpresas.empr_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Delete_Result item in list)
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

        //// POST: Empresas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
        //    db.tbEmpresas.Remove(tbEmpresas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
