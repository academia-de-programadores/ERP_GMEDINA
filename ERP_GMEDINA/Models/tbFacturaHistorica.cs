
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbFacturaHistorica
    {
        public byte facth_Id { get; set; }
        public long fact_Id { get; set; }
        public byte esfac_Id { get; set; }
        public Nullable<System.DateTime> facth_Fecha { get; set; }
    
        public virtual tbEstadoFactura tbEstadoFactura { get; set; }
        public virtual tbFactura tbFactura { get; set; }
    }
}
