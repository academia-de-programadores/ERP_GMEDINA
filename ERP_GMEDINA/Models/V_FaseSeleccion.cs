
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_FaseSeleccion
    {
        public int scan_Id { get; set; }
        public int fsel_Id { get; set; }
        public int per_Id { get; set; }
        public string Fase { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}
