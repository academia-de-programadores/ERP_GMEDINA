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
        private ERP_GMEDINAEntities1 db = null;

        // GET: HistorialCargos
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialCargos = new List<tbHistorialCargos> { };
            return View(tbHistorialCargos);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities1())
                {
                    var HistorialCargos = db.tbHistorialCargos
                        .Select(
                        t => new
                        {
                            hcar_Id = t.hcar_Id,
                            Encargado = t.tbCargos.tbEmpleados
                                .Select(p => p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos),
                            car_Anterior = t.tbCargos.car_Descripcion,
                            car_Nuevo = t.tbCargos1.car_Descripcion,
                            hcar_Fecha = t.hcar_Fecha

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
            using (db = new ERP_GMEDINAEntities1())
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
