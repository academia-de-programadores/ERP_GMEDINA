
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTechoImpuestoVecinal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTechoImpuestoVecinal()
        {
            this.tbDeduccionImpuestoVecinal = new HashSet<tbDeduccionImpuestoVecinal>();
        }
    
        public int timv_IdTechoImpuestoVecinal { get; set; }
        public string mun_Codigo { get; set; }
        public Nullable<int> tde_IdTipoDedu { get; set; }
        public Nullable<decimal> timv_RangoInicio { get; set; }
        public Nullable<decimal> timv_RangoFin { get; set; }
        public decimal timv_Rango { get; set; }
        public decimal timv_Impuesto { get; set; }
        public int timv_UsuarioCrea { get; set; }
        public System.DateTime timv_FechaCrea { get; set; }
        public Nullable<int> timv_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> timv_FechaModifica { get; set; }
        public bool timv_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDeduccionImpuestoVecinal> tbDeduccionImpuestoVecinal { get; set; }
        public virtual tbTipoDeduccion tbTipoDeduccion { get; set; }
    }
}
