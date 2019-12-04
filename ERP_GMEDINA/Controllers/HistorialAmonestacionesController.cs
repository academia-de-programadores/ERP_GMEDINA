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
    public class HistorialAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialAmonestaciones
        public ActionResult Index()
        {
            var tbHistorialAmonestaciones = db.tbPersonas.Include(t => t.tbEmpleados);
            return View(tbHistorialAmonestaciones);
        }
        // GET: HistorialAmonestaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialAmonestaciones tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
            if (tbHistorialAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialAmonestaciones);
        }

        // GET: HistorialAmonestaciones/Create
        public ActionResult Create()
        {
            ViewBag.hamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.hamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            ViewBag.hamo_AmonestacionAnterior = new SelectList(db.tbHistorialAmonestaciones, "hamo_Id", "hamo_Observacion");
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion");
            return View();
        }

        // POST: HistorialAmonestaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hamo_Id,emp_Id,tamo_Id,hamo_Fecha,hamo_AmonestacionAnterior,hamo_UsuarioCrea,hamo_FechaCrea")] tbHistorialAmonestaciones tbHistorialAmonestaciones)
        {
            if (ModelState.IsValid)
            {
                db.tbHistorialAmonestaciones.Add(tbHistorialAmonestaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioCrea);
            ViewBag.hamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialAmonestaciones.emp_Id);
            ViewBag.hamo_AmonestacionAnterior = new SelectList(db.tbHistorialAmonestaciones, "hamo_Id", "hamo_Observacion", tbHistorialAmonestaciones.hamo_AmonestacionAnterior);
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion", tbHistorialAmonestaciones.tamo_Id);
            return View(tbHistorialAmonestaciones);
        }

        // GET: HistorialAmonestaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialAmonestaciones tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
            if (tbHistorialAmonestaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.hamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioCrea);
            ViewBag.hamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialAmonestaciones.emp_Id);
            ViewBag.hamo_AmonestacionAnterior = new SelectList(db.tbHistorialAmonestaciones, "hamo_Id", "hamo_Observacion", tbHistorialAmonestaciones.hamo_AmonestacionAnterior);
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion", tbHistorialAmonestaciones.tamo_Id);
            return View(tbHistorialAmonestaciones);
        }

        // POST: HistorialAmonestaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hamo_Id,emp_Id,tamo_Id,hamo_Fecha,hamo_AmonestacionAnterior,hamo_Observacion,hamo_Estado,hamo_RazonInactivo,hamo_UsuarioCrea,hamo_FechaCrea,hamo_UsuarioModifica,hamo_FechaModifica")] tbHistorialAmonestaciones tbHistorialAmonestaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHistorialAmonestaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioCrea);
            ViewBag.hamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialAmonestaciones.hamo_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialAmonestaciones.emp_Id);
            ViewBag.hamo_AmonestacionAnterior = new SelectList(db.tbHistorialAmonestaciones, "hamo_Id", "hamo_Observacion", tbHistorialAmonestaciones.hamo_AmonestacionAnterior);
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion", tbHistorialAmonestaciones.tamo_Id);
            return View(tbHistorialAmonestaciones);
        }

        // GET: HistorialAmonestaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialAmonestaciones tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
            if (tbHistorialAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialAmonestaciones);
        }

        // POST: HistorialAmonestaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHistorialAmonestaciones tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
            db.tbHistorialAmonestaciones.Remove(tbHistorialAmonestaciones);
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
