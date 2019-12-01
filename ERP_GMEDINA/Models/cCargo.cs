using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCargo))]
    public partial class tbCargos
    {

    }
    public class cCargo
    {
        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string car_Descripcion { get; set; }
    }
}