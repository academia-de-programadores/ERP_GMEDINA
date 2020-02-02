
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_CatalogoDePlanillasConIngresosYDeducciones
    {
        public int idPlanilla { get; set; }
        public string descripcionPlanilla { get; set; }
        public int frecuenciaEnDias { get; set; }
        public Nullable<int> idIngreso { get; set; }
        public string descripcionIngreso { get; set; }
        public Nullable<int> idDeducciones { get; set; }
        public string descripcionDeduccion { get; set; }
    }
}
