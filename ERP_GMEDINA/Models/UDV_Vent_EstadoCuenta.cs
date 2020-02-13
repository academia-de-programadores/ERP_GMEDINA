
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_EstadoCuenta
    {
        public string RTN { get; set; }
        public string Nombres { get; set; }
        public string Teléfono { get; set; }
        public string Correo_Electrónico { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Número__de_Factura { get; set; }
        public Nullable<System.DateTime> Fecha_Vencimiento { get; set; }
        public decimal Saldo_Anterior { get; set; }
        public decimal Monto_Cargo { get; set; }
        public decimal Monto_Crédito { get; set; }
        public Nullable<decimal> Saldo_Actual { get; set; }
    }
}
