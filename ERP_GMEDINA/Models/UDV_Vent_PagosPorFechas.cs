
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_PagosPorFechas
    {
        public int Num { get; set; }
        public string RTN_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Código_Factura { get; set; }
        public short tpa_Id { get; set; }
        public string tpa_Descripcion { get; set; }
        public int Cajero { get; set; }
        public decimal Saldo_Anterior { get; set; }
        public decimal Total_Pago { get; set; }
        public Nullable<System.DateTime> pago_FechaElaboracion { get; set; }
        public Nullable<decimal> Saldo_Actual { get; set; }
    }
}
