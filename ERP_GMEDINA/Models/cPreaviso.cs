using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cPreaviso))]
    public partial class tbPreaviso
    {
    }
    public class cPreaviso
    {

        [Display(Name = "Número")]
        public int prea_IdPreaviso { get; set; }

        //[RegularExpression(@"^[0-9]+(\.[0-9])$", ErrorMessage = "No puede ingresar un carácter que no sea numérico!")]
        [Range(0.00, 999999.99, ErrorMessage = "La cantidad no puede ser menor de 0 dígitos, ni mayor que 6 dígitos")]
        [Display(Name = "Rango de Inicio del Mes")]
        [Required(ErrorMessage = "No puede dejar el campo vacio.")]
        public string prea_RangoInicioMeses { get; set; }

        //[RegularExpression(@"^[0-9]+(\.[0-9])$", ErrorMessage = "No puede ingresar un carácter que no sea numérico!")]
        [Range(0.00, 999999.99, ErrorMessage = "La cantidad no puede ser menor de 0 dígitos, ni mayor que 6 dígitos")]
        [Display(Name = "Rango de Fin de Mes")]
        [Required(ErrorMessage = "No puede dejar el campo vacio.")]
        public string prea_RangoFinMeses { get; set; }

        //[RegularExpression(@"^[0-9]+(\.[0-9])$", ErrorMessage = "No puede ingresar un carácter que no sea numérico!")]
        [Range(0.00, 999999.99, ErrorMessage = "La cantidad no puede ser menor de 0 dígitos, ni mayor que 6 dígitos")]
        [Display(Name = "Días de Preaviso")]
        [Required(ErrorMessage = "No puede dejar el campo vacio.")]
        public string prea_DiasPreaviso { get; set; }

        [Display(Name = "Usuario Crea")]
        public int prea_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime prea_FechaCrea { get; set; }

        public Nullable<int> prea_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prea_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool prea_Activo { get; set; }

    }
}