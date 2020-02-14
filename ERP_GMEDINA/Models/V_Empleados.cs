
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Empleados
    {
        public int emp_Id { get; set; }
        public int per_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string per_NombreCompleto { get; set; }
        public bool emp_Estado { get; set; }
    }
}
