
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbDenominacionArqueo_Edit_Result
    {
        public int arqde_Id { get; set; }
        public int mocja_Id { get; set; }
        public string mnda_Abreviatura { get; set; }
        public string mnda_Nombre { get; set; }
        public string TipoDenominacion { get; set; }
        public string deno_Descripcion { get; set; }
        public decimal deno_valor { get; set; }
        public short arqde_CantidadDenominacion { get; set; }
        public decimal arqde_MontoDenominacion { get; set; }
    }
}
