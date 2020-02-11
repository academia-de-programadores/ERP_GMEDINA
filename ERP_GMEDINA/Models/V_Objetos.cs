
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Objetos
    {
        public int obj_Id { get; set; }
        public string obj_Pantalla { get; set; }
        public string usu_NombreUsuario { get; set; }
        public Nullable<System.DateTime> obj_FechaCrea { get; set; }
        public Nullable<int> obj_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> obj_FechaModifica { get; set; }
    }
}
