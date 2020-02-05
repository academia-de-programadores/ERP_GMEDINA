
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHorarios
    {
        public int hor_Id { get; set; }
        public int jor_Id { get; set; }
        public string hor_Descripcion { get; set; }
        public System.TimeSpan hor_HoraInicio { get; set; }
        public System.TimeSpan hor_HoraFin { get; set; }
        public bool hor_Estado { get; set; }
        public string hor_RazonInactivo { get; set; }
        public int hor_UsuarioCrea { get; set; }
        public System.DateTime hor_FechaCrea { get; set; }
        public Nullable<int> hor_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hor_FechaModifica { get; set; }
        public int hor_CantidadHoras { get; set; }
    
        public virtual tbJornadas tbJornadas { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
