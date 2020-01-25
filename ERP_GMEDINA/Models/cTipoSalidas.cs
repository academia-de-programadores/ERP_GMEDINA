using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoSalidas))]
    public partial class tbTipoSalidas
    {
    }
    public class cTipoSalidas
    { 
        [Display(Name = "Id")]
        public int tsal_Id { get; set; }
        [Display(Name = "Salidas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string tsal_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool tsal_Estado { get; set; }
        [Display(Name = "Razón Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string tsal_RazonInactivo { get; set; }
        [Display(Name = "Usuario Crea")]
        public int tsal_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime tsal_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tsal_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tsal_FechaModifica { get; set; }
    }
}