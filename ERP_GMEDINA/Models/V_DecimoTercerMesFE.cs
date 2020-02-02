
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DecimoTercerMesFE
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string car_Descripcion { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public Nullable<decimal> dtm_Monto { get; set; }
        public int hipa_Anio { get; set; }
    }
}
