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
            var tbEmpresas = db.tbEmpresas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbEmpresas.ToList());
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empr_Id,empr_Nombre,empr_Estado,empr_RazonInactivo,empr_UsuarioCrea,empr_FechaCrea,empr_UsuarioModifica,empr_FechaModifica")] tbEmpresas tbEmpresas)
        {
            tbEmpresas.empr_UsuarioCrea = 1;
            tbEmpresas.empr_FechaCrea = DateTime.Now;

            string response = String.Empty;
            IEnumerable<Object> listaEmpresas = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listaEmpresas = db.UDP_RRHH_tbEmpresas_Insert(tbEmpresas.empr_Nombre,
                                                                    tbEmpresas.empr_UsuarioCrea,
                                                                    tbEmpresas.empr_FechaCrea);
                    foreach (UDP_RRHH_tbEmpresas_Insert_Result Resultado in listaEmpresas)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    response = Ex.Message.ToString();
                }
                response = "Bien";
            }

            else
            {
                response = "Error";
            }

            ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioCrea);
            return Json(response, JsonRequestBehavior.AllowGet);
    }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            var List = db.UDP_RRHH_tbEmpresas_Select(id).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //tbEmpresas tbEmpresas = db.tbEmpresas.Find(id);
            //if (tbEmpresas == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioCrea);
            //ViewBag.empr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpresas.empr_UsuarioModifica);
            //return View(tbEmpresas);
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
