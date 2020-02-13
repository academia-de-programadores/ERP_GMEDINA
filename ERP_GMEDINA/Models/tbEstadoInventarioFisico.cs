
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbEstadoInventarioFisico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEstadoInventarioFisico()
        {
            this.tbInventarioFisico = new HashSet<tbInventarioFisico>();
        }
    
        public byte estif_Id { get; set; }
        public string estif_Descripcion { get; set; }
        public int estif_UsuarioCrea { get; set; }
        public System.DateTime estif_FechaCrea { get; set; }
        public Nullable<int> estif_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> estif_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbInventarioFisico> tbInventarioFisico { get; set; }
    }
}
