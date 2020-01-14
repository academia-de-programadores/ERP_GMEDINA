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
        [Required(ErrorMessage = "El campo Motivo es Requerido")]
        [Display(Name = "Motivo")]
        public string dei_Motivo { get; set; }

        [Required(ErrorMessage = "El campo Empleado es requerido")]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor a 0 dígitos, ni mayor a 10 dígitos")]
        [Required(ErrorMessage = "El campo Monto Inicial es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Monto Inicial")]
        public decimal dei_MontoInicial { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "La Cuota no puede ser menor a 0 dígitos, ni mayor a 10 dígitos")]
        [Required(ErrorMessage = "El campo Monto Restante es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Monto Restante")]
        public decimal dei_MontoRestante { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor a 0 dígitos, ni mayor a 10 dígitos")]
        [Required(ErrorMessage = "El campo Cuota es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
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