using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

	[MetadataType(typeof(cDecimoTercerMesRPT))]
	public partial class V_DecimoTercerMes_RPT
	{

	}

	public class cDecimoTercerMesRPT
	{
		public int dtm_IdDecimoTercerMes { get; set; }
		public Nullable<int> emp_Id { get; set; }
		public string per_Nombres { get; set; }
		public string per_Apellidos { get; set; }
		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime dtm_FechaPago { get; set; }
		public Nullable<decimal> dtm_Monto { get; set; }
		public string emp_CuentaBancaria { get; set; }
		public string dtm_CodigoPago { get; set; }
		[Display(Name = "Tipo Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string cpla_DescripcionPlanilla { get; set; }
	}
}