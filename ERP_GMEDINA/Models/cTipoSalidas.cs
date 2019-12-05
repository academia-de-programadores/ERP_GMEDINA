using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoSalidas))]
    public partial class tbTipoSalidas
    {
    }
    public class cTipoSalidas
    { 
        [Display(Name = "Id")]
        public int tsal_Id { get; set; }
        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string tsal_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool tsal_Estado { get; set; }
        [Display(Name = "Razon para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string tsal_RazonInactivo { get; set; }
    }
}