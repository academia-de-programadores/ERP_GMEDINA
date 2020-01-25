using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(cDeduccionesRPT))]
	public partial class V_Deducciones_RPT
	{

	}
	public class cDeduccionesRPT
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


		[Display(Name = "Código Deducción")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int cde_IdDeducciones { get; set; }


		[Display(Name = "Descripcion Deduccion")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string cde_DescripcionDeduccion { get; set; }


		[Display(Name = "Total")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public Nullable<decimal> hidp_Total { get; set; }


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