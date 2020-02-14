
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_VentasPorCaja_EntreFechas
    {
        public System.DateTime fact_Fecha { get; set; }
        public string fact_Codigo { get; set; }
        public string clte_Nombres { get; set; }
        public string suc_Descripcion { get; set; }
        public string cja_Descripcion { get; set; }
        public string Cajero { get; set; }
        public Nullable<decimal> Total_facturado { get; set; }
        public decimal pago_TotalPago { get; set; }
        public short cja_Id { get; set; }
        public int fact_UsuarioCrea { get; set; }
        public int suc_Id { get; set; }
    }
}
