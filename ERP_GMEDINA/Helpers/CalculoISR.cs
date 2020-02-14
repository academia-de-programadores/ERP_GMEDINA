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


namespace ERP_GMEDINA.Helpers
{
    public static class CalculoISR
    {
        public static decimal CalcularISR(ERP_GMEDINAEntities db, tbEmpleados empleadoActual, decimal totalSalario, decimal totalISR, int dias)
        {
            #region Cálculo de ISR
            try
            {
                #region Declaracion de Variables
                int AnioActual = DateTime.Now.Year;
                decimal? TotalBonos = 0;
                decimal? TotalHrsExtra = 0;
                decimal? TotalComisiones = 0;
                decimal? TotalIngresosExtras = 0;
                decimal? TotalDeduccionesEquipoTrabajo = 0;
                decimal? TotalDeduccionesExtras = 0;
                decimal? TotalDeduccionesAFP = 0;
                decimal? TotalDeduccionesInstitucionesFinancieras = 0;
                decimal? ExcesoDecimoTercer = 0;
                decimal? ExcesoVacaciones = 0;
                decimal? ExcesoDecimoCuarto = 0;
                decimal Exceso = 0;
                decimal? AcumuladosISR = 0;
                decimal TotalIngresosGravables = 0;
                decimal TotalDeduccionesGravables = 0;
                decimal RentaNetaGravable = 0;
                decimal SalarioMinimo = db.tbEmpresas.Select(x => x.empr_SalarioMinimo).FirstOrDefault() ?? 0;
                #endregion

                #region Sueldo Promedio Anual
                //Sueldo redondeado del Colaborador
                DateTime AnioActualEnero = new DateTime(DateTime.Now.Year, 1, 1);

                //Obtener los pagos mensuales totales
                var mesesPago = (db.tbHistorialDePago
                    .Where(x => x.emp_Id == empleadoActual.emp_Id && x.hipa_Anio == AnioActualEnero.Year)
                    .OrderBy(x => x.hipa_Mes)
                    .GroupBy(x => x.hipa_Mes)
                    .Select(x => x.Sum(y => (Decimal)y.hipa_TotalSueldoBruto))).ToList();

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
                decimal TotalSalarioAnual = SalarioPromedioAnualISR(totalSalario,
                mesesPago,
                esMensual,
                ref SalarioPromedioAnualPagadoAlAnio,
                ref salarioPromedioAnualPagadoAlMes);
                #endregion

                #region Excesos
                ////-----------------------------------------------------------------------------------------------------------------------------
                //Exceso Décimo Tercer Mes
                //Variable para los empleados con Décimo Tercer Mes
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
                ////-----------------------------------------------------------------------------------------------------------------------------


                ////-----------------------------------------------------------------------------------------------------------------------------
                //Exceso Décimo Cuarto Mes
                //Variable para los empleados con Décimo Cuarto Mes
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
                ////-----------------------------------------------------------------------------------------------------------------------------


                ////-----------------------------------------------------------------------------------------------------------------------------
                //Exceso Vacaciones
                //Variable para las Vacaciones Pagadas del Historial de Ingresos de Pago
                var objVacaciones = db.tbHistorialVacaciones.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hvac_AnioVacaciones && x.hvac_DiasPagados == true).Select(x => x.hvac_CantDias).FirstOrDefault();

                //Validar si los dias a Pagar es mayor a 30 dias 
                if (objVacaciones > 30)
                {
                    ExcesoVacaciones = ((objVacaciones - 30) * (TotalSalarioAnual / 360));
                }
                else
                {
                    ExcesoVacaciones = 0;
                }

                #endregion

                #region Acumulados ISR

                //Variable para obtener los registros de los Acumulados ISR del empleado actual
                AcumuladosISR = db.tbAcumuladosISR.Where(x => x.aisr_Activo == true && x.emp_Id == empleadoActual.emp_Id).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.aisr_Monto)).FirstOrDefault();

                #endregion

                #region Otros Ingresos

                TotalBonos = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalBonos)).FirstOrDefault();
                TotalHrsExtra = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalHorasExtras)).FirstOrDefault();
                TotalComisiones = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalComisiones)).FirstOrDefault();
                TotalIngresosExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalIngresosIndividuales)).FirstOrDefault();

                if (TotalBonos == null)
                    TotalBonos = 0;

                if (TotalHrsExtra == null)
                    TotalHrsExtra = 0;

                if (TotalComisiones == null)
                    TotalComisiones = 0;

                if (TotalIngresosExtras == null)
                    TotalIngresosExtras = 0;

                #endregion

                #region Otras Deducciones (Posible cálculo para el ISR)

                #region Deducciones por Equipo de Trabajo
                //DET = Deducción por Equipo de Trabajo
                decimal? MontoInicialDET = 0;
                decimal? MontoRestanteDET = 0;
                List<tbDeduccionesExtraordinarias> objDeduccionesExtraordinarias = db.tbDeduccionesExtraordinarias.Where(x => x.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id && x.dex_Activo == true && x.dex_DeducirISR == true)
                                                                                                                  .ToList();
                if (objDeduccionesExtraordinarias.Count() > 0)
                {
                    foreach (var oDET in objDeduccionesExtraordinarias)
                    {
                        MontoInicialDET = MontoInicialDET + oDET.dex_MontoInicial;
                        MontoRestanteDET = MontoRestanteDET + oDET.dex_MontoRestante;
                    }
                }
                if (MontoInicialDET > 0 && MontoRestanteDET > 0)
                {
                    TotalDeduccionesEquipoTrabajo = MontoInicialDET - MontoRestanteDET;
                }
                else
                {
                    TotalDeduccionesEquipoTrabajo = 0;
                }
                #endregion

                #region Deducciones Extras
                List<tbDeduccionesIndividuales> objDeduccionIndividual = db.tbDeduccionesIndividuales.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dei_Activo == true && x.dei_DeducirISR == true)
                                                                                                     .ToList();
                if (objDeduccionIndividual.Count() > 0)
                {
                    foreach (var oDeduIndi in objDeduccionIndividual)
                    {
                        if (oDeduIndi.dei_DeducirISR == true)
                        {
                            TotalDeduccionesExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalDeduccionesIndividuales)).FirstOrDefault();
                        }
                    }
                }
                #endregion

                #region Deducciones AFP
                List<tbDeduccionAFP> objDeduccionAFP = db.tbDeduccionAFP.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dafp_Activo == true && x.dafp_DeducirISR == true)
                                                                        .ToList();
                if (objDeduccionAFP.Count() > 0)
                {
                    foreach (var oDeduAFP in objDeduccionAFP)
                    {
                        if (oDeduAFP.dafp_DeducirISR == true)
                        {
                            TotalDeduccionesAFP = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_AFP)).FirstOrDefault();
                        }
                    }
                }
                #endregion

                #region Deducciones Instituciones Financieras
                //DIF = Deducción Institución Financiera
                List<tbDeduccionInstitucionFinanciera> objDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Where(x => x.emp_Id == empleadoActual.emp_Id && x.deif_Activo == true && x.deif_DeducirISR == true)
                                                                                                                              .ToList();
                if (objDeduccionInstitucionFinanciera.Count() > 0)
                {
                    foreach (var oDIF in objDeduccionInstitucionFinanciera)
                    {
                        if (oDIF.deif_Monto > 0)
                        {
                            TotalDeduccionesInstitucionesFinancieras = TotalDeduccionesInstitucionesFinancieras + oDIF.deif_Monto;
                        }
                        else
                        {
                            TotalDeduccionesInstitucionesFinancieras = 0;
                        }
                    }
                }
                #endregion

                if (TotalDeduccionesEquipoTrabajo == null)
                    TotalDeduccionesEquipoTrabajo = 0;

                if (TotalDeduccionesExtras == null)
                    TotalDeduccionesExtras = 0;

                if (TotalDeduccionesAFP == null)
                    TotalDeduccionesAFP = 0;

                if (TotalDeduccionesInstitucionesFinancieras == null)
                    TotalDeduccionesInstitucionesFinancieras = 0;

                #endregion

                #region Calculo del ISR
                //-----------------------------------------------------------------------------------------------------------------------------
                //Total Ingresos Gravables
                TotalIngresosGravables = TotalSalarioAnual + (Decimal)ExcesoDecimoTercer + (Decimal)ExcesoDecimoCuarto + (Decimal)ExcesoVacaciones + (Decimal)TotalBonos + (Decimal)TotalHrsExtra + (Decimal)TotalComisiones + (Decimal)TotalIngresosExtras;

                //Total Deducciones Gravables
                TotalDeduccionesGravables = (Decimal)AcumuladosISR + (Decimal)TotalDeduccionesEquipoTrabajo + (Decimal)TotalDeduccionesExtras + (Decimal)TotalDeduccionesAFP + (Decimal)TotalDeduccionesInstitucionesFinancieras;

                //Renta Neta Gravable
                RentaNetaGravable = TotalIngresosGravables - TotalDeduccionesGravables;


                #region Tabla Progresiva ISR
                //Tabla Progresiva ISR

                //Variable para validar que entre primero en la primera parte de la fórmula del ISR y luego en la segunda parte
                string VI = "FirstRange";

                //Variable de tipo Lista para obtener los registros de la tabla progresiva de mayor a menor
                List<tbISR> objDeduccionISR = db.tbISR.Where(x => x.isr_Activo == true)
                                                      .OrderByDescending(x => x.isr_RangoInicial)
                                                      .ToList();

                foreach (var oISR in objDeduccionISR)
                {
                    if (objDeduccionISR.Count() > 0)
                    {
                        if (RentaNetaGravable >= oISR.isr_RangoInicial)
                        {
                            if (VI == "FirstRange")
                            {
                                totalISR = totalISR + (RentaNetaGravable - oISR.isr_RangoInicial) * (((oISR.isr_Porcentaje) ) / 100);
                                VI = "No";
                            }
                            else if (VI == "No")
                            {
                                VI = "SecondRange";
                            }
                            if (VI == "SecondRange")
                            {
                                totalISR = totalISR + ((oISR.isr_RangoFinal) - oISR.isr_RangoInicial) * ((oISR.isr_Porcentaje) / 100);
                                VI = "No";
                            }
                        }
                        else
                        {
                            totalISR = 0;
                        }
                    }
                }
                if (totalISR > 0)
                {
                    totalISR = totalISR / 12;
                    totalISR = totalISR / 30;
                    totalISR = totalISR * dias;
                }
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            #endregion
            return totalISR;
        }

        #region SalarioPromedioAnualISR
        private static decimal SalarioPromedioAnualISR(decimal? sueldoBruto, List<decimal> mesesPago, bool esMensual, ref decimal SalarioPromedioAnualPagadoAlAnio, ref decimal salarioPromedioAnualPagadoAlMes)
        {
            decimal sueldoProyeccion = 0;
            if (esMensual)
            {
                //Si es el primer mes a cobrar
                if (mesesPago.Count == 0)
                {
                    salarioPromedioAnualPagadoAlMes = ((sueldoBruto * 12)) ?? 0;
                }
                else
                {
                    int cantidadMesesPagados = mesesPago.Count;
                    //if (netoAPagarColaborador == 0)
                    //    netoAPagarColaborador = 37350.66M;
                    mesesPago.Add((Decimal)sueldoBruto);

                    decimal promedioMesesPago = mesesPago.Average();

                    for (int i = cantidadMesesPagados; i <= 12; i++)
                    {
                        sueldoProyeccion += promedioMesesPago;
                    }

                    salarioPromedioAnualPagadoAlMes = sueldoProyeccion;
                }
            }
            else
            {
                if (DateTime.Now.Month == 12)
                    //Calcular todas las fechas de este año, aunque haya entrado
                    SalarioPromedioAnualPagadoAlAnio = mesesPago.Sum();
            }


            return (salarioPromedioAnualPagadoAlMes > 0) ? salarioPromedioAnualPagadoAlMes : SalarioPromedioAnualPagadoAlAnio;
        }
        #endregion
    }
}