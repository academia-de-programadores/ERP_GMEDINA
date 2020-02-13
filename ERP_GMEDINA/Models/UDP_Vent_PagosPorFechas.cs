
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDP_Vent_PagosPorFechas
    {
        public int Num__Linea { get; set; }
        public string RTN_Cliente { get; set; }
        public string Nombre_Completo { get; set; }
        public string Código_Factura { get; set; }
        public string Tipo_Pago { get; set; }
        public decimal Saldo_Anterior { get; set; }
        public decimal Total_Pago { get; set; }
        public Nullable<decimal> Saldo_Actual { get; set; }
    }
}
