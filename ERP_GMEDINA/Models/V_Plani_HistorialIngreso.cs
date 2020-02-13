
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Plani_HistorialIngreso
    {
        public int emp_Id { get; set; }
        public int hipa_IdHistorialDePago { get; set; }
        public string cin_DescripcionIngreso { get; set; }
        public Nullable<decimal> hip_TotalPagar { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
        public string nombreEmpleado { get; set; }
        public string identidadEmpleado { get; set; }
        public int cpla_IdPlanilla { get; set; }
    }
}
