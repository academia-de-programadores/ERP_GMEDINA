using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{


    [MetadataType(typeof(cINFOPRPT))]
    public partial class V_INFOP_RPT
    {

    }
    public class cINFOPRPT
    {
		[Display(Name = "Codigo Empleado")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public int emp_Id { get; set; }

		[Display(Name = "Nombres")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string per_Nombres { get; set; }

		[Display(Name = "Apellidos")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string per_Apellidos { get; set; }

		[Display(Name = "Codigo Deduccion")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public int cde_IdDeducciones { get; set; }

		[Display(Name = "Deduccion")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string cde_DescripcionDeduccion { get; set; }

		[Display(Name = "Total a Deducir")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public Nullable<decimal> hidp_Total { get; set; }

		[Display(Name = "Fecha Inicio")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime hipa_FechaInicio { get; set; }

		[Display(Name = "Fecha Fin")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime hipa_FechaFin { get; set; }

		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime hipa_FechaPago { get; set; }

		[Display(Name = "Codigo Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public int cpla_IdPlanilla { get; set; }

		[Display(Name = "Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string cpla_DescripcionPlanilla { get; set; }

    }
}