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
    public class HistorialContratacionesController : Controller
    {
        private ERP_GMEDINAEntities1 db = null;

        // GET: HistorialContrataciones
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialContrataciones = new List<tbHistorialContrataciones> { };
            return View(tbHistorialContrataciones);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities1())
                {
                    var tbHistorialContrataciones = db.tbHistorialContrataciones 
                        .Select(
                        t => new
                        {
                           
                            hcon_Id = t.hcon_Id,
                            Colaborador = t.tbDepartamentos.tbEmpleados
                              .Select(p => p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos),
                            dep_Descripcion = t.tbDepartamentos.depto_Descripcion,
                            area_Descripcion = t.tbDepartamentos.tbAreas.area_Descripcion,
                            car_Descripcion = t.tbDepartamentos.tbCargos.car_Descripcion,
                            scan_Fecha = t.tbSeleccionCandidatos.scan_Fecha,
                            hcon_FechaContratado = t.hcon_FechaContratado
                            

                        }
                        )
                        .ToList();
                    return Json(tbHistorialContrataciones, JsonRequestBehavior.AllowGet);
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
            List<V_HistorialContrataciones> lista = new List<V_HistorialContrataciones> { };
            using (db = new ERP_GMEDINAEntities1())
            {
                try
                {
                    lista = db.V_HistorialContrataciones.Where(x => x.Id== id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
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



