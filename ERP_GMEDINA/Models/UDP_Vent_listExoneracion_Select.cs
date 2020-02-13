
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDP_Vent_listExoneracion_Select
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool persona_natural { get; set; }
        public string RTN_Identificacion { get; set; }
        public string persona_comercial { get; set; }
        public string apellido { get; set; }
    }
}
