namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbCompetenciasRequisicion
    {
        public int creq_Id { get; set; }
        public int req_Id { get; set; }
        public int comp_Id { get; set; }
        public bool creq_Estado { get; set; }
        public string creq_RazonInactivo { get; set; }
        public int creq_UsuarioCrea { get; set; }
        public System.DateTime creq_FechaCrea { get; set; }
        public Nullable<int> creq_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> req_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCompetencias tbCompetencias { get; set; }
        public virtual tbRequisiciones tbRequisiciones { get; set; }
    }
}
