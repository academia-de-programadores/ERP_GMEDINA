
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_InstitucionesFinancieras_RPT
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public int cde_IdDeducciones { get; set; }
        public string cde_DescripcionDeduccion { get; set; }
        public Nullable<decimal> hidp_Total { get; set; }
        public Nullable<System.DateTime> hipa_FechaInicio { get; set; }
        public Nullable<System.DateTime> hipa_FechaFin { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
    }
}
