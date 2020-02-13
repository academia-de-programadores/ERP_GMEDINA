
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_ISR_RPT
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        public Nullable<decimal> hipa_TotalISR { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
    }
}
