
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_tbProductoSubcategoria_Select_Result
    {
        public int pscat_Id { get; set; }
        public string pscat_Descripcion { get; set; }
        public int pcat_Id { get; set; }
        public byte pscat_EsActiva { get; set; }
        public int pscat_UsuarioCrea { get; set; }
        public System.DateTime pscat_FechaCrea { get; set; }
        public Nullable<int> pscat_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pscat_FechaModifica { get; set; }
        public decimal pscat_ISV { get; set; }
    }
}
