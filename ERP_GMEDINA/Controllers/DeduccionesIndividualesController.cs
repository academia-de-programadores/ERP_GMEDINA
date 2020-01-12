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
    public class DeduccionesIndividualesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: DeduccionesIndividuales
        public ActionResult Index()
        {
            var tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbDeduccionesIndividuales.ToList());
        }

        // GET: DeduccionesIndividuales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionesIndividuales tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Find(id);
            if (tbDeduccionesIndividuales == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionesIndividuales);
        }

        // GET: DeduccionesIndividuales/Create
        public ActionResult Create()
        {
            ViewBag.dei_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.dei_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            return View();
        }

        // POST: DeduccionesIndividuales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dei_IdDeduccionesIndividuales,dei_Motivo,emp_Id,dei_MontoInicial,dei_MontoRestante,dei_Cuota,dei_PagaSiempre,dei_Pagado,dei_UsuarioCrea,dei_FechaCrea,dei_UsuarioModifica,dei_FechaModifica,dei_Activo")] tbDeduccionesIndividuales tbDeduccionesIndividuales)
        {
            if (ModelState.IsValid)
            {
                db.tbDeduccionesIndividuales.Add(tbDeduccionesIndividuales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dei_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioCrea);
            ViewBag.dei_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbDeduccionesIndividuales.emp_Id);
            return View(tbDeduccionesIndividuales);
        }

        // GET: DeduccionesIndividuales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionesIndividuales tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Find(id);
            if (tbDeduccionesIndividuales == null)
            {
                return HttpNotFound();
            }
            ViewBag.dei_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioCrea);
            ViewBag.dei_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbDeduccionesIndividuales.emp_Id);
            return View(tbDeduccionesIndividuales);
        }

        // POST: DeduccionesIndividuales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dei_IdDeduccionesIndividuales,dei_Motivo,emp_Id,dei_MontoInicial,dei_MontoRestante,dei_Cuota,dei_PagaSiempre,dei_Pagado,dei_UsuarioCrea,dei_FechaCrea,dei_UsuarioModifica,dei_FechaModifica,dei_Activo")] tbDeduccionesIndividuales tbDeduccionesIndividuales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDeduccionesIndividuales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dei_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioCrea);
            ViewBag.dei_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDeduccionesIndividuales.dei_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbDeduccionesIndividuales.emp_Id);
            return View(tbDeduccionesIndividuales);
        }

        // GET: DeduccionesIndividuales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionesIndividuales tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Find(id);
            if (tbDeduccionesIndividuales == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionesIndividuales);
        }

        // POST: DeduccionesIndividuales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDeduccionesIndividuales tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Find(id);
            db.tbDeduccionesIndividuales.Remove(tbDeduccionesIndividuales);
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
