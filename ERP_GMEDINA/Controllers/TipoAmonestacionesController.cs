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
    public class TipoAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoAmonestaciones
        public ActionResult Index()
        {
            var tbTipoAmonestaciones = db.tbTipoAmonestaciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbTipoAmonestaciones.ToList());
        }

        // GET: TipoAmonestaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            if (tbTipoAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoAmonestaciones);
        }

        // GET: TipoAmonestaciones/Create
        public ActionResult Create()
        {
            ViewBag.tamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoAmonestaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoAmonestaciones.Add(tbTipoAmonestaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioCrea);
            ViewBag.tamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioModifica);
            return View(tbTipoAmonestaciones);
        }

        // GET: TipoAmonestaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            if (tbTipoAmonestaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.tamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioCrea);
            ViewBag.tamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioModifica);
            return View(tbTipoAmonestaciones);
        }

        // POST: TipoAmonestaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoAmonestaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioCrea);
            ViewBag.tamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoAmonestaciones.tamo_UsuarioModifica);
            return View(tbTipoAmonestaciones);
        }

        // GET: TipoAmonestaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            if (tbTipoAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoAmonestaciones);
        }

        // POST: TipoAmonestaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            db.tbTipoAmonestaciones.Remove(tbTipoAmonestaciones);
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
