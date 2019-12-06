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

        [Display(Name = "Descripcion")]
        public string jor_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool jor_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string jor_RazonInactivo { get; set; }

        [Display(Name = "Usuario crea")]
        public int jor_UsuarioCrea { get; set; }

        [Display(Name = "Fecha creacion")]
        public System.DateTime jor_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> jor_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> jor_FechaModifica { get; set; }

    }
}