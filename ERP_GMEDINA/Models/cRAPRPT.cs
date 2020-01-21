using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{


    [MetadataType(typeof(cRAPRPT))]
    public partial class V_RAP_RPT
    {
    }
        public class cRAPRPT
    {

		[Display(Name = "Codigo Empleado")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public int emp_Id { get; set; }

		[Display(Name = "Nombres")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public string per_Nombres { get; set; }

		[Display(Name = "Apellidos")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public string per_Apellidos { get; set; }

		[Display(Name = "Codigo Deduccion")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public int cde_IdDeducciones { get; set; }

		[Display(Name = "Deduccion")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public string cde_DescripcionDeduccion { get; set; }

		[Display(Name = "Total a Deducir")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public Nullable<decimal> hidp_Total { get; set; }

		[Display(Name = "Fecha Inicio")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public System.DateTime hipa_FechaInicio { get; set; }

		[Display(Name = "Fecha Fin")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public System.DateTime hipa_FechaFin { get; set; }

		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public System.DateTime hipa_FechaPago { get; set; }

		[Display(Name = "Codigo Planilla")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public int cpla_IdPlanilla { get; set; }

		[Display(Name = "Planilla")]
		[Required(ErrorMessage = "Campo {0} requerido.")]
		public string cpla_DescripcionPlanilla { get; set; }
	}
}