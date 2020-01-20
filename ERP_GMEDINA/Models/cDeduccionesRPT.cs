using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(V_Deducciones_RPT))]
	public partial class V_Deducciones_RPT
	{

	}
	public class cDeduccionesRPT
	{
		[Display(Name = "Codigo Empleado")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public int emp_Id { get; set; }


		[Display(Name = "Nombres")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string per_Nombres { get; set; }


		[Display(Name = "Apellidos")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string per_Apellidos { get; set; }


		[Display(Name = "Codigo Deduccion")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public int cde_IdDeducciones { get; set; }


		[Display(Name = "Descripcion Deduccion")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string cde_DescripcionDeduccion { get; set; }


		[Display(Name = "Total")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public Nullable<decimal> hidp_Total { get; set; }


		[Display(Name = "Fecha Inicio")]
		[Required(ErrorMessage = "campo Fecha Inicio es requerido")]
		public System.DateTime hipa_FechaInicio { get; set; }


		[Display(Name = "Fecha Fin")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public System.DateTime hipa_FechaFin { get; set; }


		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public System.DateTime hipa_FechaPago { get; set; }


		[Display(Name = "Codigo Planilla")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public int cpla_IdPlanilla { get; set; }


		[Display(Name = "Descripcion Planilla")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string cpla_DescripcionPlanilla { get; set; }	
	}
}