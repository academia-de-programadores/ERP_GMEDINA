using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDepartamentos))]
    public partial class tbDepartamentos
    {

    }
    public class cDepartamentos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Id")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public int depto_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Area Id")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public int area_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Cargo Id")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public int car_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Departamento")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public string depto_Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public bool depto_Estado { get; set; }
        public string depto_RazonInactivo { get; set; }
        public int depto_UsuarioCrea { get; set; }
        public System.DateTime depto_Fechacrea { get; set; }
        public Nullable<int> depto_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> depto_FechaModifica { get; set; }

    }
}