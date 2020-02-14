
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_Acce_GetReportes_Result
    {
        public int obj_Id { get; set; }
        public string obj_Pantalla { get; set; }
        public string obj_Referencia { get; set; }
        public Nullable<int> obj_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> obj_FechaCrea { get; set; }
        public Nullable<int> obj_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> obj_FechaModifica { get; set; }
        public bool obj_Estado { get; set; }
    }
}
