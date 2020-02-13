
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_ReporteConsumidorFinal_Result
    {
        public string fact_Codigo { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public int suc_Id { get; set; }
        public short cja_Id { get; set; }
        public string confi_Nombres { get; set; }
        public string confi_Telefono { get; set; }
        public string confi_Correo { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
    }
}
