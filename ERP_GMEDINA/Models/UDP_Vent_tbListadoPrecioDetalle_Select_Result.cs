
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbListadoPrecioDetalle_Select_Result
    {
        public string prod_Codigo { get; set; }
        public int lispd_Id { get; set; }
        public string prod_Descripcion { get; set; }
        public decimal lispd_PrecioMayorista { get; set; }
        public decimal lispd_PrecioMinorista { get; set; }
        public Nullable<decimal> lispd_DescCaja { get; set; }
        public Nullable<decimal> lispd_DescGerente { get; set; }
    }
}
