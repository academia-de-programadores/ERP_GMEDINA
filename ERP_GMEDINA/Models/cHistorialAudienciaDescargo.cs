using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialAudienciaDescargo))]
    public partial class tbHistorialAudienciaDescargo
    {

    }
    public class cHistorialAudienciaDescargo
    {

       [Display(Name ="Motivo")]
       [Required(AllowEmptyStrings =false, ErrorMessage ="El campo \"{0}\" es requerido")]
       [MaxLength(100,ErrorMessage ="Excedio el número maximo de caracteres")]
        public string aude_Descripcion { get; set; }
        [Display(Name = "Fecha Audiencia")]
        public System.DateTime aude_FechaAudiencia { get; set; }
        [Display(Name = "Testigo")]
        public bool aude_Testigo { get; set; }
        [Display(Name = "Direccion Archivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido")]
        [MaxLength(100, ErrorMessage = "Excedio el número maximo de caracteres")]
        public string aude_DireccionArchivo { get; set; }
    }
}