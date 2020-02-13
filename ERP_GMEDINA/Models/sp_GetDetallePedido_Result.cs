
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class sp_GetDetallePedido_Result
    {
        public int ped_Id { get; set; }
        public int pedd_Id { get; set; }
        public string prod_Codigo { get; set; }
        public decimal pedd_Cantidad { get; set; }
        public string prod_Descripcion { get; set; }
        public decimal lispd_PrecioMayorista { get; set; }
        public decimal pscat_ISV { get; set; }
    }
}
