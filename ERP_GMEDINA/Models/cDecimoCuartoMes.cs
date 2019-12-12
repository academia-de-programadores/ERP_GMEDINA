using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	[MetadataType(typeof(cDecimoCuartoMes))]
	public partial class V_DecimoCuartoMes
	{

	}

	public class cDecimoCuartoMes
	{
		[Display(Name = "Codigo Empleado")]
		public int emp_Id { get; set; }
		[Display(Name = "Nombre")]
		public string Nombre { get; set; }
		[Display(Name = "Apellido")]
		public string Apellido { get; set; }
		[Display(Name = "Cargo")]
		public string Cargo { get; set; }
		[Display(Name = "Tipo Planilla")]
		public string Planilla { get; set; }
		[Display(Name = "Cuenta Bancaria")]
		public string CuentaBancaria { get; set; }
		[Display(Name = "Monto")]
		public Nullable<decimal> Monto { get; set; }
	}
}