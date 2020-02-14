
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_FacturasPendientesDePago_Result
    {
        public string clte_Identificacion { get; set; }
        public string clte_Nombres { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public string suc_Descripcion { get; set; }
        public string fact_Codigo { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<decimal> MontoFacturado { get; set; }
        public int TotalCargos { get; set; }
        public Nullable<decimal> TotalCreditos { get; set; }
        public Nullable<decimal> Saldoactual { get; set; }
    }
}
