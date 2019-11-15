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
    public class TitulosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Titulos
        public ActionResult Index()
        {
            var tbTitulos = db.tbTitulos.Where(t => t.titu_Estado == true);
            return View(tbTitulos.ToList());
        }

        // GET: Titulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            return View(tbTitulos);
        }

        // GET: Titulos/Create
        public ActionResult Create()
        {
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Titulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "titu_Id,titu_Descripcion,titu_Estado,titu_RazonInactivo,titu_UsuarioCrea,titu_FechaCrea,titu_UsuarioModifica,titu_FechaModifica")] tbTitulos tbTitulos)
        {
            if (ModelState.IsValid)
            {
                db.tbTitulos.Add(tbTitulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioModifica);
            return View(tbTitulos);
        }

        // GET: Titulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioModifica);
            return View(tbTitulos);
        }

        // POST: Titulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "titu_Id,titu_Descripcion,titu_Estado,titu_RazonInactivo,titu_UsuarioCrea,titu_FechaCrea,titu_UsuarioModifica,titu_FechaModifica")] tbTitulos tbTitulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTitulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioModifica);
            return View(tbTitulos);
        }

        // GET: Titulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            return View(tbTitulos);
        }

        // POST: Titulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            db.tbTitulos.Remove(tbTitulos);
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
