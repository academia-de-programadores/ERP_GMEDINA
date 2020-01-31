
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbEmpleadoComisiones
    {
        public int cc_Id { get; set; }
        public int emp_Id { get; set; }
        public int cin_IdIngreso { get; set; }
        public System.DateTime cc_FechaRegistro { get; set; }
        public bool cc_Pagado { get; set; }
        public int cc_UsuarioCrea { get; set; }
        public System.DateTime cc_FechaCrea { get; set; }
        public Nullable<int> cc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> cc_FechaModifica { get; set; }
        public bool cc_Activo { get; set; }
        public decimal cc_TotalComision { get; set; }
        public decimal cc_TotalVenta { get; set; }
        public Nullable<System.DateTime> cc_FechaPagado { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}
