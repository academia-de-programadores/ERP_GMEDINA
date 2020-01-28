namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoAmonestaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoAmonestaciones()
        {
            this.tbHistorialAmonestaciones = new HashSet<tbHistorialAmonestaciones>();
        }
    
        public int tamo_Id { get; set; }
        public string tamo_Descripcion { get; set; }
        public bool tamo_Estado { get; set; }
        public string tamo_RazonInactivo { get; set; }
        public int tamo_UsuarioCrea { get; set; }
        public System.DateTime tamo_FechaCrea { get; set; }
        public Nullable<int> tamo_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tamo_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialAmonestaciones> tbHistorialAmonestaciones { get; set; }
    }
}
