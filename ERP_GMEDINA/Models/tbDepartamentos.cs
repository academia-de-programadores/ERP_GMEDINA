//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDepartamentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbDepartamentos()
        {
            this.tbEmpleados = new HashSet<tbEmpleados>();
            this.tbHistorialContrataciones = new HashSet<tbHistorialContrataciones>();
            this.tbRequisicionPersonal = new HashSet<tbRequisicionPersonal>();
        }
    
        public int depto_Id { get; set; }
        public int area_Id { get; set; }
        public int car_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public bool depto_Estado { get; set; }
        public string depto_RazonInactivo { get; set; }
        public int depto_UsuarioCrea { get; set; }
        public System.DateTime depto_Fechacrea { get; set; }
        public Nullable<int> depto_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> depto_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbAreas tbAreas { get; set; }
        public virtual tbCargos tbCargos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleados> tbEmpleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialContrataciones> tbHistorialContrataciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbRequisicionPersonal> tbRequisicionPersonal { get; set; }
    }
}
