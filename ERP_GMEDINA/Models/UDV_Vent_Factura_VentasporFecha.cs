
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_Factura_VentasporFecha
    {
        public string fact_Codigo { get; set; }
        public Nullable<System.DateTime> Fecha_factura { get; set; }
        public string clte_Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Tipo_Factura { get; set; }
        public Nullable<System.DateTime> Fecha_vencimiento { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public Nullable<decimal> Impuesto { get; set; }
        public Nullable<decimal> TotalFactura { get; set; }
    }
}
