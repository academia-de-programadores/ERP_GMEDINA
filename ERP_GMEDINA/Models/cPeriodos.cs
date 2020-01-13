using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cPeriodos))]
    public partial class tbPeriodos
    {
    }
    public class cPeriodos
    {

        [Display(Name = "Número")]
        public int peri_IdPeriodo { get; set; }

        [Display(Name = "Estado")]
        public bool peri_Activo { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "No puede dejar el campo {0} vacio.")]
        public string peri_DescripPeriodo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int peri_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime peri_FechaCrea { get; set; }

        public Nullable<int> peri_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> peri_FechaModifica { get; set; }

    }
}