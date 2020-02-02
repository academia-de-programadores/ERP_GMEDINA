
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Datos_Empleado
    {
        public int emp_Id { get; set; }
        public int area_Id { get; set; }
        public string area_Descripcion { get; set; }
        public int car_Id { get; set; }
        public string car_Descripcion { get; set; }
        public int depto_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public string jor_Descripcion { get; set; }
    }
}
