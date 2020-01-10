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
        [Display(Name = "Estado")]
        public bool aude_Estado { get; set; }
        [Display(Name = "Razón Inactivo")]
        public string aude_RazonInactivo { get; set; }
        [Display(Name = "Usuario Crea")]
        public int aude_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime aude_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> aude_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> aude_FechaModifica { get; set; }
        [Display(Name = "Usuario Crea")]
        public virtual tbUsuario tbUsuario { get; set; }
        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}