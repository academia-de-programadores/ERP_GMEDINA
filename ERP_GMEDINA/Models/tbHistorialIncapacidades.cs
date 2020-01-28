
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialIncapacidades
    {
        public int hinc_Id { get; set; }
        public int emp_Id { get; set; }
        public int ticn_Id { get; set; }
        public int hinc_Dias { get; set; }
        public string hinc_CentroMedico { get; set; }
        public string hinc_Doctor { get; set; }
        public string hinc_Diagnostico { get; set; }
        public Nullable<System.DateTime> hinc_FechaInicio { get; set; }
        public Nullable<System.DateTime> hinc_FechaFin { get; set; }
        public bool hinc_Estado { get; set; }
        public string hinc_RazonInactivo { get; set; }
        public int hinc_UsuarioCrea { get; set; }
        public System.DateTime hinc_FechaCrea { get; set; }
        public Nullable<int> hinc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hinc_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbTipoIncapacidades tbTipoIncapacidades { get; set; }
    }
}
