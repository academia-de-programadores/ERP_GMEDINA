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
    public class TipoHorasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoHoras
        public ActionResult Index()
        {
              tbUsuario Usuario = new tbUsuario();
            Usuario.usu_Id = 1;
            Session["Usuario"] = Usuario;
            var tbTipoHoras = db.tbTipoHoras.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Where(x=>x.tiho_Estado==true);
            return View(tbTipoHoras.ToList());
        }

        // GET: TipoHoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoHoras tbTipoHoras = db.tbTipoHoras.Find(id);
            if (tbTipoHoras == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoHoras);
        }

        // GET: TipoHoras/Create
        public ActionResult Create()
        {
            ViewBag.tiho_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tiho_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoHoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tiho_Descripcion,tiho_Recargo,tiho_UsuarioCrea,tiho_FechaCrea")] tbTipoHoras TipoHoras)
        {
         
            if (ModelState.IsValid)
            {
                db.tbTipoHoras.Add(TipoHoras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(TipoHoras);
        }

        // GET: TipoHoras/Edit/5
        public ActionResult Edit(int? id)
        {
            var List = db.UDP_RRHH_tbTipoHoras_Select(id).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoHoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tiho_Id,tiho_Descripcion,tiho_Recargo,tiho_Estado,tiho_RazonInactivo,tiho_UsuarioCrea,tiho_FechaCrea,tiho_UsuarioModifica,tiho_FechaModifica")] tbTipoHoras tbTipoHoras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoHoras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tiho_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoHoras.tiho_UsuarioCrea);
            ViewBag.tiho_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoHoras.tiho_UsuarioModifica);
            return View(tbTipoHoras);
        }

        // GET: TipoHoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoHoras tbTipoHoras = db.tbTipoHoras.Find(id);
            if (tbTipoHoras == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoHoras);
        }

        // POST: TipoHoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoHoras tbTipoHoras = db.tbTipoHoras.Find(id);
            db.tbTipoHoras.Remove(tbTipoHoras);
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
