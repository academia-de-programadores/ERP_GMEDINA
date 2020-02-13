
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDeduccionesExtraordinarias
    {
        public int dex_IdDeduccionesExtra { get; set; }
        public int eqem_Id { get; set; }
        public Nullable<decimal> dex_MontoInicial { get; set; }
        public Nullable<decimal> dex_MontoRestante { get; set; }
        public string dex_ObservacionesComentarios { get; set; }
        public int cde_IdDeducciones { get; set; }
        public Nullable<decimal> dex_Cuota { get; set; }
        public int dex_UsuarioCrea { get; set; }
        public System.DateTime dex_FechaCrea { get; set; }
        public Nullable<int> dex_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dex_FechaModifica { get; set; }
        public bool dex_Activo { get; set; }
        public Nullable<bool> dex_DeducirISR { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeDeducciones tbCatalogoDeDeducciones { get; set; }
        public virtual tbEquipoEmpleados tbEquipoEmpleados { get; set; }
    }
}
