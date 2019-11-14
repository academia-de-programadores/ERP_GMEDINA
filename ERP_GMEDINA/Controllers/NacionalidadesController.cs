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
    public class NacionalidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Nacionalidades
        public ActionResult Index()
        {
            var tbNacionalidades = db.tbNacionalidades.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbNacionalidades.ToList());
        }

        // GET: Nacionalidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            if (tbNacionalidades == null)
            {
                return HttpNotFound();
            }
            return View(tbNacionalidades);
        }

        // GET: Nacionalidades/Create
        public ActionResult Create()
        {
            ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Nacionalidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nac_Id,nac_Descripcion,nac_Estado,nac_RazonInactivo,nac_UsuarioCrea,nac_FechaCrea,nac_UsuarioModifica,nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        {
            if (ModelState.IsValid)
            {
                db.tbNacionalidades.Add(tbNacionalidades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
            ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
            return View(tbNacionalidades);
        }

        // GET: Nacionalidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            if (tbNacionalidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
            ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
            return View(tbNacionalidades);
        }

        // POST: Nacionalidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nac_Id,nac_Descripcion,nac_Estado,nac_RazonInactivo,nac_UsuarioCrea,nac_FechaCrea,nac_UsuarioModifica,nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbNacionalidades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
            ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
            return View(tbNacionalidades);
        }

        // GET: Nacionalidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            if (tbNacionalidades == null)
            {
                return HttpNotFound();
            }
            return View(tbNacionalidades);
        }

        // POST: Nacionalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            db.tbNacionalidades.Remove(tbNacionalidades);
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
