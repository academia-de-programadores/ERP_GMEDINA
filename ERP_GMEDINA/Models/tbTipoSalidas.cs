namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoSalidas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoSalidas()
        {
            this.tbHistorialSalidas = new HashSet<tbHistorialSalidas>();
        }
    
        public int tsal_Id { get; set; }
        public string tsal_Descripcion { get; set; }
        public bool tsal_Estado { get; set; }
        public string tsal_RazonInactivo { get; set; }
        public int tsal_UsuarioCrea { get; set; }
        public System.DateTime tsal_FechaCrea { get; set; }
        public Nullable<int> tsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialSalidas> tbHistorialSalidas { get; set; }
    }
}
