using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCargos))]
    public partial class tbCargos
    {

    }
    public class cCargos
    {
        [Display(Name = "ID")]
        public int car_Id { get; set; }

        [Display(Name = "Cargo")]
        public string car_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool car_Estado { get; set; }

        [Display(Name = "Razon Inacativo")]
        public string car_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int car_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime car_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificado")]
        public Nullable<int> car_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> car_FechaModifica { get; set; }

        [Display(Name = "Usuario Crea")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}