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
    public class RequisicionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Requisiciones
        public ActionResult Index()
        {
            var tbRequisiciones = db.tbRequisiciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbRequisiciones.ToList());
        }

        // GET: Requisiciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisiciones tbRequisiciones = db.tbRequisiciones.Find(id);
            if (tbRequisiciones == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisiciones);
        }

        // GET: Requisiciones/Create
        public ActionResult Create()
        {
            ViewBag.req_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.req_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Requisiciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "req_Id,req_Experiencia,req_Sexo,req_Descripcion,req_EdadMinima,req_EdadMaxima,req_EstadoCivil,req_EducacionSuperior,req_Permanente,req_Duracion,req_Estado,req_RazonInactivo,req_Vacantes,req_FechaRequisicion,req_FechaContratacion,req_UsuarioCrea,req_FechaCrea,req_UsuarioModifica,req_FechaModifica")] tbRequisiciones tbRequisiciones)
        {
            if (ModelState.IsValid)
            {
                db.tbRequisiciones.Add(tbRequisiciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.req_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioCrea);
            ViewBag.req_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioModifica);
            return View(tbRequisiciones);
        }

        // GET: Requisiciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisiciones tbRequisiciones = db.tbRequisiciones.Find(id);
            if (tbRequisiciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.req_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioCrea);
            ViewBag.req_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioModifica);
            return View(tbRequisiciones);
        }

        // POST: Requisiciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "req_Id,req_Experiencia,req_Sexo,req_Descripcion,req_EdadMinima,req_EdadMaxima,req_EstadoCivil,req_EducacionSuperior,req_Permanente,req_Duracion,req_Estado,req_RazonInactivo,req_Vacantes,req_FechaRequisicion,req_FechaContratacion,req_UsuarioCrea,req_FechaCrea,req_UsuarioModifica,req_FechaModifica")] tbRequisiciones tbRequisiciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRequisiciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.req_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioCrea);
            ViewBag.req_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbRequisiciones.req_UsuarioModifica);
            return View(tbRequisiciones);
        }

        // GET: Requisiciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisiciones tbRequisiciones = db.tbRequisiciones.Find(id);
            if (tbRequisiciones == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisiciones);
        }

        // POST: Requisiciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRequisiciones tbRequisiciones = db.tbRequisiciones.Find(id);
            db.tbRequisiciones.Remove(tbRequisiciones);
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
