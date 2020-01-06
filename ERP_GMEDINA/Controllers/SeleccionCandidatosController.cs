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
 
 
            //CARGAR DDL DE SELECCION CANDIDATOS
            ViewBag.fare_Id = new SelectList(db.tbFasesReclutamiento, "fare_Id", "fare_Descripcion");
            ViewBag.per_Id = new SelectList(personasddl, "per_Id", "per_descripcion");
            ViewBag.req_Id = new SelectList(db.tbRequisiciones, "req_Id", "req_Descripcion");

            //CARGAR DDL DE EMPLEADOS
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion");
            ViewBag.area_Id = new SelectList(db.tbAreas, "area_Id", "area_Descripcion");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.jor_Id = new SelectList(db.tbJornadas, "jor_Id", "jor_Descripcion");
            ViewBag.cpla_IdPlanilla = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            ViewBag.fpa_IdFormaPago = new SelectList(db.tbFormaPago, "fpa_IdFormaPago", "fpa_Descripcion");
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            List<tbSeleccionCandidatos> tbSeleccionCandidatos = new List<tbSeleccionCandidatos> { };
            return View(tbSeleccionCandidatos);
        }

        public ActionResult llenarTabla()
        {

      

            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var candidatos = db.V_SeleccionCandidatos
                        .Select(
                        t => new
                        {
                            Id= t.Id,
                            Identidad = t.Identidad,
                            Nombre = t.Nombre,
                            Fase = t.Fase,
                            Plaza_Solicitada = t.Plaza_Solicitada,
                            Fecha = t.Fecha,
                            Estado = t.Estado
                        }).Where(x => x.Estado).ToList();
                    return Json(candidatos, JsonRequestBehavior.AllowGet);
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
                tbPersonas = new tbPersonas { per_Identidad = IsNull(tbSeleccionCandidatos.tbPersonas).per_Identidad, per_Nombres = IsNull(tbSeleccionCandidatos.tbPersonas).per_Nombres , per_Apellidos = IsNull(tbSeleccionCandidatos.tbPersonas).per_Apellidos},
                fare_Id = tbSeleccionCandidatos.fare_Id ,
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
                    var list = db.UDP_RRHH_tbSeleccionCandidatos_Update(id, tbSeleccionCandidatos.fare_Id, tbSeleccionCandidatos.scan_Fecha,tbSeleccionCandidatos.req_Id, usuario.usu_Id, DateTime.Now);
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
            var SeleccionCandidatos = new tbSeleccionCandidatos
            {
                per_Id = tbSeleccionCandidatos.per_Id,



            };
            Session["per_id"] = SeleccionCandidatos.per_Id;
            return Json(SeleccionCandidatos, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Contratar(tbEmpleados tbEmpleados)
        {
            string msj = "";
            if (tbEmpleados.car_Id != 0)
            {
                int scan_id = (int)Session["scan_id"];
                int per_id = (int)Session["per_id"];
                var usuario = (tbUsuario)Session["Usuario"];

                var empleados = db.tbEmpleados.Where(x => x.per_Id == per_id)
               .Select(
               t => new
               {
                   per_Id = t.per_Id
               }).ToList();

                try
                {
                    if (empleados.Count == 0)
                    {
                        var list = db.UDP_RRHH_tbEmpleados_Insert(scan_id, per_id, tbEmpleados.car_Id, tbEmpleados.area_Id, tbEmpleados.depto_Id,
                        tbEmpleados.jor_Id, tbEmpleados.cpla_IdPlanilla, tbEmpleados.fpa_IdFormaPago,
                        tbEmpleados.emp_CuentaBancaria, false, tbEmpleados.emp_Fechaingreso, usuario.usu_Id, DateTime.Now);
                        foreach (UDP_RRHH_tbEmpleados_Insert_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                    }
                    else
                    {
                        var list = db.UDP_RRHH_tbEmpleados_Recontratacion(scan_id, per_id, tbEmpleados.car_Id, tbEmpleados.area_Id, tbEmpleados.depto_Id,
                        tbEmpleados.jor_Id, tbEmpleados.cpla_IdPlanilla, tbEmpleados.fpa_IdFormaPago,
                        tbEmpleados.emp_CuentaBancaria, true, tbEmpleados.emp_Fechaingreso, usuario.usu_Id, DateTime.Now);

                        foreach (UDP_RRHH_tbEmpleados_Recontratacion_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                    }

                    Session.Remove("id");
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
