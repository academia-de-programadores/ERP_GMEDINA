
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoDeduccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoDeduccion()
        {
            this.tbAFP = new HashSet<tbAFP>();
            this.tbCatalogoDeDeducciones = new HashSet<tbCatalogoDeDeducciones>();
            this.tbISR = new HashSet<tbISR>();
            this.tbTechoImpuestoVecinal = new HashSet<tbTechoImpuestoVecinal>();
        }
    
        public int tde_IdTipoDedu { get; set; }
        public string tde_Descripcion { get; set; }
        public int tde_UsuarioCrea { get; set; }
        public System.DateTime tde_FechaCrea { get; set; }
        public Nullable<int> tde_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tde_FechaModifica { get; set; }
        public bool tde_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAFP> tbAFP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCatalogoDeDeducciones> tbCatalogoDeDeducciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbISR> tbISR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTechoImpuestoVecinal> tbTechoImpuestoVecinal { get; set; }
    }
}
