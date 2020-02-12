using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(cIngresosRPT))]
	public partial class V_Ingresos_RPT
	{

	}
	public class cIngresosRPT
	{
		[Display(Name = "Código Empleado")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int emp_Id { get; set; }

		[Display(Name = "Nombres")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string per_Nombres { get; set; }

		[Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string per_Apellidos { get; set; }

		[Display(Name = "Código Ingreso")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int cin_IdIngreso { get; set; }

		[Display(Name = "Descripcion Ingreso")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string cin_DescripcionIngreso { get; set; }

		[Display(Name = "Total a Pagar")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public Nullable<decimal> hip_TotalPagar { get; set; }

		[Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public System.DateTime hipa_FechaInicio { get; set; }

		[Display(Name = "Fecha Fin")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public System.DateTime hipa_FechaFin { get; set; }

		[Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public System.DateTime hipa_FechaPago { get; set; }

		[Display(Name = "Código Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int cpla_IdPlanilla { get; set; }

		[Display(Name = "Descripcion Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string cpla_DescripcionPlanilla { get; set; }
	}
}