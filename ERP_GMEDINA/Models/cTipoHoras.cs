using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string tiho_Descripcion { get; set; }
        [Display(Name = "Recargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public int tiho_Recargo { get; set; }
        [Display(Name = "Estado")]
        public bool tiho_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string tiho_RazonInactivo { get; set; }
        public int tiho_UsuarioCrea { get; set; }
        public System.DateTime tiho_FechaCrea { get; set; }
        public Nullable<int> tiho_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tiho_FechaModifica { get; set; }

    }
}