
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbSolicitudEfectivoDetalle_Select_Result1
    {
        public int IdSolicitud { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public string Sucursal { get; set; }
        public string Caja { get; set; }
        public string Moneda { get; set; }
        public Nullable<decimal> MontoSolicitado { get; set; }
        public Nullable<decimal> MontoEntregado_ { get; set; }
    }
}
