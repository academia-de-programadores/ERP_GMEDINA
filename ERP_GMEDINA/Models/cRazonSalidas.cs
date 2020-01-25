using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cRazonSalidas))]
    public partial class tbRazonSalidas
    {
    }
    public class cRazonSalidas
    {
        [Display(Name = "ID")]
        public int rsal_Id { get; set; }

        [Display(Name = "Razon")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string rsal_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool rsal_Estado { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Display(Name = "Razón Inactivo")]
        public string rsal_RazonInactivo { get; set; }
        [Display(Name = "Usuario Crea")]
        public int rsal_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime rsal_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> rsal_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> rsal_FechaModifica { get; set; }
    }
}