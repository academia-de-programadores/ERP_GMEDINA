using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
      [MetadataType(typeof(cCompetencias))]
    
    public partial class tbCompetencias
    {
    }
    public class cCompetencias
    {
        [Display(Name = "ID")]
        public int comp_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Competencia")]
        public string comp_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool comp_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string comp_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int comp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime comp_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> comp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> comp_FechaModifica { get; set; }

    }
}