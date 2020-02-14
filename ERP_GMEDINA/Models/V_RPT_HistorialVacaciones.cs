
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialVacaciones
    {
        public int hvac_Id { get; set; }
        public Nullable<int> emp_Id { get; set; }
        public string Nombre_Empleado { get; set; }
        public int hvac_CantDias { get; set; }
        public System.DateTime hvac_FechaInicio { get; set; }
        public bool hvac_DiasPagados { get; set; }
        public int hvac_MesVacaciones { get; set; }
        public int hvac_anioVacaciones { get; set; }
        public System.DateTime hvac_FechaFin { get; set; }
        public string per_Identidad { get; set; }
        public string per_Telefono { get; set; }
    }
}
