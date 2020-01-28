using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cEquipoTrabajo))]
    public partial class tbEquipoTrabajo
    {
        
    }
    public class cEquipoTrabajo
    {
        [Display(Name = "Número")]
        public int eqtra_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Codigo")]
        [MaxLength(25, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string eqtra_Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Equipo")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string eqtra_Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Observacion")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string eqtra_Observacion { get; set; }

        [Display(Name = "Estado")]
        public string eqtra_Estado { get; set; }


    }
}