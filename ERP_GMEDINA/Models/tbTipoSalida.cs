
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoSalida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoSalida()
        {
            this.tbSalida = new HashSet<tbSalida>();
        }
    
        public byte tsal_Id { get; set; }
        public string tsal_Descripcion { get; set; }
        public int tsal_UsuarioCrea { get; set; }
        public System.DateTime tsal_FechaCrea { get; set; }
        public Nullable<int> tsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalida> tbSalida { get; set; }
    }
}
