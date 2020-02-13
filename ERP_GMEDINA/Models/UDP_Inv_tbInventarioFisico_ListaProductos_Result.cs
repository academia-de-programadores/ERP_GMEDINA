
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Inv_tbInventarioFisico_ListaProductos_Result
    {
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Marca { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Modelo { get; set; }
        public int uni_Id { get; set; }
        public string uni_Descripcion { get; set; }
    }
}
