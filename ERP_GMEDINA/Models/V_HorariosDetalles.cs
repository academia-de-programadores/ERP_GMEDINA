
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HorariosDetalles
    {
        public int hor_Id { get; set; }
        public int jor_Id { get; set; }
        public string hor_Descripcion { get; set; }
        public string hor_HoraInicio { get; set; }
        public string hor_HoraFin { get; set; }
        public string hor_CantidadHoras { get; set; }
        public bool hor_Estado { get; set; }
        public string hor_RazonInactivo { get; set; }
        public int hor_UsuarioCrea { get; set; }
        public System.DateTime hor_FechaCrea { get; set; }
        public Nullable<int> hor_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hor_FechaModifica { get; set; }
    }
}
