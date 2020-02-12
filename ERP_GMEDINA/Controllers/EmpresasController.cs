using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System.IO;

namespace ERP_GMEDINA.Controllers
{
    public class EmpresasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        //GET: Empresas
        [SessionManager("Empresas/Index")]
        public ActionResult Index()
        {

            var per_id = new List<object> { new { emp_id = 0, Nombre = "**Seleccione una opción**" } };
            ViewBag.per_id = new SelectList(per_id, "emp_id", "Nombre");
            var tbEmpresas = new tbEmpresas { };
            return View(tbEmpresas);
        }

        [HttpPost]
        [SessionManager("Empresas/Index")]
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
                    empr_Estado = x.empr_Estado,
                    RTN = x.empr_RTN,
                    Nombre = x.tbPersonas.per_Nombres + " " + x.tbPersonas.per_Apellidos
                }
                ).ToList();
                var ddlEmpl = new List<object> { new { per_Id = 0, Nombre = "**Seleccione una opción**" } };
                ddlEmpl.AddRange(db.tbPersonas
                    .Select(p => new
                    {
                        per_Id = p.per_Id,
                        Nombre = p == null ? "no aplica" : p.per_Identidad + " / " +
                                    p.per_Nombres + " " +
                                    p.per_Apellidos
                    })
                    .ToList());
                return Json(new { tbEmpresas = tbEmpresas, ddlEmpl = ddlEmpl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }

         //POST: Empresas/Create
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for
         //more details see http:go.microsoft.com/fwlink/?LinkId=317598. tbEmpresas tbEmpresas,
        [HttpPost]
        [SessionManager("Empresas/Create")]
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
                    var list = db.UDP_RRHH_tbEmpresas_Insert(tbEmpresas.empr_Nombre, ext, tbEmpresas.per_Id, tbEmpresas.empr_RTN == null ? "N/A" : tbEmpresas.empr_RTN, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbEmpresas_Insert_Result item in list)
                    {
                        msj = int.Parse(item.MensajeError);
                    }
                    if (!(Directory.Exists(Server.MapPath("~/") + "/Logos/")))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/") + "/Logos/");
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

        //GET: Empresas/Edit/5
        [SessionManager("Empresas/Datos")]
        [HttpPost]
        [ActionName("Datos")]
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
                if (tbEmpresas == null)
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
            var empresa = new
            {
                empr_Id = tbEmpresas.empr_Id,
                empr_Nombre = tbEmpresas.empr_Nombre,
                per_Id = tbEmpresas.tbPersonas.per_Id,
                per_Nombre = tbEmpresas.tbPersonas.per_Nombres+" "+ tbEmpresas.tbPersonas.per_Apellidos,
                empr_RTN = tbEmpresas.empr_RTN,
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

        //POST: Empresas/Edit/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //more details see http:go.microsoft.com/fwlink/?LinkId=317598.
        [SessionManager("Empresas/Edit")]
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
                    var list = db.UDP_RRHH_tbEmpresas_Update(id, tbEmpresas.empr_Nombre, tbEmpresas.per_Id,
                        tbEmpresas.empr_RTN == null ? "N/A" : tbEmpresas.empr_RTN,
                        ruta, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbEmpresas_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    if (!(Directory.Exists(Server.MapPath("~/") + "/Logos/")))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/") + "/Logos/");
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
        [SessionManager("Empresas/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbEmpresas_Restore(id, (int)Session["UserLogin"], Function.DatetimeNow());
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
        //GET: Empresas/Delete/5
        [HttpPost]
        [SessionManager("Empresas/Delete")]
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
                    var list = db.UDP_RRHH_tbEmpresas_Delete(id, tbEmpresas.empr_RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
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

        //POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            db.tbEmpresas.Remove(tbEmpresas);
            db.SaveChanges();
            return RedirectToAction("Index");
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
