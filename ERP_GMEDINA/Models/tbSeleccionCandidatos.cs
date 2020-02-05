
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbSeleccionCandidatos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSeleccionCandidatos()
        {
            this.tbFaseSeleccion = new HashSet<tbFaseSeleccion>();
            this.tbHistorialContrataciones = new HashSet<tbHistorialContrataciones>();
        }
    
        public int scan_Id { get; set; }
        public int per_Id { get; set; }
        public int fare_Id { get; set; }
        public Nullable<System.DateTime> scan_Fecha { get; set; }
        public bool scan_Estado { get; set; }
        public string scan_RazonInactivo { get; set; }
        public int scan_UsuarioCrea { get; set; }
        public System.DateTime scan_FechaCrea { get; set; }
        public Nullable<int> scan_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> scan_FechaModifica { get; set; }
        public int req_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFaseSeleccion> tbFaseSeleccion { get; set; }
        public virtual tbFasesReclutamiento tbFasesReclutamiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialContrataciones> tbHistorialContrataciones { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
        public virtual tbRequisiciones tbRequisiciones { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
