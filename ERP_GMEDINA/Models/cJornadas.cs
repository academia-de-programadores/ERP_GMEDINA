using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cJornadas))]
    public partial class tbJornadas
    {

    }
    public class cJornadas
    {
        [Display(Name = "Id")]
        public int jor_Id { get; set; }
        [Display(Name = "Jornadas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string jor_Descripcion { get; set; }
        public bool jor_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string jor_RazonInactivo { get; set; }
        public int jor_UsuarioCrea { get; set; }
        public System.DateTime jor_FechaCrea { get; set; }
        public Nullable<int> jor_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> jor_FechaModifica { get; set; }
    }
}