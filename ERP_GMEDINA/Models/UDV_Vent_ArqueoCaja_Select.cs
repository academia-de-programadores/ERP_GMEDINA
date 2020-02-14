
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_ArqueoCaja_Select
    {
        public int mocja_Id { get; set; }
        public string suc_Descripcion { get; set; }
        public short cja_Id { get; set; }
        public string cja_Descripcion { get; set; }
        public Nullable<System.DateTime> mocja_FechaApertura { get; set; }
        public Nullable<System.DateTime> mocja_FechaArqueo { get; set; }
        public Nullable<System.DateTime> mocja_FechaAceptacion { get; set; }
    }
}
