using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(cDecimoCuartoMesPagado))]
	public partial class V_DecimoCuartoMes_Pagados
	{

	}

	public class cDecimoCuartoMesPagado
	{
		[Display(Name = "Codigo Empleado")]
		public int emp_Id { get; set; }
		[Display(Name = "Nombres")]
		public string per_Nombres { get; set; }
		[Display(Name = "Apellidos")]
		public string per_Apellidos { get; set; }
		[Display(Name = "Cargo")]
		public string car_Descripcion { get; set; }
		[Display(Name = "Tipo Planilla")]
		public string cpla_DescripcionPlanilla { get; set; }
		[Display(Name = "Cuenta Bancaria")]
		public string emp_CuentaBancaria { get; set; }
		[Display(Name = "Monto")]
		public Nullable<decimal> dcm_Monto { get; set; }
		[Display(Name = "Fecha Pago")]
		public System.DateTime dcm_FechaPago { get; set; }
	}
}