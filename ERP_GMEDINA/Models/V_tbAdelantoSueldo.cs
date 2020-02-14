
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbAdelantoSueldo
    {
        public int adsu_IdAdelantoSueldo { get; set; }
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Identidad { get; set; }
        public System.DateTime adsu_FechaAdelanto { get; set; }
        public string adsu_RazonAdelanto { get; set; }
        public Nullable<decimal> adsu_Monto { get; set; }
        public bool adsu_Deducido { get; set; }
        public int adsu_UsuarioCrea { get; set; }
        public string NombreUsuarioCrea { get; set; }
        public string UsuarioCrea { get; set; }
        public System.DateTime adsu_FechaCrea { get; set; }
        public Nullable<int> adsu_UsuarioModifica { get; set; }
        public string NombreUsuarioModifica { get; set; }
        public string UsuarioModifica { get; set; }
        public Nullable<System.DateTime> adsu_FechaModifica { get; set; }
        public bool adsu_Activo { get; set; }
    }
}
