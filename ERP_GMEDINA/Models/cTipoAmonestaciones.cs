using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoAmonestaciones))]
    public partial class tbTipoAmonestaciones
    {

    }
    public class cTipoAmonestaciones
    {
        [Display(Name ="ID ")]
        public int tamo_Id { get; set; }

        [Display (Name ="Descripcion")]
        public string tamo_Descripcion { get; set; }

        [Display(Name ="Estado")]
        public bool tamo_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string tamo_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tamo_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tamo_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tamo_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tamo_FechaModifica { get; set; }

    }
}