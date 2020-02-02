
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_FaseSeleccion
    {
        public int fsel_Id { get; set; }
        public int per_Id { get; set; }
        public int Id_Persona { get; set; }
        public string Nombre { get; set; }
        public int fare_Id { get; set; }
        public string Fase { get; set; }
        public int req_Id { get; set; }
        public string Plaza_Solicitada { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}
