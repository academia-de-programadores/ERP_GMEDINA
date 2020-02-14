
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_FacturaPuntoEmision
    {
        public short cja_Id { get; set; }
        public int suc_Id { get; set; }
        public int pemi_Id { get; set; }
        public string pemi_NumeroCAI { get; set; }
        public string pemid_RangoInicio { get; set; }
        public string pemid_RangoFinal { get; set; }
        public System.DateTime pemid_FechaLimite { get; set; }
        public string dfisc_Id { get; set; }
    }
}
