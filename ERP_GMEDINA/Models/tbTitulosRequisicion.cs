
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTitulosRequisicion
    {
        public int treq_Id { get; set; }
        public int req_Id { get; set; }
        public int titu_Id { get; set; }
        public bool treq_Estado { get; set; }
        public string treq_RazonInactivo { get; set; }
        public int treq_UsuarioCrea { get; set; }
        public System.DateTime treq_FechaCrea { get; set; }
        public Nullable<int> treq_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> treq_FechaModifica { get; set; }
    
        public virtual tbRequisiciones tbRequisiciones { get; set; }
        public virtual tbTitulos tbTitulos { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
