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
        private ERP_GMEDINAEntities db = null;

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
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbHistorialContrataciones = db.V_HistorialContrataciones
                        .Select(
                        t => new
                        {
                           
                            hcon_Id = t.Id,
                            Colaborador = t.Nombre_Completo,
                            dep_Descripcion = t.Departamento,
                            area_Descripcion = t.Area,
                            car_Descripcion = t.Cargo,
                            scan_Fecha = t.Fecha_Seleccion_Candidato,
                            hcon_FechaContratado = t.Fecha_Contrato
                            

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
            using (db = new ERP_GMEDINAEntities())
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



