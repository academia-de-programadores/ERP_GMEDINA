
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Historialvacaciones
    {
        public int emp_Id { get; set; }
        public string emp_nombre { get; set; }
        public bool emp_Estado { get; set; }
        public int hvac_Id { get; set; }
        public System.DateTime hvac_FechaInicio { get; set; }
        public System.DateTime hvac_FechaFin { get; set; }
        public int hvac_CantDias { get; set; }
        public bool hvac_DiasPagados { get; set; }
        public int hvac_MesVacaciones { get; set; }
        public int hvac_AnioVacaciones { get; set; }
        public bool hvac_Estado { get; set; }
        public string hvac_RazonInactivo { get; set; }
        public int hvac_UsuarioCrea { get; set; }
        public System.DateTime hvac_FechaCrea { get; set; }
        public Nullable<int> hvac_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hvac_FechaModifica { get; set; }
    }
}
