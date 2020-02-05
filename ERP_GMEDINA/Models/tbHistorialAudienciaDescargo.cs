
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialAudienciaDescargo
    {
        public int aude_Id { get; set; }
        public int emp_Id { get; set; }
        public string aude_Descripcion { get; set; }
        public System.DateTime aude_FechaAudiencia { get; set; }
        public bool aude_Testigo { get; set; }
        public string aude_DireccionArchivo { get; set; }
        public bool aude_Estado { get; set; }
        public string aude_RazonInactivo { get; set; }
        public int aude_UsuarioCrea { get; set; }
        public System.DateTime aude_FechaCrea { get; set; }
        public Nullable<int> aude_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> aude_FechaModifica { get; set; }
    
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
