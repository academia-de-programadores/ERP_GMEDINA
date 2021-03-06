
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbBox
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbBox()
        {
            this.tbBoxDetalle = new HashSet<tbBoxDetalle>();
            this.tbSalidaDetalle = new HashSet<tbSalidaDetalle>();
        }
    
        public string box_Codigo { get; set; }
        public string box_Descripcion { get; set; }
        public int bod_Id { get; set; }
        public byte box_Estado { get; set; }
        public int box_UsuarioCrea { get; set; }
        public System.DateTime box_FechaCrea { get; set; }
        public Nullable<int> box_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> box_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbBodega tbBodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbBoxDetalle> tbBoxDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSalidaDetalle> tbSalidaDetalle { get; set; }
    }
}
