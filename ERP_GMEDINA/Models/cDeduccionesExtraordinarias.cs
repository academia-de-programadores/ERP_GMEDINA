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


        [Range(1, 9999999999999999, ErrorMessage = "Debe elegir una opción")]
        [Display(Name = "Id Equipo Empleado")]
        public int eqem_Id { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor de 0 dígitos, ni mayor de 10 dígitos")]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Monto Inicial")]
        public decimal dex_MontoInicial { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor de 0 dígitos, ni mayor de 10 dígitos")]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Monto Restante")]
        public decimal dex_MontoRestante { get; set; }

        [MaxLength (100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Observaciones")]
        public string dex_ObservacionesComentarios { get; set; }

        [Range(1, 9999999999999999, ErrorMessage = "Debe elegir una opción")]
        [Display(Name = "Id Deducción")]
        public int cde_IdDeducciones { get; set; }

       /*[Display(Name = "Deducción")]
        public string cde_DescripcionDeduccion { get; set; }*/

        [Range(0.01, 9999999999999999.99, ErrorMessage = "El Monto Inicial no puede ser menor de 0 dígitos, ni mayor que 10 dígitos")]
        [Required(ErrorMessage = "Campo Requerido")]
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