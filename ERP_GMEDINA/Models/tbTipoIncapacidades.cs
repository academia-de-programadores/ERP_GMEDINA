
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoIncapacidades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoIncapacidades()
        {
            this.tbHistorialIncapacidades = new HashSet<tbHistorialIncapacidades>();
        }
    
        public int ticn_Id { get; set; }
        public string ticn_Descripcion { get; set; }
        public bool ticn_Estado { get; set; }
        public string ticn_RazonInactivo { get; set; }
        public int ticn_UsuarioCrea { get; set; }
        public System.DateTime ticn_FechaCrea { get; set; }
        public Nullable<int> ticn_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> ticn_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialIncapacidades> tbHistorialIncapacidades { get; set; }
    }
}
