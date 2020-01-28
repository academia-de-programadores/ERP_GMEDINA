using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoHoras))]
    public partial class tbTipoHoras
    {

    }
    public class cTipoHoras
    {
        [Display(Name = "Id")]
        public int tiho_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Hora")]
        [MaxLength(25, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string tiho_Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Recargo")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public int tiho_Recargo { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> tiho_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string tiho_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tiho_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tiho_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tiho_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tiho_FechaModifica { get; set; }
    }

}