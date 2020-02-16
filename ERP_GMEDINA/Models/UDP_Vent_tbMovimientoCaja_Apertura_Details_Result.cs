
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbMovimientoCaja_Apertura_Details_Result
    {
        public int solef_Id { get; set; }
        public int soled_Id { get; set; }
        public Nullable<System.DateTime> mocja_FechaApertura { get; set; }
        public string CAJERO { get; set; }
        public string mnda_Abreviatura { get; set; }
        public string mnda_Nombre { get; set; }
        public short soled_CantidadEntregada { get; set; }
        public short soled_CantidadSolicitada { get; set; }
        public decimal soled_MontoEntregado { get; set; }
        public decimal deno_valor { get; set; }
        public short deno_Id { get; set; }
        public string deno_Descripcion { get; set; }
    }
}
