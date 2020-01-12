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
    public class IngresosIndividualesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: IngresosIndividuales
        public ActionResult Index()
        {
            var tbIngresosIndividuales = db.tbIngresosIndividuales.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbIngresosIndividuales.ToList());
        }

        // GET: IngresosIndividuales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIngresosIndividuales tbIngresosIndividuales = db.tbIngresosIndividuales.Find(id);
            if (tbIngresosIndividuales == null)
            {
                return HttpNotFound();
            }
            return View(tbIngresosIndividuales);
        }

        // GET: IngresosIndividuales/Create
        public ActionResult Create()
        {
            ViewBag.ini_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.ini_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            return View();
        }

        // POST: IngresosIndividuales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ini_IdIngresosIndividuales,ini_Motivo,emp_Id,ini_Monto,ini_PagaSiempre,ini_Pagado,ini_UsuarioCrea,ini_FechaCrea,ini_UsuarioModifica,ini_FechaModifica,ini_Activo")] tbIngresosIndividuales tbIngresosIndividuales)
        {
            if (ModelState.IsValid)
            {
                db.tbIngresosIndividuales.Add(tbIngresosIndividuales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ini_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioCrea);
            ViewBag.ini_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbIngresosIndividuales.emp_Id);
            return View(tbIngresosIndividuales);
        }

        // GET: IngresosIndividuales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIngresosIndividuales tbIngresosIndividuales = db.tbIngresosIndividuales.Find(id);
            if (tbIngresosIndividuales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ini_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioCrea);
            ViewBag.ini_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbIngresosIndividuales.emp_Id);
            return View(tbIngresosIndividuales);
        }

        // POST: IngresosIndividuales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ini_IdIngresosIndividuales,ini_Motivo,emp_Id,ini_Monto,ini_PagaSiempre,ini_Pagado,ini_UsuarioCrea,ini_FechaCrea,ini_UsuarioModifica,ini_FechaModifica,ini_Activo")] tbIngresosIndividuales tbIngresosIndividuales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbIngresosIndividuales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ini_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioCrea);
            ViewBag.ini_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIngresosIndividuales.ini_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbIngresosIndividuales.emp_Id);
            return View(tbIngresosIndividuales);
        }

        // GET: IngresosIndividuales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIngresosIndividuales tbIngresosIndividuales = db.tbIngresosIndividuales.Find(id);
            if (tbIngresosIndividuales == null)
            {
                return HttpNotFound();
            }
            return View(tbIngresosIndividuales);
        }

        // POST: IngresosIndividuales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbIngresosIndividuales tbIngresosIndividuales = db.tbIngresosIndividuales.Find(id);
            db.tbIngresosIndividuales.Remove(tbIngresosIndividuales);
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
