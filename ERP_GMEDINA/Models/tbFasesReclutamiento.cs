
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbFasesReclutamiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbFasesReclutamiento()
        {
            this.tbFaseSeleccion = new HashSet<tbFaseSeleccion>();
            this.tbSeleccionCandidatos = new HashSet<tbSeleccionCandidatos>();
        }
    
        public int fare_Id { get; set; }
        public string fare_Descripcion { get; set; }
        public bool fare_Estado { get; set; }
        public string fare_RazonInactivo { get; set; }
        public int fare_UsuarioCrea { get; set; }
        public System.DateTime fare_FechaCrea { get; set; }
        public Nullable<int> fare_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> fare_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFaseSeleccion> tbFaseSeleccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSeleccionCandidatos> tbSeleccionCandidatos { get; set; }
    }
}
