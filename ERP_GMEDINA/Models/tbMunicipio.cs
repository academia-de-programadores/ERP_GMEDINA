
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbMunicipio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMunicipio()
        {
            this.tbTechoImpuestoVecinal = new HashSet<tbTechoImpuestoVecinal>();
        }
    
        public string mun_Codigo { get; set; }
        public string dep_Codigo { get; set; }
        public string mun_Nombre { get; set; }
        public int mun_UsuarioCrea { get; set; }
        public System.DateTime mun_FechaCrea { get; set; }
        public Nullable<int> mun_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    
        public virtual tbDepartamento tbDepartamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTechoImpuestoVecinal> tbTechoImpuestoVecinal { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
