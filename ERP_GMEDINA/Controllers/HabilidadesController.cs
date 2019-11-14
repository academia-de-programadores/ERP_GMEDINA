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
    public class HabilidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Habilidades
        public ActionResult Index()
        {
            //var tbHabilidades = db.tbHabilidades.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            tbHabilidades Habilidad = new tbHabilidades {habi_Descripcion="hola", habi_Id=1 };
            List<tbHabilidades> tbHabilidades = new List<Models.tbHabilidades> { };
            tbHabilidades.Add(Habilidad);
            return View(tbHabilidades);
        }

        // GET: Habilidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHabilidades tbHabilidades = db.tbHabilidades.Find(id);
            if (tbHabilidades == null)
            {
                return HttpNotFound();
            }
            return View(tbHabilidades);
        }

        // GET: Habilidades/Create
        public ActionResult Create()
        {
            ViewBag.habi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.habi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Habilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "habi_Id,habi_Descripcion,habi_Estado,habi_RazonInactivo,habi_UsuarioCrea,habi_FechaCrea,habi_UsuarioModifica,habi_FechaModifica")] tbHabilidades tbHabilidades)
        {
            string msj = "";
            try
            {
                db.tbHabilidades.Add(tbHabilidades);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj);
        }

        // GET: Habilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHabilidades tbHabilidades = db.tbHabilidades.Find(id);
            if (tbHabilidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.habi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHabilidades.habi_UsuarioCrea);
            ViewBag.habi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHabilidades.habi_UsuarioModifica);
            return View(tbHabilidades);
        }

        // POST: Habilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "habi_Id,habi_Descripcion,habi_Estado,habi_RazonInactivo,habi_UsuarioCrea,habi_FechaCrea,habi_UsuarioModifica,habi_FechaModifica")] tbHabilidades tbHabilidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHabilidades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHabilidades.habi_UsuarioCrea);
            ViewBag.habi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHabilidades.habi_UsuarioModifica);
            return View(tbHabilidades);
        }

        // GET: Habilidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHabilidades tbHabilidades = db.tbHabilidades.Find(id);
            if (tbHabilidades == null)
            {
                return HttpNotFound();
            }
            return View(tbHabilidades);
        }

        // POST: Habilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHabilidades tbHabilidades = db.tbHabilidades.Find(id);
            db.tbHabilidades.Remove(tbHabilidades);
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
