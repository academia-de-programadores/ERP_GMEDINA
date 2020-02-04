using ERP_GMEDINA.Helpers;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
	public class DecimoCuartoMesController : Controller
	{
		private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

		#region GET: INDEX
		// GET: DecimoCuartoMes
		public ActionResult Index()
		{
			return View(db.V_DecimoCuartoMes.ToList());
		}
		#endregion

		#region GET: INDEX PAGADOS
		public ActionResult IndexPagado()
		{
			return View(db.V_DecimoCuartoMes_Pagados.ToList());
		}
		#endregion

		#region POST: INSERT
		public JsonResult InsertDecimoCuartoMes(List<tbDecimoCuartoMes> DecimoCuarto)
		{
			using (var dbContextTransaction = db.Database.BeginTransaction())
			{
				try
				{
					IEnumerable<object> list = null;
					string MessageError = "";

					//se construyen los lotes dependiendo de la cantidad de registros que reciba el controlador
					if (DecimoCuarto.Count == 0)
						return Json("No hay registros en el objeto", JsonRequestBehavior.AllowGet);
					int CantidadRegistros = DecimoCuarto.Count;
					//Declaración y validación del Número de lotes
					int NúmeroLotes = (CantidadRegistros <= 1) ? 1 :
									  (CantidadRegistros <= 10) ? 5 :
									  (CantidadRegistros <= 50) ? 10 :
									  (CantidadRegistros <= 100) ? 20 :
									  (CantidadRegistros <= 500) ? 50 :
									  (CantidadRegistros > 500 || CantidadRegistros <= 1000) ? 100 : 0;

					int i = 0;
					//Ciclo para insertar los registros.
					foreach (tbDecimoCuartoMes DC in DecimoCuarto)
					{
						i++;
						list = db.UDP_Plani_tbDecimoCuartoMes_Insert(DC.emp_Id, DC.dcm_Monto);

						//RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
						foreach (UDP_Plani_tbDecimoCuartoMes_Insert_Result resultado in list)
							MessageError = Convert.ToString(resultado);

						if (MessageError.StartsWith("-1"))
							return Json("-1", JsonRequestBehavior.AllowGet);

						if (i % NúmeroLotes == 0)
							db.SaveChanges();
					}

					dbContextTransaction.Commit();
				}
				catch
				{
					try
					{
						dbContextTransaction.Rollback();
					}
					catch (System.Data.Entity.Core.EntityException)
					{
						return Json("-1", JsonRequestBehavior.AllowGet);
					}

					return Json("-1", JsonRequestBehavior.AllowGet);
				}

			}

			int RegistrosInsertados = db.SaveChanges();
			return Json(RegistrosInsertados);
		}
        #endregion

        #region POST: FECHAS POR ESPECIFICACIÓN
        [HttpPost]
		public ActionResult FechaEspecifica(int? hipa_FechaInicio)
		{
			if (ModelState.IsValid)
			{
				try
				{


					var ConsultaFechas = from HP in db.V_DecimoCuartoMesFE

										 where
										 (HP.hipa_Anio == hipa_FechaInicio)
										 select new ViewModelDecimoCuartoMes
										 {
											 emp_Id = HP.emp_Id,
											 per_Nombres = HP.Nombre,
											 per_Apellidos = HP.Apellido,
											 car_Descripcion = HP.Cargo,
											 cpla_DescripcionPlanilla = HP.Planilla,
											 emp_CuentaBancaria = HP.CuentaBancaria,
											 dcm_Monto = HP.Monto
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
		#endregion

		#region DISPOSE
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

	}
}
