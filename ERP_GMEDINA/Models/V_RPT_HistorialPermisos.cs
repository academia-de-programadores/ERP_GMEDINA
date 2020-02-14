
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialPermisos
    {
        public int hper_Id { get; set; }
        public int emp_Id { get; set; }
        public string Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public int tper_Id { get; set; }
        public string TipoPermiso { get; set; }
        public int Porcentaje { get; set; }
        public bool emp_Estado { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}
