using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class DecimoTercerMesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new ERP_GMEDINA.Models.Helpers();

        #region GET: INDEX
        // GET: V_DecimoTercerMes
        [SessionManager("DecimoTercerMes/Index")]
        public ActionResult Index()
        {
            return View(db.V_DecimoTercerMes.ToList());
        }
        #endregion

        #region GET: INDEX PAGADOS
        public ActionResult IndexPagado()
        {
            return View(db.V_DecimoTercerMes_Pagados.ToList());
        }
        #endregion

        #region POST: INSERT
        public JsonResult InsertDecimoTercerMes(List<tbDecimoTercerMes> DecimoTercer)
        {
            if (DecimoTercer != null)
            {


                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<object> list = null;
                        string MessageError = "";

                        //se construyen los lotes dependiendo de la cantidad de registros que reciba el controlador
                        if (DecimoTercer.Count == 0)
                            return Json("No hay registros en el objeto", JsonRequestBehavior.AllowGet);
                        int CantidadRegistros = DecimoTercer.Count;
                        //Declaración y validación del Número de lotes
                        int NúmeroLotes = (CantidadRegistros <= 1) ? 1 :
                                          (CantidadRegistros <= 10) ? 5 :
                                          (CantidadRegistros <= 50) ? 10 :
                                          (CantidadRegistros <= 100) ? 20 :
                                          (CantidadRegistros <= 500) ? 50 :
                                          (CantidadRegistros > 500 || CantidadRegistros <= 1000) ? 100 : 0;

                        int i = 0;
                        //Ciclo para insertar los registros.
                        foreach (tbDecimoTercerMes DC in DecimoTercer)
                        {
                            i++;
                            list = db.UDP_Plani_tbDecimoTercerMes_Insert(DC.emp_Id, DC.dtm_Monto);

                            //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                            foreach (UDP_Plani_tbDecimoTercerMes_Insert_Result resultado in list)
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
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
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

					//Consulta LINQ para accesar a los datos solicitados por medio de las fechas recibidas en el controlador.
					var ConsultaFechas = from HP in db.V_DecimoTercerMesFE
										 where
										 (HP.hipa_Anio == hipa_FechaInicio)
										 select new ViewModelDecimoTercerMes
										 {
											 emp_Id = HP.emp_Id,
											 per_Nombres = HP.per_Nombres,
											 per_Apellidos = HP.per_Apellidos,
											 car_Descripcion = HP.car_Descripcion,
											 cpla_DescripcionPlanilla = HP.cpla_DescripcionPlanilla,
											 emp_CuentaBancaria = HP.emp_CuentaBancaria,
											 dtm_Monto = HP.dtm_Monto
										 };
					//La consulta LINQ se almacena en un viewbag y se convierte a list la cual vamos a recorrer con un foreach en la vista.
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
