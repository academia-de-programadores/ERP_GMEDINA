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
            tbUsuario sesion = Session["sesionUsuario"] as tbUsuario;
            string response = "bien";
            DateTime FechaActual = DateTime.Now;
            int idEncabezado = 0;

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

            //Query del encabezado
            String queryEncabezado = @"
                                    INSERT plani.tbPagoDeCesantiaEncabezado
                                    (
                                        pdce_IdCesantiaEncabezado,
                                        pdce_CodigoPlanillaCesantias,
                                        pdce_TotalCesantias,
                                        pdce_UsuarioCrea,
                                        pdce_FechaCrea
                                    )
                                    VALUES
                                    (
                                        @idCesantiaEncabezado,
                                        @codigoPlanillaCesantia,
                                        @totalCesantia,
                                        @usuarioCrea,
                                        @fechaCrea
                                    ) 
                             ";


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString))
            {
                //Comenzar la transaccion
                connection.Open();
                SqlTransaction transaccion;
                transaccion = connection.BeginTransaction();
                foreach (var item in listadoCesantia)
                    try
                    {
                        using (SqlCommand command = new SqlCommand("(SELECT ISNULL(MAX(pdce_IdCesantiaEncabezado) + 1, 1) FROM [Plani].[tbPagoDeCesantiaEncabezado])", connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    idEncabezado = reader.GetInt32(0);
                                }
                            }
                        }

                        //Asignar el codigo de planilla
                        string codigoPlanillaCesantia = "CSC-" + idEncabezado + FechaActual.Month + FechaActual.Year;

                        //Insertar en el encabezado
                        using (SqlCommand command = new SqlCommand(queryEncabezado, connection))
                        {
                            command.Parameters.AddWithValue("@idCesantiaEncabezado", idEncabezado);
                            command.Parameters.AddWithValue("@codigoPlanillaCesantia", codigoPlanillaCesantia);
                            command.Parameters.AddWithValue("@totalCesantia", item.totalCesantia);
                            command.Parameters.AddWithValue("@usuarioCrea", sesion.usu_Id);
                            command.Parameters.AddWithValue("@fechaCrea", FechaActual);

                            int result = command.ExecuteNonQuery();

                            if (result < 0)
                                try
                                {
                                    transaccion.Rollback();
                                }
                                catch
                                {

                                }
                        }

                        //Insertar en el detalle
                        using (SqlCommand command = new SqlCommand(queryDetalle, connection))
                        {
                            command.Parameters.AddWithValue("@empId", item.idEmpleado);
                            command.Parameters.AddWithValue("@totalCesantia", item.totalCesantia);
                            command.Parameters.AddWithValue("@codigoPlanillaCesantia", codigoPlanillaCesantia);
                            command.Parameters.AddWithValue("@idEncabezado", idEncabezado);
                            command.Parameters.AddWithValue("@diasPagados", item.diasPagados);
                            command.Parameters.AddWithValue("@sueldoBruto",item.sueldoBruto);
                            command.Parameters.AddWithValue("@usuarioCrea", sesion.usu_Id);
                            command.Parameters.AddWithValue("@fechaCrea", FechaActual);

                            int result = command.ExecuteNonQuery();

                            if (result < 0)
                                try
                                {
                                    transaccion.Rollback();
                                }
                                catch
                                {

                                }
                        }
                    }
                    catch(Exception ex)
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
        public decimal sueldoBruto { get; set; }
    }
}
