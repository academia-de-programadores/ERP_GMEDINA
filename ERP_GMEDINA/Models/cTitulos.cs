using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTitulos))]
    public partial class tbTitulos
    {
    }
    public class cTitulos
    {

        [Display(Name = "ID")]
        public int titu_Id { get; set; }

        [Display(Name = "Título")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string titu_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool titu_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string titu_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int titu_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime titu_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> titu_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> titu_FechaModifica { get; set; }

        [Display(Name = "Creado por")]
        public virtual tbUsuario tbUsuario { get; set; }



    }


}