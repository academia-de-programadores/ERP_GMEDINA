
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbCuponDescuento_Reporte_Result
    {
        public int cdto_ID { get; set; }
        public string clte_Identificacion { get; set; }
        public string clte_Nombres { get; set; }
        public string suc_Descripcion { get; set; }
        public System.DateTime cdto_FechaEmision { get; set; }
        public System.DateTime cdto_FechaVencimiento { get; set; }
        public Nullable<decimal> cdto_PorcentajeDescuento { get; set; }
        public Nullable<decimal> cdto_MontoDescuento { get; set; }
        public decimal cdto_CantidadCompraMinima { get; set; }
        public bool cdto_Redimido { get; set; }
        public Nullable<bool> cdto_Anulado { get; set; }
    }
}
