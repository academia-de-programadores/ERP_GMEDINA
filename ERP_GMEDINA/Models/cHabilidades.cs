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
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string habi_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool habi_Estado { get; set; }
        [Display(Name = "Razón para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string habi_RazonInactivo { get; set; }
    }
}