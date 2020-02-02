
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialIncapacidad
    {
        public int hinc_Id { get; set; }
        public int ticn_Id { get; set; }
        public string Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoIncapacidad { get; set; }
        public int Dias { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}
