
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbNotaCredito_Reporte_Result
    {
        public Nullable<long> N_ { get; set; }
        public string Código_Nota_Crédito { get; set; }
        public System.DateTime Fecha_Emisión { get; set; }
        public string Motivo { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public bool Redimido { get; set; }
        public string Código_Factura { get; set; }
        public Nullable<int> Id { get; set; }
        public Nullable<bool> Tipo_Cliente { get; set; }
        public string Identificación_Cliente { get; set; }
        public string Nombres { get; set; }
        public Nullable<decimal> devd_Monto { get; set; }
        public Nullable<decimal> Monto_Total { get; set; }
    }
}
