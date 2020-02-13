
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbNotaCreditoSelect_Result
    {
        public int clte_Id { get; set; }
        public short nocre_Id { get; set; }
        public Nullable<bool> nocre_Anulado { get; set; }
        public string nocre_Codigo { get; set; }
        public Nullable<decimal> nocre_Monto { get; set; }
        public bool nocre_Redimido { get; set; }
        public bool nocre_EsImpreso { get; set; }
    }
}
