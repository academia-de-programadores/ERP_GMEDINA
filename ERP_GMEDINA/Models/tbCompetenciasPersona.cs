
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbCompetenciasPersona
    {
        public int cope_Id { get; set; }
        public int per_Id { get; set; }
        public int comp_Id { get; set; }
        public bool cope_Estado { get; set; }
        public string cope_RazonInactivo { get; set; }
        public int cope_UsuarioCrea { get; set; }
        public System.DateTime cope_FechaCrea { get; set; }
        public Nullable<int> cope_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> cope_FechaModifica { get; set; }
    
        public virtual tbCompetencias tbCompetencias { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
