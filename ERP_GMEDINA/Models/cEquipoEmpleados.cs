using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEquipoEmpleados))]
    public partial class tbEquipoEmpleados { }

    public class cEquipoEmpleados
    {
        [Display(Name = "Fecha Entrega")]
        public System.DateTime eqem_Fecha { get; set; }        
    }
}