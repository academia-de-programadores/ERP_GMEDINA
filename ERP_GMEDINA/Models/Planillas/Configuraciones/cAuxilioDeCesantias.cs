using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cAuxilioDeCesantias))]
    public partial class tbAuxilioDeCesantias
    {

    }

    public partial class cAuxilioDeCesantias
    {
        [Display(Name = "Número Auxilio Cesantia")]
        public int aces_IdAuxilioCesantia { get; set; }
        [Display(Name = "Rango Inicio (Meses)")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int aces_RangoInicioMeses { get; set; }
        [Display(Name = "Rango Fin (Meses)")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int aces_RangoFinMeses { get; set; }
        [Display(Name = "Auxilio Cesantia (Dias)")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int aces_DiasAuxilioCesantia { get; set; }
        [Display(Name = "Usuario Creación")]
        public int aces_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        public System.DateTime aces_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        public Nullable<int> aces_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        public Nullable<System.DateTime> aces_FechaModifica { get; set; }
        [Display(Name = "Estado")]
        public bool aces_Activo { get; set; }

        //public virtual tbUsuario tbUsuario { get; set; }
        //public virtual tbUsuario tbUsuario1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tbLiquidaciones> tbLiquidaciones { get; set; }
    }
}