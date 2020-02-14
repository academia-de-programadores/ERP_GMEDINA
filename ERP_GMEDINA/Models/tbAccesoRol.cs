
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAccesoRol
    {
        public int acrol_Id { get; set; }
        public Nullable<int> rol_Id { get; set; }
        public Nullable<int> obj_Id { get; set; }
        public Nullable<int> acrol_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> acrol_FechaCrea { get; set; }
        public Nullable<int> acrol_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> acrol_FechaModifica { get; set; }
    
        public virtual tbObjeto tbObjeto { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbRol tbRol { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbObjeto tbObjeto1 { get; set; }
        public virtual tbRol tbRol1 { get; set; }
        public virtual tbUsuario tbUsuario2 { get; set; }
        public virtual tbUsuario tbUsuario3 { get; set; }
    }
}
