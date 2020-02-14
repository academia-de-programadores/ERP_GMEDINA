
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Ingresos_RPT
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public int cin_IdIngreso { get; set; }
        public string cin_DescripcionIngreso { get; set; }
        public Nullable<decimal> hip_TotalPagar { get; set; }
        public Nullable<System.DateTime> hipa_FechaInicio { get; set; }
        public Nullable<System.DateTime> hipa_FechaFin { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
    }
}
