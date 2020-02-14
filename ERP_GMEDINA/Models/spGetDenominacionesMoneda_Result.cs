
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class spGetDenominacionesMoneda_Result
    {
        public short deno_Id { get; set; }
        public string deno_Descripcion { get; set; }
        public decimal deno_valor { get; set; }
        public string mnda_Abreviatura { get; set; }
        public byte TipoDenominacion { get; set; }
    }
}
