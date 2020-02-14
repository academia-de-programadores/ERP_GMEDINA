
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTitulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTitulos()
        {
            this.tbTitulosPersona = new HashSet<tbTitulosPersona>();
            this.tbTitulosRequisicion = new HashSet<tbTitulosRequisicion>();
        }
    
        public int titu_Id { get; set; }
        public string titu_Descripcion { get; set; }
        public bool titu_Estado { get; set; }
        public string titu_RazonInactivo { get; set; }
        public int titu_UsuarioCrea { get; set; }
        public System.DateTime titu_FechaCrea { get; set; }
        public Nullable<int> titu_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> titu_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTitulosPersona> tbTitulosPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTitulosRequisicion> tbTitulosRequisicion { get; set; }
    }
}
