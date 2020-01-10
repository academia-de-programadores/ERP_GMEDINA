using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialAmonestaciones))]
    public partial class tbHistorialAmonestaciones
    {

    }
    public class cHistorialAmonestaciones
    {
       
       
        [Display(Name = "Observación")]
        public string hamo_Observacion { get; set; }
        [Display(Name = "Razón Inactivo")]
        public string hamo_RazonInactivo { get; set; }
    }
}