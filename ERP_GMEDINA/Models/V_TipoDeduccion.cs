
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_TipoDeduccion
    {
        public int tde_IdTipoDedu { get; set; }
        public string tde_Descripcion { get; set; }
        public int tde_UsuarioCrea { get; set; }
        public System.DateTime tde_FechaCrea { get; set; }
        public Nullable<int> tde_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tde_FechaModifica { get; set; }
        public bool tde_Activo { get; set; }
    }
}
