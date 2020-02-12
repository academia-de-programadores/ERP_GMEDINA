using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialPermisos_Empleados))]
    //public partial class tbEmpleados
    //{
    //    public System.DateTime hper_fechaInicio { get; set; }
    //    public System.DateTime hper_fechaFin { get; set; }
    //    public bool hper_Justificado { get; set; }
    //}
    public class cHistorialPermisos_Empleados
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
        [Display(Name = "Empleados")]
        public int Id { get; set; }
        [Display(Name = "Razon")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string Razon { get; set; }
        [Display(Name = "Fecha salida")]
        public System.DateTime hper_fechaInicio { get; set; }
        [Display(Name = "Fecha Regreso")]
        public System.DateTime hper_fechaFin { get; set; }
        [Display(Name = "Tiene justificante")]
        public bool hper_Justificado { get; set; }
    }
}