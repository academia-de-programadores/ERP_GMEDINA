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
    public class SeleccionCandidatosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: SeleccionCandidatos
        public ActionResult Index()
        {
            var tbSeleccionCandidatos = db.tbSeleccionCandidatos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbFaseSeleccion).Include(t => t.tbPersonas).Include(t => t.tbRequisiciones);
            return View(tbSeleccionCandidatos.ToList());
        }

        // GET: SeleccionCandidatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSeleccionCandidatos tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
            if (tbSeleccionCandidatos == null)
            {
                return HttpNotFound();
            }
            return View(tbSeleccionCandidatos);
        }

        // GET: SeleccionCandidatos/Create
        public ActionResult Create()
        {
            ViewBag.scan_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.scan_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.fare_Id = new SelectList(db.tbFaseSeleccion, "fsel_Id", "fsel_RazonInactivo");
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad");
            ViewBag.rper_Id = new SelectList(db.tbRequisiciones, "req_Id", "req_Experiencia");
            return View();
        }

        // POST: SeleccionCandidatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scan_Id,per_Id,fare_Id,scan_Fecha,rper_Id,scan_Estado,scan_RazonInactivo,scan_UsuarioCrea,scan_FechaCrea,scan_UsuarioModifica,scan_FechaModifica")] tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            if (ModelState.IsValid)
            {
                db.tbSeleccionCandidatos.Add(tbSeleccionCandidatos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.scan_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioCrea);
            ViewBag.scan_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioModifica);
            ViewBag.fare_Id = new SelectList(db.tbFaseSeleccion, "fsel_Id", "fsel_RazonInactivo", tbSeleccionCandidatos.fare_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbSeleccionCandidatos.per_Id);
            ViewBag.rper_Id = new SelectList(db.tbRequisiciones, "req_Id", "req_Experiencia", tbSeleccionCandidatos.rper_Id);
            return View(tbSeleccionCandidatos);
        }

        // GET: SeleccionCandidatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSeleccionCandidatos tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
            if (tbSeleccionCandidatos == null)
            {
                return HttpNotFound();
            }
            ViewBag.scan_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioCrea);
            ViewBag.scan_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioModifica);
            ViewBag.fare_Id = new SelectList(db.tbFaseSeleccion, "fsel_Id", "fsel_RazonInactivo", tbSeleccionCandidatos.fare_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbSeleccionCandidatos.per_Id);
            ViewBag.rper_Id = new SelectList(db.tbRequisiciones, "req_Id", "req_Experiencia", tbSeleccionCandidatos.rper_Id);
            return View(tbSeleccionCandidatos);
        }

        // POST: SeleccionCandidatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scan_Id,per_Id,fare_Id,scan_Fecha,rper_Id,scan_Estado,scan_RazonInactivo,scan_UsuarioCrea,scan_FechaCrea,scan_UsuarioModifica,scan_FechaModifica")] tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSeleccionCandidatos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.scan_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioCrea);
            ViewBag.scan_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSeleccionCandidatos.scan_UsuarioModifica);
            ViewBag.fare_Id = new SelectList(db.tbFaseSeleccion, "fsel_Id", "fsel_RazonInactivo", tbSeleccionCandidatos.fare_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbSeleccionCandidatos.per_Id);
            ViewBag.rper_Id = new SelectList(db.tbRequisiciones, "req_Id", "req_Experiencia", tbSeleccionCandidatos.rper_Id);
            return View(tbSeleccionCandidatos);
        }

        // GET: SeleccionCandidatos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSeleccionCandidatos tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
            if (tbSeleccionCandidatos == null)
            {
                return HttpNotFound();
            }
            return View(tbSeleccionCandidatos);
        }

        // POST: SeleccionCandidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSeleccionCandidatos tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
            db.tbSeleccionCandidatos.Remove(tbSeleccionCandidatos);
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
