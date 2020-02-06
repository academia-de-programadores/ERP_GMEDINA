using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class EquipoEmpleadosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: EquipoEmpleados
        Models.Helpers Fuction = new Models.Helpers();
        [SessionManager("EquipoEmpleados/Index")]
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            db = new ERP_GMEDINAEntities();
            bool Admin = (bool)Session["Admin"];
            var tbEquipoEmpleados = new List<tbEquipoEmpleados> { };
            var equipoe = db.tbEquipoEmpleados.Where(e => e.eqem_Estado == true).Count();
            ViewBag.eqtra_Id = new SelectList(db.tbEquipoTrabajo.Where(x => x.eqtra_Estado == true && (db.tbEquipoEmpleados.Where(ee => ee.eqtra_Id == x.eqtra_Id && ee.eqem_Estado == true).Count()) == 0).Select(x => new { eqtra_Id = x.eqtra_Id, eqtra_Descripcion = x.eqtra_Descripcion }), "eqtra_Id", "eqtra_Descripcion");
            return View(tbEquipoEmpleados);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbEmpleados = db.tbEmpleados
                        //.Where(x => x.eqem_Estado == true)
                        .Select(t =>
                        new
                        {
                            emp_Id = t.emp_Id,
                            Empleado = t.tbPersonas.per_Nombres + " " + t.tbPersonas.per_Apellidos,
                            Correo = t.tbPersonas.per_CorreoElectronico,
                            Telefono = t.tbPersonas.per_Telefono,
                            Estado = t.emp_Estado
                        }).Where(t => t.Estado == true).ToList();
                    return Json(tbEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }        

        public ActionResult RefreshEquipos()
        {
            try
            {
                db = new ERP_GMEDINAEntities();

                var Equipo = db.tbEquipoTrabajo.Select(e => new  { e.eqtra_Id, e.eqtra_Descripcion}).ToList();
                var EquipoAsignado = db.tbEquipoEmpleados.Select(e => new  {e.eqtra_Id, e.eqem_Estado }).Where(d => d.eqem_Estado == true).ToList();
                bool Found = false;
                List<tbEquipoTrabajo> Send = new List<tbEquipoTrabajo>();
                tbEquipoTrabajo _snd = null;
                foreach (var et in Equipo)
                {
                    Found = false;
                    foreach (var ee in EquipoAsignado)
                    {
                        if (ee.eqtra_Id == et.eqtra_Id)
                        {
                            Found = true;
                        }
                    }

                    if(!Found)
                    {
                        _snd = new tbEquipoTrabajo();
                        _snd.eqtra_Id = et.eqtra_Id;
                        _snd.eqtra_Descripcion = et.eqtra_Descripcion;

                        Send.Add(_snd);
                    }
                }

                return Json(Send, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChildRowData(int? id)
        {
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var lista = db.V_EquipoTrabajoDetalles.Select(tabla =>
                        new
                        {
                            eqem_Id = tabla.eqem_Id,
                            emp_Id = tabla.emp_Id,
                            eqtra_Id = tabla.eqtra_Id,
                            eqtra_Codigo = tabla.eqtra_Codigo,
                            eqtra_Descripcion = tabla.eqtra_Descripcion,
                            eqtra_Observacion = tabla.eqtra_Observacion,
                            eqem_Fecha = tabla.eqem_Fecha,
                            eqem_Estado = tabla.eqem_Estado
                        }).Where(x => x.emp_Id == id && x.eqem_Estado == true).ToList();
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
        [SessionManager("EquipoEmpleados/Create")]
        public ActionResult Create(tbEquipoEmpleados tbEquipoEmpleados)
        {
            string msj = "";
            if (tbEquipoEmpleados.eqtra_Id != 0)
            {                
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbEquipoEmpleados_Insert(tbEquipoEmpleados.emp_Id, tbEquipoEmpleados.eqtra_Id, tbEquipoEmpleados.eqem_Fecha, (int)Session["UserLogin"], Fuction.DatetimeNow());
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

        // POST: EquipoEmpleados/Edit/5
        [HttpPost]
        [SessionManager("EquipoEmpleados/Edit")]
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
        [SessionManager("EquipoEmpleados/Delete")]
        public ActionResult Delete(tbEquipoEmpleados tbEquipoEmpleados)
        {
            string msj = "";

            if (tbEquipoEmpleados.eqem_Id != 0)
            {                
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var Restore = db.tbEquipoEmpleados.Where(x => x.eqem_Id == tbEquipoEmpleados.eqem_Id).ToList().First();
                    var list = db.UDP_RRHH_tbEquipoEmpleados_Delete(tbEquipoEmpleados.eqem_Id, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbEquipoEmpleados_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    var list2 = db.UDP_RRHH_tbEquipoTrabajo_Restore(Restore.eqtra_Id, (int)Session["UserLogin"], Fuction.DatetimeNow());
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
    }
}
