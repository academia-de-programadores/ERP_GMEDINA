
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_Inv_tbBoxDetalle_Select_Result
    {
        public int boxd_Id { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public string prod_Color { get; set; }
        public string uni_Descripcion { get; set; }
        public string pcat_Nombre { get; set; }
        public string pscat_Descripcion { get; set; }
        public decimal boxd_Cantidad { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string box_Codigo { get; set; }
    }
}
