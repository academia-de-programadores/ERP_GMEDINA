
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPuntoEmision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPuntoEmision()
        {
            this.tbSucursales = new HashSet<tbSucursales>();
            this.tbPuntoEmisionDetalle = new HashSet<tbPuntoEmisionDetalle>();
            this.tbPuntoEmisionDetalle1 = new HashSet<tbPuntoEmisionDetalle>();
        }
    
        public int pemi_Id { get; set; }
        public string pemi_NumeroCAI { get; set; }
        public int pemi_UsuarioCrea { get; set; }
        public System.DateTime pemi_FechaCrea { get; set; }
        public Nullable<int> pemi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pemi_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSucursales> tbSucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuntoEmisionDetalle> tbPuntoEmisionDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuntoEmisionDetalle> tbPuntoEmisionDetalle1 { get; set; }
    }
}
