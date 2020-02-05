using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{

    public class PlanillaImpuestoVecinalController : Controller
    {
        //INSTANCIA DEL MODELO
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region GET: Index
        // GET: PlanillaImpuestoVecinal
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region POST: PROCESAR CÁLCULO DE IMPUESTO VECINAL
        public JsonResult ProcesarPlanillaImpuestoVecinal()
        {
            //VARIABLE DE RESPUESTA PARA LAS EJECUCIONES
            string response = "bien";
            //OBJETO DE RETORNO DE LA DATA Y LA RESPUESTA
            object ObjResponde = new {  response = response, data = string.Empty };

            return Json(ObjResponde, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: GENERAR LA PLANILLA DE IMPUESTO VECINAL
        public JsonResult GenerarPlanillaImpV()
        {
            string response = "bien";
            if (1 == 1)
                response = "error";

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: INFORMACIÓN DEL CÁLCULO
        [HttpGet]
        // GET: data para refrescar datatable
        public ActionResult GetDataCalculo()
        {
            var otbCalculoImpuestoVecinal = db.tbDeduccionImpuestoVecinal
                        .Select(c => new {
                            dimv_Id = c.dimv_Id,
                            per_Nombre = c.tbEmpleados.tbPersonas.per_Nombres + ' ' + c.tbEmpleados.tbPersonas.per_Apellidos,
                            dimv_MontoTotal = c.dimv_MontoTotal,
                            dimv_CuotaAPagar = c.dimv_CuotaAPagar,
                        }).OrderByDescending(c => c.dimv_Id)
                        .ToList();

            // retornar data
            return new JsonResult { Data = otbCalculoImpuestoVecinal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

    }
}