using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoDeduccion))]
    public partial class tbTipoDeduccion
    {
    }

    public class cTipoDeduccion
    {
        [Display(Name = "ID Tipo Deduccion")]
        public int tde_IdTipoDedu { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo descripción no puede quedar vacío")]
        public string tde_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tde_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime tde_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tde_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> tde_FechaModifica { get; set; }

        [Display(Name = "Es Activo")]
        public bool tde_Activo { get; set; }
    }
}

