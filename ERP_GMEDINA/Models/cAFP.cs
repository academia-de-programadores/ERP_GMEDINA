using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cAFP))]
    public partial class tbAFP
    {
    }

    public class cAFP
    {
        [Display(Name = "AFP")]
        public int afp_Id { get; set; }

        [StringLength(100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "El campo AFP es Requerido")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Texto válido sin Números")]
        [Display(Name = "AFP")]
        public string afp_Descripcion { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Aporte no puede ser menor de 0 dígitos, ni mayor que 10 dígitos")]
        [Required(ErrorMessage = "El campo Aporte Mínimo es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Aporte Mínimo")]
        public decimal afp_AporteMinimoLps { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Interés por Aporte no puede ser menor de 0 dígitos, ni mayor que 10 dígitos")]
        [Required(ErrorMessage = "El campo Interés por Aporte es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Interés por Aporte")]
        public decimal afp_InteresAporte { get; set; }

        [Range(0.01, 9999999999.99, ErrorMessage = "El Interés Anual no puede ser menor de 0 dígitos, ni mayor que 10 dígitos")]
        [Required(ErrorMessage = "El campo Interés Anual es Requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Interés Anual")]
        public decimal afp_InteresAnual { get; set; }

        [Required(ErrorMessage = "El campo Tipo Deducción es Requerido")]
        [Display(Name = "Tipo Deducción")]
        public int tde_IdTipoDedu { get; set; }

        [Display(Name = "Creado por")]
        public int afp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime afp_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> afp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> afp_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool afp_Activo { get; set; }

    }
}