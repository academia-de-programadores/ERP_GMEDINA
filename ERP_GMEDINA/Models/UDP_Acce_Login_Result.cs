
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class UDP_Acce_Login_Result
    {
        public int usu_Id { get; set; }
        public string usu_NombreUsuario { get; set; }
        public byte[] usu_Password { get; set; }
        public string usu_Nombres { get; set; }
        public string usu_Apellidos { get; set; }
        public string usu_Correo { get; set; }
        public bool usu_EsActivo { get; set; }
        public string usu_RazonInactivo { get; set; }
        public bool usu_EsAdministrador { get; set; }
        public Nullable<byte> usu_SesionesValidas { get; set; }
        public Nullable<int> suc_Id { get; set; }
        public Nullable<short> emp_Id { get; set; }
    }
}
