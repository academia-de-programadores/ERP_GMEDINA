
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialAmonestaciones
    {
        public int hamo_Id { get; set; }
        public string Colaborador { get; set; }
        public string Identidad { get; set; }
        public string TipoAmonestación { get; set; }
        public string Observaciones { get; set; }
        public System.DateTime Fecha { get; set; }
        public int tamo_Id { get; set; }
        public string DescripcionAnterior { get; set; }
        public string ObservacionAnterior { get; set; }
        public Nullable<System.DateTime> FechaAnterior { get; set; }
    }
}
