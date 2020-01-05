using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class cHistorialPermisos_Empleados
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Razon")]
        public string Razon { get; set; }
    }
}