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
    public class HistorialVacacionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialVacaciones
        public ActionResult Index()
        {
            var tbHistorialVacaciones = db.tbHistorialVacaciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbHistorialVacaciones.ToList());
        }

        // GET: HistorialVacaciones/Details/5
        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var Empleados = db.V_HVacacionesEmpleados.Where(t => t.emp_Estado == true)
                        .Select(
                        t => new
                        {
                            emp_Id = t.emp_Id,
                            Empleado = t.emp_NombreCompleto,
                            Cargo = t.car_Descripcion,
                            Departamento = t.depto_Descripcion
                        }).ToList();
                    return Json(Empleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_Historialvacaciones > lista = new List<V_Historialvacaciones> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Historialvacaciones.Where(x => x.emp_Id == id && x.hvac_Estado == true).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }




        // GET: HistorialAmonestaciones/Create
        public JsonResult Create(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string msj = "";
            try
            {
                var list = db.UDP_RRHH_tbHistorialVacaciones_Insert(tbHistorialVacaciones.emp_Id,
                                                                        tbHistorialVacaciones.hvac_FechaInicio,
                                                                        tbHistorialVacaciones.hvac_FechaFin,
                                                                        1,
                                                                        DateTime.Now);
                foreach (UDP_RRHH_tbHistorialVacaciones_Insert_Result item in list)
                {
                    msj = item.MensajeError + " ";
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }




        // GET: HistorialVacaciones/Create
        //public ActionResult Create()
        //{
        //    ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
        //    return View();
        //}

        // POST: HistorialVacaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "hvac_Id,emp_Id,hvac_FechaInicio,hvac_FechaFin,hvac_CantDias,hvac_DiasPagados,hvac_MesVacaciones,hvac_AnioVacaciones,hvac_Estado,hvac_RazonInactivo,hvac_UsuarioCrea,hvac_FechaCrea,hvac_UsuarioModifica,hvac_FechaModifica")] tbHistorialVacaciones tbHistorialVacaciones)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbHistorialVacaciones.Add(tbHistorialVacaciones);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioCrea);
        //    ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioModifica);
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialVacaciones.emp_Id);
        //    return View(tbHistorialVacaciones);
        //}

        // GET: HistorialVacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialVacaciones tbHistorialVacaciones = db.tbHistorialVacaciones.Find(id);
            if (tbHistorialVacaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioCrea);
            ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialVacaciones.emp_Id);
            return View(tbHistorialVacaciones);
        }

        // POST: HistorialVacaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hvac_Id,emp_Id,hvac_FechaInicio,hvac_FechaFin,hvac_CantDias,hvac_DiasPagados,hvac_MesVacaciones,hvac_AnioVacaciones,hvac_Estado,hvac_RazonInactivo,hvac_UsuarioCrea,hvac_FechaCrea,hvac_UsuarioModifica,hvac_FechaModifica")] tbHistorialVacaciones tbHistorialVacaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHistorialVacaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioCrea);
            ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioModifica);
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialVacaciones.emp_Id);
            return View(tbHistorialVacaciones);
        }

        // GET: HistorialVacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialVacaciones tbHistorialVacaciones = db.tbHistorialVacaciones.Find(id);
            if (tbHistorialVacaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialVacaciones);
        }

        // POST: HistorialVacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHistorialVacaciones tbHistorialVacaciones = db.tbHistorialVacaciones.Find(id);
            db.tbHistorialVacaciones.Remove(tbHistorialVacaciones);
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
