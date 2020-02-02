
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialPermisos
    {
        public int emp_Id { get; set; }
        public string emp_Nombre { get; set; }
        public bool emp_Estado { get; set; }
        public int tper_Id { get; set; }
        public string tper_Descripcion { get; set; }
        public int hper_Id { get; set; }
        public System.DateTime hper_fechaInicio { get; set; }
        public System.DateTime hper_fechaFin { get; set; }
        public int hper_Duracion { get; set; }
        public string hper_Observacion { get; set; }
        public bool hper_Justificado { get; set; }
        public int hper_PorcentajeIndemnizado { get; set; }
        public bool hper_Estado { get; set; }
        public string hper_RazonInactivo { get; set; }
        public System.DateTime hper_FechaCrea { get; set; }
        public Nullable<int> hper_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hper_FechaModifica { get; set; }
    }
}
