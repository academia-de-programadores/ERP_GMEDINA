﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Helpers
{
    public class Liquidacion
    {

        #region OBTENER DÍAS TRABAJADOS
        public static double Dias360Mes(DateTime fechaFin, int idEmpleado)
        {
            DateTime fechaInicio = DateTime.MinValue;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    fechaInicio = db.tbEmpleados.Where(x => x.emp_Id == idEmpleado).Select(x => x.emp_Fechaingreso).FirstOrDefault();
                }
                catch (Exception ex)
                {

                }
            }

            if (fechaInicio > fechaFin)
                return 0;

            int diaInicio = fechaInicio.Day;
            int mesInicio = fechaInicio.Month;
            int anioInicio = fechaInicio.Year;
            int diaFin = fechaFin.Day;
            int mesFin = fechaFin.Month;
            int anioFin = fechaFin.Year;

            if (diaInicio == 31 || EsElUltimoDiaDeFebrero(fechaInicio))
                diaInicio = 30;

            if (diaInicio == 30 && diaFin == 31)
                diaFin = 30;

            return ((anioFin - anioInicio) * 360) + ((mesFin - mesInicio) * 30) + (diaFin - diaInicio);
        }

        private static bool EsElUltimoDiaDeFebrero(DateTime date)
        {
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }
        #endregion

        #region CÁLCULO DE ANTIGUEDAD EN DÍAS
        //CALCULO DE ANTIGUEDAD DE EMPLEADO
        public static int Calculo_AntiguedadEnDias(int Emp_Id, DateTime FechaLiquidacion)
        {
            //Captura de años de antiguedad
            int DiasTrabajados = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                var FechaIngreso = db.tbEmpleados.Where(p => p.emp_Id == Emp_Id).Select(x => x.emp_Fechaingreso).FirstOrDefault();
                //Cantidad de Años convertidos a string para ser admitidos como valores de retorno
                DiasTrabajados = (((FechaLiquidacion - FechaIngreso)).Days);
                //Calcular la cantidad de años para restar los dias no habiles 
                int Anios = DiasTrabajados / 365;
                int DiasNoHabiles = Anios * 5;
                //Resta de los dias no habiles a los dias trabajados durante el año 
                DiasTrabajados = DiasTrabajados - DiasNoHabiles;
            }
            return DiasTrabajados;
        }
        #endregion

        #region CÁLCULO DE SALARIO PROMEDIO MENSUAL
        //CALCULO DE SALARIO ORDINARIO PROMEDIO MENSUAL
        public static decimal Calculo_SalarioOrdinarioMensual(int Emp_Id)
        {
            //Captura de SalarioPromedio
            decimal SalarioPromedio = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //METODO MEDIANTE HISTORIAL DE PAGO
                    IQueryable<decimal> SalariosUlt6Meses = db.tbSueldos.OrderByDescending(x => x.sue_FechaCrea).ThenByDescending(x => x.sue_Cantidad)
                                                                               .Where(p => p.emp_Id == Emp_Id)
                                                                               .Select(x => (decimal)x.sue_Cantidad).Take(6);
                    //CAPTURA DEL SALARIO PROMEDIO Y CONVERSIÓN A STRING PARA EL RETORNO 
                    SalarioPromedio = (SalariosUlt6Meses.Count() > 0) ? SalariosUlt6Meses.Average() : 0;
                    if (SalarioPromedio == 0)
                    {
                        SalariosUlt6Meses = db.tbSueldos.Where(p => p.emp_Id == Emp_Id).Select(x => (decimal)x.sue_Cantidad).Take(1);
                        SalarioPromedio = SalariosUlt6Meses.Average();
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Math.Round(SalarioPromedio, 2);
        }
        #endregion

        #region CÁLCULO DE SALARIO BRUTO MAS ALTO
        //CALCULO DE SALARIO ORDINARIO PROMEDIO MENSUAL
        public static decimal Calculo_SalarioBrutoMasAlto(int Emp_Id)
        {
            //Captura de SalarioPromedio
            decimal SalarioBrutoMasAlto = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //METODO MEDIANTE HISTORIAL DE PAGO
                    IQueryable<decimal> SalariosUlt6Meses = db.tbHistorialDePago.OrderByDescending(x => x.hipa_FechaPago).Where(p => p.emp_Id == Emp_Id)
                                                                               .Select(x => (decimal)x.hipa_TotalSueldoBruto).Take(6);
                    //CAPTURA DEL SALARIO PROMEDIO Y CONVERSIÓN A STRING PARA EL RETORNO 
                    SalarioBrutoMasAlto = (SalariosUlt6Meses.Count() > 0) ? SalariosUlt6Meses.Max() : 0;
                    //VALIDAR EN CASO QUE EL EMPLEADO NO ESTE EN EL HISTORIAL DE PAGO
                    if (SalarioBrutoMasAlto == 0)
                    {
                        //CAPTURAR EL SALARIO DEL REINGRESO EN CASO QUE SEA DE REINGRESO EN LA EMPRESA
                        SalariosUlt6Meses = db.tbSueldos.OrderByDescending(z => z.sue_FechaCrea).Where(p => p.emp_Id == Emp_Id).Select(x => (decimal)x.sue_Cantidad).Take(1);
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

        #region OBTENETER SALARIOS
        //OBTENER SALARIOS
        public static object EjecutarCalculosSalarios(int IdEmpleado)
        {
            decimal SalarioOrdinarioMensual = Calculo_SalarioOrdinarioMensual(IdEmpleado);
            decimal SalarioPromedioMensual = (SalarioOrdinarioMensual * 14) / 12;
            decimal SalarioOrdinarioDiario = SalarioOrdinarioMensual / 30;
            decimal SalarioPromedioDiario = SalarioPromedioMensual / 30;

            SalarioOrdinarioMensual = Math.Round((Decimal)SalarioOrdinarioMensual, 2);
            SalarioPromedioMensual = Math.Round((Decimal)SalarioPromedioMensual, 2);
            SalarioOrdinarioDiario = Math.Round((Decimal)SalarioOrdinarioDiario, 2);
            SalarioPromedioDiario = Math.Round((Decimal)SalarioPromedioDiario, 2);

            return new { SalarioOrdinarioMensual, SalarioPromedioMensual, SalarioOrdinarioDiario, SalarioPromedioDiario };
        }
        #endregion

        //
        //CALCULO DE CONCEPTOS
        //

        #region CÁLCULO DE PREAVISO
        //CALCULO DE PAGO POR CONCEPTO DE PREAVISO
        public static decimal Calculo_PagoDePreaviso(int Emp_Id, decimal SalarioPromedioDiario, int Antiguedad)
        {
            //ALMACENA MONTO DEL PAGO DE PREAVISO
            decimal PagoDePreaviso = 0;

            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //ALMACENA EL RANGO DE INICIO DE LA VALIDACIÓN POR AÑOS
                    int RangoInicial = 0;
                    //ALMACENA LA CANTIDAD DE DIAS CORRESPONDIENTES
                    int DiasCorrespondientes = 0;
                    ////ALMACENA LA CANTIDAD DE AÑOS LABORADOS
                    //int MesesLaborados = Antiguedad / 30;
                    //ALMACENA EL SALARIO PROMEDIO DIARIO
                    SalarioPromedioDiario = (Calculo_SalarioOrdinarioMensual(Emp_Id) * 14) / 360;
                    //INICIALIZACION DE LISTA DE LIQUIDACION_PREAVISO
                    List<tbPreaviso> TbLiquidacionPreaviso = db.tbPreaviso.ToList();
                    //ITERACION DE LA LISTA LIQUIDACION_PREAVISO
                    foreach (tbPreaviso iter in TbLiquidacionPreaviso)
                    {
                        //VALIDAR LA CANTIDAD DE DIAS CORRESPONDIENTES
                        if (Antiguedad > (RangoInicial * 30) && Antiguedad <= (iter.prea_RangoFinMeses * 30))
                            DiasCorrespondientes = iter.prea_DiasPreaviso;//ASIGACION EN CASO DE SALIDA LOGICA VERDADERA
                        else if (Antiguedad > (iter.prea_RangoFinMeses * 30))
                            DiasCorrespondientes = iter.prea_DiasPreaviso;
                        //SETEAR LA VARIABLE RangoInicial CON EL VALOR DEL RANGO FINAL DEL ITERADOR
                        RangoInicial = iter.prea_RangoFinMeses;
                    }
                    PagoDePreaviso = (SalarioPromedioDiario * DiasCorrespondientes);
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Math.Round(PagoDePreaviso, 2);
        }
        #endregion

        #region CÁLCULO DE CESANTÍA
        //CALCULO DE PAGO POR CONCEPTO DE CESANTIA
        public static decimal Calculo_PagoDeCesantia(int Emp_Id, decimal SalarioPromedioDiario, int Antiguedad)
        {
            //ALMACENA MONTO DEL PAGO DE CESANTIA
            decimal PagoDeCesantia = 0;

            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //ALMACENA EL RANGO DE INICIO DE LA VALIDACIÓN POR AÑOS
                    int RangoInicial = 0;
                    //ALMACENA LA CANTIDAD DE DIAS CORRESPONDIENTES
                    int DiasCorrespondientes = 0;
                    //ALMACENA LA CANTIDAD DE DIAS LABORADOS
                    int DiasLaborados = Antiguedad;
                    //ALMACENA LA CANTIDAD DE AÑOS LABORADOS
                    int MesesLaborados = DiasLaborados / 30;
                    //ALMACENA EL SALARIO PROMEDIO DIARIO
                    SalarioPromedioDiario = (SalarioPromedioDiario * 14) / 360;
                    //INICIALIZACION DE LISTA DE LIQUIDACION_CESANTIA
                    List<tbAuxilioDeCesantias> TbLiquidacionAuxilioCesantia = db.tbAuxilioDeCesantias.ToList();
                    int Contador = 0;
                    //ITERACION DE LA TABLA LIQUIDACION_CESANTIA
                    foreach (tbAuxilioDeCesantias iter in TbLiquidacionAuxilioCesantia)
                    {
                        if (Contador == 0)
                        {
                            //VALIDAR LA CANTIDAD DE DIAS CORRESPONDIENTES
                            if (Antiguedad >= (iter.aces_RangoInicioMeses * 30) && Antiguedad <= (iter.aces_RangoFinMeses * 30))
                                DiasCorrespondientes = iter.aces_DiasAuxilioCesantia;//ASIGACION EN CASO DE SALIDA LOGICA VERDADERA
                            else if (Antiguedad > (iter.aces_RangoFinMeses * 30))
                                DiasCorrespondientes = MesesLaborados * 30;
                        }
                        else
                        {
                            //VALIDAR LA CANTIDAD DE DIAS CORRESPONDIENTES
                            if (MesesLaborados > RangoInicial && MesesLaborados <= iter.aces_RangoFinMeses)
                                DiasCorrespondientes = iter.aces_DiasAuxilioCesantia;//ASIGACION EN CASO DE SALIDA LOGICA VERDADERA
                            else if (MesesLaborados > 12)
                                DiasCorrespondientes = MesesLaborados * 30;
                        }
                        //SETEAR LA VARIABLE RangoInicial CON EL VALOR DEL RANGO FINAL DEL ITERADOR
                        RangoInicial = iter.aces_RangoFinMeses;
                        //SETEAR LA VARIABLE CONTADOR
                        Contador++;
                    }
                    //SETEO DE LA VARIABLE CONTENEDORA DEL MONTO DE AUXILIODECESANTIA
                    PagoDeCesantia = (MesesLaborados > RangoInicial) ? ((SalarioPromedioDiario * DiasCorrespondientes) + ((DiasLaborados % 360) * ((SalarioPromedioDiario * 30) / 360))) :
                                                                       (SalarioPromedioDiario * DiasCorrespondientes);
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return Math.Round(PagoDeCesantia, 2);
        }
        #endregion

        #region CÁLCULO DE DECIMOCUARTO MES PROPORCIONAL
        //CALCULO DE DECIMO TERCER MES PENDIENTE / ADEUDADO
        public static decimal Calculo_DecimoTercerMesProporcional(int Emp_Id, DateTime Fecha, decimal SalarioPromedioDiario)
        {
            //VARIABLE DE RETORNO CON EL MONTO DTM
            decimal MontoDecimoTercerMes = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //ALMACENA EL VALOR DEL SALARIO PROMEDIO DIARIO
                    SalarioPromedioDiario = ((SalarioPromedioDiario * 14) / 12) / 360;
                    //ALMACENAR LOS DÍAS PENDIENTES DE PAGO DE DECIMO TERCER MES
                    int DiasPendientesDTM = 0;
                    //CAPTURAR LA ULTIMA FECHA DE PAGO DE DECIMO TERCER MES
                    var ListaPagos = db.tbDecimoTercerMes.OrderByDescending(x => x.dtm_FechaPago).Where(p => p.emp_Id == Emp_Id).Select(c => c.dtm_FechaPago).ToList();
                    if (ListaPagos.Count != 0)
                    {
                        DateTime UltimoPagoDTM = ListaPagos.FirstOrDefault();
                        DiasPendientesDTM = (Fecha - UltimoPagoDTM).Days;
                    }
                    else
                    {
                        DiasPendientesDTM = 0;
                    }
                    //SETEAR LA VARIBLE MontoDecimoTercerMes
                    MontoDecimoTercerMes = (DiasPendientesDTM > 0) ? DiasPendientesDTM * SalarioPromedioDiario : 0;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return (MontoDecimoTercerMes < 0) ? 0 : Math.Round(MontoDecimoTercerMes, 2);
        }
        #endregion

        #region CÁLCULO DE DECIMOCUARTO MES PROPORCIONAL
        //CALCULO DE DECIMO CUARTO MES PENDIENTE / ADEUDADO
        public static decimal Calculo_DecimoCuartoMesProporcional(int Emp_Id, DateTime Fecha, decimal SalarioPromedioDiario)
        {
            //VARIABLE DE RETORNO CON EL MONTO DCM
            decimal MontoDecimoCuartoMes = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //ALMACENA EL VALOR DEL SALARIO PROMEDIO DIARIO
                    SalarioPromedioDiario = ((SalarioPromedioDiario * 14) / 12) / 360;
                    //ALMACENAR LOS DÍAS PENDIENTES DE PAGO DE DECIMO TERCER MES
                    int DiasPendientesDCM = 0;
                    //CAPTURAR LA ULTIMA FECHA DE PAGO DE DECIMO CUARTO MES
                    var ListaPagos = db.tbDecimoCuartoMes.OrderByDescending(x => x.dcm_FechaPago).Where(p => p.emp_Id == Emp_Id).Select(c => c.dcm_FechaPago).ToList();
                    if (ListaPagos.Count != 0)
                    {
                        DateTime UltimoPagoDCM = ListaPagos.FirstOrDefault();
                        DiasPendientesDCM = (Fecha - UltimoPagoDCM).Days;
                    }
                    else
                    {
                        DiasPendientesDCM = 0;
                    }
                    //SETEAR LA VARIBLE MontoDecimoTercerMes
                    MontoDecimoCuartoMes = (DiasPendientesDCM > 0) ? DiasPendientesDCM * SalarioPromedioDiario : 0;
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return (MontoDecimoCuartoMes < 0) ? 0 : Math.Round(MontoDecimoCuartoMes, 2);
        }
        #endregion

        #region CÁLCULO DE VACACIONES PENDIENTES
        //CALCULO DE PAGO DE VACACIONES PENDIENTES
        public static decimal Calculo_VacacionesProporcionales(int Emp_Id, DateTime Fecha, decimal SalarioPromedioDiario, int Antiguedad)
        {
            //ALMACENA EL MONTO DEl MONTO DEL DECIMO TERCER MES DE FORMA PROPORCIONAL
            decimal MontoVacacionesPendientes = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                //SALARIO ORDINADIO
                decimal SalarioOrdinario = SalarioPromedioDiario;
                //ACUMULADOR DE DIAS DE VACACIONES
                int Historico_DiasDeVacacionCorrespondiente = 0;
                //ACUMULADOR DE VACACIONES TOMADAS
                int Historico_DiasVacacionesTomadas = 0;
                try
                {
                    //ALMACENA LA CANTIDAD DE AÑOS LABORADOS
                    int AniosLaborados = Antiguedad / 360;
                    //SETEO DEL SALARIO PROMEDIO DIARIO 
                    SalarioPromedioDiario = (SalarioPromedioDiario * 14) / 360;
                    //ITERADOR DEL CICLO
                    int iter = 0;
                    //ITERAR AÑOS PARA INCREMENTAR LOS DIAS DE VACACIONES ACUMULADAS
                    while (iter <= AniosLaborados)
                    {
                        Historico_DiasDeVacacionCorrespondiente += (iter == 1) ? 10 :
                                                                   (iter == 2) ? 12 :
                                                                   (iter == 3) ? 15 :
                                                                   (iter >= 4) ? 20 : 0;
                        //INCREMENTO DE ITERADOR DEL CICLO
                        iter++;
                    }
                    var ListHistorialDeVacaciones = from HV in db.tbHistorialVacaciones
                                                    where HV.emp_Id == Emp_Id
                                                    select new { HV };
                    if (ListHistorialDeVacaciones.Count() > 0)
                    {
                        var tbHistorialVacaciones = db.tbHistorialVacaciones.Where(p => p.emp_Id == Emp_Id).ToList();
                        foreach (tbHistorialVacaciones it in tbHistorialVacaciones)
                            //CAPTURA EN CONTADOR LA CANTIDAD DE VACIONES TOMADAS 
                            Historico_DiasVacacionesTomadas += it.hvac_CantDias;
                    }

                    //OBTENER LA BASE EN DIAS
                    int BaseEnDias = Historico_DiasDeVacacionCorrespondiente += (iter == 1) ? 10 :
                                                                   (iter == 2) ? 12 :
                                                                   (iter == 3) ? 15 :
                                                                   (iter >= 4) ? 20 : 0;
                    //VALIDAR VACACIONES PROPORCIONALES
                    decimal PagoProporcionalDeVacaciones = (
                                                                 (Antiguedad > 360) ?
                                                                    ((SalarioOrdinario / 30) * (Antiguedad % (AniosLaborados * 360))) / BaseEnDias :
                                                                    ((SalarioOrdinario / 30) * Antiguedad) / BaseEnDias
                                                            );



                    //VALIDAR VACACIONES TOMADAS
                    int DiasVacacionesValidos = (Historico_DiasDeVacacionCorrespondiente >= Historico_DiasVacacionesTomadas) ? (Historico_DiasDeVacacionCorrespondiente - Historico_DiasVacacionesTomadas) : 0;
                    //int Historico_DiasVacacionestomadas = db.tbHistorialVacaciones.Where(p => p.emp_Id == Emp_Id).Select(c => c.hvac_DiasTomados).Sum();
                    MontoVacacionesPendientes = ((Antiguedad / 360) > 0) ? DiasVacacionesValidos * SalarioPromedioDiario : 0;
                    //SUMATORIA DE DÍAS DE VACACIONES PROPORCIONALES
                    MontoVacacionesPendientes += PagoProporcionalDeVacaciones;

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            return (MontoVacacionesPendientes < 0) ? 0 : Math.Round(MontoVacacionesPendientes, 2);
        }
        #endregion


        //
        //CALCULO DE CESANTIA ANUAL
        //

        #region CÁLCULO DE ANTIGUEDAD EN DÍAS

        /*CALCULO DE ANTIGUEDAD DE EMPLEADO
        public static int DiasAcumuladosCesantia(int Emp_Id, DateTime FechaActual)
        {
            //CAPTURA DE ANTIGUEDAD EN DIAS
            int DiasTrabajados = 0;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                //SETEAR LA FECHA CON LA DE REINGRESO
                var FechaInicial = (DateTime)db.tbEmpleados.OrderByDescending(c => c.emp_FechaCrea).Where(p => p.emp_Id == Emp_Id && p.emp_Reingreso == true).Select(x => x.emp_Fechaingreso).Take(1).FirstOrDefault();
                if(FechaInicial == null)
                {
                    //SETEAR A FECHA CON EL ULTIMO PAGO DE CESANTIA
                    FechaInicial = (DateTime)db.tbPagoDeCesantiaDetalle.OrderByDescending(c => c.pdcd_FechaCrea).Where(p => p.emp_Id == Emp_Id).Select(x => x.pdcd_FechaCrea).Take(1).FirstOrDefault();
                }
              
                if(FechaInicial == null)
                {
                    //SETEAR A FECHA CON LA FECHA DE INGRESO
                    FechaInicial = (DateTime)db.tbEmpleados.Where(p => p.emp_Id == Emp_Id).Select(x => x.emp_Fechaingreso).FirstOrDefault();
                }
                DiasTrabajados = (((FechaActual - FechaInicial)).Days);
                //Calcular la cantidad de años para restar los dias no habiles 
                int Anios = DiasTrabajados / 365;
                int DiasNoHabiles = Anios * 5;
                //Resta de los dias no habiles a los dias trabajados durante el año 
                DiasTrabajados = DiasTrabajados - DiasNoHabiles;
            }
            return DiasTrabajados;
        }*/

        //GET DÍAS DE CESANTIA PROPORCIONAL
        public static double Dias360AcumuladosCesantia(int Emp_Id, DateTime FechaActual)
        {
            DateTime FechaInicial = DateTime.MinValue;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //SETEAR LA FECHA CON LA DE REINGRESO
                    FechaInicial = (DateTime)db.tbEmpleados.OrderByDescending(c => c.emp_FechaCrea).Where(p => p.emp_Id == Emp_Id && p.emp_Reingreso == true).Select(x => x.emp_Fechaingreso).Take(1).FirstOrDefault();
                    //VALIDAR EN CASO QUE LA FECHA REINGRESO SEA NULL
                    if (FechaInicial == null)
                    {
                        //SETEAR A FECHA CON EL ULTIMO PAGO DE CESANTIA
                        FechaInicial = (DateTime)db.tbPagoDeCesantiaDetalle.OrderByDescending(c => c.pdcd_FechaCrea).Where(p => p.emp_Id == Emp_Id).Select(x => x.pdcd_FechaCrea).Take(1).FirstOrDefault();
                    }
                    //VALIDAR EN CASO QUE LA FECHA DE HISTORIAL DE PAGO DE CESANTÍA SEA NULL
                    if (FechaInicial == null)
                    {
                        //SETEAR A FECHA CON LA FECHA DE INGRESO
                        FechaInicial = (DateTime)db.tbEmpleados.Where(p => p.emp_Id == Emp_Id).Select(x => x.emp_Fechaingreso).FirstOrDefault();
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            //VALIDAR QUE LA FECHA ACTUAL SEA MENOR QUE LA FECHA INICIAL
            if (FechaInicial > FechaActual)
                return 0;

            //DESCOMPOSICIÓN DE FECHAS PARA FACTORIZAR EL VALOR DE RETORNO
            int diaInicio = FechaInicial.Day;
            int mesInicio = FechaInicial.Month;
            int anioInicio = FechaInicial.Year;
            int diaFin = FechaActual.Day;
            int mesFin = FechaActual.Month;
            int anioFin = FechaActual.Year;
            //VALIDAR FEBRERO EN FORMATO DE DIAS 30
            if (diaInicio == 31 || EsElUltimoDiaDeFebrero(FechaInicial))
                diaInicio = 30;
            //VALIDAR MESES EN FORMATO DE AÑO 360
            if (diaInicio == 30 && diaFin == 31)
                diaFin = 30;
            //VALOR DE RETORNO
            return ((anioFin - anioInicio) * 360) + ((mesFin - mesInicio) * 30) + (diaFin - diaInicio);
        }

        #endregion

    }
}