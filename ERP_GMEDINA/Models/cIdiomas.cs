using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cIdiomas))]
    public partial class tbIdiomas
    {

    }
    public class cIdiomas
    {
        [Display(Name = "Id")]
        public int idi_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Display(Name = "Descripción")]
        public string idi_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool idi_Estado { get; set; }

        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Display(Name = "Razon Inactivo")]
        public string idi_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int idi_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime idi_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> idi_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> idi_FechaModifica { get; set; }

    }
}