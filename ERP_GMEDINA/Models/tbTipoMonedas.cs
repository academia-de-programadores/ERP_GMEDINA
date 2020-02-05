
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoMonedas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoMonedas()
        {
            this.tbSueldos = new HashSet<tbSueldos>();
        }
    
        public int tmon_Id { get; set; }
        public string tmon_Descripcion { get; set; }
        public bool tmon_Estado { get; set; }
        public string tmon_RazonInactivo { get; set; }
        public int tmon_UsuarioCrea { get; set; }
        public System.DateTime tmon_FechaCrea { get; set; }
        public Nullable<int> tmon_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tmon_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldos> tbSueldos { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
