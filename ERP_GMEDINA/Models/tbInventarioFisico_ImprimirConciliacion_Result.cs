
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class tbInventarioFisico_ImprimirConciliacion_Result
    {
        public string invf_Descripcion { get; set; }
        public System.DateTime invf_FechaInventario { get; set; }
        public string estif_Descripcion { get; set; }
        public string bod_Nombre { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string uni_Descripcion { get; set; }
        public decimal invfd_CantidadSistema { get; set; }
        public decimal invfd_Cantidad { get; set; }
    }
}
