
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_FacturasPagoSelect_Result
    {
        public long fact_Id { get; set; }
        public string fact_Codigo { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
        public Nullable<decimal> TotalPago { get; set; }
        public Nullable<decimal> SaldoFactura { get; set; }
    }
}
