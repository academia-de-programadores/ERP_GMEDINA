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

        public JsonResult llenarTabla()
        {
            List<tbRequisiciones> tbRequisiciones =
                new List<Models.tbRequisiciones> { };
            foreach (tbRequisiciones x in db.tbRequisiciones.ToList().Where(x => x.req_Estado == true))
            {
                tbRequisiciones.Add(new tbRequisiciones
                {
                    req_Id = x.req_Id,
                    req_Experiencia = x.req_Experiencia,
                    req_Sexo = x.req_Sexo,
                    req_Descripcion = x.req_Descripcion,
                    req_EdadMinima = x.req_EdadMinima,
                    req_EdadMaxima = x.req_EdadMaxima,
                    req_EstadoCivil = x.req_EstadoCivil,
                    req_EducacionSuperior = x.req_EducacionSuperior,
                    req_Permanente = x.req_Permanente,
                    req_Duracion = x.req_Duracion,
                    req_Vacantes = x.req_Vacantes,
                    req_FechaRequisicion = x.req_FechaRequisicion,
                    req_FechaContratacion = x.req_FechaContratacion
                });
            }
            return Json(tbRequisiciones, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            //List<tbHorarios> lista = new List<tbHorarios> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    List<object> lista = db.V_DatosRequisicion.Where(x => x.req_Id == id)
                        .Select(tabla => new { Descripcion = tabla.Descripcion, TipoDato = tabla.TipoDato, req_Id = tabla.req_Id }).ToList<object>();
                    foreach(object x in lista)
                    {
                        
                    }
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch()
                {
                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
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
