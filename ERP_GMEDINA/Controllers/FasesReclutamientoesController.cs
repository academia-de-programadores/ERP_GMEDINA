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
    public class FasesReclutamientoesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: FasesReclutamientoes
        public ActionResult Index()
        {
            var tbFasesReclutamiento = db.tbFasesReclutamiento.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbFasesReclutamiento.ToList());
        }

        // GET: FasesReclutamientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFasesReclutamiento tbFasesReclutamiento = db.tbFasesReclutamiento.Find(id);
            if (tbFasesReclutamiento == null)
            {
                return HttpNotFound();
            }
            return View(tbFasesReclutamiento);
        }

        // GET: FasesReclutamientoes/Create
        public ActionResult Create()
        {
            ViewBag.fare_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.fare_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: FasesReclutamientoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fare_Id,fare_Descripcion,fare_Estado,fare_RazonInactivo,fare_UsuarioCrea,fare_FechaCrea,fare_UsuarioModifica,fare_FechaModifica")] tbFasesReclutamiento tbFasesReclutamiento)
        {
            if (ModelState.IsValid)
            {
                db.tbFasesReclutamiento.Add(tbFasesReclutamiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fare_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioCrea);
            ViewBag.fare_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioModifica);
            return View(tbFasesReclutamiento);
        }

        // GET: FasesReclutamientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFasesReclutamiento tbFasesReclutamiento = db.tbFasesReclutamiento.Find(id);
            if (tbFasesReclutamiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.fare_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioCrea);
            ViewBag.fare_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioModifica);
            return View(tbFasesReclutamiento);
        }

        // POST: FasesReclutamientoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fare_Id,fare_Descripcion,fare_Estado,fare_RazonInactivo,fare_UsuarioCrea,fare_FechaCrea,fare_UsuarioModifica,fare_FechaModifica")] tbFasesReclutamiento tbFasesReclutamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFasesReclutamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fare_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioCrea);
            ViewBag.fare_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFasesReclutamiento.fare_UsuarioModifica);
            return View(tbFasesReclutamiento);
        }

        // GET: FasesReclutamientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFasesReclutamiento tbFasesReclutamiento = db.tbFasesReclutamiento.Find(id);
            if (tbFasesReclutamiento == null)
            {
                return HttpNotFound();
            }
            return View(tbFasesReclutamiento);
        }

        // POST: FasesReclutamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbFasesReclutamiento tbFasesReclutamiento = db.tbFasesReclutamiento.Find(id);
            db.tbFasesReclutamiento.Remove(tbFasesReclutamiento);
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
