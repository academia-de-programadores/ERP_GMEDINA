
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbTechosComisiones
    {
        public int tc_Id { get; set; }
        public int cin_IdIngreso { get; set; }
        public string cin_DescripcionIngreso { get; set; }
        public decimal tc_RangoInicio { get; set; }
        public decimal tc_RangoFin { get; set; }
        public decimal tc_PorcentajeComision { get; set; }
        public bool tc_Estado { get; set; }
        public int tc_UsuarioCrea { get; set; }
        public string NombreUsuarioCrea { get; set; }
        public string NombresCrea { get; set; }
        public System.DateTime tc_FechaCrea { get; set; }
        public Nullable<int> tc_UsuarioModifica { get; set; }
        public string NombreUsuarioModifica { get; set; }
        public string NombresModifica { get; set; }
        public Nullable<System.DateTime> tc_FechaModifica { get; set; }
    }
}
