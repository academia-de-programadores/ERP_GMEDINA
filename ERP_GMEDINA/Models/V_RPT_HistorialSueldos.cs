
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialSueldos
    {
        public int sue_Id { get; set; }
        public string NombreEmp { get; set; }
        public int emp_Id { get; set; }
        public decimal sue_Cantidad { get; set; }
        public string tmon_Descripcion { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> fechafin { get; set; }
        public string car_Descripcion { get; set; }
        public int car_Id { get; set; }
    }
}
