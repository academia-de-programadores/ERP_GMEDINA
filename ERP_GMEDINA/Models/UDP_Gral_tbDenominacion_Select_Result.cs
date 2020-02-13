
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Gral_tbDenominacion_Select_Result
    {
        public short deno_Id { get; set; }
        public string deno_Descripcion { get; set; }
        public byte deno_Tipo { get; set; }
        public decimal deno_valor { get; set; }
        public short mnda_Id { get; set; }
    }
}
