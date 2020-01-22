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
		[Display(Name = "Codigo Empleado")]
		public Nullable<int> emp_Id { get; set; }
		[Display(Name = "Nombres")]
		public string per_Nombres { get; set; }
		[Display(Name = "Apellidos")]
		public string per_Apellidos { get; set; }
		[Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public System.DateTime dtm_FechaPago { get; set; }
		[Display(Name = "Monto")]
		public Nullable<decimal> dtm_Monto { get; set; }
		[Display(Name = "Cuenta Bancaria")]
		public string emp_CuentaBancaria { get; set; }
		[Display(Name = "Codigo Pago")]
		public string dtm_CodigoPago { get; set; }
		[Display(Name = "Tipo Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string cpla_DescripcionPlanilla { get; set; }

        [Display(Name = "Planilla")]
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int cpla_IdPlanilla { get; set; }
    }
}