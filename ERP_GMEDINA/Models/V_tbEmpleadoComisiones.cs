
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbEmpleadoComisiones
    {
        public int emp_Id { get; set; }
        public string per_Identidad { get; set; }
        public string Nombre_Completo { get; set; }
        public string car_Descripcion { get; set; }
        public string depto_Descripcion { get; set; }
        public string area_Descripcion { get; set; }
        public string jor_Descripcion { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
    }
}
