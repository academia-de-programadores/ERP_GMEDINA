using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cUsuarios))]
    public partial class tbUsuarios
    {

    }
    public class cUsuarios
    {
        [Display(Name = "Id")]
        public int usu_Id { get; set; }

        [Display(Name = "Nombre Usuario")]
        public string usu_NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        public byte[] usu_Password { get; set; }

        [Display(Name = "Nombre ")]
        public string usu_Nombres { get; set; }

        [Display(Name = "Apellido")]
        public string usu_Apellidos { get; set; }

        [Display(Name = "Correo")]
        public string usu_Correo { get; set; }

        [Display(Name = "Es activo")]
        public bool usu_EsActivo { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string usu_RazonInactivo { get; set; }

        [Display(Name = "Es administrador")]
        public bool usu_EsAdministrador { get; set; }
        public Nullable<byte> usu_SesionesValidas { get; set; }
    }
}