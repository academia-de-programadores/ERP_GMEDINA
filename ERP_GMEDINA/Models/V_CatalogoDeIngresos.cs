
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_CatalogoDeIngresos
    {
        public int cin_IdIngreso { get; set; }
        public string cin_DescripcionIngreso { get; set; }
        public int cin_UsuarioCrea { get; set; }
        public System.DateTime cin_FechaCrea { get; set; }
        public Nullable<int> cin_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> cin_FechaModifica { get; set; }
        public bool cin_Activo { get; set; }
    }
}
