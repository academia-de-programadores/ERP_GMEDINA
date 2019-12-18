using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialSalidas_Empleados))]
    public class cHistorialSalidas_Empleados
    {
        [Display(Name = "Identidad")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
        public int Id { get; set; }
        public string Razon { get; set; }
    }
}