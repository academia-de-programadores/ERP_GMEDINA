
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbEmpleadoBonos
    {
        public int cb_Id { get; set; }
        public int emp_Id { get; set; }
        public int cin_IdIngreso { get; set; }
        public Nullable<decimal> cb_Monto { get; set; }
        public System.DateTime cb_FechaRegistro { get; set; }
        public bool cb_Pagado { get; set; }
        public int cb_UsuarioCrea { get; set; }
        public System.DateTime cb_FechaCrea { get; set; }
        public Nullable<int> cb_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> cb_FechaModifica { get; set; }
        public bool cb_Activo { get; set; }
        public Nullable<System.DateTime> cb_FechaPagado { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}
