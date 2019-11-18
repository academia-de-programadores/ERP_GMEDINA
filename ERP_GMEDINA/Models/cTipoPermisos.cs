using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoPermisos))]
    public partial class tbTipoPermisos
    {
    }

    public class cTipoPermisos
    {
        [Display(Name = "Id tipo permiso")]
        public int tper_Id { get; set; }

        [Display(Name = "Descripcion del tipo de permiso")]
        public string tper_Descripcion { get; set; }

        [Display(Name = "Estado del tipo de permiso")]
        public bool tper_Estado { get; set; }

        [Display(Name = "Razon del estado inactivo")]
        public string tper_RazonInactivo { get; set; }

        [Display(Name = "Registrado por:")]
        public int tper_UsuarioCrea { get; set; }

        [Display(Name = "Fecha creación")]
        public System.DateTime tper_FechaCrea { get; set; }

        [Display(Name = "Modificado por:")]
        public Nullable<int> tper_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de modificación")]
        public Nullable<System.DateTime> tper_FechaModifica { get; set; }
    }
}