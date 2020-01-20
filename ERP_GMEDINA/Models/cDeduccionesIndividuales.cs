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
        [Required(ErrorMessage = "Campo Motivo Requerido")]
        [Display(Name = "Motivo")]
        public string dei_Motivo { get; set; }

        [Required(ErrorMessage = "Campo Empleado Requerido")]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }


        [Required(ErrorMessage = "Campo Monto Inicial Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Inicial")]
        public decimal dei_MontoInicial { get; set; }

        [Required(ErrorMessage = "Campo Monto Restante Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Restante")]
        public decimal dei_MontoRestante { get; set; }

        [Required(ErrorMessage = "Campo Cuota Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Cuota")]
        public decimal dei_Cuota { get; set; }

        [Display(Name = "Siempre Deduce")]
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

    }
}
