
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialEmpleadosLiquidados
    {
        public Nullable<int> hdli_Id { get; set; }
        public Nullable<int> emp_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public Nullable<decimal> hdli_SalarioOrdinarioMensual_Liq { get; set; }
        public Nullable<decimal> hdli_SalarioPromedioMensual_Liql { get; set; }
        public int moli_IdMotivo { get; set; }
        public string moli_Descripcion { get; set; }
        public Nullable<decimal> hdli_MontoTotalLiquidacion { get; set; }
    }
}
