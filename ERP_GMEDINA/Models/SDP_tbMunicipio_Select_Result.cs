
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_tbMunicipio_Select_Result
    {
        public string mun_Codigo { get; set; }
        public string dep_Codigo { get; set; }
        public string mun_Nombre { get; set; }
        public int mun_UsuarioCrea { get; set; }
        public System.DateTime mun_FechaCrea { get; set; }
        public Nullable<int> mun_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    }
}
