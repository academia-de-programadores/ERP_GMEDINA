using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cPersonas))]
    public partial class tbPersonas
    {

    }
    public class cPersonas
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Número")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public int per_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Identidad ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Identidad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Nombres ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Nombres { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Apellidos ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Apellidos { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Fecha de Nacimiento ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public System.DateTime per_FechaNacimiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Sexo ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Sexo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Edad ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public Nullable<int> per_Edad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Nacionalidad ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public int nac_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Dirección ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Teléfono ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Correo Electrónico ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_CorreoElectronico { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Estado Civil ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_EstadoCivil { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [Display(Name = "Tipo de Sangre ")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        public string per_TipoSangre { get; set; }

        [Display(Name = "Estado ")]
        public bool per_Estado { get; set; }

        [Display(Name = "Razon Inactivo ")]
        public string per_RazonInactivo { get; set; }
        public int per_UsuarioCrea { get; set; }
        public System.DateTime per_FechaCrea { get; set; }
        public Nullable<int> per_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> per_FechaModifica { get; set; }
    }
    
}