namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPreaviso
    {
        public int prea_IdPreaviso { get; set; }
        public int prea_RangoInicioMeses { get; set; }
        public int prea_RangoFinMeses { get; set; }
        public int prea_DiasPreaviso { get; set; }
        public int prea_UsuarioCrea { get; set; }
        public System.DateTime prea_FechaCrea { get; set; }
        public Nullable<int> prea_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prea_FechaModifica { get; set; }
        public bool prea_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
