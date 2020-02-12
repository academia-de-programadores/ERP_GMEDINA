using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cJornadas))]
    public partial class tbJornadas
    {
    }
    public class cJornadas
    {
        [Display(Name = "Id")]
        public int jor_Id { get; set; }

        [Display(Name = "Jornada")]
        [MaxLength(30, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string jor_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool jor_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]

        public string jor_RazonInactivo { get; set; }

        [Display(Name = "Usuario crea")]
        public int jor_UsuarioCrea { get; set; }

        [Display(Name = "Fecha creación")]
        public System.DateTime jor_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> jor_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> jor_FechaModifica { get; set; }

    }
}