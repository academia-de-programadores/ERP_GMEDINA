
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbNotaCredito_Select_Result
    {
        public int ID { get; set; }
        public Nullable<decimal> MONTO { get; set; }
        public System.DateTime dev_Fecha { get; set; }
        public int IDCLIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public string NOMBRES { get; set; }
    }
}
