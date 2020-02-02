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
        // GET: PagoDeCesantiaDetalle
        public async Task<ActionResult> Index()
        {
            Session["PagoCesantiaPreview"] = null;
            //SESION DE ALMACENAMIENTO DEL PREVIEW
            //Session["PagoCesantiaPreview"] = "";
            //RETORNO DE LA LISTA
            //var List = await ObtenerListaDePagoCesantia();

            var model = Task.Run(() =>
            {
                ObtenerListaDePagoCesantia();
            });

            var CesantiaList = db.V_tbPagoDeCesantiaDetalle.ToList();
            return View(CesantiaList);
        }

        // GET: PROYECCIONES
        public ActionResult PagarCesantia()
        {
            if (Session["PagoCesantiaPreview"] == null)
                return RedirectToAction("Index");
            
            //RECUPERAR LA LISTA DEL PREVIEW
            List<V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = (List<V_tbPagoDeCesantiaDetalle>)Session["PagoCesantiaPreview"];

            return View(ModelPagoDeCesantiaDetalleList.ToList());
        }

        /////
        public async void ObtenerListaDePagoCesantia()
        {
            Session["PagoCesantiaPreview"] = null;
            //INICIALIZACION DEL OBJETO TIPO LISTA V_tbPagoDeCesantiaDetalle
            List <V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = new List<V_tbPagoDeCesantiaDetalle>();
            //await Task.Run(() => {
            //FECHA DE LA PETICION
            using (ERP_GMEDINAEntities db2 = new ERP_GMEDINAEntities())
            {
                DateTime FechaPeticion = DateTime.Now;
                //INICIALIZACION DEL OBJETO TIPO V_tbPagoDeCesantiaDetalle_Preview
                var ListEmpleados = db2.V_tbPagoDeCesantiaDetalle_Preview.OrderBy(x => x.NombreCompleto).ToList();
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
                Session["PagoCesantiaPreview"] = ModelPagoDeCesantiaDetalleList.ToList();
            }

            //});
            //return ModelPagoDeCesantiaDetalleList;
        }
    }
}