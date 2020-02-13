
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_MovimientoCaja_Apertura
    {
        public int mocja_Id { get; set; }
        public int solef_Id { get; set; }
        public short cja_Id { get; set; }
        public string cja_Descripcion { get; set; }
        public string suc_Descripcion { get; set; }
        public int suc_Id { get; set; }
    }
}
