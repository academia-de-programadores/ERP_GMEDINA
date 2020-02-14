
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_ColaboradoresPorPlanilla
    {
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        public Nullable<int> CantidadColaboradores { get; set; }
    }
}
