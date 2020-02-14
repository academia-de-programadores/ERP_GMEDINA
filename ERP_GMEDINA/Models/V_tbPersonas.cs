
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbPersonas
    {
        public int per_Id { get; set; }
        public int Relacion_Id { get; set; }
        public string Descripcion { get; set; }
        public string Relacion { get; set; }
    }
}
