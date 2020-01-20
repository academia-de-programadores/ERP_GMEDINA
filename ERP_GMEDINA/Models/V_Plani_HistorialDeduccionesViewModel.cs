using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class V_Plani_HistorialDeduccionesViewModel
    {
        public decimal totalISR { get; set; }
        public decimal AFP { get; set; }
        public string descripcion { get; set; }
        public decimal total { get; set; }
        public DateTime FechaPago { get; set; }
        public int TotalDeducciones { get; set; }
    }

    public class V_Plani_HistorialIngresoViewModel
    {
        public string descripcion { get; set; }
        public Nullable<decimal> total { get; set; }
        public System.DateTime fechaPago { get; set; }
    }
}