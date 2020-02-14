using ERP_GMEDINA.Models;
using System;


namespace ERP_GMEDINA.Helpers
{
    public static class ReportePlanilla
    {
        public static void ReporteColaboradorPlanilla(string moneda, ref ReportePlanillaViewModel oPlanillaEmpleado, tbEmpleados empleadoActual, decimal SalarioBase, int horasTrabajadas, decimal salarioHora, decimal totalSalario, decimal? totalComisiones, int horasExtrasTrabajadas, decimal? totalHorasExtras, decimal? totalHorasPermiso, decimal? totalBonificaciones, decimal? totalIngresosIndivuales, decimal? totalVacaciones, decimal? totalIngresosEmpleado, decimal totalISR, decimal? colaboradorDeducciones, decimal totalAFP, decimal? totalInstitucionesFinancieras, decimal? totalOtrasDeducciones, decimal? adelantosSueldo, decimal? totalDeduccionesEmpleado, decimal? totalDeduccionesIndividuales, decimal? netoAPagarColaborador, V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            oPlanillaEmpleado.CodColaborador = InformacionDelEmpleadoActual.emp_Id.ToString();
            oPlanillaEmpleado.NombresColaborador = $"{empleadoActual.tbPersonas.per_Nombres} {empleadoActual.tbPersonas.per_Apellidos}";
            oPlanillaEmpleado.Moneda = moneda;
            oPlanillaEmpleado.SalarioBase = SalarioBase;
            oPlanillaEmpleado.horasTrabajadas = horasTrabajadas;
            oPlanillaEmpleado.SalarioHora = salarioHora;
            oPlanillaEmpleado.totalSalario = totalSalario;
            oPlanillaEmpleado.tipoPlanilla = empleadoActual.tbCatalogoDePlanillas.cpla_DescripcionPlanilla;
            oPlanillaEmpleado.totalComisiones = totalComisiones;
            oPlanillaEmpleado.horasExtras = horasExtrasTrabajadas;
            oPlanillaEmpleado.totalHorasPermiso = totalHorasPermiso;
            oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
            oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
            oPlanillaEmpleado.totalIngresosIndivuales = totalIngresosIndivuales;
            oPlanillaEmpleado.totalVacaciones = totalVacaciones;
            oPlanillaEmpleado.totalIngresos = Math.Round((decimal)totalIngresosEmpleado, 2);
            oPlanillaEmpleado.totalISR = totalISR;
            oPlanillaEmpleado.totalDeduccionesColaborador = colaboradorDeducciones;
            oPlanillaEmpleado.totalAFP = totalAFP;
            oPlanillaEmpleado.totalInstitucionesFinancieras = totalInstitucionesFinancieras;
            oPlanillaEmpleado.otrasDeducciones = Math.Round((decimal)totalOtrasDeducciones, 2);
            oPlanillaEmpleado.adelantosSueldo = Math.Round((decimal)adelantosSueldo, 2);
            oPlanillaEmpleado.totalDeduccionesIndividuales = totalDeduccionesIndividuales;
            oPlanillaEmpleado.totalDeducciones = Math.Round((decimal)totalDeduccionesEmpleado, 2);
            oPlanillaEmpleado.totalAPagar = Math.Round((decimal)netoAPagarColaborador, 2);
        }

        public static void ReportePlanillaPrevisualizacion(string moneda, ref ReportePlanillaViewModel oPlanillaEmpleado, tbEmpleados empleadoActual, decimal SalarioBase, int horasTrabajadas, decimal salarioHora, decimal totalSalario, decimal? totalComisiones, int horasExtrasTrabajadas, decimal? totalHorasExtras, decimal? totalHorasPermiso, decimal? totalBonificaciones, decimal? totalIngresosIndivuales, decimal? totalVacaciones, decimal? totalIngresosEmpleado, decimal totalISR, decimal? colaboradorDeducciones, decimal totalAFP, decimal? totalInstitucionesFinancieras, decimal? totalOtrasDeducciones, decimal? adelantosSueldo, decimal? totalDeduccionesEmpleado, decimal? totalDeduccionesIndividuales, decimal? netoAPagarColaborador, V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            oPlanillaEmpleado.CodColaborador = InformacionDelEmpleadoActual.emp_Id.ToString();
            oPlanillaEmpleado.NombresColaborador = $"{empleadoActual.tbPersonas.per_Nombres} {empleadoActual.tbPersonas.per_Apellidos}";
            oPlanillaEmpleado.Moneda = moneda;
            oPlanillaEmpleado.SalarioBase = SalarioBase;
            oPlanillaEmpleado.horasTrabajadas = horasTrabajadas;
            oPlanillaEmpleado.SalarioHora = salarioHora;
            oPlanillaEmpleado.totalSalario = totalSalario;
            oPlanillaEmpleado.tipoPlanilla = empleadoActual.tbCatalogoDePlanillas.cpla_DescripcionPlanilla;
            oPlanillaEmpleado.totalComisiones = totalComisiones;
            oPlanillaEmpleado.horasExtras = horasExtrasTrabajadas;
            oPlanillaEmpleado.totalHorasPermiso = totalHorasPermiso;
            oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
            oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
            oPlanillaEmpleado.totalIngresosIndivuales = totalIngresosIndivuales;
            oPlanillaEmpleado.totalVacaciones = totalVacaciones;
            oPlanillaEmpleado.totalIngresos = totalIngresosEmpleado;
            oPlanillaEmpleado.totalISR = totalISR;
            oPlanillaEmpleado.totalDeduccionesColaborador = colaboradorDeducciones;
            oPlanillaEmpleado.totalAFP = totalAFP;
            oPlanillaEmpleado.totalInstitucionesFinancieras = totalInstitucionesFinancieras;
            oPlanillaEmpleado.otrasDeducciones = totalOtrasDeducciones;
            oPlanillaEmpleado.adelantosSueldo = adelantosSueldo;
            oPlanillaEmpleado.totalDeduccionesIndividuales = totalDeduccionesIndividuales;
            oPlanillaEmpleado.totalDeducciones = totalDeduccionesEmpleado;
            oPlanillaEmpleado.totalAPagar = netoAPagarColaborador;
        }
    }
}