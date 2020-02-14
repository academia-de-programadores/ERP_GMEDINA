
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_tbentradadetalle_Select_Result
    {
        public int entd_Id { get; set; }
        public int ent_Id { get; set; }
        public string prod_Codigo { get; set; }
        public decimal entd_Cantidad { get; set; }
        public int entd_UsuarioCrea { get; set; }
        public System.DateTime entd_FechaCrea { get; set; }
        public Nullable<int> entd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> entd_FechaModifica { get; set; }
    }
}
