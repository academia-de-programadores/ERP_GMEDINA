
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialIncapacidades
    {
        public int emp_Id { get; set; }
        public int hinc_Id { get; set; }
        public string NombreCompleto { get; set; }
        public int ticn_Id { get; set; }
        public string ticn_Descripcion { get; set; }
        public int hinc_Dias { get; set; }
        public string hinc_CentroMedico { get; set; }
        public string hinc_Doctor { get; set; }
        public string hinc_Diagnostico { get; set; }
        public Nullable<System.DateTime> hinc_FechaInicio { get; set; }
        public Nullable<System.DateTime> hinc_FechaFin { get; set; }
        public bool hinc_Estado { get; set; }
        public string hinc_RazonInactivo { get; set; }
        public Nullable<bool> hinc_Espermanente { get; set; }
    }
}
