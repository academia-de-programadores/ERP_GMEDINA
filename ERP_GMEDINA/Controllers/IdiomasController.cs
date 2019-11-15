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
    public class IdiomasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Idiomas
        public ActionResult Index()
        {
            var tbIdiomas = db.tbIdiomas.Where(t => t.idi_Estado == true);
            return View(tbIdiomas.ToList());
        }

        // GET: Idiomas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIdiomas tbIdiomas = db.tbIdiomas.Find(id);
            if (tbIdiomas == null)
            {
                return HttpNotFound();
            }
            return View(tbIdiomas);
        }

        // GET: Idiomas/Create
        public ActionResult Create()
        {
            ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Idiomas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idi_Id,idi_Descripcion,idi_Estado,idi_RazonInactivo,idi_UsuarioCrea,idi_FechaCrea,idi_UsuarioModifica,idi_FechaModifica")] tbIdiomas tbIdiomas)
        {
            if (ModelState.IsValid)
            {
                db.tbIdiomas.Add(tbIdiomas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
            ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbIdiomas);
        }

        // GET: Idiomas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIdiomas tbIdiomas = db.tbIdiomas.Find(id);
            if (tbIdiomas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
            ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbIdiomas);
        }

        // POST: Idiomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idi_Id,idi_Descripcion,idi_Estado,idi_RazonInactivo,idi_UsuarioCrea,idi_FechaCrea,idi_UsuarioModifica,idi_FechaModifica")] tbIdiomas tbIdiomas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbIdiomas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
            ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbIdiomas);
        }

        // GET: Idiomas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIdiomas tbIdiomas = db.tbIdiomas.Find(id);
            if (tbIdiomas == null)
            {
                return HttpNotFound();
            }
            return View(tbIdiomas);
        }

        // POST: Idiomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbIdiomas tbIdiomas = db.tbIdiomas.Find(id);
            db.tbIdiomas.Remove(tbIdiomas);
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
