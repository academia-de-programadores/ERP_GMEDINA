
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbtiposalidas
    {
        public int tsal_Id { get; set; }
        public string tsal_Descripcion { get; set; }
        public bool tsal_Estado { get; set; }
        public string tsal_RazonInactivo { get; set; }
        public int tsal_UsuarioCrea { get; set; }
        public System.DateTime tsal_FechaCrea { get; set; }
        public Nullable<int> tsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    }
}
