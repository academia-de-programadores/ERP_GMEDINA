
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialContrataciones
    {
        public int hcon_Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Identidad { get; set; }
        public int car_Id { get; set; }
        public string Cargo { get; set; }
        public Nullable<System.DateTime> FechaContratacion { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}
