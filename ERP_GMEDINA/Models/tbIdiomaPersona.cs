
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbIdiomaPersona
    {
        public int idpe_Id { get; set; }
        public Nullable<int> per_Id { get; set; }
        public Nullable<int> idi_Id { get; set; }
        public bool idpe_Estado { get; set; }
        public string idpe_RazonInactivo { get; set; }
        public int idpe_UsuarioCrea { get; set; }
        public System.DateTime idpe_FechaCrea { get; set; }
        public Nullable<int> idpe_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> idpe_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbIdiomas tbIdiomas { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
    }
}
