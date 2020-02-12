
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPeriodos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPeriodos()
        {
            this.tbCatalogoDePlanillas = new HashSet<tbCatalogoDePlanillas>();
            this.tbHistorialDePago = new HashSet<tbHistorialDePago>();
        }
    
        public int peri_IdPeriodo { get; set; }
        public string peri_DescripPeriodo { get; set; }
        public int peri_UsuarioCrea { get; set; }
        public System.DateTime peri_FechaCrea { get; set; }
        public Nullable<int> peri_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> peri_FechaModifica { get; set; }
        public bool peri_Activo { get; set; }
        public Nullable<bool> peri_RecibeSeptimoDia { get; set; }
        public Nullable<int> peri_CantidadDias { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCatalogoDePlanillas> tbCatalogoDePlanillas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialDePago> tbHistorialDePago { get; set; }
    }
}
