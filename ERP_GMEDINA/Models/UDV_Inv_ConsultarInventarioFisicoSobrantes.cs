
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Inv_ConsultarInventarioFisicoSobrantes
    {
        public System.DateTime invf_FechaInventario { get; set; }
        public string invf_Descripcion { get; set; }
        public string bod_Nombre { get; set; }
        public int bod_Id { get; set; }
        public string bod_Telefono { get; set; }
        public string invf_ResponsableBodega { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public string uni_Descripcion { get; set; }
        public decimal invfd_Cantidad { get; set; }
        public decimal invfd_CantidadSistema { get; set; }
        public Nullable<decimal> Sobrante_Faltante { get; set; }
    }
}
