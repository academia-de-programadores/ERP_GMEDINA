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
    public class EmpresasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Empresas
        public ActionResult Index()
        {
            List<tbEmpresas> tbEmpresas = new List<Models.tbEmpresas> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbEmpresas = db.tbEmpresas.Where(x => x.empr_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbEmpresas);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbEmpresas.Add(new tbEmpresas { empr_Id = 0, empr_Nombre = "Fallo la conexión" });

            }
            return View(tbEmpresas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbEmpresas> tbEmpresas =
                new List<tbEmpresas> { };
            foreach (tbEmpresas x in db.tbEmpresas.ToList().Where(x => x.empr_Estado == true))
            {
                tbEmpresas.Add(new tbEmpresas
                {
                    empr_Id = x.empr_Id,
                    empr_Nombre = x.empr_Nombre
                });
            }
            return Json(tbEmpresas, JsonRequestBehavior.AllowGet);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.empr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            string extencion = file.FileName.Split('.')[1].ToLower();
            if (file != null && (extencion == "png" || extencion == "jpg" || extencion == "jpeg"))
            {
                string path = "/Logos/" + file.FileName;
                if (!System.IO.File.Exists(Server.MapPath("~/") + path))
                {//OPEN IF
                    Session["file"] = file;
                    Session["Path"] = path;
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. tbEmpresas tbEmpresas,
        [HttpPost]
        public JsonResult Create(tbEmpresas tbEmpresas)
        {
            HttpPostedFileBase file = (HttpPostedFileBase)Session["file"];
            string path = (string)Session["Path"];
            string msj = "...";
            if (tbEmpresas.empr_Nombre != "" && file != null && path != null)
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEmpresas_Insert(tbEmpresas.empr_Nombre, path, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
                    }
                    file.SaveAs(Server.MapPath("~/") + path);
                    Session["file"] = null;
                    Session["Path"] = null;
                    Session.Remove("file");
                    Session.Remove("Path");
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
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
        public JsonResult Edit(tbEmpresas tbEmpresas)
        {
            HttpPostedFileBase file = (HttpPostedFileBase)Session["file"];
            string path = (string)Session["Path"];
            string msj = "";
            if (tbEmpresas.empr_Id != 0 && tbEmpresas.empr_Nombre != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEmpresas_Update(id,tbEmpresas.empr_Nombre, path, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    if (!System.IO.File.Exists(Server.MapPath("~/") + path))
                    {
                        file.SaveAs(Server.MapPath("~/") + path);
                    }
                    Session["file"] = null;
                    Session["Path"] = null;
                    Session.Remove("file");
                    Session.Remove("Path");
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

        // GET: Empresas/Delete/5
        public ActionResult Delete(tbEmpresas tbEmpresas)
        {
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
