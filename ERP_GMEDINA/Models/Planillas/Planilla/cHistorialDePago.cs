using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialDePago))]
    public partial class tbHistorialDePago
    {

    }

    public class cHistorialDePago
    {        
        public int hipa_IdHistorialDePago { get; set; }
        public int emp_Id { get; set; }
        public decimal hipa_SueldoNeto { get; set; }


        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [DataType(DataType.Date)]
        public System.DateTime hipa_FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [DataType(DataType.Date)]
        public System.DateTime hipa_FechaFin { get; set; }

        public System.DateTime hipa_FechaPago { get; set; }
        public int hipa_Anio { get; set; }
        public int hipa_Mes { get; set; }
        public int peri_IdPeriodo { get; set; }
        public int hipa_UsuarioCrea { get; set; }
        public System.DateTime hipa_FechaCrea { get; set; }
        public Nullable<int> hipa_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hipa_FechaModifica { get; set; }
    }
}