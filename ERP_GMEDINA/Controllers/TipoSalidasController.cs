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
    public class TipoSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoSalidas
        public ActionResult Index()
        {
            var tbTipoSalidas = db.tbTipoSalidas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbTipoSalidas.ToList());
        }

        // GET: TipoSalidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalidas tbTipoSalidas = db.tbTipoSalidas.Find(id);
            if (tbTipoSalidas == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalidas);
        }

        // GET: TipoSalidas/Create
        public ActionResult Create()
        {
            ViewBag.tsal_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tsal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoSalidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tsal_Id,tsal_Descripcion,tsal_Estado,tsal_RazonInactivo,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTipoSalidas tbTipoSalidas)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoSalidas.Add(tbTipoSalidas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tsal_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioCrea);
            ViewBag.tsal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioModifica);
            return View(tbTipoSalidas);
        }

        // GET: TipoSalidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalidas tbTipoSalidas = db.tbTipoSalidas.Find(id);
            if (tbTipoSalidas == null)
            {
                return HttpNotFound();
            }
            ViewBag.tsal_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioCrea);
            ViewBag.tsal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioModifica);
            return View(tbTipoSalidas);
        }

        // POST: TipoSalidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tsal_Id,tsal_Descripcion,tsal_Estado,tsal_RazonInactivo,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTipoSalidas tbTipoSalidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoSalidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tsal_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioCrea);
            ViewBag.tsal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoSalidas.tsal_UsuarioModifica);
            return View(tbTipoSalidas);
        }

        // GET: TipoSalidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalidas tbTipoSalidas = db.tbTipoSalidas.Find(id);
            if (tbTipoSalidas == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalidas);
        }

        // POST: TipoSalidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoSalidas tbTipoSalidas = db.tbTipoSalidas.Find(id);
            db.tbTipoSalidas.Remove(tbTipoSalidas);
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
