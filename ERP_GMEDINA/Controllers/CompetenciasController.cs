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
    public class CompetenciasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Competencias
        public ActionResult Index()
        {
            var tbCompetencias = db.tbCompetencias.Where(t => t.comp_Estado == true);
            return View(tbCompetencias.ToList());
        }

        // GET: Competencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            return View(tbCompetencias);
        }

        // GET: Competencias/Create
        public ActionResult Create()
        {
            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Competencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comp_Id,comp_Descripcion,comp_Estado,comp_RazonInactivo,comp_UsuarioCrea,comp_FechaCrea,comp_UsuarioModifica,comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            if (ModelState.IsValid)
            {
                db.tbCompetencias.Add(tbCompetencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // GET: Competencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // POST: Competencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comp_Id,comp_Descripcion,comp_Estado,comp_RazonInactivo,comp_UsuarioCrea,comp_FechaCrea,comp_UsuarioModifica,comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCompetencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // GET: Competencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            return View(tbCompetencias);
        }

        // POST: Competencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            db.tbCompetencias.Remove(tbCompetencias);
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
