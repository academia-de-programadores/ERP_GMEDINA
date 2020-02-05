
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbEquipoEmpleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEquipoEmpleados()
        {
            this.tbDeduccionesExtraordinarias = new HashSet<tbDeduccionesExtraordinarias>();
        }
    
        public int eqem_Id { get; set; }
        public int emp_Id { get; set; }
        public int eqtra_Id { get; set; }
        public System.DateTime eqem_Fecha { get; set; }
        public bool eqem_Estado { get; set; }
        public string eqem_RazonInactivo { get; set; }
        public int eqem_UsuarioCrea { get; set; }
        public System.DateTime eqem_FechaCrea { get; set; }
        public Nullable<int> eqem_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> eqem_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDeduccionesExtraordinarias> tbDeduccionesExtraordinarias { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbEquipoTrabajo tbEquipoTrabajo { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
