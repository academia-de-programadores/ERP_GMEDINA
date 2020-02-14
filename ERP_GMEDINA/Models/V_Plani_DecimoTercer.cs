
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Plani_DecimoTercer
    {
        public int dtm_IdDecimoTercerMes { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public int emp_Id { get; set; }
        public string NombreCompleto { get; set; }
        public string area_Descripcion { get; set; }
        public System.DateTime dtm_FechaPago { get; set; }
        public Nullable<decimal> dtm_Monto { get; set; }
    }
}
