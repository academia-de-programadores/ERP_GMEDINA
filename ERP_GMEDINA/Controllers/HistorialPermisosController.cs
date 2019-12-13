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
    public class HistorialPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialPermisos
        public ActionResult Index()
        {
            var tbHistorialPermisos = db.tbHistorialPermisos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados).Include(t => t.tbTipoPermisos);
            return View(tbHistorialPermisos.ToList());
        }
        public ActionResult llenarTabla()
        {

            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var V_HistorialPermisos = db.V_HistorialPermisos
                        .Select(
                        t => new
                        {
                            hper_Id = t.Id,
                            tper_Id = t.Id_Permiso,
                            tper_Descripcion = t.Descripcion_Permiso,
                            hper_fechaInicio = t.Fecha_Inicial,
                            hper_fechaFin = t.Fecha_Fin,
                            hper_Duracion = t.Duracion,
                            hper_Justificado = t.Justificado,
                            per_Nombres = t.Nombre_Completo,
                        }
                        )
                        .ToList();
                    return Json(V_HistorialPermisos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult llenarDropDowlistTipoSalida()
        {
            var tbTipoPermisos = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbTipoPermisos.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    tbTipoPermisos.AddRange(db.tbTipoPermisos
                    .Select(tabla => new {
                        Id = tabla.tper_Id,
                        Descripcion = tabla.tper_Descripcion
                    })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("tbTipoPermisos", tbTipoPermisos);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: HistorialPermisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }

        // GET: HistorialPermisos/Create
        public ActionResult Create()
        {
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion");
            return View();
        }

        // POST: HistorialPermisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(tbHistorialPermisos tbHistorialPermisos)
        {
            string msj = "...";
            if (tbHistorialPermisos.hper_Observacion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialPermisos_Insert(tbHistorialPermisos.hper_Id, 
                        tbHistorialPermisos.emp_Id, tbHistorialPermisos.tper_Id,tbHistorialPermisos.hper_fechaInicio,
                        tbHistorialPermisos.hper_fechaFin, tbHistorialPermisos.hper_Duracion, tbHistorialPermisos.hper_Observacion,
                        tbHistorialPermisos.hper_Justificado, tbHistorialPermisos.hper_PorcentajeIndemnizado,
                        tbHistorialPermisos.hper_RazonInactivo,
                        Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialPermisos_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }

            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "hper_Id,emp_Id,tper_Id,hper_fechaInicio,hper_fechaFin,hper_Duracion,hper_Observacion,hper_Justificado,hper_PorcentajeIndemnizado,hper_Estado,hper_RazonInactivo,hper_UsuarioCrea,hper_FechaCrea,hper_UsuarioModifica,hper_FechaModifica")] tbHistorialPermisos tbHistorialPermisos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbHistorialPermisos.Add(tbHistorialPermisos);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
        //    ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
        //    ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
        //    return View(tbHistorialPermisos);
        //}

        // GET: HistorialPermisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
            return View(tbHistorialPermisos);
        }

        // POST: HistorialPermisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hper_Id,emp_Id,tper_Id,hper_fechaInicio,hper_fechaFin,hper_Duracion,hper_Observacion,hper_Justificado,hper_PorcentajeIndemnizado,hper_Estado,hper_RazonInactivo,hper_UsuarioCrea,hper_FechaCrea,hper_UsuarioModifica,hper_FechaModifica")] tbHistorialPermisos tbHistorialPermisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHistorialPermisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hper_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioCrea);
            ViewBag.hper_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialPermisos.hper_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialPermisos.emp_Id);
            ViewBag.tper_Id = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion", tbHistorialPermisos.tper_Id);
            return View(tbHistorialPermisos);
        }

        // GET: HistorialPermisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }

        // POST: HistorialPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHistorialPermisos tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
            db.tbHistorialPermisos.Remove(tbHistorialPermisos);
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
