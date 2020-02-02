
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTechosComisiones
    {
        public int tc_Id { get; set; }
        public int cin_IdIngreso { get; set; }
        public decimal tc_RangoInicio { get; set; }
        public decimal tc_RangoFin { get; set; }
        public decimal tc_PorcentajeComision { get; set; }
        public bool tc_Estado { get; set; }
        public int tc_UsuarioCrea { get; set; }
        public System.DateTime tc_FechaCrea { get; set; }
        public Nullable<int> tc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tc_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
    }
}
