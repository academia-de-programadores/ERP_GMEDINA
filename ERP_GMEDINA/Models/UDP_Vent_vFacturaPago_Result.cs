
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_vFacturaPago_Result
    {
        public long fact_Id { get; set; }
        public int clte_Id { get; set; }
        public bool fact_AlCredito { get; set; }
        public string fact_Codigo { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
        public Nullable<decimal> TotalPagado { get; set; }
        public Nullable<decimal> SaldoFactura { get; set; }
    }
}
