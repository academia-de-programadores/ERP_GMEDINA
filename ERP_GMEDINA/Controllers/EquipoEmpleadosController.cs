using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class EquipoEmpleadosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
       
        // GET: EquipoEmpleados
        public ActionResult Index()
        {
            db = new ERP_GMEDINAEntities();
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbEquipoEmpleados = new List<tbEquipoEmpleados> { };
            var equipoe = db.tbEquipoEmpleados.Where(e => e.eqem_Estado == true).Select(ee => new { eqtra_Id = ee.eqtra_Id });
            ViewBag.eqtra_Id = new SelectList(db.tbEquipoTrabajo.Where(x => x.eqtra_Estado == true).Select(x => new { eqtra_Id = x.eqtra_Id, eqtra_Descripcion = x.eqtra_Descripcion }), "eqtra_Id","eqtra_Descripcion");
            return View(tbEquipoEmpleados);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbEquipoEmpleados = db.tbEquipoEmpleados
                        //.Where(x => x.eqem_Estado == true)
                        .Select(t =>
                        new
                        {
                            emp_Id = t.emp_Id,
                            eqem_Id = t.eqem_Id,
                            Empleado = t.tbEquipoTrabajo.tbEquipoEmpleados.Select(p => p.tbEmpleados.tbPersonas.per_Nombres + " " + p.tbEmpleados.tbPersonas.per_Apellidos),
                            Correo = t.tbEquipoTrabajo.tbEquipoEmpleados.Select(p => p.tbEmpleados.tbPersonas.per_CorreoElectronico),
                            Telefono = t.tbEquipoTrabajo.tbEquipoEmpleados.Select(p => p.tbEmpleados.tbPersonas.per_Telefono)
                        }).ToList();
                    return Json((object)tbEquipoEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChildRowData(int? id)
        {
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var lista = db.V_EquipoTrabajoDetalles.Where(x => x.eqem_Id == id && x.eqem_Estado == true).Select(tabla =>
                        new
                        {
                            eqem_Id = tabla.eqem_Id,
                            emp_Id = tabla.emp_Id,
                            eqtra_Id = tabla.eqtra_Id,
                            eqtra_Codigo = tabla.eqtra_Codigo,
                            eqtra_Descripcion = tabla.eqtra_Descripcion,
                            eqtra_Observacion = tabla.eqtra_Observacion,
                            eqem_Fecha = tabla.eqem_Fecha
                        }).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
        }

        // GET: EquipoEmpleados/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: EquipoEmpleados/Create
        [HttpPost]
        public ActionResult Create(tbEquipoEmpleados tbEquipoEmpleados)
        {
            string msj = "";
            if (tbEquipoEmpleados.eqtra_Id !=0)
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbEquipoEmpleados_Insert(tbEquipoEmpleados.emp_Id, tbEquipoEmpleados.eqtra_Id, tbEquipoEmpleados.eqem_Fecha, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbEquipoEmpleados_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
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

        // GET: EquipoEmpleados/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EquipoEmpleados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EquipoEmpleados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquipoEmpleados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
