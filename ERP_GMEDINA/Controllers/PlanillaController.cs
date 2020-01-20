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
                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();

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
                            int idDetalleDeduccionHisotorialesContador = 1;
                            int idDetalleIngresoHisotorialesContador = 1;
                            //procesar planilla empleado por empleado
                            foreach (var empleadoActual in oEmpleados)
                            {
                                using (var dbContextTransaccion = db.Database.BeginTransaction())
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
                                        decimal? totalHorasPermiso = 0;
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
                                        //int VerificarHorasTrabajas = 0;
                                        oPlanillaEmpleado = new ReportePlanillaViewModel();
                                        //variables para insertar en los historiales de pago
                                        IEnumerable<object> listHistorialPago = null;
                                        string MensajeError = "";
                                        List<tbHistorialDeduccionPago> lisHistorialDeducciones = new List<tbHistorialDeduccionPago>();
                                        List<tbHistorialDeIngresosPago> lisHistorialIngresos = new List<tbHistorialDeIngresosPago>();


                                        #endregion

                                        #region Procesar ingresos

                                        //informacion del colaborador actual
                                        V_InformacionColaborador InformacionDelEmpleadoActual = db.V_InformacionColaborador.Where(x => x.emp_Id == empleadoActual.emp_Id).FirstOrDefault();

                                        //salario base del colaborador actual
                                        SalarioBase = Math.Round((Decimal)InformacionDelEmpleadoActual.SalarioBase, 2);

                                        //para el voucer
                                        //ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        //{
                                        //    concepto = "Sueldo base",
                                        //    monto = SalarioBase
                                        //});
                                        //Historial de ingresos (salario base)
                                        //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                        //{
                                        //    hip_UnidadesPagar = 1,
                                        //    hip_MedidaUnitaria = 1,
                                        //    hip_TotalPagar = SalarioBase,
                                        //    cin_IdIngreso = 7
                                        //});

                                        //horas normales trabajadas
                                        horasTrabajadas = db.tbHistorialHorasTrabajadas
                                            .Where( x => x.emp_Id == empleadoActual.emp_Id && 
                                                    x.htra_Estado == true &&
                                                    x.tbTipoHoras.tiho_Recargo == 0 &&
                                                    x.htra_Fecha >= fechaInicio &&
                                                    x.htra_Fecha <= fechaFin)
                                            .Select(x => x.htra_CantidadHoras)
                                            .DefaultIfEmpty(0)
                                            .Sum();
                                        ////para el voucher
                                        //ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        //{
                                        //    concepto = "Horas trabajadas",
                                        //    monto = horasTrabajadas
                                        //});

                                        //salario por hora
                                        salarioHora = Math.Round((Decimal)SalarioBase / 240, 2);

                                        //para el voucher
                                        //ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        //{
                                        //    concepto = "Sueldo por hora",
                                        //    monto = salarioHora
                                        //});

                                        //total salario
                                        totalSalario = Math.Round((Decimal)salarioHora * horasTrabajadas, 2);
                                        //para el voucer
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Salario ordinario",
                                            monto = totalSalario
                                        });
                                        //Historial de ingresos (horas normales trabajadas)
                                        //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                        //{
                                        //    hip_UnidadesPagar = horasTrabajadas,
                                        //    hip_MedidaUnitaria = 1,
                                        //    hip_TotalPagar = totalSalario,
                                        //    cin_IdIngreso = 11
                                        //});

                                        //horas con permiso justificado
                                        List<tbHistorialPermisos> horasConPermiso = db.tbHistorialPermisos
                                            .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                    x.hper_Estado == true &&
                                                    x.hper_fechaInicio >= fechaInicio &&
                                                    x.hper_fechaFin <= fechaFin)
                                            .ToList();

                                        if (horasConPermiso.Count > 0)
                                        {
                                            int CantidadHorasPermisoActual = 0;
                                            //sumar todas las horas extras
                                            foreach (var iterHorasPermiso in horasConPermiso)
                                            {
                                                CantidadHorasPermisoActual = iterHorasPermiso.hper_Duracion;

                                                totalHorasPermiso += Math.Round(CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100)), 2);


                                                //para el voucher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{CantidadHorasPermisoActual} horas permiso indemnizado {iterHorasPermiso.hper_PorcentajeIndemnizado} %",
                                                    monto = Math.Round(CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100)), 2)
                                                });

                                                //Historial de ingresos (horas con permiso)
                                                //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                //{
                                                //    hip_UnidadesPagar = CantidadHorasPermisoActual,
                                                //    hip_MedidaUnitaria = 1,
                                                //    hip_TotalPagar = Math.Round(CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100)), 2),
                                                //    cin_IdIngreso = 12
                                                //});
                                            }
                                        }

                                        //comisiones
                                        List<tbEmpleadoComisiones> oComisionesColaboradores = db.tbEmpleadoComisiones
                                                                                                .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                        x.cc_Activo == true &&
                                                                                                        x.cc_Pagado == false &&
                                                                                                        x.cc_FechaRegistro >= fechaInicio &&
                                                                                                        x.cc_FechaRegistro <= fechaFin).ToList();
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

                                                totalComisiones += Math.Round((Decimal)(oComisionesColaboradoresIterador.cc_TotalVenta * oComisionesColaboradoresIterador.cc_PorcentajeComision) / 100, 2);

                                                //pasar el estado de las comisiones a pagadas
                                                oComisionesColaboradoresIterador.cc_Pagado = true;
                                                oComisionesColaboradoresIterador.cc_FechaPagado = DateTime.Now;
                                                db.Entry(oComisionesColaboradoresIterador).State = EntityState.Modified;

                                                //agregarlas al vocher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oComisionesColaboradoresIterador.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                    monto = Math.Round((Decimal)(oComisionesColaboradoresIterador.cc_TotalVenta * oComisionesColaboradoresIterador.cc_PorcentajeComision) / 100, 2)
                                                });
                                                //Historial de ingresos (Comisiones)
                                                //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                //{
                                                //    hip_UnidadesPagar = 1,
                                                //    hip_MedidaUnitaria = 1,
                                                //    hip_TotalPagar = Math.Round((Decimal)(oComisionesColaboradoresIterador.cc_TotalVenta * oComisionesColaboradoresIterador.cc_PorcentajeComision) / 100, 2),
                                                //    cin_IdIngreso = 8
                                                //});
                                            }
                                        }

                                        //horas extras
                                        horasExtrasTrabajadas = db.tbHistorialHorasTrabajadas
                                            .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                    x.htra_Estado == true &&
                                                    x.tbTipoHoras.tiho_Recargo > 0 &&
                                                    x.htra_Fecha >= fechaInicio &&
                                                    x.htra_Fecha <= fechaFin)
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
                                                                                        .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                x.htra_Estado == true &&
                                                                                                x.tbTipoHoras.tiho_Recargo > 0 &&
                                                                                                x.htra_Fecha >= fechaInicio &&
                                                                                                x.htra_Fecha <= fechaFin)
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


                                                //para el voucher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{CantidadHorasExtrasActual} horas {iterHorasExtras.tbTipoHoras.tiho_Descripcion} al {iterHorasExtras.tbTipoHoras.tiho_Recargo} %",
                                                    monto = Math.Round((Decimal)CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2)
                                                });
                                                //Historial de ingresos (horas extras)
                                                //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                //{
                                                //    hip_UnidadesPagar = 1,
                                                //    hip_MedidaUnitaria = 1,
                                                //    hip_TotalPagar = Math.Round((Decimal)CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2),
                                                //    cin_IdIngreso = 3
                                                //});
                                            }
                                            //para el voucher
                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = "Total horas extras",
                                                monto = totalHorasExtras
                                            });
                                        }

                                        //bonos del colaborador
                                        List<tbEmpleadoBonos> oBonosColaboradores = db.tbEmpleadoBonos
                                                                                    .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                            x.cb_Activo == true &&
                                                                                            x.cb_Pagado == false &&
                                                                                            x.cb_FechaRegistro >= fechaInicio &&
                                                                                            x.cb_FechaRegistro <= fechaFin).ToList();

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
                                                //Historial de ingresos (bonos)
                                                lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                {
                                                    hip_UnidadesPagar = 1,
                                                    hip_MedidaUnitaria = 1,
                                                    hip_TotalPagar = Math.Round((Decimal)oBonosColaboradoresIterador.cb_Monto, 2),
                                                    cin_IdIngreso = oBonosColaboradoresIterador.cin_IdIngreso
                                                });
                                            }
                                        }

                                        //vacaciones
                                        List<tbHistorialVacaciones> oVacacionesColaboradores =  db.tbHistorialVacaciones
                                                                                                .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                        x.hvac_DiasPagados == false &&
                                                                                                        x.hvac_Estado == true &&
                                                                                                        x.hvac_FechaInicio >= fechaInicio &&
                                                                                                        x.hvac_FechaFin <= fechaFin).ToList();
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
                                                oVacacionesColaboradoresIterador.hvac_DiasPagados = true;
                                                db.Entry(oVacacionesColaboradoresIterador).State = EntityState.Modified;

                                                //agregarlas al vocher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{cantidadDias} dias de vacaciones",
                                                    monto = Math.Round((Decimal)(salarioHora * 8) * cantidadDias, 2)
                                                });
                                                //Historial de ingresos (vacaciones)
                                                //lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                //{
                                                //    hip_UnidadesPagar = cantidadDias,
                                                //    hip_MedidaUnitaria = 1,
                                                //    hip_TotalPagar = Math.Round((Decimal)(salarioHora * 8) * cantidadDias, 2),
                                                //    cin_IdIngreso = 12
                                                //});
                                            }
                                        }
                                        #region Septimo Dia
                                        DateTime inicioFecha = fechaInicio;
                                        DateTime finFecha = fechaFin;
                                        TimeSpan restaFechasSeptimo = finFecha - inicioFecha;
                                        int cantidadDiasSeptimo = restaFechasSeptimo.Days + 1;
                                        DateTime fechaIterador = inicioFecha;
                                        int cantHoras = 0;
                                        int cantHorasPermiso = 0;
                                        int cantidadSeptimosDias = 0;
                                        int contadorSeptimosDias = 1;

                                        for (int i = 1; i <= cantidadDiasSeptimo; i++)
                                        {
                                            if (fechaIterador.DayOfWeek.ToString() != "Sunday")
                                            {
                                                cantHoras+= db.tbHistorialHorasTrabajadas
                                                            .Where( x => x.htra_Fecha == fechaIterador &&
                                                                    x.emp_Id == empleadoActual.emp_Id &&
                                                                    x.htra_Estado == true)
                                                            .Select(x => x.htra_CantidadHoras)
                                                            .FirstOrDefault();

                                                cantHorasPermiso += db.tbHistorialPermisos
                                                                    .Where( x => x.hper_fechaInicio <= fechaIterador &&
                                                                            x.hper_fechaFin >= fechaIterador &&
                                                                            x.emp_Id == empleadoActual.emp_Id)
                                                                    .Select(x => x.hper_Duracion)
                                                                    .FirstOrDefault();

                                                if ((cantHoras + (cantHorasPermiso * 8)) >= 48 && contadorSeptimosDias == 7)
                                                {
                                                    cantidadSeptimosDias++;
                                                    contadorSeptimosDias = 0;
                                                    cantHoras = 0;
                                                }
                                            }
                                            if (contadorSeptimosDias == 7)
                                            {
                                                cantHoras = 0;
                                                contadorSeptimosDias = 0;
                                            }
                                            fechaIterador = fechaIterador.Add(new TimeSpan(1, 0, 0, 0, 0));
                                            contadorSeptimosDias++;
                                        }

                                        decimal resultSeptimoDia = (salarioHora * 8) * cantidadSeptimosDias;
                                        if (resultSeptimoDia > 0)
                                        {
                                            //agregarlas al vocher
                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = $"{cantidadSeptimosDias}x séptimo día",
                                                monto = Math.Round(resultSeptimoDia, 2)
                                            });
                                        }
                                        #endregion

                                        //total ingresos
                                        totalIngresosEmpleado = totalSalario + totalComisiones + totalHorasExtras + totalBonificaciones + totalVacaciones + totalHorasPermiso + resultSeptimoDia;

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
                                            //Voucher
                                            ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                            {
                                                concepto = iterDeducciones.cde_DescripcionDeduccion,
                                                monto = Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2)
                                            });

                                            //Historial de deducciones
                                            lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                            {
                                                cde_IdDeducciones = iterDeducciones.cde_IdDeducciones,
                                                hidp_Total = Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2)
                                            });
                                        }

                                        //instituciones financieras
                                        List<tbDeduccionInstitucionFinanciera> oDeduInstiFinancieras =  db.tbDeduccionInstitucionFinanciera
                                                                                                        .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                                x.deif_Activo == true &&
                                                                                                                x.deif_Pagado == false &&
                                                                                                                x.deif_FechaCrea >= fechaInicio &&
                                                                                                                x.deif_FechaCrea <= fechaFin).ToList();

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
                                                //Historial de deducciones
                                                lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                {
                                                    cde_IdDeducciones = oDeduInstiFinancierasIterador.cde_IdDeducciones,
                                                    hidp_Total = Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2)
                                                });
                                            }
                                        }
                                        //afp
                                        List<tbDeduccionAFP> oDeduccionAfp = db.tbDeduccionAFP
                                                                            .Where( af => af.emp_Id == empleadoActual.emp_Id &&
                                                                                    af.dafp_Pagado == false &&
                                                                                    af.dafp_Activo == true &&
                                                                                    af.dafp_FechaCrea >= fechaInicio &&
                                                                                    af.dafp_FechaCrea <= fechaFin)
                                                                            .ToList();

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
                                        List<tbDeduccionesExtraordinarias> oDeduccionesExtrasColaborador =  db.tbDeduccionesExtraordinarias
                                                                                                            .Where( DEX => DEX.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id &&
                                                                                                                    DEX.dex_MontoRestante > 0 &&
                                                                                                                    DEX.dex_Activo == true).ToList();

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

                                                //Historial de deducciones
                                                lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                {
                                                    cde_IdDeducciones = oDeduccionesExtrasColaboradorIterador.cde_IdDeducciones,
                                                    hidp_Total = Math.Round((decimal)oDeduccionesExtrasColaboradorIterador.dex_Cuota, 2)
                                                });
                                            }
                                        }

                                        //adelantos de sueldo
                                        List<tbAdelantoSueldo> oAdelantosSueldo  =  db.tbAdelantoSueldo
                                                                                    .Where( x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                            x.adsu_Activo == true && x.adsu_Deducido == false &&
                                                                                            x.adsu_FechaAdelanto >= fechaInicio &&
                                                                                            x.adsu_FechaAdelanto <= fechaFin)
                                                                                    .ToList();

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
                                                //Historial de deducciones
                                                //lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                //{
                                                //    cde_IdDeducciones = 9,
                                                //    hidp_Total = Math.Round((decimal)oAdelantosSueldoIterador.adsu_Monto, 2)
                                                //});
                                            }
                                        }

                                        //totales
                                        totalDeduccionesEmpleado = Math.Round((decimal)totalOtrasDeducciones, 2) + totalInstitucionesFinancieras + colaboradorDeducciones + totalAFP + adelantosSueldo;
                                        netoAPagarColaborador = totalIngresosEmpleado - totalDeduccionesEmpleado;

                                        #endregion

                                        //ISR
                                        #region ISR

                                        #region Declaracion de Variables
                                        decimal SalarioMinimo = 9443.24M;
                                        int AnioActual = DateTime.Now.Year;
                                        decimal? TotalBonos = 0;
                                        decimal? TotalHrsExtra = 0;
                                        decimal? TotalComisiones = 0;
                                        decimal? ExcesoDecimoTercer = 0;
                                        decimal? ExcesoVacaciones = 0;
                                        decimal? ExcesoDecimoCuarto = 0;
                                        decimal Exceso = 0;
                                        decimal SueldoAnual = 0;
                                        decimal GastosMedicos = 0;
                                        decimal TotalIngresosGravables = 0;
                                        decimal TotalDeduccionesGravables = 0;
                                        decimal RentaNetaGravable = 0;
                                        var tablaEmp = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id).OrderBy(x => x.sue_FechaCrea);
                                        #endregion

                                        #region Sueldo Promedio Anual
                                        //Sueldo redondeado del Colaborador
                                        DateTime AnioActualEnero = new DateTime(DateTime.Now.Year, 1, 1);

                                        //Obtener los pagos mensuales totales
                                        var mesesPago = (db.tbHistorialDePago
                                            .Where(x => x.emp_Id == empleadoActual.emp_Id && x.hipa_Anio == AnioActualEnero.Year)
                                            .OrderBy(x => x.hipa_Mes)
                                            .GroupBy(x => x.hipa_Mes)
                                            .Select(x => x.Sum(y => (Decimal)y.hipa_SueldoNeto))).ToList();

                                        DateTime FechaIngresoEmpleado = db.tbEmpleados
                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id)
                                                                        .Select(x => x.emp_Fechaingreso).FirstOrDefault();
                                        bool esMensual = true;

                                        TimeSpan diferencia = AnioActualEnero - FechaIngresoEmpleado;

                                        if (TimeSpan.Zero > diferencia)
                                            esMensual = true;


                                        //Saber que mes entro
                                        int mes = FechaIngresoEmpleado.Month;
                                        decimal SalarioPromedioAnualPagadoAlAnio = 0;
                                        decimal salarioPromedioAnualPagadoAlMes = 0;
                                        decimal TotalSalarioAnual = SalarioPromedioAnualISR(netoAPagarColaborador,
                                        mesesPago,
                                        esMensual,
                                        ref SalarioPromedioAnualPagadoAlAnio,
                                        ref salarioPromedioAnualPagadoAlMes);
                                        #endregion

                                        #region Excesos
                                        //-----------------------------------------------------------------------------------------------------------------------------
                                        //Exceso Décimo Tercer Mes
                                        //Variable para llamar en duro los empleados con Décimo Tercer Mes
                                        var DecimoTercer = db.V_DecimoTercerMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dtm_FechaPago.Year == AnioActual).FirstOrDefault();

                                        //Validar primero si es en el año actual el proceso de este calculo
                                        if (DecimoTercer != null)
                                        {
                                            //Salario Mínimo Mensual por 10 Meses (Según SAR)
                                            Exceso = SalarioMinimo * 10;

                                            //Validar si el Décimo Tercer es mayor al Exceso
                                            if (DecimoTercer.dtm_Monto > Exceso)
                                            {
                                                ExcesoDecimoTercer = DecimoTercer.dtm_Monto - Exceso;
                                            }
                                            else
                                            {
                                                ExcesoDecimoTercer = 0;
                                            }
                                        }
                                        //-----------------------------------------------------------------------------------------------------------------------------


                                        //-----------------------------------------------------------------------------------------------------------------------------
                                        //Exceso Décimo Cuarto Mes
                                        //Variable para llamar en duro los empleados con Décimo Cuarto Mes
                                        var DecimoCuarto = db.V_DecimoCuartoMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dcm_FechaPago.Year == AnioActual).FirstOrDefault();

                                        //Validar primero si es en el año actual el proceso de este calculo
                                        if (DecimoCuarto != null)
                                        {
                                            //Salario Mínimo Mensual por 10 Meses (Según SAR)
                                            Exceso = SalarioMinimo * 10;

                                            //Validar si el Décimo Cuarto es mayor al Exceso
                                            if (DecimoCuarto.dcm_Monto > Exceso)
                                            {
                                                ExcesoDecimoCuarto = DecimoCuarto.dcm_Monto - Exceso;
                                            }
                                            else
                                            {
                                                ExcesoDecimoCuarto = 0;
                                            }
                                        }
                                        //-----------------------------------------------------------------------------------------------------------------------------


                                        //-----------------------------------------------------------------------------------------------------------------------------
                                        //Exceso Vacaciones
                                        //Variable para llamar en duro las Vacaciones Pagadas del Historial de Ingresos de Pago
                                        var objVacaciones = db.tbHistorialVacaciones.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hvac_AnioVacaciones && x.hvac_DiasPagados == true).Select(x => x.hvac_CantDias).FirstOrDefault();

                                        //Validar si los dias a Pagar es mayor a 30 dias 
                                        if (objVacaciones > 30)
                                        {
                                            ExcesoVacaciones = ((objVacaciones - 30) * (SueldoAnual / 360));
                                        }
                                        else
                                        {
                                            ExcesoVacaciones = 0;
                                        }

                                        #endregion

                                        #region Gastos Médicos
                                        //Variable para llamar en duro el monto de Gastos Médicos de 40,000.00
                                        var objAcumuladosISRMenor = db.tbAcumuladosISR.Where(x => x.aisr_Activo && x.aisr_Id == 1).FirstOrDefault();

                                        //Variable para llamar en duro el monto de Gastos Médicos de 70,000.00
                                        var objAcumuladosISRMayor = db.tbAcumuladosISR.Where(x => x.aisr_Activo && x.aisr_Id == 2).FirstOrDefault();

                                        //Variable para llamar en duro el monto de Gastos Médicos de 40,000.00
                                        var objEmpleados = db.tbEmpleados.Where(x => x.emp_Id == empleadoActual.emp_Id).Include(x => x.tbPersonas).Where(x => x.tbPersonas.per_Estado == true).FirstOrDefault();

                                        //Validar si el Empleado tiene menos de 60 años para saber cuanto se le cobrará de Gastos Médicos
                                        if (objEmpleados.tbPersonas.per_Edad < 60)
                                        {
                                            GastosMedicos = objAcumuladosISRMenor.aisr_Monto;
                                        }
                                        else
                                        {
                                            GastosMedicos = objAcumuladosISRMayor.aisr_Monto;
                                        }

                                        #endregion

                                        TotalBonos = db.tbEmpleadoBonos.Where(x => x.emp_Id == empleadoActual.emp_Id && x.cb_Pagado == true && AnioActualEnero == x.cb_FechaPagado).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.cb_Monto)).FirstOrDefault();
                                        TotalHrsExtra = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalHorasExtras)).FirstOrDefault();
                                        TotalComisiones = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalComisiones)).FirstOrDefault();

                                        if (TotalBonos == null)
                                            TotalBonos = 0;

                                        if (TotalHrsExtra == null)
                                            TotalHrsExtra = 0;

                                        if (TotalComisiones == null)
                                            TotalComisiones = 0;

                                        #region Total ISR Calcular
                                        //-----------------------------------------------------------------------------------------------------------------------------
                                        //Total Ingresos Gravables
                                        TotalIngresosGravables = TotalSalarioAnual + (Decimal)ExcesoDecimoTercer + (Decimal)ExcesoDecimoCuarto + (Decimal)ExcesoVacaciones + (Decimal)TotalBonos + (Decimal)TotalHrsExtra + (Decimal)TotalComisiones;

                                        //Total Deducciones Gravables
                                        TotalDeduccionesGravables = GastosMedicos;

                                        //Renta Neta Gravable
                                        RentaNetaGravable = TotalIngresosGravables - TotalDeduccionesGravables;

                                        #region ISR Dinámico en Proceso
                                        //Tabla Progresiva ISR Dinámica

                                        /*List<tbISR> objDeduccionISR = db.tbISR.Where(x => x.isr_Activo == true).ToList();

                                        foreach(var oISR in objDeduccionISR)
                                        {
                                            if ()
                                            {

                                            }
                                        }*/
                                        #endregion

                                        #region Tabla Progresiva para Deducir ISR
                                        //Cálculo con la Tabla Progresiva ISR

                                        //Variable para llamar cada uno de los Porcentajes y Techos del ISR
                                        var objISRExcento = db.tbISR.Where(x => x.isr_Id == 1).FirstOrDefault();
                                        var objISRBajo = db.tbISR.Where(x => x.isr_Id == 2).FirstOrDefault();
                                        var objISRMedio = db.tbISR.Where(x => x.isr_Id == 3).FirstOrDefault();
                                        var objISRAlto = db.tbISR.Where(x => x.isr_Id == 4).FirstOrDefault();

                                        if (RentaNetaGravable > objISRAlto.isr_RangoInicial)
                                        {
                                            totalISR = (RentaNetaGravable - objISRAlto.isr_RangoInicial) *
                                                       (objISRAlto.isr_Porcentaje / 100) + (objISRMedio.isr_RangoFinal - objISRMedio.isr_RangoInicial) *
                                                       (objISRMedio.isr_Porcentaje / 100) + (objISRBajo.isr_RangoFinal - objISRBajo.isr_RangoInicial) *
                                                       (objISRBajo.isr_Porcentaje / 100);
                                        }
                                        else if (RentaNetaGravable > objISRMedio.isr_RangoInicial)
                                        {
                                            totalISR = (RentaNetaGravable - objISRMedio.isr_RangoInicial) *
                                                       (objISRMedio.isr_Porcentaje / 100) + (objISRBajo.isr_RangoFinal - objISRBajo.isr_RangoInicial) *
                                                       (objISRBajo.isr_Porcentaje / 100);
                                        }
                                        else if (RentaNetaGravable > objISRBajo.isr_RangoInicial)
                                        {
                                            totalISR = (RentaNetaGravable - objISRBajo.isr_RangoInicial) *
                                                       (objISRBajo.isr_Porcentaje / 100);
                                        }
                                        else if (RentaNetaGravable <= objISRExcento.isr_RangoFinal)
                                        {
                                            totalISR = (RentaNetaGravable - objISRExcento.isr_RangoFinal) *
                                                       (objISRExcento.isr_Porcentaje / 100);
                                        }
                                        #endregion

                                        #endregion

                                        #endregion
                                        //Pendiente testeo
                                        //netoAPagarColaborador = netoAPagarColaborador - totalISR;

                                        #region Enviar comprobante de pago por email
                                        if (enviarEmail != null && enviarEmail == true)
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

                                        //Nuevos campos
                                        oHistorialPagoEncabezado.hipa_TotalHorasConPermisoJustificado = totalHorasPermiso;
                                        oHistorialPagoEncabezado.hipa_TotalComisiones = totalComisiones;
                                        oHistorialPagoEncabezado.hipa_TotalHorasExtras = totalHorasExtras;
                                        oHistorialPagoEncabezado.hipa_TotalVacaciones = totalVacaciones;
                                        oHistorialPagoEncabezado.hipa_TotalSeptimoDia = resultSeptimoDia;
                                        oHistorialPagoEncabezado.hipa_AdelantoSueldo = adelantosSueldo;
                                        oHistorialPagoEncabezado.hipa_TotalSalario = totalSalario;

                                        //Ejecutar el procedimiento almacenado
                                        listHistorialPago = db.UDP_Plani_tbHistorialDePago_Insert(oHistorialPagoEncabezado.emp_Id,
                                                                                                oHistorialPagoEncabezado.hipa_SueldoNeto,
                                                                                                oHistorialPagoEncabezado.hipa_FechaInicio,
                                                                                                oHistorialPagoEncabezado.hipa_FechaFin,
                                                                                                oHistorialPagoEncabezado.hipa_FechaPago,
                                                                                                oHistorialPagoEncabezado.hipa_Anio,
                                                                                                oHistorialPagoEncabezado.hipa_Mes,
                                                                                                oHistorialPagoEncabezado.peri_IdPeriodo,
                                                                                                oHistorialPagoEncabezado.hipa_UsuarioCrea,
                                                                                                oHistorialPagoEncabezado.hipa_FechaCrea,
                                                                                                oHistorialPagoEncabezado.hipa_TotalISR,
                                                                                                oHistorialPagoEncabezado.hipa_ISRPendiente,
                                                                                                oHistorialPagoEncabezado.hipa_AFP,
                                                                                                // nuevos campos
                                                                                                oHistorialPagoEncabezado.hipa_TotalHorasConPermisoJustificado,
                                                                                                oHistorialPagoEncabezado.hipa_TotalComisiones,
                                                                                                oHistorialPagoEncabezado.hipa_TotalHorasExtras,
                                                                                                oHistorialPagoEncabezado.hipa_TotalVacaciones,
                                                                                                oHistorialPagoEncabezado.hipa_TotalSeptimoDia,
                                                                                                oHistorialPagoEncabezado.hipa_AdelantoSueldo,
                                                                                                oHistorialPagoEncabezado.hipa_TotalSalario
                                                                                                );

                                        //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                                        foreach (UDP_Plani_tbHistorialDePago_Insert_Result Resultado in listHistorialPago)
                                            MensajeError = Resultado.MensajeError;


                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE                                            
                                            errores++;
                                        }
                                        //si el encabezado del historial de pago se registró correctamente, guardar los detalles
                                        else
                                        {
                                            //guardar en el detalle de deducciones del historial de pago
                                            foreach (var hisorialDeduccioneIterado in lisHistorialDeducciones)
                                            {
                                                int idDetalle = db.tbHistorialDeduccionPago.DefaultIfEmpty().Max(x => x.hidp_IdHistorialdeDeduPago);
                                                hisorialDeduccioneIterado.hidp_IdHistorialdeDeduPago = idDetalle != null ? idDetalle + idDetalleDeduccionHisotorialesContador : 1;
                                                hisorialDeduccioneIterado.hipa_IdHistorialDePago = int.Parse(MensajeError);
                                                hisorialDeduccioneIterado.hidp_UsuarioCrea = 1;
                                                hisorialDeduccioneIterado.hidp_FechaCrea = DateTime.Now;
                                                db.tbHistorialDeduccionPago.Add(hisorialDeduccioneIterado);
                                                idDetalleDeduccionHisotorialesContador++;

                                            }
                                            //guardar en el detalle de ingresos del historial de pago
                                            foreach (var hisorialIngresosIterado in lisHistorialIngresos)
                                            {
                                                int idDetalle = db.tbHistorialDeIngresosPago.DefaultIfEmpty().Max(x => x.hip_IdHistorialDeIngresosPago);
                                                hisorialIngresosIterado.hip_IdHistorialDeIngresosPago = idDetalle != null ? idDetalle + idDetalleIngresoHisotorialesContador : 1;
                                                hisorialIngresosIterado.hipa_IdHistorialDePago = int.Parse(MensajeError);
                                                hisorialIngresosIterado.hip_FechaInicio = fechaInicio;
                                                hisorialIngresosIterado.hip_FechaFinal = fechaFin;
                                                hisorialIngresosIterado.hip_UsuarioCrea = 1;
                                                hisorialIngresosIterado.hip_FechaCrea = DateTime.Now;
                                                db.tbHistorialDeIngresosPago.Add(hisorialIngresosIterado);
                                                idDetalleIngresoHisotorialesContador++;
                                            }
                                        }


                                        contador++;
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
                                        oPlanillaEmpleado.totalHorasPermiso = totalHorasPermiso;
                                        oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
                                        oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
                                        oPlanillaEmpleado.totalVacaciones = totalVacaciones;
                                        oPlanillaEmpleado.totalIngresos = Math.Round((decimal)totalIngresosEmpleado, 2);
                                        oPlanillaEmpleado.totalISR = totalISR;
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


                                        //guardar cambios en la bbdd
                                        db.SaveChanges();
                                        dbContextTransaccion.Commit();
                                    }
                                    //catch por si hubo un error al generar la planilla de un empleado
                                    catch (Exception ex)
                                    {
                                        // si hay un error, hacer un rollback
                                        dbContextTransaccion.Rollback();
                                        // mensaje del error en el registro del colaborador
                                        errores++;
                                    }
                                }
                            }

                        }
                        //catch por si se produjo un error al procesar una sola planilla
                        catch (Exception ex)
                        {
                            errores++;
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
                    //aqui guardar el excel
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
            return Json(new { Data = reporte, Response = response }, JsonRequestBehavior.AllowGet);
        }

        private static decimal SalarioPromedioAnualISR(decimal? netoAPagarColaborador, List<decimal> mesesPago, bool esMensual, ref decimal SalarioPromedioAnualPagadoAlAnio, ref decimal salarioPromedioAnualPagadoAlMes)
        {
            decimal sueldoProyeccion = 0;
            if (esMensual)
            {
                //Si es el primer mes a cobrar
                if (mesesPago == null)
                    sueldoProyeccion = ((netoAPagarColaborador * 12)) ?? 0;


                int cantidadMesesPagados = mesesPago.Count;
                //if (netoAPagarColaborador == 0)
                //    netoAPagarColaborador = 37350.66M;
                mesesPago.Add((Decimal)netoAPagarColaborador);

                decimal promedioMesesPago = mesesPago.Average();




                //TODO: faltan los meses que no se le va a pagar
                //Sacar el sueldo de los meses restantes

                for (int i = cantidadMesesPagados; i <= 12; i++)
                {
                    sueldoProyeccion += promedioMesesPago;
                }


                salarioPromedioAnualPagadoAlMes = sueldoProyeccion;
            }
            else
            {
                if (DateTime.Now.Month == 12)
                    //Calcular todas las fechas de este año, aunque haya entrado 
                    SalarioPromedioAnualPagadoAlAnio = mesesPago.Sum();
            }


            return (salarioPromedioAnualPagadoAlMes > 0) ? salarioPromedioAnualPagadoAlMes : SalarioPromedioAnualPagadoAlAnio;
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