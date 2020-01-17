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
    public class HistorialCargosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialCargos
        public ActionResult Index()
        {


            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialCargos = new List<tbHistorialCargos> { };

            var empleadosddl = db.tbEmpleados.Where(x => x.emp_Estado).Include(t => t.tbPersonas)
                .Select(
                t => new
                {
                    emp_Id = t.emp_Id,
                    emp_descripcion = t.tbPersonas.per_Identidad + " - " + t.tbPersonas.per_Nombres + " " + t.tbPersonas.per_Apellidos
                }).ToList();




            //CARGAR DDL DE EMPLEADOS
                    ViewBag.emp_Id = new SelectList(empleadosddl, "emp_Id", "emp_descripcion");
                    ViewBag.car_Id = new SelectList(db.tbCargos.Where(x => x.car_Estado), "car_Id", "car_Descripcion");
                    ViewBag.area_Id = new SelectList(db.tbAreas.Where(x => x.area_Estado), "area_Id", "area_Descripcion");
                    ViewBag.depto_Id = new SelectList(db.tbDepartamentos.Where(x => x.depto_Estado), "depto_Id", "depto_Descripcion");
                    ViewBag.jor_Id = new SelectList(db.tbJornadas.Where(x => x.jor_Estado), "jor_Id", "jor_Descripcion");


            return View(tbHistorialCargos);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var HistorialCargos = db.V_HistorialCargos
                        .Select(
                        t => new
                        {
                            hcar_Id = t.hcar_Id,
                            Encargado = t.Nombre_Completo,
                            car_Anterior = t.CargoAnterior,
                            car_Nuevo = t.CargoNuevo,
                            hcar_Fecha = t.Fecha_de_Historial

                        }
                        )
                        .ToList();






                    return Json(HistorialCargos, JsonRequestBehavior.AllowGet);
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
            List<V_HistorialCargos> lista = new List<V_HistorialCargos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_HistorialCargos.Where(x => x.hcar_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
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

        public ActionResult Promover()
        {

            var Empleado = new tbEmpleados();


            var Empleadosddl = db.tbEmpleados.Where(x => x.emp_Estado).Include(t => t.tbPersonas)
            .Select(
            t => new
            {
                emp_Id = t.per_Id,
                emp_descripcion = t.tbPersonas.per_Identidad + " - " + t.tbPersonas.per_Nombres + " " + t.tbPersonas.per_Apellidos
            }).ToList();


            //CARGAR DDL DE EMPLEADOS
            ViewBag.emp_Id = new SelectList(Empleadosddl, "emp_Id", "emp_descripcion");
            ViewBag.car_Id = new SelectList(db.tbCargos.Where(x => x.car_Estado), "car_Id", "car_Descripcion");
            ViewBag.area_Id = new SelectList(db.tbAreas.Where(x => x.area_Estado), "area_Id", "area_Descripcion");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos.Where(x => x.depto_Estado), "depto_Id", "depto_Descripcion");
            ViewBag.jor_Id = new SelectList(db.tbJornadas.Where(x => x.jor_Estado), "jor_Id", "jor_Descripcion");
            ViewBag.cpla_IdPlanilla = new SelectList(db.tbCatalogoDePlanillas.Where(x => x.cpla_Activo), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            ViewBag.fpa_IdFormaPago = new SelectList(db.tbFormaPago.Where(x => x.fpa_Activo), "fpa_IdFormaPago", "fpa_Descripcion");

            return View(Empleado);
        }


        public JsonResult PromoverGuardar(tbEmpleados tbEmpleados, tbSueldos tbSueldos, tbRequisiciones tbRequisiciones)
        {
            string msj = "";
            if (tbEmpleados.car_Id != 0)
            {
                var usuario = (tbUsuario)Session["Usuario"];


                try
                {
                        var list = db.UDP_RRHH_tbHistorialCargos_Insert(tbEmpleados.emp_Id, tbEmpleados.car_Id, tbEmpleados.area_Id, tbEmpleados.depto_Id,
                        tbEmpleados.jor_Id, tbSueldos.sue_Cantidad, tbEmpleados.emp_Fechaingreso, tbRequisiciones.req_Id, 1, DateTime.Now);
                        foreach (UDP_RRHH_tbHistorialCargos_Insert_Result item in list)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && db !=null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
