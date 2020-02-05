
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialSalidas
    {
        public int hsal_Id { get; set; }
        public int emp_Id { get; set; }
        public int tsal_Id { get; set; }
        public int rsal_Id { get; set; }
        public System.DateTime hsal_FechaSalida { get; set; }
        public string hsal_Observacion { get; set; }
        public bool hsal_Estado { get; set; }
        public string hsal_RazonInactivo { get; set; }
        public int hsal_UsuarioCrea { get; set; }
        public System.DateTime hsal_FechaCrea { get; set; }
        public Nullable<int> hsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hsal_FechaModifica { get; set; }
    
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbRazonSalidas tbRazonSalidas { get; set; }
        public virtual tbTipoSalidas tbTipoSalidas { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
