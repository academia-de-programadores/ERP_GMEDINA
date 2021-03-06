
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbInventarioFisico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbInventarioFisico()
        {
            this.tbInventarioFisicoDetalle = new HashSet<tbInventarioFisicoDetalle>();
        }
    
        public int invf_Id { get; set; }
        public string invf_Descripcion { get; set; }
        public string invf_ResponsableBodega { get; set; }
        public int bod_Id { get; set; }
        public byte estif_Id { get; set; }
        public System.DateTime invf_FechaInventario { get; set; }
        public int invf_UsuarioCrea { get; set; }
        public System.DateTime invf_FechaCrea { get; set; }
        public Nullable<int> invf_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> invf_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbBodega tbBodega { get; set; }
        public virtual tbEstadoInventarioFisico tbEstadoInventarioFisico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbInventarioFisicoDetalle> tbInventarioFisicoDetalle { get; set; }
    }
}
