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

        public JsonResult Create(tbEmpleados tbEmpleados)
        {
            string msj = "";
            try
            {
                var list = db.UDP_RRHH_tbHistorialCargos_Insert(tbEmpleados.emp_Id,
                                                                tbEmpleados.car_Id,
                                                                tbEmpleados.area_Id,
                                                                tbEmpleados.depto_Id,
                                                                tbEmpleados.jor_Id,
                                                                Convert.ToDecimal(tbEmpleados.emp_CuentaBancaria),
                                                                tbEmpleados.emp_Fechaingreso,
                                                                1,
                                                                DateTime.Now);
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
