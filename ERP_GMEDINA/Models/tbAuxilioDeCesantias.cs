
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAuxilioDeCesantias
    {
        public int aces_IdAuxilioCesantia { get; set; }
        public int aces_RangoInicioMeses { get; set; }
        public int aces_RangoFinMeses { get; set; }
        public int aces_DiasAuxilioCesantia { get; set; }
        public int aces_UsuarioCrea { get; set; }
        public System.DateTime aces_FechaCrea { get; set; }
        public Nullable<int> aces_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> aces_FechaModifica { get; set; }
        public bool aces_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
