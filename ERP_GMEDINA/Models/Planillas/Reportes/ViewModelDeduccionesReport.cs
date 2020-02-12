using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	public class ViewModelDeduccionesReport
	{
		public int emp_Id { get; set; }
		public string per_Nombres { get; set; }
		public string per_Apellidos { get; set; }
		public int cde_IdDeducciones { get; set; }
		public string cde_DescripcionDeduccion { get; set; }
		public decimal? hidp_Total { get; set; }
		public DateTime hipa_FechaInicio { get; set; }
		public DateTime hipa_FechaFin { get; set; }
		public string cpla_DescripcionPlanilla { get; set; }
	}

}