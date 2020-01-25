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
		[Display(Name = "Codigo Empleado")]
		public int emp_Id { get; set; }
		[Display(Name = "Nombres")]
		public string per_Nombres { get; set; }
		[Display(Name = "Apellidos")]
		public string per_Apellidos { get; set; }
		[Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public System.DateTime dcm_FechaPago { get; set; }
		[Display(Name = "Monto")]
		public Nullable<decimal> dcm_Monto { get; set; }
		[Display(Name = "Cuenta Bancaria")]
		public string emp_CuentaBancaria { get; set; }
		[Display(Name = "Codigo Pago")]
		public string dcm_CodigoPago { get; set; }
        [Display(Name = "Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int cpla_IdPlanilla { get; set; }
		[Display(Name = "Tipo Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string cpla_DescripcionPlanilla { get; set; }


	}
}