
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialAmonestacion
    {
        public int emp_Id { get; set; }
        public string emp_Nombre { get; set; }
        public bool emp_Estado { get; set; }
        public int tamo_Id { get; set; }
        public string tamo_Descripcion { get; set; }
        public int hamo_Id { get; set; }
        public Nullable<System.DateTime> hamo_Fecha { get; set; }
        public string hamo_Observacion { get; set; }
        public bool hamo_Estado { get; set; }
        public string hamo_RazonInactivo { get; set; }
        public int hamo_UsuarioCrea { get; set; }
        public System.DateTime hamo_FechaCrea { get; set; }
        public Nullable<int> hamo_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hamo_FechaModifica { get; set; }
    }
}
