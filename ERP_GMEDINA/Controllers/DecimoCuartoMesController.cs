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

        #region GET: DATATABLE
        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var V_DecimoCuartoMes = db.V_DecimoCuartoMes
                        .Select(c => new { emp_id = c.emp_Id, per_Nombres = c.Nombre, per_Apellidos = c.Apellido, car_Descripcion = c.Cargo, cpla_DescripcionPlanilla = c.Planilla, Monto = c.Monto })
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = V_DecimoCuartoMes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: INSERT 
            public JsonResult InsertDecimoCuartoMes(List<tbDecimoCuartoMes> DecimoCuarto)
            {
                using (ERP_GMEDINAEntities entities = new ERP_GMEDINAEntities())
                {
                    try
                    {
                    IEnumerable<object> listFormaPago = null;
                    string MessageError = "";
                    if (DecimoCuarto.Count == 0)
                            return Json("No hay registros en el objeto", JsonRequestBehavior.AllowGet);
                        int CantidadRegistros = DecimoCuarto.Count;
                        //Declaración y validación del numero de lotes
                        int numeroLotes = (CantidadRegistros <= 1)   ?  1 :
                                          (CantidadRegistros <= 10)  ?  5 :
                                          (CantidadRegistros <= 50)  ? 10 :
                                          (CantidadRegistros <= 100) ? 20 :
                                          (CantidadRegistros <= 500) ? 50 :
                                          (CantidadRegistros >  500 || CantidadRegistros <= 1000) ? 100 : 0 ;
                        int i = 0;
                        //Ciclo para insertar los registros.
                        foreach (tbDecimoCuartoMes DC in DecimoCuarto)
                        {
                            i++;
                            listFormaPago = entities.UDP_Plani_tbDecimoCuartoMes_Insert(DC.emp_Id, DC.dcm_Monto);

                            //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP                                                  
                            foreach (UDP_Plani_tbDecimoCuartoMes_Insert_Result resultado in listFormaPago)
                                MessageError = Convert.ToString(resultado);

                            if (MessageError.StartsWith("-1"))
                                return Json("Ha ocurrido un error durante la inserción", JsonRequestBehavior.AllowGet);

                            if (i % numeroLotes == 0)
                                    entities.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    int RegistrosInsertados = entities.SaveChanges();
                    return Json(RegistrosInsertados, JsonRequestBehavior.AllowGet);
                }
            }
        #endregion

        #region POST: FECHAS POR ESPECIFICACIÓN
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
                                             (HP.hipa_FechaInicio >= hipa_FechaInicio &&
                                              HP.hipa_FechaInicio <= hipa_FechaFin) &&
                                             (HP.hipa_FechaFin >= hipa_FechaInicio &&
                                              HP.hipa_FechaFin <= hipa_FechaFin) &&
                                             CP.cpla_IdPlanilla != 1
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