using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDeduccionesIndividuales))]
    public partial class tbDeduccionesIndividuales
    {
    }

    public class cDeduccionesIndividuales
    {
        [Display(Name = "Número")]
        public int dei_IdDeduccionesIndividuales { get; set; }

        [StringLength(100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Display(Name = "Motivo")]
        public string dei_Motivo { get; set; }

        [Required(ErrorMessage = "Campo Empleado Requerido")]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }


        [Required(ErrorMessage = "Campo Monto Requerido")]
        [Display(Name = "Monto")]
        public decimal dei_Monto { get; set; }

        [Required(ErrorMessage = "Campo # Cuotas Requerido")]
        [Display(Name = "# Cuotas")]
        public int dei_NumeroCuotas { get; set; }

        [Required(ErrorMessage = "Campo Monto Cuota Requerido")]
        [Display(Name = "Monto de Cuota")]
        public decimal dei_MontoCuota { get; set; }

        [Display(Name = "Deduce Siempre")]
        public Nullable<bool> dei_PagaSiempre { get; set; }

        [Display(Name = "Creado por")]
        public int dei_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime dei_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> dei_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> dei_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool dei_Activo { get; set; }

        [Display(Name = "Incluir para ISR")]
        public bool dei_DeducirISR { get; set; }

    }
}
