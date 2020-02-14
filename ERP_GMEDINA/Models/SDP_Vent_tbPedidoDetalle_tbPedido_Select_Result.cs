
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_Vent_tbPedidoDetalle_tbPedido_Select_Result
    {
        public int pedd_Id { get; set; }
        public int ped_Id { get; set; }
        public string prod_Codigo { get; set; }
        public decimal pedd_Cantidad { get; set; }
        public decimal pedd_CantidadFacturada { get; set; }
    }
}
