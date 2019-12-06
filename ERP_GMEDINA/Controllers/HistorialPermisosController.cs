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
    public class HistorialPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialPermisos
        public ActionResult Index()
        {
            var tbHistorialPermisos = db.tbHistorialPermisos.Include(t => t.tbEmpleados).Include(t => t.tbTipoPermisos).Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbHistorialPermisos.ToList());
        }

        // GET: HistorialPermisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }

        // GET: HistorialPermisos/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion");
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: HistorialPermisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hper_Id,emp_Id,tper_Id,hper_fechaInicio,hper_fechaFin,hper_Duracion,hper_Observacion,hper_PorcentajeIndemnizado,hper_Estado,hper_RazonInactivo,hper_UsuarioCrea,hper_FechaCrea,hper_UsuarioModifica,hper_FechaModifica")] tbHistorialPermisos tbHistorialPermisos)
        {
            if (ModelState.IsValid)
            {
                db.tbHistorialPermisos.Add(tbHistorialPermisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
            return View(tbHistorialPermisos);
        }

        // GET: HistorialPermisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
            return View(tbHistorialPermisos);
        }

        // POST: HistorialPermisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hper_Id,emp_Id,tper_Id,hper_fechaInicio,hper_fechaFin,hper_Duracion,hper_Observacion,hper_PorcentajeIndemnizado,hper_Estado,hper_RazonInactivo,hper_UsuarioCrea,hper_FechaCrea,hper_UsuarioModifica,hper_FechaModifica")] tbHistorialPermisos tbHistorialPermisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHistorialPermisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
            return View(tbHistorialPermisos);
        }

        // GET: HistorialPermisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }

        // POST: HistorialPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            db.tbHistorialPermisos.Remove(tbHistorialPermisos);
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
