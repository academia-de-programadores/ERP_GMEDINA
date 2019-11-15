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
    public class tbJornadasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: tbJornadas
        public ActionResult Index()
        {
            var tbJornadas = db.tbJornadas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbJornadas.ToList());
        }

        // GET: tbJornadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            return View(tbJornadas);
        }

        // GET: tbJornadas/Create
        public ActionResult Create()
        {
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: tbJornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jor_Id,jor_Descripcion,jor_Estado,jor_RazonInactivo,jor_UsuarioCrea,jor_FechaCrea,jor_UsuarioModifica,jor_FechaModifica")] tbJornadas tbJornadas)
        {
            if (ModelState.IsValid)
            {
                db.tbJornadas.Add(tbJornadas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        // GET: tbJornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        // POST: tbJornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jor_Id,jor_Descripcion,jor_Estado,jor_RazonInactivo,jor_UsuarioCrea,jor_FechaCrea,jor_UsuarioModifica,jor_FechaModifica")] tbJornadas tbJornadas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbJornadas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        // GET: tbJornadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            return View(tbJornadas);
        }

        // POST: tbJornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            db.tbJornadas.Remove(tbJornadas);
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
