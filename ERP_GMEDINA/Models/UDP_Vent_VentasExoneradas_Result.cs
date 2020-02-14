
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_VentasExoneradas_Result
    {
        public Nullable<System.DateTime> Fecha_factura { get; set; }
        public string suc_Descripcion { get; set; }
        public string prod_Codigo { get; set; }
        public string clte_Identificacion { get; set; }
        public string clte_NombreComercial { get; set; }
        public string fact_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public Nullable<decimal> Monto_Facturado { get; set; }
        public Nullable<decimal> Impuesto_Exonerado { get; set; }
    }
}
