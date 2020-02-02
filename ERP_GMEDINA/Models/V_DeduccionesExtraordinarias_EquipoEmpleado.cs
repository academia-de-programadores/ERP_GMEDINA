
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DeduccionesExtraordinarias_EquipoEmpleado
    {
        public int eqem_Id { get; set; }
        public int eqtra_Id { get; set; }
        public string eqtra_Codigo { get; set; }
        public string per_EquipoEmpleado { get; set; }
    }
}
