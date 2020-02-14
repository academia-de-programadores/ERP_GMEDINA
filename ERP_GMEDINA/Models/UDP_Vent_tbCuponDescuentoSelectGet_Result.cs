
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbCuponDescuentoSelectGet_Result
    {
        public int cdto_ID { get; set; }
        public System.DateTime cdto_FechaVencimiento { get; set; }
        public Nullable<decimal> cdto_PorcentajeDescuento { get; set; }
        public Nullable<decimal> cdto_MontoDescuento { get; set; }
        public decimal cdto_MaximoMontoDescuento { get; set; }
        public decimal cdto_CantidadCompraMinima { get; set; }
        public bool cdto_Redimido { get; set; }
    }
}
