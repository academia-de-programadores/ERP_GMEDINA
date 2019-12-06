using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCatalogoIngresos))]
    public partial class tbCatalogoDeIngresos
    {
    }
    public class cCatalogoIngresos
    {
        [Display(Name = "ID Ingresos")]
        public int cin_IdIngreso { get; set; }

        [Required]
        [Display(Name = "Descripcion Ingresos")]
        public string cin_DescripcionIngreso { get; set; }

        [Display(Name = "Usuario Crea")]
        public int cin_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime cin_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> cin_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> cin_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool cin_Activo { get; set; }

    }
}