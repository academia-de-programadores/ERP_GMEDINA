
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Vent_tbSolicitudEfectivo_DatosEncabezado_Result
    {
        public int solef_Id { get; set; }
        public int mocja_Id { get; set; }
        public int solef_UsuarioCrea { get; set; }
        public string suc_Descripcion { get; set; }
        public short cja_Id { get; set; }
        public string cja_Descripcion { get; set; }
        public Nullable<System.DateTime> FechaCrea { get; set; }
    }
}
