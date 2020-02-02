using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Helpers;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

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

        public ViewResult PagarCesantia()
        {
            //INICIALIZACION DEL OBJETO TIPO LISTA V_tbPagoDeCesantiaDetalle
            List<V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = new List<V_tbPagoDeCesantiaDetalle>();

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
                ModelPagoDeCesantiaDetalle.emp_Id = item.emp_Id;
                ModelPagoDeCesantiaDetalle.NoIdentidad = item.NoIdentidad;
                ModelPagoDeCesantiaDetalle.NombreCompleto = item.NombreCompleto;
                ModelPagoDeCesantiaDetalle.DiasPagados = (int)Liquidacion.Dias360AcumuladosCesantia(item.emp_Id, FechaPeticion);
                ModelPagoDeCesantiaDetalle.ConSueldo = Liquidacion.Calculo_SalarioBrutoMasAlto(item.emp_Id);
                ModelPagoDeCesantiaDetalle.TotalCesantiaColaborador = (ModelPagoDeCesantiaDetalle.ConSueldo / 30) * ModelPagoDeCesantiaDetalle.DiasPagados;
                ModelPagoDeCesantiaDetalle.NoDeCuenta = item.NoDeCuenta;
                ModelPagoDeCesantiaDetalleList.Add(ModelPagoDeCesantiaDetalle);
                iter++;
            }
            return View(ModelPagoDeCesantiaDetalleList);
        }

        [HttpPost]
        public JsonResult ProcesarCesantia(PagoCesantiaViewModel[] listadoCesantia)
        {
            string response = "bien";
            DateTime FechaActual = DateTime.Now;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString))
            {
                //Comenzar la transaccion
                connection.Open();
                SqlTransaction transaccion;
                transaccion = connection.BeginTransaction();

                try
                {
                    //Query del encabezado
                    String queryEncabezado = @"
                                    INSERT plani.tbPagoDeCesantiaDetalle
                                    (
                                        pdcd_IdCesantiaDetalle,
                                        emp_Id,
                                        pdcd_TotalCesantiaColaborador,
                                        pdcd_CodigoPlanillaCesantias,
                                        pdce_IdCesantiaEncabezado,
                                        pdcd_DiasPagados,
                                        pdcd_ConSueldoBruto,
                                        pdcd_UsuarioCrea,
                                        pdcd_FechaCrea
                                    )
                                    VALUES
                                    (
                                        (SELECT ISNULL(MAX(pdcd_IdCesantiaDetalle) + 1, 1) FROM [Plani].[tbPagoDeCesantiaDetalle]),
                                        @empId,
                                        @totalCesantia,
                                        @codigoPlanillaCesantia,
                                        @idEncabezado,
                                        @diasPagados,
                                        @sueldoBruto,
                                        @usuarioCrea,
                                        @fechaCrea,
                                    )   
                             ";
                    
                    //Query del detalle
                    String queryDetalle = @"
                                    INSERT plani.tbPagoDeCesantiaDetalle
                                    (
                                        pdcd_IdCesantiaDetalle,
                                        emp_Id,
                                        pdcd_TotalCesantiaColaborador,
                                        pdcd_CodigoPlanillaCesantias,
                                        pdce_IdCesantiaEncabezado,
                                        pdcd_DiasPagados,
                                        pdcd_ConSueldoBruto,
                                        pdcd_UsuarioCrea,
                                        pdcd_FechaCrea
                                    )
                                    VALUES
                                    (
                                        (SELECT ISNULL(MAX(pdcd_IdCesantiaDetalle) + 1, 1) FROM [Plani].[tbPagoDeCesantiaDetalle]),
                                        @empId,
                                        @totalCesantia,
                                        @codigoPlanillaCesantia,
                                        @idEncabezado,
                                        @diasPagados,
                                        @sueldoBruto,
                                        @usuarioCrea,
                                        @fechaCrea,
                                    )   
                             ";

                    //Asignar el codigo de planilla
                    string codigoPlanillaCesantia = "CSC-" + 1 + FechaActual.Month + FechaActual.Year;

                    using (SqlCommand command = new SqlCommand(queryDetalle, connection))
                    {

                        command.Parameters.AddWithValue("@empId", 1);
                        command.Parameters.AddWithValue("@totalCesantia", 245564M);
                        command.Parameters.AddWithValue("@codigoPlanillaCesantia", codigoPlanillaCesantia);
                        command.Parameters.AddWithValue("@idEncabezado", 1);
                        command.Parameters.AddWithValue("@diasPagados", 35);
                        command.Parameters.AddWithValue("@sueldoBruto", 553);
                        command.Parameters.AddWithValue("@usuarioCrea", 1);
                        command.Parameters.AddWithValue("@fechaCrea", FechaActual);

                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                            response = "error";
                    }
                }
                catch
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {

                    }
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #region GET: LISTADO DEL PREVIEW DE GENERAR LA PLANILLA DE CESANTIA
        [HttpGet]
        public JsonResult ObtenerListaDePagoCesantia()
        {
            //INICIALIZACION DEL OBJETO TIPO LISTA V_tbPagoDeCesantiaDetalle
            List<V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = new List<V_tbPagoDeCesantiaDetalle>();

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

    public class PagoCesantiaViewModel
    {
        public int idEmpleado { get; set; }
        public decimal totalCesantia { get; set; }
        public int diasPagados { get; set; }
    }
}
