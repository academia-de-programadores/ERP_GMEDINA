
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_Inv_PackingList_Result
    {
        public string box_Codigo { get; set; }
        public string box_Descripcion { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public string uni_Descripcion { get; set; }
        public decimal sald_Cantidad { get; set; }
        public string bod_Nombre { get; set; }
        public int sal_BodDestino { get; set; }
        public string Column1 { get; set; }
        public System.DateTime box_FechaCrea { get; set; }
    }
}
