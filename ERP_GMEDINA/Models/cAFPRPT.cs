﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cAFPRPT))]
    public partial class V_AFP_RPT
    {

    }
    public class cAFPRPT
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

		[Display(Name = "Total AFP")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public decimal hipa_AFP { get; set; }

		[Display(Name = "Codigo Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public int cpla_IdPlanilla { get; set; }

		[Display(Name = "Planilla")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public string cpla_DescripcionPlanilla { get; set; }

		[Display(Name = "Fecha Pago")]
		[Required(ErrorMessage = "No puede dejar campos vacios.")]
		public System.DateTime hipa_FechaPago { get; set; }

    }
}