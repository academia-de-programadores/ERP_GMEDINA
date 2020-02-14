
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_Efectivos_y_Pagos_Result
    {
        public short cja_Id { get; set; }
        public Nullable<decimal> Efectivo_Inicial { get; set; }
        public Nullable<decimal> Efectivo_Recibido { get; set; }
        public Nullable<decimal> Efectivo_Entregado { get; set; }
        public Nullable<decimal> Efectivo_Pagos { get; set; }
        public Nullable<decimal> SUPER_TOTAL_EFECTIVO { get; set; }
    }
}
