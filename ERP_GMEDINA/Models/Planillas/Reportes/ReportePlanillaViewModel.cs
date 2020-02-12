using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ReportePlanillaViewModel
    {
        public string CodColaborador { get; set; }
        public string NombresColaborador { get; set; }
        public decimal SalarioBase { get; set; }
        public int horasTrabajadas { get; set; }
        public decimal SalarioHora { get; set; }
        public decimal totalSalario { get; set; }
        public string tipoPlanilla { get; set; }
        public decimal? totalComisiones { get; set; }
        public int? horasExtras { get; set; }
        public decimal? totalHorasPermiso { get; set; }
        public decimal? TotalIngresosHorasExtras { get; set; }
        public decimal? totalBonificaciones { get; set; }

        public decimal? totalIngresosIndivuales { get; set; }

        public decimal? totalVacaciones { get; set; }
        public decimal? totalIngresos { get; set; }
        public decimal? totalISR { get; set; }
        public decimal? totalDeduccionesColaborador { get; set; }
        public decimal? totalAFP { get; set; }
        public decimal? totalInstitucionesFinancieras { get; set; }
        public decimal? otrasDeducciones { get; set; }
        public decimal? adelantosSueldo { get; set; }
        public decimal? totalDeduccionesIndividuales { get; set; }
        public decimal? totalDeducciones { get; set; }
        public decimal? totalAPagar { get; set; }


        //se va a generalizar en la propiedad horasExtras
        //public decimal horasAl25 { get; set; }
        //public decimal horasAl50 { get; set; }
        //public decimal horasAl75 { get; set; }
        //se va a generalizar en la propiedad TotalIngresosHorasExtras
        //public decimal TotalIngresosAl25 { get; set; }
        //public decimal TotalIngresosAl50 { get; set; }
        //public decimal TotalIngresosAl75 { get; set; }

        //se va a generalizar en la propiedad totalDeduccionesColaborador
        //public decimal totalIHSS { get; set; }
        //public decimal totalRAP { get; set; }
        //public decimal totalINFOP { get; set; }
        //public decimal totalIV { get; set; }
    }
}