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
    public class DecimoTercerMesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: V_DecimoTercerMes
        public ActionResult Index()
        {
            return View(db.V_DecimoTercerMes.ToList());
        }

		// GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
		public ActionResult GetData()
		{
			//SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
			//SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
			//DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
			var V_DecimoTercerMes = db.V_DecimoTercerMes
						.Select(c => new { emp_id = c.emp_Id, per_Nombres = c.per_Nombres, per_Apellidos = c.per_Apellidos, car_Descripcion = c.car_Descripcion, cpla_DescripcionPlanilla = c.cpla_DescripcionPlanilla, DecimoTercerMes = c.DecimoTercerMes})
						.ToList();
			//RETORNAR JSON AL LADO DEL CLIENTE
			return new JsonResult { Data = V_DecimoTercerMes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}

		public JsonResult InsertDecimoTercerMes(List<tbDecimoTercerMes> DecimoTercer)

		{
			using (ERP_GMEDINAEntities entities = new ERP_GMEDINAEntities())
			{                
				//Corroborar si la lista viene nula.
				if (DecimoTercer == null)
				{
					DecimoTercer = new List<tbDecimoTercerMes>();
				}
				//Ciclo para insertar los registros.
				foreach (tbDecimoTercerMes DC in DecimoTercer)
				{
                    entities.UDP_Plani_tbDecimoTercerMes_Insert(DC.emp_Id,DC.dtm_DecimoTercer);			
				}
				int RegistrosInsertados = entities.SaveChanges();
				return Json(RegistrosInsertados);
			}
		}

		[HttpPost]
		public ActionResult FechaEspecifica(DateTime? hipa_FechaInicio, DateTime? hipa_FechaFin)
        {
			if (ModelState.IsValid)
			{
			
			try
			{
				
				var ConsultaFechas = from HP in db.tbHistorialDePago
									 join P in db.tbPersonas on HP.emp_Id equals P.per_Id
									 join E in db.tbEmpleados on P.per_Id equals E.emp_Id
									 join C in db.tbCargos on E.car_Id equals C.car_Id
									 join CP in db.tbCatalogoDePlanillas on E.cpla_IdPlanilla equals CP.cpla_IdPlanilla
									 where
									 HP.hipa_FechaInicio == hipa_FechaInicio &&
									 HP.hipa_FechaFin == hipa_FechaFin && CP.cpla_IdPlanilla != 1
									 group HP by new
									 {
										 HP.emp_Id,
										 P.per_Nombres,
										 P.per_Apellidos,
										 C.car_Descripcion,
										 CP.cpla_DescripcionPlanilla,
										 E.emp_CuentaBancaria
									 } into PagoDT
									 select new ViewModelDecimoTercerMes
									 {
										 emp_Id = PagoDT.Key.emp_Id,
										 per_Nombres = PagoDT.Key.per_Nombres,
										 per_Apellidos = PagoDT.Key.per_Apellidos,
										 car_Descripcion = PagoDT.Key.car_Descripcion,
										 cpla_DescripcionPlanilla = PagoDT.Key.cpla_DescripcionPlanilla,
										 emp_CuentaBancaria = PagoDT.Key.emp_CuentaBancaria,
										 DecimoTercerMes = (PagoDT.Sum(x => x.hipa_SueldoNeto) / 360 * 30)
									 };
				ViewBag.ConsultasFechas = ConsultaFechas.ToList();
			}
			catch (Exception ex)
			{
				ex.Message.ToString();                
			}
			}
			return View();
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
