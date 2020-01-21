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
					//Declaración y validación del numero de lotes
					int numeroLotes = (CantidadRegistros <= 1) ? 1 :
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
							return Json("Ha ocurrido un error durante la inserción", JsonRequestBehavior.AllowGet);

						if (i % numeroLotes == 0)
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
						return Json("Ha ocurrido un error durante la inserción", JsonRequestBehavior.AllowGet);
					}

					return Json("Ha ocurrido un error durante la inserción", JsonRequestBehavior.AllowGet);
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
					DateTime hipa_FechaInicio2 = Convert.ToDateTime((hipa_FechaInicio - 1) + "/06" + "/01");
					DateTime hipa_FechaFin = Convert.ToDateTime(hipa_FechaInicio + "/06" + "/30");

					var ConsultaFechas = from HP in db.tbHistorialDePago
										 join P in db.tbPersonas on HP.emp_Id equals P.per_Id
										 join E in db.tbEmpleados on P.per_Id equals E.emp_Id
										 join C in db.tbCargos on E.car_Id equals C.car_Id
										 join CP in db.tbCatalogoDePlanillas on E.cpla_IdPlanilla equals CP.cpla_IdPlanilla
										 where

										 (HP.hipa_FechaPago >= hipa_FechaInicio2 &&
										  HP.hipa_FechaPago <= hipa_FechaFin) &&

										  

										 CP.cpla_IdPlanilla != 2
										 group HP by new
										 {
											 HP.emp_Id,
											 P.per_Nombres,
											 P.per_Apellidos,
											 C.car_Descripcion,
											 CP.cpla_DescripcionPlanilla,
											 E.emp_CuentaBancaria
										 } into PagoDT
										 select new ViewModelDecimoCuartoMes
										 {
											 emp_Id = PagoDT.Key.emp_Id,
											 per_Nombres = PagoDT.Key.per_Nombres,
											 per_Apellidos = PagoDT.Key.per_Apellidos,
											 car_Descripcion = PagoDT.Key.car_Descripcion,
											 cpla_DescripcionPlanilla = PagoDT.Key.cpla_DescripcionPlanilla,
											 emp_CuentaBancaria = PagoDT.Key.emp_CuentaBancaria,
											 dcm_Monto = (PagoDT.Sum(x => x.hipa_SueldoNeto) / 360 * 30)
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