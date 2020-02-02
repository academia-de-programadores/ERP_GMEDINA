
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPagoDeCesantiaEncabezado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPagoDeCesantiaEncabezado()
        {
            this.tbPagoDeCesantiaDetalle = new HashSet<tbPagoDeCesantiaDetalle>();
        }
    
        public int pdce_IdCesantiaEncabezado { get; set; }
        public string pdce_CodigoPlanillaCesantias { get; set; }
        public decimal pdce_TotalCesantias { get; set; }
        public int pdce_UsuarioCrea { get; set; }
        public System.DateTime pdce_FechaCrea { get; set; }
        public Nullable<int> pdce_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pdce_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPagoDeCesantiaDetalle> tbPagoDeCesantiaDetalle { get; set; }
    }
}
