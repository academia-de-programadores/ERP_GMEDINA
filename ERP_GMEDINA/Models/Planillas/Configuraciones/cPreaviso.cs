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

        
       
        [Display(Name = "Rango de Inicio del Mes")]
        [Required(ErrorMessage = "No puede dejar el campo vacio.")]
        [Range(0, 36, ErrorMessage = "El rango inicio meses debe estar entre {1} y {2}")]
        public string prea_RangoInicioMeses { get; set; }

        
      
        [Display(Name = "Rango de Fin de Mes")]
        [Required(ErrorMessage = "No puede dejar el campo vacio.")]
        [Range(1, 36, ErrorMessage = "El rango fin meses debe estar entre {1} y {2}")]
        public string prea_RangoFinMeses { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se permiten caracteres entre 0 y 9")]
        [Range(0 ,999999999, ErrorMessage = "El campo dias de preaviso debe ser mayor que cero")]
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