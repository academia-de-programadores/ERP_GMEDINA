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
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbHistorialVacaciones tbHistorialVacaciones = new tbHistorialVacaciones { hvac_Estado = true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                // tbtitulos = db.tbTitulos.Where(x => x.titu_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbHistorialVacaciones);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                // tbtitulos.Add(new tbTitulos { titu_Id = 0, titu_Descripcion = "fallo la conexion" });
            }
            return View(tbHistorialVacaciones);
        }

        // GET: HistorialVacaciones/Details/5
        //public ActionResult llenarTabla()
        //{
        //    try
        //    {
        //        using (db = new ERP_GMEDINAEntities())
        //        {
        //            var Empleados = db.V_HVacacionesEmpleados
        //                .Select(
        //                t => new
        //                {
        //                    emp_Id = t.emp_Id,
        //                    Empleado = t.emp_NombreCompleto,
        //                    Cargo = t.car_Descripcion,
        //                    //Departamento = t.depto_Descripcion,
        //                    //FechaContratacion = t.emp_Fechaingreso
        //                }).ToList();



        //            return Json(Empleados, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch
        //    {
        //        return Json("-2", JsonRequestBehavior.AllowGet);
        //    }
        //}
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

        [HttpPost]
        public ActionResult Delete(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string msj = "";
            if (tbHistorialVacaciones.hvac_Id != 0 && tbHistorialVacaciones.hvac_RazonInactivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialVacaciones_Delete(tbHistorialVacaciones.hvac_Id, tbHistorialVacaciones.hvac_RazonInactivo, 1, DateTime.Now);
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
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
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

        //GET: HistorialVacaciones/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbHistorialVacaciones tbHistorialVacaciones = db.tbHistorialVacaciones.Find(id);
        //    if (tbHistorialVacaciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioCrea);
        //    ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioModifica);
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialVacaciones.emp_Id);
        //    return View(tbHistorialVacaciones);
        //}

        // POST: HistorialVacaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "hvac_Id,emp_Id,hvac_FechaInicio,hvac_FechaFin,hvac_CantDias,hvac_DiasPagados,hvac_MesVacaciones,hvac_AnioVacaciones,hvac_Estado,hvac_RazonInactivo,hvac_UsuarioCrea,hvac_FechaCrea,hvac_UsuarioModifica,hvac_FechaModifica")] tbHistorialVacaciones tbHistorialVacaciones)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbHistorialVacaciones).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.hvac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioCrea);
        //    ViewBag.hvac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbHistorialVacaciones.hvac_UsuarioModifica);
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria", tbHistorialVacaciones.emp_Id);
        //    return View(tbHistorialVacaciones);
        //}

        // GET: HistorialVacaciones/Delete/5


        // POST: HistorialVacaciones/Delete/5
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
        public JsonResult llenarTabla()
        {
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
        public JsonResult habilitar(tbHistorialVacaciones tbHistorialVacaciones)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbHistorialVacaciones_Restore(tbHistorialVacaciones.hvac_Id, Usuario.usu_Id, DateTime.Now);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    }