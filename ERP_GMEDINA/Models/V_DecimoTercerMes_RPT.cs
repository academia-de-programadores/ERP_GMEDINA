namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DecimoTercerMes_RPT
    {
        public int dtm_IdDecimoTercerMes { get; set; }
        public Nullable<int> emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public System.DateTime dtm_FechaPago { get; set; }
        public Nullable<decimal> dtm_Monto { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public string dtm_CodigoPago { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
    }
}
