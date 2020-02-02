
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialSalidas
    {
        public int hsal_Id { get; set; }
        public int emp_Id { get; set; }
        public string Nombre_Empleado { get; set; }
        public string tsal_Descripcion { get; set; }
        public int tsal_Id { get; set; }
        public string observaciones { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
        public Nullable<System.DateTime> fechafin { get; set; }
    }
}
