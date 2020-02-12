using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cFasesReclutamiento))]
    public partial class tbFasesReclutamiento
    {

    }
    public class cFasesReclutamiento
    {
        [Display(Name = "Id")]
        public int fare_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string fare_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool fare_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string fare_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int fare_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime fare_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> fare_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> fare_FechaModifica { get; set; }
    }
}