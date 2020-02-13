
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbSolicitudEfectivo_Details_Result
    {
        public int soled_Id { get; set; }
        public string deno_Descripcion { get; set; }
        public short soled_CantidadSolicitada { get; set; }
        public Nullable<decimal> MontoSolicitado { get; set; }
        public short soled_CantidadEntregada { get; set; }
        public Nullable<decimal> MontoEntregado { get; set; }
        public short deno_Id { get; set; }
        public decimal deno_valor { get; set; }
    }
}
