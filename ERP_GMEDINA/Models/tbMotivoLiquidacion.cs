
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbMotivoLiquidacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMotivoLiquidacion()
        {
            this.tbPorcentajeMotivoLiquidacion = new HashSet<tbPorcentajeMotivoLiquidacion>();
        }
    
        public int moli_IdMotivo { get; set; }
        public string moli_Descripcion { get; set; }
        public Nullable<int> moli_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> moli_FechaCrea { get; set; }
        public Nullable<int> moli_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> moli_FechaModifica { get; set; }
        public Nullable<bool> moli_Activo { get; set; }
        public string moli_RazonInactivo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPorcentajeMotivoLiquidacion> tbPorcentajeMotivoLiquidacion { get; set; }
    }
}
