
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbFaseSeleccion
    {
        public int fsel_Id { get; set; }
        public int fare_Id { get; set; }
        public int scan_Id { get; set; }
        public bool fsel_Estado { get; set; }
        public string fsel_RazonInactivo { get; set; }
        public int fsel_UsuarioCrea { get; set; }
        public System.DateTime fsel_FechaCrea { get; set; }
        public Nullable<int> fsel_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> fsel_FechaModifica { get; set; }
    
        public virtual tbFasesReclutamiento tbFasesReclutamiento { get; set; }
        public virtual tbSeleccionCandidatos tbSeleccionCandidatos { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
