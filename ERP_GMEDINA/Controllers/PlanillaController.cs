using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using SpreadsheetLight;
using DocumentFormat.OpenXml;
using ERP_GMEDINA.Helpers;

namespace ERP_GMEDINA.Controllers
{
    public class PlanillaController : Controller
    {
        /*PENDIENTE
        * CALCULAR ISR
        * GUARDAR EN LAS TABLAS DE DETALLE DE HISTORIALES
        * Septimo día               
        */
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Planilla
        public ActionResult Index()
        {
            List<V_ColaboradoresPorPlanilla> colaboradoresPlanillas = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).ToList();
            ViewBag.PlanillasColaboradores = colaboradoresPlanillas;
            ViewBag.colaboradoresGeneral = db.tbEmpleados.Count().ToString();
            return View(db.V_PreviewPlanilla.ToList());
        }

        public ActionResult GetPlanilla(int? ID)
        {
            List<V_PreviewPlanilla> PreviewPlanilla = new List<V_PreviewPlanilla>();

            if (ID != null)
                PreviewPlanilla = db.V_PreviewPlanilla.Where(x => x.cpla_IdPlanilla == ID).ToList();
            else
                PreviewPlanilla = db.V_PreviewPlanilla.ToList();
            return Json(PreviewPlanilla, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        public ActionResult GenerarPlanilla(int? ID, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin)
        {
            #region declaracion de instancias
            //helpers
            General utilities = new General();
            //para el voucher
            List<IngresosDeduccionesVoucher> ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
            List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
            ComprobantePagoModel oComprobantePagoModel = new ComprobantePagoModel();
            IngresosDeduccionesVoucher ingresosColaborador = new IngresosDeduccionesVoucher();
            IngresosDeduccionesVoucher deduccionesColaborador = new IngresosDeduccionesVoucher();
            //para el reporte que se mandará a la vista
            ReportePlanillaViewModel oPlanillaEmpleado;
            List<ReportePlanillaViewModel> reporte = new List<ReportePlanillaViewModel>();

            //para enviar resultados al lado del cliente
            iziToast response = new iziToast();
            int errores = 0;
            #endregion          

            // INCIA PROCESO DE GENERACIÓN DE PLANILLAS
            try
            {

                #region CREAR ARCHIVO EXCEL DE LA PLANILLA
                tbCatalogoDePlanillas oNombrePlanilla = ID != null ? db.tbCatalogoDePlanillas.Where(X => X.cpla_IdPlanilla == ID).FirstOrDefault() : null;
                string nombrePlanilla = oNombrePlanilla != null ? oNombrePlanilla.cpla_DescripcionPlanilla : "General";
                string nombreDocumento = $"Planilla {nombrePlanilla} {Convert.ToString(DateTime.Now.Year)}-{Convert.ToString(DateTime.Now.Month)}-{Convert.ToString(DateTime.Now.Day)} {Convert.ToString(DateTime.Now.Hour)}-{Convert.ToString(DateTime.Now.Minute)}.xlsx";
                string nombreDocumento2 = nombreDocumento;
                string pathFile = AppDomain.CurrentDomain.BaseDirectory + nombreDocumento2;
                string direccion = pathFile;
                SLDocument oSLDocument = new SLDocument();
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add("Nombres", typeof(string));
                dt.Columns.Add("Apellidos", typeof(string));
                dt.Columns.Add("Sueldo base", typeof(decimal));
                dt.Columns.Add("Bonos", typeof(decimal));
                dt.Columns.Add("Comisiones", typeof(decimal));
                dt.Columns.Add("Deducciones extras", typeof(decimal));
                dt.Columns.Add("Deducciones Cooperativas", typeof(decimal));
                dt.Columns.Add("IHSS", typeof(decimal));
                dt.Columns.Add("ISR", typeof(decimal));
                dt.Columns.Add("AFP", typeof(decimal));
                dt.Columns.Add("RAP", typeof(decimal));
                dt.Columns.Add("TOTAL A PAGAR", typeof(decimal));
                #endregion


                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();

                    using (var dbContextTransaccion = db.Database.BeginTransaction())
                    {

                        //seleccionar las planillas que se van a generar
                        if (ID != null)
                            oIDSPlanillas = db.tbCatalogoDePlanillas.Where(X => X.cpla_IdPlanilla == ID).ToList();
                        else
                            oIDSPlanillas = db.tbCatalogoDePlanillas.Where(x => x.cpla_Activo == true).ToList();

                        //procesar todas las planillas seleccionadas
                        foreach (var iter in oIDSPlanillas)
                        {
                            try
                            {
                                //planilla actual del foreach
                                tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas.Find(iter.cpla_IdPlanilla);

                                //ingresos de la planilla actual
                                List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos.Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla).ToList();

                                //deducciones de la planilla actual
                                List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones.Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla).ToList();

                                //empleados de la planilla actual
                                List<tbEmpleados> oEmpleados = db.tbEmpleados.Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla && emp.emp_Estado == true).ToList();

                                int contador = 1;
                                //procesar planilla empleado por empleado
                                foreach (var empleadoActual in oEmpleados)
                                {
                                    try
                                    {
                                        #region variables Reporte View Model

                                        string codColaborador = string.Empty;
                                        string nombreColaborador = string.Empty;
                                        decimal SalarioBase = 0;
                                        int horasTrabajadas = 0;
                                        decimal salarioHora = 0;
                                        decimal totalSalario = 0;
                                        string tipoPlanilla = string.Empty;
                                        decimal? porcentajeComision = 0;
                                        decimal? totalVentas = 0;
                                        decimal? totalComisiones = 0;
                                        int horasExtrasTrabajadas = 0;
                                        decimal? totalHorasExtras = 0;
                                        decimal? totalBonificaciones = 0;
                                        decimal? totalVacaciones = 0;
                                        decimal? totalIngresosEmpleado = 0;
                                        decimal totalISR = 0;
                                        decimal? colaboradorDeducciones = 0;
                                        decimal totalAFP = 0;
                                        decimal? totalInstitucionesFinancieras = 0;
                                        decimal? totalOtrasDeducciones = 0;
                                        decimal? adelantosSueldo = 0;
                                        decimal? totalDeduccionesEmpleado = 0;
                                        decimal? netoAPagarColaborador = 0;
                                        oPlanillaEmpleado = new ReportePlanillaViewModel();
                                        #endregion

                                        //informacion del colaborador actual
                                        V_InformacionColaborador InformacionDelEmpleadoActual = db.V_InformacionColaborador.Where(x => x.emp_Id == empleadoActual.emp_Id).FirstOrDefault();

                                        #region Procesar ingresos

                                        //salario base del colaborador actual
                                        SalarioBase = Math.Round((Decimal)InformacionDelEmpleadoActual.SalarioBase, 2);

                                        //para el voucer
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Sueldo base",
                                            monto = SalarioBase
                                        });

                                        //horas normales trabajadas
                                        horasTrabajadas = db.tbHistorialHorasTrabajadas
                                            .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.tbTipoHoras.tiho_Recargo == 0)
                                            .Select(x => x.htra_CantidadHoras)
                                            .DefaultIfEmpty(0)
                                            .Sum();
                                        //para el voucer
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Horas trabajadas",
                                            monto = horasTrabajadas
                                        });

                                        //salario por hora
                                        salarioHora = Math.Round((Decimal)SalarioBase / 240, 2);

                                        //para el voucer
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Sueldo hora",
                                            monto = salarioHora
                                        });

                                        //total salario
                                        totalSalario = Math.Round((Decimal)salarioHora * horasTrabajadas, 2);
                                        //para el voucer
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Total sueldo",
                                            monto = totalSalario
                                        });

                                        //comisiones
                                        List<tbEmpleadoComisiones> oComisionesColaboradores = db.tbEmpleadoComisiones.Where(x => x.emp_Id == empleadoActual.emp_Id && x.cc_Activo == true && x.cc_Pagado == false).ToList();
                                        if (oComisionesColaboradores.Count > 0)
                                        {
                                            //sumar todas las comisiones
                                            foreach (var oComisionesColaboradoresIterador in oComisionesColaboradores)
                                            {
                                                porcentajeComision = (from tbEmpCom in db.tbEmpleadoComisiones
                                                                      where tbEmpCom.cc_Id == oComisionesColaboradoresIterador.cc_Id
                                                                      select tbEmpCom.cc_PorcentajeComision).FirstOrDefault();

                                                totalVentas = (from tbEmpCom in db.tbEmpleadoComisiones
                                                               where tbEmpCom.cc_Id == oComisionesColaboradoresIterador.cc_Id
                                                               select tbEmpCom.cc_TotalVenta).FirstOrDefault();

                                                totalComisiones += Math.Round((Decimal)(oComisionesColaboradoresIterador.cc_TotalVenta * oComisionesColaboradoresIterador.cc_PorcentajeComision)/100, 2);

                                                //pasar el estado de las comisiones a pagadas
                                                oComisionesColaboradoresIterador.cc_Pagado = true;
                                                oComisionesColaboradoresIterador.cc_FechaPagado = DateTime.Now;
                                                db.Entry(oComisionesColaboradoresIterador).State = EntityState.Modified;

                                                //agregarlas al vocher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oComisionesColaboradoresIterador.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                    monto = Math.Round((Decimal)(oComisionesColaboradoresIterador.cc_TotalVenta * oComisionesColaboradoresIterador.cc_PorcentajeComision)/100, 2)
                                                });
                                            }
                                        }

                                        //horas extras
                                        horasExtrasTrabajadas = db.tbHistorialHorasTrabajadas
                                            .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.tbTipoHoras.tiho_Recargo > 0)
                                            .Select(x => x.htra_CantidadHoras)
                                            .DefaultIfEmpty(0)
                                            .Sum();

                                        if (horasExtrasTrabajadas > 0)
                                        {
                                            //para el voucer
                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = "Horas extras",
                                                monto = horasExtrasTrabajadas
                                            });
                                        }

                                        //total ingresos horas extras
                                        List<tbHistorialHorasTrabajadas> oHorasExtras = db.tbHistorialHorasTrabajadas
                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.tbTipoHoras.tiho_Recargo > 0)
                                                                                        .ToList();
                                        if (oHorasExtras.Count > 0)
                                        {
                                            int CantidadHorasExtrasActual = 0;
                                            //sumar todas las horas extras
                                            foreach (var iterHorasExtras in oHorasExtras)
                                            {
                                                CantidadHorasExtrasActual = db.tbHistorialHorasTrabajadas
                                                .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.htra_Id == iterHorasExtras.htra_Id)
                                                .Select(x => x.htra_CantidadHoras)
                                                .DefaultIfEmpty(0)
                                                .Sum();

                                                totalHorasExtras += Math.Round((Decimal)CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2);


                                                //para el voucer
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{CantidadHorasExtrasActual} horas {iterHorasExtras.tbTipoHoras.tiho_Descripcion} al {iterHorasExtras.tbTipoHoras.tiho_Recargo} %",
                                                    monto = Math.Round((Decimal)CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2)
                                                });
                                            }
                                            //para el voucer
                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = "Total horas extras",
                                                monto = totalHorasExtras
                                            });
                                        }

                                        //bonos del colaborador
                                        List<tbEmpleadoBonos> oBonosColaboradores = db.tbEmpleadoBonos.Where(x => x.emp_Id == empleadoActual.emp_Id && x.cb_Activo == true && x.cb_Pagado == false).ToList();

                                        if (oBonosColaboradores.Count > 0)
                                        {
                                            //iterar los bonos
                                            foreach (var oBonosColaboradoresIterador in oBonosColaboradores)
                                            {
                                                totalBonificaciones += Math.Round((Decimal)oBonosColaboradoresIterador.cb_Monto, 2);
                                                //pasar el bono a pagado
                                                oBonosColaboradoresIterador.cb_Pagado = true;
                                                oBonosColaboradoresIterador.cb_FechaPagado = DateTime.Now;
                                                db.Entry(oBonosColaboradoresIterador).State = EntityState.Modified;

                                                //agregarlo al voucher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oBonosColaboradoresIterador.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                    monto = Math.Round((Decimal)oBonosColaboradoresIterador.cb_Monto, 2)
                                                });
                                            }
                                        }

                                        //vacaciones
                                        List<tbHistorialVacaciones> oVacacionesColaboradores = db.tbHistorialVacaciones.Where(x => x.emp_Id == empleadoActual.emp_Id && x.hvac_Estado == true).ToList();
                                        if (oVacacionesColaboradores.Count > 0)
                                        {
                                            //sumar todas las comisiones
                                            foreach (var oVacacionesColaboradoresIterador in oVacacionesColaboradores)
                                            {
                                                int cantidadDias = 0;
                                                DateTime VacacionesfechaInicio;
                                                DateTime VacacionesfechaFin;

                                                VacacionesfechaInicio = (from tbEmpVac in db.tbHistorialVacaciones
                                                                         where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                         select tbEmpVac.hvac_FechaInicio).FirstOrDefault();

                                                VacacionesfechaFin = (from tbEmpVac in db.tbHistorialVacaciones
                                                                      where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                      select tbEmpVac.hvac_FechaFin).FirstOrDefault();

                                                TimeSpan restaFechas = VacacionesfechaFin - VacacionesfechaInicio;
                                                cantidadDias = restaFechas.Days;

                                                totalVacaciones += Math.Round((salarioHora * 8) * cantidadDias, 2);

                                                //cambiar el estado de las vacaciones a pagadas
                                                //db.Entry(oVacacionesColaboradoresIterador).State = EntityState.Modified;

                                                //agregarlas al vocher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{cantidadDias} dias de vacaciones",
                                                    monto = Math.Round((Decimal)(salarioHora * 8) * cantidadDias, 2)
                                                });
                                            }
                                        }

                                        //total ingresos
                                        totalIngresosEmpleado = totalSalario + totalComisiones + totalHorasExtras + totalBonificaciones;

                                        #endregion

                                        #region Procesar deducciones

                                        //deducciones de la planilla
                                        foreach (var iterDeducciones in oDeducciones)
                                        {
                                            decimal? porcentajeColaborador = iterDeducciones.cde_PorcentajeColaborador;
                                            decimal? porcentajeEmpresa = iterDeducciones.cde_PorcentajeEmpresa;

                                            //verificar techos deducciones
                                            List<tbTechosDeducciones> oTechosDeducciones = db.tbTechosDeducciones.Where(x => x.cde_IdDeducciones == iterDeducciones.cde_IdDeducciones && x.tddu_Activo == true).OrderBy(x => x.tddu_Techo).ToList();
                                            if (oTechosDeducciones.Count() > 0)
                                            {
                                                foreach (var techosDeduccionesIter in oTechosDeducciones)
                                                {
                                                    if (SalarioBase > techosDeduccionesIter.tddu_Techo)
                                                    {
                                                        porcentajeColaborador = techosDeduccionesIter.tddu_PorcentajeColaboradores;
                                                        porcentajeEmpresa = techosDeduccionesIter.tddu_PorcentajeEmpresa;
                                                    }
                                                }
                                            }
                                            //sumar las deducciones
                                            colaboradorDeducciones += Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2);

                                            ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = iterDeducciones.cde_DescripcionDeduccion,
                                                monto = Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2)
                                            });
                                        }

                                        //instituciones financieras
                                        List<tbDeduccionInstitucionFinanciera> oDeduInstiFinancieras = db.tbDeduccionInstitucionFinanciera.Where(x => x.emp_Id == empleadoActual.emp_Id && x.deif_Activo == true && x.deif_Pagado == false).ToList();

                                        if (oDeduInstiFinancieras.Count > 0)
                                        {
                                            //sumarlas todas
                                            foreach (var oDeduInstiFinancierasIterador in oDeduInstiFinancieras)
                                            {
                                                totalInstitucionesFinancieras += Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2);
                                                //pasar el estado de la deduccion a pagada
                                                oDeduInstiFinancierasIterador.deif_Pagado = true;
                                                db.Entry(oDeduInstiFinancierasIterador).State = EntityState.Modified;

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{oDeduInstiFinancierasIterador.tbInstitucionesFinancieras.insf_DescInstitucionFinanc} {oDeduInstiFinancierasIterador.deif_Comentarios}",
                                                    monto = Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2)
                                                });
                                            }
                                        }
                                        //afp
                                        List<tbDeduccionAFP> oDeduccionAfp = db.tbDeduccionAFP.Where(af => af.emp_Id == empleadoActual.emp_Id && af.dafp_Pagado == false && af.dafp_Activo == true).ToList();

                                        if (oDeduccionAfp.Count > 0)
                                        {
                                            //sumarlas todas
                                            foreach (var oDeduccionAfpIter in oDeduccionAfp)
                                            {
                                                totalAFP += Math.Round((decimal)oDeduccionAfpIter.dafp_AporteLps, 2);
                                                //pasar el estado del aporte a pagado
                                                oDeduccionAfpIter.dafp_Pagado = true;
                                                db.Entry(oDeduccionAfpIter).State = EntityState.Modified;

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oDeduccionAfpIter.tbAFP.afp_Descripcion,
                                                    monto = Math.Round(oDeduccionAfpIter.dafp_AporteLps, 2)
                                                });
                                            }
                                        }

                                        //deducciones extras
                                        List<tbDeduccionesExtraordinarias> oDeduccionesExtrasColaborador = db.tbDeduccionesExtraordinarias.Where(DEX => DEX.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id && DEX.dex_MontoRestante > 0 && DEX.dex_Activo == true).ToList();

                                        if (oDeduccionesExtrasColaborador.Count > 0)
                                        {
                                            //sumarlas todas
                                            foreach (var oDeduccionesExtrasColaboradorIterador in oDeduccionesExtrasColaborador)
                                            {
                                                totalOtrasDeducciones += oDeduccionesExtrasColaboradorIterador.dex_MontoRestante <= oDeduccionesExtrasColaboradorIterador.dex_Cuota ? oDeduccionesExtrasColaboradorIterador.dex_MontoRestante : oDeduccionesExtrasColaboradorIterador.dex_Cuota;
                                                //restar la cuota al monto restante
                                                oDeduccionesExtrasColaboradorIterador.dex_MontoRestante = oDeduccionesExtrasColaboradorIterador.dex_MontoRestante <= oDeduccionesExtrasColaboradorIterador.dex_Cuota ? oDeduccionesExtrasColaboradorIterador.dex_MontoRestante - oDeduccionesExtrasColaboradorIterador.dex_MontoRestante : oDeduccionesExtrasColaboradorIterador.dex_MontoRestante - oDeduccionesExtrasColaboradorIterador.dex_Cuota;
                                                db.Entry(oDeduccionesExtrasColaboradorIterador).State = EntityState.Modified;

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oDeduccionesExtrasColaboradorIterador.dex_ObservacionesComentarios,
                                                    monto = Math.Round((decimal)oDeduccionesExtrasColaboradorIterador.dex_Cuota, 2)
                                                });
                                            }
                                        }

                                        //adelantos de sueldo
                                        List<tbAdelantoSueldo> oAdelantosSueldo = db.tbAdelantoSueldo.Where(x => x.emp_Id == empleadoActual.emp_Id && x.adsu_Activo == true && x.adsu_Deducido == false).ToList();

                                        if (oAdelantosSueldo.Count > 0)
                                        {
                                            //sumarlas todas
                                            foreach (var oAdelantosSueldoIterador in oAdelantosSueldo)
                                            {
                                                adelantosSueldo += Math.Round((decimal)oAdelantosSueldoIterador.adsu_Monto, 2);
                                                //pasar el estado del adelanto a deducido
                                                oAdelantosSueldoIterador.adsu_Deducido = true;
                                                db.Entry(oAdelantosSueldoIterador).State = EntityState.Modified;

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"Adelanto sueldo ({oAdelantosSueldoIterador.adsu_RazonAdelanto})",
                                                    monto = Math.Round((decimal)oAdelantosSueldoIterador.adsu_Monto, 2)
                                                });
                                            }
                                        }

                                        totalDeduccionesEmpleado = Math.Round((decimal)totalOtrasDeducciones, 2) + totalInstitucionesFinancieras + colaboradorDeducciones + totalAFP;
                                        netoAPagarColaborador = totalIngresosEmpleado - totalDeduccionesEmpleado;

                                        #endregion

                                        #region crear registro de la planilla del colaborador para el reporte

                                        oPlanillaEmpleado.CodColaborador = InformacionDelEmpleadoActual.emp_Id.ToString();
                                        oPlanillaEmpleado.NombresColaborador = $"{empleadoActual.tbPersonas.per_Nombres} {empleadoActual.tbPersonas.per_Apellidos}";
                                        oPlanillaEmpleado.SalarioBase = SalarioBase;
                                        oPlanillaEmpleado.horasTrabajadas = horasTrabajadas;
                                        oPlanillaEmpleado.SalarioHora = salarioHora;
                                        oPlanillaEmpleado.totalSalario = totalSalario;
                                        oPlanillaEmpleado.tipoPlanilla = empleadoActual.tbCatalogoDePlanillas.cpla_DescripcionPlanilla;
                                        oPlanillaEmpleado.procentajeComision = porcentajeComision;
                                        oPlanillaEmpleado.totalVentas = totalVentas;
                                        oPlanillaEmpleado.totalComisiones = totalComisiones;
                                        oPlanillaEmpleado.horasExtras = horasExtrasTrabajadas;
                                        oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
                                        oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
                                        oPlanillaEmpleado.totalVacaciones = totalVacaciones;
                                        oPlanillaEmpleado.totalIngresos = Math.Round((decimal)totalIngresosEmpleado, 2);
                                        oPlanillaEmpleado.totalISR = 0;
                                        oPlanillaEmpleado.totalDeduccionesColaborador = colaboradorDeducciones;
                                        oPlanillaEmpleado.totalAFP = totalAFP;
                                        oPlanillaEmpleado.totalInstitucionesFinancieras = totalInstitucionesFinancieras;
                                        oPlanillaEmpleado.otrasDeducciones = Math.Round((decimal)totalOtrasDeducciones, 2);
                                        oPlanillaEmpleado.adelantosSueldo = Math.Round((decimal)adelantosSueldo, 2);
                                        oPlanillaEmpleado.totalDeducciones = Math.Round((decimal)totalDeduccionesEmpleado, 2);
                                        oPlanillaEmpleado.totalAPagar = Math.Round((decimal)netoAPagarColaborador, 2);
                                        reporte.Add(oPlanillaEmpleado);
                                        oPlanillaEmpleado = null;
                                        #endregion

                                        #region agregar registro al excel                                        

                                        //agregar registroo a la hoja de excel
                                        dt.Rows.Add(empleadoActual.tbPersonas.per_Nombres,
                                                    empleadoActual.tbPersonas.per_Apellidos,
                                                    SalarioBase,
                                                    totalBonificaciones,
                                                    totalComisiones,
                                                    totalOtrasDeducciones,
                                                    totalInstitucionesFinancieras,
                                                    0,
                                                    0,
                                                    0,
                                                    0,
                                                    netoAPagarColaborador);
                                        #endregion

                                        #region Enviar comprobante de pago por email
                                        if (enviarEmail!=null && enviarEmail ==true)
                                        {
                                            oComprobantePagoModel.EmailAsunto = "Comprobante de pago";
                                            oComprobantePagoModel.NombreColaborador = empleadoActual.tbPersonas.per_Nombres + " " + empleadoActual.tbPersonas.per_Apellidos;
                                            oComprobantePagoModel.idColaborador = empleadoActual.emp_Id;
                                            oComprobantePagoModel.EmailDestino = empleadoActual.tbPersonas.per_CorreoElectronico;
                                            oComprobantePagoModel.PeriodoPago = $"{fechaInicio.ToString("dd/MM/yyyy")}- {fechaFin.ToString("dd/MM/yyyy")}";
                                            oComprobantePagoModel.Ingresos = ListaIngresosVoucher;
                                            oComprobantePagoModel.Deducciones = ListaDeduccionesVoucher;
                                            oComprobantePagoModel.totalIngresos = totalIngresosEmpleado;
                                            oComprobantePagoModel.totalDeducciones = totalDeduccionesEmpleado;
                                            oComprobantePagoModel.NetoPagar = netoAPagarColaborador;

                                            //Enviar comprobante de pago
                                            try
                                            {
                                                if (!utilities.SendEmail(oComprobantePagoModel))
                                                    errores++;
                                                else
                                                {
                                                    ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
                                                    ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                errores++;
                                            }
                                        }                                        
                                        #endregion

                                        #region guardar en el historial de pago

                                        tbHistorialDePago oHistorialPagoEncabezado = new tbHistorialDePago();                                        

                                        oHistorialPagoEncabezado.hipa_IdHistorialDePago = db.tbHistorialDePago.Max(x => x.hipa_IdHistorialDePago) + contador;
                                        oHistorialPagoEncabezado.emp_Id = empleadoActual.emp_Id;
                                        oHistorialPagoEncabezado.hipa_SueldoNeto = Math.Round((decimal)netoAPagarColaborador, 2);

                                        oHistorialPagoEncabezado.hipa_FechaInicio = fechaInicio;
                                        oHistorialPagoEncabezado.hipa_FechaFin = fechaFin;

                                        oHistorialPagoEncabezado.hipa_FechaPago = DateTime.Now;
                                        oHistorialPagoEncabezado.hipa_Anio = DateTime.Now.Year;
                                        oHistorialPagoEncabezado.hipa_Mes = DateTime.Now.Month;

                                        oHistorialPagoEncabezado.peri_IdPeriodo = 1;

                                        oHistorialPagoEncabezado.hipa_UsuarioCrea = 1;
                                        oHistorialPagoEncabezado.hipa_FechaCrea = DateTime.Now;
                                        oHistorialPagoEncabezado.hipa_TotalISR = totalISR;
                                        oHistorialPagoEncabezado.hipa_ISRPendiente = true;
                                        oHistorialPagoEncabezado.hipa_AFP = totalAFP;
                                        db.tbHistorialDePago.Add(oHistorialPagoEncabezado);
                                        contador++;
                                        #endregion


                                    }
                                    //catch por si hubo un error al generar la planilla de un empleado
                                    catch (Exception ex)
                                    {
                                        // mensaje del error en el registro del colaborador
                                        dt.Rows.Add(empleadoActual.tbPersonas.per_Nombres + ' ' + empleadoActual.tbPersonas.per_Apellidos,
                                                    "Ocurrió un error al generar la planilla de este empleado.");
                                        errores++;

                                    }                                    
                                }
                                //guardar cambios en la bbdd
                                db.SaveChanges();
                                dbContextTransaccion.Commit();
                            }

                            //catch por si se produjo un error al procesar una sola planilla
                            catch (Exception ex)
                            {
                                // SI ALGO FALLA, HACER UN ROLLBACK
                                dbContextTransaccion.Rollback();
                                errores++;
                                dt.Rows.Add($"Ocurrió un error al generar la planilla {nombrePlanilla}.");
                            }
                        }
                    }
                }

                //enviar resultado al cliente
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = "success";
                //guardar archivo excel
                try
                {
                    oSLDocument.ImportDataTable(1, 1, dt, true);
                    oSLDocument.SaveAs(direccion);
                }
                catch (Exception ex)
                {
                    response.Response = "Planilla generada, error al crear documento excel.";
                    response.Encabezado = "Advertencia";
                    response.Tipo = "warning";
                }

            }
            // catch por si se produjo un error fatal en el proceso generar planilla
            catch (Exception ex)
            {
                response.Response = "El proceso de generación de planillas falló, contacte al adminstrador.";
                response.Encabezado = "Error";
                response.Tipo = "error";
            }
            //retornar resultado al cliente
            return Json (new { Data = reporte, Response = response },JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    class iziToast
    {
        public string Response { get; set; }
        public string Encabezado { get; set; }
        public string Tipo { get; set; }
    }
}