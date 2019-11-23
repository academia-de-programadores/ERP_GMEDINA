using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEmpresas))]
    public partial class tbHabilidades
    {
    }
    public class cEmpresas
    {
        [Display(Name = "Id")]
        public int empr_Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string habi_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool empr_Estado { get; set; }
        [Display(Name = "Razón para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string empr_RazonInactivo { get; set; }
    }
}