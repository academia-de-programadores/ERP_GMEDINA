using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cAcumuladosISR))]
    public partial class tbAcumuladosISR
    {
    }
    public class cAcumuladosISR
    {
        [Display(Name = "ID acumulados ISR")]
        [Required(ErrorMessage ="Este campo requerido")]
        public int aisr_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Campo {0} requerido")]
        
        public string aisr_Descripcion { get; set; }
        [Display(Name = "Monto")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Este campo es un decimal requerido.")]
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public decimal aisr_Monto { get; set; }
        [Display(Name = "Creado por")]
        public int aisr_UsuarioCrea { get; set; }
        [Display(Name = "Fecha creacion")]
        public System.DateTime aisr_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> aisr_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modificacion")]
        public Nullable<System.DateTime> aisr_FechaModifica { get; set; }
        [Display(Name = "Activo")]
        public bool aisr_Activo { get; set; }

        [Display(Name = "Creado por")]
        public virtual tbUsuario tbUsuario { get; set; }
        [Display(Name = "Modificado por")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}