using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(cDecimoCuartoMesRPT))]
	public partial class V_DecimoCuartoMes_RPT
	{

	}


	public class cDecimoCuartoMesRPT
	{
		public int dcm_IdDecimoCuartoMes { get; set; }
		public int emp_Id { get; set; }
		public string per_Nombres { get; set; }
		public string per_Apellidos { get; set; }
		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime dcm_FechaPago { get; set; }
		public Nullable<decimal> dcm_Monto { get; set; }
		public string emp_CuentaBancaria { get; set; }
		public string dcm_CodigoPago { get; set; }
		public int cpla_IdPlanilla { get; set; }
		[Display(Name = "Tipo Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string cpla_DescripcionPlanilla { get; set; }
	}
}