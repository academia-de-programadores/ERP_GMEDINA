using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cDeduccionesExtraordinarias))]
    public partial class tbDeduccionesExtraordinarias
    {
    }

    public class cDeduccionesExtraordinarias
    {
        [Display(Name = "Id Deducciones Extraordinarias")]
        public int dex_IdDeduccionesExtra { get; set; }

        [Required(ErrorMessage = "El campo Equipo Empleado es requerido")]
        [Display(Name = "Equipo Empleado")]
        public int eqem_Id { get; set; }

        [Display(Name = "Deducción")]
        public int cde_IdDeducciones { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor a 0 dígitos, a mayor de 10 dígitos")]
        [Required(ErrorMessage = "El campo Monto Inicial es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Monto Inicial")]
        public decimal dex_MontoInicial { get; set; }

        [Range(0.00, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor a 0 dígitos, ni mayor a 10 dígitos")]
        [Required(ErrorMessage = "El campo Monto Restante es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Monto Restante")]
        public decimal dex_MontoRestante { get; set; }

        [MaxLength (100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "El campo Observaciones es Requerido")]
        [Display(Name = "Observaciones")]
        public string dex_ObservacionesComentarios { get; set; }


        [Range(0.00, 9999999999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor a 0 dígitos, ni mayor a 10 dígitos")]
        [Required(ErrorMessage = "El campo Cuota es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Cuota")]
        public decimal dex_Cuota { get; set; }

        [Display(Name = "Creado por")]
        public int dex_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime dex_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> dex_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> dex_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool dex_Activo { get; set; }

    }
}