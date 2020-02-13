
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_tbSolicituEfectivo_EntreFechas
    {
        public int solef_Id { get; set; }
        public string cja_Descripcion { get; set; }
        public string suc_Descripcion { get; set; }
        public string Cajero { get; set; }
        public string mnda_Nombre { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<decimal> MontoSolicitado { get; set; }
        public Nullable<decimal> MontoEntregado { get; set; }
        public bool solef_EsApertura { get; set; }
        public bool solef_EsAnulada { get; set; }
        public int solef_UsuarioCrea { get; set; }
        public short cja_Id { get; set; }
    }
}
