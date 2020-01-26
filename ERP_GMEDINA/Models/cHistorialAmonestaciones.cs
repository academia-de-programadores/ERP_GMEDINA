using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialAmonestaciones))]
    public partial class tbHistorialAmonestaciones
    {

    }
    public class cHistorialAmonestaciones
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(150, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Display(Name = "Observación")]
        public string hamo_Observacion { get; set; }
        [Display(Name = "Razón Inactivo")]
        public string hamo_RazonInactivo { get; set; }
    }
}