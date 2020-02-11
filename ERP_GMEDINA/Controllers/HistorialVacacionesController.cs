using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class HistorialVacacionesController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: HistorialVacaciones
        [SessionManager("HistorialVacaciones/Index")]
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbHistorialVacaciones tbHistorialVacaciones = new tbHistorialVacaciones { hvac_Estado = true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                return View(tbHistorialVacaciones);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return View(tbHistorialVacaciones);
        }
        [SessionManager("HistorialVacaciones/Index")]
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_Historialvacaciones> lista = new List<V_Historialvacaciones> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Historialvacaciones.Where(x => x.emp_Id == id).ToList();
                    if (lista == null)
                    {
                        lista.Add(new V_Historialvacaciones { });
                    }
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }




        // GET: HistorialAmonestaciones/Create
        [HttpPost]
        [SessionManager("HistorialVacaciones/Create")]
        public JsonResult Create(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string msj = "";
            using (db = new ERP_GMEDINAEntities())
                try
                {
                    var list = db.UDP_RRHH_tbHistorialVacaciones_Insert(tbHistorialVacaciones.emp_Id,
                                                                            tbHistorialVacaciones.hvac_FechaInicio,
                                                                            tbHistorialVacaciones.hvac_FechaFin,
                                                                            (int)Session["UserLogin"],
                                                                            Function.DatetimeNow());
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

        [HttpPost]
        [SessionManager("HistorialVacaciones/Delete")]
        public ActionResult Delete(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string msj = "";
            using (db = new ERP_GMEDINAEntities())
                if (tbHistorialVacaciones.hvac_Id != 0 && tbHistorialVacaciones.hvac_RazonInactivo != "")
                {
                    var Usuario = (tbUsuario)Session["Usuario"];
                    try
                    {
                        var list = db.UDP_RRHH_tbHistorialVacaciones_Delete(tbHistorialVacaciones.hvac_Id,
                                                                            tbHistorialVacaciones.hvac_RazonInactivo,
                                                                            (int)Session["UserLogin"],
                                                                            Function.DatetimeNow());
                        foreach (UDP_RRHH_tbHistorialVacaciones_Delete_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                    }
                    catch (Exception ex)
                    {
                        msj = "-2";
                        ex.Message.ToString();
                    }
                    Session.Remove("id");
                }
                else
                {
                    msj = "-3";
                }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        [SessionManager("HistorialVacaciones/Edit")]
        public ActionResult Edit(int? id)
        {
            using (db = new ERP_GMEDINAEntities())
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            List<tbHistorialVacaciones> tbHistorialVacaciones = null;
            try
            {
                tbHistorialVacaciones = new List<Models.tbHistorialVacaciones> { };
                tbHistorialVacaciones = db.tbHistorialVacaciones.Where(x => x.emp_Id == id).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = id;
            var vacaciones = new tbHistorialVacaciones();
            foreach (var item in tbHistorialVacaciones)
            {
                vacaciones = new tbHistorialVacaciones
                {
                    hvac_Id = item.hvac_Id,
                    hvac_FechaInicio = item.hvac_FechaInicio,
                    hvac_FechaFin = item.hvac_FechaFin,
                    hvac_CantDias = item.hvac_CantDias,
                    hvac_DiasPagados = item.hvac_DiasPagados,
                    hvac_MesVacaciones = item.hvac_MesVacaciones,
                    hvac_AnioVacaciones = item.hvac_AnioVacaciones,
                    hvac_Estado = item.hvac_Estado,
                    hvac_RazonInactivo = item.hvac_RazonInactivo,
                    hvac_UsuarioCrea = item.hvac_UsuarioCrea,
                    hvac_FechaCrea = item.hvac_FechaCrea,
                    hvac_FechaModifica = item.hvac_FechaModifica,
                    //tbUsuario = item.tbUsuario,
                    //tbUsuario1 = item.tbUsuario1
                };

            }
            return Json(vacaciones, JsonRequestBehavior.AllowGet);
        }
        
        [SessionManager("HistorialVacaciones/Detalles")]
        public ActionResult Detalles(int? id)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbHistorialVacaciones = db.tbHistorialVacaciones
                        .Select(
                        p => new
                        {
                            hvac_Id = p.hvac_Id,
                            hvac_FechaInicio = p.hvac_FechaInicio,
                            hvac_FechaFin = p.hvac_FechaFin,
                            hvac_CantDias = p.hvac_CantDias,
                            hvac_DiasPagados = p.hvac_DiasPagados,
                            hvac_MesVacaciones = p.hvac_MesVacaciones,
                            hvac_AnioVacaciones = p.hvac_AnioVacaciones,
                            hvac_Estado = p.hvac_Estado,
                            hvac_RazonInactivo = p.hvac_RazonInactivo,
                            hvac_FechaCrea = p.hvac_FechaCrea,
                            hvac_FechaModifica = p.hvac_FechaModifica,
                            hvac_UsuarioCrea = p.tbUsuario.usu_Nombres,
                            hvac_UsuarioModifica = p.tbUsuario1.usu_Nombres
                        })
                        .Where(x => x.hvac_Id == id).ToList();
                    return Json(tbHistorialVacaciones, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DiasRestantes(int? id, int? annio)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbHistorialVacaciones = db.V_HVacacionesEmpleados
                        .Select(
                        p => new
                        {
                            hvac_Id = p.emp_Id,
                            hvac_DiasRestantes = p.DiasTotales,
                            hvac_Annio = p.Annio

                        })
                        .Where(x => x.hvac_Id == id).Where(x => x.hvac_Annio == annio).ToList();
                    return Json(tbHistorialVacaciones, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }


        
        [HttpPost]
        [SessionManager("HistorialVacaciones/Index")]
        public JsonResult llenarTabla()
        {
            using (db = new ERP_GMEDINAEntities())
                try
                {
                    var Empleados = db.V_HVacacionesEmpleados
                            .Select(
                            t => new
                            {
                                emp_Id = t.emp_Id,
                                Empleado = t.emp_NombreCompleto,
                                Cargo = t.car_Descripcion,
                                Departamento = t.depto_Descripcion,
                                FechaContratacion = t.emp_Fechaingreso,
                                DiasTotales = t.DiasMax,
                                DiasTomados = t.DiasTomados,
                                DiasRestantes = t.DiasTotales,
                                Año = t.Annio
                            }).ToList();


                    return Json(Empleados, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
        }
        [HttpPost]
        [SessionManager("HistorialVacaciones/habilitar")]
        public JsonResult habilitar(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbHistorialVacaciones_Restore(tbHistorialVacaciones.hvac_Id,
                                                                         (int)Session["UserLogin"],
                                                                         Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialVacaciones_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}