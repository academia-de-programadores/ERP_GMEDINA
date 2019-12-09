using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cPersonas))]
    public partial class tbPersonas
    {

    }
    public class cPersonas
    {
        [Display(Name = "ID ")]
        public int per_Id { get; set; }
        [Display(Name = "Identidad ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(13, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Identidad { get; set; }
        [Display(Name = "Nombre Completo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        [Display(Name = "Fecha Nacimiento ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public System.DateTime per_FechaNacimiento { get; set; }
        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(1, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Sexo { get; set; }
        [Display(Name ="Edad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(3, ErrorMessage = "Exedio el numero maximo")]
        public Nullable<int> per_Edad { get; set; }
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo")]
        public int nac_Id { get; set; }
        [Display(Name = "Direccion ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Direccion { get; set; }
        [Display(Name = "Telefono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(6, ErrorMessage = "Exedio el numero maximo")]
        public string per_Telefono { get; set; }
        [Display(Name = "Correo Electronico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_CorreoElectronico { get; set; }
        [Display(Name = "Estado Civil")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_EstadoCivil { get; set; }
        [Display(Name = "Tipo de Sangre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_TipoSangre { get; set; }
        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(1, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public bool per_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_RazonInactivo { get; set; }
        public int per_UsuarioCrea { get; set; }
        public System.DateTime per_FechaCrea { get; set; }
        public Nullable<int> per_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> per_FechaModifica { get; set; }
    }
}