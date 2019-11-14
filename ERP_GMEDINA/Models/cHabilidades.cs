using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHabilidades))]
    public partial class tbHabilidades
    {
    }
    public class cHabilidades
    {
        [Display(Name = "Id")]
        public int habi_Id { get; set; }
        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string habi_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool habi_Estado { get; set; }
        [Display(Name = "Razon para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string habi_RazonInactivo { get; set; }
    }
}