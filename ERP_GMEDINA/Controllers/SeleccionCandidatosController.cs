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
    public class SeleccionCandidatosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        public ActionResult Index()
        {

            var candidatosddl = db.V_SeleccionCandidatos.Where(x => x.Estado)
            .Select(
            t => new
            {
                per_Id = t.IdPersona,
                per_descripcion = t.Identidad + " - " + t.Nombre
            }).ToList();

            var empleadosddl = db.tbEmpleados.Where(x => x.emp_Estado).Include(t => t.tbPersonas)
                .Select(
                t => new
                {
                    per_Id = t.per_Id,
                    per_descripcion = t.tbPersonas.per_Identidad + " - " + t.tbPersonas.per_Nombres + " " + t.tbPersonas.per_Apellidos
                }).ToList();



            var personasddl = db.tbPersonas.Where(x => x.per_Estado)
                .Select(
                t => new
                {
                    per_Id = t.per_Id,
                    per_descripcion = t.per_Identidad + " - " + t.per_Nombres + " " + t.per_Apellidos
                }).ToList();

            personasddl = personasddl.Except(candidatosddl).ToList();
            personasddl = personasddl.Except(empleadosddl).ToList();

            var requisicionesddl = db.tbRequisiciones.Where(x => x.req_Estado)
            .Select(
            t => new
            {
                req_Id = t.req_Id,
                req_descripcion = t.req_Descripcion,
                req_Vacantes = t.req_Vacantes,
                req_VacantesOcupadas = t.req_VacantesOcupadas
            }).ToList();



            //CARGAR DDL DE SELECCION CANDIDATOS
            ViewBag.fare_Id = new SelectList(db.tbFasesReclutamiento.Where(x => x.fare_Estado), "fare_Id", "fare_Descripcion");
            ViewBag.per_Id = new SelectList(personasddl, "per_Id", "per_descripcion");
            try
            {
                ViewBag.req_Id = new SelectList(requisicionesddl.Where(x => Convert.ToInt32(x.req_Vacantes) > x.req_VacantesOcupadas), "req_Id", "req_Descripcion");
            }
            catch
            {
                ViewBag.req_Id = new SelectList(db.tbRequisiciones.Where(x => x.req_Estado), "req_Id", "req_Descripcion");
            }


            tbSeleccionCandidatos tbSeleccionCandidatos = new tbSeleccionCandidatos { scan_Estado = true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                return View(tbSeleccionCandidatos);
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
            return View(tbSeleccionCandidatos);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                var candidatos = db.V_SeleccionCandidatos
                    .Select(
                    t => new {
                        per_Id = t.IdPersona,
                        Id = t.Id,
                        Identidad = t.Identidad,
                        Nombre = t.Nombre,
                        Fase = t.Fase,
                        Plaza_Solicitada = t.Plaza_Solicitada,
                        Fecha = t.Fecha,
                        Estado = t.Estado
                    }

                    ).ToList();
                return Json(candidatos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_SeleccionCandidatos> lista = new List<V_SeleccionCandidatos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_SeleccionCandidatos.Where(x => x.Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            string msj = "";
            try
            {
                var list = db.UDP_RRHH_tbSeleccionCandidatos_Insert(tbSeleccionCandidatos.per_Id,
                                                                        tbSeleccionCandidatos.fare_Id,
                                                                        tbSeleccionCandidatos.scan_Fecha,
                                                                        tbSeleccionCandidatos.req_Id,
                                                                        1,
                                                                        DateTime.Now);
                foreach (UDP_RRHH_tbSeleccionCandidatos_Insert_Result item in list)
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSeleccionCandidatos tbSeleccionCandidatos = null;


            try
            {
                tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
                if (tbSeleccionCandidatos == null || !tbSeleccionCandidatos.scan_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = id;
            var SeleccionCandidatos = new tbSeleccionCandidatos
            {
                per_Id = tbSeleccionCandidatos.per_Id,
                tbPersonas = new tbPersonas { per_Identidad = IsNull(tbSeleccionCandidatos.tbPersonas).per_Identidad, per_Nombres = IsNull(tbSeleccionCandidatos.tbPersonas).per_Nombres, per_Apellidos = IsNull(tbSeleccionCandidatos.tbPersonas).per_Apellidos },
                fare_Id = tbSeleccionCandidatos.fare_Id,
                tbFasesReclutamiento = new tbFasesReclutamiento { fare_Descripcion = IsNull(tbSeleccionCandidatos.tbFasesReclutamiento).fare_Descripcion },
                req_Id = tbSeleccionCandidatos.req_Id,
                tbRequisiciones = new tbRequisiciones { req_Descripcion = IsNull(tbSeleccionCandidatos.tbRequisiciones).req_Descripcion },
                scan_Fecha = tbSeleccionCandidatos.scan_Fecha,
                scan_FechaCrea = tbSeleccionCandidatos.scan_FechaCrea,
                scan_FechaModifica = tbSeleccionCandidatos.scan_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbSeleccionCandidatos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbSeleccionCandidatos.tbUsuario1).usu_NombreUsuario }
            };
            return Json(SeleccionCandidatos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            string msj = "";
            if (tbSeleccionCandidatos.fare_Id != 0)
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSeleccionCandidatos_Update(id, tbSeleccionCandidatos.fare_Id, tbSeleccionCandidatos.scan_Fecha, tbSeleccionCandidatos.req_Id, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSeleccionCandidatos_Update_Result item in list)
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

        [HttpPost]
        public ActionResult Delete(tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            string msj = "";
            if (tbSeleccionCandidatos.scan_Id != 0)
            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSeleccionCandidatos_Delete(tbSeleccionCandidatos.scan_Id, tbSeleccionCandidatos.scan_RazonInactivo, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSeleccionCandidatos_Delete_Result item in list)
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


        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }

        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbSeleccionCandidatos_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSeleccionCandidatos_Restore_Result item in list)
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


        protected tbPersonas IsNull(tbPersonas valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbPersonas { per_Identidad = "" };
            }
        }

        protected tbFasesReclutamiento IsNull(tbFasesReclutamiento valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbFasesReclutamiento { fare_Descripcion = "" };
            }
        }

        protected tbRequisiciones IsNull(tbRequisiciones valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbRequisiciones { req_Descripcion = "" };
            }
        }

        //EMPLEADO///////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult Contratar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSeleccionCandidatos tbSeleccionCandidatos = null;
            try
            {
                tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
                if (tbSeleccionCandidatos == null || !tbSeleccionCandidatos.scan_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["scan_id"] = id;

            var candidatos = db.V_SeleccionCandidatos.Where(x => x.Estado)
            .Select(
            t => new
            {
                per_Id = t.IdPersona,
            }).ToList().
            First();

            var Empleado = new tbEmpleados
            {
                per_Id = tbSeleccionCandidatos.per_Id,
            };

            Empleado.per_Id = candidatos.per_Id;
            //CARGAR DDL DE EMPLEADOS
            ViewBag.car_Id = new SelectList(db.tbEmpleados.Where(x => x.emp_Estado), "emp_Id", "car_Descripcion");
            ViewBag.car_Id = new SelectList(db.tbCargos.Where(x => x.car_Estado), "car_Id", "car_Descripcion");
            ViewBag.area_Id = new SelectList(db.tbAreas.Where(x => x.area_Estado), "area_Id", "area_Descripcion");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos.Where(x => x.depto_Estado), "depto_Id", "depto_Descripcion");
            ViewBag.jor_Id = new SelectList(db.tbJornadas.Where(x => x.jor_Estado), "jor_Id", "jor_Descripcion");
            ViewBag.cpla_IdPlanilla = new SelectList(db.tbCatalogoDePlanillas.Where(x => x.cpla_Activo), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            ViewBag.fpa_IdFormaPago = new SelectList(db.tbFormaPago.Where(x => x.fpa_Activo), "fpa_IdFormaPago", "fpa_Descripcion");

            return View(Empleado);
        }

        private static tbSeleccionCandidatos GetTbSeleccionCandidatos(tbSeleccionCandidatos tbSeleccionCandidatos)
        {
            return tbSeleccionCandidatos;
        }

        [HttpPost]
        public JsonResult Contratar(tbSeleccionCandidatos tbSeleccionCandidatos, tbEmpleados tbEmpleados, tbSueldos tbSueldos, tbRequisiciones tbRequisiciones)
        {
            string msj = "";
            if (tbEmpleados.car_Id != 0)
            {
                var usuario = (tbUsuario)Session["Usuario"];

                //Comprueba si el candidato ah sido empleado antes.
                var empleados = db.tbEmpleados.Where(x => x.per_Id == tbEmpleados.per_Id)
               .Select(
               t => new
               {
                   per_Id = t.per_Id
               }).ToList();

                try
                {
                    //Si el candidato nunca ah sido empleado se contratara
                    if (empleados.Count == 0)
                    {
                        var list = db.UDP_RRHH_tbEmpleados_Contratar(tbSeleccionCandidatos.scan_Id, tbEmpleados.car_Id, tbEmpleados.area_Id, tbEmpleados.depto_Id,
                        tbEmpleados.jor_Id, tbEmpleados.cpla_IdPlanilla, tbEmpleados.fpa_IdFormaPago,
                        tbEmpleados.emp_CuentaBancaria, false, tbRequisiciones.req_Id, tbSueldos.tmon_Id, tbSueldos.sue_Cantidad, tbEmpleados.emp_Fechaingreso, 1, DateTime.Now);
                        foreach (UDP_RRHH_tbEmpleados_Contratar_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                    }
                    else
                    {
                        //Si el candidato ah sido empleado se recontratara
                        var list = db.UDP_RRHH_tbEmpleados_Recontratar(tbSeleccionCandidatos.scan_Id, tbEmpleados.car_Id, tbEmpleados.area_Id, tbEmpleados.depto_Id,
                        tbEmpleados.jor_Id, tbEmpleados.cpla_IdPlanilla, tbEmpleados.fpa_IdFormaPago,
                        tbEmpleados.emp_CuentaBancaria, true, tbRequisiciones.req_Id, tbSueldos.tmon_Id, tbSueldos.sue_Cantidad, tbEmpleados.emp_Fechaingreso, 1, DateTime.Now);

                        foreach (UDP_RRHH_tbEmpleados_Recontratar_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                    }

                    //Session.Remove("id");
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
        public ActionResult llenarDropDowlistTipoMonedas()
        {
            var Monedas = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {

                    Monedas.AddRange(db.tbTipoMonedas
                    .Select(tabla => new { Id = tabla.tmon_Id, Descripcion = tabla.tmon_Descripcion, Estado = tabla.tmon_Estado})
                    .Where(x => x.Estado).ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Monedas", Monedas);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult llenarDropDowlistRequisicion()
        {
            var Requisicion = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var requisicionesddl = db.tbRequisiciones.Where(x => x.req_Estado)
                        .Select(
                        t => new
                        {
                            req_Id = t.req_Id,
                            req_Descripcion = t.req_Descripcion,
                            req_Vacantes = t.req_Vacantes,
                            req_VacantesOcupadas = t.req_VacantesOcupadas,
                            req_Estado = t.req_Estado
                        }).ToList();

                    Requisicion.AddRange(requisicionesddl
                    .Select(tabla => new { Id = tabla.req_Id, Descripcion = tabla.req_Descripcion, Estado = tabla.req_Estado, tabla.req_Vacantes, tabla.req_VacantesOcupadas })
                    .Where(x => x.Estado).Where(x => Convert.ToInt32(x.req_Vacantes) > x.req_VacantesOcupadas).ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Requisicion", Requisicion);
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
