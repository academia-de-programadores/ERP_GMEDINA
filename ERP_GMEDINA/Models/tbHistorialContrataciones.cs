
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialContrataciones
    {
        public int hcon_Id { get; set; }
        public int scan_Id { get; set; }
        public int depto_Id { get; set; }
        public System.DateTime hcon_FechaContratado { get; set; }
        public bool hcon_Estado { get; set; }
        public string hcon_RazonInactivo { get; set; }
        public int hcon_UsuarioCrea { get; set; }
        public System.DateTime hcon_FechaCrea { get; set; }
        public Nullable<int> hcon_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hcon_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbDepartamentos tbDepartamentos { get; set; }
        public virtual tbSeleccionCandidatos tbSeleccionCandidatos { get; set; }
    }
}
