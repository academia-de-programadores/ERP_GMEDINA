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
    public class HistorialHorasTrabajadasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: HistorialHorasTrabajadas
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialHorasTrabajadas = new List<tbHistorialHorasTrabajadas> { };
            return View(tbHistorialHorasTrabajadas);
        }
        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbHistorialHoras = db.V_HistorialHorasTrabajadas
                        .Select(
                        t => new
                        {
                            htra_Id = t.Id,
                            Colaborador = t.Nombre_Completo,
                           
                            tiho_Recargo = t.Recargo,
                            tiho_Descripcion = t.Tipo_Horas
                        }
                        )
                        .ToList();
                    return Json(tbHistorialHoras, JsonRequestBehavior.AllowGet);
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
            List<V_HistorialHorasTrabajadas> lista = new List<V_HistorialHorasTrabajadas> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_HistorialHorasTrabajadas.Where(x => x.Id == id).ToList();
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
