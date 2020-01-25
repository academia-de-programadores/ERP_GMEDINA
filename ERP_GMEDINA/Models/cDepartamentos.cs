using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDepartamentos))]
    public partial class tbDepartamentos
    {
        public string car_Descripcion { get; set; }
        public string Accion { get; set; }
    }
    public class cDepartamentos
    {
        [Display(Name = "Id")]
        public int depto_Id { get; set; }
        public int area_Id { get; set; }
        public int car_Id { get; set; }
        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string depto_Descripcion { get; set; }
        public bool depto_Estado { get; set; }
        [Display(Name = "Razon para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string depto_RazonInactivo { get; set; }
        public int depto_UsuarioCrea { get; set; }
        public System.DateTime depto_Fechacrea { get; set; }
        public Nullable<int> depto_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> depto_FechaModifica { get; set; }

        //Propiedades extra...
        [Display(Name = "Cargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string car_Descripcion { get; set; }
        public string Accion { get; set; }
    }
}