
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_Inv_Salida_Imprimir_Reporte_Result
    {
        public Nullable<int> sal_Id { get; set; }
        public Nullable<System.DateTime> sal_FechaElaboracion { get; set; }
        public string tsal_Descripcion { get; set; }
        public string bod_Nombre { get; set; }
        public string sal_BodDestino { get; set; }
        public string fact_Codigo { get; set; }
    }
}
