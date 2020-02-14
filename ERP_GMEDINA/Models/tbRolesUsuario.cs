
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbRolesUsuario
    {
        public int rolu_Id { get; set; }
        public Nullable<int> rol_Id { get; set; }
        public Nullable<int> usu_Id { get; set; }
        public Nullable<int> rolu_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> rolu_FechaCrea { get; set; }
        public Nullable<int> rolu_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> rolu_FechaModifica { get; set; }
    
        public virtual tbRol tbRol { get; set; }
        public virtual tbRol tbRol1 { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbUsuario tbUsuario2 { get; set; }
        public virtual tbUsuario tbUsuario3 { get; set; }
        public virtual tbUsuario tbUsuario4 { get; set; }
        public virtual tbUsuario tbUsuario5 { get; set; }
    }
}
