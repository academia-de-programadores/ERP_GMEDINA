
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDeduccionImpuestoVecinal
    {
        public int dimv_Id { get; set; }
        public Nullable<int> emp_Id { get; set; }
        public Nullable<decimal> dimv_MontoTotal { get; set; }
        public Nullable<decimal> dimv_CuotaAPagar { get; set; }
        public Nullable<int> dimv_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> dimv_FechaCrea { get; set; }
        public Nullable<int> dimv_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dimv_FechaModifica { get; set; }
        public bool dimv_Estado { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}
