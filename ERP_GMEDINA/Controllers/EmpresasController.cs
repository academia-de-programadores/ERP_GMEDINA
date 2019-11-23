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
        private ERP_GMEDINAEntities1 db = new ERP_GMEDINAEntities1();

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

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            if (tbEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpresas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbEmpresas> tbEmpresas =
                new List<Models.tbEmpresas> { };
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

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(tbEmpresas tbEmpresas)
        {
            string msj = "...";
            if (tbEmpresas.empr_Nombre != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbEmpresas_Insert(tbEmpresas.empr_Nombre, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEmpresas_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
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

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            if (tbEmpresas == null)
            {
                return HttpNotFound();
            }
            ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioCrea);
            ViewBag.empr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioModifica);
            return View(tbEmpresas);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empr_Id,empr_Nombre,empr_Estado,empr_RazonInactivo,empr_UsuarioCrea,empr_FechaCrea,empr_UsuarioModifica,empr_FechaModifica")] tbEmpresas tbEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEmpresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioCrea);
            ViewBag.empr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioModifica);
            return View(tbEmpresas);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            if (tbEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpresas);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            db.tbEmpresas.Remove(tbEmpresas);
            db.SaveChanges();
            return RedirectToAction("Index");
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
