
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class ObtenerRol_Result
    {
        public int rol_Id { get; set; }
        public string rol_Descripcion { get; set; }
        public Nullable<int> rol_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> rol_FechaCrea { get; set; }
        public Nullable<int> rol_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> rol_FechaModifica { get; set; }
        public bool rol_Estado { get; set; }
    }
}
