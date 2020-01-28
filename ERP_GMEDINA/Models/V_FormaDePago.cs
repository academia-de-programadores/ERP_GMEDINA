namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_FormaDePago
    {
        public int fpa_IdFormaPago { get; set; }
        public string fpa_Descripcion { get; set; }
        public int fpa_UsuarioCrea { get; set; }
        public System.DateTime fpa_FechaCrea { get; set; }
        public Nullable<int> fpa_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> fpa_FechaModifica { get; set; }
        public bool fpa_Activo { get; set; }
    }
}
