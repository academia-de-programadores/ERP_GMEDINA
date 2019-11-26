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
    public class AreasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Areas
        public ActionResult Index()
        {
            var tbAreas = db.tbAreas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCargos);
            return View(tbAreas.ToList());
        }
        //[HttpPost]
        public ActionResult ChildRowData(int? id)
        {
            var tbAreas = db.V_Departamentos.Where(x=>x.area_Id==id);
            return Json(tbAreas,JsonRequestBehavior.AllowGet);
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAreas tbAreas = db.tbAreas.Find(id);
            if (tbAreas == null)
            {
                return HttpNotFound();
            }
            return View(tbAreas);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.suc_Id = new SelectList(db.tbSucursales, "suc_Id", "suc_Descripcion");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Include = "suc_Id,area_Descripcion")] tbAreas tbAreas)
        {
            tbAreas.tbCargos = new tbCargos {car_Descripcion= Request["tbCargos.car_Descripcion"] };
            if (ModelState.IsValid)
            {
                db.tbAreas.Add(tbAreas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.suc_Id = new SelectList(db.tbSucursales, "suc_Id", "suc_Descripcion");
            return View(tbAreas);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAreas tbAreas = db.tbAreas.Find(id);
            if (tbAreas == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_Usuariocrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAreas.area_Usuariocrea);
            ViewBag.area_Usuariomodifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAreas.area_Usuariomodifica);
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion", tbAreas.car_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursales, "suc_Id", "suc_Descripcion");
            return View(tbAreas);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbAreas tbAreas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAreas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.area_Usuariocrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAreas.area_Usuariocrea);
            ViewBag.area_Usuariomodifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAreas.area_Usuariomodifica);
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion", tbAreas.car_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursales, "suc_Id", "suc_Descripcion");
            return View(tbAreas);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAreas tbAreas = db.tbAreas.Find(id);
            if (tbAreas == null)
            {
                return HttpNotFound();
            }
            return View(tbAreas);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAreas tbAreas = db.tbAreas.Find(id);
            db.tbAreas.Remove(tbAreas);
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
