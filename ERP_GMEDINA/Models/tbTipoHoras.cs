namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoHoras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoHoras()
        {
            this.tbHistorialHorasTrabajadas = new HashSet<tbHistorialHorasTrabajadas>();
        }
    
        public int tiho_Id { get; set; }
        public string tiho_Descripcion { get; set; }
        public int tiho_Recargo { get; set; }
        public bool tiho_Estado { get; set; }
        public string tiho_RazonInactivo { get; set; }
        public int tiho_UsuarioCrea { get; set; }
        public System.DateTime tiho_FechaCrea { get; set; }
        public Nullable<int> tiho_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tiho_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialHorasTrabajadas> tbHistorialHorasTrabajadas { get; set; }
    }
}
