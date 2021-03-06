using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Helpers;
using System.Threading.Tasks;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{

    public class PlanillaController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region GET: Planilla
        [SessionManager("Planilla/Index")]
        public ActionResult Index()
        {
            List<V_ColaboradoresPorPlanilla> colaboradoresPlanillas = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).ToList();
            ViewBag.PlanillasColaboradores = colaboradoresPlanillas;
            ViewBag.colaboradoresGeneral = db.V_PreviewPlanilla.Count().ToString();
            return View(db.V_PreviewPlanilla.ToList());
        }
        #endregion

        #region GetMonedas

        public ActionResult getMonedas()
        {
            var objMonedas = db.tbTipoMonedas
                        .Where(c => c.tmon_Estado == true)
                        .Select(c => new { tmon_Id = c.tmon_Id, tmon_Descripcion = c.tmon_Descripcion })
                        .ToList();

            // retornar información
            return new JsonResult { Data = objMonedas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult getMonedasPlanilla(int? cpla_IdPlanilla)
        {
            var objMonedas = (from empleados in db.tbEmpleados
                              join sueldo in db.tbSueldos on empleados.emp_Id equals sueldo.emp_Id
                              join monedas in db.tbTipoMonedas on sueldo.tmon_Id equals monedas.tmon_Id
                              where cpla_IdPlanilla == null ? "1" == "1" : empleados.cpla_IdPlanilla == cpla_IdPlanilla
                              select new { tmon_Id = monedas.tmon_Id, tmon_Descripcion = monedas.tmon_Descripcion }
                                ).Distinct();

            return new JsonResult { Data = objMonedas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: GetPlanilla
        // cargar en el datatable una planilla en específico
        public ActionResult GetPlanilla(int? ID)
        {
            List<V_PreviewPlanilla> PreviewPlanilla = new List<V_PreviewPlanilla>();

            if (ID != null)
                PreviewPlanilla = db.V_PreviewPlanilla.Where(x => x.cpla_IdPlanilla == ID).ToList();
            else
                PreviewPlanilla = db.V_PreviewPlanilla.ToList();
            return Json(PreviewPlanilla, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GenerarPlanilla
        [SessionManager("Planilla/Index")]
        public async Task<ActionResult> GenerarPlanilla(int? ID, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin, List<ViewModelTasasDeCambio> monedas, int tmon_IdMonedaDeduccionesDePlanilla)
        {
            #region declaracion de instancias

            // helper
            General utilities = new General();

            // instancias para el comprobante de pago
            List<IngresosDeduccionesVoucher> ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
            List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
            ComprobantePagoModel oComprobantePagoModel = new ComprobantePagoModel();
            IngresosDeduccionesVoucher ingresosColaborador = new IngresosDeduccionesVoucher();
            IngresosDeduccionesVoucher deduccionesColaborador = new IngresosDeduccionesVoucher();

            // instancias para el reporte final
            ReportePlanillaViewModel oPlanillaEmpleado;
            List<ReportePlanillaViewModel> reporte = new List<ReportePlanillaViewModel>();
            ViewModelListaErrores oError;
            List<ViewModelListaErrores> listaErrores = new List<ViewModelListaErrores>();

            // instancia para resultado del proceso en izitoast
            General.iziToast response = new General.iziToast();
            int errores = 0;
            string codigoDePlanillaGenerada = String.Empty;
            #endregion

            #region inicia proceso de generación de planilla
            try
            {
                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();
                    List<int> planillasConColaboradores = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).Select(x => x.cpla_IdPlanilla).ToList();

                    // seleccionar las planillas que se van a generar
                    if (ID != null)
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(X => X.cpla_IdPlanilla == ID)
                                          .ToList();
                    else
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(x => x.cpla_Activo == true && planillasConColaboradores.Contains(x.cpla_IdPlanilla))
                                          .ToList();

                    if (oIDSPlanillas != null)
                    {
                        // procesar todas las planillas seleccionadas
                        foreach (var iter in oIDSPlanillas)
                        {
                            codigoDePlanillaGenerada = $"PLANI_{Function.DatetimeNow().Year}{Function.DatetimeNow().Month}{Function.DatetimeNow().Day}_{Function.DatetimeNow().Hour}{Function.DatetimeNow().Minute}{Function.DatetimeNow().Second}";
                            try
                            {
                                // planilla actual del foreach
                                tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas
                                                                    .Find(iter.cpla_IdPlanilla);

                                // ingresos de la planilla actual
                                List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos
                                                                       .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                       .ToList();

                                // deducciones de la planilla actual
                                List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones
                                                                             .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                             .ToList();

                                // empleados de la planilla actual
                                List<tbEmpleados> oEmpleados = db.tbEmpleados
                                                                 .Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla &&
                                                                        emp.emp_Estado == true)
                                                                .ToList();

                                int contador = 1,
                                    idHistorialPago = 0,
                                    idDetalleDeduccionHisotorialesContador = 1,
                                    idDetalleIngresoHisotorialesContador = 1;

                                string identidadEmpleado = string.Empty,
                                    NombresEmpleado = string.Empty;

                                // procesar planilla empleado por empleado
                                foreach (var empleadoActual in oEmpleados)
                                {
                                    using (var dbContextTransaccion = db.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            #region variables reporte view model

                                            string codColaborador = string.Empty,
                                                nombreColaborador = string.Empty,
                                                tipoPlanilla = string.Empty;

                                            decimal SalarioBase = 0,
                                                salarioHora = 0,
                                                totalAFP = 0,
                                                totalISR = 0,
                                                totalSalario = 0;

                                            int horasTrabajadas = 0,
                                                horasExtrasTrabajadas = 0,
                                                cantidadUnidadesBonos = 0;

                                            decimal? totalComisiones = 0,
                                                totalHorasExtras = 0,
                                                totalHorasPermiso = 0,
                                                totalBonificaciones = 0,
                                                totalIngresosIndivuales = 0,
                                                totalVacaciones = 0,
                                                totalIngresosEmpleado = 0,
                                                colaboradorDeducciones = 0,
                                                totalInstitucionesFinancieras = 0,
                                                totalOtrasDeducciones = 0,
                                                adelantosSueldo = 0,
                                                totalDeduccionesEmpleado = 0,
                                                totalDeduccionesIndividuales = 0,
                                                netoAPagarColaborador = 0;

                                            oPlanillaEmpleado = new ReportePlanillaViewModel();
                                            oError = new ViewModelListaErrores();
                                            string moneda = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id && x.sue_Estado == true).Select(x => x.tbTipoMonedas.tmon_Descripcion).FirstOrDefault();

                                            // variables para insertar en los historiales de pago
                                            IEnumerable<object> listHistorialPago = null;
                                            string MensajeError = "";
                                            List<tbHistorialDeduccionPago> lisHistorialDeducciones = new List<tbHistorialDeduccionPago>();
                                            List<tbHistorialDeIngresosPago> lisHistorialIngresos = new List<tbHistorialDeIngresosPago>();


                                            #endregion

                                            V_InformacionColaborador InformacionDelEmpleadoActual = null;
                                            decimal resultSeptimoDia = 0;

                                            int userId = (int)Session["UserLogin"];
                                            //Procesar Ingresos
                                            await Task.Run(() => Ingresos.ProcesarIngresos(userId, fechaInicio,
                                                   fechaFin,
                                                   ListaIngresosVoucher,
                                                   listaErrores,
                                                   ref errores,
                                                   db,
                                                   empleadoActual,
                                                   ref SalarioBase,
                                                   out horasTrabajadas,
                                                   ref salarioHora,
                                                   ref totalSalario,
                                                   ref totalComisiones,
                                                   out horasExtrasTrabajadas,
                                                   ref cantidadUnidadesBonos,
                                                   ref totalHorasExtras,
                                                   ref totalHorasPermiso,
                                                   ref totalBonificaciones,
                                                   ref totalIngresosIndivuales,
                                                   ref totalVacaciones,
                                                   out totalIngresosEmpleado,
                                                   lisHistorialIngresos,
                                                   out InformacionDelEmpleadoActual,
                                                   out resultSeptimoDia));

                                            //Procesar Deducciones
                                            await Task.Run(() => Deducciones.ProcesarDeducciones(tmon_IdMonedaDeduccionesDePlanilla,
                                                 monedas,
                                                 userId, fechaInicio,
                                                 fechaFin,
                                                 ListaDeduccionesVoucher,
                                                 listaErrores,
                                                 ref errores,
                                                 db,
                                                 oDeducciones,
                                                 empleadoActual,
                                                 SalarioBase,
                                                 totalIngresosEmpleado,
                                                 ref colaboradorDeducciones,
                                                 ref totalAFP,
                                                 ref totalInstitucionesFinancieras,
                                                 ref totalOtrasDeducciones,
                                                 ref adelantosSueldo,
                                                 out totalDeduccionesEmpleado,
                                                 ref totalDeduccionesIndividuales,
                                                 out netoAPagarColaborador,
                                                 lisHistorialDeducciones,
                                                 InformacionDelEmpleadoActual));

                                            //ISR
                                            TimeSpan tDias = fechaFin - fechaInicio;
                                            totalISR = CalculoISR.CalcularISR(db, empleadoActual, totalSalario, totalISR, tDias.Days + 1);

                                            idHistorialPago = GuardarEnHistorialDePago.GuardarHistorialDePago(fechaInicio,
                                                fechaFin,
                                                listaErrores,
                                                ref errores,
                                                codigoDePlanillaGenerada,
                                                db,
                                                ref contador,
                                                ref idDetalleDeduccionHisotorialesContador,
                                                ref idDetalleIngresoHisotorialesContador,
                                                empleadoActual,
                                                totalSalario,
                                                totalComisiones,
                                                horasExtrasTrabajadas,
                                                cantidadUnidadesBonos,
                                                totalHorasExtras,
                                                totalHorasPermiso,
                                                totalBonificaciones,
                                                totalIngresosIndivuales,
                                                totalVacaciones,
                                                totalISR,
                                                totalAFP,
                                                adelantosSueldo,
                                                totalDeduccionesIndividuales,
                                                netoAPagarColaborador,
                                                ref listHistorialPago,
                                                ref MensajeError,
                                                lisHistorialDeducciones,
                                                lisHistorialIngresos,
                                                InformacionDelEmpleadoActual,
                                                resultSeptimoDia);

                                            // guardar cambios en la bbdd
                                            db.SaveChanges();
                                            dbContextTransaccion.Commit();

                                            //EnviarComprobanteDePago
                                            await Task.Run(() => EnviarComprobanteDePago.EnviarComprobanteDePagoColaborador(moneda,
                                                  enviarEmail,
                                                  fechaInicio,
                                                  fechaFin,
                                                  utilities,
                                                  ref ListaIngresosVoucher,
                                                  ref ListaDeduccionesVoucher,
                                                  oComprobantePagoModel,
                                                  listaErrores,
                                                  ref errores,
                                                  db,
                                                  empleadoActual,
                                                  totalIngresosEmpleado,
                                                  totalDeduccionesEmpleado,
                                                  netoAPagarColaborador,
                                                  InformacionDelEmpleadoActual));

                                            #region crear registro de la planilla del colaborador para el reporte
                                            await Task.Run(() => ReportePlanilla.ReporteColaboradorPlanilla(moneda,
                                                   ref oPlanillaEmpleado,
                                                   empleadoActual,
                                                   SalarioBase,
                                                   horasTrabajadas,
                                                   salarioHora,
                                                   totalSalario,
                                                   totalComisiones,
                                                   horasExtrasTrabajadas,
                                                   totalHorasExtras,
                                                   totalHorasPermiso,
                                                   totalBonificaciones,
                                                   totalIngresosIndivuales,
                                                   totalVacaciones,
                                                   totalIngresosEmpleado,
                                                   totalISR,
                                                   colaboradorDeducciones,
                                                   totalAFP,
                                                   totalInstitucionesFinancieras,
                                                   totalOtrasDeducciones,
                                                   adelantosSueldo,
                                                   totalDeduccionesEmpleado,
                                                   totalDeduccionesIndividuales,
                                                   netoAPagarColaborador,
                                                   InformacionDelEmpleadoActual));

                                            reporte.Add(oPlanillaEmpleado);
                                            oPlanillaEmpleado = null;
                                            identidadEmpleado = InformacionDelEmpleadoActual.per_Identidad;
                                            NombresEmpleado = InformacionDelEmpleadoActual.per_Nombres;
                                            #endregion

                                        }
                                        #endregion
                                        // catch por si hubo un error al generar la planilla de un empleado
                                        catch (Exception ex)
                                        {
                                            // si hay un error, hacer un rollback
                                            dbContextTransaccion.Rollback();

                                            // mensaje del error en el registro del colaborador
                                            errores++;

                                            listaErrores.Add(new ViewModelListaErrores
                                            {
                                                Identidad = identidadEmpleado,
                                                NombreColaborador = NombresEmpleado,
                                                Error = "Error al procesar planilla del colaborador.",
                                                PosibleSolucion = "Verifique información registrada al colaborador y vuelva a intentarlo."

                                            });
                                        }
                                    } // termina transaccion
                                }

                            }
                            // catch si se produjo un error al procesar una sola planilla
                            catch (Exception ex)
                            {
                                listaErrores.Add(new ViewModelListaErrores
                                {
                                    Identidad = $"Planilla {iter.cpla_DescripcionPlanilla}",
                                    NombreColaborador = "",
                                    Error = $"Ocurrió un error al procesar la planilla {iter.cpla_DescripcionPlanilla}"
                                });
                                errores++;
                            }
                        } // for each idsPlanillas

                    } // if idsPlanilla != null

                } // using entities model

                // enviar resultado al cliente
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = errores == 0 ? "success" : "warning";

            }
            // catch se produjo un error fatal en el proceso generar planilla
            catch (Exception ex)
            {
                response.Response = "";
                response.Encabezado = "Error";
                response.Tipo = "error";
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = $"Planilla",
                    NombreColaborador = "",
                    Error = $"El proceso de generación de planillas falló, contacte al adminstrador.",
                    PosibleSolucion = "Vuelva a intentarlo"
                });
            }

            // retornar resultado del proceso
            return Json(new { Data = reporte, listaDeErrores = listaErrores, Response = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region previsualizar
        [SessionManager("Planilla/Index")]
        public ActionResult PrevisualizarPlanilla(int? ID, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin, List<ViewModelTasasDeCambio> monedas, int tmon_IdMonedaDeduccionesDePlanilla)
        {
            #region declaracion de instancias

            // helper
            General utilities = new General();

            // instancias para el reporte final
            ReportePlanillaViewModel oPlanillaEmpleado;
            List<ReportePlanillaViewModel> reporte = new List<ReportePlanillaViewModel>();
            ViewModelListaErrores oError;
            List<ViewModelListaErrores> listaErrores = new List<ViewModelListaErrores>();

            // instancia para resultado del proceso en izitoast
            General.iziToast response = new General.iziToast();
            int errores = 0;
            #endregion

            #region inicia proceso de previsualizacion de planilla
            try
            {
                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();
                    List<int> planillasConColaboradores = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).Select(x => x.cpla_IdPlanilla).ToList();

                    // seleccionar las planillas que se van a generar
                    if (ID != null)
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(X => X.cpla_IdPlanilla == ID)
                                          .ToList();
                    else
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(x => x.cpla_Activo == true && planillasConColaboradores.Contains(x.cpla_IdPlanilla))
                                          .ToList();

                    // procesar todas las planillas seleccionadas
                    foreach (var iter in oIDSPlanillas)
                    {
                        try
                        {
                            // planilla actual del foreach
                            tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas
                                                                .Find(iter.cpla_IdPlanilla);

                            // ingresos de la planilla actual
                            List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos
                                                                   .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                   .ToList();

                            // deducciones de la planilla actual
                            List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones
                                                                         .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                         .ToList();

                            // empleados de la planilla actual
                            List<tbEmpleados> oEmpleados = db.tbEmpleados
                                                             .Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla &&
                                                                    emp.emp_Estado == true)
                                                            .ToList();


                            // procesar planilla empleado por empleado
                            foreach (var empleadoActual in oEmpleados)
                            {
                                using (var dbContextTransaccion = db.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        #region inicia proceso de previsualización

                                        #region variables reporte view model

                                        string codColaborador = string.Empty,
                                            nombreColaborador = string.Empty,
                                            tipoPlanilla = string.Empty;

                                        decimal SalarioBase = 0,
                                            salarioHora = 0,
                                            totalSalario = 0,
                                            totalISR = 0,
                                            totalAFP = 0;

                                        int horasTrabajadas = 0,
                                            horasExtrasTrabajadas = 0,
                                            cantidadUnidadesBonos = 0;

                                        decimal?
                                            totalComisiones = 0,
                                            totalHorasExtras = 0,
                                            totalHorasPermiso = 0,
                                            totalBonificaciones = 0,
                                            totalIngresosIndivuales = 0,
                                            totalVacaciones = 0,
                                            totalIngresosEmpleado = 0,
                                            colaboradorDeducciones = 0,
                                            totalInstitucionesFinancieras = 0,
                                            totalOtrasDeducciones = 0,
                                            adelantosSueldo = 0,
                                            totalDeduccionesEmpleado = 0,
                                            totalDeduccionesIndividuales = 0,
                                            netoAPagarColaborador = 0;

                                        oPlanillaEmpleado = new ReportePlanillaViewModel();
                                        string moneda = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id).Select(x => x.tbTipoMonedas.tmon_Descripcion).FirstOrDefault();

                                        #endregion

                                        V_InformacionColaborador InformacionDelEmpleadoActual;
                                        decimal resultSeptimoDia = 0;
                                        int userId = (int)Session["UserLogin"];

                                        // procesa ingresos
                                        Ingresos.PrevisualizarProcesarIngresos(userId,
                                               fechaInicio,
                                               fechaFin,
                                               listaErrores,
                                               ref errores,
                                               db,
                                               empleadoActual,
                                               ref SalarioBase,
                                               out horasTrabajadas,
                                               ref salarioHora,
                                               ref totalSalario,
                                               ref totalComisiones,
                                               out horasExtrasTrabajadas,
                                               ref cantidadUnidadesBonos,
                                               ref totalHorasExtras,
                                               ref totalHorasPermiso,
                                               ref totalBonificaciones,
                                               ref totalIngresosIndivuales,
                                               ref totalVacaciones,
                                               out totalIngresosEmpleado,
                                               out InformacionDelEmpleadoActual,
                                               out resultSeptimoDia);

                                        // procesar deducciones
                                        Deducciones.PrevisualizarProcesarDeducciones(tmon_IdMonedaDeduccionesDePlanilla,
                                             monedas,
                                             userId,
                                             fechaInicio,
                                             fechaFin,
                                             listaErrores,
                                             ref errores,
                                             db,
                                             oDeducciones,
                                             empleadoActual,
                                             SalarioBase,
                                             totalIngresosEmpleado,
                                             ref colaboradorDeducciones,
                                             ref totalAFP,
                                             ref totalInstitucionesFinancieras,
                                             ref totalOtrasDeducciones,
                                             ref adelantosSueldo,
                                             out totalDeduccionesEmpleado,
                                             ref totalDeduccionesIndividuales,
                                             out netoAPagarColaborador,
                                             InformacionDelEmpleadoActual);

                                        //Deducciones.ProcesarDeduccionesParaPrevisualizacion(fechaInicio, fechaFin, db, oDeducciones, empleadoActual, SalarioBase, totalIngresosEmpleado, ref colaboradorDeducciones, ref totalAFP, ref totalInstitucionesFinancieras, ref totalOtrasDeducciones, ref adelantosSueldo, out totalDeduccionesEmpleado, ref totalDeduccionesIndividuales, out netoAPagarColaborador);

                                        // procesar isr
                                        TimeSpan tDias = fechaFin - fechaInicio;
                                        totalISR = CalculoISR.CalcularISR(db, empleadoActual, totalSalario, totalISR, tDias.Days + 1);

                                        #region crear registro de la planilla del colaborador para el reporte

                                        // reporte
                                        ReportePlanilla.ReportePlanillaPrevisualizacion(moneda, ref oPlanillaEmpleado, empleadoActual, SalarioBase, horasTrabajadas, salarioHora, totalSalario, totalComisiones, horasExtrasTrabajadas, totalHorasExtras, totalHorasPermiso, totalBonificaciones, totalIngresosIndivuales, totalVacaciones, totalIngresosEmpleado, totalISR, colaboradorDeducciones, totalAFP, totalInstitucionesFinancieras, totalOtrasDeducciones, adelantosSueldo, totalDeduccionesEmpleado, totalDeduccionesIndividuales, netoAPagarColaborador, InformacionDelEmpleadoActual);
                                        reporte.Add(oPlanillaEmpleado);
                                        oPlanillaEmpleado = null;
                                        #endregion


                                        #endregion

                                    }
                                    // catch por si hubo un error al generar la planilla de un empleado
                                    catch (Exception ex)
                                    {
                                        // mensaje del error en el registro del colaborador
                                        errores++;
                                    }
                                }
                            }

                        }
                        // catch si se produjo un error al procesar una sola planilla
                        catch (Exception ex)
                        {
                            errores++;
                        }
                    }
                }

                // enviar resultado al cliente
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = errores == 0 ? "success" : "warning";

            }
            // catch se produjo un error fatal en el proceso generar planilla
            catch (Exception ex)
            {
                response.Response = "El proceso de generación de planillas falló, contacte al adminstrador.";
                response.Encabezado = "Error";
                response.Tipo = "error";
            }
            #endregion

            // retornar resultado del proceso
            return Json(new { Data = reporte, listaDeErrores = listaErrores, Response = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dispose
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