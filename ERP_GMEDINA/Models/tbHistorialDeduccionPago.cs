namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialDeduccionPago
    {
        public int hidp_IdHistorialdeDeduPago { get; set; }
        public int cde_IdDeducciones { get; set; }
        public int hipa_IdHistorialDePago { get; set; }
        public Nullable<decimal> hidp_Total { get; set; }
        public int hidp_UsuarioCrea { get; set; }
        public System.DateTime hidp_FechaCrea { get; set; }
        public Nullable<int> hidp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hidp_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeDeducciones tbCatalogoDeDeducciones { get; set; }
        public virtual tbHistorialDePago tbHistorialDePago { get; set; }
    }
}
