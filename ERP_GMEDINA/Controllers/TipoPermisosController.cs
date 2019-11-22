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
    public class TipoPermisosController : Controller
    {

        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: tbTipoPermisos
        public ActionResult Index()
        {
            var tbTipoPermisos = db.tbTipoPermisos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbTipoPermisos.ToList());
        }

        // GET: tbTipoPermisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPermisos tbTipoPermisos = db.tbTipoPermisos.Find(id);
            if (tbTipoPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPermisos);
        }

        // GET: tbTipoPermisos/Create
        public ActionResult Create()
        {
            ViewBag.tper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: tbTipoPermisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tper_Id,tper_Descripcion,tper_Estado,tper_RazonInactivo,tper_UsuarioCrea,tper_FechaCrea,tper_UsuarioModifica,tper_FechaModifica")] tbTipoPermisos tbTipoPermisos)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoPermisos.Add(tbTipoPermisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioCrea);
            ViewBag.tper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioModifica);
            return View(tbTipoPermisos);
        }

        // GET: tbTipoPermisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPermisos tbTipoPermisos = db.tbTipoPermisos.Find(id);
            if (tbTipoPermisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.tper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioCrea);
            ViewBag.tper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioModifica);
            return View(tbTipoPermisos);
        }

        // POST: tbTipoPermisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tper_Id,tper_Descripcion,tper_Estado,tper_RazonInactivo,tper_UsuarioCrea,tper_FechaCrea,tper_UsuarioModifica,tper_FechaModifica")] tbTipoPermisos tbTipoPermisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoPermisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioCrea);
            ViewBag.tper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPermisos.tper_UsuarioModifica);
            return View(tbTipoPermisos);
        }

        // GET: tbTipoPermisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPermisos tbTipoPermisos = db.tbTipoPermisos.Find(id);
            if (tbTipoPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPermisos);
        }

        // POST: tbTipoPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoPermisos tbTipoPermisos = db.tbTipoPermisos.Find(id);
            db.tbTipoPermisos.Remove(tbTipoPermisos);
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
