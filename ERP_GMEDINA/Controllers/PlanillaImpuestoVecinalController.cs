using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{

    public class PlanillaImpuestoVecinalController : Controller
    {
        //INSTANCIA DEL MODELO
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();
        #region GET: Index
        // GET: PlanillaImpuestoVecinal
        [SessionManager("PlanillaImpuestoVecinal/Index")]
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
        [SessionManager("PlanillaImpuestoVecinal/Index")]
        public ActionResult IndexNuevaProyeccion()
        {
            return View();
        }
        #endregion


        #region POST: PROCESAR CÁLCULO DE IMPUESTO VECINAL
        [SessionManager("PlanillaImpuestoVecinal/ProcesarPlanillaImpuestoVecinal")]
        public JsonResult ProcesarPlanillaImpuestoVecinal()
        {
            //VARIABLE DE RESPUESTA PARA LAS EJECUCIONES
            string response = "bien";
            //INICIALIZACION DEL OBJETO TIPO ViewModel_PlanillaImpuestoVecinal
            List<ViewModel_PlanillaImpuestoVecinal> ViewModel_PlanillaImpuestoVecinalList = new List<ViewModel_PlanillaImpuestoVecinal>();
            try
            {

                //INICIALIZACION DEL OBJETO CON LA LISTA DE EMPLEADOS REUTILIZANDO EL VIEW TIPO V_tbPagoDeCesantiaDetalle_Preview
                var ListaEmpleados = db.V_tbPagoDeCesantiaDetalle_Preview.OrderBy(x => x.NombreCompleto).ToList();

                //FECHA DE LA PETICION
                DateTime FechaPeticion = Function.DatetimeNow();
                //ITERADOR DEL CICLO
                int iter = 1;
                //Variable de tipo lista para traer los registros de la base de datos de menor a mayor
                List<tbTechoImpuestoVecinal> objDeduccionIV = db.tbTechoImpuestoVecinal.Where(x => x.timv_Activo == true)
                                                                .OrderBy(x => x.timv_RangoInicio)
                                                                .ToList();
                //ITERAR LA LISTA
                foreach (V_tbPagoDeCesantiaDetalle_Preview item in ListaEmpleados)
                {
                    //INICIALIZACION DEL OBJETO TIPO ViewModel_PlanillaImpuestoVecinal
                    ViewModel_PlanillaImpuestoVecinal ViewModel_PlanillaImpuestoVecinal = new ViewModel_PlanillaImpuestoVecinal();
                    //SETEAR LOS CAMPOS PARA MOSTRAR LA PROYECCIÓN
                    ViewModel_PlanillaImpuestoVecinal.No = iter;
                    ViewModel_PlanillaImpuestoVecinal.emp_Id = item.emp_Id;
                    ViewModel_PlanillaImpuestoVecinal.NoIdentidad = item.NoIdentidad.Substring(0, 4) + "-" + item.NoIdentidad.Substring(4, 4) + "-" + item.NoIdentidad.Substring(9, item.NoIdentidad.Length - 9);
                    ViewModel_PlanillaImpuestoVecinal.NombreCompleto = item.NombreCompleto;

                    //GET CALCULO DE LA DEDUCCION
                    decimal TotalIngresoBrutoAnual = Proyeccion_ImpuestoVecinal(item.emp_Id);
                    ViewModel_PlanillaImpuestoVecinal.Total_ImpuestoVecinal = CalculoImpuestoVecinal(TotalIngresoBrutoAnual, objDeduccionIV);

                    //string TotalImpuestoVecinal a decimal
                    decimal DeduccionMensual = Math.Round((ViewModel_PlanillaImpuestoVecinal.Total_ImpuestoVecinal / 12), 2);
                    ViewModel_PlanillaImpuestoVecinal.DeduccionMensual = DeduccionMensual;

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
            var ListGeneric = ViewModel_PlanillaImpuestoVecinalList.Select(c => new
            {
                No = c.No,
                NoIdentidad = c.NoIdentidad,
                NombreCompleto = c.NombreCompleto,
                Total_ImpuestoVecinal = c.Total_ImpuestoVecinal,
                DeduccionMensual = c.DeduccionMensual,
                NoDeCuenta = c.NoDeCuenta
            }).ToList();
            return Json(ListGeneric, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region POST: GENERAR LA PLANILLA DE IMPUESTO VECINAL
        [SessionManager("PlanillaImpuestoVecinal/GenerarPlanillaImpV")]
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
            DateTime FechaInsercion = Function.DatetimeNow();
            #endregion

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString))
            {
                //RECUPERAR EL OBJETO DE SESIÓN
                int idUsuarioCrea = (int)HttpContext.Session["UserLogin"];
                //ABRIR LA CONEXIÓN 
                connection.Open();
                //INICIALIZAR LA TRANSACCIÓN
                transaccion = connection.BeginTransaction("InsersionImpuestoVecinal");
                try
                {
                    //QUERY
                    //string UDP_NAME = "[plani].[UDP_Plani_tbDeduccionImpuestoVecinal_Insert] @emp_Id, @dimv_MontoTotal, @dimv_CuotaAPagar, @timv_UsuarioCrea, @timv_FechaCrea";
                    #region Insertar en el encabezado
                    using (SqlCommand command = new SqlCommand("Plani.UDP_Plani_tbDeduccionImpuestoVecinal_Insert", connection, transaccion))
                    {
                        //DECLARACION DE COMANDO TIPO SP
                        command.CommandType = CommandType.StoredProcedure;

                        //ITERACION DE LA LISTA DE INSERCION
                        foreach (ViewModel_PlanillaImpuestoVecinal item in ViewModel_PlanillaImpuestoVecinalList)
                        {

                            //SOBRECARGA DE PARAMETROS DEL SP
                            command.Parameters.AddWithValue("@emp_Id", item.emp_Id);
                            command.Parameters.AddWithValue("@dimv_MontoTotal", item.Total_ImpuestoVecinal);
                            command.Parameters.AddWithValue("@dimv_CuotaAPagar", item.DeduccionMensual);
                            command.Parameters.AddWithValue("@timv_UsuarioCrea", idUsuarioCrea);
                            command.Parameters.AddWithValue("@timv_FechaCrea", FechaInsercion);

                            //ALMACENAR EL NUMERO DE INSERCIONES
                            int result = int.Parse(((command.ExecuteScalar() as string) ?? "-1"));

                            if (result <= 0)
                                try
                                {
                                    transaccion.Rollback();
                                    response = "error";
                                }
                                catch
                                {

                                }

                            command.Parameters.Clear();
                        }

                    }
                    #endregion
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
                    //FECHA INICIAL DEL RANGO
                    DateTime FechaInicio = (DateTime.Now).AddYears(-1);
                    //METODO MEDIANTE HISTORIAL DE PAGO
                    IQueryable<decimal> SalariosUltMeses = db2.tbHistorialDePago.OrderByDescending(x => x.hipa_FechaPago)
                                                                               .Where(p => p.emp_Id == Emp_Id &&
                                                                               p.hipa_FechaPago >= FechaInicio && p.hipa_FechaPago <= DateTime.Now)
                                                                               .Select(x => (decimal)x.hipa_TotalSueldoBruto);
                    //REALIZAR PROYECCIÓN EN BASE A PROMEDIO
                    SalarioBrutoMasAlto = (SalariosUltMeses.Count() > 0) ? SalariosUltMeses.Average() : 0;
                    //VALIDAR EN CASO QUE EL EMPLEADO NO ESTE EN EL HISTORIAL DE PAGO
                    if (SalarioBrutoMasAlto == 0)
                    {
                        //TOMAR EL PRIMER SALARIO
                        SalariosUltMeses = db2.tbSueldos.OrderByDescending(z => z.sue_FechaCrea)
                                               .Where(p => p.emp_Id == Emp_Id && p.sue_Estado == true)
                                               .Select(x => (decimal)x.sue_Cantidad).Take(1);
                        //ASEGURAR QUE TOMA EL PRIMER SALARIO
                        SalarioBrutoMasAlto = SalariosUltMeses.FirstOrDefault();
                    }
                    SalarioBrutoMasAlto *= 12;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Math.Round(SalarioBrutoMasAlto, 2);
        }
        #endregion

        #region CÁCULO DE IMPUESTO VECINAL
        public decimal CalculoImpuestoVecinal(decimal TotalSalarioDevengado, List<tbTechoImpuestoVecinal> objDeduccionIV)
        {

            #region Impuesto Vecinal

            //TOTAL DEDUCCION
            decimal? TotalImpuestoVecinal = 0;

            //DECLARACIONES DE VALIDACION
            //Variable para validar que entre en cierto momento en cada uno de los rangos
            string IV = "PrimerRango";
            //Variable para validar como aparece en la formula del Excel
            decimal? RangoInicio = 0;
            //Variable para validar como aparece en la formula del Excel
            decimal? RangoFin = 0;

            //Variable para validar que entre justo en el ultimo rango sin importar la cantidad de registros
            int iteradorA = 0;

            //Comienzo de la formula del calculo del Impuesto Vecinal
            foreach (var oIV in objDeduccionIV)
            {
                //Cada vez que pase de nuevo si sume uno mas, para luego validarlo para que entre en el ultimo registro 
                iteradorA = iteradorA + 1;

                //Si trae datos o siquiera uno entre a hacer el calculo 
                if (objDeduccionIV.Count() > 0)
                {
                    //Entrada del Primer Rango
                    if (IV == "PrimerRango")
                    {
                        TotalImpuestoVecinal = TotalImpuestoVecinal + (oIV.timv_RangoFin * oIV.timv_Impuesto) / 1000;
                        IV = "SegundoRango";
                    }
                    //Entrada del segundo rango
                    else if (IV == "SegundoRango")
                    {
                        RangoInicio = oIV.timv_RangoInicio;
                        RangoFin = oIV.timv_RangoFin;
                        if (TotalSalarioDevengado > RangoFin)
                        {
                            //Formula del Excel
                            TotalImpuestoVecinal = TotalImpuestoVecinal + ((oIV.timv_RangoFin - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));
                        }
                        else
                        {
                            //Formula del Excel
                            TotalImpuestoVecinal = TotalImpuestoVecinal + ((TotalSalarioDevengado - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));
                        }
                        IV = "TercerRango";
                    }
                    //Entrada de los siguientes rangos hasta el ultimo
                    else if (IV == "TercerRango")
                    {
                        if (TotalSalarioDevengado < RangoInicio)
                        {
                            TotalImpuestoVecinal = 0;
                        }
                        //Entrada en especifico del ultimo registro
                        else if (objDeduccionIV.Count() == iteradorA)
                        {
                            if (TotalSalarioDevengado < (oIV.timv_RangoInicio - 1))
                            {
                                TotalImpuestoVecinal = TotalImpuestoVecinal + 0;
                            }
                            else
                            {
                                //Formula del Excel
                                TotalImpuestoVecinal = TotalImpuestoVecinal + ((TotalSalarioDevengado - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));
                            }
                        }
                        else if (TotalSalarioDevengado > RangoFin)
                        {
                            //Formula del Excel
                            TotalImpuestoVecinal = TotalImpuestoVecinal + ((oIV.timv_RangoFin - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));
                        }

                    }

                }
            }
            #endregion
            //Verificar con el Excel, y colocar el valor de la variable TotalSalarioDevengado en el excel "Ingreso Devengado 2019"
            TotalImpuestoVecinal = (TotalImpuestoVecinal == null) ? 0 : Math.Round((decimal)TotalImpuestoVecinal, 2);
            //RETURN
            return (decimal)TotalImpuestoVecinal;
        }
        #endregion
    }
}