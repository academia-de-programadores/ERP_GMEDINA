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
        [Display(Name ="ID ISR")]
        public int isr_Id { get; set; }
        [Display(Name = "Rango inicial")]
        public decimal isr_RangoInicial { get; set; }
        [Display(Name = "Rango final")]
        public decimal isr_RangoFinal { get; set; }
        [Display(Name = "Porcentaje")]
        public decimal isr_Porcentaje { get; set; }

        [Display(Name = "Tipo deduccion")]
        public int tde_IdTipoDedu { get; set; }
        [Display(Name = "Creado por")]
        public int isr_UsuarioCrea { get; set; }
        [Display(Name = "Fecha creacion")]
        public System.DateTime isr_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> isr_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modificacion")]
        public Nullable<System.DateTime> isr_FechaModifica { get; set; }
        [Display(Name = "Activo")]
        public bool isr_Activo { get; set; }

        [Display(Name = "Creado por")]
        public virtual tbUsuario tbUsuario { get; set; }
        [Display(Name = "Modificado por")]
        public virtual tbUsuario tbUsuario1 { get; set; }
        [Display(Name = "Tipo deduccin")]
        public virtual tbTipoDeduccion tbTipoDeduccion { get; set; }

    }
}