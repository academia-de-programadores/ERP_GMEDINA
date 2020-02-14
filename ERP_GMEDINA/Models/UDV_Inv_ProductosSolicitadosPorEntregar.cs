
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Inv_ProductosSolicitadosPorEntregar
    {
        public int sal_Id { get; set; }
        public int bod_Id { get; set; }
        public string BodegaOrigen { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public System.DateTime sal_FechaElaboracion { get; set; }
        public byte estm_Id { get; set; }
        public string estm_Descripcion { get; set; }
        public byte tsal_Id { get; set; }
        public string tsal_Descripcion { get; set; }
        public int sal_BodDestino { get; set; }
        public string BodegaDestino { get; set; }
        public decimal sald_Cantidad { get; set; }
        public int sald_Id { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public int uni_Id { get; set; }
        public string uni_Descripcion { get; set; }
    }
}
