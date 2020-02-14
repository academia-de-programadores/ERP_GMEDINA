
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDP_Vent_SolicituEfectivo_Select
    {
        public int IdSolicitud { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public string Sucursal { get; set; }
        public string Caja { get; set; }
        public string Moneda { get; set; }
        public Nullable<decimal> MontoSolicitado { get; set; }
        public Nullable<decimal> MontoEntregado { get; set; }
        public bool Apertura { get; set; }
        public bool Anulada { get; set; }
        public int usuariocrea { get; set; }
    }
}
