using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEmpresas))]
    public partial class tbEmpresas
    {

    }
        
    public class cEmpresas
    {
        [Display(Name = "Id")]
        public int empr_Id { get; set; } 

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Empresa")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string empr_Nombre { get; set; }
        [Display(Name = "Logo")]
        public string empr_Logo { get; set; }
        [Display(Name ="Estado")]
        public Nullable<bool> empr_Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Razón Inactivo")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string empr_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int empr_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime empr_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable <int> empr_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> empr_FechaModifica { get; set; }
    }
}