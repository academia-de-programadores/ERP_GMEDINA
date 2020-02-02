
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbLiquidacionVacaciones
    {
        public int pvac_IdPagoVacaciones { get; set; }
        public int pvac_Antiguedad { get; set; }
        public int pvac_dias { get; set; }
        public int pvac_UsuarioCrea { get; set; }
        public System.DateTime pvac_FechaCrea { get; set; }
        public Nullable<int> pvac_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pvac_FechaModifica { get; set; }
        public bool pvac_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
