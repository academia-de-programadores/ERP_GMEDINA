using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Helpers;
using System.Data.SqlClient;
using System.Configuration;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class PagoDeCesantiaDetalleController : Controller
    {
        //INSTANCIA DEL MODELO
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region GET: INDEX DE CESANTIA PAGADA
        [SessionManager("PagoDeCesantiaDetalle/Index")]
        public ActionResult Index()
        {
            Session["GenerarPlanillaCesantia"] = "";
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

        #region VISTA DE PROYECCION DE CESANTIAS
        public ViewResult PagarCesantia()
        {
            return View();
        }
        #endregion

        #region GET: LISTADO DEL PREVIEW DE GENERAR LA PLANILLA DE CESANTIA
        [HttpGet]
        public JsonResult ObtenerListaDePagoCesantia()
        {
            //INICIALIZACION DEL OBJETO TIPO LISTA V_tbPagoDeCesantiaDetalle
            List<V_tbPagoDeCesantiaDetalle> ModelPagoDeCesantiaDetalleList = new List<V_tbPagoDeCesantiaDetalle>();
            try
            {
                //OBTENER LOS RANGOS DE AUXILIO DE CESANTIA
                List<tbAuxilioDeCesantias> TbLiquidacionAuxilioCesantia = db.tbAuxilioDeCesantias.ToList();
                //FECHA DE LA PETICION
                DateTime FechaPeticion = Function.DatetimeNow();
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
                    ModelPagoDeCesantiaDetalle.NoColaborador = item.emp_Id;
                    ModelPagoDeCesantiaDetalle.NoIdentidad = item.NoIdentidad.Substring(0, 4) + "-" + item.NoIdentidad.Substring(4, 4) + "-" + item.NoIdentidad.Substring(9, item.NoIdentidad.Length - 9);
					ModelPagoDeCesantiaDetalle.NombreCompleto = item.NombreCompleto;
                    ModelPagoDeCesantiaDetalle.DiasPagados = (int)Liquidacion.Dias360AcumuladosCesantia(item.emp_Id, FechaPeticion);
                    ModelPagoDeCesantiaDetalle.SueldoBrutoDiario = Liquidacion.Calculo_SalarioBrutoMasAlto(item.emp_Id);
                    ModelPagoDeCesantiaDetalle.TotalCesantiaPRO = Liquidacion.Calculo_ReduccionPasivoLaboral(item.emp_Id, ModelPagoDeCesantiaDetalle.SueldoBrutoDiario, ModelPagoDeCesantiaDetalle.DiasPagados, TbLiquidacionAuxilioCesantia);
                    ModelPagoDeCesantiaDetalle.NoDeCuenta = item.NoDeCuenta;
                    ModelPagoDeCesantiaDetalleList.Add(ModelPagoDeCesantiaDetalle);
                    iter++;
                }

                Session["GenerarPlanillaCesantia"] = ModelPagoDeCesantiaDetalleList;

                
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            return Json(ModelPagoDeCesantiaDetalleList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: REGISTRAR CESANTÍA
        [HttpPost]
        public JsonResult ProcesarCesantia()
        {
            //VALIDAR QUE LA LISTA DE SESSION NO SEA NULL
            if (Convert.ToString(Session["GenerarPlanillaCesantia"]) == "" || Session["GenerarPlanillaCesantia"] == null)
                return Json("SinCargar", JsonRequestBehavior.AllowGet);
            //RECUPERAR LA LISTA DE LA SESSION
            List<V_tbPagoDeCesantiaDetalle> listadoCesantia = (List<V_tbPagoDeCesantiaDetalle>)Session["GenerarPlanillaCesantia"];
            //ALMACENAR EL TOTAL DEL PAGO DE CESANTIA
            decimal TotalCesantiaEncabezado = (decimal)listadoCesantia.Select(x => x.TotalCesantiaPRO).ToList().Sum();

            #region Declaración de variables
            tbUsuario sesion = Session["sesionUsuario"] as tbUsuario;
            string response = "bien";
            DateTime FechaActual = Function.DatetimeNow();
            int idEncabezado = 0;
            SqlTransaction transaccion = null;

            //Query del detalle
            String queryDetalle = @"
                                    INSERT plani.tbPagoDeCesantiaDetalle
                                    (
                                        pdcd_IdCesantiaDetalle,
                                        emp_Id,
                                        pdcd_TotalCesantiaColaborador,
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
                                        @idEncabezado,
                                        @diasPagados,
                                        @sueldoBruto,
                                        @usuarioCrea,
                                        @fechaCrea
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
            #endregion

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString))
            {
                //Comenzar la transaccion
                connection.Open();
                transaccion = connection.BeginTransaction("InsersionCesasntias");
                try
                {

                    #region Obtener el id del encabezado a insertar
                    using (SqlCommand command = new SqlCommand("(SELECT ISNULL(MAX(pdce_IdCesantiaEncabezado) + 1, 1) FROM [Plani].[tbPagoDeCesantiaEncabezado])", connection, transaccion))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idEncabezado = reader.GetInt32(0);
                            }
                        }
                    }
                    #endregion

                    //GENERAR CÓDIGO DE PLANILLA
                    string codigoPlanillaCesantia = "CSC" + idEncabezado + "-" + FechaActual.Day + FechaActual.Month + "-" + FechaActual.Year;

                    #region Insertar en el encabezado
                    using (SqlCommand command = new SqlCommand(queryEncabezado, connection, transaccion))
                    {
                        command.Parameters.AddWithValue("@idCesantiaEncabezado", idEncabezado);
                        command.Parameters.AddWithValue("@codigoPlanillaCesantia", codigoPlanillaCesantia);
                        command.Parameters.AddWithValue("@totalCesantia", TotalCesantiaEncabezado);
                        command.Parameters.AddWithValue("@usuarioCrea", sesion.usu_Id);
                        command.Parameters.AddWithValue("@fechaCrea", FechaActual);

                        int result = command.ExecuteNonQuery();

                        //si hay error en encabezado hacer rollback
                        if (result < 0)
                            try
                            {
                                transaccion.Rollback();
                                response = "error";
                            }
                            catch
                            {

                            }
                    }
                    #endregion

                    #region Insertar en el detalle
                    foreach (var item in listadoCesantia)
                    {

                        using (SqlCommand command = new SqlCommand(queryDetalle, connection, transaccion))
                        {
                            command.Parameters.AddWithValue("@empId", item.NoColaborador);
                            command.Parameters.AddWithValue("@totalCesantia", item.TotalCesantiaPRO);
                            command.Parameters.AddWithValue("@idEncabezado", idEncabezado);
                            command.Parameters.AddWithValue("@diasPagados", item.DiasPagados);
                            command.Parameters.AddWithValue("@sueldoBruto", item.SueldoBrutoDiario);
                            command.Parameters.AddWithValue("@usuarioCrea", sesion.usu_Id);
                            command.Parameters.AddWithValue("@fechaCrea", FechaActual);

                            int result = command.ExecuteNonQuery();

                            //Si hay error hacer un rollback
                            if (result < 0)
                                try
                                {
                                    transaccion.Rollback();
                                    response = "error";
                                }
                                catch
                                {

                                }
                        }

                    }
                    #endregion

                    //Confirmar los cambios en la base de datos
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        ex.Message.ToString();
                        transaccion.Rollback();
                        response = "error";
                    }
                    catch
                    {

                    }
                }
            }
            object Obj_Return = new { Obj_response = response, data = listadoCesantia.ToList() };
            return Json(Obj_Return, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

}