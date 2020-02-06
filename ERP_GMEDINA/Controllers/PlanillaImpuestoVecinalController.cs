using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
            Session["GenerarPlanillaImpVecinal"] = "";
            List<tbDeduccionImpuestoVecinal> ImpuestoVecinalList = null;
            try
            {
                ImpuestoVecinalList = db.tbDeduccionImpuestoVecinal.ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return View(ImpuestoVecinalList);
        }
        #endregion

        #region GET: Index Nueva Proyeccion
        // GET: PlanillaImpuestoVecinal
        public ActionResult IndexNuevaProyeccion()
        {
            return View();
        }
        #endregion


        #region POST: PROCESAR CÁLCULO DE IMPUESTO VECINAL
        public JsonResult ProcesarPlanillaImpuestoVecinal()
        {
            //VARIABLE DE RESPUESTA PARA LAS EJECUCIONES
            string response = "bien";
            //INICIALIZACION DEL OBJETO TIPO ViewModel_PlanillaImpuestoVecinal
            List<ViewModel_PlanillaImpuestoVecinal> ViewModel_PlanillaImpuestoVecinalList = new List<ViewModel_PlanillaImpuestoVecinal>();
            try
            {
                //OBTENER LOS RANGOS DE AUXILIO DE CESANTIA
                var ListaEmpleados = (from tbEmpleados in db.tbEmpleados
                                     join tbPersonas in db.tbPersonas on tbEmpleados.per_Id equals tbPersonas.per_Id
                                     where tbPersonas.per_Estado == true
                                     select
                                     new
                                     {
                                         No = tbEmpleados.emp_Id,
                                         Emp_Id = tbEmpleados.emp_Id,
                                         NoIdentidad = tbPersonas.per_Identidad,
                                         NombreCompleto = tbPersonas.per_Nombres + " " + tbPersonas.per_Apellidos,
                                         NoDeCuenta = tbEmpleados.emp_CuentaBancaria
                                     }).OrderBy(x => x.NombreCompleto);

                //FECHA DE LA PETICION
                DateTime FechaPeticion = DateTime.Now;
                //ITERADOR DEL CICLO
                int iter = 0;
                foreach(ViewModel_PlanillaImpuestoVecinal item in (List<ViewModel_PlanillaImpuestoVecinal>)ListaEmpleados)
                {
                    //INICIALIZACION DEL OBJETO TIPO ViewModel_PlanillaImpuestoVecinal
                    ViewModel_PlanillaImpuestoVecinal ViewModel_PlanillaImpuestoVecinal = new ViewModel_PlanillaImpuestoVecinal();
                    //SETEAR LOS CAMPOS PARA MOSTRAR LA PROYECCIÓN
                    ViewModel_PlanillaImpuestoVecinal.No = iter;
                    ViewModel_PlanillaImpuestoVecinal.NoIdentidad = item.NoIdentidad.Substring(0, 4) + "-" + item.NoIdentidad.Substring(4, 4) + "-" + item.NoIdentidad.Substring(9, item.NoIdentidad.Length - 9);
                    ViewModel_PlanillaImpuestoVecinal.NombreCompleto = item.NombreCompleto;

                    //GET CALCULO DE LA DEDUCCION
                    decimal TotalImpuestoVecinal = Proyeccion_ImpuestoVecinal(item.emp_Id);
                    ViewModel_PlanillaImpuestoVecinal.TotalImpuestoVecinal = Convert.ToString(TotalImpuestoVecinal); 

                    //string TotalImpuestoVecinal a decimal
                    decimal DeduccionMensual = Math.Round((TotalImpuestoVecinal / 12), 2);
                    ViewModel_PlanillaImpuestoVecinal.DeduccionMensual = Convert.ToString(DeduccionMensual);

                    ViewModel_PlanillaImpuestoVecinal.NoDeCuenta = item.NoDeCuenta;

                    iter++;
                    ViewModel_PlanillaImpuestoVecinalList.Add(ViewModel_PlanillaImpuestoVecinal);
                }
                Session["GenerarPlanillaImpVecinal"] = ViewModel_PlanillaImpuestoVecinalList;
 
            }
            catch (Exception Ex)
            {
                response = "error";
                Ex.Message.ToString();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return Json(ViewModel_PlanillaImpuestoVecinalList, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region POST: GENERAR LA PLANILLA DE IMPUESTO VECINAL
        public JsonResult GenerarPlanillaImpV()
        {
            #region DECLARACION DE VARIABLES 
            //RECUPERAR LA LISTA EN SESSION
            List<ViewModel_PlanillaImpuestoVecinal> ViewModel_PlanillaImpuestoVecinalList = (List<ViewModel_PlanillaImpuestoVecinal>)Session["GenerarPlanillaImpVecinal"];
            //VARIABLE DE RESPUESTA PARA LA VALIDACIÓN
            string response = "bien";
            //INICIALIZACION DE LA TRANSACCION
            SqlTransaction transaccion = null;
            //FECHA DE LA INSERCIÓN
            DateTime FechaInsercion = DateTime.Now;
            #endregion

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString))
            {
                //RECUPERAR EL OBJETO DE SESIÓN
                tbUsuario sesion = Session["sesionUsuario"] as tbUsuario;
                //ABRIR LA CONEXIÓN 
                connection.Open();
                //INICIALIZAR LA TRANSACCIÓN
                transaccion = connection.BeginTransaction("InsersionImpuestoVecinal");
                try
                {
                    //QUERY
                    string UDP_NAME = "UDP_Plani_tbDeduccionImpuestoVecinal_Insert";
                    //ITERACION DE LA LISTA DE INSERCION
                    foreach (ViewModel_PlanillaImpuestoVecinal item in ViewModel_PlanillaImpuestoVecinalList)
                    {

                        #region Insertar en el encabezado
                        using (SqlCommand command = new SqlCommand(UDP_NAME, connection, transaccion))
                        {
                            //DECLARACION DE COMANDO TIPO SP
                            command.CommandType = CommandType.StoredProcedure;
                            //CONVERSIÓN: string item.TotalImpuestoVecinal a decimal
                            decimal TotalImpuestoVecinal = Convert.ToDecimal(item.TotalImpuestoVecinal);
                            //CONVERSIÓN: string item.DeduccionMensual a decimal
                            decimal DeduccionMensual = Convert.ToDecimal(item.DeduccionMensual);
                            //SOBRECARGA DE PARAMETROS DEL SP
                            command.Parameters.AddWithValue("@emp_Id", item.emp_Id);
                            command.Parameters.AddWithValue("@dimv_MontoTotal", TotalImpuestoVecinal);
                            command.Parameters.AddWithValue("@dimv_CuotaAPagar", DeduccionMensual);
                            command.Parameters.AddWithValue("@timv_UsuarioCrea", sesion.usu_Id);
                            command.Parameters.AddWithValue("@timv_FechaCrea", FechaInsercion);

                            //ALMACENAR EL NUMERO DE INSERCIONES
                            int result = command.ExecuteNonQuery();
                            //VALIDAR SI HAY ERRORES EN LA INSERCIÓN
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

                    }
                    //AFIRMAR LOS CAMBIOS EN LA DB
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        ex.Message.ToString();
                        transaccion.Rollback();
                        response = "error";
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {

                    }
                }
            }
            //RETORNO DE LA EJECUCIÓN
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region CÁLCULO DE SALARIO BRUTO MAS ALTO
        //CALCULO DE SALARIO ORDINARIO PROMEDIO MENSUAL
        public static decimal Proyeccion_ImpuestoVecinal(int Emp_Id)
        {
            //Captura de SalarioPromedio
            decimal SalarioBrutoMasAlto = 0;
            using (ERP_GMEDINAEntities db2 = new ERP_GMEDINAEntities())
            {
                try
                {
                    //METODO MEDIANTE HISTORIAL DE PAGO
                    IQueryable<decimal> SalariosUlt6Meses = db2.tbHistorialDePago.OrderByDescending(x => x.hipa_FechaPago)
                                                                               .Where(p => p.emp_Id == Emp_Id  &&
                                                                               p.hipa_FechaPago >= (DateTime.Now).AddYears(-1) && p.hipa_FechaPago <= DateTime.Now)
                                                                               .Select(x => (decimal)x.hipa_TotalSueldoBruto);
                    //REALIZAR PROYECCIÓN EN BASE A PROMEDIO
                    SalarioBrutoMasAlto = (SalariosUlt6Meses.Count() > 0) ? SalariosUlt6Meses.Average() : 0;
                    //VALIDAR EN CASO QUE EL EMPLEADO NO ESTE EN EL HISTORIAL DE PAGO
                    if (SalarioBrutoMasAlto == 0)
                    {
                        //TOMAR EL PRIMER SALARIO
                        SalariosUlt6Meses = db2.tbSueldos.OrderByDescending(z => z.sue_FechaCrea)
                                               .Where(p => p.emp_Id == Emp_Id && p.sue_Estado == true)
                                               .Select(x => (decimal)x.sue_Cantidad).Take(1);
                        //ASEGURAR QUE TOMA EL PRIMER SALARIO
                        SalarioBrutoMasAlto = SalariosUlt6Meses.FirstOrDefault();
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Math.Round(SalarioBrutoMasAlto, 2);
        }
        #endregion

    }
}