
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbDevolucion_GetDetalle_Result
    {
        public int devd_Id { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public decimal devd_CantidadProducto { get; set; }
        public string devd_Descripcion { get; set; }
        public decimal devd_Monto { get; set; }
        public long fact_Id { get; set; }
        public System.DateTime dev_Fecha { get; set; }
    }
}
