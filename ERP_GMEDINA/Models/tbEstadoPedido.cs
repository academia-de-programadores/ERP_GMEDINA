
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbEstadoPedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEstadoPedido()
        {
            this.tbPedido = new HashSet<tbPedido>();
        }
    
        public byte esped_Id { get; set; }
        public string esped_Descripcion { get; set; }
        public int esped_UsuarioCrea { get; set; }
        public System.DateTime esped_FechaCrea { get; set; }
        public Nullable<int> esped_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> esped_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedido> tbPedido { get; set; }
    }
}
