namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialHorasTrabajadas
    {
        public int htra_Id { get; set; }
        public int emp_Id { get; set; }
        public int tiho_Id { get; set; }
        public int jor_Id { get; set; }
        public int htra_CantidadHoras { get; set; }
        public Nullable<System.DateTime> htra_Fecha { get; set; }
        public bool htra_Estado { get; set; }
        public string htra_RazonInactivo { get; set; }
        public int htra_UsuarioCrea { get; set; }
        public System.DateTime htra_FechaCrea { get; set; }
        public Nullable<int> htra_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> htra_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbJornadas tbJornadas { get; set; }
        public virtual tbTipoHoras tbTipoHoras { get; set; }
    }
}
