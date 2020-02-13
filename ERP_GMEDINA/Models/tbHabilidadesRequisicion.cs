
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHabilidadesRequisicion
    {
        public int hreq_Id { get; set; }
        public int req_Id { get; set; }
        public int habi_Id { get; set; }
        public bool hreq_Estado { get; set; }
        public string hreq_RazonInactivo { get; set; }
        public int hreq_UsuarioCrea { get; set; }
        public System.DateTime hreq_FechaCrea { get; set; }
        public Nullable<int> hreq_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hreq_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbHabilidades tbHabilidades { get; set; }
        public virtual tbRequisiciones tbRequisiciones { get; set; }
    }
}
