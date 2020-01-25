using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Librería que se agrega
using System.Linq;
using System.Web;


namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cNacionalidades))]

    public partial class tbNacionalidades
    {

    }

    public class cNacionalidades
    {
        [Display(Name = "ID")]
        public int nac_Id { get; set; }

        [Display(Name = "Nacionalidad")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido.")]
        public string nac_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool nac_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido.")]
        public string nac_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]

        public int nac_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime nac_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> nac_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> nac_FechaModifica { get; set; }
    }
}