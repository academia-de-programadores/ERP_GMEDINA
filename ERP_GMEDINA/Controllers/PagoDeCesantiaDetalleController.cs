using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Helpers;
using System.Threading.Tasks;

namespace ERP_GMEDINA.Controllers
{
    public class PagoDeCesantiaDetalleController : Controller
    {
        //INSTANCIA DEL MODELO
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region GET: INDEX DE CESANTIA PAGADA
        public ActionResult Index()
        {
            //var model = Task.Run(() =>
            //{
            //    ObtenerListaDePagoCesantia();
            //});
            List<V_tbPagoDeCesantiaDetalle> CesantiaList = null;
            try
            {
                CesantiaList = db.V_tbPagoDeCesantiaDetalle.ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return View(CesantiaList);
        }
        #endregion

        #region GET: LISTADO DEL PREVIEW DE GENERAR LA PLANILLA DE CESANTIA
        [HttpGet]
        public JsonResult ObtenerListaDePagoCesantia()
        {
            //INICIALIZACION DEL OBJETO TIPO LISTA V_tbPagoDeCesantiaDetalle
            List <V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = new List<V_tbPagoDeCesantiaDetalle>();

            //FECHA DE LA PETICION
            DateTime FechaPeticion = DateTime.Now;
            //INICIALIZACION DEL OBJETO TIPO V_tbPagoDeCesantiaDetalle_Preview
            var ListEmpleados = db.V_tbPagoDeCesantiaDetalle_Preview.OrderBy(x => x.NombreCompleto).ToList();
            //Iterador
            int iter = 1;
            foreach (V_tbPagoDeCesantiaDetalle_Preview item in ListEmpleados)
            {
                //INICIALIZACION DEL OBJETO TIPO V_tbPagoDeCesantiaDetalle
                V_tbPagoDeCesantiaDetalle ModelPagoDeCesantiaDetalle = new V_tbPagoDeCesantiaDetalle();
                //SETEAR LOS CAMPOS PARA MOSTRAR LA PROYECCIÓN 
                ModelPagoDeCesantiaDetalle.IdCesantia = iter;
                ModelPagoDeCesantiaDetalle.NoIdentidad = item.NoIdentidad;
                ModelPagoDeCesantiaDetalle.NombreCompleto = item.NombreCompleto;
                ModelPagoDeCesantiaDetalle.DiasPagados = (int)Liquidacion.Dias360AcumuladosCesantia(item.emp_Id, FechaPeticion);
                ModelPagoDeCesantiaDetalle.ConSueldo = Liquidacion.Calculo_SalarioBrutoMasAlto(item.emp_Id);
                ModelPagoDeCesantiaDetalle.TotalCesantiaColaborador = (ModelPagoDeCesantiaDetalle.ConSueldo / 30) * ModelPagoDeCesantiaDetalle.DiasPagados;
                ModelPagoDeCesantiaDetalle.NoDeCuenta = item.NoDeCuenta;
                ModelPagoDeCesantiaDetalleList.Add(ModelPagoDeCesantiaDetalle);
                iter++;
            }
            return Json(ModelPagoDeCesantiaDetalleList, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}