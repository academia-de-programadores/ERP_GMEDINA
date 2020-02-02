
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbSueldos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSueldos()
        {
            this.tbSueldos1 = new HashSet<tbSueldos>();
        }
    
        public int sue_Id { get; set; }
        public int emp_Id { get; set; }
        public int tmon_Id { get; set; }
        public decimal sue_Cantidad { get; set; }
        public Nullable<int> sue_SueldoAnterior { get; set; }
        public bool sue_Estado { get; set; }
        public string sue_RazonInactivo { get; set; }
        public int sue_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }
        public Nullable<int> sue_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldos> tbSueldos1 { get; set; }
        public virtual tbSueldos tbSueldos2 { get; set; }
        public virtual tbTipoMonedas tbTipoMonedas { get; set; }
    }
}
