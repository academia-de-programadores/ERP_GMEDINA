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
        [Display(Name = "ID Tipo permiso")]
        public int tper_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(25, ErrorMessage = "Excedió el número máximo de carácteres")]
        public string tper_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool tper_Estado { get; set; }

        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres")]
        [Display(Name = "Razon de inactivación")]
        public string tper_RazonInactivo { get; set; }

        [Display(Name = "Registrado por")]
        public int tper_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de registro")]
        public System.DateTime tper_FechaCrea { get; set; }

        [Display(Name = "Modificado por:")]
        public Nullable<int> tper_UsuarioModifica { get; set; }

        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> tper_FechaModifica { get; set; }

        [Display(Name = "Usuario")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Usuario")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}