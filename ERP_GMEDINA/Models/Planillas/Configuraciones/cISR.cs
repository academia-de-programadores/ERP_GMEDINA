using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cISR))]
    public partial class tbISR { }


    public class cISR
    {
        [Display(Name ="Número")]
        public int isr_Id { get; set; }
        [Display(Name = "Rango inicial")]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public decimal isr_RangoInicial { get; set; }
        [Display(Name = "Rango final")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal isr_RangoFinal { get; set; }
        [Display(Name = "Porcentaje")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal isr_Porcentaje { get; set; }

        [Display(Name = "Tipo deducción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int tde_IdTipoDedu { get; set; }
        [Display(Name = "Creado por")]
        public int isr_UsuarioCrea { get; set; }
        [Display(Name = "Fecha creación")]
        public System.DateTime isr_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> isr_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> isr_FechaModifica { get; set; }
        [Display(Name = "Activo")]
        public bool isr_Activo { get; set; }

        [Display(Name = "Creado por")]
        public virtual tbUsuario tbUsuario { get; set; }
        [Display(Name = "Modificado por")]
        public virtual tbUsuario tbUsuario1 { get; set; }
        [Display(Name = "Tipo deducción")]
        public virtual tbTipoDeduccion tbTipoDeduccion { get; set; }

    }
}