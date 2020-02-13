
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDirectoriosEmpleados
    {
        public int direm_Id { get; set; }
        public string direm_Carpeta { get; set; }
        public string direm_NombreArchivo { get; set; }
        public int emp_Id { get; set; }
        public bool direm_Estado { get; set; }
        public string direm_RazonInactivo { get; set; }
        public int direm_UsuarioCrea { get; set; }
        public System.DateTime direm_FechaCrea { get; set; }
        public Nullable<int> direm_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> direm_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}
