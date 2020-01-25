using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoMonedas))]
    public partial class tbTipoMonedas
        {

        }

        public class cTipoMonedas
        {
            [Display(Name = "Id")]
            public int tmon_Id { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
            [Display(Name = "Moneda")]
            [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
            public string tmon_Descripcion { get; set; }

            [Display(Name = "Estado")]
            public Nullable<bool> tmon_Estado { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
            [Display(Name = "Razón Inactivo")]
            [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
            public string tmon_RazonInactivo { get; set; }

            [Display(Name = "Usuario Crea")]
            public int tmon_UsuarioCrea { get; set; }

            [Display(Name = "Fecha Crea")]
            public System.DateTime tmon_FechaCrea { get; set; }

            [Display(Name = "Usuario Modifica")]
            public Nullable<int> tmon_UsuarioModifica { get; set; }

            [Display(Name = "Fecha Modifica")]
            public Nullable<System.DateTime> tmon_FechaModifica { get; set; }
        }
    }

