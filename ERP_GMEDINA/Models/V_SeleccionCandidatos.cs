
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_SeleccionCandidatos
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public int FaseReclutamientoId { get; set; }
        public string Fase { get; set; }
        public int RequisionId { get; set; }
        public string Plaza_Solicitada { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public System.DateTime Fecha_Crea { get; set; }
        public int Usuario_Crea { get; set; }
        public Nullable<System.DateTime> Fecha_Modifica { get; set; }
        public Nullable<int> Usuario_Modifica { get; set; }
        public bool Estado { get; set; }
        public string Razon_Inactivo { get; set; }
    }
}
