
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialCargos
    {
        public int hcar_Id { get; set; }
        public string Nombre { get; set; }
        public string Identidad { get; set; }
        public string CargoAnterior { get; set; }
        public int car_Id { get; set; }
        public string CargoNuevo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public int emp_Id { get; set; }
    }
}
