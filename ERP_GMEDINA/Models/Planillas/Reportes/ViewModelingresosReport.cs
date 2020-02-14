using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	public class ViewModelingresosReport
	{
		public int emp_Id { get; set; }
		public string per_Nombres { get; set; }
		public string per_Apellidos { get; set; }
		public int cin_IdIngreso { get; set; }
		public string cin_DescripcionIngreso { get; set; }
		public decimal? hip_TotalPagar { get; set; }
		public DateTime hipa_FechaInicio { get; set; }
		public DateTime hipa_FechaFin { get; set; }
		public string cpla_DescripcionPlanilla { get; set; }
	}
}