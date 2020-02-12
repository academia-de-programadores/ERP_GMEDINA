using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cTipoIncapacidades))]

    public partial class tbTipoIncapacidades
    {
    }

    public class cTipoIncapacidades
    {
        [Display(Name = "Numero")]
        public int ticn_Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(25, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string ticn_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool ticn_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        public string ticn_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int ticn_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime ticn_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> ticn_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> ticn_FechaModifica { get; set; }
    }
}
