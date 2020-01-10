using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Rotativa;

namespace ERP_GMEDINA.Controllers
{
    public class FechaPlanillaController : Controller
    {        
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ComprobanteEmpleadoEncabezado(int ID = 1, int anio = 2019)
        {
            var V_Plani_EncabezadoHistorialPlanilla = db.V_Plani_EncabezadoHistorialPlanilla.Where( x => x.cpla_IdPlanilla == ID && (x.hipa_FechaPago.Value).Year == anio);

            foreach (var item in V_Plani_EncabezadoHistorialPlanilla)
            {
                var value = item;
            }

            return View(V_Plani_EncabezadoHistorialPlanilla);
            
        }

        [HttpGet]
        public JsonResult getFechaPlanilla()
        {
            var oV_Plani_FechaPlani = db.V_Plani_AnioPlanilla
            .Select(x => new FechaPlanilla{ Anio = x.hipa_Anio }).ToList();


            object json = new { data = oV_Plani_FechaPlani };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getTipoPlanilla(int anio)
        {

            var tbHistorialPlanilla = db.V_Plani_DesplegableHistorialPlanilla
               .Where(x => x.fecha == anio)
               .Select(x => new FechaPlanillaViewModel { DescripcionPlanilla = x.cpla_DescripcionPlanilla, idPlanilla = x.cpla_IdPlanilla, anioPlanilla = (x.fecha ??0) });


            object json = new { data = tbHistorialPlanilla };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Report()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DetailsEmpleadoEncablezado(int id)
		{
			//string NombrePdf = db.tbCatalogoDeDeducciones.Where(x => x.cde_IdDeducciones == cde_IdDeducciones).Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();
			string NombrePdf = "comprobante-de-pago";
			return new ActionAsPdf("Report")
			{
				FileName = $"{NombrePdf}.pdf"
			};

		}
	}
}